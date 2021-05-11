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
    class Markes
    {
        string connectionString = "server=34.91.29.158;user id=root;persistsecurityinfo=True;port=3306;database=lsongulija;password=123456";
        MySqlConnection cnn;



        public DataTable displayMarkes()
        {
            DataTable dta = new DataTable();
            try
            {
                cnn = new MySqlConnection(connectionString);//assign connection. The variable cnn, which is of type SqlConnection is used to establish the connection to the database.
                cnn.Open();//open connection. we use the Open method of the cnn variable to open a connection to the database.

                MySqlCommand cmd = new MySqlCommand("Select * from tmarke", cnn);//select all from newTestTable

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
        public TransportoMarke InsertMarke(TransportoMarke marke)//provide transportas object when calling this function
        {
            try
            {


                //setting new SqlConnection, providing connectionString
                cnn = new MySqlConnection(connectionString);
                cnn.Open();//open database

                //check if user exist
                MySqlCommand cmd = new MySqlCommand("Select * from tmarke where Markes_pav=@Markes_pav", cnn);//to check if username exist we have to select all items with username
                cmd.Parameters.AddWithValue("@Markes_pav", marke.markes_Pav);
                MySqlDataReader dataReader = cmd.ExecuteReader();
                if ((dataReader.Read() == true))
                {
                    Console.WriteLine("Transport with that Marke already exists");
                    return null;
                }
                else
                {
                    Console.WriteLine("Transport Marke is free so you can register");
                }
                dataReader.Close();//close data reader when it finishes work

                MySqlCommand cmd1 = new MySqlCommand("Insert into tmarke (Markes_pav) VALUES(@Markes_pav)", cnn);
                cmd1.Parameters.AddWithValue("@Markes_pav", marke.markes_Pav);
                cmd1.ExecuteNonQuery();
                cnn.Close();

            }
            catch (Exception exc)
            {
                Console.WriteLine(exc);
            }
            return marke;//return 
        }


        public void UpdateMarke(TransportoMarke marke)
        {
            try
            {
                //setting new SqlConnection, providing connectionString
                cnn = new MySqlConnection(connectionString);

                //check if user exist
                MySqlCommand cmd = new MySqlCommand("Update tmarke SET Markes_pav=@Markes_pav WHERE MarkesId=@MarkesId", cnn);//to check if username exist we have to select all items with username
                cmd.Parameters.AddWithValue("@Markes_pav", marke.markes_Pav);
                cmd.Parameters.AddWithValue("@MarkesId", marke.markes_Id);
                cnn.Open();
                cmd.ExecuteNonQuery();
                cnn.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }


        public void DeleteMarke(TransportoMarke marke)
        {
            try
            {
                cnn = new MySqlConnection(connectionString);

                string newSql = "DELETE FROM tmarke WHERE tmarke.MarkesId=@id;";

                cnn.Open();//open connection. we use the Open method of the cnn variable to open a connection to the database.
                MySqlCommand cmd = new MySqlCommand(newSql, cnn);//select all from newTestTable
                cmd.Parameters.AddWithValue("@id", marke.markes_Id);
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
