namespace PlatfAgent.Console.Configuration;

public class AzureOpenAIConfiguration
{
    public string Endpoint { get; set; } = string.Empty;
    public string ApiKey { get; set; } = string.Empty;
    public string DeploymentName { get; set; } = string.Empty;
    public string ModelName { get; set; } = "gpt-4o";
}

public class AgentConfiguration
{
    public string SystemPrompt { get; set; } = "You are a helpful AI agent. Provide clear, concise, and accurate responses to user questions.";
    public int MaxTokens { get; set; } = 1000;
    public double Temperature { get; set; } = 0.7;
}