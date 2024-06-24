using WebAPI;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
StartupConfiguration.ConfigureServices(builder.Services, builder.Configuration);

var app = builder.Build();

// Configure the HTTP request pipeline.
StartupConfiguration.Configure(app);

app.Run();