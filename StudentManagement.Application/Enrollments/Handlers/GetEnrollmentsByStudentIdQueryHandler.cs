using MediatR;
using StudentManagement.Application.Common.Models;
using StudentManagement.Application.Enrollments.Dtos;
using StudentManagement.Application.Enrollments.Queries;
using StudentManagement.Application.Interfaces;
using StudentManagement.Domain.Exceptions;

namespace StudentManagement.Application.Enrollments.Handlers
{
    public class GetEnrollmentsByStudentIdQueryHandler : IRequestHandler<GetEnrollmentsByStudentIdQuery, PagedResult<EnrollmentDto>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetEnrollmentsByStudentIdQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<PagedResult<EnrollmentDto>> Handle(GetEnrollmentsByStudentIdQuery request, CancellationToken cancellationToken)
        {
            var student = await _unitOfWork.Students.GetByIdAsync(request.StudentId)
                          ?? throw new RuleExceptions("Student not found");

            var total = student.Enrollments.Count;
            var items = student.Enrollments
                .Skip((request.Pagination.PageNumber - 1) * request.Pagination.PageSize)
                .Take(request.Pagination.PageSize)
                .Select(e => new EnrollmentDto
                {
                    SubjectId = e.SubjectId,
                    SubjectName = e.Subject.Name,
                    Credits = e.Subject.Credits
                });

            return new PagedResult<EnrollmentDto>
            {
                Items = items,
                TotalItems = total,
                PageNumber = request.Pagination.PageNumber,
                PageSize = request.Pagination.PageSize
            };
        }
    }
}
