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

        public MotivationService(
            IUnitOfWork unitOfWork,
            IEventHandler<MotivationCreatedEvent> eventHandler,
            IRandomProvider randomProvider)
        {
            _unitOfWork = unitOfWork;
            _eventHandler = eventHandler;
            _randomProvider = randomProvider;
        }

        public async Task<Motivation> GetRandomMotivationAsync()
        {
            var all = (await _unitOfWork.Motivations.GetAllAsync()).ToList();

            if (all.Count == 0)
                return new Motivation { Message = "Motivation seems lost. But you can still do it!" };

            var randomMotivation = all[_randomProvider.Next(all.Count)];
            _eventHandler.Handle(new MotivationCreatedEvent(randomMotivation.Id));
            return randomMotivation;
        }
    }
}