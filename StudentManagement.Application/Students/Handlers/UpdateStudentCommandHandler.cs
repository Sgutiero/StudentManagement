using MediatR;
using StudentManagement.Application.Common.Exceptions;
using StudentManagement.Application.Common.Interfaces;
using StudentManagement.Application.Students.Commands;
using StudentManagement.Domain.Entities;

namespace StudentManagement.Application.Students.Handlers
{
    public class UpdateStudentCommandHandler : IRequestHandler<UpdateStudentCommand>
    {
        private readonly IStudentRepository _repo;

        public UpdateStudentCommandHandler(IStudentRepository repo)
        {
            _repo = repo;
        }

        public async Task<Unit> Handle(UpdateStudentCommand request, CancellationToken cancellationToken)
        {
            var student = await _repo.GetByIdAsync(request.Id) ?? throw new NotFoundException(nameof(Student), request.Id);
            student.Update(request.Name, request.Document, request.Email);

            await _repo.UpdateAsync(student);
            return Unit.Value;
        }
    }
}
