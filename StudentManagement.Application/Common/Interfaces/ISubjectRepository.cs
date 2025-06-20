using StudentManagement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentManagement.Application.Common.Interfaces
{
    public interface ISubjectRepository
    {
        Task<Subject?> GetByIdAsync(Guid id);
        Task<IEnumerable<Subject>> GetAllAsync();
        Task AddAsync(Subject subject);
        Task UpdateAsync(Subject subject);
        void Delete(Subject subject);
    }
}
