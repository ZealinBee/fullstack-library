namespace IntegrifyLibrary.IntegrationTesting;
public class AuthorControllerTest : IClassFixture<CustomWebApplicationFactory<Program>>
{
    private readonly CustomWebApplicationFactory<Program> _factory;
    public AuthorControllerTest(CustomWebApplicationFactory<Program> factory)
    {
        _factory = factory;
    }
    [Fact]
    public async Task GetAuthor_GetWithoutLogin_Successfully()
    {
        var client = _factory.CreateClient(new WebApplicationFactoryClientOptions
        {
            AllowAutoRedirect = false
        });
        // Author get endpoint should be accessible without login
        var response = await client.GetAsync("/api/v1/authors");
        var responseString = await response.Content.ReadAsStringAsync();

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }
}