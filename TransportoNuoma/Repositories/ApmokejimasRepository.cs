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
    class ApmokejimasRepository
    {
        string connectionString = "server=34.91.29.158;user id=root;persistsecurityinfo=True;port=3306;database=lsongulija;password=123456";
        MySqlConnection cnn;



        public DataTable displayApmokejimas()
        {
            DataTable dta = new DataTable();
            try
            {
                cnn = new MySqlConnection(connectionString);//assign connection. The variable cnn, which is of type SqlConnection is used to establish the connection to the database.
                cnn.Open();//open connection. we use the Open method of the cnn variable to open a connection to the database.

                MySqlCommand cmd = new MySqlCommand("SELECT apmokejimas.apmok_nr,klientas.Kliento_nr,apmokejimas.Saskaitos_nr, apmokejimas.Apmokejimo_suma, apmokejimas.Apmok_data, nuoma.Nuomos_nr, nuoma.NuomosPrData, nuoma.NuomosPabData, klientas.Email FROM apmokejimas INNER JOIN nuoma ON apmokejimas.Nuomos_nr=nuoma.Nuomos_nr INNER JOIN klientas ON nuoma.Kliento_nr=klientas.Kliento_nr", cnn);//select all from newTestTable

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
        public Apmokejimas InsertApmokejimas(Apmokejimas apmokejimas)//provide transportas object when calling this function
        {
            try
            {
                bool nuomaYra = false;
                
                //setting new SqlConnection, providing connectionString
                cnn = new MySqlConnection(connectionString);
                cnn.Open();//open database

                //check if transportas exist
                MySqlCommand cmd = new MySqlCommand("Select * from nuoma where Nuomos_nr=@Nuomos_nr", cnn);
                cmd.Parameters.AddWithValue("@Nuomos_nr", apmokejimas.nuomos_Nr);

                MySqlDataReader dataReader = cmd.ExecuteReader();//sends SQLCommand.CommandText to the SQLCommand.Connection and builds SqlDataReader
                if ((dataReader.Read() == true))
                {
                    nuomaYra = true;
                }
                else
                {
                    Console.WriteLine("Tiekejo nera. Galima pridet");
                    return null;

                }
                dataReader.Close();//close data reader when it finishes work

                if(nuomaYra == true)
                {
                    MySqlCommand cmd2 = new MySqlCommand("Insert into apmokejimas (Saskaitos_nr,Apmokejimo_suma,Nuomos_nr,Apmok_data) VALUES(@Saskaitos_nr,@Apmokejimo_suma,@Nuomos_nr,@Apmok_data)", cnn);
                    cmd2.Parameters.AddWithValue("@Saskaitos_nr", apmokejimas.saskaitos_Nr);
                    cmd2.Parameters.AddWithValue("@Apmokejimo_suma", apmokejimas.apmokejimo_Suma);
                    cmd2.Parameters.AddWithValue("@Nuomos_nr", apmokejimas.nuomos_Nr);
                    cmd2.Parameters.AddWithValue("@Apmok_data", apmokejimas.apmok_data);
                    cmd2.ExecuteNonQuery();

                    cnn.Close();
                }

                
            }
            catch (Exception exc)
            {
                Console.WriteLine(exc);
            }
            return apmokejimas;//return 
        } 


        public void UpdateApmokejimas(Apmokejimas apmokejimas)
        {
            try
            {
                //setting new SqlConnection, providing connectionString
                cnn = new MySqlConnection(connectionString);

                //check if user exist
                MySqlCommand cmd = new MySqlCommand("Update apmokejimas SET Apmok_data=@Apmok_data WHERE apmok_nr=@apmok_nr", cnn);//to check if username exist we have to select all items with username
                cmd.Parameters.AddWithValue("@Apmok_data", apmokejimas.apmok_data);
                cmd.Parameters.AddWithValue("@apmok_nr", apmokejimas.apmok_Id);
                cnn.Open();
                cmd.ExecuteNonQuery();
                cnn.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }


        public void DeleteApmokejimas(Apmokejimas apmokejima)
        {
            try
            {
                cnn = new MySqlConnection(connectionString);

                string newSql = ("Delete from apmokejimas where apmokejimas.apmok_nr=@id;");

                cnn.Open();//open connection. we use the Open method of the cnn variable to open a connection to the database.
                MySqlCommand cmd = new MySqlCommand(newSql, cnn);//select all from newTestTable
                cmd.Parameters.AddWithValue("@id", apmokejima.apmok_Id);
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
