namespace IntegrifyLibrary.IntegrationTesting;
[Collection("Integration Tests")]

public class ReservationControllerTest : IAsyncLifetime
{
    private readonly HttpClient _client;
    private readonly Func<Task> _resetDatabase;

    public ReservationControllerTest(CustomWebApplicationFactory factory)
    {
        _client = factory.HttpClient;
        _resetDatabase = factory.ResetDatabaseAsync;
    }

    // GET RESERVATIONS ENDPOINT
    [Fact]
    public async Task GetOwnReservation_GetWithLogin_Successfully()
    {
        // in order to get your own reservation, you need to be logged in as an user, so we need to login first
        var loginCredentials = new LoginUserDto
        {
            Email = "userseed@mail.com",
            Password = "user123"
        };
        var loginResponse = await _client.PostAsJsonAsync("/api/v1/auth", loginCredentials);
        var token = await loginResponse.Content.ReadAsStringAsync();

        _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        var response = await _client.GetAsync("/api/v1/reservations/own-reservations");
        var responseString = await response.Content.ReadAsStringAsync();

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        // seeded reservation id should be in the response
        Assert.Contains("cb41dde4-40b4-443f-abe6-374c33b10291", responseString);
        // After login, clear the authorization header
        _client.DefaultRequestHeaders.Authorization = null;
    }
    // POST RESERVATION ENDPOINT
    [Fact]
    public async Task PostReservation_BookStillAvailable_Fail()
    {
        var loginCredentials = new LoginUserDto
        {
            Email = "userseed@mail.com",
            Password = "user123"
        };
        var loginResponse = await _client.PostAsJsonAsync("/api/v1/auth", loginCredentials);
        var token = await loginResponse.Content.ReadAsStringAsync();
        _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

        var reservationRequest = new CreateReservationDto
        {
            BookId = Guid.Parse("010fe0a2-4b53-45bb-92ab-89ce03e730b1")
        };
        var response = await _client.PostAsJsonAsync("/api/v1/reservations", reservationRequest);
        var responseString = await response.Content.ReadAsStringAsync();

        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        Assert.Contains("Book is available, no need to reserve", responseString);
        // After login, clear the authorization header
        _client.DefaultRequestHeaders.Authorization = null;
    }
    public Task InitializeAsync() => Task.CompletedTask;

    public Task DisposeAsync() => _resetDatabase();
}