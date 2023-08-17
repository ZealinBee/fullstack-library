namespace IntegrifyLibrary.Domain;

public interface IUserRepo : IBaseRepo<User>
{
    Task<User> CreateAdmin(User user);
    Task<User> GetOneByEmail(string email);
}
