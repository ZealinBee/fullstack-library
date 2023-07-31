using AutoMapper;
using IntegrifyLibrary.Dto;
using IntegrifyLibrary.Entities;

namespace IntegrifyLibrary
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<User, UserDto>();
            CreateMap<UserDto, User>()
                .ForMember(dest => dest.Password, opt => opt.Ignore());
        }
    }
}