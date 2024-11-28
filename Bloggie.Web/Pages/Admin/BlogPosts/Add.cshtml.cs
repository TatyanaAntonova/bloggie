using System.Text.Json;
using Bloggie.Web.Enums;
using Bloggie.Web.Models.Domain;
using Bloggie.Web.Models.ViewModels;
using Bloggie.Web.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Bloggie.Web.Pages.Admin.BlogPosts;

public class Add(IBlogPostRepository blogPostRepository) : PageModel
{
    [BindProperty] public AddBlogPost AddBlogPostRequest { get; set; }

    [BindProperty] public IFormFile FeaturedImage { get; set; }

    public void OnGet()
    {
    }

    public async Task<IActionResult> OnPost()
    {
        var blogPost = new BlogPost
        {
            Heading = AddBlogPostRequest.Heading,
            PageTitle = AddBlogPostRequest.PageTitle,
            Content = AddBlogPostRequest.Content,
            ShortDescription = AddBlogPostRequest.ShortDescription,
            FeatureImageUrl = AddBlogPostRequest.FeatureImageUrl,
            UrlHandle = AddBlogPostRequest.UrlHandle,
            PublishDate = AddBlogPostRequest.PublishDate,
            Author = AddBlogPostRequest.Author,
            Visible = AddBlogPostRequest.Visible
        };
        await blogPostRepository.CreateAsync(blogPost);

        var notification = new Notification
        {
            Message = "Blog post has been created successfully.",
            Type = NotificationType.Success
        };
        TempData["Notification"] = JsonSerializer.Serialize(notification);

        return Redirect("/Admin/BlogPosts/List");
    }
}