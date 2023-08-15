using IntegrifyLibrary.Domain;
using IntegrifyLibrary.Business;

using Microsoft.EntityFrameworkCore;

namespace IntegrifyLibrary.Infrastructure;

public class UserRepo : BaseRepo<User>, IUserRepo
{
    public UserRepo(DatabaseContext context) : base(context)
    {

    }
    public User GetOneByEmail(string email)
    {
        return _context.Users.FirstOrDefault(u => u.Email == email);
    }

    public User CreateAdmin(User user)
    {
        user.Role = Role.Librarian;
        return user;
    }
}