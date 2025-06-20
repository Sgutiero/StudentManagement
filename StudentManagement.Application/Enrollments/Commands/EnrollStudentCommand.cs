using MediatR;
using StudentManagement.Application.Common.Interfaces;
using StudentManagement.Application.Enrollments.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentManagement.Application.Enrollments.Commands
{
    public class EnrollStudentCommand : IRequest<IEnumerable<EnrollmentDto>>, IAuditableCommand
    {
        public Guid StudentId { get; set; }
        public Guid SubjectId { get; set; }
        public string ActionName => "Create";
        public string EntityName => "Student";
        public Guid EntityId => _id;

        private Guid _id = Guid.NewGuid();
    }
}
