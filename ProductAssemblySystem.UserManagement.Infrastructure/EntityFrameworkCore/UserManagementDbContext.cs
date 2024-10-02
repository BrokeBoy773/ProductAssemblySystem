using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using ProductAssemblySystem.UserManagement.Domain.Entities;
using ProductAssemblySystem.UserManagement.Infrastructure.Authorization;
using ProductAssemblySystem.UserManagement.Infrastructure.EntityFrameworkCore.Configurations;
using Serilog;

namespace ProductAssemblySystem.UserManagement.Infrastructure.EntityFrameworkCore
{
    public class UserManagementDbContext(
        IConfiguration configuration,
        IOptions<AuthorizationOptions> authOptions)
        : DbContext
    {
        private readonly IConfiguration _configuration = configuration;
        private readonly IOptions<AuthorizationOptions> _authOptions = authOptions;

        public required DbSet<User> Users { get; set; }
        public required DbSet<Role> Roles { get; set; }
        public required DbSet<Permission> Permissions { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder
                .UseSqlServer(_configuration.GetConnectionString("UserManagementDbContext"))
                .UseLoggerFactory(LoggerFactory.Create(loggingBuilder => loggingBuilder.AddSerilog()))
                .EnableSensitiveDataLogging();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(UserManagementDbContext).Assembly);

            modelBuilder.ApplyConfiguration(new RolePermissionConfiguration(_authOptions.Value));
        }
    }
}
