namespace IntegrifyLibrary.IntegrationTesting;
[Collection("Integration Tests")]

public class AuthorControllerTest : IAsyncLifetime
{
    private readonly HttpClient _client;
    private readonly Func<Task> _resetDatabase;

    public AuthorControllerTest(CustomWebApplicationFactory factory)
    {
        _client = factory.HttpClient;
        _resetDatabase = factory.ResetDatabaseAsync;
    }

    // GET AUTHOR ENDPOINT
    [Fact]
    public async Task GetAuthor_GetWithoutLogin_Successfully()
    {
        // Author get endpoint should be accessible without login
        var response = await _client.GetAsync("/api/v1/authors");
        var responseString = await response.Content.ReadAsStringAsync();

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        // J.K Rowling is seeded in the database
        Assert.Contains("J.K Rowling", responseString);
    }

    // UPDATE AUTHOR ENDPOINT
    [Fact]
    public async Task UpdateAuthor_UpdateWithoutLogin_Unauthorized()
    {
        // Author update endpoint should be inaccessible without login
        var response = await _client.PatchAsync("/api/v1/authors/be924f89-17bd-445d-a341-335d9ef0938f", new StringContent(""));
        var responseString = await response.Content.ReadAsStringAsync();

        Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
    }

    [Fact]
    public async Task UpdateAuthor_UpdateWithLibrarian_Successfully()
    {
        // in order to update an author, you need to be logged in as a librarian, so we need to login first
        var loginCredentials = new LoginUserDto
        {
            Email = "adminseed@mail.com",
            Password = "admin123"
        };
        var loginResponse = await _client.PostAsJsonAsync("/api/v1/auth", loginCredentials);
        var token = await loginResponse.Content.ReadAsStringAsync();
        _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        var updateRequest = new UpdateAuthorDto
        {
            AuthorName = "J.K Rowling",
            AuthorImage = "newlinkkkkkkkk"
        };

        // Author update endpoint should be accessible with librarian login
        var response = await _client.PatchAsync("/api/v1/authors/be924f89-17bd-445d-a341-335d9ef0938f", JsonContent.Create(updateRequest));
        var responseString = await response.Content.ReadAsStringAsync();

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        Assert.Contains("newlinkkkkkkkk", responseString);
        // After login, clear the authorization header
        _client.DefaultRequestHeaders.Authorization = null;

    }



    public Task InitializeAsync() => Task.CompletedTask;

    public Task DisposeAsync() => _resetDatabase();
}