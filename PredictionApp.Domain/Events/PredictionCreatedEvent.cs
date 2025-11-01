using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PredictionApp.Domain.Events
{
    public class PredictionCreatedEvent
    {
        public int PredictionId { get; }
        public DateTime CreatedAt { get; }

        public PredictionCreatedEvent(int id)
        {
            PredictionId = id;
            CreatedAt = DateTime.UtcNow;
        }
    }
}

