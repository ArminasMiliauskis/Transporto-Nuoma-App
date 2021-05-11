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
    class KlientoLokacijaRepository
    {

        string connectionString = "server=34.91.29.158;user id=root;persistsecurityinfo=True;port=3306;database=lsongulija;password=123456";
        MySqlConnection cnn;



        public DataTable displayKlientoLokacija()
        {
            DataTable dta = new DataTable();
            try
            {
                cnn = new MySqlConnection(connectionString);//assign connection. The variable cnn, which is of type SqlConnection is used to establish the connection to the database.
                cnn.Open();//open connection. we use the Open method of the cnn variable to open a connection to the database.

                MySqlCommand cmd = new MySqlCommand("SELECT kliento_lokacija.KlientoLokId,kliento_lokacija.Salis, kliento_lokacija.Miestas, kliento_lokacija.KoordinatesX,kliento_lokacija.KoordinatesY, klientas.Kliento_nr, klientas.Vardas,klientas.Pavarde,klientas.Email  FROM kliento_lokacija INNER JOIN klientas ON kliento_lokacija.Kliento_nr=klientas.Kliento_nr", cnn);//select all from newTestTable

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
        /**
         *  public int kliento_Lok_Id { get; set; }
        public string salis { get; set; }
        public string miestas { get; set; }
        public double koorindatesX { get; set; }
        public double koorindatesY { get; set; }
        public int kliento_Nr { get; set; }
         * 
         */
        public KlientoLokacija GetKlientoLokacija(Klientas klientas)
        {
            KlientoLokacija klientoLokacija = new KlientoLokacija();

            cnn = new MySqlConnection(connectionString);//assign connection. The variable cnn, which is of type SqlConnection is used to establish the connection to the database.
            cnn.Open();//open connection. we use the Open method of the cnn variable to open a connection to the database.

            MySqlCommand cmd = new MySqlCommand("Select * from kliento_lokacija where Kliento_Nr=@Kliento_Nr", cnn);//select all from newTestTable
            cmd.Parameters.AddWithValue("@Kliento_Nr", klientas.klientoNr);
            MySqlDataReader dataReader = cmd.ExecuteReader();
            if ((dataReader.Read() == true))
            {
                int kliento_Lok_Id = int.Parse(dataReader["KlientoLokId"].ToString());
                string salis = dataReader["Salis"].ToString();
                string miestas = dataReader["Miestas"].ToString();
                double koorindatesX = double.Parse(dataReader["KoordinatesX"].ToString());
                double koorindatesY = double.Parse(dataReader["KoordinatesY"].ToString());
                int kliento_Nr = int.Parse(dataReader["Kliento_nr"].ToString());

                klientoLokacija.kliento_Lok_Id = kliento_Lok_Id;
                klientoLokacija.salis = salis;
                klientoLokacija.miestas = miestas;
                klientoLokacija.koorindatesX = koorindatesX;
                klientoLokacija.koorindatesY = koorindatesY;
                klientoLokacija.kliento_Nr = kliento_Nr; 
            }

            return klientoLokacija;
        }
        
        public KlientoLokacija InsertKlientoLokacija(KlientoLokacija klientoLokacija)//provide transportas object when calling this function
        {
            try
            {
                bool klientasYra = false;

                //setting new SqlConnection, providing connectionString
                cnn = new MySqlConnection(connectionString);
                cnn.Open();//open database

                //check if rezervacija exist
                MySqlCommand cmd = new MySqlCommand("Select * from klientas where Kliento_nr=@Kliento_nr", cnn);//to check if username exist we have to select all items with username
                cmd.Parameters.AddWithValue("@Kliento_nr", klientoLokacija.kliento_Nr);

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
                    MySqlCommand cmd1 = new MySqlCommand("Insert into kliento_lokacija (Salis,Miestas,KoordinatesX,KoordinatesY,Kliento_nr) VALUES(@Salis,@Miestas,@KoordinatesX,@KoordinatesY,@Kliento_nr)", cnn);
                    cmd1.Parameters.AddWithValue("@Salis", klientoLokacija.salis);
                    cmd1.Parameters.AddWithValue("@Miestas", klientoLokacija.miestas);
                    cmd1.Parameters.AddWithValue("@KoordinatesX", klientoLokacija.koorindatesX);
                    cmd1.Parameters.AddWithValue("@KoordinatesY", klientoLokacija.koorindatesY);
                    cmd1.Parameters.AddWithValue("@Kliento_nr", klientoLokacija.kliento_Nr);
                    cmd1.ExecuteNonQuery();
                }

                cnn.Close();
            }
            catch (Exception exc)
            {
                Console.WriteLine(exc);
            }
            return klientoLokacija;//return 
        }
        

        public void UpdateKlientoLokacija(KlientoLokacija klientoLokacija)
        {
            try
            {
                //setting new SqlConnection, providing connectionString
                cnn = new MySqlConnection(connectionString);

                //check if user exist
                MySqlCommand cmd = new MySqlCommand("Update kliento_lokacija SET KoordinatesX=@KoordinatesX,KoordinatesY=@KoordinatesY WHERE KlientoLokId=@KlientoLokId", cnn);//to check if username exist we have to select all items with username
                cmd.Parameters.AddWithValue("@KoordinatesX", klientoLokacija.koorindatesX);
                cmd.Parameters.AddWithValue("@KoordinatesY", klientoLokacija.koorindatesY);
                cmd.Parameters.AddWithValue("@KlientoLokId", klientoLokacija.kliento_Lok_Id);
                cnn.Open();
                cmd.ExecuteNonQuery();
                cnn.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }


        public void DeleteKlientoLok(KlientoLokacija klientoLokacija)
        {
            try
            {
                cnn = new MySqlConnection(connectionString);

                string newSql = ("Delete from kliento_lokacija where kliento_lokacija.KlientoLokId=@id;");

                cnn.Open();//open connection. we use the Open method of the cnn variable to open a connection to the database.
                MySqlCommand cmd = new MySqlCommand(newSql, cnn);//select all from newTestTable
                cmd.Parameters.AddWithValue("@id", klientoLokacija.kliento_Lok_Id);
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
