using Microsoft.AspNetCore.Mvc;
using Tech.API.Helpers.Responses;
using Tech.DAL.DTOs.UserDTOs;
using Tech.Services.Interfaces.Users;

namespace Tech.API.Controllers.Commons
{
	[Route("api/[controller]")]
	[ApiController]
	public class LoginController(
		IAuthService authService,
		IAccountService accountService) : ControllerBase
	{
		[HttpPost]
		public async ValueTask<IActionResult> Login(
			[FromBody] LoginDto request,
			CancellationToken cancellation = default)
			=> Ok(new Response
			{
				Flag = true,
				Message = "Success",
				Data = await accountService.LoginAsync(request, cancellation)
			});
	}
}
