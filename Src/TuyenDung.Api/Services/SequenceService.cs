using Dapper;
using System.Data;
using System.Data.SqlClient;

namespace TuyenDung.Api.Services
{
    public class SequenceService : ISequenceService
    {
        private readonly IConfiguration _configuration;

        public SequenceService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<int> GetKnowledgeBaseNewId()
        {
            using (SqlConnection conn = new SqlConnection(_configuration.GetConnectionString("TuyenDungDatabase")))
            {
                if (conn.State == ConnectionState.Closed)
                {
                    await conn.OpenAsync();
                }

                var result = await conn.ExecuteScalarAsync<int>(@"SELECT (NEXT VALUE FOR TuyenDungSequence)", null, null, 120, CommandType.Text);
                return result;
            }
        }
    }
}
