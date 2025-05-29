using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace drupAuto
{
    internal static class db_operations
    {
        private static string connectionstring = @"Server=TXCHD-LAP-241\\SQLEXPRESS;Database=dupra1;Trusted_Connection=True;Encrypt=False;TrustServerCertificate=Optional;";

        public static void InsertAccont(int pageNumber,string accountName)
        {
            using (var connection = new SqlConnection(connectionstring))
            using (var command = new SqlCommand("InsertAccount", connection))
            {
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@pagenumber", pageNumber);
                command.Parameters.AddWithValue("@accountname", accountName);
                

                if (connection.State == ConnectionState.Open)
                {
                    connection.Close();
                }
                connection.Open();
                command.ExecuteNonQuery();
                connection.Close();
            }
        }

        public static void InsertPage(int pageNumber)
        {
            using (var connection = new SqlConnection(connectionstring))
            using (var command = new SqlCommand("pages_insert", connection))
            {
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@pagenumber", pageNumber);

                if(connection.State == ConnectionState.Open)
                {
                    connection.Close();
                }
                connection.Open();
                command.ExecuteNonQuery();
                connection.Close();
            }
        }

        public static int getPage()
        {
            using (var connection = new SqlConnection(connectionstring))
            using (var command = new SqlCommand("getpages", connection))
            {
                command.CommandType = CommandType.StoredProcedure;
               
                if (connection.State == ConnectionState.Open)
                {
                    connection.Close();
                }
                connection.Open();
                object result = command.ExecuteScalar();
                connection.Close();

                return result != null ? Convert.ToInt32(result) : 1;
            }
        }
    }
}
