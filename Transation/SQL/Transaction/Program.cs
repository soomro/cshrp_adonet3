using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
namespace Transaction
{
    class Program
    {
        static void Main(string[] args)
        {
            SqlConnection connection;
            string connectionString = "Data Source=ELEV\\MSSQLSERVER2014;Initial Catalog=NORTHWND;Integrated Security=SSPI";
            connection = new SqlConnection(connectionString);
            connection.Open();

            SqlTransaction transaction = connection.BeginTransaction("transaction");

            SqlCommand command = connection.CreateCommand();
            command.Transaction = transaction;

            try
            {
                command.CommandText = "INSERT INTO Categories (CategoryName, Description) VALUES ('JB_kategori', 'Beskrivning') SET @newId = SCOPE_IDENTITY();";
                command.Parameters.Add("@newId", SqlDbType.Int).Direction = ParameterDirection.Output;
                command.ExecuteScalar();
                int id = (int)command.Parameters["@newId"].Value;

                command.CommandText = "INSERT INTO Products (ProductName, CategoryID, Discontinued) VALUES ('JB_produkt', " + id + ", 0)";
                command.ExecuteNonQuery();

                transaction.Commit();
                Console.WriteLine("OK! Commit!");

            }
            catch(Exception ex)
            {
                // rollback
                transaction.Rollback();
                Console.WriteLine("ERROR! Rollback!");
                Console.WriteLine(ex.Message);
            }

            command.Dispose();
            connection.Close();

            Console.ReadLine();

        }
    }
}
