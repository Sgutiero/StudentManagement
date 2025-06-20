using MediatR;
using StudentManagement.Application.Common.Exceptions;
using StudentManagement.Application.Common.Interfaces;
using StudentManagement.Application.Subjects.Commands;
using StudentManagement.Domain.Entities;

namespace StudentManagement.Application.Subjects.Handlers
{
    public class UpdateSubjectCommandHandler : IRequestHandler<UpdateSubjectCommand>
    {
        private readonly ISubjectRepository _repo;

        public UpdateSubjectCommandHandler(ISubjectRepository repo)
        {
            _repo = repo;
        }

        public async Task<Unit> Handle(UpdateSubjectCommand request, CancellationToken cancellationToken)
        {
            var subject = await _repo.GetByIdAsync(request.Id) ?? throw new NotFoundException(nameof(Subject), request.Id);
            subject.Update(request.Name, request.Code, request.Credits);

            await _repo.UpdateAsync(subject);
            return Unit.Value;
        }
    }
}
