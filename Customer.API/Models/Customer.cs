using Cities.API.Models;
using Newtonsoft.Json;

namespace Customers.API.Models
{
    public class Customer
    {
        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }
        [JsonProperty(PropertyName = "customer_name")]
        public string CustomerName { get; set; }
        [JsonProperty(PropertyName = "city")]
        public City City { get; set; }
        [JsonProperty(PropertyName ="address")]
        public string Address { get; set; }
        [JsonProperty(PropertyName ="phone")]
        public string Phone { get; set; }
        [JsonProperty(PropertyName ="email")]
        public string Email { get; set; }
        [JsonProperty(PropertyName ="password")]
        public string Password { get; set; }


    }
}
