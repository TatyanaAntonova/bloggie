using System.Text.Json;
using Bloggie.Web.Enums;
using Bloggie.Web.Models.Domain;
using Bloggie.Web.Models.ViewModels;
using Bloggie.Web.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Bloggie.Web.Pages.Admin.BlogPosts;

public class Edit(IBlogPostRepository blogPostRepository) : PageModel
{
    [BindProperty] public BlogPost BlogPost { get; set; }
    [BindProperty] public IFormFile FeaturedImage { get; set; }

    public async Task OnGet(Guid id)
    {
        BlogPost = await blogPostRepository.GetByIdAsync(id);
    }

    public async Task<IActionResult> OnPostEdit()
    {
        try
        {
            await blogPostRepository.UpdateAsync(BlogPost);

            ViewData["Notification"] = new Notification
            {
                Message = "Blog post was successfully updated.",
                Type = NotificationType.Success
            };
        }
        catch (Exception e)
        {
            ViewData["Notification"] = new Notification
            {
                Message = "Something went wrong. Please try again.",
                Type = NotificationType.Error
            };
        }

        return Page();
    }

    public async Task<IActionResult> OnPostDelete()
    {
        var deleted = await blogPostRepository.DeleteAsync(BlogPost.Id);
        if (deleted)
        {
            var notification = ViewData["Notification"] = new Notification
            {
                Message = "Blog post was successfully deleted.",
                Type = NotificationType.Success
            };
            TempData["Notification"] = JsonSerializer.Serialize(notification);
            return RedirectToPage("/Admin/BlogPosts/List");
        }

        return Page();
    }
}