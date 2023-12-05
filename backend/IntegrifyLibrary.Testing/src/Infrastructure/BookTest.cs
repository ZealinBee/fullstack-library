// using IntegrifyLibrary.Domain;
// using IntegrifyLibrary.Infrastructure;
// using Microsoft.EntityFrameworkCore;
// using System.Linq;
// using Xunit;

// using Microsoft.EntityFrameworkCore.InMemory;

// namespace IntegrifyLibrary.Testing.Infrastructure;

// public class BookRepositoryTests
// {
//       [Fact]
//         public void CanCreateBook()
//         {
//             // Arrange
//             var optionsBuilder = new DbContextOptionsBuilder<DatabaseContext>()
//                 .UseInMemoryDatabase(Guid.NewGuid().ToString());
            
//             // You need to provide IConfiguration here
//             var configuration = new ConfigurationBuilder()
//                 .AddJsonFile("appsettings.json")  // Adjust the file path accordingly
//                 .Build();

//             var context = new DatabaseContext(configuration, optionsBuilder.Options);

//             var bookRepo = new BookRepository(context);

//             // Act
//             bookRepo.CreateOne(new Book { BookName = "Test Book" });

//             // Assert
//             Assert.Single(context.Books);
//             var createdBook = context.Books.First();
//             Assert.Equal("Test Book", createdBook.BookName);
//         }

// }