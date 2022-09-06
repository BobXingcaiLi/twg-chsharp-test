using CSharpTest.Services.BusinessLogic;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;

namespace CSharpTest.Controllers {
    [ApiController]
    [Route ("[controller]")]
    public class ProductController : ControllerBase {
        private readonly IConfiguration _configuration;
        private readonly ISearchService _searchService;

        public ProductController(IConfiguration iconfig,
            ISearchService searchService) 
        {
            _configuration = iconfig;
            _searchService = searchService;

        }

        [Route ("Search"), HttpGet]
        public async Task<IActionResult> SearchAsync([FromQuery] string q) 
        {
            var result = await _searchService.SearchProductsAsync(q);
            return new OkObjectResult (result);
        }

        [Route ("Price"), HttpGet]
        public async Task<IActionResult> Price([FromQuery] string q) 
        {
            var result = await _searchService.SearchProductPriceAsync(q);
            return new OkObjectResult (result);
        }
    }
}
