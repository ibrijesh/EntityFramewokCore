using DBOperationWithEFCore.Data;
using Microsoft.AspNetCore.Mvc;

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
        public IActionResult GetAllCurrencies()
        {
            var result = _appDbContext.Currencies.ToList();
            return Ok(result);
        }
    }
}