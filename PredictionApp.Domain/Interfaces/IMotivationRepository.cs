using PredictionApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PredictionApp.Domain.Interfaces
{
    public interface IMotivationRepository : IRepository<Motivation>
    {
        Task<Motivation?> GetByMessageAsync(string message);
    }
}
