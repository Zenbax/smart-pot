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

// Configure logging
builder.Logging.ClearProviders();
builder.Logging.AddConsole();
builder.Logging.AddDebug();
builder.Logging.SetMinimumLevel(LogLevel.Information);


// Configure MongoDB
var mongoDbSettings = builder.Configuration.GetSection("MongoDB");
builder.Services.AddSingleton<IMongoClient>(serviceProvider =>
{
    var logger = serviceProvider.GetService<ILogger<Program>>();
    logger.LogInformation("Creating MongoDB client with connection string: {ConnectionString}", mongoDbSettings["ConnectionString"]);
    return new MongoClient(mongoDbSettings["ConnectionString"]);
});

builder.Services.AddSingleton(serviceProvider =>
{
    var client = serviceProvider.GetService<IMongoClient>();
    var database = client.GetDatabase(mongoDbSettings["DatabaseName"]);
    return database.GetCollection<User>("Users");
});

builder.Services.AddSingleton(serviceProvider =>
{
    var client = serviceProvider.GetService<IMongoClient>();
    var database = client.GetDatabase(mongoDbSettings["DatabaseName"]);
    return database.GetCollection<Plant>("Plants");
});

builder.Services.AddSingleton(serviceProvider =>
{
    var client = serviceProvider.GetService<IMongoClient>();
    var database = client.GetDatabase(mongoDbSettings["DatabaseName"]);
    return database.GetCollection<Pot>("Pots");
});

// Add services to the container.
builder.Services.AddScoped<IAuthLogic, AuthLogic>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IUserLogic, UserLogic>();
builder.Services.AddScoped<IPotLogic, PotLogic>();
builder.Services.AddScoped<IPlantLogic, PlantLogic>();

builder.Services.AddAuthentication(options =>
    {
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    })
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(builder.Configuration["Jwt:Secret"])),
            ValidateIssuer = false,
            ValidateAudience = false
        };
    });

var client = new MongoClient(mongoDbSettings["ConnectionString"]);
var database = client.GetDatabase(mongoDbSettings["DatabaseName"]);
var userCollection = database.GetCollection<User>("Users");
builder.Services.AddSingleton(userCollection);
builder.Services.AddScoped<IUserLogic, UserLogic>(); // Dependency injection for UserLogic

// Set up MVC, Swagger and CORS
builder.Services.AddControllers();
builder.Services.AddLogging();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(builder => builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());
});


var app = builder.Build();


app.UseDeveloperExceptionPage();
app.UseSwagger();
app.UseSwaggerUI();
app.UseMiddleware<CustomAuthenticationMiddleware>();


app.UseRouting();
app.UseCors();
app.UseAuthorization();
app.MapControllers();


app.Run();