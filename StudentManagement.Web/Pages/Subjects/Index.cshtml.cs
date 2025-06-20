using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using StudentManagement.Application.Common.Models;
using StudentManagement.Application.Subjects.Dtos;
using StudentManagement.Web.Services;

namespace StudentManagement.Web.Pages.Subjects
{
    public class IndexModel : PageModel
    {
        private readonly SubjectService _subjectService;

        public IndexModel(SubjectService subjectService)
        {
            _subjectService = subjectService;
        }

        public PagedResult<SubjectDto>? Subjects { get; set; }
        [BindProperty(SupportsGet = true)]
        public Guid Id { get; set; }

        [BindProperty(SupportsGet = true)]
        public int PageNumber { get; set; } = 1;

        [BindProperty(SupportsGet = true)]
        public int PageSize { get; set; } = 10;

        public async Task<IActionResult> OnGetAsync()
        {
            var token = HttpContext.Session.GetString("AccessToken");
            if (string.IsNullOrEmpty(token))
            {
                return RedirectToPage("/Account/Login", new { ReturnUrl = $"/Subjects/Edit?id={Id}" });
            }

            Subjects = await _subjectService.GetPagedSubjects(PageNumber, PageSize);
            return Page();
        }
    }
}
