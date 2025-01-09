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
    }
}