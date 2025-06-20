using MediatR;
using StudentManagement.Application.Common.Interfaces;
using StudentManagement.Application.Subjects.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentManagement.Application.Subjects.Commands
{
    public class CreateSubjectCommand : IRequest<SubjectDto>, IAuditableCommand
    {
        public string Name { get; set; } = string.Empty;
        public string Code { get; set; } = string.Empty;
        public int Credits { get; set; }
        public string ActionName => "Create";
        public string EntityName => "Student";
        public Guid EntityId => _id;

        private Guid _id = Guid.NewGuid();
    }
}
