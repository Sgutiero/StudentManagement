using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using StudentManagement.Application.Students.Commands;
using StudentManagement.Web.Models;
using StudentManagement.Web.Services;

namespace StudentManagement.Web.Pages.Students
{
    public class CreateModel : PageModel
    {
        private readonly StudentService _studentService;

        public CreateModel(StudentService studentService)
        {
            _studentService = studentService;
        }

        [BindProperty]
        public StudentFormModel Student { get; set; } = new();

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

            var command = new CreateStudentCommand
            {
                Name = Student.Name,
                Document = Student.Document,
                Email = Student.Email
            };

            var response = await _studentService.CreateStudent(command);

            if (response.IsSuccessStatusCode)
                return RedirectToPage("Index");

            ModelState.AddModelError(string.Empty, "Error creating student");
            return Page();
        }
    }
}
