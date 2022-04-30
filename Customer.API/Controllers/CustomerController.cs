using Customers.API.Models;
using Customers.API.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Customers.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerRepository _customerRepository;

        public CustomerController(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository ?? throw new ArgumentNullException(nameof(customerRepository));


        }
        [HttpGet]
        public async Task<IActionResult> List()
        {
            return Ok(await _customerRepository.GetMultipleAsync("SELECT * FROM c"));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(string id)
        {
            return Ok(await _customerRepository.GetAsync(id));
        }

        [HttpPost]
        public async Task<Customer> Create([FromBody] CustomerForCreation customerForCreation)
        {


            var restaurant = await _customerRepository.AddAsync(customerForCreation);
            return restaurant;

        }
        [HttpPut("{id}")]
        public async Task<IActionResult> Edit(string id, CustomerForUpdate customer)
        {
            await _customerRepository.UpdateAsync(id, customer);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            await _customerRepository.DeleteAsync(id);
            return NoContent();
        }
    }
}
