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
    class NuomaRepository
    {

        string connectionString = "server=34.91.29.158;user id=root;persistsecurityinfo=True;port=3306;database=lsongulija;password=123456";
        MySqlConnection cnn;



        public DataTable displayNuoma()
        {
            DataTable dta = new DataTable();
            try
            {
                cnn = new MySqlConnection(connectionString);//assign connection. The variable cnn, which is of type SqlConnection is used to establish the connection to the database.
                cnn.Open();//open connection. we use the Open method of the cnn variable to open a connection to the database.

                MySqlCommand cmd = new MySqlCommand("Select * from nuoma", cnn);//select all from newTestTable

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
        public Nuoma InsertNuoma(Nuoma nuoma)//provide transportas object when calling this function
        {
            try
            {
                bool rezYra = false;

                //setting new SqlConnection, providing connectionString
                cnn = new MySqlConnection(connectionString);
                cnn.Open();//open database

                //check if rezervacija exist
                MySqlCommand cmd = new MySqlCommand("Select * from rezervacija where rezId=@rezId", cnn);
                cmd.Parameters.AddWithValue("@rezId", nuoma.rezId);
                MySqlDataReader dataReader = cmd.ExecuteReader();//sends SQLCommand.CommandText to the SQLCommand.Connection and builds SqlDataReader
                if ((dataReader.Read() == true))
                {
                    rezYra = true;
                }
                else
                {
                    Console.WriteLine("Transport is free so you can register");
                    return null;
                }
                dataReader.Close();//close data reader when it finishes work

                if(rezYra == true)
                {
                    Console.WriteLine("Transport with that Number already exists");
                    MySqlCommand cmd1 = new MySqlCommand("Insert into nuoma (NuomosPrData,NuomosPradLaik,NuomosPabLaik,NuomosPabData,Trans_Id,Kliento_nr,rezId) VALUES(@NuomosPrData,@NuomosPradLaik,@NuomosPabLaik,@NuomosPabData,@Trans_Id,@Kliento_nr,@rezId)", cnn);
                    cmd1.Parameters.AddWithValue("@NuomosPrData", nuoma.nuomosPrData);
                    cmd1.Parameters.AddWithValue("@NuomosPradLaik", nuoma.nuomosPradLaik);
                    cmd1.Parameters.AddWithValue("@NuomosPabLaik", nuoma.nuomosPabLaik);
                    cmd1.Parameters.AddWithValue("@NuomosPabData", nuoma.nuomosPabData);
                    cmd1.Parameters.AddWithValue("@Trans_Id", nuoma.transporto_Id);
                    cmd1.Parameters.AddWithValue("@Kliento_nr", nuoma.kliento_Nr);
                    cmd1.Parameters.AddWithValue("@rezId", nuoma.rezId);
                    cmd1.ExecuteNonQuery();
                }

                cnn.Close();

            }
            catch (Exception exc)
            {
                Console.WriteLine(exc);
            }
            return nuoma;//return 
        }

        public bool CheckIfAvailable(Transportas transportas, Klientas klientas)
        {
            try
            {
                cnn = new MySqlConnection(connectionString);
                cnn.Open();//open database

                //check if rezervacija exist
                MySqlCommand cmd = new MySqlCommand("SELECT * FROM nuoma WHERE Nuomos_nr=( SELECT MAX(Nuomos_nr) FROM nuoma WHERE Trans_Id=@Trans_Id)", cnn);//to check if username exist we have to select all items with username
                cmd.Parameters.AddWithValue("@Trans_Id", transportas.transporto_Id);
                MySqlDataReader dataReader = cmd.ExecuteReader();//sends SQLCommand.CommandText to the SQLCommand.Connection and builds SqlDataReader
                if ((dataReader.Read() == true) && TimeSpan.Parse(dataReader["NuomosPabLaik"].ToString()) > DateTime.Now.TimeOfDay && DateTime.Parse(dataReader["NuomosPabData"].ToString()) >= DateTime.Today)
                {
                    if (int.Parse(dataReader["Kliento_nr"].ToString()) == klientas.klientoNr) { return true; }
                    Console.WriteLine("transport is taken");
                    return false;
                }
                else
                {
                    Console.WriteLine("transport is not taken");
                }
                dataReader.Close();
            }
            catch (Exception ex) { Console.WriteLine(ex.Message); }
            cnn.Close();
            return true;
        }

        public bool RegisterNewNuoma(Klientas klientas, Transportas transportas, Rezervacija rezervacija)
        {
            try
            {
                cnn = new MySqlConnection(connectionString);
                cnn.Open();//opens connection
                MySqlCommand cmd = new MySqlCommand("SELECT * FROM nuoma WHERE Nuomos_nr=( SELECT MAX(Nuomos_nr) FROM nuoma WHERE Kliento_nr=@Kliento_nr);", cnn);//to check if username exist we have to select all items with username
                cmd.Parameters.AddWithValue("@Kliento_nr", klientas.klientoNr);
                MySqlDataReader dataReader = cmd.ExecuteReader();
                if (dataReader.Read() == true)
                {
                    if (TimeSpan.Parse(dataReader["NuomosPabLaik"].ToString()) > DateTime.Now.TimeOfDay && DateTime.Parse(dataReader["NuomosPabData"].ToString()) >= DateTime.Today)
                    {
                        Console.WriteLine("Client already has an active lease");
                        return false;
                    }
                }
                dataReader.Close();
                Console.WriteLine("Creating new lease...");
                MySqlCommand cmd1 = new MySqlCommand("Insert into nuoma (NuomosPrData,NuomosPradLaik,NuomosPabLaik,NuomosPabData,Trans_Id,Kliento_nr,rezId) VALUES(@NuomosPrData,@NuomosPradLaik,@NuomosPabLaik,@NuomosPabData,@Trans_Id,@Kliento_nr,@rezId)", cnn);
                cmd1.Parameters.AddWithValue("@NuomosPrData", DateTime.Today);
                cmd1.Parameters.AddWithValue("@NuomosPradLaik", DateTime.Now.TimeOfDay);
                TimeSpan nuomosLaikas = new TimeSpan(1, 0, 0);
                cmd1.Parameters.AddWithValue("@NuomosPabLaik", DateTime.Now.TimeOfDay.Add(nuomosLaikas));               
                cmd1.Parameters.AddWithValue("@NuomosPabData", DateTime.Today);
                cmd1.Parameters.AddWithValue("@Trans_Id", transportas.transporto_Id);
                cmd1.Parameters.AddWithValue("@Kliento_nr", klientas.klientoNr);
                cmd1.Parameters.AddWithValue("@rezId", rezervacija.rezervacijos_Id);
                cmd1.ExecuteNonQuery();
                Console.WriteLine("Lease succesfuly inserted");
                cnn.Close();
            }catch(Exception ex) { Console.WriteLine(ex.Message); }
            
            return true;
        }
            
        public void UpdateNuoma(Nuoma nuoma)
        {
            try
            {
                //setting new SqlConnection, providing connectionString
                cnn = new MySqlConnection(connectionString);

                //check if user exist
                MySqlCommand cmd = new MySqlCommand("Update nuoma SET Trans_Id=@Trans_Id,Kliento_nr=@Kliento_nr WHERE Nuomos_nr=@Nuomos_nr", cnn);//to check if username exist we have to select all items with username
                cmd.Parameters.AddWithValue("@Trans_Id", nuoma.transporto_Id);
                cmd.Parameters.AddWithValue("@Kliento_nr", nuoma.kliento_Nr);
                cmd.Parameters.AddWithValue("@Nuomos_nr", nuoma.nuomos_Nr);
                cnn.Open();
                cmd.ExecuteNonQuery();
                cnn.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        public void CancelNuoma(Klientas klientas)
        {
            try
            {
                //setting new SqlConnection, providing connectionString
                cnn = new MySqlConnection(connectionString);
                cnn.Open();//open database
                MySqlCommand cmd = new MySqlCommand("SELECT * FROM nuoma WHERE Nuomos_nr=( SELECT MAX(Nuomos_nr) FROM nuoma WHERE Kliento_nr=@Kliento_nr)", cnn);//to check if username exist we have to select all items with username
                cmd.Parameters.AddWithValue("@Kliento_nr", klientas.klientoNr);
                MySqlDataReader dataReader = cmd.ExecuteReader();//sends SQLCommand.CommandText to the SQLCommand.Connection and builds SqlDataReader
                while (dataReader.Read() == true)
                {
                    if (TimeSpan.Parse(dataReader["NuomosPabLaik"].ToString()) > DateTime.Now.TimeOfDay && DateTime.Parse(dataReader["NuomosPabData"].ToString()) >= DateTime.Today)
                    {
                        int nuomosID = Convert.ToInt32(dataReader["Nuomos_nr"]);

                        MySqlCommand cmd1 = new MySqlCommand("Update nuoma SET NuomosPabLaik=@NuomosPabLaik WHERE Nuomos_nr=@Nuomos_nr", cnn);//to check if username exist we have to select all items with username

                        cmd1.Parameters.AddWithValue("@NuomosPabLaik", DateTime.Now.TimeOfDay);
                        cmd1.Parameters.AddWithValue("@Nuomos_nr", nuomosID);
                        dataReader.Close();
                        cmd1.ExecuteNonQuery();
                        Console.WriteLine("Lease succesfuly canceled");

                        return;
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
        }
        public (bool, Nuoma) CheckForActiveNuoma(Klientas klientas)
        {
            try
            {
                //setting new SqlConnection, providing connectionString
                cnn = new MySqlConnection(connectionString);
                cnn.Open();//open database
                MySqlCommand cmd = new MySqlCommand("SELECT * FROM nuoma WHERE Nuomos_nr=( SELECT MAX(Nuomos_nr) FROM nuoma WHERE Kliento_nr=@Kliento_nr)", cnn);//to check if username exist we have to select all items with username
                cmd.Parameters.AddWithValue("@Kliento_nr", klientas.klientoNr);
                MySqlDataReader dataReader = cmd.ExecuteReader();//sends SQLCommand.CommandText to the SQLCommand.Connection and builds SqlDataReader
                while (dataReader.Read() == true)
                {
                    if (TimeSpan.Parse(dataReader["NuomosPabLaik"].ToString()) > DateTime.Now.TimeOfDay && DateTime.Parse(dataReader["NuomosPabData"].ToString()) == DateTime.Today)
                    {
                        Nuoma nuoma = new Nuoma();
                        nuoma.kliento_Nr = int.Parse(dataReader["Kliento_nr"].ToString());
                        nuoma.nuomosPabData = DateTime.Parse(dataReader["NuomosPabData"].ToString());
                        nuoma.nuomosPabLaik = TimeSpan.Parse(dataReader["NuomosPabLaik"].ToString());
                        nuoma.nuomosPradLaik = TimeSpan.Parse(dataReader["NuomosPradLaik"].ToString());
                        nuoma.nuomosPrData = DateTime.Parse(dataReader["NuomosPrData"].ToString());
                        nuoma.nuomos_Nr = int.Parse(dataReader["Nuomos_nr"].ToString());
                        nuoma.rezId = int.Parse(dataReader["rezId"].ToString());
                        nuoma.transporto_Id = int.Parse(dataReader["Trans_Id"].ToString());
                       
                        dataReader.Close();
                        return (true, nuoma);
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

        public void DeleteNuoma(Nuoma nuoma)
        {
            try
            {
                cnn = new MySqlConnection(connectionString);

                string newSql = ("Delete from apmokejimas where apmokejimas.Nuomos_nr=@id; ");
                newSql += ("Delete from nuoma where nuoma.Nuomos_nr=@id");

                cnn.Open();//open connection. we use the Open method of the cnn variable to open a connection to the database.
                MySqlCommand cmd = new MySqlCommand(newSql, cnn);//select all from newTestTable
                cmd.Parameters.AddWithValue("@id", nuoma.nuomos_Nr);
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
