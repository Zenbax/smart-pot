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
using YourApiNamespace.Controllers;

var builder = WebApplication.CreateBuilder(args);

// Configure logging
builder.Logging.ClearProviders();
builder.Logging.AddConsole();
builder.Logging.AddDebug();

// Add services to the container.
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
    return database.GetCollection<Pot>("Pots");  // Ensure the collection name matches exactly what's in the database
});

builder.Services.AddSingleton(serviceProvider =>
{
    var client = serviceProvider.GetService<IMongoClient>();
    var database = client.GetDatabase(mongoDbSettings["DatabaseName"]);
    return database.GetCollection<User>("Users");  // Ensure the collection name matches exactly what's in the database
});

builder.Services.AddSingleton(serviceProvider =>
{
    var client = serviceProvider.GetService<IMongoClient>();
    var database = client.GetDatabase(mongoDbSettings["DatabaseName"]);
    return database.GetCollection<Plant>("Plants");  // Ensure the collection name matches exactly what's in the database
});

builder.Services.AddSingleton<IPotLogic, PotLogic>();
builder.Services.AddSingleton<IUserLogic, UserLogic>();


var client = new MongoClient(mongoDbSettings["ConnectionString"]);
var database = client.GetDatabase(mongoDbSettings["DatabaseName"]);
var userCollection = database.GetCollection<User>("Users");  // Ensure the collection name matches exactly what's in the database
builder.Services.AddSingleton(userCollection);

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
