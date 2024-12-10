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
            var result = _appDbContext.Languages.ToList();
            return Ok(result);
        }
        
    }
}
