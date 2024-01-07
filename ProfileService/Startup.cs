using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using ProfileService.Models;
using System.Text;

public class Startup
{
    public IConfiguration Configuration { get; }

    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    public void ConfigureServices(IServiceCollection services)
    {
        services.AddControllers();

        // Configure DbContext with the specified connection string
        services.AddDbContext<AppDbContext>(options =>
            options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

        services.AddEndpointsApiExplorer();
        // Add Swagger
        services.AddSwaggerGen();
        // Add JWT authentication
        var secretKey = Configuration.GetValue<string>("JwtSettings:SecretKey");

        if (secretKey != null)
        {
            var key = Encoding.ASCII.GetBytes(secretKey);
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme) // Specify the default authentication scheme
                .AddJwtBearer(options =>
                {
                    options.RequireHttpsMetadata = false;
                    options.SaveToken = true;
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(key),
                        ValidateIssuer = false,
                        ValidateAudience = false
                    };
                });
        }
        else
        {
            // Handle the case where the secret key is null (throw an exception, provide a default, etc.)
            throw new InvalidOperationException("JWT secret key is null.");
        }

        // Add authorization globally
        services.AddAuthorization();
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        app.UseRouting();

        app.UseSwagger();
        app.UseSwaggerUI(c =>
        {
            c.SwaggerEndpoint("../swagger/v1/swagger.json", "Profile Service");
        });


        app.UseHttpsRedirection();
        app.UseStaticFiles();

        app.UseAuthentication(); // Adds the authentication middleware to the pipeline
        app.UseAuthorization();  // Adds the authorization middleware to the pipeline

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
            // Other endpoint mappings...
        });
    }
}
