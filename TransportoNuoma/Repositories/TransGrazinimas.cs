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
    class TransGrazinimas
    {
        string connectionString = "server=34.91.29.158;user id=root;persistsecurityinfo=True;port=3306;database=lsongulija;password=123456";
        MySqlConnection cnn;



        public void displayTransGrazinimas()
        {
            try
            {
                cnn = new MySqlConnection(connectionString);//assign connection. The variable cnn, which is of type SqlConnection is used to establish the connection to the database.
                cnn.Open();//open connection. we use the Open method of the cnn variable to open a connection to the database.

                MySqlCommand cmd = new MySqlCommand("Select * from trans_grazinimas", cnn);//select all from newTestTable

                cmd.ExecuteNonQuery();

                DataTable dta = new DataTable();
                MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                adapter.Fill(dta);
            }
            catch (Exception err)
            {
                Console.WriteLine(err.ToString());
            }

            cnn.Close();
            Console.WriteLine("Connection Closed. Press any key to exit...");
            Console.Read();
        }

        //REGISTER STUDENT
        public TransportoGrazinimas InsertTransGrazinimas(TransportoGrazinimas transportoGrazinimas)//provide transportas object when calling this function
        {
            try
            {
                bool nuomaYra = false;

                //setting new SqlConnection, providing connectionString
                cnn = new MySqlConnection(connectionString);
                cnn.Open();//open database

                //check if rezervacija exist
                MySqlCommand cmd = new MySqlCommand("Select * from nuoma where Nuomos_nr=@Nuomos_nr", cnn);//to check if username exist we have to select all items with username
                cmd.Parameters.AddWithValue("@Nuomos_nr", transportoGrazinimas.nuomos_Nr);

                MySqlDataReader dataReader = cmd.ExecuteReader();//sends SQLCommand.CommandText to the SQLCommand.Connection and builds SqlDataReader
                if ((dataReader.Read() == true))
                {
                    nuomaYra = true;
                }
                else
                {
                    return null;
                }
                dataReader.Close();//close data reader when it finishes work
                if (nuomaYra == true)
                {
                    MySqlCommand cmd1 = new MySqlCommand("Insert into trans_grazinimas (Grazinimo_data,Grazinimo_laikas) VALUES(@Grazinimo_data,@Grazinimo_laikas)", cnn);
                    cmd1.Parameters.AddWithValue("@Grazinimo_data", transportoGrazinimas.grazinimo_Data);
                    cmd1.Parameters.AddWithValue("@Grazinimo_laikas", transportoGrazinimas.grazinimo_Laikas);
                    cmd1.ExecuteNonQuery();
                }

                cnn.Close();
            }
            catch (Exception exc)
            {
                Console.WriteLine(exc);
            }
            return transportoGrazinimas;//return 
        }


        public void UpdateTransGrazinimas(TransportoGrazinimas transportoGrazinimas)
        {
            try
            {
                //setting new SqlConnection, providing connectionString
                cnn = new MySqlConnection(connectionString);
                cnn.Open();//open database

                //check if user exist
                MySqlCommand cmd = new MySqlCommand("Update trans_grazinimas SET Grazinimo_data=@Grazinimo_data, Grazinimo_laikas=@Grazinimo_laikas,Nuomos_nr=@Nuomos_nr WHERE Graz_nr=@Graz_nr", cnn);//to check if username exist we have to select all items with username
                cmd.Parameters.AddWithValue("@Grazinimo_data", transportoGrazinimas.grazinimo_Data);
                cmd.Parameters.AddWithValue("@Grazinimo_laikas", transportoGrazinimas.grazinimo_Laikas);
                cmd.Parameters.AddWithValue("@Nuomos_nr", transportoGrazinimas.nuomos_Nr);
                cmd.Parameters.AddWithValue("@Graz_nr", transportoGrazinimas.graz_Nr);
                cnn.Open();
                cmd.ExecuteNonQuery();
                cnn.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }
    }
}
