namespace IntegrifyLibrary.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    [Route("api/v1/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {

        private static List<string> _books = new()
        {
        "book1", "book2", "book3", "book4", "book5"
    };

        [HttpGet]
        public async Task<IActionResult> GetAllBooks()
        {
            return Ok(_books);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetBookById(int id)
        {
            return Ok(_books[id]);
        }

        [HttpPost]
        public async Task<IActionResult> AddBook([FromBody] string book)
        {
            _books.Add(book);
            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateBook([FromRoute] int index, [FromBody] string book)
        {
            _books[index] = book;
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBook(int id)
        {
            _books.RemoveAt(id);
            return Ok();
        }

    }
}