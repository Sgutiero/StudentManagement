using Microsoft.AspNetCore.Mvc;
using StudentManagement.Application.Auth.Dtos;
using StudentManagement.Application.Common.Interfaces;
using StudentManagement.Application.Interfaces;
using System.Net.Mime;

namespace StudentManagement.API.Controllers
{
    /// <summary>
    /// Endpoints for authentication
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    [Produces(MediaTypeNames.Application.Json)]
    public class AuthController : ControllerBase
    {
        private readonly IStudentRepository _studentRepo;
        private readonly IJwtService _jwtService;

        public AuthController(IStudentRepository studentRepo, IJwtService jwtService)
        {
            _studentRepo = studentRepo;
            _jwtService = jwtService;
        }

        /// <summary>
        /// Authenticates a student and returns a JWT token
        /// </summary>
        /// <param name="request">Login credentials</param>
        /// <returns>JWT token</returns>
        /// <response code="200">Returns the generated token</response>
        /// <response code="400">If the model is invalid</response>
        /// <response code="401">If credentials are incorrect</response>
        [HttpPost("login")]
        [ProducesResponseType(typeof(TokenResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var students = await _studentRepo.GetAllAsync();
            var student = students.FirstOrDefault(s =>
                s.Email.Equals(request.Email, StringComparison.OrdinalIgnoreCase) &&
                s.Document == request.Document);

            if (student == null)
                return Unauthorized("Invalid credentials");

            var token = _jwtService.GenerateToken(student.Id, student.Email);
            return Ok(new TokenResponse { Token = token });
        }
    }
}
