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
    class NusizengimaiRepository
    {
        string connectionString = "server=34.91.29.158;user id=root;persistsecurityinfo=True;port=3306;database=lsongulija;password=123456";
        MySqlConnection cnn;



        public DataTable displayNusizengimai()
        {
            DataTable dta = new DataTable();
            try
            {
                cnn = new MySqlConnection(connectionString);//assign connection. The variable cnn, which is of type SqlConnection is used to establish the connection to the database.
                cnn.Open();//open connection. we use the Open method of the cnn variable to open a connection to the database.

                MySqlCommand cmd = new MySqlCommand("Select * from nusizengimai", cnn);//select all from newTestTable

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
        public Nusizengimai InsertNusizengimai(Nusizengimai nusizengimai)//provide transportas object when calling this function
        {
            try
            {
                bool klientasYra = false;

                //setting new SqlConnection, providing connectionString
                cnn = new MySqlConnection(connectionString);
                cnn.Open();//open database

                //check if rezervacija exist
                MySqlCommand cmd = new MySqlCommand("Select * from klientas where Kliento_nr=@Kliento_nr", cnn);//to check if username exist we have to select all items with username
                cmd.Parameters.AddWithValue("@Kliento_nr", nusizengimai.kliento_Nr);

                MySqlDataReader dataReader = cmd.ExecuteReader();//sends SQLCommand.CommandText to the SQLCommand.Connection and builds SqlDataReader
                if ((dataReader.Read() == true))
                {
                    klientasYra = true;
                }
                else
                {
                    return null;
                }
                dataReader.Close();//close data reader when it finishes work
                if (klientasYra == true)
                {
                    MySqlCommand cmd1 = new MySqlCommand("Insert into nusizengimai (NusizData,Kliento_nr) VALUES(@NusizData,@Kliento_nr)", cnn);
                    cmd1.Parameters.AddWithValue("@NusizData", nusizengimai.nusizengimo_Data);
                    cmd1.Parameters.AddWithValue("@Kliento_nr", nusizengimai.kliento_Nr);
                    cmd1.ExecuteNonQuery();
                }

                cnn.Close();
            }
            catch (Exception exc)
            {
                Console.WriteLine(exc);
            }
            return nusizengimai;//return 
        }


        public void UpdateNusizengimai(Nusizengimai nusizengimai)
        {
            try
            {
                //setting new SqlConnection, providing connectionString
                cnn = new MySqlConnection(connectionString);

                //check if user exist
                MySqlCommand cmd = new MySqlCommand("Update nusizengimai SET NusizData=@NusizData, Kliento_nr=@Kliento_nr WHERE NusizengimaiId=@NusizengimaiId", cnn);//to check if username exist we have to select all items with username
                cmd.Parameters.AddWithValue("@NusizData", nusizengimai.nusizengimo_Data);
                cmd.Parameters.AddWithValue("@Kliento_nr", nusizengimai.kliento_Nr);
                cmd.Parameters.AddWithValue("@NusizengimaiId", nusizengimai.nusizengimai_Id);
                cnn.Open();
                cmd.ExecuteNonQuery();
                cnn.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }


        public void DeleteNusiz(Nusizengimai nusizengimai)
        {
            try
            {
                cnn = new MySqlConnection(connectionString);
                
                string newSql = (" Delete from galimi_nusiz where galimi_nusiz.NusizengimaiId=@id;");
                newSql += ("Delete from nusizengimai where nusizengimai.NusizengimaiId=@id");

                cnn.Open();//open connection. we use the Open method of the cnn variable to open a connection to the database.
                MySqlCommand cmd = new MySqlCommand(newSql, cnn);//select all from newTestTable
                cmd.Parameters.AddWithValue("@id", nusizengimai.nusizengimai_Id);
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
