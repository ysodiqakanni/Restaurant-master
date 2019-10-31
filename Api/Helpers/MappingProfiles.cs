using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using DomainModel;
using ServiceLayer.DTO;

namespace Api.Helpers
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<Restaurant, RestaurantResponseDTO>()
                 .ForMember(d => d.Images, opts => opts.MapFrom(source => source.RestaurantImages)); ;
        }
    }
}
