using IntegrifyLibrary.Domain;
using IntegrifyLibrary.Business;

using Microsoft.EntityFrameworkCore;

namespace IntegrifyLibrary.Infrastructure;

public class UserRepo : BaseRepo<User>, IUserRepo
{
    public UserRepo(DatabaseContext context) : base(context)
    {

    }
    public async Task<User> GetOneByEmail(string email)
    {
        return _context.Users.FirstOrDefault(u => u.Email == email);
    }

    public async Task<User> CreateAdmin(User user)
    {
        user.Role = Role.Librarian;
        return user;
    }
}