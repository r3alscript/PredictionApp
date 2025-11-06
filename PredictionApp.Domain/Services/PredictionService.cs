using AutoMapper;
using Enyim.Caching;
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
        private readonly IMemcachedClient _cache;

        public PredictionService(
            IUnitOfWork unitOfWork,
            IEventHandler<PredictionCreatedEvent> eventHandler,
            IRandomProvider randomProvider,
            IMapper mapper,
            IMemcachedClient cache)
        {
            _unitOfWork = unitOfWork;
            _eventHandler = eventHandler;
            _randomProvider = randomProvider;
            _mapper = mapper;
            _cache = cache;
        }

        public async Task<PredictionDto> GetRandomPredictionAsync()
        {
            const string cacheKey = "all_predictions";
            var cacheResult = await _cache.GetAsync<List<PredictionDto>>(cacheKey);

            List<PredictionDto> predictions;

            if (cacheResult.HasValue)
            {
                Console.WriteLine("Got all predictions from cache.");
                predictions = cacheResult.Value;
            }
            else
            {
                var all = (await _unitOfWork.Predictions.GetAllAsync()).ToList();
                predictions = all.Select(p => _mapper.Map<PredictionDto>(p)).ToList();

                await _cache.SetAsync(cacheKey, predictions, TimeSpan.FromMinutes(5));
                Console.WriteLine("Saved all predictions to Memcached for 5 mins.");
            }

            if (predictions.Count == 0)
                return new PredictionDto { Message = "No predictions available." };

            var randomPrediction = predictions[_randomProvider.Next(predictions.Count)];
            _eventHandler.Handle(new PredictionCreatedEvent(randomPrediction.Id));

            return randomPrediction;
        }
    }
}
