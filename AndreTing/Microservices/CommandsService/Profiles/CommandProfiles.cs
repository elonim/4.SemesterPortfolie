
using AutoMapper;
using CommandsService.Dtos;
using CommandsService.Models;
using Platformservice;

namespace CommandsService.Profiles
{
    public class CommandProfiles : Profile
    {
        public CommandProfiles()
        {
            //Source -> Target
            CreateMap<Platform, PlatformReadDto>();
            CreateMap<CommandCreateDto, Command>();
            CreateMap<Command, CommandReadDto>();
            CreateMap<PlatformPublishdDto, Platform>()
                .ForMember(dest =>
                 dest.ExternalID, opt =>
                 opt.MapFrom(src =>
                 src.Id));
            CreateMap<GrpcPlatformModel, Platform>()
                .ForMember(dest =>
                dest.ExternalID, opt =>
                opt.MapFrom(src => src.PlatformId))
                .ForMember(dest =>
                dest.Name, opt =>
                opt.MapFrom(src => src.Name))
                .ForMember(dest =>
                dest.Commands, opt =>
                opt.Ignore());
        }
    }
}