using Newtonsoft.Json;
using Orders.API.Models;

namespace OrderStatus.API.Models
{
    public class OrderStats
    {
        [JsonProperty(PropertyName="id")]
        public string Id { get; set; }
        [JsonProperty(PropertyName = "order")]
        public Order Order { get; set; }
        [JsonProperty(PropertyName ="status")]
        public string Status { get;set; }
    }
}
