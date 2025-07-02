using Microsoft.Extensions.Logging;
using PlatfAgent.Console.Services;

namespace PlatfAgent.Console;

public class ConsoleApp
{
    private readonly IAIAgentService _aiAgentService;
    private readonly ILogger<ConsoleApp> _logger;

    public ConsoleApp(IAIAgentService aiAgentService, ILogger<ConsoleApp> logger)
    {
        _aiAgentService = aiAgentService;
        _logger = logger;
    }

    public async Task RunAsync()
    {
        DisplayWelcomeMessage();

        while (true)
        {
            try
            {
                System.Console.Write("\n🤖 You: ");
                var userInput = System.Console.ReadLine();

                if (string.IsNullOrWhiteSpace(userInput))
                {
                    continue;
                }

                // Check for exit commands
                if (IsExitCommand(userInput))
                {
                    DisplayGoodbyeMessage();
                    break;
                }

                // Check for help command
                if (IsHelpCommand(userInput))
                {
                    DisplayHelpMessage();
                    continue;
                }

                // Check for clear command
                if (IsClearCommand(userInput))
                {
                    System.Console.Clear();
                    DisplayWelcomeMessage();
                    continue;
                }

                // Show thinking indicator
                System.Console.Write("🤔 Agent: Thinking...");
                
                // Get response from AI service
                var response = await _aiAgentService.GetResponseAsync(userInput);
                
                // Clear the thinking line and display response
                System.Console.Write("\r" + new string(' ', 25) + "\r");
                System.Console.WriteLine($"🤖 Agent: {response}");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while processing user input");
                System.Console.WriteLine("\n❌ An error occurred while processing your request. Please try again.");
            }
        }
    }

    private static void DisplayWelcomeMessage()
    {
        System.Console.Clear();
        System.Console.WriteLine("╔══════════════════════════════════════════════════════════════╗");
        System.Console.WriteLine("║                        PlatfAgent v1.0                      ║");
        System.Console.WriteLine("║                  AI-Powered Console Agent                    ║");
        System.Console.WriteLine("║                   Powered by Azure OpenAI                   ║");
        System.Console.WriteLine("╚══════════════════════════════════════════════════════════════╝");
        System.Console.WriteLine();
        System.Console.WriteLine("Welcome! I'm your AI assistant. Ask me anything or type 'help' for commands.");
        System.Console.WriteLine("Type 'exit', 'quit', or 'bye' to exit the application.");
        System.Console.WriteLine(new string('─', 64));
    }

    private static void DisplayHelpMessage()
    {
        System.Console.WriteLine();
        System.Console.WriteLine("📚 Available Commands:");
        System.Console.WriteLine("  help          - Show this help message");
        System.Console.WriteLine("  clear         - Clear the screen");
        System.Console.WriteLine("  exit/quit/bye - Exit the application");
        System.Console.WriteLine();
        System.Console.WriteLine("💡 Tips:");
        System.Console.WriteLine("  • Ask questions naturally - I maintain conversation context");
        System.Console.WriteLine("  • Be specific for better responses");
        System.Console.WriteLine("  • I can help with various topics and tasks");
    }

    private static void DisplayGoodbyeMessage()
    {
        System.Console.WriteLine();
        System.Console.WriteLine("👋 Thank you for using PlatfAgent! Goodbye!");
        System.Console.WriteLine();
    }

    private static bool IsExitCommand(string input)
    {
        var exitCommands = new[] { "exit", "quit", "bye", "goodbye" };
        return exitCommands.Contains(input.Trim().ToLowerInvariant());
    }

    private static bool IsHelpCommand(string input)
    {
        var helpCommands = new[] { "help", "?", "/help", "--help" };
        return helpCommands.Contains(input.Trim().ToLowerInvariant());
    }

    private static bool IsClearCommand(string input)
    {
        var clearCommands = new[] { "clear", "cls", "/clear" };
        return clearCommands.Contains(input.Trim().ToLowerInvariant());
    }
}