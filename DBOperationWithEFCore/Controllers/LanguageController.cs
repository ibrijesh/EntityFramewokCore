using DBOperationWithEFCore.Data;
using Microsoft.AspNetCore.Mvc;

namespace DBOperationWithEFCore.Controllers
{
    [Route("api/languages")]
    [ApiController]
    public class LanguageController : ControllerBase
    {
        private readonly AppDbContext _appDbContext;

        public LanguageController(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        [HttpGet("")]
        public IActionResult GetAllLanguages()
        {
            var result = (from languages in _appDbContext.Languages
                select languages).ToList();
            return Ok(result);
        }
    }
}