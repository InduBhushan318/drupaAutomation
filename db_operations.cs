using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace drupAuto
{
    internal static class db_operations
    {
        private static string connectionstring = "";
       
        public static void InsertData(string query)
        {
            // Code to insert data into the database using the connection string
            Console.WriteLine("Inserting data with query: " + query);
            // Implement database insertion logic here
        }
        public static void UpdateData(string query)
        {
            // Code to update data in the database using the connection string
            Console.WriteLine("Updating data with query: " + query);
            // Implement database update logic here
        }
       
    }
}
