using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Tech.API.Helpers.Responses;
using Tech.DAL.DTOs.SubjectDTOs;
using Tech.Services.Interfaces.Generics;

namespace Tech.API.Controllers.Subjects;

[Route("api/[controller]")]
[ApiController, Authorize]
public class SubjectController(
    IGettable<SubjectDto> service,
    IModification<SubjectDto, SubjectForCreateDto, SubjectForUpdateDto> modService,
    IIncludable<SubjectDto, string[]> includeService,
    IValidator<SubjectForCreateDto> createValidator,
    IValidator<SubjectForUpdateDto> updateValidator,
    ILogger<SubjectController> logger) : ControllerBase
{
    // GET: api/<SubjectController>
    [HttpGet]
    public async Task<IActionResult> GetAllAsync(CancellationToken cancellation = default)
        => Ok(new Response
        {
            Flag = true,
            Message = "Success",
            Data = await service.RetreiveAllAsync(cancellation)
        });

    // GET api/<SubjectController>/id
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

    //GET api/<SubjectController>/categoryId
    [HttpGet("categorys/{categoryId}")]
    public async Task<IActionResult> GetByCategoryIdAsync(
        [FromRoute] long categoryId,
        CancellationToken cancellation = default)
    {
        try
        {
            string[] includes = { "Category" };
            return Ok(new Response
            {
                Flag = true,
                Message = "Success",
                Data = await includeService.RetreiveByIncludesAsync(categoryId, includes, cancellation)
            });
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "An error occured receiving subject");

            return StatusCode(500, new Response
            {
                Flag = false,
                Message = ex.InnerException.Message,
                Data = ex.InnerException.Data
            });
        }
    }

    // POST api/<SubjectController>
    [HttpPost]
    public async Task<IActionResult> CreateAsync(
        [FromBody] SubjectForCreateDto request,
        CancellationToken cancellation = default)
    {
        try
        {
            var validationResult = await createValidator.ValidateAsync(request);

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
            logger.LogError(500, "An error occured while creating course");

            return StatusCode(500, new Response
            {
                Flag = false,
                Message = ex.InnerException.Message,
                Data = ex.InnerException.Data
            });
        }
    }

    // PUT api/<SubjectController>/5
    [HttpPut("{id}")]
    public async Task<IActionResult> ModifyAsync(
        [FromRoute] long id,
        [FromBody] SubjectForUpdateDto request,
        CancellationToken cancellation = default)
    {
        try
        {
            var validationResult = await updateValidator.ValidateAsync(request);

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
                Data = await modService.UpdateAsync(id, request, cancellation)
            });
        }
        catch(Exception ex)
        {
            logger.LogError(500, "An occured error while updated subject.");

            return StatusCode(500, new Response
            {
                Flag = false,
                Message = ex.InnerException.Message,
                Data = ex.InnerException.Data
            });
        }
    }

    // DELETE api/<SubjectController>/5
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
}
