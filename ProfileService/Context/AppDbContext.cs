using Microsoft.EntityFrameworkCore;

namespace ProfileService.Models
{
    public class AppDbContext : DbContext
    {
        public DbSet<Profile> Profiles { get; set; }
        public DbSet<Trail> Trails { get; set; }
        public DbSet<UserProfileCompletedTrail> UserProfileCompletedTrails { get; set; }
        public DbSet<CompletedTrail> CompletedTrails { get; set; }
        public DbSet<Stats> Stats { get; set; }
        public DbSet<TrailReviewJunction> TrailReviewJunctions { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<CustomRoute> CustomRoutes { get; set; }
        public DbSet<AuditLog> AuditLogs { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("dist-6-505.uopnet.plymouth.ac.uk");
        }
    }
}
