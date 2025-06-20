using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using StudentManagement.Application.Common.Models;
using StudentManagement.Application.Students.Dtos;
using StudentManagement.Web.Services;

namespace StudentManagement.Web.Pages.Students
{
    public class IndexModel : PageModel
    {
        private readonly StudentService _studentService;

        public IndexModel(StudentService studentService)
        {
            _studentService = studentService;
        }

        public PagedResult<StudentDto>? Students { get; set; }

        [BindProperty(SupportsGet = true)]
        public int PageNumber { get; set; } = 1;

        [BindProperty(SupportsGet = true)]
        public int PageSize { get; set; } = 10;

        public async Task<IActionResult> OnGetAsync()
        {
            var token = HttpContext.Session.GetString("AccessToken");

            if (string.IsNullOrEmpty(token))
            {
                return RedirectToPage("/Account/Login", new { ReturnUrl = "/Subjects/Create" });
            }

            Students = await _studentService.GetPagedStudents(PageNumber, PageSize);
            return Page();
        }
    }
}
