using Educational_Victoria.Data;
using Educational_Victoria.Models;
using Microsoft.EntityFrameworkCore;

namespace Educational_Victoria.Repositories.AuthRepository
{
    public class AuthRepository
    {
        private readonly EducationalDbContext _context;
        private readonly IConfiguration _configuration;
        private readonly ILogger<AuthRepository> _logger;

        public AuthRepository(EducationalDbContext context, IConfiguration configuration, ILogger<AuthRepository> logger)
        {
            _context = context;
            _configuration = configuration;
            _logger = logger;
        }

        public async Task<User> LoginAsync(string email, string password)
        {
            return await _context.Users
                .FirstOrDefaultAsync(u => u.Email == email);
            // A validação da senha é feita no AuthService
        }
    }
}
