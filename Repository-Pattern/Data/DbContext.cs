using Microsoft.EntityFrameworkCore;
using DotNetCore6.Models;

namespace DotNetCore6.Data
{
    public class ApplicationDbContext : DbContext
    {
        protected readonly IConfiguration Configuration;

        public ApplicationDbContext(DbContextOptions<DbContext> options) : base(options)
        {
        }

        public ApplicationDbContext(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"));
        }

        public DbSet<Computer> Computers { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Lab> Labs { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<DotNetCore6.Models.Computer>()
                .ToTable("computer");
            builder.Entity<DotNetCore6.Models.User>()
                .ToTable("user");
            builder.Entity<DotNetCore6.Models.Lab>()
                .ToTable("lab");
        }
    }
}