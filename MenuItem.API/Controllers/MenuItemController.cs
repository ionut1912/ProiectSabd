using AutoMapper;
using MenuItems.API.Models;
using MenuItems.API.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MenuItems.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MenuItemController : ControllerBase
    {
        private readonly IMenuItemRepository _menuItemRepository;
        private readonly IMapper _mapper;
        public MenuItemController(IMenuItemRepository menuItemRepository, IMapper mapper)
        {
            _menuItemRepository = menuItemRepository ?? throw new ArgumentNullException(nameof(menuItemRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));

        }
        [HttpGet]
        public async Task<IActionResult> List()
        {
            return Ok(await _menuItemRepository.GetMultipleAsync("SELECT * FROM c"));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(string id)
        {
            return Ok(await _menuItemRepository.GetAsync(id));
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] MenuItemForCreation menuItemForCreation)
        {
            var menuItem = _mapper.Map<MenuItem>(menuItemForCreation);
            menuItem.Id = Guid.NewGuid().ToString();
            await _menuItemRepository.AddAsync(menuItem);
            return CreatedAtAction(nameof(Get), new { id = menuItem.Id }, menuItem);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Edit(string id, MenuItemForUpdate menuItemForUpdate)
        {



            var menuItem = await _menuItemRepository.GetAsync(id);

            if (menuItem is null)
            {
                return NotFound();
            }
            var updatedMenuItem = _mapper.Map<MenuItem>(menuItemForUpdate);
            updatedMenuItem.Id = menuItem.Id;

            await _menuItemRepository.UpdateAsync(id, updatedMenuItem);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            await _menuItemRepository.DeleteAsync(id);
            return NoContent();
        }
    }
}
