namespace StudentManagement.Domain.Entities
{
    public class Enrollment
    {
        public Guid Id { get; private set; }
        public Guid StudentId { get; private set; }
        public Guid SubjectId { get; private set; }

        public Student Student { get; private set; }
        public Subject Subject { get; private set; }

        private Enrollment() { }

        public Enrollment(Student student, Subject subject)
        {
            Id = Guid.NewGuid();
            Student = student;
            Subject = subject;
            StudentId = student.Id;
            SubjectId = subject.Id;
        }
    }
}
