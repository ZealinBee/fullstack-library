using AutoMapper;
using IntegrifyLibraryApi.Business;
using IntegrifyLibraryApi.Domain;

namespace IntegrifyLibraryApi.Infrastructure
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<User, UserDto>();
            CreateMap<UserDto, User>()
                .ForMember(dest => dest.Password, opt => opt.Ignore())
            .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => Guid.NewGuid()))
            .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => DateOnly.FromDateTime(DateTime.Now)));

        }
    }
}