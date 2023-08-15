namespace IntegrifyLibrary.Domain;

public interface IUserRepo : IBaseRepo<User>
{
    User CreateAdmin(User user);
    User GetOneByEmail(string email);
}
