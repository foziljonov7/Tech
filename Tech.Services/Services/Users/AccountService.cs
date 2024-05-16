using Tech.DAL.DTOs.UserDTOs;
using Tech.Domain.Entities;
using Tech.Infrastructure.Interfaces;
using Tech.Services.Commons.Exceptions;
using Tech.Services.Helpers;
using Tech.Services.Interfaces.Users;

namespace Tech.Services.Services.Users;

public class AccountService(
	IRepository<User> repository,
	IAuthService service) : IAccountService
{
	public async Task<string> LoginAsync(LoginDto login, CancellationToken cancellation = default)
	{
		var user = await repository.SelectAsync(x => x.PhoneNumber ==  login.PhoneNumber);
		if (user is null)
			throw new CustomException(404, "Phone number or password wrong!");

		var hasherResult = PasswordHelper.Verify(login.Password, user.Salt, user.Password);
		if (hasherResult is false)
			throw new CustomException(404, "Phone numbe or password wrong!");

		return service.GenerateToken(user);
	}
}
