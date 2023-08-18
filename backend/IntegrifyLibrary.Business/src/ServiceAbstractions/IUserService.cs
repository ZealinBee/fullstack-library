using IntegrifyLibrary.Domain;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace IntegrifyLibrary.Business
{
    public interface IUserService : IBaseService<CreateUserDto, GetUserDto, UpdateUserDto>
    {

        Task<CreateUserDto> CreateAdmin(CreateUserDto dto);
        Task MakeUserLibrarian(string userEmail);
    }
}
