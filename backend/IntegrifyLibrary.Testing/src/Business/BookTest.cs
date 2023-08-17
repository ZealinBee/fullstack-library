// using AutoMapper;
// using IntegrifyLibrary.Business;
// using IntegrifyLibrary.Domain;
// using Moq;
// using System;
// using Xunit;

// namespace IntegrifyLibrary.Testing.Business;
// public class BookTest
// {
//     private readonly Mock<IBookRepo> _mockBookRepo;
//     private readonly IMapper _mapper;

//     public BookTest()
//     {
//         _mockBookRepo = new Mock<IBookRepo>();
//         _mapper = new MapperConfiguration(cfg => cfg.AddProfile<AutoMapperProfile>()).CreateMapper();
//     }

//     [Fact]
//     public void CreateOne_Should_Create_New_Book_Successfully()
//     {
//         // Arrange
//         var bookService = new BookService(_mockBookRepo.Object, _mapper);
//         var createDto = new BookDto
//         {
//             BookName = "Harry Potter and the Philosopher's Stone",
//             AuthorName = "J.K Rowling",
//             Description = "A sample book description",
//             ISBN = "1234567890",
//             Quantity = 10,
//             PageCount = 200,
//             PublishedDate = new DateOnly(2023, 8, 16),
//             GenreId = Guid.NewGuid(),
//             AuthorId = Guid.NewGuid()
//         };
//         var createdBook = _mapper.Map<Book>(createDto);

//         _mockBookRepo.Setup(repo => repo.CreateOne(It.IsAny<Book>())).Returns(createdBook);

//         var result = bookService.CreateOne(createDto);

//         Assert.NotNull(result);
//         Assert.Equal(createDto.BookName, result.BookName);
//         Assert.Equal(createDto.AuthorName, result.AuthorName);
//     }

//     [Fact]
//     public void CreateOne_Should_Throw_Exception_When_Book_Is_Null()
//     {
//         var bookService = new BookService(_mockBookRepo.Object, _mapper);
//         BookDto createDto = null;

//         Assert.Throws<ArgumentNullException>(() => bookService.CreateOne(createDto));
//     }




// }
