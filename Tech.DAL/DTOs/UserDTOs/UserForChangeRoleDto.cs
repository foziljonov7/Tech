using Tech.Domain.Enums;

namespace Tech.DAL.DTOs.UserDTOs;

public class UserForChangeRoleDto
{
    public long Id { get; set; }
    public Roles Role { get; set; }
}
