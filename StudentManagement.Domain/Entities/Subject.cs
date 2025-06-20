using System.ComponentModel.DataAnnotations;

namespace StudentManagement.Domain.Entities
{
    public class Subject
    {
        public Guid Id { get; private set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; private set; }

        [Required]
        [MaxLength(10)]
        public string Code { get; private set; }

        [Range(1, 10)]
        public int Credits { get; private set; }

        private readonly List<Enrollment> _enrollments = new();
        public IReadOnlyCollection<Enrollment> Enrollments => _enrollments.AsReadOnly();

        private Subject() { }

        public Subject(string name, string code, int credits)
        {
            Id = Guid.NewGuid();
            Name = name;
            Code = code;
            Credits = credits;
        }

        public void Update(string name, string code, int credits)
        {
            Name = name;
            Code = code;
            Credits = credits;
        }
    }
}
