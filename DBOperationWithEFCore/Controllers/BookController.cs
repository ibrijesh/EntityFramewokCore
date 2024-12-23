using DBOperationWithEFCore.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DBOperationWithEFCore.Controllers
{
    [Route("api/books")]
    [ApiController]
    public class BookController(AppDbContext _appDbContext) : ControllerBase
    {
        [HttpPost("")]
        public async Task<IActionResult> AddBooksAsync([FromBody] Book model)
        {
            _appDbContext.Books.Add(model);
            await _appDbContext.SaveChangesAsync();
            return Ok(model);
        }

        [HttpPost("bulk")]
        public async Task<IActionResult> AddBookInBulkAsync([FromBody] List<Book> model)
        {
            _appDbContext.Books.AddRange(model);
            await _appDbContext.SaveChangesAsync();
            return Ok(model);
        }

        [HttpPut("")]
        public async Task<IActionResult> UpdateBookAsync([FromBody] Book model)
        {
            _appDbContext.Books.Update(model);
            await _appDbContext.SaveChangesAsync();
            return Ok(model);
        }
    }
}