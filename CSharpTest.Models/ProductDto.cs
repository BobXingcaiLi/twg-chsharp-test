using Newtonsoft.Json;

namespace CSharpTest.Models
{
    [JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
    public class ProductDto
    {
        public string? Class0 { get; set; }
        public PriceDto? Price { get; set; }
        public string? Barcode { get; set; }
        public string? ItemDescription { get; set; }
        public string? DeptID { get; set; }
        public string? SubClass { get; set; }
        public string? Class0ID { get; set; }
        public string? SubDeptID { get; set; }
        public string? Description { get; set; }
        public string? ItemCode { get; set; }
        public string? SubDept { get; set; }
        public string? ClassID { get; set; }
        public string? ImageURL { get; set; }
        public string? Dept { get; set; }
        public string? SubClassID { get; set; }
        public string? Class { get; set; }
        public string? ProductKey { get; set; }
    }
}