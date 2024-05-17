using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Tech.DAL.DTOs.UserDTOs;
using Tech.Domain.Entities;
using Tech.Domain.Enums;
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

	public async Task<bool> ChangePasswordAsync(long id, UserForChangePasswordDto dto, CancellationToken cancellation = default)
	{
		var user = await repository.SelectAsync(x => x.Id == id);
		if (user is null || !PasswordHelper.Verify(dto.OldPassword, user.Salt, user.Password))
			throw new CustomException(404, "User or password is incorrect");

		else if (dto.NewPassword != dto.ConfirmPassword)
			throw new CustomException(404, "New password and confirm password aren't equal");

		var hash = PasswordHelper.Hash(dto.ConfirmPassword);
		user.Salt = hash.Salt;
		user.Password = hash.Hash;
		var updated = await repository.UpdateAsync(user, cancellation);

		return await repository.SaveAsync(cancellation);
	}

	public async Task<bool> ForgerPasswordAsync(string PhoneNumber, string Password, string ConfirmPassword, CancellationToken cancellation = default)
	{
		var user = await repository.SelectAsync(x => x.PhoneNumber == PhoneNumber);

		if (user is null)
			throw new CustomException(404, "User not found");

		if (Password != ConfirmPassword)
			throw new CustomException(404, "New password and confirm password aren't equal");

		var hash = PasswordHelper.Hash(Password);
		user.Salt = hash.Salt;
		user.Password = hash.Hash;
		var updated = await repository.UpdateAsync(user, cancellation);

		return await repository.SaveAsync(cancellation);
	}

	public async Task<UserDto> ModifyAsync(long id, UserForUpdateDto dto, CancellationToken cancellation = default)
	{
		var user = await repository.SelectAsync(x => x.Id == id);
		if (user is null)
			throw new CustomException(404, "User not found");

		if(dto is not null)
		{
			user.Firstname = string.IsNullOrEmpty(dto.Firstname) ? user.Firstname : dto.Firstname;
			user.Lastname = string.IsNullOrEmpty(dto.Lastname) ? user.Lastname : dto.Lastname;
			user.Password = string.IsNullOrEmpty(dto.Password) ? user.Password : dto.Password;

			user.UpdatedAt = TimeHelper.GetServerTime();
			await repository.UpdateAsync(user, cancellation);
			var result = await repository.SaveAsync(cancellation);
		}
		var person = mapper.Map(dto, user);

		return mapper.Map<UserDto>(person);
	}

	public async Task<bool> RemoveAsync(long id, CancellationToken cancellation = default)
	{
		var user = await repository.ExistAsync(id);
		if (user is false)
			throw new CustomException(404, "User not found");

		await repository.DeleteAsync(id, cancellation);
		return await repository.SaveAsync(cancellation);
	}

	public async Task<IEnumerable<UserDto>> RetrieveAllAsync(CancellationToken cancellation = default)
	{
		var userQuery = await repository.SelectAllAsync(null, null, cancellation);
			
		var users = await userQuery
			.Where(x => x.UserRole.Equals((Roles)0))
			.AsNoTracking()
			.ToListAsync(cancellation);
			
		var mapped = mapper.Map<IEnumerable<UserDto>>(users);

		return mapped;
	}

	public async Task<UserDto> RetrieveByIdAsync(long id, CancellationToken cancellation = default)
	{
		var userQuery = await repository.SelectAllAsync(null, null, cancellation);

		var user = await userQuery
			.Where(x => x.Id == id)
			.AsNoTracking()
			.ToListAsync(cancellation);

		if (user is null)
			throw new CustomException(404, "User not found");

		var mapped = mapper.Map<UserDto>(user);
		return mapped;
	}

	public async Task<UserDto> RetrieveByPhoneNumberAsync(string PhoneNumber, CancellationToken cancellation = default)
	{
		var user = await repository.SelectAsync(x => x.PhoneNumber == PhoneNumber);

		if (user is null)
			throw new CustomException(404, "User not found");

		var mapped = mapper.Map<UserDto>(user);
		return mapped;
	}
}
