using Cities.API.Models;
using Customers.API.Models;

namespace Customers.API.Repository
{
    public interface ICustomerRepository
    {
        Task<IEnumerable<Customer>> GetMultipleAsync(string query);
        Task<Customer> GetAsync(string id);
        Task<Customer> AddAsync(CustomerForCreation customerForCreation);
        Task UpdateAsync(string id, CustomerForUpdate customerForUpdate);
        Task DeleteAsync(string id);
        Task<City> GetByIdAsync(string id);
    }
}
