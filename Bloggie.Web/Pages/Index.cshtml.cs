using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.SemanticKernel.ChatCompletion;
using Microsoft.SemanticKernel.Connectors.OpenAI;

namespace Bloggie.Web.Pages;

public class IndexModel : PageModel
{
    private readonly ILogger<IndexModel> _logger;
    private readonly OpenAIChatCompletionService _aiService;
    private readonly ChatHistory _chatHistory;

    public IndexModel(ILogger<IndexModel> logger, IConfiguration configuration)
    {
        _logger = logger;

        string model = "gpt-3.5-turbo";
        string key = configuration["OpenAIKey"];

        _aiService = new OpenAIChatCompletionService(model, key);
        _chatHistory = new ChatHistory("""
                                       You are a blogging expert who helps people create engaging, high-quality blog posts. 
                                       You are friendly, encouraging, and insightful. 

                                       Introduce yourself when first saying hello. 
                                       When assisting people, ask them the following questions to understand how best to help with their blogging needs:

                                       1. What is the main topic or niche of their blog?
                                       2. What specific help do they need (e.g., finding topics, writing tips, SEO, or improving readability)?

                                       Once you have this information, provide three detailed suggestions based on their needs. 
                                       If they’re looking for topic ideas, suggest three engaging topics that align with their blog’s niche. 
                                       If they need writing tips, offer practical advice to improve their blog's style, readability, or SEO. 

                                       Additionally, share a fun blogging fact or tip that can inspire or motivate them as they work on their blog.
                                       """);

        // Initialize chat message list
        ChatMessages = new List<ChatMessage>();
    }

    [BindProperty] public string UserMessage { get; set; }

    public string AiResponse { get; private set; }

    // Holds chat messages to display in view
    public List<ChatMessage> ChatMessages { get; }

    public void OnGet()
    {
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (!string.IsNullOrWhiteSpace(UserMessage))
        {
            // Add user message to chat history and list
            _chatHistory.AddUserMessage(UserMessage);
            ChatMessages.Add(new ChatMessage { IsUser = true, Content = UserMessage });

            // Fetch AI response
            var aiResponse = await _aiService.GetChatMessageContentAsync(_chatHistory,
                new OpenAIPromptExecutionSettings { MaxTokens = 400 });
            _chatHistory.Add(aiResponse);

            // Add AI response to chat list
            AiResponse = aiResponse.Content;
            ChatMessages.Add(new ChatMessage { IsUser = false, Content = AiResponse });
        }

        return Page();
    }
}

public class ChatMessage
{
    public bool IsUser { get; set; }
    public string Content { get; set; }
}