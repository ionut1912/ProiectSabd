
using Microsoft.AspNetCore.Mvc;
using OrderStatus.API.Models;
using OrderStatus.API.Repository;

namespace OrderStatus.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderStatsController : ControllerBase
    {
        private readonly IOrderStatsRepository _orderStatsRepository;

        public OrderStatsController(IOrderStatsRepository orderStatsRepository)
        {
            _orderStatsRepository = orderStatsRepository?? throw new ArgumentNullException(nameof(orderStatsRepository));


        }
        [HttpGet]
        public async Task<IActionResult> List()
        {
            return Ok(await _orderStatsRepository.GetMultipleAsync("SELECT * FROM c"));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(string id)
        {
            return Ok(await _orderStatsRepository.GetAsync(id));
        }

        [HttpPost]
        public async Task<OrderStats> Create([FromBody] OrderStatsForCreation orderStatsForCreation)
        {


            var orderStats = await _orderStatsRepository.AddAsync(orderStatsForCreation);
            return orderStats;

        }
        [HttpPut("{id}")]
        public async Task<IActionResult> Edit(string id,OrderStatsForUpdate restaurant)
        {
            await _orderStatsRepository.UpdateAsync(id, restaurant);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            await _orderStatsRepository.DeleteAsync(id);
            return NoContent();
        }
    }
}
