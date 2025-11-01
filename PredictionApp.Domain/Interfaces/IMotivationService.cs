using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PredictionApp.Domain.Entities;

namespace PredictionApp.Domain.Interfaces
{
    public interface IMotivationService
    {
        Task<Motivation> GetRandomMotivationAsync();
    }
}
