namespace StudentManagement.Web.Services
{
    public class AuthService
    {
        private readonly ApiClient _api;

        public AuthService(ApiClient api)
        {
            _api = api;
        }

        public async Task<string?> Login(string email, string document)
        {
            var loginRequest = new { Email = email, Document = document };
            var response = await _api.PostAsJsonAsync("https://localhost:7221/api/auth/login", loginRequest);

            if (!response.IsSuccessStatusCode) return null;

            var json = await response.Content.ReadFromJsonAsync<Dictionary<string, string>>();
            return json?["token"];
        }
    }
}
