namespace IntegrifyLibrary.IntegrationTesting;
[Collection("Integration Tests")]

public class UserControllerTest : IAsyncLifetime
{
    // Testing to see if routes work
    private readonly HttpClient _client;
    private readonly Func<Task> _resetDatabase;
    public UserControllerTest(CustomWebApplicationFactory factory)
    {
        _client = factory.HttpClient;
        _resetDatabase = factory.ResetDatabaseAsync;
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
    public async Task SignUpUser_WithExistingEmail_Fail()
    {
        // The admin@mail.com is already seeded in db, so creating it again should fail

        var signUpUser = new User()
        {
            FirstName = "Zhiyuan",
            LastName = "Liu",
            Email = "admin@mail.com",
            Password = "12345678"
        };
        var response = await _client.PostAsync("/api/v1/users", JsonContent.Create(signUpUser));
        var responseString = await response.Content.ReadAsStringAsync();
        Assert.Equal(HttpStatusCode.InternalServerError, response.StatusCode);
    }

    [Fact]
    public async Task GetUser_WithoutToken_Unauthorized()
    {
        // Get user should be unauthorized without token
        var response = await _client.GetAsync("/api/v1/users");
        var responseString = await response.Content.ReadAsStringAsync();

        Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
    }


    public Task InitializeAsync() => Task.CompletedTask;

    public Task DisposeAsync() => _resetDatabase();


}