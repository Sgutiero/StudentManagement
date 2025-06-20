using Microsoft.EntityFrameworkCore;
using StudentManagement.Application.Common.Interfaces;
using StudentManagement.Domain.Entities;
using StudentManagement.Infrastructure.Context;

namespace StudentManagement.Infrastructure.Repositories
{
    public class SubjectRepository : ISubjectRepository
    {
        private readonly AppDbContext _context;

        public SubjectRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Subject?> GetByIdAsync(Guid id)
        {
            return await _context.Subjects
                .Include(s => s.Enrollments)
                .ThenInclude(e => e.Student)
                .FirstOrDefaultAsync(s => s.Id == id);
        }

        public async Task<IEnumerable<Subject>> GetAllAsync()
        {
            return await _context.Subjects
                .Include(s => s.Enrollments)
                .ThenInclude(e => e.Student)
                .ToListAsync();
        }

        public async Task AddAsync(Subject subject)
        {
            await _context.Subjects.AddAsync(subject);
        }

        public Task UpdateAsync(Subject subject)
        {
            _context.Subjects.Update(subject);
            return Task.CompletedTask;
        }

        public void Delete(Subject subject)
        {
            _context.Subjects.Remove(subject);
        }
    }
}
