using StudentManagement.Application.Enrollments.Dtos;

namespace StudentManagement.Web.Services
{
    public class EnrollmentService
    {
        private readonly ApiClient _api;

        public EnrollmentService(ApiClient api)
        {
            _api = api;
        }

        public async Task<List<EnrollmentDto>> GetEnrollments(Guid studentId)
        {
            var url = $"https://localhost:5001/api/enrollments/{studentId}";
            var result = await _api.GetAsync<IEnumerable<EnrollmentDto>>(url);
            return result?.ToList() ?? new List<EnrollmentDto>();
        }

        public async Task<HttpResponseMessage> EnrollSubjects(Guid studentId, List<Guid> subjectIds)
        {
            var url = $"https://localhost:5001/api/enrollments/{studentId}";
            return await _api.PostAsync(url, subjectIds);
        }

        public async Task<HttpResponseMessage> RemoveEnrollment(Guid studentId, Guid subjectId)
        {
            var url = $"https://localhost:5001/api/enrollments/{studentId}/subject/{subjectId}";
            return await _api.DeleteAsync(url);
        }
    }
}
