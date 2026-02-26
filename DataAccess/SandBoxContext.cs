using DataAccess.Models;
using Infrastructure.Configurations;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace Infrastructure
{
    public class SandBoxContext : DbContext
    {
        public SandBoxContext(DbContextOptions<SandBoxContext> options) : base(options) 
        { 
        }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UserConfiguration());

        }
    }
}
