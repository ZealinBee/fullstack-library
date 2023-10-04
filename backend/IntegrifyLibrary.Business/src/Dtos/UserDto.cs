using IntegrifyLibrary.Domain;

namespace IntegrifyLibrary.Business
{
    public class CreateUserDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string UserImage { get; set; } = "https://cdn.pixabay.com/photo/2015/10/05/22/37/blank-profile-picture-973460_640.png";
    }

    public class LoginUserDto
    {
        public string Email { get; set; }
        public string Password { get; set; }

    }

    public class UpdateUserDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserImage { get; set; }

    }

    public class GetUserDto
    {
        public Guid UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public Role Role { get; set; }
        public string UserImage { get; set; }
    }
}