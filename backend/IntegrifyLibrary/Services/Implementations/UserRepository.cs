using IntegrifyLibrary.Services.Abstractions;
using IntegrifyLibrary.Entities;
using IntegrifyLibrary.Dto;
using AutoMapper;

namespace IntegrifyLibrary.Services.Implementations
{
    public class UserRepository : IUserRepository
    {
        private readonly IMapper _mapper;

        public UserRepository(IMapper mapper)
        {
            _mapper = mapper;
        }

        private readonly List<User> _users = new() {
            new User {
                id = Guid.NewGuid(),
                FirstName = "John",
                LastName = "Doe",
                Email = "JohnDoe@gmail.com",
                Password = new byte[] { 1, 2, 3, 4, 5, 6 },
                isLibrarian = false,
                CreatedAt = DateOnly.FromDateTime(DateTime.Now),
                UpdatedAt = DateOnly.FromDateTime(DateTime.Now)
                },
            new User {
                id = Guid.NewGuid(),
                FirstName = "Jane",
                LastName = "Doe",
                Email = "JaneDoe@gmail.com",
                Password = new byte[] { 1, 2, 3, 4, 5, 6 },
                isLibrarian = true,
                CreatedAt = DateOnly.FromDateTime(DateTime.Now),
                UpdatedAt = DateOnly.FromDateTime(DateTime.Now)
            }
        };

        public List<UserDto> GetAllUsers()
        {
            var userDtos = _mapper.Map<List<UserDto>>(_users);
            return userDtos;
        }

        public UserDto CreateUser(UserDto userDto)
        {
            var user = _mapper.Map<User>(userDto);
            _users.Add(user);
            return userDto;
        }

        public UserDto GetUserById(Guid id)
        {
            var foundUser = _users.FirstOrDefault(user => user.id == id);
            if (foundUser == null)
            {
                return null;
            }
            var userDto = _mapper.Map<UserDto>(foundUser);
            return userDto;
        }
    }
}