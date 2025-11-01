using PredictionApp.Domain.Interfaces;
using PredictionApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PredictionApp.Domain.Services
{
    public class PredictionService : IPredictionService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly Random _random = new();

        public PredictionService(IUnitOfWork unitOfWork) => _unitOfWork = unitOfWork;

        public async Task<Prediction> GetRandomPredictionAsync()
        {
            var all = await _unitOfWork.Predictions.GetAllAsync();
            var list = all.ToList();
            if (list.Count == 0)
                return new Prediction { Message = "No predictions available." };

            var randomPrediction = list[_random.Next(list.Count)];
            return randomPrediction;
        }
    }
}
