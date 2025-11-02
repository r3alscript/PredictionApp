using Microsoft.AspNetCore.Mvc;
using PredictionApp.Domain.DTOs;
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
        public async Task<ActionResult<MotivationDto>> Get()
        {
            var motivation = await _service.GetRandomMotivationAsync();
            return Ok(motivation);
        }
    }
}

