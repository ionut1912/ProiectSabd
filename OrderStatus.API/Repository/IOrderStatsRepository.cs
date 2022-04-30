using Orders.API.Models;
using OrderStatus.API.Models;

namespace OrderStatus.API.Repository
{
    public interface IOrderStatsRepository
    {
        Task<IEnumerable<OrderStats>> GetMultipleAsync(string query);
        Task<OrderStats> GetAsync(string id);
        Task<OrderStats> AddAsync(OrderStatsForCreation orderStatsForCreation);
        Task UpdateAsync(string id, OrderStatsForUpdate orderStatsForUpdate);
        Task DeleteAsync(string id);
        Task<Order> GetByIdAsync(string id);
    }
}
