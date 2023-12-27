using Microsoft.EntityFrameworkCore;

namespace ProfileService.Models
{
    public class AppDbContext : DbContext
    {
        public DbSet<Profile> Profiles { get; set; }
        public DbSet<Trail> Trails { get; set; }
        public DbSet<UserProfileCompletedTrail> UserProfileCompletedTrails { get; set; }
        public DbSet<CompletedTrail> CompletedTrails { get; set; }
        public DbSet<AuditLog> AuditLogs { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=dist-6-505.uopnet.plymouth.ac.uk;Database=COMP2001_MAjayi;User=MAjayi;Password=OnoE922*;TrustServerCertificate=true");
        }
    }
}
