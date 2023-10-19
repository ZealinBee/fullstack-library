using IntegrifyLibrary.Domain;
using Xunit;
using System.Security.Cryptography;
using System.Text;


namespace IntegrifyLibrary.Testing.Domain
{
    public class UserTests
{
    [Fact]
    public void CreateUser_SingleValidData_NewUser()
    {
        var userId = Guid.NewGuid();
        var createdAt = new DateOnly(2023, 10, 19);
        var updatedAt = new DateOnly(2023, 10, 20);

        var user = new User
        {
            UserId = userId,
            FirstName = "John",
            LastName = "Doe",
            Email = "johndoe@example.com",
            Password = "password123",
            Salt = Encoding.UTF8.GetBytes("somesalt"),
            Role = Role.User,
            CreatedAt = createdAt,
            UpdatedAt = updatedAt
        };

        Assert.Equal(userId, user.UserId);
        Assert.Equal("John", user.FirstName);
        Assert.Equal("Doe", user.LastName);
        Assert.Equal("johndoe@example.com", user.Email);
        Assert.Equal("password123", user.Password);
        Assert.Equal(Role.User, user.Role);
        Assert.Equal(createdAt, user.CreatedAt);
        Assert.Equal(updatedAt, user.UpdatedAt);
        Assert.NotNull(user.Loans); 
    }

    [Fact]
    public void CreateUser_CheckImage_NewUser()
    {
        var user = new User();
        // Just to check if the user image is initalized correctly to an image placeholder
        Assert.Equal("https://cdn.pixabay.com/photo/2015/10/05/22/37/blank-profile-picture-973460_640.png", user.UserImage);
    }

}

}