using Bloggie.Web.Enums;
using Bloggie.Web.Models.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Bloggie.Web.Pages;

public class Login : PageModel
{
    private readonly SignInManager<IdentityUser> _signInManager;
    [BindProperty] public LoginView LoginViewModel { get; set; }

    public Login(SignInManager<IdentityUser> signInManager)
    {
        _signInManager = signInManager;
    }

    public void OnGet()
    {
    }

    public async Task<IActionResult> OnPostAsync()
    {
        var signInResult = await _signInManager.PasswordSignInAsync(
            LoginViewModel.Username, LoginViewModel.Password, false, false);

        if (signInResult.Succeeded)
        {
            return RedirectToPage("Index");
        }

        ViewData["Notification"] = new Notification
        {
            Type = NotificationType.Error,
            Message = "Invalid username or password"
        };
        return Page();
    }
}