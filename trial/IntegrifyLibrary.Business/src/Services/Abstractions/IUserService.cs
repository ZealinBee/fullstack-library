using IntegrifyLibrary.Domain;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace IntegrifyLibrary.Business
{
    public interface IUserService
    {
        Task<ReadUserDto> GetUserByIdAsync(Guid id);
        Task<UserDto> CreateUserAsync(CreateUserDto user);
        Task<List<UserDto>> GetAllUsersAsync();
        // Task<UpdateUserDto> UpdateUserAsync(UpdateUserDto user);
    }
}
