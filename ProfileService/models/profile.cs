using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProfileService.Models
{
[Table("CW2_USER_PROFILE")]
public class Profile
{
    // Properties
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int User_ID { get; set; }

    [Required]
    [MaxLength(50)]
    public string First_Name { get; set; }

    [Required]
    [MaxLength(50)]
    public string Last_Name { get; set; }

    [MaxLength(100)] // Adjust the length as needed
    public string Email { get; set; } // New property for email

    public string About { get; set; }
    public string Location { get; set; }
    public string Units { get; set; }
    public string Calorie_Counter_Info { get; set; }
    public float Height { get; set; }
    public float Weight { get; set; }
    public DateTime Birthday { get; set; }

    [Required]
    [MaxLength(50)]
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
        Email = ""; // Add this line for the email property
        About = "";
        Location = "";
        Units = "";
        Calorie_Counter_Info = "";
        Set_Password = "";
        Profile_Picture = Array.Empty<byte>(); // You might want to provide a default value or handle it differently
        Birthday = DateTime.MinValue; // Provide a default value for DateTime

        // Initialize navigation properties
        CompletedTrails = new List<UserProfileCompletedTrail>();
        AuditLogs = new List<AuditLog>();
    }
}


    [Table("CW2_Trails")]
public class Trail
{
    // Properties
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Trail_ID { get; set; }

    [Required]
    [MaxLength(100)]
    public string Trail_Name { get; set; }

    [Required]
    [MaxLength(10000)] // Replace MAX_LENGTH with the appropriate max length for List_of_Trails
    public string List_of_Trails { get; set; } // Add this property

    // Navigation properties
    public List<UserProfileCompletedTrail> CompletedTrails { get; set; }

    // Constructor
    public Trail()
    {
        // Initialize properties
        Trail_ID = 0;
        Trail_Name = "";
        List_of_Trails = ""; // Provide a default value or handle it differently

        // Initialize navigation properties
        CompletedTrails = new List<UserProfileCompletedTrail>();
    }
}


[Table("CW2_UserProfile_CompletedTrails_JT")]
public class UserProfileCompletedTrail
{
    // Properties
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int User_Trail_ID { get; set; }

    // Foreign keys
    public int User_ID { get; set; }
    public int Trail_ID { get; set; }
    public int CompletedTrail_ID { get; set; }

    // Navigation properties
    public Profile? User { get; set; } // Nullable
    public Trail? Trail { get; set; } // Nullable
    public CompletedTrail? CompletedTrail { get; set; } // Nullable

    // Constructor
    public UserProfileCompletedTrail()
    {
        // Initialize properties
        User_Trail_ID = 0;
        User_ID = 0;
        Trail_ID = 0;
        CompletedTrail_ID = 0;

        // Initialize navigation properties as needed
        User = null;
        Trail = null;
        CompletedTrail = null;
    }
}



[Table("CW2_COMPLETED_TRAILS")]
public class CompletedTrail
{
    // Properties
    [Key]
    public int Completed_Trail_ID { get; set; }

    public int Completed_Trail_Count { get; set; }

    // Navigation properties
    public List<UserProfileCompletedTrail> UserProfileCompletedTrails { get; set; }

    // Constructor
    public CompletedTrail()
    {
        // Initialize properties
        Completed_Trail_ID = 0;
        Completed_Trail_Count = 0;

        // Initialize navigation properties
        UserProfileCompletedTrails = new List<UserProfileCompletedTrail>();
    }
}

[Table("CW2_Audit_Log")]
public class AuditLog
{
    // Properties
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Audit_ID { get; set; }

    public string Operation_Type { get; set; }
    public DateTime Operation_DateTime { get; set; }
    public string Operation_Details { get; set; }

    public AuditLog()
    {
        // Initialize properties
        Audit_ID = 0;
        Operation_Type = "";
        Operation_DateTime = DateTime.MinValue;
        Operation_Details = "";
    }
}
}

//dotnet ef migrations add InitialCreate
//dotnet ef database update
