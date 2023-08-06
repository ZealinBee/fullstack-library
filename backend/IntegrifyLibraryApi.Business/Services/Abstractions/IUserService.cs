using IntegrifyLibraryApi.Business;

namespace IntegrifyLibrary.Services.Abstractions
{
    public interface IUserService
    {
        UserDto GetUserById(Guid id);
        List<UserDto> GetAllUsers();
        UserDto CreateUser(UserDto userDto);

    }
}