using MediatR;
using StudentManagement.Application.Common.Models;
using StudentManagement.Application.Common.Queries;
using StudentManagement.Application.Students.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentManagement.Application.Students.Queries
{
    public class GetAllStudentsQuery : IRequest<PagedResult<StudentDto>>
    {
        public PaginationQuery Pagination { get; set; }

        public GetAllStudentsQuery(PaginationQuery pagination)
        {
            Pagination = pagination;
        }
    }
}
