using Microsoft.Data.SqlClient;

namespace APIProject.Common
{
    public class Common
    {
        private readonly IConfiguration _configuration;

        public Common(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public SqlConnection GetConnection()
        {
            string connectionString =
                _configuration.GetConnectionString("ConnectionStrings")
                ?? throw new InvalidOperationException(
                    "Database connection string is missing.");

            return new SqlConnection(connectionString);
        }
    }
}
