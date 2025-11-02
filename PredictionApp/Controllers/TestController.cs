using Microsoft.AspNetCore.Mvc;
using PredictionApp.Domain.Entities;
using PredictionApp.Domain.Interfaces;
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

        private readonly IUnitOfWork _unitOfWork;

        public TestController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpPost("create-prediction")]
        public async Task<IActionResult> CreatePrediction([FromBody] Prediction prediction)
        {
            await _unitOfWork.Predictions.AddAsync(prediction);
            await _unitOfWork.CompleteAsync();
            return Ok("Prediction created successfully.");
        }

        [HttpPost("create-motivation")]
        public async Task<IActionResult> CreateMotivation([FromBody] Motivation motivation)
        {
            await _unitOfWork.Motivations.AddAsync(motivation);
            await _unitOfWork.CompleteAsync();
            return Ok("Motivation created successfully.");
        }
    }
}
