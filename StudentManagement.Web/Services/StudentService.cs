using StudentManagement.Application.Common.Models;
using StudentManagement.Application.Students.Commands;
using StudentManagement.Application.Students.Dtos;
using StudentManagement.Web.Models;

namespace StudentManagement.Web.Services
{
    public class StudentService
    {
        private readonly ApiClient _api;

        public StudentService(ApiClient api)
        {
            _api = api;
        }

        public async Task<PagedResult<StudentDto>?> GetPagedStudents(int pageNumber, int pageSize)
        {
            var url = $"https://localhost:5001/api/students/paged?pageNumber={pageNumber}&pageSize={pageSize}";
            return await _api.GetAsync<PagedResult<StudentDto>>(url);
        }

        public async Task<HttpResponseMessage> CreateStudent(CreateStudentCommand command)
        {
            return await _api.PostAsync("https://localhost:5001/api/students", command);
        }

        public async Task<StudentDto?> GetByIdAsync(Guid id)
        {
            return await _api.GetAsync<StudentDto>($"https://localhost:5001/api/students/{id}");
        }

        public async Task<HttpResponseMessage> UpdateStudent(Guid id, StudentFormModel model)
        {
            var command = new UpdateStudentCommand
            {
                Id = id,
                Name = model.Name,
                Document = model.Document,
                Email = model.Email
            };

            return await _api.PutAsync($"https://localhost:5001/api/students/{id}", command);
        }
    }
}
