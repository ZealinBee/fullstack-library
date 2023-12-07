namespace IntegrifyLibrary.IntegrationTesting;

public class AuthorControllerTest
{
    private readonly CustomWebApplicationFactory _factory;
    private readonly HttpClient _client;

    public AuthorControllerTest(CustomWebApplicationFactory<Program> factory)
    {
        _factory = new CustomWebApplicationFactory();
        _client = _factory.CreateClient(new WebApplicationFactoryClientOptions
        {
            AllowAutoRedirect = false
        });
    }
    [Fact]
    public async Task GetAuthor_GetWithoutLogin_Successfully()
    {
        // Author get endpoint should be accessible without login
        var response = await _client.GetAsync("/api/v1/authors");
        var responseString = await response.Content.ReadAsStringAsync();

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }
}