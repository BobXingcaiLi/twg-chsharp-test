using CSharpTest.Models;

namespace CSharpTest.Services.Repository
{
    public interface ISearchTopProductsRepository
    {
        Task InsertAsync(long rid, List<TopProduct> topProducts);
    }
}
