using Cities.API.Models;
using Restaurants.API.Models;

namespace Restaurants.API.Repository
{
  
    public interface IRestaurantRepository
    {
        Task<IEnumerable<Restaurant>> GetMultipleAsync(string query);
        Task<Restaurant> GetAsync(string id);
        Task<Restaurant> AddAsync(RestaurantForCreation restaurantForCreation);
        Task UpdateAsync(string id, Restaurant restaurant);
        Task DeleteAsync(string id);
        Task<City> GetByIdAsync(string id); 
    }
}
