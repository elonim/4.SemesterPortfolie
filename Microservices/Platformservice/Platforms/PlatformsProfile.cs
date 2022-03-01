using AutoMapper;
using Platformservice.Dtos;
using Platformservice.Models;

namespace Platformservice.Platforms
{
    public class PlatformsProfile : Profile
    {
        public PlatformsProfile()
        {
            //Source -> Target
            CreateMap<Platform, PlatformReadDto>();
            CreateMap<PlatformCreateDto, Platform>();
        }
    }
}
