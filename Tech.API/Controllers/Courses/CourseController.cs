using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Tech.API.Controllers.Courses
{
	[Route("api/[controller]")]
	[ApiController, Authorize]
	public class CourseController : ControllerBase
	{
		[HttpGet]
		public IActionResult Get()
			=> Ok("Active");
	}
}
