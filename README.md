# PlatfAgent

An AI-powered console agent built with .NET 9 and Azure OpenAI that provides interactive conversational AI capabilities through a command-line interface.

## Features

- 🤖 Interactive AI conversations using Azure OpenAI
- 💬 Maintains conversation context throughout the session
- ⚡ Real-time responses with typing indicators
- 🎨 Clean, user-friendly console interface
- 📝 Configurable AI parameters (temperature, max tokens, system prompt)
- 🔧 Environment variable support for secure configuration
- 📋 Built-in help system and commands

## Prerequisites

- [.NET 9 SDK](https://dotnet.microsoft.com/download/dotnet/9.0)
- Azure OpenAI Service account with:
  - Azure OpenAI endpoint URL
  - API key
  - Deployed model (e.g., GPT-4o)

## Setup

### 1. Clone the repository

```bash
git clone https://github.com/reniciuspagotto/PlatfAgent.git
cd PlatfAgent
```

### 2. Configure Azure OpenAI

Update the `src/PlatfAgent.Console/appsettings.json` file with your Azure OpenAI details:

```json
{
  "AzureOpenAI": {
    "Endpoint": "https://your-openai-endpoint.openai.azure.com/",
    "ApiKey": "your-api-key-here",
    "DeploymentName": "your-deployment-name",
    "ModelName": "gpt-4o"
  },
  "Agent": {
    "SystemPrompt": "You are a helpful AI agent. Provide clear, concise, and accurate responses to user questions.",
    "MaxTokens": 1000,
    "Temperature": 0.7
  }
}
```

**Alternative**: Use environment variables for secure configuration:
- `AzureOpenAI__Endpoint`
- `AzureOpenAI__ApiKey`
- `AzureOpenAI__DeploymentName`
- `AzureOpenAI__ModelName`

### 3. Build and run

```bash
# Restore dependencies
dotnet restore

# Build the solution
dotnet build

# Run the console application
dotnet run --project src/PlatfAgent.Console
```

## Usage

Once the application starts, you'll see a welcome screen. Simply type your questions or messages and press Enter to interact with the AI agent.

### Available Commands

- `help` - Show available commands and tips
- `clear` - Clear the console screen
- `exit`, `quit`, or `bye` - Exit the application

### Example Interaction

```
🤖 You: What is artificial intelligence?
🤖 Agent: Artificial intelligence (AI) refers to the simulation of human intelligence in machines...

🤖 You: Can you explain it more simply?
🤖 Agent: Sure! AI is basically teaching computers to think and make decisions like humans do...
```

## Configuration Options

### Azure OpenAI Settings

- **Endpoint**: Your Azure OpenAI service endpoint
- **ApiKey**: Your Azure OpenAI API key
- **DeploymentName**: The name of your deployed model
- **ModelName**: The model name (e.g., "gpt-4o", "gpt-35-turbo")

### Agent Settings

- **SystemPrompt**: The initial system message that defines the AI's behavior
- **MaxTokens**: Maximum number of tokens in the response (default: 1000)
- **Temperature**: Controls randomness in responses (0.0-2.0, default: 0.7)

## Project Structure

```
PlatfAgent/
├── src/
│   └── PlatfAgent.Console/
│       ├── Configuration/
│       │   └── AppConfiguration.cs
│       ├── Services/
│       │   ├── IAIAgentService.cs
│       │   └── AIAgentService.cs
│       ├── ConsoleApp.cs
│       ├── Program.cs
│       ├── appsettings.json
│       └── PlatfAgent.Console.csproj
├── PlatfAgent.sln
└── README.md
```

## Dependencies

- **Azure.AI.OpenAI** (2.1.0) - Azure OpenAI client library
- **Microsoft.Extensions.Configuration** (9.0.0) - Configuration management
- **Microsoft.Extensions.DependencyInjection** (9.0.0) - Dependency injection
- **Microsoft.Extensions.Hosting** (9.0.0) - Host builder and application lifetime management
- **Microsoft.Extensions.Logging** (9.0.0) - Logging framework

## Contributing

1. Fork the repository
2. Create a feature branch (`git checkout -b feature/amazing-feature`)
3. Commit your changes (`git commit -m 'Add some amazing feature'`)
4. Push to the branch (`git push origin feature/amazing-feature`)
5. Open a Pull Request

## License

This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details.

## Troubleshooting

### Common Issues

1. **"Azure OpenAI API request failed"**
   - Verify your endpoint URL is correct
   - Check that your API key is valid
   - Ensure your deployment name matches the actual deployment

2. **"An unexpected error occurred"**
   - Check the console logs for detailed error information
   - Verify your .NET 9 SDK installation
   - Ensure all NuGet packages are properly restored

3. **Configuration not loading**
   - Verify the `appsettings.json` file is in the correct location
   - Check that the JSON syntax is valid
   - Ensure environment variables are properly set if using them

For more help, please open an issue in the GitHub repository.