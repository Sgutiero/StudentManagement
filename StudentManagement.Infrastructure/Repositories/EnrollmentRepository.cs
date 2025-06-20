using Microsoft.EntityFrameworkCore;
using StudentManagement.Application.Common.Interfaces;
using StudentManagement.Domain.Entities;
using StudentManagement.Infrastructure.Context;

namespace StudentManagement.Infrastructure.Repositories
{
    public class EnrollmentRepository : IEnrollmentRepository
    {
        private readonly AppDbContext _context;

        public EnrollmentRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Enrollment>> GetByStudentIdAsync(Guid studentId)
        {
            return await _context.Enrollments
                .Include(e => e.Subject)
                .Where(e => e.StudentId == studentId)
                .ToListAsync();
        }

        public async Task AddAsync(Enrollment enrollment)
        {
            await _context.Enrollments.AddAsync(enrollment);
        }

        public void Delete(Enrollment enrollment)
        {
            _context.Enrollments.Remove(enrollment);
        }
    }
}
