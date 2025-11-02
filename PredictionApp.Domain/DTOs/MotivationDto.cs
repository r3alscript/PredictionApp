using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PredictionApp.Domain.DTOs
{
    public class MotivationDto
    {
        public int Id { get; set; }
        public string Message { get; set; } = string.Empty;
    }
}