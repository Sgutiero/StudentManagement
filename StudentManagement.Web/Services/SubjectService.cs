using StudentManagement.Application.Common.Models;
using StudentManagement.Application.Students.Dtos;
using StudentManagement.Application.Subjects.Commands;
using StudentManagement.Application.Subjects.Dtos;
using StudentManagement.Web.Models;

namespace StudentManagement.Web.Services
{
    public class SubjectService
    {
        private readonly ApiClient _api;

        public SubjectService(ApiClient api)
        {
            _api = api;
        }

        public async Task<List<SubjectDto>> GetAllAsync()
        {
            var response = await _api.GetFromJsonAsync<List<SubjectDto>>("https://localhost:5001/api/subjects");
            return response ?? new List<SubjectDto>();
        }

        public async Task<PagedResult<SubjectDto>?> GetPagedSubjects(int pageNumber, int pageSize)
        {
            var url = $"https://localhost:5001/api/subjects/paged?pageNumber={pageNumber}&pageSize={pageSize}";
            return await _api.GetAsync<PagedResult<SubjectDto>>(url);
        }

        public async Task<SubjectDto?> GetByIdAsync(Guid id)
        {
            return await _api.GetAsync<SubjectDto>($"https://localhost:5001/api/subjects/{id}");
        }

        public async Task<HttpResponseMessage> CreateSubject(SubjectFormModel model)
        {
            var command = new CreateSubjectCommand
            {
                Name = model.Name,
                Code = model.Code,
                Credits = model.Credits
            };

            return await _api.PostAsync("https://localhost:5001/api/subjects", command);
        }

        public async Task<HttpResponseMessage> UpdateSubject(Guid id, SubjectFormModel model)
        {
            var command = new UpdateSubjectCommand
            {
                Id = id,
                Name = model.Name,
                Code = model.Code,
                Credits = model.Credits
            };

            return await _api.PutAsync($"https://localhost:5001/api/subjects/{id}", command);
        }
    }
}
