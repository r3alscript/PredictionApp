using Microsoft.AspNetCore.Mvc;
using System;

namespace PredictionApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TestController : ControllerBase
    {
        [HttpGet("throw-filter")]
        public IActionResult ThrowException()
        {
            throw new InvalidOperationException("Test exception from controller");
        }
    }
}
