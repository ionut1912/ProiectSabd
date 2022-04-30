using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Restaurants.API.Models;
using Restaurants.API.Repository;

namespace Restaurants.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RestaurantController : ControllerBase
    {
        private readonly IRestaurantRepository _restaurantRepository;
  
        public RestaurantController(IRestaurantRepository restaurantRepository)
        {
            _restaurantRepository = restaurantRepository ?? throw new ArgumentNullException(nameof(restaurantRepository));
          

        }
        [HttpGet]
        public async Task<IActionResult> List()
        {
            return Ok(await _restaurantRepository.GetMultipleAsync("SELECT * FROM c"));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(string id)
        {
            return Ok(await _restaurantRepository.GetAsync(id));
        }

        [HttpPost]
        public async Task Create([FromBody] RestaurantForCreation restaurantForCreation)
        {


            await _restaurantRepository.AddAsync(restaurantForCreation);

        }

    }
}
