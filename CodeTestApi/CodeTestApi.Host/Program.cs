using CodeTestApi.Application.Queries.Vehicles;
using CodeTestApi.Domain.Interfaces;
using CodeTestApi.Host;
using CodeTestApi.Infrastructure.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using MongoDB.Driver;
using System.Text;

public class Program
{
    public static async Task Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        var key = Encoding.UTF8.GetBytes("#MySuperSecretKeyThatNoOneWillGuessAndSuperLongEnough123456789!$");

        builder.Services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        })
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

        builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(GetAvailableVehiclesQueryHandler).Assembly));

        builder.Services.Configure<MongoDbSettings>(
            builder.Configuration.GetSection("MongoDbSettings"));

        builder.Services.AddSingleton<IMongoClient, MongoClient>(sp =>
        {
            var settings = sp.GetRequiredService<IOptions<MongoDbSettings>>().Value;
            return new MongoClient(settings.ConnectionString);
        });

        builder.Services.AddScoped<IMongoDatabase>(sp =>
        {
            var settings = sp.GetRequiredService<IOptions<MongoDbSettings>>().Value;
            var client = sp.GetRequiredService<IMongoClient>();
            return client.GetDatabase(settings.DatabaseName);
        });

        builder.Services.AddScoped<IVehicleRepository, VehicleRepository>();
        builder.Services.AddScoped<IUserRepository, UserRepository>();

        // Add other services
        builder.Services.AddControllers();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen(options =>
        {
            options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                Name = "Authorization",
                Type = SecuritySchemeType.Http,
                Scheme = "Bearer",
                BearerFormat = "JWT",
                In = ParameterLocation.Header,
                Description = "Type 'Bearer' [space] and your token JWT in this field."
            });

            options.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                           Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        }
                    },
                    new string[] {}
                }
            });
        });

        var app = builder.Build();

        using (var scope = app.Services.CreateScope())
        {
            var userRepository = scope.ServiceProvider.GetRequiredService<IUserRepository>();
            await DatabaseSeeder.SeedUsersAsync(userRepository);

            var vehicleRepository = scope.ServiceProvider.GetRequiredService<IVehicleRepository>();
            await DatabaseSeeder.SeedVehiclesAsync(vehicleRepository);

            Console.WriteLine("Data Seeding Succesful");
        }

        app.UseSwagger();
        app.UseSwaggerUI(c =>
        {
            c.SwaggerEndpoint("http://localhost:8080/swagger/v1/swagger.json", "My API V1");
            c.RoutePrefix = string.Empty;  // Hace que Swagger sea la página de inicio
        });


        app.UseHttpsRedirection();

        app.UseAuthentication();
        app.UseAuthorization();

        app.MapControllers();

        await app.RunAsync();
    }
}
