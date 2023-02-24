using System.Data;
using System.Data.SqlClient;

namespace H3ServersideProject.Data
{
    public class DatabaseContext
    {
        private readonly string _databaseName;
        public DatabaseContext(IConfiguration configuration)
        {
            _databaseName = configuration.GetConnectionString("");
        }

        public IDbConnection Connection()
        {
            return new SqlConnection(_databaseName);
        }
    }
}
