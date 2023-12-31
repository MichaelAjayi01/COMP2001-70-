// ProfileController.cs
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProfileService.Models;
using Microsoft.Data.SqlClient;
using Swashbuckle.AspNetCore.Annotations;
using Swashbuckle.AspNetCore.Filters;
using ProfileExamples.Examples;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Authentication.JwtBearer;



[Route("api/[controller]")]
[ApiController]
public class ProfileController : ControllerBase
{
    private int adminId = 12;
    private readonly AppDbContext _dbContext;

    private readonly IConfiguration _configuration;

public ProfileController(AppDbContext dbContext, IConfiguration configuration)
{
    _dbContext = dbContext;
    _configuration = configuration; // Corrected from 'configuration' to '_configuration'
}


    [HttpGet]
    public async Task<ActionResult<IEnumerable<Profile>>> GetProfiles()
    {
        return await _dbContext.Profiles.ToListAsync();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Profile>> GetProfile(int id)
    {
        var profile = await _dbContext.Profiles.FindAsync(id);

        if (profile == null)
        {
            return NotFound();
        }

        return profile;
    }


// Add a new route for authentication
[HttpPost("auth/api/users")]
[SwaggerOperation("Authenticate a user")]
[ProducesResponseType(typeof(string), 200)] // Assuming you return a JWT token
[ProducesResponseType(typeof(IEnumerable<string>), 200)]
[ProducesResponseType(500)]
[SwaggerRequestExample(typeof(AuthenticateUserDTO), typeof(AuthenticateUserExample))]
public async Task<ActionResult> AuthenticateUser([FromBody] AuthenticateUserDTO authenticateUserDTO)
{
    try
    {
        var user = await _dbContext.Profiles.FirstOrDefaultAsync(u =>
            u.Email == authenticateUserDTO.Email && u.Set_Password == authenticateUserDTO.Set_Password);

        if (user != null)
        {
            // Authentication successful
            var token = GenerateJwtToken(user.User_ID);
            string revealToken = token;
            JwtUtils.PrintTokenClaims(revealToken);
            // Include user information in the response
            return Ok(new
            {
                token,
                user.Set_Password,
            });
        }
        else
        {
            // Authentication failed
            return Ok(new[] { "Verified", "False" });
        }
    }
    catch (Exception ex)
    {
        // Log the error or take appropriate action
        // Return a meaningful error response to the client
        return StatusCode(500, $"Internal Server Error: {ex.Message}");
    }
}



private string GenerateJwtToken(int userId)
{
    var secretKey = _configuration["JwtSettings:SecretKey"];

    if (secretKey != null)
    {
        var key = Encoding.ASCII.GetBytes(secretKey);
        var tokenHandler = new JwtSecurityTokenHandler();
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new[] { new Claim("unique_name", userId.ToString()) }),
            Expires = DateTime.UtcNow.AddHours(1),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };
        
        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }
    else
    {
        // Handle the case where the secret key is null (throw an exception, provide a default, etc.)
        throw new InvalidOperationException("JWT secret key is null.");
    }
}





[HttpPost]
[SwaggerOperation("Create a new profile")]
[SwaggerRequestExample(typeof(CreateProfileWrapperDTO), typeof(CreateProfileExample))]
[ProducesResponseType(typeof(Profile), 201)]
[ProducesResponseType(typeof(IDictionary<string, string[]>), 400)]
[ProducesResponseType(500)]
[SwaggerRequestExample(typeof(CreateProfileWrapperDTO), typeof(CreateProfileExample))]
public async Task<ActionResult<Profile>> CreateProfile(
    [FromBody] CreateProfileWrapperDTO wrapperDTO)
{
    // Validate the profile model as needed
    if (!ModelState.IsValid)
    {
        return BadRequest(ModelState);
    }

    // Access the profileDTO from the wrapper
    CreateProfileDTO profileDTO = wrapperDTO.profileDTO;

    // Map the DTO to the actual profile entity
#pragma warning disable CS8601 // Possible null reference assignment.
    var profile = new Profile
    {
        First_Name = profileDTO.First_Name,
        Last_Name = profileDTO.Last_Name,
        Email = profileDTO.Email,
        About = profileDTO.About,
        Location = profileDTO.Location,
        Units = profileDTO.Units,
        Calorie_Counter_Info = profileDTO.Calorie_Counter_Info,
        Height = profileDTO.Height,
        Weight = profileDTO.Weight,
        Birthday = profileDTO.Birthday,
        Set_Password = profileDTO.Set_Password,
        Profile_Picture = profileDTO.Profile_Picture,
        CompletedTrails = profileDTO.CompletedTrails?.Select(ct => new UserProfileCompletedTrail
        {
            Trail = new Trail
            {
                Trail_Name = ct.Trail_Name,
                // Add other necessary properties for trail
            }
            // Add other necessary properties for completed trail
        }).ToList()
    };
#pragma warning restore CS8601 // Possible null reference assignment.

    // Call the stored procedure to insert the user profile
    await Task.Run(() => // Use Task.Run to simulate an asynchronous operation
    {
        // Ensure that null or empty lists are handled properly
        var allTrailNames = string.Join(", ", profile.CompletedTrails?.Select(ct => ct.Trail?.Trail_Name ?? "") ?? Enumerable.Empty<string>());
        var allTrailDetails = string.Join(", ", profile.CompletedTrails?.Select(ct => ct.Trail?.List_of_Trails ?? "") ?? Enumerable.Empty<string>());

#pragma warning disable CS8604 // Possible null reference argument.
        _dbContext.InsertUserProfile(
            profile.First_Name,
            profile.Last_Name,
            profile.Email,
            profile.About,
            profile.Location,
            profile.Units,
            profile.Calorie_Counter_Info,
            profile.Height,
            profile.Weight,
            profile.Birthday,
            profile.Set_Password,
            profile.Profile_Picture,
            allTrailNames,
            allTrailDetails);
#pragma warning restore CS8604 // Possible null reference argument.
    });

    var createdProfile = await _dbContext.Profiles
        .FirstOrDefaultAsync(p => p.Email == profileDTO.Email && p.Set_Password == profileDTO.Set_Password);

    if (createdProfile != null)
    {
        JwtUtils.user_id_value = Convert.ToString(createdProfile.User_ID);
        var newToken = GenerateJwtToken(createdProfile.User_ID);
        string revealToken = newToken;
        JwtUtils.PrintTokenClaims(revealToken);

        Console.WriteLine(JwtUtils.user_id_value);

        // Return the created profile with a CreatedAtAction result
        return CreatedAtAction(nameof(GetProfile), new { id = createdProfile.User_ID }, createdProfile);
    }
    return StatusCode(500, "Internal Server Error: Unable to retrieve the created profile");
}



[HttpPut("{id}")]
public async Task<IActionResult> UpdateProfile(int id, [FromBody] UpdateProfileDTO updateDTO)
{
    if (id != updateDTO.User_ID)
    {
        return BadRequest();
    }

    try
    {
            // Access the profileDTO from the wrapper

            CreateProfileDTO? profileDTO = updateDTO.ProfileDTO;


            // Map the DTO to the actual profile entity
#pragma warning disable CS8602 // Dereference of a possibly null reference.
#pragma warning disable CS8601 // Possible null reference assignment.
#pragma warning disable CS8619 // Nullability of reference types in value doesn't match target type.
            var profile = new Profile
        {
            User_ID = updateDTO.User_ID,
            First_Name = profileDTO.First_Name,
            Last_Name = profileDTO.Last_Name,
            Email = profileDTO.Email,
            About = profileDTO.About,
            Location = profileDTO.Location,
            Units = profileDTO.Units,
            Calorie_Counter_Info = profileDTO.Calorie_Counter_Info,
            Height = profileDTO.Height,
            Weight = profileDTO.Weight,
            Birthday = profileDTO.Birthday,
            Set_Password = profileDTO.Set_Password,
            Profile_Picture = profileDTO.Profile_Picture ?? new byte[0], // Handle null case, replace with an appropriate default value
            CompletedTrails = profileDTO.CompletedTrails?.Select(ct => ct != null ? new UserProfileCompletedTrail
            {
                // Map properties accordingly
            } : null).ToList(),

        };
#pragma warning restore CS8619 // Nullability of reference types in value doesn't match target type.
#pragma warning restore CS8601 // Possible null reference assignment.
#pragma warning restore CS8602 // Dereference of a possibly null reference.

            // Call the stored procedure to update the user profile
            await _dbContext.Database.ExecuteSqlRawAsync("EXEC UpdateUserProfile " +
            "@User_ID, @First_Name, @Last_Name, @Email, @About, @Location, @Units, " +
            "@Calorie_Counter_Info, @Height, @Weight, @Birthday, @Set_Password, @Profile_Picture",
            new SqlParameter("@User_ID", profile.User_ID),
            new SqlParameter("@First_Name", profile.First_Name),
            new SqlParameter("@Last_Name", profile.Last_Name),
            new SqlParameter("@Email", profile.Email),
            new SqlParameter("@About", profile.About),
            new SqlParameter("@Location", profile.Location),
            new SqlParameter("@Units", profile.Units),
            new SqlParameter("@Calorie_Counter_Info", profile.Calorie_Counter_Info),
            new SqlParameter("@Height", profile.Height),
            new SqlParameter("@Weight", profile.Weight),
            new SqlParameter("@Birthday", profile.Birthday),
            new SqlParameter("@Set_Password", profile.Set_Password),
            new SqlParameter("@Profile_Picture", profile.Profile_Picture));

        return NoContent();
    }
    catch (DbUpdateConcurrencyException)
    {
        if (!ProfileExists(id))
        {
            return NotFound();
        }
        else
        {
            throw;
        }
    }
    catch (Exception ex)
    {
        // Log the error or take appropriate action
        // Return a meaningful error response to the client
        return StatusCode(500, $"Internal Server Error: {ex.Message}");
    }
}


[HttpDelete("{id}")]
public async Task<IActionResult> DeleteProfile(int id)
{
    try
    {
        // unable to get userid naturally for some reason so will work around it.
        var userIdClaim = JwtUtils.user_id_value;

        if (userIdClaim != null && int.TryParse(userIdClaim, out var currentUserId))
        {
            // Check the token's validity based on nbf and exp
            var now = DateTimeOffset.UtcNow.ToUnixTimeSeconds();

            var nbfClaim = User.FindFirst("nbf");
            var expClaim = User.FindFirst("exp");

            if (nbfClaim != null && expClaim != null)
            {
                var nbf = long.Parse(nbfClaim.Value);
                var exp = long.Parse(expClaim.Value);

                if (now < nbf || now >= exp)
                {
                    // Token is not yet valid or has expired
                    Console.WriteLine("Token is not yet valid or has expired");
                    return Unauthorized(); // 401 Unauthorized
                }
            }

            if (currentUserId == adminId)//edit this to account for a user deleting their own profile
            {
                // Call the stored procedure to delete the user profile
                var rowsAffected = await _dbContext.Database.ExecuteSqlRawAsync("EXEC DeleteUserProfile @User_ID", new SqlParameter("@User_ID", id));

                // Check if any rows were affected (profile deleted)
                if (rowsAffected > 0)
                {
                    return NoContent();
                }
                else
                {
                    // No rows affected means the profile was not found
                    return NotFound();
                }
            }
            else
            {
                // Unauthorized access
                return Unauthorized();
            }
        }
        else
        {
            // User ID claim not found or Invalid
            return Unauthorized(); // 401 Unauthorized
        }
    }
    catch (Exception ex)
    {
        // Log the error or take appropriate action
        // Return a meaningful error response to the client
        return StatusCode(500, $"Internal Server Error: {ex.Message}");
    }
}



private bool ProfileExists(int id)
{
    // Check if the profile exists in the local DbSet (no database query)
    return _dbContext.Profiles.Local.Any(e => e.User_ID == id);
}
}
