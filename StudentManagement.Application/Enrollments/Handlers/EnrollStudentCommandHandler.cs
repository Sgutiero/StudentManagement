using MediatR;
using StudentManagement.Application.Enrollments.Commands;
using StudentManagement.Application.Enrollments.Dtos;
using StudentManagement.Application.Interfaces;
using StudentManagement.Domain.Entities;
using StudentManagement.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentManagement.Application.Enrollments.Handlers
{
    public class EnrollStudentCommandHandler : IRequestHandler<EnrollStudentCommand, IEnumerable<EnrollmentDto>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public EnrollStudentCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<EnrollmentDto>> Handle(EnrollStudentCommand request, CancellationToken cancellationToken)
        {
            var student = await _unitOfWork.Students.GetByIdAsync(request.StudentId)
                          ?? throw new RuleExceptions("Student not found");

            var subject = await _unitOfWork.Subjects.GetByIdAsync(request.SubjectId)
                          ?? throw new RuleExceptions("Subject not found");

            var enrollment = new Enrollment(student, subject);

            await _unitOfWork.Enrollments.AddAsync(enrollment);
            await _unitOfWork.CommitAsync();

            var enrolledSubjects = student.Enrollments.Select(e => new EnrollmentDto
            {
                SubjectId = e.SubjectId,
                SubjectName = e.Subject.Name,
                Credits = e.Subject.Credits
            });

            return enrolledSubjects;
        }
    }
}
