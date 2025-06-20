using MediatR;
using StudentManagement.Application.Interfaces;
using StudentManagement.Application.Subjects.Commands;
using StudentManagement.Application.Subjects.Dtos;
using StudentManagement.Domain.Entities;

namespace StudentManagement.Application.Subjects.Handlers
{
    public class CreateSubjectCommandHandler : IRequestHandler<CreateSubjectCommand, SubjectDto>
    {
        private readonly IUnitOfWork _unitOfWork;

        public CreateSubjectCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<SubjectDto> Handle(CreateSubjectCommand request, CancellationToken cancellationToken)
        {
            var subject = new Subject(request.Name, request.Code, request.Credits);

            await _unitOfWork.Subjects.AddAsync(subject);
            await _unitOfWork.CommitAsync();

            return new SubjectDto
            {
                Id = subject.Id,
                Name = subject.Name,
                Code = subject.Code,
                Credits = subject.Credits
            };
        }

    }
}
