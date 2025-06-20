using StudentManagement.Application.Common.Interfaces;
using StudentManagement.Application.Interfaces;
using StudentManagement.Infrastructure.Context;
using StudentManagement.Infrastructure.Repositories;

namespace StudentManagement.Persistence.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _context;

        public IStudentRepository Students { get; }
        public ISubjectRepository Subjects { get; }
        public IEnrollmentRepository Enrollments { get; }

        public UnitOfWork(AppDbContext context)
        {
            _context = context;
            Students = new StudentRepository(context);
            Subjects = new SubjectRepository(context);
            Enrollments = new EnrollmentRepository(context);
        }

        public async Task<int> CommitAsync()
        {
            return await _context.SaveChangesAsync();
        }
    }
}
