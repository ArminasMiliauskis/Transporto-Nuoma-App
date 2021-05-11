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
    class DraudimasRepository
    {
        string connectionString = "server=34.91.29.158;user id=root;persistsecurityinfo=True;port=3306;database=lsongulija;password=123456";
        MySqlConnection cnn;



        public DataTable displayDraudimas()
        {
            DataTable dta = new DataTable();
            try
            {
                cnn = new MySqlConnection(connectionString);//assign connection. The variable cnn, which is of type SqlConnection is used to establish the connection to the database.
                cnn.Open();//open connection. we use the Open method of the cnn variable to open a connection to the database.

                MySqlCommand cmd = new MySqlCommand("SELECT draudimas.draudId,draudimas.draudPradData, draudimas.draudPabData,draudimoTiekejai.tiekId, draudimoTiekejai.pavadinimas,transportas.Trans_Id, transportas.Trans_nr,transportas.Tipas FROM draudimas INNER JOIN draudimoTiekejai ON draudimas.tiekId=draudimoTiekejai.tiekId INNER JOIN transportas ON draudimas.Trans_Id=transportas.Trans_Id", cnn);//select all from newTestTable

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
        public Draudimas InsertDraudimas(Draudimas draudimas)//provide transportas object when calling this function
        {
            try
            {
                bool transYra = false;
                bool draudimoTiekYra = false;

                //setting new SqlConnection, providing connectionString
                cnn = new MySqlConnection(connectionString);
                cnn.Open();//open database

                //check if transportas exist
                MySqlCommand cmd = new MySqlCommand("Select * from transportas where Trans_Id=@Trans_Id", cnn);
                cmd.Parameters.AddWithValue("@Trans_Id", draudimas.Trans_Id);

                MySqlDataReader dataReader = cmd.ExecuteReader();//sends SQLCommand.CommandText to the SQLCommand.Connection and builds SqlDataReader
                if ((dataReader.Read() == true))
                {
                    transYra = true;
                    Console.WriteLine("Transportas toks yra");
                }
                else
                {
                    Console.WriteLine("Tokio transporto nera");
                    return null;
                }
                dataReader.Close();//close data reader when it finishes work



                //check if draudimoTiekejas exist
                MySqlCommand cmd1 = new MySqlCommand("Select * from draudimoTiekejai where tiekId=@tiekId", cnn);
                cmd1.Parameters.AddWithValue("@tiekId", draudimas.tiekId);

                MySqlDataReader dataReader1 = cmd1.ExecuteReader();//sends SQLCommand.CommandText to the SQLCommand.Connection and builds SqlDataReader
                if ((dataReader1.Read() == true))
                {
                    draudimoTiekYra = true;
                    Console.WriteLine("Transportas toks yra");
                }
                else
                {
                    Console.WriteLine("Tokio transporto nera");
                    return null;
                }
                dataReader1.Close();


                if (transYra == true && draudimoTiekYra==true)
                {
                    MySqlCommand cmd2 = new MySqlCommand("Insert into draudimas (draudPradData,draudPabData,tiekId,Trans_Id) VALUES(@draudPradData,@draudPabData,@tiekId,@Trans_Id)", cnn);
                    cmd2.Parameters.AddWithValue("@draudPradData", draudimas.draudPradData);
                    cmd2.Parameters.AddWithValue("@draudPabData", draudimas.draudPabData);
                    cmd2.Parameters.AddWithValue("@tiekId", draudimas.tiekId);
                    cmd2.Parameters.AddWithValue("@Trans_Id", draudimas.Trans_Id);
                    cmd2.ExecuteNonQuery();
                }

                cnn.Close();
            }
            catch (Exception exc)
            {
                Console.WriteLine(exc);
            }
            return draudimas;//return 
        }


        public void UpdateDraudimas(Draudimas draudimas)
        {
            try
            {
                //setting new SqlConnection, providing connectionString
                cnn = new MySqlConnection(connectionString);

                //check if user exist
                MySqlCommand cmd = new MySqlCommand("Update draudimas SET draudPradData=@draudPradData,draudPabData=@draudPabData,tiekId=@tiekId,Trans_Id=@Trans_Id WHERE draudId=@draudId", cnn);//to check if username exist we have to select all items with username
                cmd.Parameters.AddWithValue("@draudPradData", draudimas.draudPradData);
                cmd.Parameters.AddWithValue("@draudPabData", draudimas.draudPabData);
                cmd.Parameters.AddWithValue("@tiekId", draudimas.tiekId);
                cmd.Parameters.AddWithValue("@Trans_Id", draudimas.Trans_Id);
                cmd.Parameters.AddWithValue("@draudId", draudimas.draudId);
                cnn.Open();
                cmd.ExecuteNonQuery();
                cnn.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }



        public void DeleteDraud(Draudimas draudimas)
        {
            try
            {
                cnn = new MySqlConnection(connectionString);

                string newSql = ("Delete from draudimas where draudimas.draudId=@id");

                cnn.Open();//open connection. we use the Open method of the cnn variable to open a connection to the database.
                MySqlCommand cmd = new MySqlCommand(newSql, cnn);//select all from newTestTable
                cmd.Parameters.AddWithValue("@id", draudimas.draudId);
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
