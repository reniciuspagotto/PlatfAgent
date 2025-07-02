using Azure;
using Azure.AI.OpenAI;
using OpenAI.Chat;
using System.Text.Json;

namespace PlatfAgent.Console;

public class SimpleAIAgent
{
    private readonly ChatClient _chatClient;
    private readonly List<ChatMessage> _conversationHistory;
    private readonly string _systemPrompt;

    /// <summary>
    /// Creates AI agent with API key authentication
    /// </summary>
    public SimpleAIAgent(string endpoint, string apiKey, string deploymentName, 
                        string systemPrompt = "You are a helpful AI agent.")
    {
        _systemPrompt = systemPrompt;
        _conversationHistory = new List<ChatMessage>();
        
        // Initialize Azure OpenAI client following Microsoft example
        var credential = new AzureKeyCredential(apiKey);
        var azureClient = new AzureOpenAIClient(new Uri(endpoint), credential);
        _chatClient = azureClient.GetChatClient(deploymentName);
        
        // Initialize with system prompt - using DeveloperChatMessage as in Microsoft example
        _conversationHistory.Add(new DeveloperChatMessage(_systemPrompt));
    }

    public async Task<string> GetResponseAsync(string userInput)
    {
        try
        {
            _conversationHistory.Add(new UserChatMessage(userInput));

            // Use default options without token limits to avoid parameter conflicts
            var completion = await _chatClient.CompleteChatAsync(_conversationHistory);
            
            var assistantMessage = completion.Value.Content[0].Text ?? "I couldn't generate a response.";
            _conversationHistory.Add(new AssistantChatMessage(assistantMessage));

            return assistantMessage;
        }
        catch (RequestFailedException ex)
        {
            RemoveLastUserMessage();
            
            return ex.Status switch
            {
                429 => "Rate limit exceeded. Please wait a moment and try again.",
                >= 500 => $"Service temporarily unavailable. Please try again later. (Error: {ex.Status})",
                _ => $"Azure OpenAI error: {ex.Message}"
            };
        }
        catch (Exception ex)
        {
            RemoveLastUserMessage();
            return $"Unexpected error: {ex.Message}";
        }
    }

    private void RemoveLastUserMessage()
    {
        if (_conversationHistory.Count > 1)
            _conversationHistory.RemoveAt(_conversationHistory.Count - 1);
    }

    public void ClearHistory()
    {
        _conversationHistory.Clear();
        _conversationHistory.Add(new DeveloperChatMessage(_systemPrompt));
    }
}
