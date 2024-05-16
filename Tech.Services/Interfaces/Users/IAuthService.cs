using Tech.Domain.Entities;

namespace Tech.Services.Interfaces.Users;

public interface IAuthService
{
	string GenerateToken(User user);
}
