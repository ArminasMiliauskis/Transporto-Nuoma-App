using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TransportoNuoma.Classes;

namespace TransportoNuoma.Repositories
{
    class DraudimoTiekRepository
    {
        string connectionString = "server=34.91.29.158;user id=root;persistsecurityinfo=True;port=3306;database=lsongulija;password=123456";
        MySqlConnection cnn;



        public DataTable displayDraudimoTiekejai()
        {
            DataTable dta = new DataTable();
            try
            {
                cnn = new MySqlConnection(connectionString);//assign connection. The variable cnn, which is of type SqlConnection is used to establish the connection to the database.
                cnn.Open();//open connection. we use the Open method of the cnn variable to open a connection to the database.

                MySqlCommand cmd = new MySqlCommand("Select * from draudimoTiekejai", cnn);//select all from newTestTable

                cmd.ExecuteNonQuery();

                
                MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                adapter.Fill(dta);
            }
            catch (Exception err)
            {
                Console.WriteLine(err.ToString());
            }

            cnn.Close();
            return dta;
        }

        //REGISTER STUDENT
        public DraudimoTiekejai InsertDraudimoTiekejai(DraudimoTiekejai draudimoTiekejai)//provide transportas object when calling this function
        {
            try
            {
               

                //setting new SqlConnection, providing connectionString
                cnn = new MySqlConnection(connectionString);
                cnn.Open();//open database

                //check if transportas exist
                MySqlCommand cmd = new MySqlCommand("Select * from draudimoTiekejai where pavadinimas=@pavadinimas", cnn);
                cmd.Parameters.AddWithValue("@pavadinimas", draudimoTiekejai.pavadinimas);

                MySqlDataReader dataReader = cmd.ExecuteReader();//sends SQLCommand.CommandText to the SQLCommand.Connection and builds SqlDataReader
                if ((dataReader.Read() == true))
                {
                    Console.WriteLine("Tiekejo toks yra");
                    return null;
                }
                else
                {
                    Console.WriteLine("Tiekejo nera. Galima pridet");
                    
                }
                dataReader.Close();//close data reader when it finishes work

                MySqlCommand cmd2 = new MySqlCommand("Insert into draudimoTiekejai (pavadinimas) VALUES(@pavadinimas)", cnn);
                cmd2.Parameters.AddWithValue("@pavadinimas", draudimoTiekejai.pavadinimas);
                cmd2.ExecuteNonQuery();

                cnn.Close();
            }
            catch (Exception exc)
            {
                Console.WriteLine(exc);
            }
            return draudimoTiekejai;//return 
        }


        public void UpdateDraudimoTiekejai(DraudimoTiekejai draudimoTiekejai)
        {
            try
            {
                //setting new SqlConnection, providing connectionString
                cnn = new MySqlConnection(connectionString);

                //check if user exist
                MySqlCommand cmd = new MySqlCommand("Update draudimoTiekejai SET pavadinimas=@pavadinimas WHERE tiekId=@tiekId", cnn);//to check if username exist we have to select all items with username
                cmd.Parameters.AddWithValue("@pavadinimas", draudimoTiekejai.pavadinimas);
                cmd.Parameters.AddWithValue("@tiekId", draudimoTiekejai.tiekejo_Id);
                cnn.Open();
                cmd.ExecuteNonQuery();
                cnn.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }



        public void DeleteDraudTiek(DraudimoTiekejai draudimoTiekejai)
        {
            try
            {
                cnn = new MySqlConnection(connectionString);

                string newSql = ("Delete from draudimas where draudimas.tiekId=@id; ");
                newSql += ("Delete from draudimoTiekejai where draudimoTiekejai.tiekId=@id");

                cnn.Open();//open connection. we use the Open method of the cnn variable to open a connection to the database.
                MySqlCommand cmd = new MySqlCommand(newSql, cnn);//select all from newTestTable
                cmd.Parameters.AddWithValue("@id", draudimoTiekejai.tiekejo_Id);
                cmd.ExecuteNonQuery();//execute function

                cnn.Close();
            }
            catch (Exception exc)
            {
                Console.WriteLine(exc);
            }

        }
    }
}
