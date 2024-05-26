using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MagniseTask.Web.Controllers;

[ApiController]
[Route("[controller]")]
public class TestController : ControllerBase
{
    [HttpGet("Test")]
    public IActionResult Test()
    {
        return Ok("Testing");
    }
}
