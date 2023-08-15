namespace IntegrifyLibrary.Domain;

public interface IUserRepo : IBaseRepo<User>
{
    User GetOneByEmail(string email);
}
