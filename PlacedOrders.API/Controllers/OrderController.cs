using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Orders.API.Models;
using Orders.API.Repository;

namespace Orders.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderRepository _orderRepository;

        public OrderController(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository ?? throw new ArgumentNullException(nameof(orderRepository));


        }
        [HttpGet]
        public async Task<IActionResult> List()
        {
            return Ok(await _orderRepository.GetMultipleAsync("SELECT * FROM c"));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(string id)
        {
            return Ok(await _orderRepository.GetAsync(id));
        }

        [HttpPost]
        public async Task<Order> Create([FromBody] OrderForCreation orderForCreation)
        {


            var order = await _orderRepository.AddAsync(orderForCreation);
            return order;

        }
        [HttpPut("{id}")]
        public async Task<IActionResult> Edit(string id,OrderForUpdate order)
        {
            await _orderRepository.UpdateAsync(id,order);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            await _orderRepository.DeleteAsync(id);
            return NoContent();
        }
    }
}
