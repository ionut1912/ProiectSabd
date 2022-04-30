using Newtonsoft.Json;

namespace MenuItems.API.Models
{
    public class MenuItem
    {
        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }
        [JsonProperty(PropertyName = "item_name")]
        public string ItemName { get; set; }
        [JsonProperty(PropertyName ="category")]
        public string Category { get; set; }
        [JsonProperty(PropertyName = "ingredients")]
        public List<string> Ingredients { get; set; } = new List<string>();
        [JsonProperty(PropertyName = "price")]
        public double Price { get; set; }

    }   
}
