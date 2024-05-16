using AutoMapper;
using Microsoft.Extensions.Configuration;
using Tech.DAL.DTOs.UserDTOs;
using Tech.Domain.Entities;
using Tech.Infrastructure.Interfaces;
using Tech.Services.Commons.Exceptions;
using Tech.Services.Helpers;
using Tech.Services.Interfaces.Users;

namespace Tech.Services.Services.Users;

public class UserService(
	IRepository<User> repository,
	IMapper mapper,
	IConfiguration configuration) : IUserService
{
	public async Task<UserDto> AddAsync(UserForCreateDto dto, CancellationToken cancellation = default)
	{
		var user = await repository.SelectAsync(x => x.PhoneNumber == dto.PhoneNumber);
		if (user is not null)
			throw new CustomException(404, "User is already exists");

		var hasherResult = PasswordHelper.Hash(dto.Password);
		var mapped = mapper.Map<User>(dto);

		mapped.CreatedAt = TimeHelper.GetServerTime();
		mapped.Salt = hasherResult.Salt;
		mapped.Password = hasherResult.Hash;

		var result = await repository.AddAsync(mapped, cancellation);
		await repository.SaveAsync(cancellation);
		return mapper.Map<UserDto>(result);
	}

	public Task<bool> ChangePasswordAsync(long id, UserForChangePasswordDto dto, CancellationToken cancellation = default)
	{
		throw new NotImplementedException();
	}

	public Task<bool> ForgerPasswordAsync(string PhoneNumber, string Password, string ConfirmPassword, CancellationToken cancellation = default)
	{
		throw new NotImplementedException();
	}

	public Task<UserDto> ModifyAsync(long id, UserForUpdateDto dto, CancellationToken cancellation = default)
	{
		throw new NotImplementedException();
	}

	public Task<bool> RemoveAsync(long id, CancellationToken cancellation = default)
	{
		throw new NotImplementedException();
	}

	public Task<IEnumerable<UserDto>> RetrieveAllAsync(CancellationToken cancellation = default)
	{
		throw new NotImplementedException();
	}

	public Task<UserDto> RetrieveByIdAsync(long id, CancellationToken cancellation = default)
	{
		throw new NotImplementedException();
	}

	public Task<UserDto> RetrieveByPhoneNumberAsync(string PhoneNumber, CancellationToken cancellation = default)
	{
		throw new NotImplementedException();
	}
}
