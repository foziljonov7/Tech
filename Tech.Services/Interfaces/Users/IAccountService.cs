using Tech.DAL.DTOs.UserDTOs;

namespace Tech.Services.Interfaces.Users;

public interface IAccountService
{
	Task<string> LoginAsync(LoginDto login, CancellationToken cancellation = default);
}
