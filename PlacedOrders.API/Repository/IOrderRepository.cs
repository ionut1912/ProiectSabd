using Customers.API.Models;
using Orders.API.Models;
using Restaurants.API.Models;

namespace Orders.API.Repository
{
    public interface IOrderRepository
    {  Task<IEnumerable<Order>> GetMultipleAsync(string query);
    Task<Order> GetAsync(string id);
    Task<Order> AddAsync(OrderForCreation orderForCreation);
    Task UpdateAsync(string id, OrderForUpdate orderForUpdate);
    Task DeleteAsync(string id);
    Task<Customer> GetByIdAsync(string id);
    Task<Restaurant> GetRestaurantByIdAsync(string id);
    }
}
