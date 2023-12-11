namespace IntegrifyLibrary.IntegrationTesting;
[Collection("Integration Tests")]

public class AuthControllerTest : IAsyncLifetime
{
    private readonly HttpClient _client;
    private readonly Func<Task> _resetDatabase;

    public AuthControllerTest(CustomWebApplicationFactory factory)
    {
        _client = factory.HttpClient;
        _resetDatabase = factory.ResetDatabaseAsync;
    }

    [Fact]
    public async Task Login_WithCorrectCredentials_Successfully()
    {
        // Login with the seeded user
        var loginCredentials = new LoginUserDto()
        {
            Email = "adminseed@mail.com",
            Password = "admin123"
        };

        var response = await _client.PostAsync("/api/v1/auth", JsonContent.Create(loginCredentials));
        var responseString = await response.Content.ReadAsStringAsync();

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        // should contain JWT header signature
        Assert.Contains("eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9", responseString, StringComparison.OrdinalIgnoreCase);
    }

    [Fact]
    public async Task Login_WithIncorrectCredentials_Fail()
    {
        var loginCredentials = new LoginUserDto()
        {
            Email = "adminseed@mail.com",
            Password = "incorrectPassword"
        };

        var response = await _client.PostAsync("/api/v1/auth", JsonContent.Create(loginCredentials));
        var responseString = await response.Content.ReadAsStringAsync();

        Assert.Equal(HttpStatusCode.InternalServerError, response.StatusCode);
        var expectedErrorMessage = "Password is incorrect";
        Assert.Contains(expectedErrorMessage, responseString, StringComparison.OrdinalIgnoreCase);
    }

    [Fact]
    public async Task Login_WithNonExistingEmail_Fail()
    {
        var loginCredentials = new LoginUserDto()
        {
            Email = "nonexistingemail@mail.com",
            Password = "incorrectPassword"
        };

        var response = await _client.PostAsync("/api/v1/auth", JsonContent.Create(loginCredentials));
        var responseString = await response.Content.ReadAsStringAsync();

        Assert.Equal(HttpStatusCode.InternalServerError, response.StatusCode);
        var expectedErrorMessage = "Email not found";
        Assert.Contains(expectedErrorMessage, responseString, StringComparison.OrdinalIgnoreCase);
    }


    public Task InitializeAsync() => Task.CompletedTask;

    public Task DisposeAsync() => _resetDatabase();


}