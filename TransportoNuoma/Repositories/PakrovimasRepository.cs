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
    class PakrovimasRepository
    {

        string connectionString = "server=34.91.29.158;user id=root;persistsecurityinfo=True;port=3306;database=lsongulija;password=123456";
        MySqlConnection cnn;



        public DataTable displayPakrovimas()
        {
            DataTable dta = new DataTable();
            try
            {
                cnn = new MySqlConnection(connectionString);//assign connection. The variable cnn, which is of type SqlConnection is used to establish the connection to the database.
                cnn.Open();//open connection. we use the Open method of the cnn variable to open a connection to the database.

                MySqlCommand cmd = new MySqlCommand("SELECT pakrovimas.pakrovimoNr, pakrovimas.PakrovimoDydis, pakrovimas.Trans_Id, transportas.Trans_nr,transportas.Tipas,transportas.Spalva, transportas.QrKodas FROM pakrovimas INNER JOIN transportas ON pakrovimas.Trans_Id=transportas.Trans_Id", cnn);//select all from newTestTable

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
        public Pakrovimas InsertPakrovimas(Pakrovimas pakrovimas)//provide transportas object when calling this function
        {
            try
            {
                Console.WriteLine("TransId: " + pakrovimas.transporto_Id + "   PakrovDydis: " + pakrovimas.pakrovimo_Dydis);
                
                //setting new SqlConnection, providing connectionString
                cnn = new MySqlConnection(connectionString);
                


                MySqlCommand cmd = new MySqlCommand("Insert into pakrovimas(PakrovimoDydis,Trans_Id) VALUES(@PakrovimoDydis,@Trans_Id)", cnn);
                cmd.Parameters.AddWithValue("@PakrovimoDydis", pakrovimas.pakrovimo_Dydis);
                cmd.Parameters.AddWithValue("@Trans_Id", pakrovimas.transporto_Id);
                cnn.Open();//open database
                cmd.ExecuteNonQuery();
                cnn.Close();
            }
            catch (Exception exc)
            {
                Console.WriteLine(exc);
            }
            return pakrovimas;//return 
        }


        public void UpdatePakrovimas(Pakrovimas pakrovimas)
        {
            try
            {
                //setting new SqlConnection, providing connectionString
                cnn = new MySqlConnection(connectionString);

                //check if user exist
                MySqlCommand cmd = new MySqlCommand("Update pakrovimas SET PakrovimoDydis=@PakrovimoDydis, Trans_Id=@Trans_Id WHERE pakrovimoNr=@pakrovimoNr", cnn);//to check if username exist we have to select all items with username
                cmd.Parameters.AddWithValue("@PakrovimoDydis", pakrovimas.pakrovimo_Dydis);
                cmd.Parameters.AddWithValue("@Trans_Id", pakrovimas.transporto_Id);
                cmd.Parameters.AddWithValue("@pakrovimoNr", pakrovimas.pakrovimo_Nr);
                cnn.Open();
                cmd.ExecuteNonQuery();
                cnn.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }


        public void DeletePakrovimas(Pakrovimas pakrovimas)
        {
            try
            {
                cnn = new MySqlConnection(connectionString);

                string newSql = ("Delete from pakrovimas where pakrovimas.pakrovimoNr=@id ");

                cnn.Open();//open connection. we use the Open method of the cnn variable to open a connection to the database.
                MySqlCommand cmd = new MySqlCommand(newSql, cnn);//select all from newTestTable
                cmd.Parameters.AddWithValue("@id", pakrovimas.pakrovimo_Nr);
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
