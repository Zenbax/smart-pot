using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace MongoDB;

public static class ServiceCollectionExtensions
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

        return services;
    }
}