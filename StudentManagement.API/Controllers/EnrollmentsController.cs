using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StudentManagement.Application.Common.Queries;
using StudentManagement.Application.Enrollments.Commands;
using StudentManagement.Application.Enrollments.Dtos;
using StudentManagement.Application.Enrollments.Queries;
using System.Net.Mime;

namespace StudentManagement.API.Controllers
{
    /// <summary>
    /// Manages enrollments of students in subjects
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    [Produces(MediaTypeNames.Application.Json)]
    public class EnrollmentsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public EnrollmentsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Enrolls a student in a subject
        /// </summary>
        /// <param name="command">The enrollment command</param>
        /// <returns>List of enrolled subjects</returns>
        /// <response code="200">Returns the updated list of enrollments</response>
        /// <response code="400">If the input is invalid</response>
        /// <response code="401">If the user is not authenticated</response>
        [HttpPost]
        [Authorize]
        [ProducesResponseType(typeof(IEnumerable<EnrollmentDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<IEnumerable<EnrollmentDto>>> Enroll([FromBody] EnrollStudentCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
            
        }

        /// <summary>
        /// Gets all enrollments for a student with pagination
        /// </summary>
        /// <param name="studentId">The ID of the student</param>
        /// <param name="pageNumber">The page number</param>
        /// <param name="pageSize">The page size</param>
        /// <returns>Paginated list of enrollments</returns>
        /// <response code="200">Returns the list of enrollments</response>
        /// <response code="401">If the user is not authenticated</response>
        [HttpGet("{studentId}")]
        [Authorize]
        [ProducesResponseType(typeof(IEnumerable<EnrollmentDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<IEnumerable<EnrollmentDto>>> GetByStudent(Guid studentId, [FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
        {
            var pagination = new PaginationQuery
            {
                PageNumber = pageNumber,
                PageSize = pageSize
            };
            var result = await _mediator.Send(new GetEnrollmentsByStudentIdQuery(studentId, pagination));
            return Ok(result);
        }

        /// <summary>
        /// Removes an enrollment from a student
        /// </summary>
        /// <param name="studentId">The ID of the student</param>
        /// <param name="subjectId">The ID of the subject</param>
        /// <returns>No content if successful, not found if not enrolled</returns>
        /// <response code="204">If enrollment was removed successfully</response>
        /// <response code="404">If the enrollment was not found</response>
        /// <response code="401">If the user is not authenticated</response>
        [HttpDelete("{studentId}/{subjectId}")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> RemoveEnrollment(Guid studentId, Guid subjectId)
        {
            var command = new RemoveEnrollmentCommand(studentId, subjectId);
            var result = await _mediator.Send(command);
            return result ? NoContent() : NotFound();
        }
    }
}
