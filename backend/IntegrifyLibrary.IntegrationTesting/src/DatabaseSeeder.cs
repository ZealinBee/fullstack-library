public class DatabaseSeeder
{
    private readonly DatabaseContext _context;
    private readonly Guid authorId = Guid.Parse("be924f89-17bd-445d-a341-335d9ef0938f");
    private readonly Guid genreId = Guid.Parse("84af208d-9418-4ca7-8c77-37f3fe14f7fa");
    private readonly Guid book1Id = Guid.Parse("b7aef6dc-534f-4a7d-96c8-98f2b72b53bd");
    private readonly Guid book2Id = Guid.Parse("010fe0a2-4b53-45bb-92ab-89ce03e730b1");
    private readonly Guid book3Id = Guid.Parse("60e1017d-545d-47eb-8e39-9e8dc57b3e08");
    private readonly Guid user1Id = Guid.Parse("9e6a7c45-2b86-423a-89c0-30b049ec97be");
    private readonly Guid user2Id = Guid.Parse("4e2d80dc-f452-40a8-a037-bd62e6912a87");
    private readonly Guid adminId = Guid.Parse("b042dc17-5959-44a6-baad-7b32377577a3");
    private readonly Guid loan1Id = Guid.Parse("72b79569-4365-40e7-b448-fa85272d38a6");
    private readonly Guid loan2Id = Guid.Parse("430cb202-f6ed-48ac-ae52-38af92273284");

    public DatabaseSeeder(DatabaseContext context)
    {
        _context = context;
    }

    public async Task SeedAuthorsAsync()
    {
        await _context.AddAsync(new Author
        {
            AuthorId = authorId,
            AuthorName = "J.K Rowling",
            AuthorImage = "imagelink"
        });
    }

    public async Task SeedGenresAsync()
    {
        await _context.AddAsync(new Genre
        {
            GenreId = genreId,
            GenreName = "Fantasy"
        });

    }

    public async Task SeedBooksAsync()
    {
        await _context.AddAsync(new Book
        {
            BookId = book1Id,
            BookName = "Harry Potter and the Philosopher's Stone",
            ISBN = "1234567891",
            AuthorName = "J.K Rowling",
            Description = "Description",
            AuthorId = authorId,
            Quantity = 10,
            PageCount = 100,
            LoanedTimes = 0,
            BookImage = "imagelink",
            GenreId = genreId,
            GenreName = "Fantasy",
        });
        await _context.AddAsync(new Book
        {
            BookId = book2Id,
            BookName = "Harry Potter and the Chamber of Secrets",
            ISBN = "1234567891",
            AuthorName = "J.K Rowling",
            Description = "Description",
            AuthorId = authorId,
            Quantity = 10,
            PageCount = 100,
            LoanedTimes = 0,
            BookImage = "imagelink",
            GenreId = genreId,
            GenreName = "Fantasy",
        });
        await _context.AddAsync(new Book
        {
            BookId = book3Id,
            BookName = "Harry Potter and the Deathly Hallows",
            ISBN = "12345678912",
            AuthorName = "J.K Rowling",
            Description = "Description",
            AuthorId = authorId,
            Quantity = 0,
            PageCount = 100,
            LoanedTimes = 0,
            BookImage = "imagelink",
            GenreId = genreId,
            GenreName = "Fantasy",
        });

    }

    public async Task SeedUsersAsync()
    {
        await _context.AddAsync(new User
        {
            UserId = adminId,
            FirstName = "tester",
            LastName = "tester",
            Email = "adminseed@mail.com",
            Password = "admin123",
            Salt = new byte[128],
            Role = Role.Librarian,
            UserImage = "imagelink"
        });
        await _context.AddAsync(new User
        {
            UserId = user1Id,
            FirstName = "tester",
            LastName = "tester",
            Email = "userseed@mail.com",
            Password = "user123",
            Salt = new byte[128],
            Role = Role.User,
            UserImage = "imagelink"
        });
        await _context.AddAsync(new User
        {
            UserId = user2Id,
            FirstName = "tester2",
            LastName = "tester2",
            Email = "userseed2@mail.com",
            Password = "user123",
            Salt = new byte[128],
            Role = Role.User,
            UserImage = "imagelink"
        });
    }

    public async Task SeedLoansAsync()
    {
        await _context.AddAsync(new Loan
        {
            LoanId = loan1Id,
            UserId = user1Id,
            LoanDate = new DateOnly(2023, 10, 10),
            DueDate = new DateOnly(2023, 10, 17),
            ReturnedDate = new DateOnly(2021, 10, 11),
            IsReturned = true,
            IsOverdue = false
        });
        // this loan is overdue
        await _context.AddAsync(new Loan
        {
            LoanId = loan2Id,
            UserId = user2Id,
            LoanDate = new DateOnly(2023, 10, 10),
            DueDate = new DateOnly(2023, 10, 17),
            ReturnedDate = new DateOnly(2021, 10, 11),
            IsReturned = false,
            IsOverdue = false
        });
    }

    public async Task SeedLoanDetailsAsync()
    {
        await _context.AddAsync(new LoanDetails
        {
            LoanDetailsId = Guid.Parse("bfad1a40-9959-44d0-a9b4-fa86e335eba5"),
            LoanId = loan1Id,
            BookId = book1Id
        });
        await _context.AddAsync(new LoanDetails
        {
            LoanDetailsId = Guid.Parse("bf7655e0-1df6-4761-8cf8-8d43db4ca4fd"),
            LoanId = loan2Id,
            BookId = book3Id
        });
    }

    public async Task SeedReservationsAsync()
    {
        await _context.AddAsync(new Reservation
        {
            ReservationId = Guid.Parse("cb41dde4-40b4-443f-abe6-374c33b10291"),
            UserId = user1Id,
            BookId = book3Id,
        });
    }

    public async Task SeedDatabaseAsync()
    {
        await SeedAuthorsAsync();
        await SeedGenresAsync();
        await SeedBooksAsync();
        await SeedUsersAsync();
        await SeedLoansAsync();
        await SeedLoanDetailsAsync();
        await SeedReservationsAsync();
        await _context.SaveChangesAsync();
    }




}