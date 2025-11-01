using Microsoft.AspNetCore.Mvc;
using PredictionApp.Domain;
using PredictionApp.Domain.Interfaces;

namespace PredictionApp.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PredictionsController : ControllerBase
    {
        private readonly IPredictionService _service;

        public PredictionsController(IPredictionService service) => _service = service;

        [HttpGet("random")]
        public async Task<IActionResult> GetRandom()
        {
            var result = await _service.GetRandomPredictionAsync();
            return Ok(result);
        }
    }
}
