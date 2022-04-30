using Cities.API.Models;
using Microsoft.Azure.Cosmos;

namespace Cities.API.Repository
{
    public class CityRepository : ICityRepository
    {
        private Container _container;
        public CityRepository(
            CosmosClient cosmosDbClient,
            string databaseName,
            string containerName)
        {
            _container = cosmosDbClient.GetContainer(databaseName, containerName);
        }
        public async Task AddAsync(City city)
        {
            await _container.CreateItemAsync(city, new PartitionKey(city.Id));
        }
        public async Task DeleteAsync(string id)
        {
            await _container.DeleteItemAsync<City>(id, new PartitionKey(id));
        }
        public async Task<City> GetAsync(string id)
        {
            try
            {
                var response = await _container.ReadItemAsync<City>(id, new PartitionKey(id));
                return response.Resource;
            }
            catch (CosmosException) //For handling item not found and other exceptions
            {
                return null;
            }
        }
        public async Task<IEnumerable<City>> GetMultipleAsync(string queryString)
        {
            var query = _container.GetItemQueryIterator<City>(new QueryDefinition(queryString));
            var results = new List<City>();
            while (query.HasMoreResults)
            {
                var response = await query.ReadNextAsync();
                results.AddRange(response.ToList());
            }
            return results;
        }
        public async Task UpdateAsync(string id, City city)
        {
            await _container.UpsertItemAsync(city, new PartitionKey(id));
        }
    }
}
