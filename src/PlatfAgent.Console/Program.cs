using Microsoft.Extensions.Configuration;

namespace PlatfAgent.Console;

class Program
{
    static async Task Main(string[] args)
    {
        try
        {
            var config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddEnvironmentVariables()
                .Build();
            
            var agent = CreateAIAgent(config);
            await RunChatAsync(agent);
        }
        catch (Exception ex)
        {
            ConsoleInterface.ShowError($"Fatal error: {ex.Message}");
            if (!System.Console.IsInputRedirected)
            {
                System.Console.WriteLine("Press any key to exit...");
                System.Console.ReadKey();
            }
        }
    }

    private static SimpleAIAgent CreateAIAgent(IConfiguration config)
    {
        var endpoint = config["AzureOpenAI:Endpoint"] 
            ?? throw new InvalidOperationException("Azure OpenAI endpoint not configured.");
        
        var apiKey = config["AzureOpenAI:Key"] 
            ?? throw new InvalidOperationException("Azure OpenAI API key not configured.");
        
        var deploymentName = config["AzureOpenAI:Deployment"] ?? "gpt-4";
        var systemPrompt = config["AzureOpenAI:SystemPrompt"] ?? "You are a helpful AI agent.";

        return new SimpleAIAgent(endpoint, apiKey, deploymentName, systemPrompt);
    }

    private static async Task RunChatAsync(SimpleAIAgent agent)
    {
        ConsoleInterface.DisplayWelcome();

        while (true)
        {
            var userInput = ConsoleInterface.GetUserInput();
            if (string.IsNullOrWhiteSpace(userInput)) continue;

            if (ConsoleInterface.IsCommand(userInput, "exit", "quit", "bye"))
            {
                ConsoleInterface.DisplayGoodbye();
                break;
            }

            if (ConsoleInterface.IsCommand(userInput, "help", "?"))
            {
                ConsoleInterface.DisplayHelp();
                continue;
            }

            if (ConsoleInterface.IsCommand(userInput, "clear", "cls"))
            {
                agent.ClearHistory();
                ConsoleInterface.DisplayWelcome();
                continue;
            }

            try
            {
                ConsoleInterface.ShowThinking();
                var response = await agent.GetResponseAsync(userInput);
                ConsoleInterface.ShowResponse(response);
            }
            catch (Exception ex)
            {
                ConsoleInterface.ShowError(ex.Message);
            }
        }
    }
}