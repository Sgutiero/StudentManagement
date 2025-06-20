using MediatR;
using StudentManagement.Application.AuditLogs.Commands;
using StudentManagement.Application.Common.Interfaces;
using StudentManagement.Domain.Entities;

namespace StudentManagement.Application.AuditLogs.Handlers
{
    public class CreateAuditLogCommandHandler : IRequestHandler<CreateAuditLogCommand>
    {
        private readonly IAuditLogRepository _repo;

        public CreateAuditLogCommandHandler(IAuditLogRepository repo)
        {
            _repo = repo;
        }

        public async Task<Unit> Handle(CreateAuditLogCommand request, CancellationToken cancellationToken)
        {
            var log = new AuditLog
            {
                UserEmail = request.UserEmail,
                Action = request.Action,
                Entity = request.Entity,
                EntityId = request.EntityId,
                Timestamp = DateTime.UtcNow
            };

            await _repo.AddAsync(log);
            return Unit.Value;
        }
    }
}
