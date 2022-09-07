using System;
using System.Collections.Generic;

using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CSharpTest.Models;
using CSharpTest.Services.Log;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

namespace CSharpTest.Services.BusinessLogic
{
    public class SearchService : ISearchService
    {
        private readonly IConfiguration _configuration;
        private readonly ILogService _logService;
        public SearchService(IConfiguration iconfig, ILogService logService)
        {
            _configuration = iconfig;
            _logService = logService;
        }

        public async Task<List<ProductObj>> SearchProductsAsync(string searchTerm)
        {
            var productList = new List<ProductObj>();
            string SubscriptionKey = _configuration.GetSection("SUB_KEY").Value;
            
            // Search
            HttpClient searchClient = new();
            searchClient.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", $"{SubscriptionKey}");
            // Hardcode user for POC
            string url = "https://twg.azure-api.net/bolt/search.json?Start=0&Limit=10&UserId=21E3BC8B-CA74-4C9A-9A0F-F0748A550B92&Search=" + searchTerm;
            HttpResponseMessage searchResponse = await searchClient.GetAsync(url);
            string searchResult = searchResponse.Content.ReadAsStringAsync().Result;

            //Log
            var rid = await _logService.LogRequest('S');
            

            var result = JsonConvert.DeserializeObject<ProductSearchResultObj?>(searchResult);
            if(result != null)
            {
                if(result.Found == 'Y')
                {
                    await _logService.LogSearchRequest(rid, searchTerm, result.Found, result.HitCount);
                    productList = result.Results!.SelectMany(x => x.Products!).Where(y => !string.IsNullOrEmpty(y.ProductKey)).ToList();
                    var topProducts = productList.Take(3).Select((x, index) => new TopProduct(index, x.ProductKey)).ToList();
                    await _logService.LogSearchTopProducts(rid, topProducts);
                }
                else
                {
                    await _logService.LogSearchRequest(rid, searchTerm, result.Found, 0);
                }
            }

            return productList;
        }

        public async Task<ProductObj> SearchProductPriceAsync(string searchTerm)
        {
            string SubscriptionKey = _configuration.GetSection("SUB_KEY").Value;
            HttpClient priceClient = new();
            priceClient.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", $"{SubscriptionKey}");
            string url = "https://twg.azure-api.net/bolt/price.json?UserId=21E3BC8B-CA74-4C9A-9A0F-F0748A550B92&MachineID=test&Barcode=" + searchTerm;
            HttpResponseMessage searchResponse = await priceClient.GetAsync(url);
            string searchResult = searchResponse.Content.ReadAsStringAsync().Result;

            //Log
            var rid = await _logService.LogRequest('P');

            var result = JsonConvert.DeserializeObject<ProductPriceSearchResult?>(searchResult);
            if (result != null && result.Found == 'Y')
            {
                return result.Product;
            }
            return new ProductObj();
        }
    }
}
