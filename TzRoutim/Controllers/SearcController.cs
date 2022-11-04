using Microsoft.AspNetCore.Mvc;
using TzRoutim.Utilities;

namespace TzRoutim.Controllers
{
    [Route("api/")]
    public class SearcController : ControllerBase
    {
        private readonly ApplicationContext _db;
        private readonly ILogger<ApplicationContext> _logger;
        private readonly ISearchServices _seachServices;
        private readonly AddDataDb _addDataDb;
        public SearcController(ApplicationContext db, ILogger<ApplicationContext> logger, ISearchServices seachServices, AddDataDb addDataDb)
        {
            _db = db;
            _logger = logger;
            _seachServices = seachServices;
            _addDataDb = addDataDb;
        }

        [HttpPost]
        [Route("find")]
        public async Task<IActionResult> FindAsync([FromBody] string SearchSting)
        {
            try
            {
                _logger.LogInformation("Запрос FindAsync");
                var search = await _seachServices.SearchAsync(SearchSting);
                if (search == null)
                {
                    var result = await _addDataDb.Write(SearchSting);
                    return Ok(result);
                }
                return Ok(search);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest(ex.Message);
            }

        }
        [HttpGet]
        [Route("find")]
        public async Task<IActionResult> GetAsync()
        {
            try
            {
                var result = await _seachServices.GetSearchAsync();
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest(ex.Message);
            }
        }
        [HttpDelete]
        [Route("find/{id}")]
        public async Task<IActionResult> GetAsync(Guid id)
        {
            try
            {
               var search = await  _db.Searches.FirstOrDefaultAsync(s => s.Id == id);
                if (search != null)
                {
                    _db.Searches.Remove(search);
                    await _db.SaveChangesAsync();
                    return Ok(search);
                }
                return Ok(search);

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest(ex.Message);
            }
        }

    };
}
