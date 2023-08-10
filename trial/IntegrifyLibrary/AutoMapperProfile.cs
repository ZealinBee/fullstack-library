using AutoMapper;
using IntegrifyLibrary.Domain.Dtos;
using IntegrifyLibrary.Domain.Entities;

namespace IntegrifyLibrary.Application
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<User, ReadUserDto>();
            CreateMap<CreateUserDto, User>();
        }
    }
}
