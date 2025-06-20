using StudentManagement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentManagement.Application.Common.Interfaces
{
    public interface IEnrollmentRepository
    {
        Task<IEnumerable<Enrollment>> GetByStudentIdAsync(Guid studentId);
        Task AddAsync(Enrollment enrollment);
        void Delete(Enrollment enrollment);
    }
}
