using MediatR;
using StudentManagement.Application.Common.Models;
using StudentManagement.Application.Interfaces;
using StudentManagement.Application.Subjects.Dtos;
using StudentManagement.Application.Subjects.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentManagement.Application.Subjects.Handlers
{
    public class GetAllSubjectsQueryHandler : IRequestHandler<GetAllSubjectsQuery, PagedResult<SubjectDto>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetAllSubjectsQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<PagedResult<SubjectDto>> Handle(GetAllSubjectsQuery request, CancellationToken cancellationToken)
        {
            var all = await _unitOfWork.Subjects.GetAllAsync();

            var total = all.Count();
            var items = all
                .Skip((request.Pagination.PageNumber - 1) * request.Pagination.PageSize)
                .Take(request.Pagination.PageSize)
                .Select(s => new SubjectDto
                {
                    Id = s.Id,
                    Name = s.Name,
                    Code = s.Code,
                    Credits = s.Credits
                });

            return new PagedResult<SubjectDto>
            {
                Items = items,
                TotalItems = total,
                PageNumber = request.Pagination.PageNumber,
                PageSize = request.Pagination.PageSize
            };
        }
    }
}
