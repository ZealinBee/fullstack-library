using System;
using System.Linq;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System.Data;
using System.Data.Common;
using Respawn;
using Npgsql;
using System.Security.Cryptography;

public class CustomWebApplicationFactory
    : WebApplicationFactory<Program>, IAsyncLifetime

{
    public HttpClient HttpClient { get; private set; } = default!;

    private DbConnection _dbConnection = default!;
    private Respawner _respawner = default!;

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureServices(services =>
        {
            var descriptor = services.SingleOrDefault(
                d => d.ServiceType == typeof(DbContextOptions<DatabaseContext>));
            if (descriptor != null)
            {
                services.Remove(descriptor);
            }

            services.AddDbContext<DatabaseContext>((options, context) =>
            {
                context.UseNpgsql("Host=localhost;Port=5432;Username=tester;Password=password;Database=integration_library").UseSnakeCaseNamingConvention();
            });

            var sp = services.BuildServiceProvider();

            using (var scope = sp.CreateScope())
            {
                var scopedServices = scope.ServiceProvider;
                var db = scopedServices.GetRequiredService<DatabaseContext>();
                db.Database.EnsureCreated();
            }
        });
    }

    public async Task InitializeAsync()
    {
        HttpClient = CreateClient();
        _dbConnection = new NpgsqlConnection("Host=localhost;Port=5432;Username=tester;Password=password;Database=integration_library");
        await _dbConnection.OpenAsync();
        _respawner = await Respawner.CreateAsync(_dbConnection, new RespawnerOptions
        {
            DbAdapter = DbAdapter.Postgres,
            SchemasToInclude = new[] { "public" },
        });

    }

    public new async Task DisposeAsync()
    {

    }

    public async Task ResetDatabaseAsync()
    {
        await _respawner.ResetAsync(_dbConnection);
        await SeedDatabaseAsync();
    }

    public async Task SeedDatabaseAsync()
    {
        using (var scope = Services.CreateScope())
        {
            var scopedServices = scope.ServiceProvider;
            var db = scopedServices.GetRequiredService<DatabaseContext>();
            await db.AddAsync(new Author
            {
                AuthorId = Guid.Parse("be924f89-17bd-445d-a341-335d9ef0938f"),
                AuthorName = "J.K Rowling",
                AuthorImage = "imagelink"
            });
            await db.AddAsync(new Genre
            {
                GenreId = Guid.Parse("84af208d-9418-4ca7-8c77-37f3fe14f7fa"),
                GenreName = "Fantasy"
            });
            await db.AddAsync(new Book
            {
                BookId = Guid.Parse("b7aef6dc-534f-4a7d-96c8-98f2b72b53bd"),
                BookName = "Harry Potter and the Philosopher's Stone",
                ISBN = "123456789",
                AuthorName = "J.K Rowling",
                Description = "Description",
                AuthorId = Guid.Parse("be924f89-17bd-445d-a341-335d9ef0938f"),
                Quantity = 10,
                PageCount = 100,
                LoanedTimes = 0,
                BookImage = "imagelink",
                GenreId = Guid.Parse("84af208d-9418-4ca7-8c77-37f3fe14f7fa"),
                GenreName = "Fantasy",
            });
            await db.AddAsync(new Book
            {
                BookId = Guid.Parse("010fe0a2-4b53-45bb-92ab-89ce03e730b1"),
                BookName = "Harry Potter and the Chamber of Secrets",
                ISBN = "123456789",
                AuthorName = "J.K Rowling",
                Description = "Description",
                AuthorId = Guid.Parse("be924f89-17bd-445d-a341-335d9ef0938f"),
                Quantity = 10,
                PageCount = 100,
                LoanedTimes = 0,
                BookImage = "imagelink",
                GenreId = Guid.Parse("84af208d-9418-4ca7-8c77-37f3fe14f7fa"),
                GenreName = "Fantasy",
            });
            await db.AddAsync(new User
            {
                UserId = Guid.Parse("b042dc17-5959-44a6-baad-7b32377577a3"),
                FirstName = "tester",
                LastName = "tester",
                Email = "adminseed@mail.com",
                Password = "admin123",
                Salt = new byte[128],
                Role = Role.Librarian,
                UserImage = "imagelink"
            });
            await db.AddAsync(new User
            {
                UserId = Guid.Parse("9e6a7c45-2b86-423a-89c0-30b049ec97be"),
                FirstName = "tester",
                LastName = "tester",
                Email = "userseed@mail.com",
                Password = "user123",
                Salt = new byte[128],
                Role = Role.User,
                UserImage = "imagelink"
            });
            await db.AddAsync(new Loan
            {
                LoanId = Guid.Parse("72b79569-4365-40e7-b448-fa85272d38a6"),
                UserId = Guid.Parse("9e6a7c45-2b86-423a-89c0-30b049ec97be"),
                LoanDate = new DateOnly(2021, 10, 10),
                DueDate = new DateOnly(2021, 10, 17),
                ReturnedDate = new DateOnly(2021, 10, 11),
                IsReturned = true,
                IsOverdue = false
            });
            await db.AddAsync(new LoanDetails
            {
                LoanDetailsId = Guid.Parse("bfad1a40-9959-44d0-a9b4-fa86e335eba5"),
                LoanId = Guid.Parse("72b79569-4365-40e7-b448-fa85272d38a6"),
                BookId = Guid.Parse("b7aef6dc-534f-4a7d-96c8-98f2b72b53bd")
            });
            await db.SaveChangesAsync();
        }


    }


}



