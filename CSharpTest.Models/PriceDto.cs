using Newtonsoft.Json;

namespace CSharpTest.Models
{
    [JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
    public class PriceDto
    {
        public string? Price { get; set; }
        public string? Type { get; set; }
    }
}
