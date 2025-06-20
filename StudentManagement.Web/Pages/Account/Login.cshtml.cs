using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using StudentManagement.Web.Models;
using StudentManagement.Web.Services;

namespace StudentManagement.Web.Pages.Account
{
    public class LoginModel : PageModel
    {
        private readonly AuthService _authService;

        public LoginModel(AuthService authService)
        {
            _authService = authService;
        }

        [BindProperty]
        public LoginViewModel Login { get; set; } = new();

        [BindProperty(SupportsGet = true)]
        public string? ReturnUrl { get; set; }

        public void OnGet()
        {
            ReturnUrl ??= "/Students/Index";
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid) return Page();

            var token = await _authService.Login(Login.Email, Login.Document);
            if (token == null)
            {
                ModelState.AddModelError(string.Empty, "Invalid credentials.");
                return Page();
            }

            HttpContext.Session.SetString("AccessToken", token);
            return LocalRedirect(ReturnUrl ?? "/");
        }
    }
}
