using MediatR;
using StudentManagement.Application.Common.Models;
using StudentManagement.Application.Common.Queries;
using StudentManagement.Application.Enrollments.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentManagement.Application.Enrollments.Queries
{
    public class GetEnrollmentsByStudentIdQuery : IRequest<PagedResult<EnrollmentDto>>
    {
        public Guid StudentId { get; set; }
        public PaginationQuery Pagination { get; set; }

        public GetEnrollmentsByStudentIdQuery(Guid studentId, PaginationQuery pagination)
        {
            StudentId = studentId;
            Pagination = pagination;
        }
    }
}
