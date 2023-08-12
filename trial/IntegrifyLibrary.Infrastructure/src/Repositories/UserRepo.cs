using IntegrifyLibrary.Domain;
using Microsoft.EntityFrameworkCore;

using AutoMapper;

namespace IntegrifyLibrary.Infrastructure
{
    public class UserRepo : IUserRepo
    {
        private readonly DatabaseContext _context;
        private readonly IMapper _mapper;

        public UserRepo(DatabaseContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ReadUserDto> GetUserByIdAsync(Guid id)
        {
            var userEntity = await _context.Users
                .AsNoTracking()
                .SingleOrDefaultAsync(u => u.UserId == id);

            return _mapper.Map<ReadUserDto>(userEntity);
        }


        public async Task<List<ReadUserDto>> GetAllUsersAsync()
        {
            var userEntities = await _context.Users
                .AsNoTracking()
                .ToListAsync();
            return _mapper.Map<List<ReadUserDto>>(userEntities);
        }

        public async Task<UserDto> CreateUserAsync(CreateUserDto user)
        {
            var userEntity = _mapper.Map<User>(user); // convert CreateUserDto to User
            await _context.Users.AddAsync(userEntity);
            await _context.SaveChangesAsync();
            return _mapper.Map<UserDto>(userEntity);
        }
    }
}
