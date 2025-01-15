using DBOperationWithEFCore.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DBOperationWithEFCore.Controllers
{
    [Route("api/books")]
    [ApiController]
    public class BooksController(AppDbContext appDbContext) : ControllerBase
    {
        [HttpGet("navigationalProperties")]
        public async Task<IActionResult> GetNavigationalProperties()
        {
            var result = appDbContext.Books.Select(x => new
            {
                Id = x.Id,
                Title = x.Title,
                Author = x.Author != null ? x.Author.Name : "NA",
                Language = x.Language
            });
            return Ok(result);
        }

        [HttpGet("")]
        public async Task<IActionResult> GetBooks()
        {
            var result = await (from currencies in appDbContext.Currencies
                select currencies).AsNoTracking().ToListAsync();
            return Ok(result);
        }

        [HttpGet("{bookId}")]
        public async Task<IActionResult> GetBookByIdAsync([FromRoute] int bookId)
        {
            var book = await appDbContext.Books.FindAsync(bookId);

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

        [HttpPut("")]
        public async Task<IActionResult> UpdateBookInSingleQueryAsync([FromBody] Book model)
        {
            appDbContext.Books.Update(model);
            await appDbContext.SaveChangesAsync();

            return Ok(model);
        }

        [HttpPut("bulk")]
        public async Task<IActionResult> UpdateBooksInBulkAsync()
        {
            await appDbContext.Books.ExecuteUpdateAsync(x => x
                .SetProperty(p => p.Description, p => p.Description + " This is  updated description"));
            return Ok();
        }

        [HttpDelete("{bookId}")]
        public async Task<IActionResult> DeleteBookAsync([FromRoute] int bookId)
        {
            var book = new Book { Id = bookId };
            appDbContext.Entry(book).State = EntityState.Deleted;
            await appDbContext.SaveChangesAsync();

            // var book = await appDbContext.Books.FindAsync(bookId);
            // if (book == null)   
            // {
            //     return NotFound();
            // }
            //
            // appDbContext.Remove(book);
            // await appDbContext.SaveChangesAsync();

            return Ok();
        }

        [HttpDelete("bulk")]
        public async Task<IActionResult> DeleteBooksInbulkAsync()
        {
            await appDbContext.Books.Where(x => x.Id > 15 && x.Id < 17).ExecuteDeleteAsync();
            return Ok();
        }
    }
}