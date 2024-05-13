using Application_.Logic;
using Application_.LogicInterfaces;
using Domain.Model;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using MongoDB.Driver;
using System.Text;
using Cloud.Services;
using Microsoft.IdentityModel.Tokens;
using YourApiNamespace.Controllers;
using Microsoft.Extensions.Configuration;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(
        builder =>
        {
            builder.AllowAnyOrigin()
                .AllowAnyHeader()
                .AllowAnyMethod();
        });
});

// Configure logging
builder.Logging.ClearProviders();
builder.Logging.AddConsole();
builder.Logging.AddDebug();
builder.Logging.SetMinimumLevel(LogLevel.Information);

// Add services to the container.
builder.Services.AddScoped<IAuthLogic, AuthLogic>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IUserLogic, UserLogic>();
builder.Services.AddScoped<IPotLogic, PotLogic>();
builder.Services.AddScoped<IPlantLogic, PlantLogic>();

// Configure MongoDB
var mongoDbSettings = builder.Configuration.GetSection("MongoDB");
builder.Services.AddSingleton<IMongoClient>(serviceProvider =>
{
    var logger = serviceProvider.GetService<ILogger<Program>>();
    logger.LogInformation("Creating MongoDB client with connection string: {ConnectionString}", mongoDbSettings["ConnectionString"]);
    return new MongoClient(mongoDbSettings["ConnectionString"]);
});

// Set up MVC, Swagger
builder.Services.AddControllers();
builder.Services.AddLogging();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


var app = builder.Build();

app.UseRouting();
app.UseCors(); // Place UseCors after UseRouting and before UseAuthorization

// Configure the HTTP request pipeline.
app.UseDeveloperExceptionPage();
app.UseSwagger();
app.UseSwaggerUI();
app.UseMiddleware<CustomAuthenticationMiddleware>();

app.UseAuthentication(); // UseAuthentication must be called before UseAuthorization
app.UseAuthorization();
app.MapControllers();

app.Run();