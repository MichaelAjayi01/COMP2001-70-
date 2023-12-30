using Microsoft.EntityFrameworkCore;
using Microsoft.Data.SqlClient;

namespace ProfileService.Models
{
    public class AppDbContext : DbContext
    {
        public DbSet<Profile> Profiles { get; set; }
        public DbSet<Trail> Trails { get; set; }
        public DbSet<UserProfileCompletedTrail> UserProfileCompletedTrails { get; set; }
        public DbSet<CompletedTrail> CompletedTrails { get; set; }
        public DbSet<AuditLog> AuditLogs { get; set; }

        // Add this constructor
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=dist-6-505.uopnet.plymouth.ac.uk;Database=COMP2001_MAjayi;User=MAjayi;Password=OnoE922*;TrustServerCertificate=true");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserProfileCompletedTrail>()
                .HasKey(uc => new { uc.User_ID, uc.Trail_ID, uc.CompletedTrail_ID });

            modelBuilder.Entity<UserProfileCompletedTrail>()
                .HasOne(uc => uc.User)
                .WithMany(u => u.CompletedTrails)
                .HasForeignKey(uc => uc.User_ID);

            modelBuilder.Entity<UserProfileCompletedTrail>()
                .HasOne(uc => uc.Trail)
                .WithMany(t => t.CompletedTrails)
                .HasForeignKey(uc => uc.Trail_ID);

            modelBuilder.Entity<UserProfileCompletedTrail>()
                .HasOne(uc => uc.CompletedTrail)
                .WithMany(ct => ct.UserProfileCompletedTrails)
                .HasForeignKey(uc => uc.CompletedTrail_ID);
        }

        public void InsertUserProfile(
            string firstName,
            string lastName,
            string email,
            string about,
            string location,
            string units,
            string calorieCounterInfo,
            float height,
            float weight,
            DateTime birthday,
            string setPassword,
            byte[] profilePicture,
            string trailName,
            string listOfTrails)
        {
            // Provide a default value if profilePicture is null
    byte[] actualProfilePicture = profilePicture ?? Array.Empty<byte>();

    Database.ExecuteSqlRaw("EXEC InsertUserProfile " +
        "@First_Name, @Last_Name, @Email, @About, @Location, @Units, @Calorie_Counter_Info, @Height, @Weight, " +
        "@Birthday, @Set_Password, @Profile_Picture, @Trail_Name, @List_of_Trails",
        new SqlParameter("@First_Name", firstName),
        new SqlParameter("@Last_Name", lastName),
        new SqlParameter("@Email", email),
        new SqlParameter("@About", about),
        new SqlParameter("@Location", location),
        new SqlParameter("@Units", units),
        new SqlParameter("@Calorie_Counter_Info", calorieCounterInfo),
        new SqlParameter("@Height", height),
        new SqlParameter("@Weight", weight),
        new SqlParameter("@Birthday", birthday),
        new SqlParameter("@Set_Password", setPassword),
        new SqlParameter("@Profile_Picture", actualProfilePicture),
        new SqlParameter("@Trail_Name", trailName),
        new SqlParameter("@List_of_Trails", listOfTrails));
        }
    }
}
