using System.Data.SqlClient;
using Dapper;
using Microsoft.Extensions.Configuration;

namespace CSharpTest.Services.Repository
{
    public class RequestRepository : IRequestRepository
    {
        private readonly IConfiguration _configuration;

        public RequestRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<long> InsertAsync(char kind)
        {
            string connectionString = _configuration.GetSection("ConnectionStrings:DB").Value;
            long generatedRid = 0;
            
            if (string.IsNullOrEmpty(connectionString))
            {
                throw new Exception("DB connection string is required");
            }

            string commandText = "INSERT INTO devtest.Request (TimeStamp, Kind) VALUES ( @TIMESTAMP, @KIND );SELECT SCOPE_IDENTITY();";
            using var connection = new SqlConnection(connectionString);
            await connection.OpenAsync();
            try
            {
                var result = await connection.ExecuteScalarAsync(commandText, new { TIMESTAMP = DateTime.Now, KIND = kind });

                if(result != null)
                {
                    generatedRid = long.Parse(result.ToString());
                }
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return generatedRid;
        }
    }
}
