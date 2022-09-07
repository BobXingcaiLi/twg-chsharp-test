using System.Data.SqlClient;
using CSharpTest.Models;
using Dapper;
using Microsoft.Extensions.Configuration;

namespace CSharpTest.Services.Repository
{
    public class SearchTopProductsRepository : ISearchTopProductsRepository
    {
        private readonly IConfiguration _configuration;

        public SearchTopProductsRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task InsertAsync(long rid, List<TopProduct> topProducts)
        {
            string connectionString = _configuration.GetSection("ConnectionStrings:DB").Value;

            if (string.IsNullOrEmpty(connectionString))
            {
                throw new Exception("DB connection string is required");
            }

            string commandText = "INSERT INTO devtest.SearchTopProducts (Rid, [Order], ProductId) VALUES ( @RID, @ORDER, @PRODUCT_ID);";
            using var connection = new SqlConnection(connectionString);
            await connection.OpenAsync();
            try
            {
                foreach (var product in topProducts)
                {
                    await connection.ExecuteScalarAsync(commandText, new { RID = rid, ORDER = product.Order, PRODUCT_ID = product.ProductId});
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
