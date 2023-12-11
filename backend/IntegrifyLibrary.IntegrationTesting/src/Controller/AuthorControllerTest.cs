namespace IntegrifyLibrary.IntegrationTesting;
[Collection("Integration Tests")]

public class AuthorControllerTest : IAsyncLifetime
{
    // The genre and author controller/service are identical, so I will only test the author
    private readonly HttpClient _client;
    private readonly Func<Task> _resetDatabase;

    public AuthorControllerTest(CustomWebApplicationFactory factory)
    {
        _client = factory.HttpClient;
        _resetDatabase = factory.ResetDatabaseAsync;
    }

    // GET AUTHOR ENDPOINT
    [Fact]
    public async Task GetAuthorAndTheirBooks_WithoutLogin_Successfully()
    {
        // Author get endpoint should be accessible without login
        var response = await _client.GetAsync("/api/v1/authors");
        var responseString = await response.Content.ReadAsStringAsync();

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        // J.K Rowling and these books are already seeded in the database, the get author end point returns the author and the books
        Assert.Contains("J.K Rowling", responseString);
        Assert.Contains("Harry Potter and the Philosopher's Stone", responseString);
        Assert.Contains("Harry Potter and the Chamber of Secrets", responseString);
        Assert.Contains("Harry Potter and the Deathly Hallows", responseString);
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
    public async Task UpdateAuthor_UpdateWithUser_Forbidden()
    {
        var loginCredentials = new LoginUserDto
        {
            Email = "userseed@mail.com",
            Password = "user123"
        };
        var loginResponse = await _client.PostAsJsonAsync("/api/v1/auth", loginCredentials);
        var token = await loginResponse.Content.ReadAsStringAsync();
        _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        var updateRequest = new UpdateAuthorDto
        {
            AuthorName = "J.K Rowling",
            AuthorImage = "newlinkkkkkkkk"
        };

        // Author update endpoint should NOT be accessible with user login
        var response = await _client.PatchAsync("/api/v1/authors/be924f89-17bd-445d-a341-335d9ef0938f", JsonContent.Create(updateRequest));
        var responseString = await response.Content.ReadAsStringAsync();

        Assert.Equal(HttpStatusCode.Forbidden, response.StatusCode);
        // After login, clear the authorization header
        _client.DefaultRequestHeaders.Authorization = null;
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

    // DELETE AUTHOR ENDPOINT
    [Fact]
    public async Task DeleteAuthor_AlsoDeletesAllTheirBooks_Successfully()
    {
        var loginCredentials = new LoginUserDto
        {
            Email = "adminseed@mail.com",
            Password = "admin123"
        };
        var loginResponse = await _client.PostAsJsonAsync("/api/v1/auth", loginCredentials);
        var token = await loginResponse.Content.ReadAsStringAsync();
        _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

        var response = await _client.DeleteAsync("/api/v1/authors/be924f89-17bd-445d-a341-335d9ef0938f");
        var responseString = await response.Content.ReadAsStringAsync();

        // Deleting an author also deletes all their books
        Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);
        Assert.DoesNotContain("be924f89-17bd-445d-a341-335d9ef0938f", responseString);
        Assert.DoesNotContain("Harry Potter and the Philosopher's Stone", responseString);
        Assert.DoesNotContain("Harry Potter and the Chamber of Secrets", responseString);
        Assert.DoesNotContain("Harry Potter and the Deathly Hallows", responseString);
        // After login, clear the authorization header
        _client.DefaultRequestHeaders.Authorization = null;
    }

    [Fact]
    public async Task DeleteAuthor_WithUserLogin_Forbidden()
    {
        var loginCredentials = new LoginUserDto
        {
            Email = "userseed@mail.com",
            Password = "user123"
        };
        var loginResponse = await _client.PostAsJsonAsync("/api/v1/auth", loginCredentials);
        var token = await loginResponse.Content.ReadAsStringAsync();
        _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

        var response = await _client.DeleteAsync("/api/v1/authors/be924f89-17bd-445d-a341-335d9ef0938f");
        var responseString = await response.Content.ReadAsStringAsync();

        Assert.Equal(HttpStatusCode.Forbidden, response.StatusCode);
        // After login, clear the authorization header
        _client.DefaultRequestHeaders.Authorization = null;
    }

    public Task InitializeAsync() => Task.CompletedTask;

    public Task DisposeAsync() => _resetDatabase();
}