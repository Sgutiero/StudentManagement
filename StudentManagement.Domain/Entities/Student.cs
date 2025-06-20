using StudentManagement.Domain.Exceptions;
using System.ComponentModel.DataAnnotations;

namespace StudentManagement.Domain.Entities
{
    public class Student
    {
        public Guid Id { get; private set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; private set; }

        [Required]
        [MaxLength(20)]
        public string Document { get; private set; }

        [Required]
        [EmailAddress]
        [MaxLength(100)]
        public string Email { get; private set; }

        public readonly List<Enrollment> _enrollments = new();
        public IReadOnlyCollection<Enrollment> Enrollments => _enrollments.AsReadOnly();

        private Student() { }

        public Student(string name, string document, string email)
        {
            Id = Guid.NewGuid();
            Name = name;
            Document = document;
            Email = email;
        }

        public void EnrollInSubject(Subject subject)
        {
            if (_enrollments.Count(e => e.Subject.Credits > 4) >= 3 && subject.Credits > 4)
                throw new RuleExceptions("Cannot enroll in more than 3 subjects with more than 4 credits.");

            if (_enrollments.Any(e => e.SubjectId == subject.Id))
                throw new RuleExceptions("Already enrolled in this subject.");

            _enrollments.Add(new Enrollment(this, subject));
        }

        public void RemoveEnrollment(Guid subjectId)
        {
            var enrollment = Enrollments.FirstOrDefault(e => e.SubjectId == subjectId);

            if (enrollment == null)
                throw new RuleExceptions("The student is not enrolled in this subject.");

            _enrollments.Remove(enrollment);
        }

        public void Update(string name, string document, string email)
        {
            Name = name;
            Document = document;
            Email = email;
        }
    }
}
