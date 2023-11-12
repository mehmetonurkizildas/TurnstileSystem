using Application.Features.Persons.Dtos;
using AutoMapper;
using Domain.Entities;

namespace Application.Features.Persons.Profiles
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<Person, PersonDto>()
                .ForMember(dest => dest.FullName,
                           opt => opt.MapFrom(src => src.FirstName + " " + src.LastName))
                .ReverseMap();
        }
    }
}
