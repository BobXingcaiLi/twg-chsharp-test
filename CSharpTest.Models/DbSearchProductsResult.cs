using Newtonsoft.Json;

namespace CSharpTest.Models
{
    [JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
    public class DbSearchProductsResult
    {
        public string? HitCount { get; set; }
        public List<DbSearchProductsResultItem>? Results { get; set; }
        public string? SearchID { get; set; }
        public string? ProdQAT { get; set; }
        public string? Found { get; set; }
    }
}
