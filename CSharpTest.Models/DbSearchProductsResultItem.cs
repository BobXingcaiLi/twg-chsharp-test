using Newtonsoft.Json;

namespace CSharpTest.Models
{
    [JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
    public class DbSearchProductsResultItem
    {
        public string? Description { get; set; }
        public List<ProductDto>? Products { get; set; }
    }
}
