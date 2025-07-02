using Azure.AI.OpenAI;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using PlatfAgent.Console.Configuration;
using Azure;

namespace PlatfAgent.Console.Services;

public class AIAgentService : IAIAgentService
{
    private readonly OpenAIClient _openAIClient;
    private readonly AzureOpenAIConfiguration _azureOpenAIConfig;
    private readonly AgentConfiguration _agentConfig;
    private readonly ILogger<AIAgentService> _logger;
    private readonly List<ChatRequestMessage> _conversationHistory;

    public AIAgentService(
        OpenAIClient openAIClient,
        IOptions<AzureOpenAIConfiguration> azureOpenAIConfig,
        IOptions<AgentConfiguration> agentConfig,
        ILogger<AIAgentService> logger)
    {
        _openAIClient = openAIClient;
        _azureOpenAIConfig = azureOpenAIConfig.Value;
        _agentConfig = agentConfig.Value;
        _logger = logger;
        _conversationHistory = new List<ChatRequestMessage>
        {
            new ChatRequestSystemMessage(_agentConfig.SystemPrompt)
        };
    }

    public async Task<string> GetResponseAsync(string userInput, CancellationToken cancellationToken = default)
    {
        try
        {
            _logger.LogInformation("Processing user input: {UserInput}", userInput);

            // Add user message to conversation history
            _conversationHistory.Add(new ChatRequestUserMessage(userInput));

            var chatCompletionsOptions = new ChatCompletionsOptions(_azureOpenAIConfig.DeploymentName, _conversationHistory)
            {
                MaxTokens = _agentConfig.MaxTokens,
                Temperature = (float)_agentConfig.Temperature,
            };

            var response = await _openAIClient.GetChatCompletionsAsync(chatCompletionsOptions, cancellationToken);
            
            var assistantMessage = response.Value.Choices[0].Message.Content;
            
            // Add assistant response to conversation history
            _conversationHistory.Add(new ChatRequestAssistantMessage(assistantMessage));

            _logger.LogInformation("Generated response successfully");
            
            return assistantMessage ?? "I apologize, but I couldn't generate a response. Please try again.";
        }
        catch (RequestFailedException ex)
        {
            _logger.LogError(ex, "Azure OpenAI API request failed: {ErrorMessage}", ex.Message);
            return "I'm experiencing some technical difficulties. Please check your Azure OpenAI configuration and try again.";
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Unexpected error occurred while processing request");
            return "An unexpected error occurred. Please try again later.";
        }
    }
}