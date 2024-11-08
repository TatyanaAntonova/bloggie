using Bloggie.Web.Data;
using Bloggie.Web.Models.Domain;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace Bloggie.Web.Pages.Admin.BlogPosts;

public class List(BloggieDbContext dbContext) : PageModel
{
    public List<BlogPost> BlogPosts { get; set; }

    public async Task OnGet()
    {
        BlogPosts = await dbContext.BlogPosts.ToListAsync();
    }
}