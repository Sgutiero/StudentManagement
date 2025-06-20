using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentManagement.Application.Interfaces
{
    public interface IJwtService
    {
        string GenerateToken(Guid studentId, string email);
    }
}
