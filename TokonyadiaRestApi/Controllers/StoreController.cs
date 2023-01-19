using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TokonyadiaRestApi.Entities;
using TokonyadiaRestApi.Repositories;

namespace TokonyadiaRestApi.Controllers
{
    [ApiController]
    [Route("api/stores")]
    public class StoreController : ControllerBase
    {
        private readonly AppDbContext _appDbContext;
        private readonly ILogger _logger;

        public StoreController(AppDbContext appDbContext, ILogger<StoreController> logger)
        {
            _appDbContext = appDbContext;
            _logger = logger;
        }

        [HttpPost]
        public async Task<IActionResult> CreateNewStore([FromBody] Store store)
        {
            var entry = await _appDbContext.Stores.AddAsync(store);
            await _appDbContext.SaveChangesAsync();
            return new CreatedResult("api/stores", entry.Entity);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllStore()
        {
            var stores = await _appDbContext.Stores.ToListAsync();
            return Ok(stores);

        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetStoreById(string id)
        {
            try
            {
                var store = await _appDbContext.Stores.FirstOrDefaultAsync(store => store.Id.Equals(Guid.Parse(id)));
                if (store is null) return NotFound("store not found");

                return Ok(store);

            }
            catch (Exception e)
            {
                return new StatusCodeResult(500);
            }
        }



        [HttpPut]
        public async Task<IActionResult> UpdateStore([FromBody] Store store)
        {
            try
            {
                if (store.Id == Guid.Empty) return new NotFoundObjectResult("store not found");
                var currentStore = await _appDbContext.Stores.FirstOrDefaultAsync(store => store.Id.Equals(store.Id));
                if (currentStore is null) return new NotFoundObjectResult("storenot found");

                var entry = _appDbContext.Stores.Attach(store);
                _appDbContext.Stores.Update(store);

                await _appDbContext.SaveChangesAsync();
                return Ok("store successfullydeleted");

            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return new StatusCodeResult(500);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStore(string id)
        {
            try
            {
                var store = await _appDbContext.Stores.FirstOrDefaultAsync(store => store.Id.Equals(Guid.Parse(id)));
                if (store.Id == Guid.Empty) return new NotFoundObjectResult("id not found");

                _appDbContext.Stores.Remove(store);
                await _appDbContext.SaveChangesAsync();
                return Ok("store successfullydeleted");
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return new StatusCodeResult(500);
            }
        }





    }

}