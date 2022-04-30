using Newtonsoft.Json;

namespace Cities.API.Models
{
    public class City
    {
        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }
        [JsonProperty(PropertyName = "city_name")]
        public string CityName { get; set; }
        [JsonProperty(PropertyName ="zip_code")]
        public string ZipCode { get; set; }
    }
}
