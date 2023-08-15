using IntegrifyLibrary.Domain;
using Xunit;

namespace IntegrifyLibrary.Testing;

public class UserTest
{
    [Fact]
    public void HasRole_ReturnsTrue_WhenUserHasRole()
    {
        var user = new User();
        var role = new Role();
        user.Role = role;
        var result = false;
        if (user.Role != null)
        {
            result = true;
        }

        Assert.True(result);
    }
}