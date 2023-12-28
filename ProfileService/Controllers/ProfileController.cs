// ProfileController.cs
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProfileService.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

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
    [HttpPost]
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

        _dbContext.Entry(profile).State = EntityState.Modified;

        try
        {
            await _dbContext.SaveChangesAsync();
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

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteProfile(int id)
    {
        var profile = await _dbContext.Profiles.FindAsync(id);
        if (profile == null)
        {
            return NotFound();
        }

        _dbContext.Profiles.Remove(profile);
        await _dbContext.SaveChangesAsync();

        return NoContent();
    }

    private bool ProfileExists(int id)
    {
        return _dbContext.Profiles.Any(e => e.User_ID == id);
    }
}
