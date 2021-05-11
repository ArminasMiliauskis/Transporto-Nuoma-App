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
    class GalimiTestaiRepository
    {
        string connectionString = "server=34.91.29.158;user id=root;persistsecurityinfo=True;port=3306;database=lsongulija;password=123456";
        MySqlConnection cnn;



        public DataTable displayGalimiTestai()
        {
            DataTable dta = new DataTable();
            try
            {
                cnn = new MySqlConnection(connectionString);//assign connection. The variable cnn, which is of type SqlConnection is used to establish the connection to the database.
                cnn.Open();//open connection. we use the Open method of the cnn variable to open a connection to the database.

                MySqlCommand cmd = new MySqlCommand("SELECT galimi_test.Testo_kodas,galimi_test.Test_pav, galimi_test.TestId, trans_test.Test_data, trans_test.Trans_Id, transportas.Trans_nr,transportas.Tipas FROM galimi_test INNER JOIN trans_test ON galimi_test.TestId=trans_test.TestId INNER JOIN transportas ON trans_test.Trans_Id=transportas.Trans_Id", cnn);//select all from newTestTable

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
        public GalimiTestai InsertGalimiTestai(GalimiTestai galimiTestai)//provide transportas object when calling this function
        {
            try
            {


                //setting new SqlConnection, providing connectionString
                cnn = new MySqlConnection(connectionString);
                cnn.Open();//open database

                MySqlCommand cmd1 = new MySqlCommand("Insert into galimi_test (Test_pav,TestId) VALUES(@Test_pav,@TestId)", cnn);
                cmd1.Parameters.AddWithValue("@Test_pav", galimiTestai.testPav);
                cmd1.Parameters.AddWithValue("@TestId", galimiTestai.testId);
                cmd1.ExecuteNonQuery();
                cnn.Close();

            }
            catch (Exception exc)
            {
                Console.WriteLine(exc);
            }
            return galimiTestai;//return 
        }


        public void UpdateGalimiTestai(GalimiTestai galimiTestai)
        {
            try
            {
                //setting new SqlConnection, providing connectionString
                cnn = new MySqlConnection(connectionString);

                //check if user exist
                MySqlCommand cmd = new MySqlCommand("Update galimi_test SET Test_pav=@Test_pav, TestId=@TestId  WHERE Testo_kodas=@Testo_kodas", cnn);//to check if username exist we have to select all items with username
                cmd.Parameters.AddWithValue("@Test_pav", galimiTestai.testPav);
                cmd.Parameters.AddWithValue("@TestId", galimiTestai.testId);
                cmd.Parameters.AddWithValue("@Testo_kodas", galimiTestai.testoKodas);
                cnn.Open();
                cmd.ExecuteNonQuery();
                cnn.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }


        public void DeleteGalimiTestai(GalimiTestai galimiTestai)
        {
            try
            {
                cnn = new MySqlConnection(connectionString);

                string newSql = ("Delete from galimi_test where galimi_test.Testo_kodas=@id ");

                cnn.Open();//open connection. we use the Open method of the cnn variable to open a connection to the database.
                MySqlCommand cmd = new MySqlCommand(newSql, cnn);//select all from newTestTable
                cmd.Parameters.AddWithValue("@id", galimiTestai.testoKodas);
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
