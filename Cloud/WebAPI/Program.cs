using Domain;
using MongoDB.Driver;

var builder = WebApplication.CreateBuilder(args);




// MongoDB configuration
var mongoDbSettings = builder.Configuration.GetSection("MongoDB");
builder.Services.AddSingleton<IMongoClient>(serviceProvider =>
{
    return new MongoClient(mongoDbSettings["ConnectionString"]);
});

var client = new MongoClient(mongoDbSettings["ConnectionString"]);
var database = client.GetDatabase(mongoDbSettings["DatabaseName"]);
var userCollection = database.GetCollection<User>("users"); // The "Users" is the name of the collection in MongoDB
builder.Services.AddSingleton(userCollection);


// Add services to the container.


builder.UseStartup<Startup>().UseUrls("https://+:443");

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment() || true)
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();