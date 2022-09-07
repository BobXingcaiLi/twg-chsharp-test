using CSharpTest.Models;
using CSharpTest.Services.Repository;

namespace CSharpTest.Services.Log
{
    public class LogService : ILogService
    {
        private readonly IRequestRepository _requestRepository;
        private readonly ISearchRequestRepository _searchRequestRepository;
        private readonly ISearchTopProductsRepository _searchTopProductsRepository;

        public LogService (
            IRequestRepository requestRepository,
            ISearchRequestRepository searchRequestRepository,
            ISearchTopProductsRepository searchTopProductsRepository
            )
        {
            _requestRepository = requestRepository;
            _searchRequestRepository = searchRequestRepository;
            _searchTopProductsRepository = searchTopProductsRepository;
        }

        public async Task<long> LogRequest(char kind)
        {
            return await _requestRepository.InsertAsync(kind);
        }

        public async Task LogSearchRequest(long rid, string search, char successInd, int hits)
        {
            await _searchRequestRepository.InsertAsync(rid, search, successInd, hits);
        }

        public async Task LogSearchTopProducts(long rid, List<TopProduct> topProducts)
        {
            await _searchTopProductsRepository.InsertAsync(rid, topProducts);
        }
    }
}
