namespace CSharpTest.Models
{
    public class ProductSearchResultObj
    {
        public int HitCount { get; set; }
        public List<ProductSearchResultItem>? Results { get; set; }
        public int SearchID { get; set; }
        public string? ProdQAT { get; set; }
        public char Found { get; set; }
    }
}
