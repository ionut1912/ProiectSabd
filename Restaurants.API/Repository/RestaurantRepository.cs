using Cities.API.Models;
using Microsoft.Azure.Cosmos;

using Restaurants.API.Models;

namespace Restaurants.API.Repository
{
    public class RestaurantRepository: IRestaurantRepository
    {
        private readonly Container? _container;
        private readonly Container? _container1;
 
        public RestaurantRepository(
            CosmosClient cosmosDbClient,
            string databaseName,
            string containerName
            )
           
        {
            _container = cosmosDbClient.GetContainer(databaseName, containerName);
            _container1= cosmosDbClient.GetContainer(databaseName, "Cities");
         
        }
    


        public async Task<Restaurant> AddAsync(RestaurantForCreation restaurantForCreation)
        {
            var restaurantCity = GetByIdAsync(restaurantForCreation.CityId).Result;
            Restaurant restaurant = new Restaurant
            {
                Id = Guid.NewGuid().ToString(),
                Name = restaurantForCreation.Name,
                City = restaurantCity
            };
           return await _container.CreateItemAsync(restaurant, new PartitionKey(restaurant.Id));
        }
        public async Task DeleteAsync(string id)
        {
            await _container.DeleteItemAsync<Restaurant>(id, new PartitionKey(id));
        }
        public async Task<Restaurant> GetAsync(string id)
        {
            try
            {
                var response = await _container.ReadItemAsync<Restaurant>(id, new PartitionKey(id));
                return response.Resource;
            }
            catch (CosmosException) //For handling item not found and other exceptions
            {
                return null;
            }
        }
        public async Task<IEnumerable<Restaurant>> GetMultipleAsync(string queryString)
        {
            var query = _container.GetItemQueryIterator<Restaurant>(new QueryDefinition(queryString));
            var results = new List<Restaurant>();
            while (query.HasMoreResults)
            {
                var response = await query.ReadNextAsync();
                results.AddRange(response.ToList());
            }
            return results;
        }
        public async Task UpdateAsync(string id, RestaurantForUpdate restaurantForUpdate)
        {
            var cityRestaurant=GetByIdAsync(restaurantForUpdate.CityId).Result;
            Restaurant restaurant = new Restaurant
            {
                Id = id,
                Name=restaurantForUpdate.Name,
                City = cityRestaurant
            };
            await _container.UpsertItemAsync(restaurant, new PartitionKey(id));
        }

        public async Task<City> GetByIdAsync(string id)
        {
            try
            {
                var response = await _container1.ReadItemAsync<City>(id, new PartitionKey(id));
                return response.Resource;
            }
            catch (CosmosException) //For handling item not found and other exceptions
            {
                return null;
            }
        }
    }
}
