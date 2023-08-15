using IntegrifyLibrary.Domain;
using Npgsql;

namespace IntegrifyLibrary.Domain
{
    public interface IUserRepo
    {
        Task<ReadUserDto> GetUserByIdAsync(Guid id);
        Task<List<ReadUserDto>> GetAllUsersAsync();
        Task<UserDto> CreateUserAsync(CreateUserDto user);
    }
}