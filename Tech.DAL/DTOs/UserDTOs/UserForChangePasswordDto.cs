namespace Tech.DAL.DTOs.UserDTOs;

public class UserForChangePasswordDto
{
    public string OldPassword { get; set; }
    public string NewPassword { get; set; }
    public string ConfirmPassword { get; set; }
}
