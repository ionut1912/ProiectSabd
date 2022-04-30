using Customers.API.Models;
using Newtonsoft.Json;
using Restaurants.API.Models;

namespace Orders.API.Models
{
    public class Order
    {
        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }
        [JsonProperty(PropertyName = "restaurant")]
        public Restaurant Restaurant;
        [JsonProperty(PropertyName ="address")]
        public string Address { get; set; }
        [JsonProperty(PropertyName ="customer")]
        public Customer Customer { get; set; }
        [JsonProperty(PropertyName ="price")]
        public decimal  Price { get; set; }

    }
}
