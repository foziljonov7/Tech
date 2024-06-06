using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Tech.API.Helpers.Responses;
using Tech.DAL.DTOs.PaymentDTOs;
using Tech.Services.Interfaces.Generics;
using Tech.Services.Interfaces.Payments;

namespace Tech.API.Controllers.Payments;

[Route("api/[controller]")]
[ApiController]
public class PaymentController(
    IGettable<GetPaymentDto> service,
    IPayment<GetPaymentDto, PaymentForCourseDto> paymentService,
    IValidator<PaymentForCourseDto> paymentValidator,
    ILogger<PaymentController> logger) : ControllerBase
{
    // GET: api/<PaymentController>
    [HttpGet]
    public async Task<IActionResult> GetAllAsync(
        CancellationToken cancellation = default)
        => Ok(new Response
        {
            Flag = true,
            Message = "Success",
            Data = await service.RetreiveAllAsync(cancellation)
        });

    // GET api/<PaymentController>/{id}
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

    // POST api/<PaymentController>/course
    [HttpPost("course")]
    public async Task<IActionResult> PostPaymentForCourseAsync(
        [FromBody] PaymentForCourseDto request,
        CancellationToken cancellation = default)
    {
        try
        {
            var validationResult = await paymentValidator.ValidateAsync(request, cancellation);

            if (!validationResult.IsValid)
                return BadRequest(new Response
                {
                    Flag = false,
                    Message = string.Join(Environment.NewLine, validationResult.Errors.Select(request => request.ErrorMessage)),
                    Data = null
                });

            var response = await paymentService.PostPaymentForCourseAsync(request, cancellation);

            return Ok(new Response
            {
                Flag = true,
                Message = "Success",
                Data = response
            });
        }
        catch(Exception ex)
        {
            logger.LogError(500, "An error occured while Payment for course.");

            return StatusCode(500, new Response
            {
                Flag = false,
                Message = ex.InnerException.Message,
                Data = ex.InnerException.Data
            });
        }
    }

    // GET api/<PaymentController>/course/{id}
    [HttpGet("course/{id}")]
    public async Task<IActionResult> GetPaymentByCourseAsync(
        [FromRoute] long id,
        CancellationToken cancellation = default)
        => Ok(new Response
        {
            Flag = true,
            Message = "Success",
            Data = await paymentService.GetPaymentByCoursesAsync(id, cancellation)
        });

    // GET api/<PaymentController>/student/{id}
    [HttpGet("student/{id}")]
    public async Task<IActionResult> GetPaymentByStudentAsync(
        [FromRoute] long id,
        CancellationToken cancellation = default)
        => Ok(new Response
        {
            Flag = true,
            Message = "Success",
            Data = await paymentService.GetPaymentByStudentAsync(id, cancellation)
        });
}
