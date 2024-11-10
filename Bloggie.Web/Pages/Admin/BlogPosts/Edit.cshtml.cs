using Bloggie.Web.Models.Domain;
using Bloggie.Web.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Bloggie.Web.Pages.Admin.BlogPosts;

public class Edit(IBlogPostRepository blogPostRepository) : PageModel
{
    [BindProperty] public BlogPost BlogPost { get; set; }

    public async Task OnGet(Guid id)
    {
        BlogPost = await blogPostRepository.GetByIdAsync(id);
    }

    public async Task<IActionResult> OnPostEdit(BlogPost blogPost)
    {
        await blogPostRepository.UpdateAsync(blogPost);
        return RedirectToPage("/Admin/BlogPosts/List");
    }

    public async Task<IActionResult> OnPostDelete()
    {
        var deleted = await blogPostRepository.DeleteAsync(BlogPost.Id);
        if (deleted)
        {
            return RedirectToPage("/Admin/BlogPosts/List");
        }

        return Page();
    }
}