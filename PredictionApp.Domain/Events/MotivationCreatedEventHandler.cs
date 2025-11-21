using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PredictionApp.Domain.Events;
using PredictionApp.Domain.Interfaces;
using Microsoft.Extensions.Logging;

namespace PredictionApp.Infrastructure.Events
{
    public class MotivationCreatedEventHandler : IEventHandler<MotivationCreatedEvent>
    {
        private readonly ILogger<MotivationCreatedEventHandler> _logger;

        public MotivationCreatedEventHandler(ILogger<MotivationCreatedEventHandler> logger)
        {
            _logger = logger;
        }

        public void Handle(MotivationCreatedEvent @event)
        {
            _logger.LogInformation($"New motivation created! ID: {@event.MotivationId}, Time: {@event.CreatedAt}");
        }
    }
}

