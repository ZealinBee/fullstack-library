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

    public Task InitializeAsync() => Task.CompletedTask;

    public Task DisposeAsync() => _resetDatabase();
}