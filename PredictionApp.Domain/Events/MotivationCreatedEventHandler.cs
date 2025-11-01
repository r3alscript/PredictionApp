using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using PredictionApp.Domain.Events;
using PredictionApp.Domain.Interfaces;

namespace PredictionApp.Infrastructure.Events
{
    public class MotivationCreatedEventHandler : IEventHandler<MotivationCreatedEvent>
    {
        public void Handle(MotivationCreatedEvent @event)
        {
            Console.WriteLine($"New motivation created! ID: {@event.MotivationId}, Time: {@event.CreatedAt}");
        }
    }
}

