using CSharpTest.Models;

namespace CSharpTest.Services.BusinessLogic
{
    public interface ISearchService
    {
        Task<List<ProductObj>> SearchProductsAsync(string searchTerm);
        Task<ProductObj> SearchProductPriceAsync(string searchTerm);
    }
}
