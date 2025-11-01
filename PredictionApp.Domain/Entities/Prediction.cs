using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PredictionApp.Domain.Entities
{
    public class Prediction
    {
        public int Id { get; set; }
        public string Message { get; set; } = string.Empty;
        public DateTime Date { get; set; }
    }
}
