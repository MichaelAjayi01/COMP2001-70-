// ProfileController.cs
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProfileService.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Data.SqlClient;

[Route("api/[controller]")]
[ApiController]
public class ProfileController : ControllerBase
{
    private readonly AppDbContext _dbContext;

    public ProfileController(AppDbContext dbContext)
    {
        _dbContext = dbContext;
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

    
[HttpPost]
public async Task<ActionResult<Profile>> CreateProfile(Profile profile)
{
    // Validate the profile model as needed

    // Check if profile.CompletedTrails is not null
    if (profile.CompletedTrails != null)
    {
        // Call the stored procedure to insert the user profile
        var allTrailNames = string.Join(", ", profile.CompletedTrails.Select(ct => ct.Trail?.Trail_Name));
        var allTrailDetails = string.Join(", ", profile.CompletedTrails.Select(ct => ct.Trail?.List_of_Trails));

        await Task.Run(() => // Use Task.Run to simulate an asynchronous operation
        {
            _dbContext.InsertUserProfile(
                profile.First_Name,
                profile.Last_Name,
                profile.Email, // Add the email property
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
        });
    }

    // The profile has been added through the stored procedure, so no need to use _dbContext.Profiles.Add(profile);
    // If you need to fetch the newly created profile from the database, you can do so here.

    return CreatedAtAction(nameof(GetProfile), new { id = profile.User_ID }, profile);
}

[HttpPut("{id}")]
public async Task<IActionResult> UpdateProfile(int id, Profile profile)
{
    if (id != profile.User_ID)
    {
        return BadRequest();
    }

    try
    {
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

private bool ProfileExists(int id)
{
    // Check if the profile exists in the local DbSet (no database query)
    return _dbContext.Profiles.Local.Any(e => e.User_ID == id);
}
}
