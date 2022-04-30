using MenuItems.API.Models;

namespace MenuItems.API.Repository
{
    public interface IMenuItemRepository
    {
        Task<IEnumerable<MenuItem>> GetMultipleAsync(string query);
        Task<MenuItem> GetAsync(string id);
        Task AddAsync(MenuItem menuItem);
        Task UpdateAsync(string id, MenuItem menuItem);
        Task DeleteAsync(string id);
    }
}
