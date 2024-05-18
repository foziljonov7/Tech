using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.TagHelpers.Cache;
using System.ComponentModel.DataAnnotations;
using System.Security.Cryptography;
using Tech.API.Helpers.Responses;
using Tech.DAL.DTOs.UserDTOs;
using Tech.Services.Interfaces.Users;

namespace Tech.API.Controllers.Users
{
	[Route("api/[controller]")]
	[ApiController]
	public class UserController(IUserService service) : ControllerBase
	{
		[HttpPost]
		public async Task<IActionResult> Register(
			[FromBody] UserForCreateDto request,
			CancellationToken cancellation = default)
			=> Ok(new Response
			{
				Flag = true,
				Message = "Success",
				Data = await service.AddAsync(request, cancellation)
			});

		[HttpGet]
		public async Task<IActionResult> GetAllAsync(
			CancellationToken cancellation = default)
			=> Ok(new Response
			{
				Flag = true,
				Message = "Success",
				Data = await service.RetrieveAllAsync(cancellation)
			});

		[HttpGet("{id}")]
		public async Task<IActionResult> GetByIdAsync(
			[FromRoute(Name = "id")] long id,
			CancellationToken cancellation = default)
			=> Ok(new Response
			{
				Flag = true,
				Message = "Success",
				Data = await service.RetrieveByIdAsync(id, cancellation)
			});

		[HttpPut("{id}")]
		public async Task<IActionResult> UpdateAsync(
			[FromRoute(Name = "id")] long id,
			[FromBody] UserForUpdateDto request,
			CancellationToken cancellation = default)
			=> Ok(new Response
			{
				Flag = true,
				Message = "Success",
				Data = await service.ModifyAsync(id, request, cancellation)
			});

		[HttpDelete("{id}")]
		public async Task<IActionResult> DeleteAsync(
			[FromRoute(Name = "id")] long id,
			CancellationToken cancellation = default)
			=> Ok(new Response
			{
				Flag = true,
				Message = "Success",
				Data = await service.RemoveAsync(id, cancellation)
			});

		[HttpPut("change-password")]
		public async Task<IActionResult> ChangePasswordAsync(
			[FromRoute(Name = "id")] long id,
			UserForChangePasswordDto request,
			CancellationToken cancellation = default)
			=> Ok(new Response
			{
				Flag = true,
				Message = "Success",
				Data = await service.ChangePasswordAsync(id, request, cancellation)
			});

		[AllowAnonymous]
		[HttpPut("forget-password")]
		public async Task<IActionResult> ForgerPasswordAsync(
			[Required] string PhoneNumber,
			[Required] string NewPassword,
			[Required] string ConfirmPassword,
			CancellationToken cancellation = default)
			=> Ok(new Response
			{
				Flag = true,
				Message = "Success",
				Data = await service.ForgerPasswordAsync(PhoneNumber, NewPassword, ConfirmPassword, cancellation)
			});

		[AllowAnonymous]
		[HttpGet("phone/{phone}")]
		public async Task<IActionResult> RetrieveByPhoneNumberAsync(
			[FromRoute(Name = "phone")] string phone,
			CancellationToken cancellation = default)
			=> Ok(new Response
			{
				Flag = true,
				Message = "Success",
				Data = await service.RetrieveByPhoneNumberAsync(phone, cancellation)
			});
	}
}
