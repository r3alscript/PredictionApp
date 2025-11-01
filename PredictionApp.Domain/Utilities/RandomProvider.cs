using PredictionApp.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PredictionApp.Infrastructure.Utilities
{
    public class RandomProvider : IRandomProvider
    {
        private readonly Random _random = new();
        public int Next(int max) => _random.Next(max);
    }
}
