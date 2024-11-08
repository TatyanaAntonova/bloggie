using Bloggie.Web.Data;
using Bloggie.Web.Models.Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Bloggie.Web.Pages.Admin.BlogPosts;

public class Edit(BloggieDbContext dbContext) : PageModel
{
    [BindProperty] public BlogPost BlogPost { get; set; }

    public async Task OnGet(Guid id)
    {
        BlogPost = await dbContext.BlogPosts.FindAsync(id);
    }

    public async Task<IActionResult> OnPostEdit(BlogPost blogPost)
    {
        var existingBlogPost = await dbContext.BlogPosts.FindAsync(BlogPost.Id);
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
        }

        await dbContext.SaveChangesAsync();
        return RedirectToPage("/Admin/BlogPosts/List");
    }

    public async Task<IActionResult> OnPostDelete()
    {
        var existingBlogPost = await dbContext.BlogPosts.FindAsync(BlogPost.Id);
        if (existingBlogPost != null)
        {
            dbContext.BlogPosts.Remove(existingBlogPost);
            await dbContext.SaveChangesAsync();
            return RedirectToPage("/Admin/BlogPosts/List");
        }

        return Page();
    }
}