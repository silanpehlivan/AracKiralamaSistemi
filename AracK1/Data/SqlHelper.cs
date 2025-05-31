using System.Data.SqlClient;

namespace AracK1.Data
{
    public static class SqlHelper
    {
        private static readonly string connectionString = "Server=.;Database=aracKiralamaSistemi;Trusted_Connection=True;";

        public static SqlConnection GetConnection()
        {
            return new SqlConnection(connectionString);
        }
    }
}

