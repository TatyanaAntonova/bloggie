using Microsoft.AspNetCore.Mvc;
using Microsoft.SemanticKernel.ChatCompletion;
using Microsoft.SemanticKernel.Connectors.OpenAI;

namespace Bloggie.Web.Controllers;

[Route("api/[controller]")]
[ApiController]
public class HomeController : ControllerBase
{
    private readonly OpenAIChatCompletionService _aiService;
    private readonly ChatHistory _chatHistory;

    public HomeController(IConfiguration configuration)
    {
        // Load configuration and retrieve OpenAI API key
        string model = "gpt-3.5-turbo";
        string key = configuration["OpenAIKey"];

        // Initialize the AI chat service and chat history
        _aiService = new OpenAIChatCompletionService(model, key);
        _chatHistory = new ChatHistory("""
                                           You are a hiking enthusiast who helps people discover fun hikes in their area. You are upbeat and friendly.
                                           You introduce yourself when first saying hello. When helping people out, you always ask them
                                           for this information to inform the hiking recommendation you provide:
                                       
                                           1. Where they are located
                                           2. What hiking intensity they are looking for
                                       
                                           You will then provide three suggestions for nearby hikes that vary in length after you get that information.
                                           You will also share an interesting fact about the local nature on the hikes when making a recommendation.
                                       """);
    }

    [HttpPost]
    public async Task<IActionResult> GetAiBasedResult(string searchText)
    {
        // Add user's input to the conversation history
        _chatHistory.AddUserMessage(searchText);

        // Get the AI's response
        var aiResponse =
            await _aiService.GetChatMessageContentAsync(_chatHistory,
                new OpenAIPromptExecutionSettings { MaxTokens = 400 });

        // Add AI response to chat history and return it as response
        _chatHistory.Add(aiResponse);

        return Ok(new
        {
            userMessage = searchText,
            aiResponse = aiResponse.Content
        });
    }
}
