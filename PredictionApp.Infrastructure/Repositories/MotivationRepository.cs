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
    public class MotivationRepository : Repository<Motivation>, IMotivationRepository
    {
        public MotivationRepository(PredictionAppDbContext context) : base(context) { }

        public async Task<Motivation?> GetByMessageAsync(string message)
        {
            return await _context.Motivations
                .FirstOrDefaultAsync(m => m.Message == message);
        }
    }
}
