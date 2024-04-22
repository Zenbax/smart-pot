using System.Net;
using Application_.Logic;
using Application_.LogicInterfaces;
using Domain;
using Domain.Model;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using MongoDB.Driver;

var builder = WebApplication.CreateBuilder(args);


// Configure logging
builder.Logging.ClearProviders();
builder.Logging.AddConsole();
builder.Logging.AddDebug();
builder.Logging.SetMinimumLevel(LogLevel.Information);

// Add services to the container.
// Configure MongoDB
var mongoDbSettings = builder.Configuration.GetSection("MongoDB");
builder.Services.AddSingleton<IMongoClient>(serviceProvider =>
{
    var logger = serviceProvider.GetService<ILogger<Program>>();
    logger.LogInformation("Creating MongoDB client with connection string: {ConnectionString}", mongoDbSettings["ConnectionString"]);
    return new MongoClient(mongoDbSettings["ConnectionString"]);
});

var client = new MongoClient(mongoDbSettings["ConnectionString"]);
var database = client.GetDatabase(mongoDbSettings["DatabaseName"]);
var userCollection = database.GetCollection<User>("Users");  
builder.Services.AddSingleton(userCollection);
builder.Services.AddScoped<IUserLogic, UserLogic>(); // Dependency injection for UserLogic

// Set up MVC and Swagger
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Configure CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("Open", builder => builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();  // Show detailed exceptions in development mode
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseRouting();
app.UseAuthorization();
app.UseCors("Open");
app.MapControllers();

app.Run();