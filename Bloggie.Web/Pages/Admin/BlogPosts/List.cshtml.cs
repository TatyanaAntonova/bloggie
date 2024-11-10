using Bloggie.Web.Models.Domain;
using Bloggie.Web.Repositories;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Bloggie.Web.Pages.Admin.BlogPosts;

public class List(IBlogPostRepository blogPostRepository) : PageModel
{
    public List<BlogPost> BlogPosts { get; set; }

    public async Task OnGet()
    {
        BlogPosts = (await blogPostRepository.GetAllAsync()).ToList();
    }
}