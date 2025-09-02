using Educational_Victoria.Models;
using Microsoft.EntityFrameworkCore;

namespace Educational_Victoria.Data
{
    public class EducationalDbContext : DbContext
    {
        public EducationalDbContext(DbContextOptions<EducationalDbContext> options)
            : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Subject> Subjects { get; set; }

        public DbSet<UserSubjectAccess> UserSubjectsAccesses { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            // Configuração da chave composta
            modelBuilder.Entity<UserSubjectAccess>()
                .HasKey(ua => new { ua.UserId, ua.SubjectId });

            // Relacionamentos
            modelBuilder.Entity<UserSubjectAccess>()
                .HasOne(ua => ua.User)
                .WithMany(u => u.UserAccesses)
                .HasForeignKey(ua => ua.UserId);

            modelBuilder.Entity<UserSubjectAccess>()
                .HasOne(ua => ua.Subject)
                .WithMany(s => s.UserAccesses)
                .HasForeignKey(ua => ua.SubjectId);

            // Configuração roles
            modelBuilder.Entity<User>()
                .Property(u => u.Role)
                .HasConversion<int>();
        }
    }
}
