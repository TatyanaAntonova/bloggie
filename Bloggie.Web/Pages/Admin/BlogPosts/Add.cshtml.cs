using Bloggie.Web.Data;
using Bloggie.Web.Models.Domain;
using Bloggie.Web.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Bloggie.Web.Pages.Admin.BlogPosts;

public class Add(BloggieDbContext dbContext) : PageModel
{
    [BindProperty] public AddBlogPost AddBlogPostRequest { get; set; }

    public void OnGet()
    {
    }

    public IActionResult OnPost()
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
        dbContext.BlogPosts.Add(blogPost);
        dbContext.SaveChanges();
        
        return Redirect("/Admin/BlogPosts/List");
    }
}