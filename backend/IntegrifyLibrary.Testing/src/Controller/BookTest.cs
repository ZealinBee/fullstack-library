// using AutoMapper;
// using IntegrifyLibrary.Business;
// using IntegrifyLibrary.Domain;
// using IntegrifyLibrary.Controllers;

// using Microsoft.AspNetCore.Mvc;
// using Moq;
// using System;
// using System.Collections.Generic;
// using Xunit;

// namespace IntegrifyLibrary.Testing
// {
//     public class BookControllerTests
//     {
//         private readonly Mock<IBaseService<BookDto, BookDto, BookDto>> _mockBookService;
//         private readonly IMapper _mapper;

//         public BookControllerTests()
//         {
//             _mockBookService = new Mock<IBaseService<BookDto, BookDto, BookDto>>();
//             _mapper = new MapperConfiguration(cfg => cfg.AddProfile<AutoMapperProfile>()).CreateMapper();
//         }

//         [Fact]
//         public void GetBook_Should_Return_Book_When_Exists()
//         {
//             // Arrange
//             var controller = new BookController(_mockBookService);
//             var bookId = Guid.NewGuid();
//             var expectedBookDto = new BookDto
//             {
//                 BookName = "Sample Book",
//                 AuthorName = "John Doe",
//                 Description = "A sample book description",
//                 ISBN = "1234567890",
//                 Quantity = 10,
//                 PageCount = 200,
//                 PublishedDate = new DateOnly(2023, 8, 16),
//                 GenreId = Guid.NewGuid(),
//                 AuthorId = Guid.NewGuid()
//             };

//             _mockBookService.Setup(service => service.GetOne(bookId)).Returns(expectedBookDto);

//             // Act
//             var result = controller.GetOne(bookId);

//             // Assert
//             var okResult = Assert.IsType<OkObjectResult>(result);
//             var returnedBook = Assert.IsType<BookDto>(okResult.Value);

//             Assert.Equal(expectedBookDto.BookName, returnedBook.BookName);
//             Assert.Equal(expectedBookDto.AuthorName, returnedBook.AuthorName);
//             Assert.Equal(expectedBookDto.Description, returnedBook.Description);
//             Assert.Equal(expectedBookDto.ISBN, returnedBook.ISBN);
//             Assert.Equal(expectedBookDto.Quantity, returnedBook.Quantity);
//             Assert.Equal(expectedBookDto.PageCount, returnedBook.PageCount);
//             Assert.Equal(expectedBookDto.PublishedDate, returnedBook.PublishedDate);
//             Assert.Equal(expectedBookDto.GenreId, returnedBook.GenreId);
//             Assert.Equal(expectedBookDto.AuthorId, returnedBook.AuthorId);
//         }



//     }
// }
