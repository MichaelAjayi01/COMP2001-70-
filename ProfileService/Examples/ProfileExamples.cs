// Assuming the folder structure is like this:
// YourProjectRoot/Examples/CreateProfileExample.cs

using System;
using System.Collections.Generic;
using ProfileService.Models; // Adjust the namespace based on your project structure
using Swashbuckle.AspNetCore.Filters;

namespace ProfileExamples.Examples
{
    public class CreateProfileExample : IExamplesProvider<CreateProfileWrapperDTO>
    {
        public CreateProfileWrapperDTO GetExamples()
        {
            return new CreateProfileWrapperDTO
            {
                profileDTO = new CreateProfileDTO
                {
                    First_Name = string.Empty,
                    Last_Name = string.Empty,
                    Email = string.Empty,
                    About = string.Empty,
                    Location = string.Empty,
                    Units = string.Empty,
                    Calorie_Counter_Info = string.Empty,
                    Height = 0,
                    Weight = 0,
                    Birthday = DateTime.MinValue,
                    Set_Password = string.Empty,
                    Profile_Picture = Array.Empty<byte>(), // Change the type back to byte[]
                    CompletedTrails = new List<CreateCompletedTrailDTO>
                    {
                        new CreateCompletedTrailDTO
                        {
                            Trail_Name = string.Empty
                            // ... other properties of CreateCompletedTrailDTO
                        },
                        new CreateCompletedTrailDTO
                        {
                            Trail_Name = string.Empty
                            // ... other properties of CreateCompletedTrailDTO
                        }
                    }
                }
            };
        }
    }
}
