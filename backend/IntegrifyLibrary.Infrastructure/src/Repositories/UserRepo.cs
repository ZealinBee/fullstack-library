using IntegrifyLibrary.Domain;
using IntegrifyLibrary.Business;

using Microsoft.EntityFrameworkCore;

namespace IntegrifyLibrary.Infrastructure;

public class UserRepo : IUserRepo
{
    private readonly DatabaseContext _context;
    private readonly DbSet<User> _users;

    public UserRepo(DatabaseContext context)
    {
        _context = context;
        _users = context.Set<User>();
    }

    public User GetOneByEmail(string email)
    {
        return _users.FirstOrDefault(u => u.Email == email);
    }

    public User CreateOne(User user)
    {
        _users.Add(user);
        _context.SaveChanges();
        return user;
    }

    public User GetOne(Guid id)
    {
        return _users.FirstOrDefault(u => u.UserId == id);
    }

    public List<User> GetAll()
    {
        return _users.ToList();
    }

    public User UpdateOne(User user)
    {
        _users.Update(user);
        _context.SaveChanges();
        return user;
    }

    public bool DeleteOne(User user)
    {
        _users.Remove(user);
        _context.SaveChanges();
        return true;
    }

}