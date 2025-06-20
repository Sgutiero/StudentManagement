using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using StudentManagement.Web.Models;
using StudentManagement.Web.Services;

namespace StudentManagement.Web.Pages.Students
{
    public class EnrollmentsModel : PageModel
    {
        private readonly EnrollmentService _enrollmentService;
        private readonly SubjectService _subjectService;

        public EnrollmentsModel(EnrollmentService enrollmentService, SubjectService subjectService)
        {
            _enrollmentService = enrollmentService;
            _subjectService = subjectService;
        }

        [BindProperty(SupportsGet = true)]
        public Guid StudentId { get; set; }

        [BindProperty]
        public EnrollmentViewModel Form { get; set; } = new();

        public async Task<IActionResult> OnGetAsync()
        {
            var token = HttpContext.Session.GetString("AccessToken");

            if (string.IsNullOrEmpty(token))
            {
                return RedirectToPage("/Account/Login", new { ReturnUrl = "/Subjects/Create" });
            }

            var allSubjects = await _subjectService.GetAllAsync();
            var enrolled = await _enrollmentService.GetEnrollments(StudentId);

            Form.StudentId = StudentId;
            Form.EnrolledSubjects = enrolled;
            Form.AvailableSubjects = allSubjects
                .Where(s => !enrolled.Any(e => e.SubjectId == s.Id))
                .ToList();

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                // Volver a cargar materias si falló
                var allSubjects = await _subjectService.GetAllAsync();
                var enrolled = await _enrollmentService.GetEnrollments(StudentId);
                Form.EnrolledSubjects = enrolled;
                Form.AvailableSubjects = allSubjects
                    .Where(s => !enrolled.Any(e => e.SubjectId == s.Id))
                    .ToList();
                return Page();
            }

            var response = await _enrollmentService.EnrollSubjects(StudentId, Form.SelectedSubjectIds);

            if (!response.IsSuccessStatusCode)
            {
                ModelState.AddModelError(string.Empty, "Error enrolling subjects.");
                return await OnGetAsync();
            }

            return RedirectToPage("Index");
        }

        public async Task<IActionResult> OnPostRemoveAsync(Guid subjectId)
        {
            var result = await _enrollmentService.RemoveEnrollment(StudentId, subjectId);
            return RedirectToPage(new { StudentId });
        }
    }
}
