namespace IntegrifyLibrary.IntegrationTesting;
public class BookControllerTest
{
    // Testing to see if routes work
    private readonly CustomWebApplicationFactory<Program> _factory;

    public BookControllerTest(CustomWebApplicationFactory<Program> factory)
    {
        _factory = factory;
    }

    [Fact]
    public async Task PostBook_PostWithoutLogin_Fail()
    {
        var client = _factory.CreateClient(new WebApplicationFactoryClientOptions
        {
            AllowAutoRedirect = false
        });
        // Book post endpoint should not be accessible without login
        var newBook = new Book()
        {
            BookName = "Harry Potter and the Philosopher's Stone",
            AuthorName = "J.K Rowling",
            Description = "A sample book description",
            ISBN = "1234567890",
            Quantity = 10,
            PageCount = 200,
            PublishedDate = new DateOnly(2023, 8, 16),
            GenreName = "Fantasy",
            LoanedTimes = 0
        };
        var response = await client.PostAsync("/api/v1/books", JsonContent.Create(newBook));
        var responseString = await response.Content.ReadAsStringAsync();

        Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
    }

    [Fact]
    public async Task GetBooks_GetWithoutLogin_Successfully()
    {
        var client = _factory.CreateClient(new WebApplicationFactoryClientOptions
        {
            AllowAutoRedirect = false
        });
        // Book get endpoint should be accessible without login
        var response = await client.GetAsync("/api/v1/books");
        var responseString = await response.Content.ReadAsStringAsync();

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }



}

