using System.Text.Json;
using Bloggie.Web.Models.Domain;
using Bloggie.Web.Models.ViewModels;
using Bloggie.Web.Repositories;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Bloggie.Web.Pages.Admin.BlogPosts;

public class List(IBlogPostRepository blogPostRepository) : PageModel
{
    public List<BlogPost> BlogPosts { get; set; }

    public async Task OnGet()
    {
        var notificationJson = TempData["Notification"]?.ToString();
        if (notificationJson != null)
        {
            ViewData["Notification"] = JsonSerializer.Deserialize<Notification>(notificationJson);
        }
        BlogPosts = (await blogPostRepository.GetAllAsync()).ToList();
    }
}