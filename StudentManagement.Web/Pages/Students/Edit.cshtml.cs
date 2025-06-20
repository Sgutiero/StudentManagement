using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using StudentManagement.Web.Models;
using StudentManagement.Web.Services;

namespace StudentManagement.Web.Pages.Students
{
    public class EditModel : PageModel
    {
        private readonly StudentService _studentService;

        public EditModel(StudentService studentService)
        {
            _studentService = studentService;
        }

        [BindProperty]
        public StudentFormModel Student { get; set; } = new();

        [BindProperty(SupportsGet = true)]
        public Guid Id { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            var token = HttpContext.Session.GetString("AccessToken");

            if (string.IsNullOrEmpty(token))
            {
                return RedirectToPage("/Account/Login", new { ReturnUrl = "/Subjects/Create" });
            }

            var student = await _studentService.GetByIdAsync(Id);
            if (student == null) return NotFound();

            Student = new StudentFormModel
            {
                Name = student.Name,
                Document = student.Document,
                Email = student.Email
            };

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
                return Page();

            var response = await _studentService.UpdateStudent(Id, Student);

            if (response.IsSuccessStatusCode)
                return RedirectToPage("Index");

            ModelState.AddModelError(string.Empty, "Error updating student");
            return Page();
        }
    }
}
