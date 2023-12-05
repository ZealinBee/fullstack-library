// using System;
// using System.Collections.Generic;
// using System.Linq;
// using System.Text;
// using System.Threading.Tasks;
// using AutoMapper;
// using IntegrifyLibrary.Business;
// using IntegrifyLibrary.Domain;
// using Moq;
// using Xunit;
// using Xunit.Abstractions;

// namespace IntegrifyLibrary.IntegrationTesting.src.Business;

// public class BookAuthorTest{
//    private readonly Mock<IBookRepo> _mockBookRepo;
//    private readonly IMapper _mapper;
//    private readonly Mock<IAuthorRepo> _mockAuthorRepo;
//    private readonly Mock<IGenreRepo> _mockGenreRepo;
//    private readonly ITestOutputHelper _output;


//    public BookAuthorTest(ITestOutputHelper output)
//    {
//       _mockBookRepo = new Mock<IBookRepo>();
//       _mockAuthorRepo = new Mock<IAuthorRepo>();
//       _mockGenreRepo = new Mock<IGenreRepo>();
//       _mapper = new MapperConfiguration(cfg => cfg.AddProfile<AutoMapperProfile>()).CreateMapper();
//       _output = output;
//    }

//    [Fact]
//    public async Task CreateOne_ShouldCreateNewAuthorIfNotExist_Successfully() {
//       // My code logic is that, if the author does not exist while creating book, then create a new author, so I will test this logic here.
//          var bookService = new BookService(_mockBookRepo.Object, _mapper, _mockAuthorRepo.Object, _mockGenreRepo.Object);
//          var authorService = new AuthorService(_mockAuthorRepo.Object, _mapper);

//          var createDto = new BookDto
//          {
//             BookName = "Harry Potter and the Philosopher's Stone",
//             AuthorName = "j.k rowling",
//             Description = "A sample book description",
//             ISBN = "1234567890",
//             Quantity = 10,
//             PageCount = 200,
//             PublishedDate = new DateOnly(2023, 12, 5),
//             GenreName = "Fantasy",
//             LoanedTimes = 0
//          };

//          var createdBook = _mapper.Map<Book>(createDto);
//          _mockBookRepo.Setup(repo => repo.CreateOne(It.IsAny<Book>())).ReturnsAsync(createdBook);
//          var result = await bookService.CreateOne(createDto);

//          _output.WriteLine(result.BookName);
//          _output.WriteLine(result.AuthorName);
//          _output.WriteLine(result.GenreId);
//          Assert.NotNull(result);
//          // Assert.Equal(createDto.AuthorName, author.AuthorName);
//    }


// }