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
            int menuSelectedValue = 0;

            string connectionString = "Data Source=ELEV\\MSSQLSERVER2014;Initial Catalog=NORTHWND;Integrated Security=SSPI";
            //connection = new SqlConnection(connetionString);
            //connection.Open();

            string SQL;
            SqlCommand command;
            SqlDataReader dataReader;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                //
                // Koppla till databas
                //

                connection.Open();

                while (menuSelectedValue != 5)
                {
                    // menyval
                    menuSelectedValue = GetInputFromMenu();

                    switch (menuSelectedValue)
                    {
                        case -1:

                            //
                            // I funktionen som visar menyn känner vi av om man matat in en siffra
                            // Om det inte är en siffra så skickar vi tillbaka -1
                            //
                            Console.WriteLine("Skriv in en siffra!");
                            break;

                        case 1:

                            //
                            // Visa alla
                            //
                            SQL = "SELECT ProductID, ProductName FROM Products ";
                            command = new SqlCommand(SQL, connection);

                            dataReader = command.ExecuteReader();
                            while (dataReader.Read())
                            {
                                //Console.WriteLine(dataReader.GetValue(0));
                                Console.WriteLine(dataReader["ProductID"].ToString() + " " + dataReader["ProductName"].ToString());
                            }
                            dataReader.Close();
                            command.Dispose();
                            break;

                        case 2:
                            //
                            // Lägg till
                            //

                            break;

                        case 3:
                            //
                            // Uppdatera
                            //

                            break;

                        case 4:
                            //
                            // Ta bort
                            //
                            break;

                    }
                    Console.WriteLine("-----------------\n");
                }

                connection.Close();
            }
        }

        /// <summary>
        /// Visar menyn och returnerar ett menyval
        /// </summary>
        /// <returns>Returnerar -1 om ingen siffra</returns>
        static int GetInputFromMenu()
        {
            Console.WriteLine("1. Visa all");
            Console.WriteLine("2. Lägg till");
            Console.WriteLine("3. Uppdatera");
            Console.WriteLine("4. Ta bort");
            Console.WriteLine("5. Avsluta");

            int menuSelectedValue;
            string inputText = Console.ReadLine();
            bool parseOK = int.TryParse(inputText, out menuSelectedValue);
            if (parseOK)
            {
                return menuSelectedValue;
            }
            else
            {
                return -1;
            }
            
        }
    }
}
