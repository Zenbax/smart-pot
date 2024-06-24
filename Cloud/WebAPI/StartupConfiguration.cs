using System.Text;
using Application_.Logic;
using Application_.LogicInterfaces;
using Cloud.Services;
using Domain.Model;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using MongoDB.Driver;
using MongoDB;

namespace WebAPI
{
    public static class StartupConfiguration
    {
        public static void ConfigureServices(IServiceCollection services, IConfiguration configuration)
        {
            // Configure logging
            services.AddLogging(configure =>
            {
                configure.ClearProviders();
                configure.AddConsole();
                configure.AddDebug();
                configure.SetMinimumLevel(LogLevel.Information);
            });

            // Add MongoDB context
            services.AddMongoDbContext(configuration);

            // Add services to the container
            services.AddScoped<IAuthLogic, AuthLogic>();
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IUserLogic, UserLogic>();
            services.AddScoped<IPotLogic, PotLogic>();
            services.AddScoped<IPlantLogic, PlantLogic>();

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(configuration["Jwt:Secret"])),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });

            // Set up MVC, Swagger and CORS
            services.AddControllers();
            services.AddLogging();
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();
            services.AddCors(options =>
            {
                options.AddDefaultPolicy(builder => builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());
            });
        }

        public static void Configure(WebApplication app)
        {
            if (app.Environment.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseSwagger();
            app.UseSwaggerUI();
            app.UseRouting();
            app.UseCors();
            app.UseAuthentication();
            app.UseAuthorization();
            app.MapControllers();
        }
    }
}
