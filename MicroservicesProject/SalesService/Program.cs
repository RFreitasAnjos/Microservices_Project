using Microsoft.EntityFrameworkCore;
using FluentValidation.AspNetCore;
using Polly;
using Polly.Extensions.Http;
using SalesService.Data;
using SalesService.Integrations;
using SalesService.Services;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// Controllers + FluentValidation
builder.Services.AddControllers()
    .AddJsonOptions(o =>
    {
        o.JsonSerializerOptions.PropertyNamingPolicy = null; 
    });
builder.Services.AddFluentValidationAutoValidation();

builder.Services.AddDbContext<SalesContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen( c=>
{
    var xml = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xml);
    if(File.Exists(xmlPath)) c.IncludeXmlComments(xmlPath);
});

builder.Services.AddHttpClient<IStockClient, StockClient>(client =>
{
    client.BaseAddress = new Uri(builder.Configuration["StockService:BaseUrl"]!);
}).AddPolicyHandler(HttpPolicyExtensions.HandleTransientHttpError()
    .WaitAndRetryAsync(3, retry => TimeSpan.FromMilliseconds(200 * Math.Pow(2, retry)))); ;

builder.Services.AddScoped<IOrderService, OrderService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// TODO: implementation JWT
app.UseAuthorization();
app.UseHttpsRedirection();
app.MapControllers();

app.Run();
