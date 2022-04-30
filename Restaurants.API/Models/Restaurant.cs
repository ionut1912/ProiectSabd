using Cities.API.Models;
using Newtonsoft.Json;

namespace Restaurants.API.Models
{
    public class Restaurant
    {
        [JsonProperty(PropertyName = "id")]
        public string? Id { get; set; }
        [JsonProperty(PropertyName ="address")]
        public string? Address { get; set; }
        [JsonProperty(PropertyName ="city")]
        public City? City{ get; set; }
    }
}
