using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;


namespace ProfileService.Models
{
    public class Profile
    {
        // Properties
        public int User_ID { get; set; }
        public string First_Name { get; set; }
        public string Last_Name { get; set; }
        public string About { get; set; }
        public string Location { get; set; }
        public string Units { get; set; }
        public string Calorie_Counter_Info { get; set; }
        public float Height { get; set; }
        public float Weight { get; set; }
        public DateTime Birthday { get; set; }
        public string Set_Password { get; set; }
        public byte[] Profile_Picture { get; set; }

        // Navigation properties
        public List<UserProfileCompletedTrail> CompletedTrails { get; set; }
        public List<AuditLog> AuditLogs { get; set; }

        // Constructor
        public Profile()
        {
            // Initialize non-nullable properties here
            First_Name = "";
            Last_Name = "";
            About = "";
            Location = "";
            Units = "";
            Calorie_Counter_Info = "";
            Set_Password = "";
            Profile_Picture = new byte[0]; // You might want to provide a default value or handle it differently
            Birthday = DateTime.MinValue; // Provide a default value for DateTime

            // Initialize navigation properties
            CompletedTrails = new List<UserProfileCompletedTrail>();
            AuditLogs = new List<AuditLog>();
        }
    }

    public class Trail
    {
        // Properties
        public int Trail_ID { get; set; }
        public string Trail_Name { get; set; }

        // Navigation properties
        public List<UserProfileCompletedTrail> CompletedTrails { get; set; }

        // Constructor
        public Trail()
        {
            // Initialize properties
            Trail_ID = 0;
            Trail_Name = "";

            // Initialize navigation properties
            CompletedTrails = new List<UserProfileCompletedTrail>();
        }
    }

    public class UserProfileCompletedTrail
    {
        // Properties
        public int User_ID { get; set; }
        public int Trail_ID { get; set; }

        // Navigation properties
        public Profile User { get; set; }
        public Trail Trail { get; set; }
        public CompletedTrail CompletedTrail { get; set; }

        // Constructor
        public UserProfileCompletedTrail()
        {
            // Initialize properties
            User_ID = 0;
            Trail_ID = 0;

            // Initialize navigation properties
            User = new Profile();
            Trail = new Trail();
            CompletedTrail = new CompletedTrail();
        }
    }

    public class CompletedTrail
    {
        // Properties
        [ForeignKey("UserProfileCompletedTrail")]
        public int User_ID { get; set; }

        [ForeignKey("UserProfileCompletedTrail")]
        public int Trail_ID { get; set; }

        public int Completed_Trail_Count { get; set; }

        // Navigation properties
        [ForeignKey("User_ID, Trail_ID")] // Define the composite foreign key
        public UserProfileCompletedTrail UserProfileCompletedTrail { get; set; }

        // Constructor
        public CompletedTrail()
        {
            // Initialize properties
            User_ID = 0;
            Trail_ID = 0;
            Completed_Trail_Count = 0;

            // Initialize navigation properties
            UserProfileCompletedTrail = new UserProfileCompletedTrail();
        }
    }

    public class AuditLog
    {
        // Properties
        public int Audit_ID { get; set; }
        public int User_ID { get; set; }
        public string Operation_Type { get; set; }
        public DateTime Operation_DateTime { get; set; }
        public string Operation_Details { get; set; }

        // Navigation property
        public Profile User { get; set; }

        // Constructor
        public AuditLog()
        {
            // Initialize properties
            Audit_ID = 0;
            User_ID = 0;
            Operation_Type = "";
            Operation_DateTime = DateTime.MinValue;
            Operation_Details = "";

            // Initialize navigation properties
            User = new Profile();
        }
    }
}
