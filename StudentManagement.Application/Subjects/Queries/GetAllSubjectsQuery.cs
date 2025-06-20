using MediatR;
using StudentManagement.Application.Common.Models;
using StudentManagement.Application.Common.Queries;
using StudentManagement.Application.Subjects.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentManagement.Application.Subjects.Queries
{
    public class GetAllSubjectsQuery : IRequest<PagedResult<SubjectDto>>
    {
        public PaginationQuery Pagination { get; set; }

        public GetAllSubjectsQuery(PaginationQuery pagination)
        {
            Pagination = pagination;
        }
    }
}
