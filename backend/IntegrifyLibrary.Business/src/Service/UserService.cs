using IntegrifyLibrary.Domain;

using AutoMapper;

namespace IntegrifyLibrary.Business
{
    public class UserService : BaseService<User, CreateUserDto, GetUserDto, UpdateUserDto>, IUserService
    {
        private readonly IMapper _mapper;
        private readonly IUserRepo _userRepo;
        public UserService(IUserRepo userRepo, IMapper mapper) : base(userRepo, mapper)
        {
            _mapper = mapper;
            _userRepo = userRepo;
        }
        public async Task<GetUserDto> CreateAdmin(CreateUserDto dto)
        {
            var user = _mapper.Map<User>(dto);
            if (await _userRepo.GetOneByEmail(dto.Email) != null) throw new Exception($"User with email {dto.Email} already exists");
            PasswordService.HashPassword(dto.Password, out var hashedPassword, out var salt);
            user.Password = hashedPassword;
            user.Salt = salt;
            user.Role = Role.Librarian;
            return _mapper.Map<GetUserDto>(await _userRepo.CreateOne(user));
        }

        public override async Task<GetUserDto> CreateOne(CreateUserDto dto)
        {
            var user = _mapper.Map<User>(dto);
            if (await _userRepo.GetOneByEmail(dto.Email) != null) throw new CustomException().BadRequestException($"User with email {dto.Email} already exists");
            PasswordService.HashPassword(dto.Password, out var hashedPassword, out var salt);
            user.Password = hashedPassword;
            user.Salt = salt;
            user.Role = Role.User;
            return _mapper.Map<GetUserDto>(await _userRepo.CreateOne(user));
        }

        public async Task<string> MakeUserLibrarian(string userEmail)
        {
            var user = await _userRepo.GetOneByEmail(userEmail);
            if (user == null) throw new Exception($"User with email {userEmail} does not exist");
            user.Role = Role.Librarian;
            await _userRepo.UpdateOne(user);
            return user.Email;
        }

        public async Task<GetUserDto> UpdateOwnProfile(Guid id, UpdateUserDto dto)
        {
            var user = await _userRepo.GetOne(id);
            var updatedUser = _mapper.Map(dto, user);
            return _mapper.Map<GetUserDto>(await _userRepo.UpdateOne(updatedUser));
        }
    }
}
