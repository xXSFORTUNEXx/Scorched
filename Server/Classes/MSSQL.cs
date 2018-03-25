using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using Lidgren.Network;
using static System.Console;
using System.IO;

namespace Server.Classes
{
    public static class MSSQL
    {
        public static void DatabaseExists()
        {
            var sqlFile = File.ReadAllText("SQL Scripts/Database.sql");
            var sqlQueries = sqlFile.Split(new[] { "GO" }, StringSplitOptions.RemoveEmptyEntries);
            var sqlFile2 = File.ReadAllText("SQL Scripts/Tables.sql");
            var sqlQueries2 = sqlFile2.Split(new[] { "GO" }, StringSplitOptions.RemoveEmptyEntries);
            var sqlFile3 = File.ReadAllText("SQL Scripts/Version.sql");
            var sqlQueries3 = sqlFile3.Split(new[] { "GO" }, StringSplitOptions.RemoveEmptyEntries);

            string connection = @"Data Source=FDESKTOP-01\SFORTUNESQL;Integrated Security=True";
            using (SqlConnection conn = new SqlConnection(connection))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand("query", conn))
                {
                    foreach (var query in sqlQueries)
                    {
                        cmd.CommandText = query;
                        cmd.ExecuteNonQuery();
                        Logging.WriteMessageLog("[DB Query] : " + query);
                    }

                    foreach (var query in sqlQueries2)
                    {
                        cmd.CommandText = query;
                        cmd.ExecuteNonQuery();
                        Logging.WriteMessageLog("[DB Query] : " + query);
                    }

                    foreach (var query in sqlQueries3)
                    {
                        cmd.CommandText = query;
                        cmd.ExecuteNonQuery();
                        Logging.WriteMessageLog("[DB Query] : " + query);
                    }
                }
            }
        }
    }
}
