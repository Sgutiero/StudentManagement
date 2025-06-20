using MediatR;
using StudentManagement.Application.Interfaces;
using StudentManagement.Application.Students.Commands;
using StudentManagement.Application.Students.Dtos;
using StudentManagement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentManagement.Application.Students.Handlers
{
    public class CreateStudentCommandHandler : IRequestHandler<CreateStudentCommand, StudentDto>
    {
        private readonly IUnitOfWork _unitOfWork;

        public CreateStudentCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<StudentDto> Handle(CreateStudentCommand request, CancellationToken cancellationToken)
        {
            var student = new Student(request.Name, request.Document, request.Email);

            await _unitOfWork.Students.AddAsync(student);
            await _unitOfWork.CommitAsync();

            return new StudentDto
            {
                Id = student.Id,
                Name = student.Name,
                Document = student.Document,
                Email = student.Email
            };
        }
    }
}
