using PredictionApp.Domain.Events;
using PredictionApp.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace PredictionApp.Infrastructure.Events
{
    public class PredictionCreatedEventHandler : IEventHandler<PredictionCreatedEvent>
    {
        private readonly ILogger<PredictionCreatedEventHandler> _logger;

        public PredictionCreatedEventHandler(ILogger<PredictionCreatedEventHandler> logger)
        {
            _logger = logger;
        }

        public void Handle(PredictionCreatedEvent @event)
        {
            _logger.LogInformation($"New prediction created (ID: {@event.PredictionId}) at {@event.CreatedAt}");
        }
    }
}
