using PredictionApp.Domain.Entities;
using PredictionApp.Domain.Interfaces;
using System.Linq;
using System.Threading.Tasks;

namespace PredictionApp.Domain.Services
{
    public class MotivationService : IMotivationService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly Random _random = new();

        public MotivationService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Motivation> GetRandomMotivationAsync()
        {
            var all = (await _unitOfWork.Motivations.GetAllAsync()).ToList();

            if (all.Count == 0)
                return new Motivation { Message = "Motivation seems lost. But you can still do it!" };

            return all[_random.Next(all.Count)];
        }
    }
}