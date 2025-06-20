using MediatR;
using StudentManagement.Application.Enrollments.Commands;
using StudentManagement.Application.Interfaces;
using StudentManagement.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentManagement.Application.Enrollments.Handlers
{
    public class RemoveEnrollmentCommandHandler : IRequestHandler<RemoveEnrollmentCommand, bool>
    {
        private readonly IUnitOfWork _unitOfWork;

        public RemoveEnrollmentCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> Handle(RemoveEnrollmentCommand request, CancellationToken cancellationToken)
        {
            var student = await _unitOfWork.Students.GetByIdAsync(request.StudentId)
                          ?? throw new RuleExceptions("Student not found");

            var subject = await _unitOfWork.Subjects.GetByIdAsync(request.SubjectId)
                          ?? throw new RuleExceptions("Subject not found");

            student.RemoveEnrollment(subject.Id);

            await _unitOfWork.Students.UpdateAsync(student);
            await _unitOfWork.CommitAsync();

            return true;
        }
    }
}
