using StudentManagement.Application.Enrollments.Dtos;
using StudentManagement.Application.Subjects.Dtos;
using System.ComponentModel.DataAnnotations;

namespace StudentManagement.Web.Models
{
    public class EnrollmentViewModel
    {
        public Guid StudentId { get; set; }
        public List<SubjectDto> AvailableSubjects { get; set; } = new();
        public List<EnrollmentDto> EnrolledSubjects { get; set; } = new();

        [Required(ErrorMessage = "Select at least one subject.")]
        public List<Guid> SelectedSubjectIds { get; set; } = new();
    }
}
