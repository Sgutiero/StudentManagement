using MediatR;
using StudentManagement.Application.AuditLogs.Commands;
using StudentManagement.Application.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentManagement.Application.AuditLogs.Behaviors
{
    public class AuditBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : IBaseCommand
    {
        private readonly IMediator _mediator;
        private readonly ICurrentUserService _currentUser;

        public AuditBehavior(IMediator mediator, ICurrentUserService currentUser)
        {
            _mediator = mediator;
            _currentUser = currentUser;
        }

        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            var response = await next();

            if (request is IAuditableCommand auditable)
            {
                var logCommand = new CreateAuditLogCommand
                {
                    UserEmail = _currentUser.Email,
                    Action = auditable.ActionName,
                    Entity = auditable.EntityName,
                    EntityId = auditable.EntityId.ToString()
                };

                await _mediator.Send(logCommand);
            }

            return response;
        }
    }
}
