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


    // CREATE USER ENDPOINT
    [Fact]
    public async Task SignUpUser_Always_Successfully()
    {
        // Sign up user should be accessible to everyone
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
        // this user is already seeded in the database, so it should fail
        var signUpUser = new User()
        {
            FirstName = "Zhiyuan",
            LastName = "Liu",
            Email = "adminseed@mail.com",
            Password = "12345678"
        };

        var response = await _client.PostAsync("/api/v1/users", JsonContent.Create(signUpUser));
        var responseString = await response.Content.ReadAsStringAsync();

        Assert.Equal(HttpStatusCode.InternalServerError, response.StatusCode);
    }


    // GET USER ENDPOINT

    [Fact]
    public async Task GetUsers_WithAdminToken_Successfully()
    {
        // Get user should be accessible when logged in as librarian
        var loginCredentials = new LoginUserDto()
        {
            Email = "adminseed@mail.com",
            Password = "admin123"
        };
        var loginResponse = await _client.PostAsync("/api/v1/auth", JsonContent.Create(loginCredentials));
        var token = await loginResponse.Content.ReadAsStringAsync();
        _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        var response = await _client.GetAsync("/api/v1/users");
        var responseString = await response.Content.ReadAsStringAsync();

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        Assert.Contains("userseed@mail.com", responseString, StringComparison.OrdinalIgnoreCase);
        // After each login, clear the authorization header
        _client.DefaultRequestHeaders.Authorization = null;
    }

    [Fact]
    public async Task GetUsers_WithoutToken_Unauthorized()
    {
        // Get user should be unauthorized without token
        var response = await _client.GetAsync("/api/v1/users");
        var responseString = await response.Content.ReadAsStringAsync();

        Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
    }

    [Fact]
    public async Task GetUsers_WithUserToken_Forbidden()
    {
        // Get user should be forbidden when logged in as user
        var loginCredentials = new LoginUserDto()
        {
            Email = "userseed@mail.com",
            Password = "user123"
        };
        var loginResponse = await _client.PostAsync("/api/v1/auth", JsonContent.Create(loginCredentials));
        var token = await loginResponse.Content.ReadAsStringAsync();
        _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        var response = await _client.GetAsync("/api/v1/users");
        var responseString = await response.Content.ReadAsStringAsync();

        Assert.Equal(HttpStatusCode.Forbidden, response.StatusCode);
        // After each login, clear the authorization header
        _client.DefaultRequestHeaders.Authorization = null;
    }
    // UPDATE SELF ENDPOINT
    [Fact]
    public async Task UpdateSelf_WithUserToken_Successfully()
    {
        // Update self should be accessible when logged in as user or librarian
        var loginCredentials = new LoginUserDto()
        {
            Email = "userseed@mail.com",
            Password = "user123"
        };
        var loginResponse = await _client.PostAsync("/api/v1/auth", JsonContent.Create(loginCredentials));
        var token = await loginResponse.Content.ReadAsStringAsync();
        _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

        var updateUser = new UpdateUserDto()
        {
            FirstName = "Zhiyuan",
            LastName = "Liuuuuuuuuuuuu",
            UserImage = "imagelink"
        };
        var response = await _client.PatchAsync("/api/v1/users/profile", JsonContent.Create(updateUser));
        var responseString = await response.Content.ReadAsStringAsync();

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        Assert.Contains("Liuuuuuuuuuuuu", responseString, StringComparison.OrdinalIgnoreCase);
        // After each login, clear the authorization header
        _client.DefaultRequestHeaders.Authorization = null;
    }

    // DELETE SELF ENDPOINT
    [Fact]
    public async Task DeleteSelf_WithUserToken_Successfully()
    {
        // Update self should be accessible when logged in as user or librarian
        var loginCredentials = new LoginUserDto()
        {
            Email = "userseed@mail.com",
            Password = "user123"
        };
        var loginResponse = await _client.PostAsync("/api/v1/auth", JsonContent.Create(loginCredentials));
        var token = await loginResponse.Content.ReadAsStringAsync();
        _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

        var response = await _client.DeleteAsync("/api/v1/users/profile");
        var responseString = await response.Content.ReadAsStringAsync();

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        // After each login, clear the authorization header
        _client.DefaultRequestHeaders.Authorization = null;
    }


    public Task InitializeAsync() => Task.CompletedTask;

    public Task DisposeAsync() => _resetDatabase();


}