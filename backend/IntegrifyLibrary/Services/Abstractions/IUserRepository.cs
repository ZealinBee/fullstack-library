using IntegrifyLibrary.Dto;

namespace IntegrifyLibrary.Services.Abstractions
{
    public interface IUserRepository
    {
        UserDto GetUserById(Guid id);
        List<UserDto> GetAllUsers();
        UserDto CreateUser(UserDto userDto);

    }
}