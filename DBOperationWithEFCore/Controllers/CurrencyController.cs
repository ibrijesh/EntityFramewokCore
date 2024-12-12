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
        public async Task<IActionResult> GetAllCurrencies()
        {
            var result = await _appDbContext.Currencies.ToListAsync();
            return Ok(result);
        }
    }
}