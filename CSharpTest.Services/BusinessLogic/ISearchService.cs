using CSharpTest.Models;

namespace CSharpTest.Services.BusinessLogic
{
    public interface ISearchService
    {
        Task<List<ProductDto>> SearchProductsAsync(string searchTerm);
        Task<ProductDto> SearchProductPriceAsync(string searchTerm);
    }
}
