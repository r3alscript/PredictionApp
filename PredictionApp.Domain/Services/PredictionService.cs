using PredictionApp.Domain.Entities;
using PredictionApp.Domain.Events;
using PredictionApp.Domain.Interfaces;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace PredictionApp.Domain.Services
{
    public class PredictionService : IPredictionService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IEventHandler<PredictionCreatedEvent> _eventHandler;
        private readonly Random _random = new();

        public PredictionService(IUnitOfWork unitOfWork, IEventHandler<PredictionCreatedEvent> eventHandler)
        {
            _unitOfWork = unitOfWork;
            _eventHandler = eventHandler;
        }

        public async Task<Prediction> GetRandomPredictionAsync()
        {
            var all = await _unitOfWork.Predictions.GetAllAsync();
            var list = all.ToList();

            if (list.Count == 0)
                return new Prediction { Message = "No predictions available." };

            var randomPrediction = list[_random.Next(list.Count)];

            var evt = new PredictionCreatedEvent(randomPrediction.Id);
            _eventHandler.Handle(evt); 

            return randomPrediction;
        }
    }
}
