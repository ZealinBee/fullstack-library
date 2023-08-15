namespace IntegrifyLibrary.Business
{
    public class CreateUserDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public byte[] Password { get; set; }
    }

    public class LoginUserDto
    {
        public string Email { get; set; }
        public string Password { get; set; }

    }

    public class UserDto
    {
        public Guid UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public bool IsLibrarian { get; set; }
        public DateOnly CreatedAt { get; init; }
        public DateOnly UpdatedAt { get; init; }
    }


    public class UpdateUserDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public byte[] NewPassword { get; set; }

    }

    public class ReadUserDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public bool IsLibrarian { get; set; }

    }
}