using AutoMapper;
using IntegrifyLibrary.Domain;

namespace IntegrifyLibrary.Business
{
    public class UserService : BaseService<User, CreateUserDto, GetUserDto, UpdateUserDto>, IUserService
    {
        public UserService(IUserRepo userRepo, IMapper mapper) : base(userRepo, mapper)
        {
        }
    }
}
