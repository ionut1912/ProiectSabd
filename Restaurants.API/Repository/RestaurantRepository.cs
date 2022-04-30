﻿using Cities.API.Models;
using Microsoft.Azure.Cosmos;
using Restaurants.API.Extensions;
using Restaurants.API.Models;

namespace Restaurants.API.Repository
{
    public class RestaurantRepository: IRestaurantRepository
    {
        private readonly Container? _container;
        private readonly IHttpClientFactory? _httpClientFactory;
        public RestaurantRepository(
            CosmosClient cosmosDbClient,
            string databaseName,
            string containerName
            )
           
        {
            _container = cosmosDbClient.GetContainer(databaseName, containerName);
          
         
        }
        public RestaurantRepository(

            CosmosClient cosmosDbClient,
            string databaseName,
            string containerName, IHttpClientFactory httpClientFactory) : this(cosmosDbClient, databaseName, containerName)
        {
            _httpClientFactory = httpClientFactory;
        }
            


        public async Task AddAsync(RestaurantForCreation restaurantForCreation)
        {
            var restaurantCity = GetByIdAsync(restaurantForCreation.CityId).Result;
            Restaurant restaurant = new Restaurant
            {
                Id = Guid.NewGuid().ToString(),
                Address = restaurantForCreation.Address,
                City = restaurantCity
            };
            await _container.CreateItemAsync(restaurant, new PartitionKey(restaurant.Id));
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
        public async Task UpdateAsync(string id, Restaurant restaurant)
        {
            await _container.UpsertItemAsync(restaurant, new PartitionKey(id));
        }

        public async Task<City> GetByIdAsync(string id)
        {
            var httpClient = _httpClientFactory.CreateClient();
            var response = await httpClient.GetAsync($"/api/City/{id}");
            return await response.ReadContentAs<City>();
        }
    }
}