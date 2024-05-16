using Tech.DAL.DTOs.UserDTOs;

namespace Tech.Services.Interfaces.Users;

public interface IUserService
{
	Task<bool> RemoveAsync(long id, CancellationToken cancellation = default);
	Task<UserDto> RetrieveByIdAsync(long id, CancellationToken cancellation = default);
	Task<UserDto> AddAsync(UserForCreateDto dto, CancellationToken cancellation = default);
	Task<UserDto> ModifyAsync(long id, UserForUpdateDto dto, CancellationToken cancellation = default);
	Task<bool> ChangePasswordAsync(long id, UserForChangePasswordDto dto, CancellationToken cancellation = default);
	Task<UserDto> RetrieveByPhoneNumberAsync(string PhoneNumber, CancellationToken cancellation = default);
	Task<IEnumerable<UserDto>> RetrieveAllAsync(CancellationToken cancellation = default);
	Task<bool> ForgerPasswordAsync(string PhoneNumber, string Password, string ConfirmPassword, CancellationToken cancellation = default);
}
