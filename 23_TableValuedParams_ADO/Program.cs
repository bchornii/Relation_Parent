using System;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;

namespace _23_TableValuedParams_ADO
{
    class Program
    {
        static void Main(string[] args)
        {
            string cs = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(cs))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.CommandText = "spInsertOrder";
                    cmd.CommandTimeout = 60;

                    SqlParameter param = new SqlParameter
                    {
                        ParameterName = "@TestStructTp",
                        SqlDbType = SqlDbType.Structured,
                        Value = GetData()
                    };                                        
                    cmd.Parameters.Add(param);

                    conn.Open();
                    cmd.ExecuteNonQuery();                    
                }
            }

            Console.WriteLine("Data inserted into table.");
            Console.ReadLine();
        }

        static DataTable GetData()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("Id");
            dt.Columns.Add("Fill1");
            dt.Columns.Add("Fill2");

            dt.Rows.Add(1, "a", "b");
            dt.Rows.Add(2, "c", "d");

            return dt;
        }
    }
}
