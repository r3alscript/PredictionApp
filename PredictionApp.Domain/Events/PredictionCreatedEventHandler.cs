using PredictionApp.Domain.Events;
using PredictionApp.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PredictionApp.Infrastructure.Events
{
    public class PredictionCreatedEventHandler : IEventHandler<PredictionCreatedEvent>
    {
        public void Handle(PredictionCreatedEvent @event)
        {
            Console.WriteLine($"New prediction created (ID: {@event.PredictionId}) at {@event.CreatedAt}");
        }
    }
}
