// namespace IntegrifyLibrary.IntegrationTesting;
// public class BookControllerTest
// {
//     // Testing to see if routes work
//     private readonly CustomWebApplicationFactory _factory;
//     private readonly HttpClient _client;

//     public BookControllerTest()
//     {
//         _factory = new CustomWebApplicationFactory();
//         _client = _factory.CreateClient(new WebApplicationFactoryClientOptions
//         {
//             AllowAutoRedirect = false
//         });
//     }


//     [Fact]
//     public async Task PostBook_PostWithoutLogin_Fail()
//     {
//         // Book post endpoint should not be accessible without login
//         var newBook = new Book()
//         {
//             BookName = "Harry Potter and the Philosopher's Stone",
//             AuthorName = "J.K Rowling",
//             Description = "A sample book description",
//             ISBN = "1234567890",
//             Quantity = 10,
//             PageCount = 200,
//             PublishedDate = new DateOnly(2023, 8, 16),
//             GenreName = "Fantasy",
//             LoanedTimes = 0
//         };
//         var response = await _client.PostAsync("/api/v1/books", JsonContent.Create(newBook));
//         var responseString = await response.Content.ReadAsStringAsync();

//         Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
//     }

//     [Fact]
//     public async Task GetBooks_GetWithoutLogin_Successfully()
//     {
//         // Book get endpoint should be accessible without login
//         var response = await _client.GetAsync("/api/v1/books");
//         var responseString = await response.Content.ReadAsStringAsync();

//         Assert.Equal(HttpStatusCode.OK, response.StatusCode);
//     }



// }

