namespace PlatfAgent.Console;

public static class ConsoleInterface
{
    public static void DisplayWelcome()
    {
        System.Console.Clear();
        System.Console.WriteLine("╔══════════════════════════════════════════════════════════════╗");
        System.Console.WriteLine("║                        PlatfAgent v2.1                      ║");
        System.Console.WriteLine("║                  AI-Powered Console Agent                    ║");
        System.Console.WriteLine("║                   Powered by Azure OpenAI                   ║");
        System.Console.WriteLine("╚══════════════════════════════════════════════════════════════╝");
        System.Console.WriteLine();
        System.Console.WriteLine("Welcome! I'm your AI assistant. Ask me anything or type 'help' for commands.");
        System.Console.WriteLine("Type 'exit', 'quit', or 'bye' to exit the application.");
        System.Console.WriteLine(new string('─', 64));
    }

    public static void DisplayHelp()
    {
        System.Console.WriteLine();
        System.Console.WriteLine("📚 Available Commands:");
        System.Console.WriteLine("  help          - Show this help message");
        System.Console.WriteLine("  clear         - Clear the screen and conversation history");
        System.Console.WriteLine("  exit/quit/bye - Exit the application");
        System.Console.WriteLine();
        System.Console.WriteLine("💡 Tips:");
        System.Console.WriteLine("  • Ask questions naturally - I maintain conversation context");
        System.Console.WriteLine("  • Be specific for better responses");
        System.Console.WriteLine("  • I can help with various topics and tasks");
    }

    public static void DisplayGoodbye()
    {
        System.Console.WriteLine();
        System.Console.WriteLine("👋 Thank you for using PlatfAgent! Goodbye!");
        System.Console.WriteLine();
    }

    public static bool IsCommand(string input, params string[] commands) =>
        commands.Contains(input.Trim().ToLowerInvariant());

    public static string? GetUserInput(string prompt = "\n🤖 You: ")
    {
        System.Console.Write(prompt);
        return System.Console.ReadLine();
    }

    public static void ShowThinking() => System.Console.Write("🤔 Agent: Thinking...");

    public static void ShowResponse(string response)
    {
        // Clear thinking indicator and show response
        System.Console.Write("\r" + new string(' ', 25) + "\r");
        System.Console.WriteLine($"🤖 Agent: {response}");
    }

    public static void ShowError(string error) => System.Console.WriteLine($"\n❌ Error: {error}");
}
