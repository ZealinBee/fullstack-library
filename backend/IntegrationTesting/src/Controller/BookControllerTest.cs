namespace IntegrifyLibrary.IntegrationTesting.Controller;
public class BookControllerTest
{
    [Fact]
    public async Task GetBooks_GetWithoutLogin_Successfully()
    {
        // Book get endpoint should be accessible without login
        var factory = new CustomWebApplicationFactory();
        var client = factory.CreateClient();

        var response = await client.GetAsync("/api/v1/books");
        var responseString = await response.Content.ReadAsStringAsync();

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);

    }




}

