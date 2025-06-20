using MediatR;
using StudentManagement.Application.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentManagement.Application.Enrollments.Commands
{
    public class RemoveEnrollmentCommand : IRequest<bool>, IAuditableCommand
    {
        public Guid StudentId { get; set; }
        public Guid SubjectId { get; set; }

        public RemoveEnrollmentCommand(Guid studentId, Guid subjectId)
        {
            StudentId = studentId;
            SubjectId = subjectId;
        }
        public string ActionName => "Create";
        public string EntityName => "Student";
        public Guid EntityId => _id;

        private Guid _id = Guid.NewGuid();
    }
}
