using System.Text.Json;

namespace IntegrifyLibrary.IntegrationTesting;
[Collection("Integration Tests")]

public class LoanControllerTest : IAsyncLifetime
{
    private readonly HttpClient _client;
    private readonly Func<Task> _resetDatabase;

    public LoanControllerTest(CustomWebApplicationFactory factory)
    {
        _client = factory.HttpClient;
        _resetDatabase = factory.ResetDatabaseAsync;
    }

    // GET LOANS ENDPOINT
    [Fact]
    public async Task PostLoan_ValidBookId_Successfully()
    {
        // in order to loan a book, you need to be logged in as an user, so we need to login first
        var loginCredentials = new LoginUserDto
        {
            Email = "userseed@mail.com",
            Password = "user123"
        };
        var loginResponse = await _client.PostAsJsonAsync("/api/v1/auth", loginCredentials);
        var token = await loginResponse.Content.ReadAsStringAsync();
        // now we can loan a book
        var loanRequest = new CreateLoanDto
        {
            LoanDate = new DateOnly(2021, 10, 10),
            // Harry Potter and the Chamber of Secrets is seeded in the database
            BookIds = new List<Guid> { Guid.Parse("010fe0a2-4b53-45bb-92ab-89ce03e730b1") }
        };

        _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        var response = await _client.PostAsJsonAsync("/api/v1/loans", loanRequest);
        var responseString = await response.Content.ReadAsStringAsync();
        Assert.Equal(HttpStatusCode.Created, response.StatusCode);
        Assert.Contains("Harry Potter and the Chamber of Secrets", responseString);
        // After login, clear the authorization header
        _client.DefaultRequestHeaders.Authorization = null;
    }

    [Fact]
    public async Task PostLoan_InvalidBookId_Fail()
    {
        // in order to loan a book, you need to be logged in as an user, so we need to login first
        var loginCredentials = new LoginUserDto
        {
            Email = "userseed@mail.com",
            Password = "user123"
        };
        var loginResponse = await _client.PostAsJsonAsync("/api/v1/auth", loginCredentials);
        var token = await loginResponse.Content.ReadAsStringAsync();
        // now we can loan a book
        var loanRequest = new CreateLoanDto
        {
            LoanDate = new DateOnly(2021, 10, 10),
            // this book id is invalid
            BookIds = new List<Guid> { Guid.Parse("b7aef6dc-0000-0000-0000-98f2b72b53bd") }
        };

        _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        var response = await _client.PostAsJsonAsync("/api/v1/loans", loanRequest);
        var responseString = await response.Content.ReadAsStringAsync();
        Assert.Equal(HttpStatusCode.InternalServerError, response.StatusCode);
        Assert.Contains("Id is not found", responseString);
        // After login, clear the authorization header
        _client.DefaultRequestHeaders.Authorization = null;
    }

    [Fact]
    public async Task PostLoan_AsLibrarian_Fail()
    {
        // in order to loan a book, you need to be logged in as an user, librarians shouldn't be able to loan books
        var loginCredentials = new LoginUserDto
        {
            Email = "adminseed@mail.com",
            Password = "admin123"
        };
        var loginResponse = await _client.PostAsJsonAsync("/api/v1/auth", loginCredentials);
        var token = await loginResponse.Content.ReadAsStringAsync();

        var loanRequest = new CreateLoanDto
        {
            LoanDate = new DateOnly(2021, 10, 10),
            // Harry Potter and the Chamber of Secrets is seeded in the database
            BookIds = new List<Guid> { Guid.Parse("010fe0a2-4b53-45bb-92ab-89ce03e730b1") }
        };

        _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        var response = await _client.PostAsJsonAsync("/api/v1/loans", loanRequest);
        var responseString = await response.Content.ReadAsStringAsync();

        Assert.Equal(HttpStatusCode.Forbidden, response.StatusCode);
        // After eachlogin, clear the authorization header
        _client.DefaultRequestHeaders.Authorization = null;
    }

    [Fact]
    public async Task PostLoan_QuantityZero_Fail()
    {
        // in order to loan a book, you need to be logged in as an user, so we need to login first
        var loginCredentials = new LoginUserDto
        {
            Email = "userseed@mail.com",
            Password = "user123"
        };
        var loginResponse = await _client.PostAsJsonAsync("/api/v1/auth", loginCredentials);
        var token = await loginResponse.Content.ReadAsStringAsync();
        // now we can loan a book
        var loanRequest = new CreateLoanDto
        {
            LoanDate = new DateOnly(2021, 10, 10),
            // this book id is invalid
            BookIds = new List<Guid> { Guid.Parse("60e1017d-545d-47eb-8e39-9e8dc57b3e08") }
        };
        _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

        var response = await _client.PostAsJsonAsync("/api/v1/loans", loanRequest);
        var responseString = await response.Content.ReadAsStringAsync();

        Assert.Equal(HttpStatusCode.InternalServerError, response.StatusCode);
        Assert.Contains("Book is not available", responseString);
        // After login, clear the authorization header
        _client.DefaultRequestHeaders.Authorization = null;
    }

    // GET OWN LOANS ENDPOINT
    [Fact]
    public async Task GetOwnLoans_GetWithLogin_Successfully()
    {
        // in order to get own loans, you need to be logged in as an user, so we need to login first
        var loginCredentials = new LoginUserDto
        {
            Email = "userseed@mail.com",
            Password = "user123"
        };
        var loginResponse = await _client.PostAsJsonAsync("/api/v1/auth", loginCredentials);
        var token = await loginResponse.Content.ReadAsStringAsync();

        _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        var response = await _client.GetAsync("/api/v1/loans/own-loans");
        var responseString = await response.Content.ReadAsStringAsync();

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        // This id is seeded in the database as loan of userseed@mail
        Assert.Contains("72b79569-4365-40e7-b448-fa85272d38a6", responseString);
        // After each login, clear the authorization header
        _client.DefaultRequestHeaders.Authorization = null;
    }

    [Fact]
    public async Task GetOwnLoans_GetWithoutLogin_Fail()
    {
        // Get own loans endpoint should not be accessible without login
        var response = await _client.GetAsync("/api/v1/loans/own-loans");
        var responseString = await response.Content.ReadAsStringAsync();

        Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
    }

    [Fact]
    public async Task GetOwnLoans_AsLibrarian_Fail()
    {
        // in order to get own loans, you need to be logged in as an user, librarians shouldn't be able to get own loans
        var loginCredentials = new LoginUserDto
        {
            Email = "adminseed@mail.com",
            Password = "admin123"
        };
        var loginResponse = await _client.PostAsJsonAsync("/api/v1/auth", loginCredentials);
        var token = await loginResponse.Content.ReadAsStringAsync();

        _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        var response = await _client.GetAsync("/api/v1/loans/own-loans");

        Assert.Equal(HttpStatusCode.Forbidden, response.StatusCode);
        // After each login, clear the authorization header
        _client.DefaultRequestHeaders.Authorization = null;
    }

    // RETURN LOAN ENDPOINT
    public async Task ReturnLoan_ReturnWithLogin_Successfully()
    {
        // in order to return a loan, you need to be logged in as an user, so we need to login first
        var loginCredentials = new LoginUserDto
        {
            Email = "userseed@mail.com",
            Password = "user123"
        };
        var loginResponse = await _client.PostAsJsonAsync("/api/v1/auth", loginCredentials);
        var token = await loginResponse.Content.ReadAsStringAsync();
        _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

        // This loan id is seeded in the database as loan of userseed@mail
        var response = await _client.PatchAsync("/api/v1/loans/return/72b79569-4365-40e7-b448-fa85272d38a6", null);
        var responseString = await response.Content.ReadAsStringAsync();

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        Assert.Contains("72b79569-4365-40e7-b448-fa85272d38a6", responseString);
        // After each login, clear the authorization header
        _client.DefaultRequestHeaders.Authorization = null;
    }

    [Fact]
    public async Task ReturnLoan_ReturnWithoutLogin_Fail()
    {
        // Return loan endpoint should not be accessible without login
        var response = await _client.PatchAsync("/api/v1/loans/return/72b79569-4365-40e7-b448-fa85272d38a6", null);
        var responseString = await response.Content.ReadAsStringAsync();

        Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
    }



    public Task InitializeAsync() => Task.CompletedTask;

    public Task DisposeAsync() => _resetDatabase();
}