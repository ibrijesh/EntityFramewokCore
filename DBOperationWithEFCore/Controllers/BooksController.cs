using DBOperationWithEFCore.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DBOperationWithEFCore.Controllers
{
    [Route("api/books")]
    [ApiController]
    public class BooksController(AppDbContext appDbContext) : ControllerBase
    {
        [HttpGet("")]
        public async Task<IActionResult> GetBookByIdAsync([FromQuery] int id)
        {
            var book = await appDbContext.Books.FindAsync(id);

            if (book == null)
            {
                return NotFound();
            }

            return Ok(book);
        }


        [HttpPost("")]
        public async Task<IActionResult> AddBookAsync([FromBody] Book model)
        {
            appDbContext.Books.Add(model);
            await appDbContext.SaveChangesAsync(); // it makes single sql call here only.

            return Ok(model);
        }

        [HttpPost("bulk")]
        public async Task<IActionResult> AddBooksAsync([FromBody] List<Book> models)
        {
            appDbContext.Books.AddRange(models);
            await appDbContext.SaveChangesAsync();

            return Ok(models);
        }

        [HttpPost("bulkNotOptimized")]
        public async Task<IActionResult> AddBooksAsyncNotOptimized([FromBody] List<Book> models)
        {
            var books = new List<Book>();

            foreach (var model in models)
            {
                appDbContext.Books.Add(model);
                await appDbContext.SaveChangesAsync();
                books.Add(model);
            }

            return Ok(books);
        }

        [HttpPost("related")]
        public async Task<IActionResult> AddBooksRelatedAsync([FromBody] Book model)
        {
            var author = new Author()
            {
                Name = "Author 1",
                Email = "author1@email.com"
            };

            model.Author = author;
            appDbContext.Books.Add(model);
            await appDbContext.SaveChangesAsync();

            return Ok(model);
        }

        [HttpPost("author")]
        public async Task<IActionResult> AddBookWithAuthorAsync([FromBody] Book model)
        {
            appDbContext.Books.Add(model);
            await appDbContext.SaveChangesAsync(); // it makes single sql call here only.

            return Ok(model);
        }

        [HttpPut("{bookId}")]
        public async Task<IActionResult> UpdateBookAsync([FromRoute] int bookId, [FromBody] Book model)
        {
            var book = appDbContext.Books.FirstOrDefault(x => x.Id == bookId);

            if (book == null)
            {
                return NotFound();
            }

            book.Title = model.Title;
            book.Description = model.Description;
            book.NoOfPages = model.NoOfPages;

            await appDbContext.SaveChangesAsync();

            return Ok(book);
        }
    }
}