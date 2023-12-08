namespace IntegrifyLibrary.IntegrationTesting.Controller;
public class UserControllerTest : IClassFixture<CustomWebApplicationFactory<Program>>
{
    // Testing to see if routes work
    private readonly CustomWebApplicationFactory<Program> _factory;
    public UserControllerTest(CustomWebApplicationFactory<Program> factory)
    {
        _factory = factory;
    }

    [Fact]
    public async Task SignUpUser_Always_Successfully()
    {
        // Sign up user should be accessible without anything
        var client = _factory.CreateClient(new WebApplicationFactoryClientOptions
        {
            AllowAutoRedirect = false
        });

        var signUpUser = new User()
        {
            FirstName = "Zhiyuan",
            LastName = "Liu",
            Email = "test@mail.com",
            Password = "12345678"
        };

        var response = await client.PostAsync("/api/v1/users", JsonContent.Create(signUpUser));
        var responseString = await response.Content.ReadAsStringAsync();

        Assert.Equal(HttpStatusCode.Created, response.StatusCode);
    }

    [Fact]
    public async Task SignUpUser_AlreadySignedUp_Fail()
    {
        var client = _factory.CreateClient(new WebApplicationFactoryClientOptions
        {
            AllowAutoRedirect = false
        });
        // Sign up user should be accessible without anything
        var signUpUser = new User()
        {
            FirstName = "Zhiyuan",
            LastName = "Liu",
            Email = "test@mail.com",
            Password = "12345678"
        };

        var response = await client.PostAsync("/api/v1/users", JsonContent.Create(signUpUser));
        var responseString = await response.Content.ReadAsStringAsync();

        Assert.Equal(HttpStatusCode.InternalServerError, response.StatusCode);

    }
}