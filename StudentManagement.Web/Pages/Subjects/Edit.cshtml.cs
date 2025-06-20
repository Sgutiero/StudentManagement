using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using StudentManagement.Web.Models;
using StudentManagement.Web.Services;

namespace StudentManagement.Web.Pages.Subjects
{
    public class EditModel : PageModel
    {
        private readonly SubjectService _subjectService;

        public EditModel(SubjectService subjectService)
        {
            _subjectService = subjectService;
        }

        [BindProperty(SupportsGet = true)]
        public Guid Id { get; set; }

        [BindProperty]
        public SubjectFormModel Subject { get; set; } = new();

        public async Task<IActionResult> OnGetAsync()
        {
            var token = HttpContext.Session.GetString("AccessToken");
            if (string.IsNullOrEmpty(token))
            {
                return RedirectToPage("/Account/Login", new { ReturnUrl = $"/Subjects/Edit?id={Id}" });
            }

            var subject = await _subjectService.GetByIdAsync(Id);
            if (subject == null) return NotFound();

            Subject = new SubjectFormModel
            {
                Name = subject.Name,
                Code = subject.Code,
                Credits = subject.Credits
            };

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
                return Page();

            var response = await _subjectService.UpdateSubject(Id, Subject);
            if (response.IsSuccessStatusCode)
                return RedirectToPage("Index");

            ModelState.AddModelError(string.Empty, "Error updating subject.");
            return Page();
        }
    }
}
