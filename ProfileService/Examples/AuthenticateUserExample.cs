using Swashbuckle.AspNetCore.Filters;

public class AuthenticateUserExample : IExamplesProvider<AuthenticateUserDTO>
{
    public AuthenticateUserDTO GetExamples()
    {
        return new AuthenticateUserDTO
        {
            Email = "example@plymouth.ac.uk",
            Set_Password = "examplePassword"
        };
    }
}
