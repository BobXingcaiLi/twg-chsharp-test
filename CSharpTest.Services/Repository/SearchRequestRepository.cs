using System.Data.SqlClient;
using Dapper;
using Microsoft.Extensions.Configuration;

namespace CSharpTest.Services.Repository
{
    public class SearchRequestRepository : ISearchRequestRepository
    {
        private readonly IConfiguration _configuration;

        public SearchRequestRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task InsertAsync(long rid, string search, char successInd, int hits)
        {
            string connectionString = _configuration.GetSection("ConnectionStrings:DB").Value;

            if (string.IsNullOrEmpty(connectionString))
            {
                throw new Exception("DB connection string is required");
            }

            string commandText = "INSERT INTO devtest.SearchRequest (Rid, Search, SuccessInd, Hits) VALUES ( @RID, @SEARCH, @SUCCESS_IND, @HITS);";
            using var connection = new SqlConnection(connectionString);
            await connection.OpenAsync();
            try
            {
                await connection.ExecuteScalarAsync(commandText, new { RID = rid, SEARCH = search, SUCCESS_IND = successInd, HITS = hits });
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
