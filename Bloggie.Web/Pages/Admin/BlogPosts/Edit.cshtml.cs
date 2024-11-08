using Bloggie.Web.Data;
using Bloggie.Web.Models.Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Bloggie.Web.Pages.Admin.BlogPosts;

public class Edit(BloggieDbContext dbContext) : PageModel
{
    [BindProperty] public BlogPost BlogPost { get; set; }

    public void OnGet(Guid id)
    {
        BlogPost = dbContext.BlogPosts.Find(id);
    }

    public IActionResult OnPostEdit(BlogPost blogPost)
    {
        var existingBlogPost = dbContext.BlogPosts.Find(BlogPost.Id);
        if (existingBlogPost != null)
        {
            existingBlogPost.Heading = BlogPost.Heading;
            existingBlogPost.PageTitle = BlogPost.PageTitle;
            existingBlogPost.Content = BlogPost.Content;
            existingBlogPost.ShortDescription = BlogPost.ShortDescription;
            existingBlogPost.FeatureImageUrl = BlogPost.FeatureImageUrl;
            existingBlogPost.UrlHandle = BlogPost.UrlHandle;
            existingBlogPost.PublishDate = BlogPost.PublishDate;
            existingBlogPost.Author = BlogPost.Author;
            existingBlogPost.Visible = BlogPost.Visible;
            
            dbContext.SaveChanges();
        }
        
        return RedirectToPage("/Admin/BlogPosts/List");
    }

    public IActionResult OnPostDelete()
    {
        var existingBlogPost = dbContext.BlogPosts.Find(BlogPost.Id);
        if (existingBlogPost != null)
        {
            dbContext.BlogPosts.Remove(existingBlogPost);
            dbContext.SaveChanges();
        }
        
        return RedirectToPage("/Admin/BlogPosts/List");
    }
}