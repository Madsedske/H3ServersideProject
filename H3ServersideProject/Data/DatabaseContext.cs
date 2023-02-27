using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Data.SqlClient;

namespace H3ServersideProject.Data
{
    public class DatabaseContext : DbContext
    {
        private readonly string _databaseName;
        public DatabaseContext(IConfiguration configuration)
        {
            _databaseName = configuration.GetConnectionString("connection");
        }

        public IDbConnection Connection()
        {
            return new SqlConnection(_databaseName);
        }
    }
}
