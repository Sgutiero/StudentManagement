using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using StudentManagement.Web.Models;
using StudentManagement.Web.Services;

namespace StudentManagement.Web.Pages.Subjects
{
    public class CreateModel : PageModel
    {
        private readonly SubjectService _subjectService;

        public CreateModel(SubjectService subjectService)
        {
            _subjectService = subjectService;
        }

        [BindProperty]
        public SubjectFormModel Subject { get; set; } = new();

        public IActionResult OnGet()
        {
            var token = HttpContext.Session.GetString("AccessToken");

            if (string.IsNullOrEmpty(token))
            {
                return RedirectToPage("/Account/Login", new { ReturnUrl = "/Subjects/Create" });
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
                return Page();

            var response = await _subjectService.CreateSubject(Subject);
            if (response.IsSuccessStatusCode)
                return RedirectToPage("Index");

            ModelState.AddModelError(string.Empty, "Error creating subject");
            return Page();
        }
    }
}
