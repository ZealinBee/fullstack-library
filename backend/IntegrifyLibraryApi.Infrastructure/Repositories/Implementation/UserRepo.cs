using IntegrifyLibraryApi.Domain;
using IntegrifyLibraryApi.Business;
using Npgsql;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace IntegrifyLibraryApi.Infrastructure
{
    public class UserRepo : IUserRepo
    {
        private readonly DbSet<User> _users;
        private readonly IMapper _mapper;
        private readonly DatabaseContext _context;

        public UserRepo(DatabaseContext context, IMapper mapper)
        {
            _users = context.Users;
            _mapper = mapper;
            _context = context;
        }

        public UserDto GetUserById(Guid id)
        {
            throw new NotImplementedException();
        }

        public List<UserDto> GetAllUsers()
        {
            throw new NotImplementedException();
        }

        public UserDto CreateUser(UserDto userDto)
        {
            _users.Add(_mapper.Map<User>(userDto));
            _context.SaveChanges();
            return userDto;
        }
    }
}