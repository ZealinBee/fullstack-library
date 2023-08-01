using IntegrifyLibrary.Services.Abstractions;
using IntegrifyLibrary.Entities;
using IntegrifyLibrary.Dto;
using AutoMapper;
using System.Text;

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
                UserId = Guid.NewGuid(),
                FirstName = "John",
                LastName = "Doe",
                Email = "JohnDoe@gmail.com",
                Password = new byte[] { 1, 2, 3, 4, 5, 6 },
                IsLibrarian = false,
                CreatedAt = DateOnly.FromDateTime(DateTime.Now),
                UpdatedAt = DateOnly.FromDateTime(DateTime.Now)
                },
            new User {
                UserId = Guid.NewGuid(),
                FirstName = "Jane",
                LastName = "Doe",
                Email = "JaneDoe@gmail.com",
                Password = new byte[] { 1, 2, 3, 4, 5, 6 },
                IsLibrarian = true,
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
            try
            {
                byte[] passwordHash = Encoding.ASCII.GetBytes(userDto.Password);
                var user = _mapper.Map<User>(userDto);
                user.Password = passwordHash;
                _users.Add(user);
                return userDto;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public UserDto GetUserById(Guid id)
        {
            var foundUser = _users.FirstOrDefault(user => user.UserId == id);
            if (foundUser == null)
            {
                throw new Exception($"User with id {id} not found");
            }
            var userDto = _mapper.Map<UserDto>(foundUser);
            return userDto;
        }
    }
}