using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StudentManagement.Application.Common.Models;
using StudentManagement.Application.Common.Queries;
using StudentManagement.Application.Subjects.Commands;
using StudentManagement.Application.Subjects.Dtos;
using StudentManagement.Application.Subjects.Queries;
using System.Net.Mime;

namespace StudentManagement.API.Controllers
{
    /// <summary>
    /// Manages subject creation and retrieval
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    [Produces(MediaTypeNames.Application.Json)]
    public class SubjectsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public SubjectsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Creates a new subject
        /// </summary>
        /// <param name="command">The subject data</param>
        /// <returns>The created subject</returns>
        /// <response code="201">Returns the created subject</response>
        /// <response code="400">If the request is invalid</response>
        /// <response code="401">If the user is not authenticated</response>
        [HttpPost]
        [Authorize]
        [ProducesResponseType(typeof(SubjectDto), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<SubjectDto>> Create([FromBody] CreateSubjectCommand command)
        {
            var result = await _mediator.Send(command);
            return CreatedAtAction(nameof(GetAllSubjects), new { id = result.Id }, result);
        }

        /// <summary>
        /// Retrieves a paginated list of all subjects
        /// </summary>
        /// <param name="pageNumber">Page number (default: 1)</param>
        /// <param name="pageSize">Page size (default: 10)</param>
        /// <returns>Paginated list of subjects</returns>
        /// <response code="200">Returns the list of subjects</response>
        /// <response code="401">If the user is not authenticated</response>
        [HttpGet("getAll")]
        [Authorize]
        [ProducesResponseType(typeof(PagedResult<SubjectDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<PagedResult<SubjectDto>>> GetAllSubjects([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
        {
            var pagination = new PaginationQuery { PageNumber = pageNumber, PageSize = pageSize };
            var result = await _mediator.Send(new GetAllSubjectsQuery(pagination));
            return Ok(result);
        }
    }
}
