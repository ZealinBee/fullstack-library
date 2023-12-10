namespace IntegrifyLibrary.IntegrationTesting;
[Collection("Integration Tests")]

public class BookControllerTest : IAsyncLifetime
{
    private readonly HttpClient _client;
    private readonly Func<Task> _resetDatabase;

    public BookControllerTest(CustomWebApplicationFactory factory)
    {
        _client = factory.HttpClient;
        _resetDatabase = factory.ResetDatabaseAsync;
    }

    // CREATE BOOK ENDPOINT
    [Fact]
    public async Task PostBook_AsAdmin_Success()
    {
        // Post book should be accessible when logged in as librarian
        var loginCredentials = new LoginUserDto()
        {
            Email = "adminseed@mail.com",
            Password = "admin123"
        };
        var loginResponse = await _client.PostAsync("/api/v1/auth", JsonContent.Create(loginCredentials));
        var token = await loginResponse.Content.ReadAsStringAsync();
        _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        var newBook = new Book()
        {
            BookName = "Harry Potter and the Prisoner of Azkaban",
            AuthorName = "J.K Rowling",
            Description = "A sample book description",
            ISBN = "1234567890",
            Quantity = 10,
            PageCount = 200,
            PublishedDate = new DateOnly(2023, 8, 16),
            GenreName = "Fantasy",
            LoanedTimes = 0
        };

        var response = await _client.PostAsync("/api/v1/books", JsonContent.Create(newBook));
        var responseString = await response.Content.ReadAsStringAsync();

        Assert.Equal(HttpStatusCode.Created, response.StatusCode);
        Assert.Contains("Harry Potter and the Prisoner of Azkaban", responseString);
        // After each login, clear the authorization header
        _client.DefaultRequestHeaders.Authorization = null;
    }
    [Fact]
    public async Task PostBook_PostWithoutLogin_Fail()
    {
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
        var response = await _client.PostAsync("/api/v1/books", JsonContent.Create(newBook));
        var responseString = await response.Content.ReadAsStringAsync();

        Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
    }

    // GET BOOK ENDPOINT
    [Fact]
    public async Task GetBooks_GetWithoutLogin_Successfully()
    {
        // Book get endpoint should be accessible without login
        var response = await _client.GetAsync("/api/v1/books");
        var responseString = await response.Content.ReadAsStringAsync();

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        // Harry Potter and the Philosopher's Stone is seeded in the database
        Assert.Contains("Harry Potter and the Philosopher's Stone", responseString);
    }

    // GET BOOK BY ID ENDPOINT
    [Fact]
    public async Task GetBookById_GetWithoutLogin_Successfully()
    {
        // Book get endpoint should be accessible without login
        var response = await _client.GetAsync("/api/v1/books/b7aef6dc-534f-4a7d-96c8-98f2b72b53bd");
        var responseString = await response.Content.ReadAsStringAsync();

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        // Harry Potter and the Philosopher's Stone is seeded in the database
        Assert.Contains("Harry Potter and the Philosopher's Stone", responseString);
    }

    // UPDATE BOOK ENDPOINT
    [Fact]
    public async Task UpdateBook_UpdateWithoutLogin_Fail()
    {
        // Book update endpoint should not be accessible without login
        var updatedBook = new Book()
        {
            BookId = Guid.Parse("b7aef6dc-534f-4a7d-96c8-98f2b72b53bd"),
            BookName = "Harry Potter and the Philosopher's Stoneeeeeeeeee",
            AuthorName = "J.K Rowling",
            Description = "A sample book description",
            ISBN = "1234567890",
            Quantity = 10,
            PageCount = 200,
            PublishedDate = new DateOnly(2023, 8, 16),
            GenreName = "Fantasy",
            LoanedTimes = 0
        };
        var response = await _client.PatchAsync("/api/v1/books/b7aef6dc-534f-4a7d-96c8-98f2b72b53bd", JsonContent.Create(updatedBook));
        var responseString = await response.Content.ReadAsStringAsync();

        Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
    }

    [Fact]
    public async Task UpdateBook_UpdateWithLibrarian_Success()
    {
        var loginCredentials = new LoginUserDto()
        {
            Email = "adminseed@mail.com",
            Password = "admin123"
        };
        var loginResponse = await _client.PostAsync("/api/v1/auth", JsonContent.Create(loginCredentials));
        var token = await loginResponse.Content.ReadAsStringAsync();
        _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        var updatedBook = new Book()
        {
            BookId = Guid.Parse("b7aef6dc-534f-4a7d-96c8-98f2b72b53bd"),
            BookName = "Harry Potter and the Philosopher's Stoneeeeeeeeee",
            AuthorName = "J.K Rowling",
            Description = "A sample book description",
            ISBN = "1234567890",
            Quantity = 10,
            PageCount = 200,
            PublishedDate = new DateOnly(2023, 8, 16),
            GenreName = "Fantasy",
            LoanedTimes = 0
        };
        var response = await _client.PatchAsync("/api/v1/books/b7aef6dc-534f-4a7d-96c8-98f2b72b53bd", JsonContent.Create(updatedBook));
        var responseString = await response.Content.ReadAsStringAsync();

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        Assert.Contains("Harry Potter and the Philosopher's Stoneeeeeeeeee", responseString);
        // After each login, clear the authorization header
        _client.DefaultRequestHeaders.Authorization = null;
    }

    [Fact]
    public async Task UpdateBook_UpdateWithUser_Forbidden()
    {
        var loginCredentials = new LoginUserDto()
        {
            Email = "userseed@mail.com",
            Password = "user123"
        };
        var loginResponse = await _client.PostAsync("/api/v1/auth", JsonContent.Create(loginCredentials));
        var token = await loginResponse.Content.ReadAsStringAsync();
        _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

        var updatedBook = new Book()
        {
            BookId = Guid.Parse("b7aef6dc-534f-4a7d-96c8-98f2b72b53bd"),
            BookName = "Harry Potter and the Philosopher's Stoneeeeeeeeee",
            AuthorName = "J.K Rowling",
            Description = "A sample book description",
            ISBN = "1234567890",
            Quantity = 10,
            PageCount = 200,
            PublishedDate = new DateOnly(2023, 8, 16),
            GenreName = "Fantasy",
            LoanedTimes = 0
        };
        var response = await _client.PatchAsync("/api/v1/books/b7aef6dc-534f-4a7d-96c8-98f2b72b53bd", JsonContent.Create(updatedBook));

        Assert.Equal(HttpStatusCode.Forbidden, response.StatusCode);
        // After each login, clear the authorization header
        _client.DefaultRequestHeaders.Authorization = null;
    }

    public Task InitializeAsync() => Task.CompletedTask;

    public Task DisposeAsync() => _resetDatabase();



}

