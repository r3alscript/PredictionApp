using AutoMapper;
using Enyim.Caching;
using PredictionApp.Domain.DTOs;
using PredictionApp.Domain.Entities;
using PredictionApp.Domain.Events;
using PredictionApp.Domain.Interfaces;
using System.Linq;
using System.Threading.Tasks;

namespace PredictionApp.Domain.Services
{
    public class MotivationService : IMotivationService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IEventHandler<MotivationCreatedEvent> _eventHandler;
        private readonly IRandomProvider _randomProvider;
        private readonly IMapper _mapper;
        private readonly IMemcachedClient _cache;

        public MotivationService(
            IUnitOfWork unitOfWork,
            IEventHandler<MotivationCreatedEvent> eventHandler,
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

        public async Task<MotivationDto> GetRandomMotivationAsync()
        {
            const string cacheKey = "all_motivations";
            var cacheResult = await _cache.GetAsync<List<MotivationDto>>(cacheKey);

            List<MotivationDto> motivations;

            if (cacheResult.HasValue)
            {
                Console.WriteLine("Got all motivations from cache.");
                motivations = cacheResult.Value;
            }
            else
            {
                var all = (await _unitOfWork.Motivations.GetAllAsync()).ToList();
                motivations = all.Select(m => _mapper.Map<MotivationDto>(m)).ToList();

                await _cache.SetAsync(cacheKey, motivations, TimeSpan.FromMinutes(5));
                Console.WriteLine("Saved all motivations to Memcached for 5 mins.");
            }

            if (motivations.Count == 0)
                return new MotivationDto { Message = "Motivation seems lost. But you can still do it!" };

            var randomMotivation = motivations[_randomProvider.Next(motivations.Count)];
            _eventHandler.Handle(new MotivationCreatedEvent(randomMotivation.Id));

            return randomMotivation;
        }
    }
}