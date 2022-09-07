namespace CSharpTest.Models
{
    public class ProductPriceSearchResult
    {
        public string? MachineID { get; set; }
        public string? Action { get; set; }
        public string? ScanBarcode { get; set; }
        public string? ScanID { get; set; }
        public string? UserDescription { get; set; }
        public ProductObj? Product { get; set; }
        public string? ProdQAT { get; set; }
        public DateTime ScanDateTime { get; set; }
        public char Found { get; set; }
        public Guid UserID { get; set; }
    }
}
