using Microsoft.Azure.Cosmos;
using Orders.API.Models;
using OrderStatus.API.Models;

namespace OrderStatus.API.Repository
{
    public class OrderStatsRepository:IOrderStatsRepository
    {
        private readonly Container? _container;
        private readonly Container? _container1;

        public OrderStatsRepository(
            CosmosClient cosmosDbClient,
            string databaseName,
            string containerName
            )

        {
            _container = cosmosDbClient.GetContainer(databaseName, containerName);
            _container1 = cosmosDbClient.GetContainer(databaseName, "Orders");

        }



        public async Task<OrderStats> AddAsync(OrderStatsForCreation orderStatsForCreation)
        {
            var order = GetByIdAsync(orderStatsForCreation.orderId).Result;
            OrderStats orderStats = new OrderStats
            {
                Id = Guid.NewGuid().ToString(),
                Order = order,
                Status = orderStatsForCreation.status
            };
            return await _container.CreateItemAsync(orderStats, new PartitionKey(orderStats.Id));
        }
        public async Task DeleteAsync(string id)
        {
            await _container.DeleteItemAsync<OrderStats>(id, new PartitionKey(id));
        }
        public async Task<OrderStats> GetAsync(string id)
        {
            try
            {
                var response = await _container.ReadItemAsync<OrderStats>(id, new PartitionKey(id));
                return response.Resource;
            }
            catch (CosmosException) //For handling item not found and other exceptions
            {
                return null;
            }
        }
        public async Task<IEnumerable<OrderStats>> GetMultipleAsync(string queryString)
        {
            var query = _container.GetItemQueryIterator<OrderStats>(new QueryDefinition(queryString));
            var results = new List<OrderStats>();
            while (query.HasMoreResults)
            {
                var response = await query.ReadNextAsync();
                results.AddRange(response.ToList());
            }
            return results;
        }
        public async Task UpdateAsync(string id,OrderStatsForUpdate orderStatsForUpdate)
        {
            var order = GetByIdAsync(orderStatsForUpdate.orderId).Result;
            OrderStats orderStats = new OrderStats
            {
                Id = id,
                Order = order,
                Status = orderStatsForUpdate.status
            };
            await _container.UpsertItemAsync(orderStats, new PartitionKey(id));
        }

        public async Task<Order> GetByIdAsync(string id)
        {
            try
            {
                var response = await _container1.ReadItemAsync<Order>(id, new PartitionKey(id));
                return response.Resource;
            }
            catch (CosmosException) //For handling item not found and other exceptions
            {
                return null;
            }
        }

    }
}
