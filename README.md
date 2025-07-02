dotnet run
dotnet restore
dotnet build
dotnet run --project src/PlatfAgent.Console
# PlatfAgent - Azure OpenAI Console Agent

A minimal console application for chatting with Azure OpenAI using the official Azure SDK.

## What You Need

From your Azure OpenAI resource:
- **Endpoint URL** (e.g., `https://your-resource.openai.azure.com/`)
- **API Key**
- **Deployment name** (the name you gave your model deployment in Azure)

## Quick Setup

Update `src/PlatfAgent.Console/appsettings.json` with your Azure OpenAI details:

```json
{
  "AzureOpenAI": {
    "Endpoint": "https://your-resource.openai.azure.com/",
    "Key": "your-api-key-here",
    "Deployment": "your-deployment-name"
  }
}
```

## Usage

```bash
cd src/PlatfAgent.Console
# (Optional) Set environment variables to override config
# export AZURE_OPENAI_ENDPOINT=...
# export AZURE_OPENAI_KEY=...
# export AZURE_OPENAI_DEPLOYMENT=...
dotnet run
```

## Project Structure

```
📁 src/PlatfAgent.Console/
├── Program.cs           # Entry point & config
├── SimpleAIAgent.cs     # AI communication using Azure SDK
├── ConsoleInterface.cs  # UI utilities
└── appsettings.json     # Configuration
```

## Features

- Uses official Azure.AI.OpenAI SDK
- Loads config from appsettings.json (or environment variables)
- Maintains conversation history
- Simple commands: help, clear, exit
- Graceful error handling

## Commands

- `help`   - Show available commands
- `clear`  - Clear conversation history
- `exit`   - Exit application

## Prerequisites

- [.NET 9 SDK](https://dotnet.microsoft.com/download/dotnet/9.0)
- Azure OpenAI Service account with endpoint, API key, and deployed model

## Troubleshooting

- Ensure your endpoint, API key, and deployment name are correct
- If you change `appsettings.json` while the app is running, restart the app to reload config
- For errors, check the console output for details

## License

MIT