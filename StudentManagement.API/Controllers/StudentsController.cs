using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StudentManagement.Application.Common.Models;
using StudentManagement.Application.Common.Queries;
using StudentManagement.Application.Students.Commands;
using StudentManagement.Application.Students.Dtos;
using StudentManagement.Application.Students.Queries;
using System.Net.Mime;

namespace StudentManagement.API.Controllers
{
    /// <summary>
    /// Manages student creation and retrieval
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    [Produces(MediaTypeNames.Application.Json)]
    public class StudentsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public StudentsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Creates a new student
        /// </summary>
        /// <param name="command">The command with student data</param>
        /// <returns>The newly created student</returns>
        /// <response code="201">Returns the created student</response>
        /// <response code="400">If the input is invalid</response>
        /// <response code="401">If the user is not authenticated</response>
        [HttpPost]
        [Authorize]
        [ProducesResponseType(typeof(StudentDto), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<StudentDto>> Create([FromBody] CreateStudentCommand command)
        {
            var student = await _mediator.Send(command);
            return CreatedAtAction(nameof(GetAllStudents), new { id = student.Id }, student);
        }

        /// <summary>
        /// Retrieves a paginated list of all students
        /// </summary>
        /// <param name="pageNumber">Page number (starting at 1)</param>
        /// <param name="pageSize">Page size (default is 10)</param>
        /// <returns>Paginated list of students</returns>
        /// <response code="200">Returns the list of students</response>
        /// <response code="401">If the user is not authenticated</response>
        [HttpGet("getAll")]
        [Authorize]
        [ProducesResponseType(typeof(PagedResult<StudentDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<PagedResult<StudentDto>>> GetAllStudents([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
        {
            var pagination = new PaginationQuery { PageNumber = pageNumber, PageSize = pageSize };
            var result = await _mediator.Send(new GetAllStudentsQuery(pagination));
            return Ok(result);
        }
    }
}
