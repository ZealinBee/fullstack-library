namespace IntegrifyLibrary.IntegrationTesting;
[Collection("Integration Tests")]

public class NotificationControllerTest : IAsyncLifetime
{
    private readonly HttpClient _client;
    private readonly Func<Task> _resetDatabase;

    public NotificationControllerTest(CustomWebApplicationFactory factory)
    {
        _client = factory.HttpClient;
        _resetDatabase = factory.ResetDatabaseAsync;
    }

    [Fact]
    public async Task NotifyUser_BookIsAvailable_Successfully()
    {
        // Harry Potter and the Deathly Hallows was reserved by userseed, userseed2 is currently borrowing the book, let's return from userseed2, and see if userseed gets notified
        var loginCredentials = new LoginUserDto
        {
            Email = "userseed2@mail.com",
            Password = "user123"
        };
        var loginResponse = await _client.PostAsJsonAsync("/api/v1/auth", loginCredentials);
        var token = await loginResponse.Content.ReadAsStringAsync();
        _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        await _client.PatchAsync("/api/v1/loans/return/430cb202-f6ed-48ac-ae52-38af92273284", null);

        // now userseed should get notified, we will log out userseed2 and log in userseed
        _client.DefaultRequestHeaders.Authorization = null;
        loginCredentials = new LoginUserDto
        {
            Email = "userseed@mail.com",
            Password = "user123"
        };
        loginResponse = await _client.PostAsJsonAsync("/api/v1/auth", loginCredentials);
        token = await loginResponse.Content.ReadAsStringAsync();
        _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

        // now we can check if userseed got notified
        var response = await _client.GetAsync("/api/v1/notifications/own-notifications");
        var responseString = await response.Content.ReadAsStringAsync();

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        Assert.Contains("Harry Potter and the Deathly Hallows is now available", responseString);
        // After login, clear the authorization header
        _client.DefaultRequestHeaders.Authorization = null;
    }
    // TODO: fix this test
    // [Fact]
    // public async Task ScheduledService_NotifyOverdueLoan_Successfully()
    // {
    //     // user 2 has a loan that is overdue, let's check if the scheduled service can notify him, we'll login to check
    //     var loginCredentials = new LoginUserDto
    //     {
    //         Email = "userseed@mail.com",
    //         Password = "user123"
    //     };
    //     var loginResponse = await _client.PostAsJsonAsync("/api/v1/auth", loginCredentials);
    //     var token = await loginResponse.Content.ReadAsStringAsync();
    //     _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

    //     // now we can check if userseed2 got notified
    //     var response = await _client.GetAsync("/api/v1/notifications/own-notifications");
    //     var responseString = await response.Content.ReadAsStringAsync();

    //     Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    //     Assert.Contains("Your loan is overdue", responseString);
    //     // After login, clear the authorization header
    //     _client.DefaultRequestHeaders.Authorization = null;
    // }

    public Task InitializeAsync() => Task.CompletedTask;

    public Task DisposeAsync() => _resetDatabase();

}