using IntegrifyLibrary.Domain;

using AutoMapper;

namespace IntegrifyLibrary.Business
{
    public class UserService : BaseService<User, CreateUserDto, GetUserDto, UpdateUserDto>, IUserService
    {
        private readonly IMapper _mapper;
        public UserService(IUserRepo userRepo, IMapper mapper) : base(userRepo, mapper)
        {
            _mapper = mapper;
        }
        public GetUserDto CreateAdmin(CreateUserDto dto)
        {
            var user = _mapper.Map<User>(dto);
            user.Role = Role.Librarian;
            return _mapper.Map<GetUserDto>(user);
        }
    }
}
