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
        private readonly IRandomProvider _randomProvider;

        public PredictionService(
            IUnitOfWork unitOfWork,
            IEventHandler<PredictionCreatedEvent> eventHandler,
            IRandomProvider randomProvider)
        {
            _unitOfWork = unitOfWork;
            _eventHandler = eventHandler;
            _randomProvider = randomProvider;
        }

        public async Task<Prediction> GetRandomPredictionAsync()
        {
            var all = (await _unitOfWork.Predictions.GetAllAsync()).ToList();

            if (all.Count == 0)
                return new Prediction { Message = "No predictions available." };

            var randomPrediction = all[_randomProvider.Next(all.Count)];
            _eventHandler.Handle(new PredictionCreatedEvent(randomPrediction.Id));
            return randomPrediction;
        }
    }
}
