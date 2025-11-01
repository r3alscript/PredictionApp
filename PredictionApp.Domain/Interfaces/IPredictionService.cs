using PredictionApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PredictionApp.Domain.Interfaces
{
    public interface IPredictionService
    {
        Task<Prediction> GetRandomPredictionAsync();
    }
}
