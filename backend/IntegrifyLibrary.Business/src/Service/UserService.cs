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
        public CreateUserDto CreateAdmin(CreateUserDto dto)
        {
            var user = _mapper.Map<User>(dto);
            PasswordService.HashPassword(dto.Password, out var hashedPassword, out var salt);
            user.Password = hashedPassword;
            user.Salt = salt;
            user.Role = Role.Librarian;
            return _mapper.Map<CreateUserDto>(_userRepo.CreateOne(user));
        }

        public override CreateUserDto CreateOne(CreateUserDto dto)
        {
            var user = _mapper.Map<User>(dto);
            PasswordService.HashPassword(dto.Password, out var hashedPassword, out var salt);
            user.Password = hashedPassword;
            user.Salt = salt;
            user.Role = Role.User;
            return _mapper.Map<CreateUserDto>(_userRepo.CreateOne(user));
        }
    }
}
