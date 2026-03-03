

namespace UserManagement.Application.Common.DTOs;

public class AuthResponse
{
    public string Token { get; set; }
    public DateTime ExpiresAt { get; set; }
}
