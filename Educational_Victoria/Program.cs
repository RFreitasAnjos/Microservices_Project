using Educational_Victoria.Data;
using Educational_Victoria.Interfaces.Generic;
using Educational_Victoria.Interfaces.IRepositories;
using Educational_Victoria.Interfaces.IServices;
using Educational_Victoria.Repositories;
using Educational_Victoria.Repositories.AuthRepository;
using Educational_Victoria.Repositories.SubjectRepository;
using Educational_Victoria.Repositories.UsersRepository;
using Educational_Victoria.Repositories.UserSubjectAccessRepository;
using Educational_Victoria.Services;
using Educational_Victoria.Services.AuthService;
using Educational_Victoria.Services.JwtService;
using Educational_Victoria.Services.SubjectService;
using Educational_Victoria.Services.UsersService;
using Educational_Victoria.Services.UserSubjectAccessService;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

//DbConnection
builder.Services.AddDbContext<EducationalDbContext>(options => 
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"),
    sqlOptions => sqlOptions.CommandTimeout(60)));

// Repositories
builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
builder.Services.AddScoped<IUserRepository, UsersRepository>();
builder.Services.AddScoped<ISubjectRepository, SubjectRepository>();
builder.Services.AddScoped<IUserSubjectAccessRepository, UserSubjectAccessRepository>();
builder.Services.AddScoped<AuthRepository>();


// Services
builder.Services.AddScoped(typeof(IService<,,>), typeof(Service<,,>));
builder.Services.AddScoped<IUsersService, UserService>();
builder.Services.AddScoped<ISubjectService, SubjectService>();
builder.Services.AddScoped<IUserSubjectAccessService, UserSubjectAccessService>();
builder.Services.AddScoped<IAuthService, AuthService>();

// JWT Service
builder.Services.AddScoped<IJwtService, JwtService>();


// AutoMapper
builder.Services.AddAutoMapper(cfg => cfg.AddMaps(AppDomain.CurrentDomain.GetAssemblies()));

// Jwt
builder.Services.AddAuthentication("Bearer")
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"])
            )
        };
    });

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(c =>
{
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "JWT Authorization header using the Bearer scheme.",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.Http,
        Scheme = "bearer"
    });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string [] {}
        }
    });
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Educational Victoria API",
        Version = "v1",
        Description = "API para gerenciamento de usuários e recursos educacionais",
        Contact = new OpenApiContact
        {
            Name = "Renan Freitas",
            Email = "renanddeveloper@gmail.com"
        }
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
