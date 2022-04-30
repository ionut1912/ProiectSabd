using AutoMapper;
using Cities.API.Models;
using Cities.API.Repository;
using Microsoft.AspNetCore.Mvc;

namespace Cities.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CityController : ControllerBase
    {
        private readonly ICityRepository _cityRepository;
        private readonly IMapper _mapper;
        public CityController(ICityRepository cityRepository,IMapper mapper)
        {
            _cityRepository = cityRepository ?? throw new ArgumentNullException(nameof(cityRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));

        }
        [HttpGet]
        public async Task<IActionResult> List()
        {
            return Ok(await _cityRepository.GetMultipleAsync("SELECT * FROM c"));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(string id)
        {
            return Ok(await _cityRepository.GetAsync(id));
        }
     
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CityForCreation cityForCreation)
        {
            var city= _mapper.Map<City>(cityForCreation);
            city.Id = Guid.NewGuid().ToString();
            await _cityRepository.AddAsync(city);
            return CreatedAtAction(nameof(Get), new { id = city.Id },city);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Edit(string id, CityForUpdate cityForUpdate)
        {

        
        
            var city = await _cityRepository.GetAsync(id);

            if (city is null)
            {
                return NotFound();
            }
            var updatedCity = _mapper.Map<City>(cityForUpdate);
            updatedCity.Id = city.Id;

            await _cityRepository.UpdateAsync(id, updatedCity);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            await _cityRepository.DeleteAsync(id);
            return NoContent();
        }
    }
}