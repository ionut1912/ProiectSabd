using Cities.API.Models;

namespace Cities.API.Repository
{
    public interface ICityRepository
    {
        Task<IEnumerable<City>> GetMultipleAsync(string query);
        Task<City> GetAsync(string id);
        Task AddAsync(City city);
        Task UpdateAsync(string id,City city);
        Task DeleteAsync(string id);
    }
}
