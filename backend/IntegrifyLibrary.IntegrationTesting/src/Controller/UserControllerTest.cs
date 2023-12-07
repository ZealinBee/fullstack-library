namespace IntegrifyLibrary.IntegrationTesting.Controller;
public class UserControllerTest
{
    // Testing to see if routes work
    private readonly CustomWebApplicationFactory _factory;
    private readonly HttpClient _client;

    public UserControllerTest()
    {
        _factory = new CustomWebApplicationFactory();
        _client = _factory.CreateClient(new WebApplicationFactoryClientOptions
        {
            AllowAutoRedirect = false
        });
    }

    [Fact]
    public async Task SignUpUser_Always_Successfully()
    {
        // Sign up user should be accessible without anything


        var signUpUser = new User()
        {
            FirstName = "Zhiyuan",
            LastName = "Liu",
            Email = "test@mail.com",
            Password = "12345678"
        };

        var response = await _client.PostAsync("/api/v1/users", JsonContent.Create(signUpUser));
        var responseString = await response.Content.ReadAsStringAsync();

        Assert.Equal(HttpStatusCode.Created, response.StatusCode);
    }

    [Fact]
    public async Task SignUpUser_AlreadySignedUp_Fail()
    {
        // Sign up user should be accessible without anything
        var signUpUser = new User()
        {
            FirstName = "Zhiyuan",
            LastName = "Liu",
            Email = "test@mail.com",
            Password = "12345678"
        };

        var response = await _client.PostAsync("/api/v1/users", JsonContent.Create(signUpUser));
        var responseString = await response.Content.ReadAsStringAsync();

        Assert.Equal(HttpStatusCode.InternalServerError, response.StatusCode);
    }
}