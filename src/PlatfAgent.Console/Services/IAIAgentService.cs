namespace PlatfAgent.Console.Services;

public interface IAIAgentService
{
    Task<string> GetResponseAsync(string userInput, CancellationToken cancellationToken = default);
}