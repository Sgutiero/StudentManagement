using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentManagement.Application.Common.Interfaces
{
    public interface IAuditableCommand : IBaseCommand
    {
        string ActionName { get; }
        string EntityName { get; }
        Guid EntityId { get; }
    }
}
