using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Tech.API.Helpers.Responses;
using Tech.DAL.DTOs.AttendanceDTOs;
using Tech.DAL.DTOs.CourseDTOs;
using Tech.Services.Interfaces.Attendaces;
using Tech.Services.Interfaces.Generics;

namespace Tech.API.Controllers.Attendances
{
    [Route("api/[controller]")]
    [ApiController]
    public class AttendanceController(
        IGettable<AttendanceDto> service,
        IAttendance attendanceService,
        IValidator<AttendanceForUpdateDto> updateValidator,
        ILogger<AttendanceController> logger) : ControllerBase
    {
        // GET: api/<AttendanceController>
        [HttpGet]
        public async Task<IActionResult> GetAllAsync(CancellationToken cancellation = default)
            => Ok(new Response
            {
                Flag = true,
                Message = "Success",
                Data = await service.RetreiveAllAsync(cancellation)
            });

        // GET api/<AttendanceController>/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync(
            [FromRoute] long id,
            CancellationToken cancellation = default)
            => Ok(new Response
            {
                Flag = true,
                Message = "Success",
                Data = await service.RetreiveByIdAsync(id, cancellation)
            });

        // POST api/<AttendanceController>
        [HttpPost]
        public async Task<IActionResult> MarkAttendaceAsync(
            [FromRoute] long id,
            [FromBody] List<long> studentIds,
            CancellationToken cancellation = default)
        {
            try
            {
                var response = await attendanceService.MarkAttendanceAsync(id, studentIds, cancellation);

                if (!response)
                    return NotFound(new Response
                    {
                        Flag = false,
                        Message = "Id or studentIds not found",
                        Data = null
                    });

                return Ok(new Response
                {
                    Flag = true,
                    Message = "Success",
                    Data = response
                });
            }
            catch(Exception ex)
            {
                logger.LogError(500, "An error occured while mark Attendace.");

                return StatusCode(500, new Response
                {
                    Flag = false,
                    Message = ex.InnerException.Message,
                    Data = ex.InnerException.Data
                });
            }
        }

        // PUT api/<AttendanceController>/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> ModifyAsync(
            [FromRoute] long id,
            [FromBody] AttendanceForUpdateDto request,
            CancellationToken cancellation = default)
        {
            try
            {
                var validationResult = await updateValidator.ValidateAsync(request, cancellation);

                if (!validationResult.IsValid)
                    return BadRequest(new Response
                    {
                        Flag = false,
                        Message = string.Join(Environment.NewLine, validationResult.Errors.Select(request => request.ErrorMessage)),
                        Data = null,
                    });

                var response = await attendanceService.ModifyAsync(id, request, cancellation);

                return Ok(new Response
                {
                    Flag = true,
                    Message = "Success",
                    Data = response
                });
            }
            catch(Exception ex)
            {
                logger.LogError(500, "An error occured while modify attendance.");

                return StatusCode(500, new Response
                {
                    Flag = false,
                    Message = ex.InnerException.Message,
                    Data = ex.InnerException.Data
                });
            }
        }

        // GET api/<AttendanceController>/{id}
        [HttpGet("user/{id}")]
        public async Task<IActionResult> GetByUserAsync(
            [FromRoute] long id,
            CancellationToken cancellation = default)
            => Ok(new Response
            {
                Flag = true,
                Message = "Success",
                Data = await attendanceService.RetreiveByUserAsync(id, cancellation)
            });
    }
}
