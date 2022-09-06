using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CSharpTest.Models;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

namespace CSharpTest.Services.BusinessLogic
{
    public class SearchService : ISearchService
    {
        private IConfiguration _configuration;
        public SearchService(IConfiguration iconfig)
        {
            _configuration = iconfig;
        }

        public async Task<List<ProductDto>> SearchProductsAsync(string searchTerm)
        {
            string SubscriptionKey = _configuration.GetSection("SUB_KEY").Value;
            string ConnectionString = _configuration.GetSection("ConnectionStrings:DB").Value;
            // Search
            HttpClient searchClient = new();
            searchClient.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", $"{SubscriptionKey}");
            // Hardcode user for POC
            string url = "https://twg.azure-api.net/bolt/search.json?Start=0&Limit=10&UserId=21E3BC8B-CA74-4C9A-9A0F-F0748A550B92&Search=" + searchTerm;
            HttpResponseMessage searchResponse = await searchClient.GetAsync(url);
            string searchResult = searchResponse.Content.ReadAsStringAsync().Result;

            
            // Log
            SqlConnection connection = new SqlConnection(ConnectionString);
            await connection.OpenAsync();
            string sql = "INSERT INTO devtest.SearchRequest (Rid, Search, SuccessInd, Hits) VALUES ('" + new Random().Next() + "',' " + searchTerm + "','Y','1')";
            SqlCommand command = new SqlCommand(sql, connection);
            command.ExecuteNonQuery();

            var result = JsonConvert.DeserializeObject<DbSearchProductsResult?>(searchResult);
            if(result != null && result.Found == "Y")
            {
                return result.Results.SelectMany(x => x.Products).Where(y => !string.IsNullOrEmpty(y.ProductKey)).ToList();
            }

            return new List<ProductDto>();
        }

        public async Task<ProductDto> SearchProductPriceAsync(string searchTerm)
        {
            string SubscriptionKey = _configuration.GetSection("SUB_KEY").Value;
            string ConnectionString = _configuration.GetSection("ConnectionStrings:DB").Value;
            HttpClient priceClient = new();
            priceClient.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", $"{SubscriptionKey}");
            string url = "https://twg.azure-api.net/bolt/price.json?UserId=21E3BC8B-CA74-4C9A-9A0F-F0748A550B92&MachineID=test&Barcode=" + searchTerm;
            HttpResponseMessage searchResponse = await priceClient.GetAsync(url);
            string searchResult = searchResponse.Content.ReadAsStringAsync().Result;

            var result = JsonConvert.DeserializeObject<DbSearchProductPriceResult?>(searchResult);
            if (result != null && result.Found == "Y")
            {
                return result.Product;
            }
            return new ProductDto();
        }
    }
}
