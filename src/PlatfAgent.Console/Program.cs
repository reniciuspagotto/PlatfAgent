using Azure.AI.OpenAI;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using PlatfAgent.Console.Configuration;
using PlatfAgent.Console.Services;
using Azure;

namespace PlatfAgent.Console;

class Program
{
    static async Task Main(string[] args)
    {
        // Build configuration
        var configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .AddEnvironmentVariables()
            .Build();

        // Create host builder
        var host = Host.CreateDefaultBuilder(args)
            .ConfigureServices((context, services) =>
            {
                // Configure options
                services.Configure<AzureOpenAIConfiguration>(configuration.GetSection("AzureOpenAI"));
                services.Configure<AgentConfiguration>(configuration.GetSection("Agent"));

                // Register Azure OpenAI client
                var azureOpenAIConfig = configuration.GetSection("AzureOpenAI").Get<AzureOpenAIConfiguration>();
                if (azureOpenAIConfig != null)
                {
                    services.AddSingleton(provider =>
                    {
                        return new OpenAIClient(new Uri(azureOpenAIConfig.Endpoint), new AzureKeyCredential(azureOpenAIConfig.ApiKey));
                    });
                }

                // Register services
                services.AddScoped<IAIAgentService, AIAgentService>();
                services.AddScoped<ConsoleApp>();
            })
            .ConfigureLogging(logging =>
            {
                logging.ClearProviders();
                logging.AddConsole();
            })
            .Build();

        // Run the console application
        try
        {
            var app = host.Services.GetRequiredService<ConsoleApp>();
            await app.RunAsync();
        }
        catch (Exception ex)
        {
            var logger = host.Services.GetRequiredService<ILogger<Program>>();
            logger.LogError(ex, "Application terminated unexpectedly");
        }
    }
}