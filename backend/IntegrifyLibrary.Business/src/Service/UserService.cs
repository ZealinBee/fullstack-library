using AutoMapper;
using IntegrifyLibrary.Domain;

namespace IntegrifyLibrary.Business
{
    public class UserService : IUserService
    {
        private readonly IUserRepo _userRepo;
        private readonly IMapper _mapper;

        public UserService(IUserRepo userRepo, IMapper mapper)
        {
            _userRepo = userRepo;
            _mapper = mapper;
        }

        public async Task<ReadUserDto> GetUserByIdAsync(Guid id)
        {
            var userEntity = await _userRepo.GetUserByIdAsync(id);
            return _mapper.Map<ReadUserDto>(userEntity);
        }

        public async Task<UserDto> CreateUserAsync(CreateUserDto user)
        {
            await _userRepo.CreateUserAsync(user);
            var userEntity = _mapper.Map<UserDto>(user);
            return userEntity;
        }

        public async Task<List<UserDto>> GetAllUsersAsync()
        {
            var userEntities = await _userRepo.GetAllUsersAsync();
            return _mapper.Map<List<UserDto>>(userEntities);
        }

        // public async Task<UpdateUserDto> UpdateUserAsync(UpdateUserDto user)
        // {
        //     var userEntity = await _userRepo.GetUserByIdAsync(user.UserId);
        //     if (userEntity == null)
        //     {
        //         throw new NotFoundException($"User with ID {user.UserId} not found.");
        //     }

        //     _mapper.Map(user, userEntity);
        //     userEntity = await _userRepo.UpdateUserAsync(userEntity);
        //     return _mapper.Map<UpdateUserDto>(userEntity);
        // }
    }
}
