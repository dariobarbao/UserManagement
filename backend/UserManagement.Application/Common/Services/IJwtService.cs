

using UserManagement.Domain.Entities;

namespace UserManagement.Application.Common.Services;

public interface IJwtService
{
    string GenerateToken(User user, out DateTime expiresAt);
}
