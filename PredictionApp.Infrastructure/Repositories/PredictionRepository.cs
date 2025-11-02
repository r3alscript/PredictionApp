using Microsoft.EntityFrameworkCore;
using PredictionApp.Domain.Entities;
using PredictionApp.Domain.Interfaces;
using PredictionApp.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PredictionApp.Infrastructure.Repositories
{
    public class PredictionRepository : Repository<Prediction>, IPredictionRepository
    {
        public PredictionRepository(PredictionAppDbContext context) : base(context) { }

        public async Task<Prediction?> GetByMessageAsync(string message)
        {
            return await _context.Predictions
                .FirstOrDefaultAsync(p => p.Message == message);
        }
    }
}