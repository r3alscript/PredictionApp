using PredictionApp.Domain.Interfaces;
using PredictionApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PredictionApp.Infrastructure.Data;

namespace PredictionApp.Infrastructure
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly PredictionAppDbContext _context;

        public IRepository<Prediction> Predictions { get; }
        public IRepository<Motivation> Motivations { get; }

        public UnitOfWork(PredictionAppDbContext context)
        {
            _context = context;
            Predictions = new Repository<Prediction>(_context);
            Motivations = new Repository<Motivation>(_context);
        }

        public async Task<int> CompleteAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
