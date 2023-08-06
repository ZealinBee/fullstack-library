using IntegrifyLibraryApi.Domain;
using IntegrifyLibraryApi.Business;
using Npgsql;

namespace IntegrifyLibraryApi.Infrastructure
{
    public interface IUserRepo
    {
        UserDto GetUserById(Guid id);
        List<UserDto> GetAllUsers();
        UserDto CreateUser(UserDto userDto);
    }
}