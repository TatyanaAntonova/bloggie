using Bloggie.Web.Enums;
using Bloggie.Web.Models.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Bloggie.Web.Pages;

public class Register : PageModel
{
    private readonly UserManager<IdentityUser> userManager;

    public Register(UserManager<IdentityUser> userManager)
    {
        this.userManager = userManager;
    }

    [BindProperty] public RegisterView RegisterViewModel { get; set; }

    public void OnGet()
    {
    }

    public async Task<IActionResult> OnPost()
    {
        var user = new IdentityUser
        {
            UserName = RegisterViewModel.Username,
            Email = RegisterViewModel.Email,
        };
        var identityResult = await userManager.CreateAsync(user, RegisterViewModel.Password);

        if (identityResult.Succeeded)
        {
            ViewData["Notification"] = new Notification
            {
                Type = NotificationType.Success,
                Message = "Registration Successful",
            };

            return Page();
        }

        ViewData["Notification"] = new Notification
        {
            Type = NotificationType.Error,
            Message = "Something went wrong. Try again later.",
        };
        Console.Error.WriteLine(identityResult.Errors.First().Description);
        return Page();
    }
}