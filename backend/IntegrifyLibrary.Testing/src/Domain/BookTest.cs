using IntegrifyLibrary.Domain;
using System;
using Xunit;

namespace IntegrifyLibrary.Testing.Domain{
    public class BookTest {
        [Fact]
        public void CreateBook_SingleValidData_NewBook() {
            var bookId = Guid.NewGuid();
            var createdAt = new DateOnly(2023, 10, 19);
            var updatedAt = new DateOnly(2023, 10, 20);

            var book = new Book {
                BookId = bookId,
                BookName = "The Lord of the Rings",
                ISBN = "9780544003415",
                AuthorName = "J.R.R. Tolkien",
                Description = "The Lord of the Rings is an epic high-fantasy novel written by English author and scholar J. R. R. Tolkien.",
                Quantity = 10,
                PageCount = 1178,
                PublishedDate = new DateOnly(1954, 7, 29),
                CreatedAt = createdAt,
                ModifiedAt = updatedAt,
                Genre = new Genre(),
                GenreId = Guid.NewGuid(),
                GenreName = "",
                LoanDetails = new List<LoanDetails>()
            };

            Assert.Equal(bookId, book.BookId);
            Assert.Equal("The Lord of the Rings", book.BookName);
            Assert.Equal("9780544003415", book.ISBN);
            Assert.Equal("J.R.R. Tolkien", book.AuthorName);
            Assert.Equal("The Lord of the Rings is an epic high-fantasy novel written by English author and scholar J. R. R. Tolkien.", book.Description);
            Assert.Equal(10, book.Quantity);
            Assert.Equal(1178, book.PageCount);
            Assert.Equal(new DateOnly(1954, 7, 29), book.PublishedDate);
            Assert.Equal(createdAt, book.CreatedAt);
            Assert.Equal(updatedAt, book.ModifiedAt);
            Assert.NotNull(book.Genre);
            Assert.NotNull(book.LoanDetails);
            Assert.Empty(book.LoanDetails);
        }
    }
}