using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SQL
{
    class Program
    {
        static void Main(string[] args)
        {

            try
            {
                //
                // Koppla till databas
                // connect to database
                //
                SqlConnection connection;
                string connetionString = "Data Source=ELEV\\MSSQLSERVER2014;Initial Catalog=NORTHWND;Integrated Security=SSPI";
                connection = new SqlConnection(connetionString);
                connection.Open();

                //
                // Hämta alla Customers
                // fetch all Customers
                //
                string SQL_read = "SELECT CompanyName, Phone FROM Customers ";
                SqlCommand command_read = new SqlCommand(SQL_read, connection);

                SqlDataReader dataReader = command_read.ExecuteReader();
                while (dataReader.Read())
                {
                    //Console.WriteLine(dataReader.GetValue(0));
                    Console.WriteLine(dataReader["CompanyName"].ToString() + " " + dataReader["Phone"].ToString());
                }
                dataReader.Close();
                command_read.Dispose();

                Console.ReadLine();





                //
                // Lägg till en kategori
                // Add new category
                //
                string catName = "Kat1";
                string DescName = "DescName1";

                string SQL_add = "INSERT INTO Categories (CategoryName, Description) VALUES ('" + catName + "', '" + DescName + "')";
                SqlCommand command_add = new SqlCommand(SQL_add, connection);
                int result = command_add.ExecuteNonQuery();
                command_add.Dispose();

                Console.ReadLine();


                //
                // Lägg till en kategori med Parametrar
                // add new catagory with parameters
                //
                string SQL_add2 = "INSERT INTO Categories (CategoryName, Description) VALUES (@CategoryName, @Description)";
                SqlCommand command_add2 = new SqlCommand(SQL_add2, connection);
                command_add2.Parameters.AddWithValue("@CategoryName", "CatName1");
                command_add2.Parameters.AddWithValue("@Description", "DescName1");
                command_add2.ExecuteNonQuery();
                command_add2.Dispose();

                Console.ReadLine();

                //
                // Update
                //

                string SQL_update = "Update Categories SET CategoryName = @CategoryName WHERE CategoryID = 1";
                SqlCommand command_update = new SqlCommand(SQL_update, connection);
                command_update.Parameters.AddWithValue("@CategoryName", "Ny kategori");
                command_update.ExecuteNonQuery();
                command_update.Dispose();

                //
                // Delete
                //

                string SQL_delete = "DELETE FROM Categories WHERE CategoryID = 24";
                SqlCommand command_delete = new SqlCommand(SQL_delete, connection);
                int reultat_delete = command_delete.ExecuteNonQuery();
                command_delete.Dispose();

                connection.Close();

                Console.ReadLine();

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            Console.ReadLine();

        }
    }
}
