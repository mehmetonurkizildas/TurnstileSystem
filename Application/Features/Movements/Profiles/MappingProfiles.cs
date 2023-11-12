using Application.Features.Movements.Command.EnterMovement;
using Application.Features.Movements.Command.ExitMovement;
using Application.Features.Movements.Dtos;
using AutoMapper;
using Domain.Entities;

namespace Application.Features.Movements.Profiles
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<Movement, EnteredMovementDto>().ReverseMap();
            CreateMap<Movement, EnterMovementCommand>().ReverseMap();

            CreateMap<Movement, ExitedMovementDto>().ReverseMap();
            CreateMap<Movement, ExitMovementCommand>().ReverseMap();

            CreateMap<Movement, MovementGetByIdDto>().ReverseMap();
            CreateMap<Movement, MovementGetByPersonIdDto>()
                 .ForMember(dest => dest.EventType,
                           opt => opt.MapFrom(src => src.EventType.GetEnumDescription()))
                .ReverseMap();

        }
    }
}
