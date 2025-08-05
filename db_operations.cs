using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using drupAuto.Models;
using OpenQA.Selenium.DevTools.V135.Page;

namespace drupAuto
{
    internal static class db_operations
    {
        private static string connectionstring = @"Server=TXCHD-LAP-241\SQLEXPRESS;Database=dupra;Trusted_Connection=True;Encrypt=False;TrustServerCertificate=True;";



        public static List<pagesModel> getPage()
        {
            List<pagesModel> pm = new List<pagesModel>();
            using (var connection = new SqlConnection(connectionstring))
            {
                using (var command = new SqlCommand("select * from [dbo].[Pages] where isprocessed=0 order by id ", connection))
                {
                    command.CommandType = CommandType.Text;

                    if (connection.State == ConnectionState.Open)
                    {
                        connection.Close();
                    }
                    connection.Open();
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            pm.Add(new pagesModel
                            {
                                id = Convert.ToInt32(reader["id"]),
                                PageNumber = Convert.ToInt32(reader["PageNumber"]),
                                isprocessed = Convert.ToBoolean(reader["isprocessed"])
                            });
                        }
                    }
                    return pm;
                }
            }
        }

        public static void UpdatePage(int pageId, bool isprocessed)
        {
            using (var connection = new SqlConnection(connectionstring))
            {
                using (var command = new SqlCommand("update pages  set isprocessed = 1 where id= " + pageId, connection))
                {
                    command.CommandType = CommandType.Text;

                    if (connection.State == ConnectionState.Open)
                    {
                        connection.Close();
                    }
                    connection.Open();
                    command.ExecuteNonQuery();
                    connection.Close();
                }
            }
        }
    }
}
