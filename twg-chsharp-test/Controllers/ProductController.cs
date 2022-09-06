using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;

namespace CSharpTest.Controllers {
    [ApiController]
    [Route ("[controller]")]
    public class ProductController : ControllerBase {
        private IConfiguration configuration;

        public ProductController(IConfiguration iconfig) {
            configuration = iconfig;

        }

        [Route ("Search"), HttpGet]
        public IActionResult SearchAsync([FromQuery] string q) {
            string SubscriptionKey = configuration.GetValue<string> ("SUB_KEY");
            string ConnectionString = configuration.GetValue<string> ("ConnectionStrings:DB");
            // Search
            HttpClient searchClient = new ();
            searchClient.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", $"{SubscriptionKey}");
            // Hardcode user for POC
            string url = "https://twg.azure-api.net/bolt/search.json?UserId=21E3BC8B-CA74-4C9A-9A0F-F0748A550B92&Search=" + q;
            HttpResponseMessage searchResponse = searchClient.GetAsync (url).Result;
            string searchResult = searchResponse.Content.ReadAsStringAsync ().Result;

            // Log
            SqlConnection connection = new SqlConnection (ConnectionString);
            connection.Open ();
            string sql = "INSERT INTO devtest.SearchRequest (Rid, Search, SuccessInd, Hits) VALUES ('" + new Random ().Next () + "',' " + q + "','Y','1')";
            SqlCommand command = new SqlCommand (sql, connection);
            command.ExecuteNonQuery ();

            return new OkObjectResult (searchResult);
        }

        [Route ("Price"), HttpGet]
        public IActionResult Price([FromQuery] string q) {
            string SubscriptionKey = configuration.GetValue<string> ("SUB_KEY");
            string ConnectionString = configuration.GetValue<string> ("ConnectionStrings:DB");
            HttpClient priceClient = new ();
            string url = "https://twg.azure-api.net/bolt/price.json?UserId=21E3BC8B-CA74-4C9A-9A0F-F0748A550B92&MachineID=test&Barcode=" + q;
            HttpResponseMessage searchResponse = priceClient.GetAsync (url).Result;
            string searchResult = searchResponse.Content.ReadAsStringAsync ().Result;
            return new OkObjectResult (searchResult);

            // log 
        }
    }
}
