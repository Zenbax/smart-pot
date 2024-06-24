using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace WebAPI
{
    public class LambdaEntryPoint : Amazon.Lambda.AspNetCoreServer.APIGatewayProxyFunction
    {
        protected override void Init(IWebHostBuilder builder)
        {
            builder
                .UseStartup<Program>(); // Ensure this matches your Program class
        }

        protected override void Init(IHostBuilder builder)
        {
            builder
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Program>(); // Ensure this matches your Program class
                });
        }
    }
}