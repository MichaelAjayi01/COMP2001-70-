// DTOs.cs
using System.ComponentModel.DataAnnotations;


public class CreateProfileDTO
{
    public CreateProfileDTO()
    {
        First_Name = string.Empty;
        Last_Name = string.Empty;
        Email = string.Empty;
        About = string.Empty;
        Location = string.Empty;
        Units = string.Empty;
        Calorie_Counter_Info = string.Empty;
        Set_Password = string.Empty;
        CompletedTrails = new List<CreateCompletedTrailDTO>();
        
    }

    public string First_Name { get; set; }
    public string Last_Name { get; set; }
    public string Email { get; set; }
    public string About { get; set; }
    public string Location { get; set; }
    public string Units { get; set; }
    public string Calorie_Counter_Info { get; set; }
    public float Height { get; set; }
    public float Weight { get; set; }
    public DateTime Birthday { get; set; }
    public string Set_Password { get; set; }
    public byte[]? Profile_Picture { get; set; }
    public List<CreateCompletedTrailDTO> CompletedTrails { get; set; }
}

public class CreateCompletedTrailDTO
{
    public CreateCompletedTrailDTO()
    {
        Trail_Name = string.Empty;
        // Initialize other properties
    }

    public string? Trail_Name { get; set; }
    // Add other necessary properties for completed trail
}

public class CreateProfileWrapperDTO
{
    public CreateProfileDTO profileDTO { get; set; }

    public CreateProfileWrapperDTO()
    {
        profileDTO = new CreateProfileDTO();
    }
}

public class UpdateProfileDTO
{
    public int User_ID { get; set; }
    public CreateProfileDTO? ProfileDTO { get; set; }
}

public class AuthenticateUserDTO
{
    [Required(ErrorMessage = "Email is required")]
    [EmailAddress(ErrorMessage = "Invalid email address")]
    public string Email { get; set; } = string.Empty;

    [Required(ErrorMessage = "Password is required")]
    public string Set_Password { get; set; } = string.Empty;
}
