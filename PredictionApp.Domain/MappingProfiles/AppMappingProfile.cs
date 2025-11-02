using AutoMapper;
using PredictionApp.Domain.DTOs;
using PredictionApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PredictionApp.Domain.MappingProfiles
{
    public class AppMappingProfile : Profile
    {
        public AppMappingProfile()
        {
            CreateMap<Prediction, PredictionDto>().ReverseMap();
            CreateMap<Motivation, MotivationDto>().ReverseMap();
        }
    }
}
