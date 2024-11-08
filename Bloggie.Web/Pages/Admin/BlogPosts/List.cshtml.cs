using Bloggie.Web.Data;
using Bloggie.Web.Models.Domain;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Bloggie.Web.Pages.Admin.BlogPosts;

public class List(BloggieDbContext dbContext) : PageModel
{
    public List<BlogPost> BlogPosts { get; set; }

    public void OnGet()
    {
        BlogPosts = dbContext.BlogPosts.ToList();
    }
}