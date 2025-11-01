using Microsoft.AspNetCore.Mvc;
using PredictionApp.Domain.Interfaces;

namespace PredictionApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MotivationsController : ControllerBase
    {
        private readonly IMotivationService _service;

        public MotivationsController(IMotivationService service)
        {
            _service = service;
        }

        [HttpGet("today")]
        public async Task<IActionResult> Get()
        {
            var motivation = await _service.GetRandomMotivationAsync();
            return Ok(new
            {
                date = DateTime.Today,
                motivation.Id,
                motivation.Message
            });
        }
    }
}

