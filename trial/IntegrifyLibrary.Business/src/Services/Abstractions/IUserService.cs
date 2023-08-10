using IntegrifyLibrary.Domain;

namespace IntegrifyLibrary.Business
{
    public interface IUserService
    {
        ReadUserDto GetUserById(Guid id);
        CreateUserDto CreateUser(CreateUserDto user);
        UpdateUserDto UpdateUser(UpdateUserDto user);
    }
}
