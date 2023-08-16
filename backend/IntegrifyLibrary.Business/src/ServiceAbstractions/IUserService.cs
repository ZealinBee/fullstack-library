using IntegrifyLibrary.Domain;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace IntegrifyLibrary.Business
{
    public interface IUserService : IBaseService<CreateUserDto, GetUserDto, UpdateUserDto>
    {

        CreateUserDto CreateAdmin(CreateUserDto dto);
    }
}
