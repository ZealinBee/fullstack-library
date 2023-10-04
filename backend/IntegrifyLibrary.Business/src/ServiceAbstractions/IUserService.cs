using IntegrifyLibrary.Domain;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace IntegrifyLibrary.Business
{
    public interface IUserService : IBaseService<CreateUserDto, GetUserDto, UpdateUserDto>
    {

        Task<GetUserDto> CreateAdmin(CreateUserDto dto);
        Task<string> MakeUserLibrarian(string userEmail);
        Task<GetUserDto> UpdateOwnProfile(Guid id, UpdateUserDto dto);
    }
}
