using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Tech.API.Helpers.Responses;
using Tech.DAL.DTOs.CourseDTOs;
using Tech.Services.Interfaces.Exports;
using Tech.Services.Interfaces.Generics;

namespace Tech.API.Controllers.Courses
{
    [Route("api/[controller]")]
	[ApiController, Authorize]
	public class CourseController(
		IGettable<CourseDto> service,
		IModification<CourseDto, CourseForCreateDto, CourseForUpdateDto> modService,
		IIncludable<CourseDto, string[]> includeService,
		IExport exportService,
		IValidator<CourseForCreateDto> createValidator,
		IValidator<CourseForUpdateDto> updateValidator,
		ILogger<CourseController> logger) : ControllerBase
	{
		[HttpGet]
		public async Task<IActionResult> GetAllAsync(CancellationToken cancellation = default)
			=> Ok(new Response
			{
				Flag = true,
				Message = "Success",
				Data = await service.RetreiveAllAsync(cancellation)
			});

		[HttpGet("{id}")]
		public async Task<IActionResult> GetAsync(
			[FromRoute] long id,
			CancellationToken cancellation = default)
			=> Ok(new Response
			{
				Flag = true,
				Message = "Success",
				Data = await service.RetreiveByIdAsync(id, cancellation)
			});

		[HttpPost]
		public async Task<IActionResult> CreateAsync(
			[FromBody] CourseForCreateDto request,
			CancellationToken cancellation = default)
		{
			try
			{
				var validationResult = await createValidator.ValidateAsync(request, cancellation);

				if (!validationResult.IsValid)
					return BadRequest(new Response
					{
						Flag = false,
						Message = string.Join(Environment.NewLine, validationResult.Errors.Select(request => request.ErrorMessage)),
						Data = null
					});

				return Ok(new Response
				{
					Flag = true,
					Message = "Success",
					Data = await modService.AddAsync(request, cancellation)
				});
			}
			catch (Exception ex)
			{
				logger.LogError(ex, "An error occured while creating course");

				return StatusCode(500, new Response
				{
					Flag = false,
					Message = ex.InnerException.Message,
					Data = ex.InnerException.Data
				});
			}
		}

		[HttpPut("{id}")]
		public async Task<IActionResult> ModifyAsync(
			[FromRoute] long id,
			[FromBody] CourseForUpdateDto request,
			CancellationToken cancellation = default)
		{
			try
			{
				var validationResult = await updateValidator.ValidateAsync(request, cancellation);

				if (!validationResult.IsValid)
					return BadRequest(new Response
					{
						Flag = true,
						Message = string.Join(Environment.NewLine, validationResult.Errors.Select(request => request.ErrorMessage)),
						Data = null
					});

				return Ok(new Response
				{
					Flag = true,
					Message = "Success",
					Data = await modService.UpdateAsync(id, request, cancellation)
				});
			}
			catch(Exception ex)
			{
				logger.LogError(ex, "An error occured while updated course.");

				return StatusCode(500, new Response
				{
					Flag = false,
					Message = ex.InnerException.Message,
					Data = ex.InnerException.Data
				});
			}
		}

		[HttpDelete("{id}")]
		public async Task<IActionResult> RemoveAsync(
			[FromRoute] long id,
			CancellationToken cancellation = default)
			=> Ok(new Response
			{
				Flag = true,
				Message = "Success",
				Data = await modService.RemoveAsync(id, cancellation)
			});

		[HttpGet("students/{id}")]
		public async Task<IActionResult> GetStudentsAsync(
			[FromRoute] long id,
			CancellationToken cancellation = default)
		{
			try
			{
				string[] includes = { "Students", "Attendaces" };
				return Ok(new Response
				{
					Flag = true,
					Message = "Success",
					Data = await includeService.RetreiveByIncludesAsync(id, includes, cancellation)
				});
			}
			catch(Exception ex)
			{
				logger.LogError(ex, "An error occured receiving course.");

				return StatusCode(500, new Response
				{
					Flag = false,
					Message = ex.InnerException.Message,
					Data = ex.InnerException.Data
				});
			}
		}

		[HttpGet("export")]
		public async Task<IActionResult> ExportToExcelAsync(CancellationToken cancellation = default)
			=> Ok(new Response
			{
				Flag = true,
				Message = "Success",
				Data = await exportService.ExportToExcelAsync(cancellation)
			});
	}
}
