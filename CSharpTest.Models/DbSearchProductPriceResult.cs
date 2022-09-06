namespace CSharpTest.Models
{
    public class DbSearchProductPriceResult
    {
        public string? MachineID { get; set; }
        public string? Action { get; set; }
        public string? ScanBarcode { get; set; }
        public string? ScanID { get; set; }
        public string? UserDescription { get; set; }
        public ProductDto? Product { get; set; }
        public string? ProdQAT { get; set; }
        public string? ScanDateTime { get; set; }
        public string? Found { get; set; }
        public string? UserID { get; set; }
    }
}
