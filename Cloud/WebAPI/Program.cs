using System.Net;
using MongoDB;
using Application_.Logic;
using Application_.LogicInterfaces;
using Cloud.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Domain.Model;
using Microsoft.AspNetCore.Diagnostics;
using MongoDB.Driver;
using Newtonsoft.Json;
using YourApiNamespace.Controllers;
using Microsoft.AspNetCore.HttpOverrides;  // Add this namespace

var builder = WebApplication.CreateBuilder(args);

// Configure logging
builder.Logging.ClearProviders();
builder.Logging.AddConsole();
builder.Logging.AddDebug();
builder.Logging.SetMinimumLevel(LogLevel.Information);

// Add MongoDB context
builder.Services.AddMongoDbContext(builder.Configuration);

// Add services to the container
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
app.UseRouting();
app.UseCors();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.UseExceptionHandler(a => a.Run(async context =>
{
    var exceptionHandlerPathFeature = context.Features.Get<IExceptionHandlerPathFeature>();
    var exception = exceptionHandlerPathFeature.Error;

    // Using Newtonsoft.Json to serialize the error response
    var result = JsonConvert.SerializeObject(new { error = exception.Message });
    context.Response.ContentType = "application/json";
    context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

    await context.Response.WriteAsync(result);
}));

// Add forwarded headers middleware to handle headers from Nginx
app.UseForwardedHeaders(new ForwardedHeadersOptions
{
    ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
});

app.Run();
