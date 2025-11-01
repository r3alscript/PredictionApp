using PredictionApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace PredictionApp.Infrastructure.Data
{
    public class PredictionAppDbContext : DbContext
    {
        public PredictionAppDbContext(DbContextOptions<PredictionAppDbContext> options) : base(options) { }

        public DbSet<Prediction> Predictions { get; set; }
        public DbSet<Motivation> Motivations { get; set; }
    }
}
