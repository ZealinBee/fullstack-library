
namespace IntegrifyLibraryApi.Business
{
    public interface IUserService
    {
        UserDto GetUserById(Guid id);
        List<UserDto> GetAllUsers();
        UserDto CreateUser(UserDto userDto);

    }
}