using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace MongoDB;

public static class ServiceCollectionExtensions //klasse for at tilføje MongoDB til dependency injection
{
    public static IServiceCollection AddMongoDbContext(this IServiceCollection services, IConfiguration configuration)
    {
        var mongoDbSettings = configuration.GetSection("MongoDB").Get<MongoDbSettings>();
        services.AddSingleton(mongoDbSettings);

        services.AddSingleton(serviceProvider =>
        {
            var logger = serviceProvider.GetService<ILogger<MongoDbContext>>();
            return new MongoDbContext(mongoDbSettings, logger);
        });

        services.AddSingleton(serviceProvider =>
        {
            var context = serviceProvider.GetService<MongoDbContext>();
            return context.Users;
        });

        services.AddSingleton(serviceProvider =>
        {
            var context = serviceProvider.GetService<MongoDbContext>();
            return context.Pots;
        });

        services.AddSingleton(serviceProvider =>
        {
            var context = serviceProvider.GetService<MongoDbContext>();
            return context.Plants;
        });

        services.AddSingleton(serviceProvider =>
        {
            var context = serviceProvider.GetService<MongoDbContext>();
            return context.SensorData;
        });

        return services;
    }
}