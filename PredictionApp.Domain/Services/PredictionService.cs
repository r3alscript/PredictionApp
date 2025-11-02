using AutoMapper;
using PredictionApp.Domain.DTOs;
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
        private readonly IMapper _mapper;

        public PredictionService(
            IUnitOfWork unitOfWork,
            IEventHandler<PredictionCreatedEvent> eventHandler,
            IRandomProvider randomProvider,
            IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _eventHandler = eventHandler;
            _randomProvider = randomProvider;
            _mapper = mapper;
        }

        public async Task<PredictionDto> GetRandomPredictionAsync()
        {
            var all = (await _unitOfWork.Predictions.GetAllAsync()).ToList();
            if (all.Count == 0)
                return new PredictionDto { Message = "No predictions available." };

            var randomPrediction = all[_randomProvider.Next(all.Count)];
            _eventHandler.Handle(new PredictionCreatedEvent(randomPrediction.Id));

            return _mapper.Map<PredictionDto>(randomPrediction);
        }
    }
}
