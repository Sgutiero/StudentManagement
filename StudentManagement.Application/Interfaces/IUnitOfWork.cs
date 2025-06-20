using StudentManagement.Application.Common.Interfaces;

namespace StudentManagement.Application.Interfaces
{
    public interface IUnitOfWork
    {
        IStudentRepository Students { get; }
        ISubjectRepository Subjects { get; }
        IEnrollmentRepository Enrollments { get; }

        Task<int> CommitAsync();
    }
}
