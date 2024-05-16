using Tech.Domain.Enums;

namespace Tech.DAL.DTOs.UserDTOs;

public class UserDto
{
	public string Firstname { get; set; }
	public string Lastname { get; set; }
	public string PhoneNumber { get; set; }
	public string Password { get; set; }
	public Roles Role { get; set; }
}
