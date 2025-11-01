using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PredictionApp.Domain.Events
{
    public class MotivationCreatedEvent
    {
        public int MotivationId { get; }
        public DateTime CreatedAt { get; }

        public MotivationCreatedEvent(int id)
        {
            MotivationId = id;
            CreatedAt = DateTime.UtcNow;
        }
    }
}
