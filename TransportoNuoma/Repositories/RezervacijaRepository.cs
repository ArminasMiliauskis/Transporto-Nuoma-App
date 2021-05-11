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
    class RezervacijaRepository
    {
        string connectionString = "server=34.91.29.158;user id=root;persistsecurityinfo=True;port=3306;database=lsongulija;password=123456";
        MySqlConnection cnn;
        MySqlConnection cnn1;



        public DataTable displayRezervacija()
        {
            DataTable dta = new DataTable();
            try
            {
                cnn = new MySqlConnection(connectionString);//assign connection. The variable cnn, which is of type SqlConnection is used to establish the connection to the database.
                cnn.Open();//open connection. we use the Open method of the cnn variable to open a connection to the database.

                MySqlCommand cmd = new MySqlCommand("SELECT rezervacija.rezId, rezervacija.rezData, rezervacija.rezPrad, rezervacija.rezPab,klientas.Kliento_nr,klientas.Vardas,klientas.Pavarde," +
                    " transportas.Trans_Id,transportas.Trans_nr,lokacija.lokacijosId,lokacija.KoordinatesX,lokacija.KoordinatesY FROM rezervacija INNER JOIN klientas ON rezervacija.Kliento_nr=klientas.Kliento_nr" +
                    " INNER JOIN transportas ON rezervacija.Trans_Id=transportas.Trans_Id INNER JOIN lokacija ON rezervacija.lokacijosId=lokacija.lokacijosId", cnn);//select all from newTestTable

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
        public (Rezervacija, bool) InsertRezervacija(Rezervacija rezervacija)//provide transportas object when calling this function
        {
            try
            {


                //setting new SqlConnection, providing connectionString
                cnn = new MySqlConnection(connectionString);
                cnn.Open();//open database

                //check if rezervacija exist
                MySqlCommand cmd = new MySqlCommand("SELECT * FROM rezervacija WHERE rezId=( SELECT MAX(rezId) FROM rezervacija WHERE Trans_Id=@Trans_Id);", cnn);//to check if username exist we have to select all items with username
                cmd.Parameters.AddWithValue("@Trans_Id", rezervacija.Transporto_Id);

                MySqlDataReader dataReader = cmd.ExecuteReader();//sends SQLCommand.CommandText to the SQLCommand.Connection and builds SqlDataReader
                if ((dataReader.Read() == true) && TimeSpan.Parse(dataReader["rezPab"].ToString()) >= DateTime.Now.TimeOfDay && DateTime.Parse(dataReader["rezData"].ToString()) >= DateTime.Today)
                {

                    if (TimeSpan.Parse(dataReader["rezPab"].ToString()) >= DateTime.Now.TimeOfDay)
                    {
                        Console.WriteLine("Transport is already under reservation");
                        return (null, false);
                    }

                }
                else
                {
                    Console.WriteLine("Transport is free so you can register");
                }
                dataReader.Close();//close data reader when it finishes work

                MySqlCommand cmd1 = new MySqlCommand("Insert into rezervacija (rezData,rezPrad,rezPab,Kliento_nr,Trans_Id,lokacijosId) VALUES(@rezData,@rezPrad,@rezPab,@Kliento_nr,@Trans_Id,@lokacijosId)", cnn);
                cmd1.Parameters.AddWithValue("@rezData", rezervacija.rezervacijos_Data);
                cmd1.Parameters.AddWithValue("@rezPrad", rezervacija.rezervacijosPrad);
                cmd1.Parameters.AddWithValue("@rezPab", rezervacija.rezervacijosPab);
                cmd1.Parameters.AddWithValue("@Kliento_nr", rezervacija.kliento_Id);
                cmd1.Parameters.AddWithValue("@Trans_Id", rezervacija.Transporto_Id);
                cmd1.Parameters.AddWithValue("@lokacijosId", rezervacija.lokacijos_Id);
                cmd1.ExecuteNonQuery();
                cnn.Close();
                Console.WriteLine("Rezervation completed succesfuly");



            }
            catch (Exception exc)
            {
                Console.WriteLine(exc);
            }

            return (rezervacija, true);//return 
        }




        //REGISTER STUDENT
        public Rezervacija InsertRezervacijaAdmin(Rezervacija rezervacija)//provide transportas object when calling this function
        {
            try
            {


                //setting new SqlConnection, providing connectionString
                cnn = new MySqlConnection(connectionString);
                cnn.Open();//open database

                //check if rezervacija exist
                MySqlCommand cmd = new MySqlCommand("Select * from rezervacija where Trans_Id=@Trans_Id", cnn);//to check if username exist we have to select all items with username
                cmd.Parameters.AddWithValue("@Trans_Id", rezervacija.Transporto_Id);

                MySqlDataReader dataReader = cmd.ExecuteReader();//sends SQLCommand.CommandText to the SQLCommand.Connection and builds SqlDataReader
                if (dataReader.Read() == true)
                {
                    Console.WriteLine("Transport is already under reservation");
                    return null;
                }
                else
                {
                    Console.WriteLine("Transport is free so you can register");
                }
                dataReader.Close();//close data reader when it finishes work

                MySqlCommand cmd1 = new MySqlCommand("Insert into rezervacija (rezData,rezPrad,rezPab,Kliento_nr,Trans_Id,lokacijosId) VALUES(@rezData,@rezPrad,@rezPab,@Kliento_nr,@Trans_Id,@lokacijosId)", cnn);
                cmd1.Parameters.AddWithValue("@rezData", rezervacija.rezervacijos_Data);
                cmd1.Parameters.AddWithValue("@rezPrad", rezervacija.rezervacijosPrad);
                cmd1.Parameters.AddWithValue("@rezPab", rezervacija.rezervacijosPab);
                cmd1.Parameters.AddWithValue("@Kliento_nr", rezervacija.kliento_Id);
                cmd1.Parameters.AddWithValue("@Trans_Id", rezervacija.Transporto_Id);
                cmd1.Parameters.AddWithValue("@lokacijosId", rezervacija.lokacijos_Id);
                cmd1.ExecuteNonQuery();
                cnn.Close();

            }
            catch (Exception exc)
            {
                Console.WriteLine(exc);
            }
            return rezervacija;//return 
        }




        public void UpdateRezervacija(Rezervacija rezervacija)
        {
            try
            {
                //setting new SqlConnection, providing connectionString
                cnn = new MySqlConnection(connectionString);

                //check if user exist
                MySqlCommand cmd = new MySqlCommand("Update rezervacija SET Trans_Id=@Trans_Id, Kliento_nr=@Kliento_nr WHERE rezId=@rezId", cnn);//to check if username exist we have to select all items with username
                cmd.Parameters.AddWithValue("@Trans_Id", rezervacija.Transporto_Id);
                cmd.Parameters.AddWithValue("@Kliento_nr", rezervacija.kliento_Id);
                cmd.Parameters.AddWithValue("@rezId", rezervacija.rezervacijos_Id);
                cnn.Open();
                cmd.ExecuteNonQuery();
                cnn.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }




        public void DeleteRezervacija(Rezervacija rezervacija, int nuomosNr)
        {
            try
            {
                cnn = new MySqlConnection(connectionString);

                string newSql = ("Delete from apmokejimas where apmokejimas.Nuomos_nr=@Nuomos_nr; ");
                newSql += ("Delete from nuoma where nuoma.rezId=@rezId; ");
                newSql += ("Delete from rezervacija where rezervacija.rezId=@rezId");

                cnn.Open();//open connection. we use the Open method of the cnn variable to open a connection to the database.
                MySqlCommand cmd = new MySqlCommand(newSql, cnn);//select all from newTestTable
                cmd.Parameters.AddWithValue("@Nuomos_nr", nuomosNr);
                cmd.Parameters.AddWithValue("@rezId", rezervacija.rezervacijos_Id);
                cmd.ExecuteNonQuery();//execute function

                cnn.Close();
            }
            catch (Exception exc)
            {
                Console.WriteLine(exc);
            }

        }
        public (bool, Rezervacija) CheckForActiveRes(Klientas klientas)
        {
            try
            {
                //setting new SqlConnection, providing connectionString
                cnn = new MySqlConnection(connectionString);
                cnn.Open();//open database
                MySqlCommand cmd = new MySqlCommand("SELECT * FROM rezervacija WHERE rezId=( SELECT MAX(rezId) FROM rezervacija WHERE Kliento_nr=@Kliento_nr)", cnn);//to check if username exist we have to select all items with username
                cmd.Parameters.AddWithValue("@Kliento_nr", klientas.klientoNr);
                MySqlDataReader dataReader = cmd.ExecuteReader();//sends SQLCommand.CommandText to the SQLCommand.Connection and builds SqlDataReader
                while (dataReader.Read() == true)
                {
                    if (TimeSpan.Parse(dataReader["rezPab"].ToString()) > DateTime.Now.TimeOfDay && DateTime.Parse(dataReader["rezData"].ToString()) >= DateTime.Today)
                    {
                        Rezervacija rezervacija = new Rezervacija();
                        rezervacija.kliento_Id = int.Parse(dataReader["Kliento_nr"].ToString());
                        rezervacija.lokacijos_Id = int.Parse(dataReader["lokacijosId"].ToString());
                        rezervacija.Transporto_Id = int.Parse(dataReader["Trans_Id"].ToString());
                        rezervacija.rezervacijos_Data = DateTime.Parse(dataReader["rezData"].ToString());
                        rezervacija.rezervacijosPrad = TimeSpan.Parse(dataReader["rezPrad"].ToString());
                        rezervacija.rezervacijosPab = TimeSpan.Parse(dataReader["rezPab"].ToString());
                        dataReader.Close();
                        return (true, rezervacija);
                    }
                }


                Console.WriteLine("connection is closing");
                dataReader.Close();
                cnn.Close();

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            return (false, null);
        }

        public bool isTransportasTaken(Transportas transportas, Klientas klientas)
        {
            try
            {
                cnn = new MySqlConnection(connectionString);
                cnn.Open();//open database

                //check if rezervacija exist
                MySqlCommand cmd = new MySqlCommand("SELECT * FROM rezervacija WHERE rezId=( SELECT MAX(rezId) FROM rezervacija WHERE Trans_Id=@Trans_Id);", cnn);//to check if username exist we have to select all items with username
                cmd.Parameters.AddWithValue("@Trans_Id", transportas.transporto_Id);
                MySqlDataReader dataReader = cmd.ExecuteReader();//sends SQLCommand.CommandText to the SQLCommand.Connection and builds SqlDataReader
                if ((dataReader.Read() == true) && TimeSpan.Parse(dataReader["rezPab"].ToString()) > DateTime.Now.TimeOfDay && DateTime.Parse(dataReader["rezData"].ToString()) >= DateTime.Today)
                {
                    if (int.Parse(dataReader["Kliento_nr"].ToString()) == klientas.klientoNr) { return false; }
                    return true;
                }
                else
                {
                    Console.WriteLine("transport is not taken");
                }

            }
             
            catch (Exception ex) { Console.WriteLine(ex.Message); }
            return false;

        }
        public void CancelRezervacija(Klientas klientas)
        {
            try
            {
                //setting new SqlConnection, providing connectionString
                cnn = new MySqlConnection(connectionString);
                cnn.Open();//open database
                MySqlCommand cmd = new MySqlCommand("SELECT * FROM rezervacija WHERE rezId=( SELECT MAX(rezId) FROM rezervacija WHERE Kliento_nr=@Kliento_nr)", cnn);//to check if username exist we have to select all items with username
                cmd.Parameters.AddWithValue("@Kliento_nr", klientas.klientoNr);
                MySqlDataReader dataReader = cmd.ExecuteReader();//sends SQLCommand.CommandText to the SQLCommand.Connection and builds SqlDataReader
                while (dataReader.Read() == true)
                {
                    if (TimeSpan.Parse(dataReader["rezPab"].ToString()) > DateTime.Now.TimeOfDay)
                    {
                        int rezervID = Convert.ToInt32(dataReader["rezId"]);

                        MySqlCommand cmd1 = new MySqlCommand("Update rezervacija SET rezPab=@rezPab WHERE rezId=@rezId", cnn);//to check if username exist we have to select all items with username

                        cmd1.Parameters.AddWithValue("@rezPab", DateTime.Now.TimeOfDay);
                        cmd1.Parameters.AddWithValue("@rezId", rezervID);
                        dataReader.Close();
                        cmd1.ExecuteNonQuery();
                        Console.WriteLine("Succesfuly canceled");

                        return;
                    }
                }

                Console.WriteLine("connection is closing");
                cnn.Close();

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }
        public Rezervacija getLastReservacija(Klientas klientas)
        {
            Rezervacija rezervacija = new Rezervacija();
            try
            {
                cnn = new MySqlConnection(connectionString);
                cnn.Open();
                MySqlCommand cmd = new MySqlCommand("SELECT * FROM rezervacija WHERE rezId=( SELECT MAX(rezId) FROM rezervacija WHERE Kliento_nr=@Kliento_nr);", cnn);
                cmd.Parameters.AddWithValue("@Kliento_nr", klientas.klientoNr);
                MySqlDataReader dataReader = cmd.ExecuteReader();
                dataReader.Read();
                rezervacija.rezervacijos_Id = int.Parse(dataReader["rezId"].ToString());
                rezervacija.kliento_Id = int.Parse(dataReader["Kliento_nr"].ToString());
                rezervacija.lokacijos_Id = int.Parse(dataReader["lokacijosId"].ToString());
                rezervacija.rezervacijosPab = TimeSpan.Parse(dataReader["rezPab"].ToString());
                rezervacija.rezervacijosPrad = TimeSpan.Parse(dataReader["rezPrad"].ToString());
                rezervacija.rezervacijos_Data = DateTime.Parse(dataReader["rezData"].ToString());
                dataReader.Close();
                cnn.Close();
            }
            catch (Exception ex) { Console.WriteLine(ex.Message); }

            return rezervacija;
        }

        public bool addNewRezervacija(Klientas klientas, Transportas transportas, Lokacija lokacija)
        {

            cnn = new MySqlConnection(connectionString);
            cnn.Open();//opens connection
            cnn1 = new MySqlConnection(connectionString);
            cnn1.Open();//opens connection
            MySqlCommand cmd = new MySqlCommand("SELECT * FROM rezervacija WHERE rezId=( SELECT MAX(rezId) FROM rezervacija WHERE Kliento_nr=@Kliento_nr);", cnn);//to check if username exist we have to select all items with username
            cmd.Parameters.AddWithValue("@Kliento_nr", klientas.klientoNr);
            MySqlDataReader dataReader = cmd.ExecuteReader();//sends SQLCommand.CommandText to the SQLCommand.Connection and builds SqlDataReader

            MySqlCommand cmd1 = new MySqlCommand("SELECT * FROM nuoma WHERE Nuomos_nr=( SELECT MAX(Nuomos_nr) FROM nuoma WHERE Kliento_nr=@Kliento_nr)", cnn1);
            cmd1.Parameters.AddWithValue("@Kliento_nr", klientas.klientoNr);
            MySqlDataReader dataReader1 = cmd1.ExecuteReader();

            if (dataReader.Read() == true && dataReader1.Read())
            {

                if (TimeSpan.Parse(dataReader["rezPab"].ToString()) > DateTime.Now.TimeOfDay && DateTime.Parse(dataReader["rezData"].ToString()) >= DateTime.Today ||
                TimeSpan.Parse(dataReader1["NuomosPabLaik"].ToString()) > DateTime.Now.TimeOfDay && DateTime.Parse(dataReader1["NuomosPabData"].ToString()) >= DateTime.Today)
                {

                    Console.WriteLine("Client already has an active reservation or lease");
                    dataReader1.Close();
                    cnn1.Close();
                    return false;

                }
                else
                {
                    Console.WriteLine("Creating new rezervation object");
                    Rezervacija rezervacija1 = new Rezervacija();
                    TimeSpan rezervacijosLaikas1 = new TimeSpan(0, 15, 0);
                    rezervacija1.kliento_Id = klientas.klientoNr;
                    rezervacija1.lokacijos_Id = lokacija.lokacijos_Id;
                    rezervacija1.Transporto_Id = transportas.transporto_Id;
                    rezervacija1.rezervacijos_Data = DateTime.Today;
                    rezervacija1.rezervacijosPrad = DateTime.Now.TimeOfDay;
                    rezervacija1.rezervacijosPab = rezervacija1.rezervacijosPrad.Add(rezervacijosLaikas1);     //DateTime.Today.AddSeconds(900);
                    if (InsertRezervacija(rezervacija1).Item2 == true) { return true; }


                }


            }
            else if (dataReader.Read() == false)
            {
                Console.WriteLine("Creating new rezervation object");
                Rezervacija rezervacija = new Rezervacija();
                TimeSpan rezervacijosLaikas = new TimeSpan(0, 15, 0);
                rezervacija.kliento_Id = klientas.klientoNr;
                rezervacija.lokacijos_Id = lokacija.lokacijos_Id;
                rezervacija.Transporto_Id = transportas.transporto_Id;
                rezervacija.rezervacijos_Data = DateTime.Today;
                rezervacija.rezervacijosPrad = DateTime.Now.TimeOfDay;
                rezervacija.rezervacijosPab = rezervacija.rezervacijosPrad.Add(rezervacijosLaikas);     //DateTime.Today.AddSeconds(900);
                if (InsertRezervacija(rezervacija).Item2 == true) { return true; }

            }

            dataReader.Close();
            cnn.Close();
            cnn1.Close();
            return false;

        }
    }
}
