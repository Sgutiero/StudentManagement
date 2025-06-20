using MediatR;
using StudentManagement.Application.Common.Interfaces;
using StudentManagement.Application.Students.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentManagement.Application.Students.Commands
{
    public class CreateStudentCommand : IRequest<StudentDto>, IAuditableCommand
    {
        public string Name { get; set; } = string.Empty;
        public string Document { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string ActionName => "Create";
        public string EntityName => "Student";
        public Guid EntityId => _id;

        private Guid _id = Guid.NewGuid();
    }
}
