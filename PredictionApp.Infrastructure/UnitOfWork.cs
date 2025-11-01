using PredictionApp.Domain.Interfaces;
using PredictionApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PredictionApp.Infrastructure.Data;

namespace PredictionApp.Infrastructure.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly PredictionAppDbContext _context;

        public IRepository<Prediction> Predictions { get; }
        public IRepository<Motivation> Motivations { get; }

        public UnitOfWork(
            PredictionAppDbContext context,
            IRepository<Prediction> predictions,
            IRepository<Motivation> motivations)
        {
            _context = context;
            Predictions = predictions;
            Motivations = motivations;
        }

        public async Task<int> CompleteAsync() => await _context.SaveChangesAsync();

        public void Dispose()
        {
            _context.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
