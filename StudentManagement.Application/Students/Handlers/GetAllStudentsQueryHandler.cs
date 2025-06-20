using MediatR;
using StudentManagement.Application.Common.Models;
using StudentManagement.Application.Interfaces;
using StudentManagement.Application.Students.Dtos;
using StudentManagement.Application.Students.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentManagement.Application.Students.Handlers
{
    public class GetAllStudentsQueryHandler : IRequestHandler<GetAllStudentsQuery, PagedResult<StudentDto>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetAllStudentsQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<PagedResult<StudentDto>> Handle(GetAllStudentsQuery request, CancellationToken cancellationToken)
        {
            var all = await _unitOfWork.Students.GetAllAsync();

            var total = all.Count();
            var items = all
                .Skip((request.Pagination.PageNumber - 1) * request.Pagination.PageSize)
                .Take(request.Pagination.PageSize)
                .Select(s => new StudentDto
                {
                    Id = s.Id,
                    Name = s.Name,
                    Email = s.Email,
                    Document = s.Document
                });

            return new PagedResult<StudentDto>
            {
                Items = items,
                TotalItems = total,
                PageNumber = request.Pagination.PageNumber,
                PageSize = request.Pagination.PageSize
            };
        }
    }
}
