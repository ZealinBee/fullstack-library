using IntegrifyLibraryApi.Domain;
using IntegrifyLibraryApi.Infrastructure;
using AutoMapper;
using System.Text;

namespace IntegrifyLibraryApi.Business
{
    public class UserService : IUserService
    {
        private readonly IMapper _mapper;
        private readonly IUserRepo _userRepo;
        public UserService(IMapper mapper, IUserRepo userRepo)
        {
            _mapper = mapper;
            _userRepo = userRepo;
        }

        private List<User> _users = new() {
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
            var userList = _users.ToList();
            var userDtos = _mapper.Map<List<UserDto>>(userList);
            for (int i = 0; i < _users.Count; i++)
            {
                Console.WriteLine(_users[i]);
            }
            return userDtos;
        }

        public UserDto CreateUser(UserDto userDto)
        {
            try
            {
                var user = _mapper.Map<User>(userDto);
                byte[] passwordHash = Encoding.ASCII.GetBytes(userDto.Password);
                user.Password = passwordHash;
                _userRepo.CreateUser(userDto);
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