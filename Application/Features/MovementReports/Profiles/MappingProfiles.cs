using Application.Features.MovementReports.Dtos;
using AutoMapper;
using Domain.Entities;

namespace Application.Features.MovementReports.Profiles
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<MovementReport, GetMovementReportDto>()
                 .ForMember(dest => dest.PersonFullName,
                            opt => opt.MapFrom(src => src.Person.FirstName + " " + src.Person.LastName))
                 .ForMember(dest => dest.EntryTime,
                            opt => opt.MapFrom(src => src.FirstEntryTime))
                 .ForMember(dest => dest.ExitTime,
                            opt => opt.MapFrom(src => src.LastEntryTime))
                .ReverseMap();

        }
    }
}
