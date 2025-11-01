using PredictionApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PredictionApp.Domain.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<Prediction> Predictions { get; }
        IRepository<Motivation> Motivations { get; }
        Task<int> CompleteAsync();
    }
}
