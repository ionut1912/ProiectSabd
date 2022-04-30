using Customers.API.Models;
using Microsoft.Azure.Cosmos;
using Orders.API.Models;
using Restaurants.API.Models;

namespace Orders.API.Repository
{
    public class OrderRepository : IOrderRepository
    {
        private readonly Container? _container;
        private readonly Container? _container1;
        private readonly Container? _container2;
        public OrderRepository(
            CosmosClient cosmosDbClient,
            string databaseName,
            string containerName
            )

        {
            _container = cosmosDbClient.GetContainer(databaseName, containerName);
            _container1 = cosmosDbClient.GetContainer(databaseName, "Customers");
            _container2 = cosmosDbClient.GetContainer(databaseName, "Restaurants");
        }

        public async Task<Order> AddAsync(OrderForCreation orderForCreation)
        {
            var orderCustomer = GetByIdAsync(orderForCreation.customerId).Result;
            var orderRestaurant =GetRestaurantByIdAsync(orderForCreation.restaurantId).Result;
            Order order = new Order
            {
                Id = Guid.NewGuid().ToString(),
                Restaurant=orderRestaurant,
                Address=orderForCreation.address,
                Customer = orderCustomer,
                Price=orderForCreation.price
            };
            return await _container.CreateItemAsync(order, new PartitionKey(order.Id));
        }

        public async Task DeleteAsync(string id)
        {
            await _container.DeleteItemAsync<Order>(id, new PartitionKey(id));
        }
        public async Task<Order> GetAsync(string id)
        {
            try
            {
                var response = await _container.ReadItemAsync<Order>(id, new PartitionKey(id));
                return response.Resource;
            }
            catch (CosmosException) //For handling item not found and other exceptions
            {
                return null;
            }
        }
        public async Task<IEnumerable<Order>> GetMultipleAsync(string queryString)
        {
            var query = _container.GetItemQueryIterator<Order>(new QueryDefinition(queryString));
            var results = new List<Order>();
            while (query.HasMoreResults)
            {
                var response = await query.ReadNextAsync();
                results.AddRange(response.ToList());
            }
            return results;
        }

        public async Task<Customer> GetByIdAsync(string id)
        {
            try
            {
                var response = await _container1.ReadItemAsync<Customer>(id, new PartitionKey(id));
                return response.Resource;
            }
            catch (CosmosException) //For handling item not found and other exceptions
            {
                return null;
            }
        }

     
        public async Task<Restaurant> GetRestaurantByIdAsync(string id)
        {
            try
            {
                var response = await _container2.ReadItemAsync<Restaurant>(id, new PartitionKey(id));
                return response.Resource;
            }
            catch (CosmosException) //For handling item not found and other exceptions
            {
                return null;
            }
        }

        public async Task UpdateAsync(string id, OrderForUpdate orderForUpdate)
        {
            var orderCustomer = GetByIdAsync(orderForUpdate.customerId).Result;
            var orderRestaurant = GetRestaurantByIdAsync(orderForUpdate.restaurantId).Result;
            Order order = new Order
            {
                Id =id,
                Restaurant = orderRestaurant,
                Address = orderForUpdate.address,
                Customer = orderCustomer,
                Price = orderForUpdate.price
            }; ;
            await _container.UpsertItemAsync(order, new PartitionKey(id));
        }
    }
}
