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
    class GalimiNusizengimaiRepository
    {
        string connectionString = "server=34.91.29.158;user id=root;persistsecurityinfo=True;port=3306;database=lsongulija;password=123456";
        MySqlConnection cnn;



        public DataTable displayGalimiNusizengimai()
        {
            DataTable dta = new DataTable();
            try
            {
                cnn = new MySqlConnection(connectionString);//assign connection. The variable cnn, which is of type SqlConnection is used to establish the connection to the database.
                cnn.Open();//open connection. we use the Open method of the cnn variable to open a connection to the database.

                MySqlCommand cmd = new MySqlCommand("SELECT galimi_nusiz.NusizKodas,klientas.Kliento_nr, galimi_nusiz.NusPav, nusizengimai.NusizengimaiId, nusizengimai.NusizData,klientas.Vardas,klientas.Pavarde FROM galimi_nusiz INNER JOIN nusizengimai ON galimi_nusiz.NusizengimaiId=nusizengimai.NusizengimaiId INNER JOIN klientas ON nusizengimai.Kliento_nr=klientas.Kliento_nr", cnn);//select all from newTestTable

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
        public GalimiNusizengimai InsertGalimiNusizengimai(GalimiNusizengimai galimiNusiz)//provide transportas object when calling this function
        {
            try
            {
                //setting new SqlConnection, providing connectionString
                cnn = new MySqlConnection(connectionString);
                cnn.Open();//open database

                MySqlCommand cmd1 = new MySqlCommand("Insert into galimi_nusiz (NusPav,NusizengimaiId) VALUES(@NusPav,@NusizengimaiId)", cnn);
                cmd1.Parameters.AddWithValue("@NusPav", galimiNusiz.nusizengimo_Pavadinimas);
                cmd1.Parameters.AddWithValue("@NusizengimaiId", galimiNusiz.nusizengimo_Id);
                cmd1.ExecuteNonQuery();
                cnn.Close();

            }
            catch (Exception exc)
            {
                Console.WriteLine(exc);
            }
            return galimiNusiz;//return 
        }


        public void UpdateGalimiNusizengimai(GalimiNusizengimai galimiNusiz)
        {
            try
            {
                //setting new SqlConnection, providing connectionString
                cnn = new MySqlConnection(connectionString);

                //check if user exist
                MySqlCommand cmd = new MySqlCommand("Update galimi_nusiz SET NusPav=@NusPav, NusizengimaiId=@NusizengimaiId  WHERE NusizKodas=@NusizKodas", cnn);//to check if username exist we have to select all items with username
                cmd.Parameters.AddWithValue("@NusPav", galimiNusiz.nusizengimo_Pavadinimas);
                cmd.Parameters.AddWithValue("@NusizengimaiId", galimiNusiz.nusizengimo_Id);
                cmd.Parameters.AddWithValue("@NusizKodas", galimiNusiz.nusizengimo_Kodas);
                cnn.Open();
                cmd.ExecuteNonQuery();
                cnn.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }


        public void DeleteGalimiNusiz(GalimiNusizengimai galimiNusiz)
        {
            try
            {
                cnn = new MySqlConnection(connectionString);

                string newSql = ("Delete from galimi_nusiz where galimi_nusiz.NusizKodas=@id ");

                cnn.Open();//open connection. we use the Open method of the cnn variable to open a connection to the database.
                MySqlCommand cmd = new MySqlCommand(newSql, cnn);//select all from newTestTable
                cmd.Parameters.AddWithValue("@id", galimiNusiz.nusizengimo_Kodas);
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
