using MenuItems.API.Models;
using Microsoft.Azure.Cosmos;

namespace MenuItems.API.Repository
{
    public class MenuItemRepository:IMenuItemRepository
    {
        private Container _container;
        public MenuItemRepository(
            CosmosClient cosmosDbClient,
            string databaseName,
            string containerName)
        {
            _container = cosmosDbClient.GetContainer(databaseName, containerName);
        }
        public async Task AddAsync(MenuItem menuItem)
        {
            await _container.CreateItemAsync(menuItem, new PartitionKey(menuItem.Id));
        }
        public async Task DeleteAsync(string id)
        {
            await _container.DeleteItemAsync<MenuItem>(id, new PartitionKey(id));
        }
        public async Task<MenuItem> GetAsync(string id)
        {
            try
            {
                var response = await _container.ReadItemAsync<MenuItem>(id, new PartitionKey(id));
                return response.Resource;
            }
            catch (CosmosException) //For handling item not found and other exceptions
            {
                return null;
            }
        }
        public async Task<IEnumerable<MenuItem>> GetMultipleAsync(string queryString)
        {
            var query = _container.GetItemQueryIterator<MenuItem>(new QueryDefinition(queryString));
            var results = new List<MenuItem>();
            while (query.HasMoreResults)
            {
                var response = await query.ReadNextAsync();
                results.AddRange(response.ToList());
            }
            return results;
        }
        public async Task UpdateAsync(string id, MenuItem menuItem)
        {
            await _container.UpsertItemAsync(menuItem, new PartitionKey(id));
        }
    }
}
