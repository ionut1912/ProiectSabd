using Cities.API.Models;
using Customers.API.Models;
using Microsoft.Azure.Cosmos;

namespace Customers.API.Repository
{
    public class CustomerRepository:ICustomerRepository
    {
        private readonly Container? _container;
        private readonly Container? _container1;

        public CustomerRepository(
            CosmosClient cosmosDbClient,
            string databaseName,
            string containerName
            )

        {
            _container = cosmosDbClient.GetContainer(databaseName, containerName);
            _container1 = cosmosDbClient.GetContainer(databaseName, "Cities");

        }



        public async Task<Customer> AddAsync(CustomerForCreation customerForCreation)
        {
            var customerCity = GetByIdAsync(customerForCreation.city_id).Result;
            Customer customer = new Customer
            {
                Id = Guid.NewGuid().ToString(),
                CustomerName = customerForCreation.CustomerName,
                City = customerCity,
                Address = customerForCreation.Address,
                Phone = customerForCreation.Phone,
                Email = customerForCreation.Email,
                Password = customerForCreation.Password
                
            };
            return await _container.CreateItemAsync(customer, new PartitionKey(customer.Id));
        }
        public async Task DeleteAsync(string id)
        {
            await _container.DeleteItemAsync<Customer>(id, new PartitionKey(id));
        }
        public async Task<Customer> GetAsync(string id)
        {
            try
            {
                var response = await _container.ReadItemAsync<Customer>(id, new PartitionKey(id));
                return response.Resource;
            }
            catch (CosmosException) //For handling item not found and other exceptions
            {
                return null;
            }
        }
        public async Task<IEnumerable<Customer>> GetMultipleAsync(string queryString)
        {
            var query = _container.GetItemQueryIterator<Customer>(new QueryDefinition(queryString));
            var results = new List<Customer>();
            while (query.HasMoreResults)
            {
                var response = await query.ReadNextAsync();
                results.AddRange(response.ToList());
            }
            return results;
        }
        public async Task UpdateAsync(string id, CustomerForUpdate CustomerForUpdate)
        {
            var customerCity = GetByIdAsync(CustomerForUpdate.city_id).Result;
            Customer customer = new Customer
            {
                Id = id,
                CustomerName = CustomerForUpdate.CustomerName,
                City = customerCity,
                Address = CustomerForUpdate.Address,
                Phone = CustomerForUpdate.Phone,
                Email = CustomerForUpdate.Email,
                Password = CustomerForUpdate.Password

            };
            await _container.UpsertItemAsync(customer, new PartitionKey(id));
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
