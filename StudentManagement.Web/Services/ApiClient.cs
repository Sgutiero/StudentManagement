using System.Net.Http.Headers;

namespace StudentManagement.Web.Services
{
    public class ApiClient
    {
        private readonly HttpClient _httpClient;
        private readonly IHttpContextAccessor _contextAccessor;

        public ApiClient(HttpClient httpClient, IHttpContextAccessor contextAccessor)
        {
            _httpClient = httpClient;
            _contextAccessor = contextAccessor;
        }

        private void AttachToken()
        {
            var token = _contextAccessor.HttpContext?.Session.GetString("AccessToken");
            if (!string.IsNullOrEmpty(token))
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        }

        public async Task<T?> GetAsync<T>(string url)
        {
            AttachToken();
            var response = await _httpClient.GetAsync(url);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<T>();
        }

        public async Task<HttpResponseMessage> PostAsync<T>(string url, T body)
        {
            AttachToken();
            return await _httpClient.PostAsJsonAsync(url, body);
        }

        public async Task<HttpResponseMessage> DeleteAsync(string url)
        {
            AttachToken();
            return await _httpClient.DeleteAsync(url);
        }

        public async Task<HttpResponseMessage> PutAsync<T>(string url, T body)
        {
            AttachToken();
            return await _httpClient.PutAsJsonAsync(url, body);
        }

        public async Task<T?> GetFromJsonAsync<T>(string url)
        {
            AttachToken();
            return await _httpClient.GetFromJsonAsync<T>(url);
        }

        public async Task<HttpResponseMessage> PostAsJsonAsync<T>(string url, T data)
        {
            AttachToken();
            return await _httpClient.PostAsJsonAsync(url, data);
        }
    }
}
