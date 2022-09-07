using System.Web;
using CSharpTest.Models;
using CSharpTest.Services.Log;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

namespace CSharpTest.Services.BusinessLogic
{
    public class SearchService : ISearchService
    {
        private readonly IConfiguration _configuration;
        private readonly ILogService _logService;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public SearchService(IConfiguration iconfig, ILogService logService, IHttpClientFactory httpClientFactory, IHttpContextAccessor httpContextAccessor)
        {
            _configuration = iconfig;
            _logService = logService;
            _httpClientFactory = httpClientFactory;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<List<ProductObj>> SearchProductsAsync(string searchTerm)
        {
            var productList = new List<ProductObj>();
            
            // Search
            var searchClient = _httpClientFactory.CreateClient("twg-test-client");

            var queryString = HttpUtility.ParseQueryString(string.Empty);

            //queryString["Start"] = "{string}";
            //queryString["Limit"] = "{string}";
            //queryString["MachineID"] = "{string}";
            //queryString["Branch"] = "{string}";
            queryString["Search"] = searchTerm;
            //queryString["Screen"] = "{string}";
            queryString["UserID"] = _configuration.GetSection("USER_ID").Value;
            var urlPath = $"search.json?" + queryString;
            var searchResponse = await searchClient.GetAsync(urlPath);

            //Log
            var rid = long.Parse(_httpContextAccessor.HttpContext.Request.Headers["RID"].ToString());
            if (searchResponse.IsSuccessStatusCode)
            {
                string searchResult = searchResponse.Content.ReadAsStringAsync().Result;
                var result = JsonConvert.DeserializeObject<ProductSearchResultObj?>(searchResult);
                if (result != null)
                {
                    if (result.Found == 'Y')
                    {
                        await _logService.LogSearchRequest(rid, searchTerm, result.Found, result.HitCount);
                        productList = result.Results!.SelectMany(x => x.Products!).Where(y => !string.IsNullOrEmpty(y.ProductKey)).ToList();
                        var topProducts = productList.Take(3).Select((x, index) => new TopProduct(index, $"R{x.ProductKey}")).ToList();
                        await _logService.LogSearchTopProducts(rid, topProducts);
                    }
                    else
                    {
                        await _logService.LogSearchRequest(rid, searchTerm, result.Found, 0);
                    }
                }
            }
            
            return productList;
        }

        public async Task<ProductObj?> SearchProductPriceAsync(string searchTerm)
        {
            // Search
            var priceClient = _httpClientFactory.CreateClient("twg-test-client");

            var queryString = HttpUtility.ParseQueryString(string.Empty);

            queryString["Barcode"] = searchTerm;
            queryString["MachineID"] = "test";
            queryString["UserID"] = _configuration.GetSection("USER_ID").Value;
            //queryString["Branch"] = "{number}";
            //queryString["DontSave"] = "{string}";
            var urlPath = $"price.json?" + queryString;
            var searchResponse = await priceClient.GetAsync(urlPath);

            //var rid = long.Parse(_httpContextAccessor.HttpContext.Request.Headers["RID"].ToString());

            if (searchResponse.IsSuccessStatusCode)
            {
                string searchResult = searchResponse.Content.ReadAsStringAsync().Result;
                var result = JsonConvert.DeserializeObject<ProductPriceSearchResult?>(searchResult);
                if (result != null && result.Found == 'Y')
                {
                    return result.Product!;
                }
            }
                        
            return null;
        }
    }
}
