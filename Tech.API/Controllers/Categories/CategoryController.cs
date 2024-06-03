using Microsoft.AspNetCore.Mvc;
using Tech.API.Helpers.Responses;
using Tech.DAL.DTOs.CategoryDTOs;
using Tech.Services.Interfaces.Generics;

namespace Tech.API.Controllers.Categories;

[Route("api/[controller]")]
[ApiController]
public class CategoryController(
    IGettable<CategoryDto> service,
    ILogger<CategoryController> logger) : ControllerBase
{
    // GET: api/<CategoryController>
    [HttpGet]
    public async Task<IActionResult> GetAllAsync(CancellationToken cancellation = default)
    {
        return Ok(new Response
        {
            Flag = true,
            Message = "Success",
            Data = await service.RetreiveAllAsync(cancellation)
        });
    }

    // GET api/<CategoryController>/5
    [HttpGet("{id}")]
    public async Task<IActionResult> GetByIdAsync(
        [FromRoute] long id,
        CancellationToken cancellation = default)
    {
        return Ok(new Response
        {
            Flag = true,
            Message = "Success",
            Data = await service.RetreiveByIdAsync(id, cancellation)
        });
    }
}
