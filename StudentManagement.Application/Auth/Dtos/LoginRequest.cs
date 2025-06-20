using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentManagement.Application.Auth.Dtos
{
    public class LoginRequest
    {
        public string Email { get; set; } = string.Empty;
        public string Document { get; set; } = string.Empty;
    }

    public class TokenResponse
    {
        public string Token { get; set; } = string.Empty;
    }
}
