using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace Exception_test
{
    class Program
    {
        static void Main(string[] args)
        {
            SqlConnection connection = null;
            string connectionString;
            string SQL;
            SqlCommand command = null;
            SqlDataReader dataReader;

            try
            {
                connectionString = "Data Source=ELEV\\SQLEXPRESS;Initial Catalog=NORTHWIND;Integrated Security=SSPI";
                connection = new SqlConnection(connectionString);
                connection.Open();

                SQL = "SELECT ID, ProductName FROM Products ";
                command = new SqlCommand(SQL, connection);

                dataReader = command.ExecuteReader();

                connection.Close();

                string text = "aa";
                int tal = System.Convert.ToInt32(text);

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            finally
            {
                command.Dispose();

                if (connection.State == System.Data.ConnectionState.Open)
                {
                    connection.Close();
                }
            }

        }
    }
}
