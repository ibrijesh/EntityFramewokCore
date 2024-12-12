using DBOperationWithEFCore.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DBOperationWithEFCore.Controllers
{
    [Route("api/currencies")]
    [ApiController]
    public class CurrencyController : ControllerBase
    {
        private readonly AppDbContext _appDbContext;

        public CurrencyController(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        [HttpGet("")]
        public async Task<IActionResult> GetAllCurrenciesAsync()
        {
            var result = await _appDbContext.Currencies.ToListAsync();
            return Ok(result);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> FindCurrencyByIdAsync([FromRoute] int id)
        {
            var result = await _appDbContext.Currencies.FindAsync(id);
            return Ok(result);
        }


        [HttpGet("{name}")]
        public async Task<IActionResult> FindCurrencyByNameAsync([FromRoute] string name)
        {
            var result = await _appDbContext.Currencies.Where(x => x.Title == name).SingleOrDefaultAsync();
            return Ok(result);
        }
    }
}