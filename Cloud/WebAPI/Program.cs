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
using System;
using Domain.Model;
using Application_.Logic;
using Application_.LogicInterfaces;
using YourApiNamespace.Controllers;

var builder = WebApplication.CreateBuilder(args);


public void ConfigureServices(IServiceCollection services)
{
    services.AddSignalR();
    services.AddScoped<IHumidityLogService, HumidityLogService>();
}

public void Configure(IApplicationBuilder app, IHostingEnvironment env)
{
    app.UseSignalR(routes =>
    {
        routes.MapHub<HumidityHub>("/humidityHub");
    });
}


// Configure logging
builder.Logging.ClearProviders();
builder.Logging.AddConsole();
builder.Logging.AddDebug();
builder.Logging.SetMinimumLevel(LogLevel.Information);
services.AddScoped<IHumidityLogService, HumidityLogService>();


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

builder.Services.AddScoped<IUserLogic, UserLogic>(); // Dependency injection for UserLogic
builder.Services.AddScoped<IPlantLogic, PlantLogic>(); // Dependency injection for PlantLogic
builder.Services.AddScoped<IPotLogic, PotLogic>(); // Dependency injection for PotLogic



var client = new MongoClient(mongoDbSettings["ConnectionString"]);
var database = client.GetDatabase(mongoDbSettings["DatabaseName"]);
var userCollection = database.GetCollection<User>("Users");
builder.Services.AddSingleton(userCollection);
builder.Services.AddScoped<IUserLogic, UserLogic>(); // Dependency injection for UserLogic

// Set up MVC, Swagger and CORS
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCors(options =>
{
    options.AddPolicy("Open", builder => builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());
});


var app = builder.Build();


app.UseDeveloperExceptionPage();
app.UseSwagger();
app.UseSwaggerUI();


app.UseRouting();
app.UseCors("Open");
app.UseAuthorization();
app.MapControllers();


app.Run();