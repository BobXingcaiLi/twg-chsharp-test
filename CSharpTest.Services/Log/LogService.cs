using System.Data.SqlClient;
using CSharpTest.Models;
using Microsoft.Extensions.Configuration;

namespace CSharpTest.Services.Log
{
    public class LogService : ILogService
    {
        private readonly IConfiguration _configuration;
        public LogService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<long> LogRequest(char kind)
        {
            long generatedRid = 0;
            string connectionString = _configuration.GetSection("ConnectionStrings:DB").Value;

            string commandText = "INSERT INTO devtest.Request (TimeStamp, Kind) VALUES ( @TIMESTAMP, @KIND );";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(commandText, connection);
                command.Parameters.AddWithValue("@TIMESTAMP", DateTime.Now);
                command.Parameters.AddWithValue("@KIND", kind);

                try
                {
                    await connection.OpenAsync();
                    var result = await command.ExecuteScalarAsync();
                    
                }
                catch (Exception ex)
                {

                }
            }

            return generatedRid;
        }

        public async Task LogSearchRequest(long rid, string search, char successInd, int hits)
        {
            string connectionString = _configuration.GetSection("ConnectionStrings:DB").Value;

            string commandText = "INSERT INTO devtest.SearchRequest (Rid, Search, SuccessInd, Hits) VALUES ( @RID, @SEARCH, @SUCCESS_IND, @HITS);";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(commandText, connection);
                command.Parameters.AddWithValue("@RID", rid);
                command.Parameters.AddWithValue("@SEARCH", search);
                command.Parameters.AddWithValue("@SUCCESS_IND", successInd);
                command.Parameters.AddWithValue("@HITS", hits);

                try
                {
                    await connection.OpenAsync();
                    await command.ExecuteNonQueryAsync();
                }
                catch (Exception ex)
                {
                    //Console.WriteLine(ex.Message);
                }
            }
        }

        public async Task LogSearchTopProducts(long rid, List<TopProduct> topProducts)
        {
            string connectionString = _configuration.GetSection("ConnectionStrings:DB").Value;

            string commandText = "INSERT INTO devtest.SearchTopProducts (Rid, [Order], ProductId) VALUES ( @RID, @ORDER, @PRODUCT_ID);";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(commandText, connection);
                
                try
                {
                    await connection.OpenAsync();
                    foreach (var product in topProducts)
                    {
                        command.Parameters.Clear();
                        command.Parameters.AddWithValue("@RID", rid);
                        command.Parameters.AddWithValue("@ORDER", product.Order);
                        command.Parameters.AddWithValue("@PRODUCT_ID", product.ProductId);
                        await command.ExecuteNonQueryAsync();
                    }
                }
                catch (Exception ex)
                {
                    //Console.WriteLine(ex.Message);
                }
            }
        }
    }
}
