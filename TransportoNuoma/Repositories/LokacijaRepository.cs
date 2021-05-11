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
    class LokacijaRepository
    {
        string connectionString = "server=34.91.29.158;user id=root;persistsecurityinfo=True;port=3306;database=lsongulija;password=123456";
        MySqlConnection cnn;



        public DataTable displayLokacija()
        {
            DataTable dta = new DataTable();
            try
            {
                cnn = new MySqlConnection(connectionString);//assign connection. The variable cnn, which is of type SqlConnection is used to establish the connection to the database.
                cnn.Open();//open connection. we use the Open method of the cnn variable to open a connection to the database.

                MySqlCommand cmd = new MySqlCommand("SELECT lokacija.Salis, lokacija.Miestas, lokacija.KoordinatesX,lokacija.KoordinatesY, transportas.Trans_Id, transportas.Trans_nr,transportas.Tipas FROM lokacija INNER JOIN transportas ON lokacija.Trans_Id=transportas.Trans_Id", cnn);//select all from newTestTable

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


        public Lokacija getTransportoLokacija(Transportas transportas)
        {
            Lokacija lokacija = new Lokacija();
            try
            {
                cnn = new MySqlConnection(connectionString);//assign connection. The variable cnn, which is of type SqlConnection is used to establish the connection to the database.
                cnn.Open();//open connection. we use the Open method of the cnn variable to open a connection to the database.

                MySqlCommand cmd = new MySqlCommand("Select * from lokacija where Trans_Id=@Trans_Id", cnn);//select all from newTestTable
                cmd.Parameters.AddWithValue("@Trans_Id", transportas.transporto_Id);
                MySqlDataReader dataReader = cmd.ExecuteReader();
                if ((dataReader.Read() == true))
                {
                    int lokacijos_Id = int.Parse(dataReader["lokacijosId"].ToString());
                    string salis = dataReader["Salis"].ToString();
                    string miestas = dataReader["Miestas"].ToString();
                    double koordinatesX = double.Parse(dataReader["KoordinatesX"].ToString());
                    double koordinatesY = double.Parse(dataReader["KoordinatesY"].ToString());
                    int transporto_Id = int.Parse(dataReader["Trans_Id"].ToString());

                    lokacija.transporto_Id = transporto_Id;
                    lokacija.salis = salis;
                    lokacija.miestas = miestas;
                    lokacija.koordinatesX = koordinatesX;
                    lokacija.koordinatesY = koordinatesY;
                    lokacija.transporto_Id = transporto_Id;
                    lokacija.lokacijos_Id = lokacijos_Id;
                    
                }
                dataReader.Close();
                cnn.Close();
                

            }
            catch (Exception e) { Console.WriteLine(e.Message); }

            return lokacija;
        }
        //REGISTER STUDENT
        public Lokacija InsertLokacija(Lokacija lokacija)//provide transportas object when calling this function
        {
            try
            {
                bool transYra = false;

                //setting new SqlConnection, providing connectionString
                cnn = new MySqlConnection(connectionString);
                cnn.Open();//open database

                //check if rezervacija exist
                MySqlCommand cmd = new MySqlCommand("Select * from transportas where Trans_Id=@Trans_Id", cnn);//to check if username exist we have to select all items with username
                cmd.Parameters.AddWithValue("@Trans_Id", lokacija.transporto_Id);

                MySqlDataReader dataReader = cmd.ExecuteReader();//sends SQLCommand.CommandText to the SQLCommand.Connection and builds SqlDataReader
                if ((dataReader.Read() == true))
                {
                    transYra = true;
                }
                else
                {
                    return null;
                }
                dataReader.Close();//close data reader when it finishes work
                if (transYra == true)
                {
                    MySqlCommand cmd1 = new MySqlCommand("Insert into lokacija (Salis,Miestas,KoordinatesX,KoordinatesY,Trans_Id) VALUES(@Salis,@Miestas,@KoordinatesX,@KoordinatesY,@Trans_Id)", cnn);
                    cmd1.Parameters.AddWithValue("@Salis", lokacija.salis);
                    cmd1.Parameters.AddWithValue("@Miestas", lokacija.miestas);
                    cmd1.Parameters.AddWithValue("@KoordinatesX", lokacija.koordinatesX);
                    cmd1.Parameters.AddWithValue("@KoordinatesY", lokacija.koordinatesY);
                    cmd1.Parameters.AddWithValue("@Trans_Id", lokacija.transporto_Id);
                    cmd1.ExecuteNonQuery();
                }

                cnn.Close();
            }
            catch (Exception exc)
            {
                Console.WriteLine(exc);
            }
            return lokacija;//return 
        }


        public void UpdateLokacija(Lokacija lokacija)
        {
            try
            {
                //setting new SqlConnection, providing connectionString
                cnn = new MySqlConnection(connectionString);

                //check if user exist
                MySqlCommand cmd = new MySqlCommand("Update lokacija SET Miestas=@Miestas,KoordinatesX=@KoordinatesX,KoordinatesY=@KoordinatesY WHERE lokacijosId=@lokacijosId", cnn);//to check if username exist we have to select all items with username
                cmd.Parameters.AddWithValue("@Miestas", lokacija.miestas);
                cmd.Parameters.AddWithValue("@KoordinatesX", lokacija.koordinatesX);
                cmd.Parameters.AddWithValue("@KoordinatesY", lokacija.koordinatesY);
                cmd.Parameters.AddWithValue("@lokacijosId", lokacija.lokacijos_Id);
                cnn.Open();
                cmd.ExecuteNonQuery();
                cnn.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }


        public void DeleteLokacija(Lokacija lokacija)
        {
            try
            {
                cnn = new MySqlConnection(connectionString);

                string newSql = ("DELETE lokacija FROM lokacija LEFT JOIN rezervacija ON lokacija.lokacijosId=rezervacija.lokacijosId LEFT JOIN nuoma ON rezervacija.rezId=nuoma.rezId LEFT JOIN apmokejimas ON nuoma.Nuomos_nr=apmokejimas.Nuomos_nr WHERE lokacija.lokacijosId=@id");

                cnn.Open();//open connection. we use the Open method of the cnn variable to open a connection to the database.
                MySqlCommand cmd = new MySqlCommand(newSql, cnn);//select all from newTestTable
                cmd.Parameters.AddWithValue("@id", lokacija.lokacijos_Id);
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
