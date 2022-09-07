using AutoMapper;
using CSharpTest.Models;
using CSharpTest.Services.BusinessLogic;
using Microsoft.AspNetCore.Mvc;

namespace CSharpTest.Controllers
{
    [ApiController]
    [Route ("[controller]")]
    public class ProductController : ControllerBase {
        private readonly ISearchService _searchService;
        private readonly IMapper _mapper;
        public ProductController(IMapper mapper,
            ISearchService searchService) 
        {
            _mapper = mapper;
            _searchService = searchService;

        }

        [Route ("Search"), HttpGet]
        public async Task<IActionResult> SearchAsync([FromQuery] string q) 
        {
            var result = await _searchService.SearchProductsAsync(q);

            //return the product array
            return new OkObjectResult (_mapper.Map<List<ProductModel>>(result));
        }

        [Route ("Price"), HttpGet]
        public async Task<IActionResult> Price([FromQuery] string q) 
        {
            var result = await _searchService.SearchProductPriceAsync(q);

            //return the product object with price object
            return new OkObjectResult (result != null ? _mapper.Map<ProductWithPriceModel>(result) : "Not Found");
        }
    }
}
