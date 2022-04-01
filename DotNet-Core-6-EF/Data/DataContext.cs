using Microsoft.EntityFrameworkCore;
using WebApplication8.Models;

namespace WebApplication8.Data
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
            // connect to sql server with connection string from app settings
            options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"));
        }

        public DbSet<Computer> Computers { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Lab> Labs { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<WebApplication8.Models.Computer>()
                .ToTable("computer");
        }
    }
}
