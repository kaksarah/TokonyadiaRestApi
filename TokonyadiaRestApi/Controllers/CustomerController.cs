using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TokonyadiaRestApi.Entities;
using TokonyadiaRestApi.Repositories;

namespace TokonyadiaRestApi.Controllers
{
    [ApiController]
    [Route("api/customers")]
    public class CustomerController : ControllerBase
    {
        private readonly AppDbContext _appDbContext;
        private readonly ILogger _logger;

        public CustomerController(AppDbContext appDbContext, ILogger<CustomerController> logger)
        {
            _appDbContext = appDbContext;
            _logger = logger;
        }

        [HttpPost]
        public async Task<IActionResult> CreateNewCustomer([FromBody] Customer customer)
        {
            var entry = await _appDbContext.Customers.AddAsync(customer);
            await _appDbContext.SaveChangesAsync();
            return new CreatedResult("api/customers", entry.Entity);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllCustomer()
        {
            var customers = await _appDbContext.Customers.ToListAsync();
            return Ok(customers);
           
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCustomerById(string id)
        {
            try
            {
                var customer = await _appDbContext.Customers.FirstOrDefaultAsync(customer => customer.Id.Equals(Guid.Parse(id)));
                if (customer is null) return NotFound("customer not found");

                return Ok(customer);

            }
            catch(Exception e)
            {
                return new StatusCodeResult(500);
            }
        }

        

        [HttpPut]
        public async Task<IActionResult> UpdateCustomer([FromBody] Customer customer)
        {
            try
            {
                if (customer.Id == Guid.Empty) return new NotFoundObjectResult("customer not found");
                //var currentCustomer = await _appDbContext.Customers.FirstOrDefaultAsync(c => c.Id.Equals(customer.Id));
                //if (currentCustomer is null) return new NotFoundObjectResult("customernot found");

                _appDbContext.Customers.Attach(customer);
                _appDbContext.Customers.Update(customer);

                await _appDbContext.SaveChangesAsync();
                return Ok("Success Update Customer");

            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return new StatusCodeResult(500);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCustomer(string id)
        {
            try
            {
                var customer = await _appDbContext.Customers.FirstOrDefaultAsync(customer => customer.Id.Equals(Guid.Parse(id)));
                if (customer.Id == Guid.Empty) return new NotFoundObjectResult("id not found");

                _appDbContext.Customers.Remove(customer);
                await _appDbContext.SaveChangesAsync();
                return Ok("customer successfullydeleted");
            }
            catch(Exception e)
            {
                _logger.LogError(e.Message);
                return new StatusCodeResult(500);
            }
        }

       



    }

}

//[HttpPut("{id}")]
//public async Task<IActionResult> UpdateCustomer(string id, [FromBody] Customer customer)
//{
//    var existingCustomer = await _appDbContext.Customers.FirstOrDefaultAsync(customer => customer.Id.Equals(Guid.Parse(id)));
//    if (existingCustomer is null) return NotFound("customer not found");

//    existingCustomer.CustomerName = customer.CustomerName;
//    existingCustomer.PhoneNumber = customer.PhoneNumber;
//    existingCustomer.Address = customer.Address;
//    existingCustomer.Email = customer.Email;
//    await _appDbContext.SaveChangesAsync();
//    return Ok();
//}

//public async Task<IActionResult> DeleteCustomer(string id)
//{
//var customer = await _appDbContext.Customers.FirstOrDefaultAsync(customer => customer.Id.Equals(Guid.Parse(id)));
//    if (customer is null) return NotFound("customer not found");

//    _appDbContext.Customers.Remove(customer);
//    await _appDbContext.SaveChangesAsync();

//    return Ok();
//}