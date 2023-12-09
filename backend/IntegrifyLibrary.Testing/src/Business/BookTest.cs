using AutoMapper;
using IntegrifyLibrary.Business;
using IntegrifyLibrary.Domain;
using Moq;
using System;
using System.Collections.Generic;
using Xunit;

// I am only mainly testing the Book Service, because all services are using the same base class, if the tests here pass, then the other services should pass as well. I will only test other service methods that are not in the base class.
// Loan tests and other complex service will be tested in the integration test project.

namespace IntegrifyLibrary.Testing.Business
{
    public class BookTest
    {
        private readonly Mock<IBookRepo> _mockBookRepo;
        private readonly IMapper _mapper;
        private readonly Mock<IAuthorRepo> _mockAuthorRepo;
        private readonly Mock<IGenreRepo> _mockGenreRepo;

        public BookTest()
        {
            _mockBookRepo = new Mock<IBookRepo>();
            _mockAuthorRepo = new Mock<IAuthorRepo>();
            _mockGenreRepo = new Mock<IGenreRepo>();
            _mapper = new MapperConfiguration(cfg => cfg.AddProfile<AutoMapperProfile>()).CreateMapper();
        }

        [Fact]
        public async Task CreateOne_ShouldCreateNewBook_Successfully()
        {
            var bookService = new BookService(_mockBookRepo.Object, _mapper, _mockAuthorRepo.Object, _mockGenreRepo.Object);
            var createDto = new BookDto
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

            var createdBook = _mapper.Map<Book>(createDto);
            _mockBookRepo.Setup(repo => repo.CreateOne(It.IsAny<Book>())).ReturnsAsync(createdBook);
            var result = await bookService.CreateOne(createDto);

            Assert.NotNull(result);
            Assert.Equal(createDto.BookName, result.BookName);
            Assert.Equal(createDto.AuthorName, result.AuthorName);
        }

        [Fact]
        public async void CreateOne_ShouldThrowException_WhenBookIsNull()
        {
            var bookService = new BookService(_mockBookRepo.Object, _mapper, _mockAuthorRepo.Object, _mockGenreRepo.Object);
            BookDto createDto = null;

            Assert.ThrowsAsync<ArgumentNullException>(() => bookService.CreateOne(createDto));
        }

        [Fact]
        public async Task GetAllBooks_Always_ReturnsAllBooks()
        {
            var bookService = new BookService(_mockBookRepo.Object, _mapper, _mockAuthorRepo.Object, _mockGenreRepo.Object);
            var books = new List<Book>
            {
                new Book
                {
                    BookId = Guid.NewGuid(),
                    BookName = "Harry Potter and the Philosopher's Stone",
                    ISBN = "1234567890",
                    AuthorName = "J.K Rowling",
                    Description = "A sample book description",
                    AuthorId = Guid.NewGuid(),
                    Quantity = 10,
                    PageCount = 200,
                    PublishedDate = new DateOnly(2023, 8, 16),
                    GenreName = "Fantasy",
                    LoanedTimes = 0,
                },
                new Book
                {
                    BookId = Guid.NewGuid(),
                    BookName = "Harry Potter and the Chamber of Secrets",
                    ISBN = "1234567890",
                    AuthorName = "J.K Rowling",
                    Description = "A sample book description",
                    AuthorId = Guid.NewGuid(),
                    Quantity = 10,
                    PageCount = 200,
                    PublishedDate = new DateOnly(2023, 8, 16),
                    GenreName = "Fantasy",
                    LoanedTimes = 0,
                }
            };

            QueryOptions queryOptions = new QueryOptions();
            _mockBookRepo.Setup(repo => repo.GetAll(queryOptions)).ReturnsAsync(books);
            var result = await bookService.GetAll(queryOptions);

            Assert.NotNull(result);
            Assert.Equal(books.Count, result.Count);
        }
        [Fact]
        public async Task GetOneById_IfExists_ReturnsOneBook()
        {
            var bookService = new BookService(_mockBookRepo.Object, _mapper, _mockAuthorRepo.Object, _mockGenreRepo.Object);
            var book = new Book
            {
                BookId = Guid.NewGuid(),
                BookName = "Harry Potter and the Philosopher's Stone",
                ISBN = "1234567890",
                AuthorName = "J.K Rowling",
                Description = "A sample book description",
                AuthorId = Guid.NewGuid(),
                Quantity = 10,
                PageCount = 200,
                PublishedDate = new DateOnly(2023, 8, 16),
                GenreName = "Fantasy",
                LoanedTimes = 0,
            };

            _mockBookRepo.Setup(repo => repo.GetOne(It.IsAny<Guid>())).ReturnsAsync(book);
            var result = await bookService.GetOne(Guid.NewGuid());

            Assert.NotNull(result);
            Assert.Equal(book.BookId, result.BookId);
            Assert.Equal(book.BookName, result.BookName);
        }

        [Fact]
        public async void GetOneById_ShouldThrowException_WhenIdIsEmpty()
        {
            var bookService = new BookService(_mockBookRepo.Object, _mapper, _mockAuthorRepo.Object, _mockGenreRepo.Object);
            Guid id = Guid.Empty;

            Assert.ThrowsAsync<ArgumentException>(() => bookService.GetOne(id));
        }

        [Fact]
        public async Task UpdateOneById_ShouldUpdateBook_Successfully()
        {
            var bookService = new BookService(_mockBookRepo.Object, _mapper, _mockAuthorRepo.Object, _mockGenreRepo.Object);
            var bookId = Guid.NewGuid();
            var book = new Book
            {
                BookId = bookId,
                BookName = "Harry Potter and the Philosopher's Stone",
                ISBN = "1234567890",
                AuthorName = "J.K Rowling",
                Description = "A sample book description",
                AuthorId = Guid.NewGuid(),
                Quantity = 10,
                PageCount = 200,
                PublishedDate = new DateOnly(2023, 8, 16),
                GenreName = "Fantasy",
                LoanedTimes = 0,
            };
            var updateDto = new BookDto
            {
                BookName = "Harry Potter and the Chamber of Secrets", // changed
                ISBN = "1234567890",
                AuthorName = "J.K Rowling",
                Description = "A sample book description",
                Quantity = 10,
                PageCount = 200,
                PublishedDate = new DateOnly(2023, 8, 16),
                GenreName = "Fantasy",
                LoanedTimes = 0,
            };

            _mockBookRepo.Setup(repo => repo.GetOne(It.IsAny<Guid>())).ReturnsAsync(book);
            _mockBookRepo.Setup(repo => repo.UpdateOne(It.IsAny<Book>())).ReturnsAsync(book);
            var result = await bookService.UpdateOne(bookId, updateDto);

            Assert.NotNull(result);
            Assert.Equal(updateDto.BookName, result.BookName);
        }
        [Fact]
        public async void DeleteOneById_IfExists_Successfully()
        {
            var bookService = new BookService(_mockBookRepo.Object, _mapper, _mockAuthorRepo.Object, _mockGenreRepo.Object);
            var bookId = Guid.NewGuid();
            var book = new Book
            {
                BookId = bookId,
                BookName = "Harry Potter and the Philosopher's Stone",
                ISBN = "1234567890",
                AuthorName = "J.K Rowling",
                Description = "A sample book description",
                AuthorId = Guid.NewGuid(),
                Quantity = 10,
                PageCount = 200,
                PublishedDate = new DateOnly(2023, 8, 16),
                GenreName = "Fantasy",
                LoanedTimes = 0,
            };

            _mockBookRepo.Setup(repo => repo.GetOne(It.IsAny<Guid>())).ReturnsAsync(book);
            _mockBookRepo.Setup(repo => repo.DeleteOne(It.IsAny<Book>())).ReturnsAsync(true);
            var result = await bookService.DeleteOne(bookId);

            Assert.True(result);
        }

    }
}
