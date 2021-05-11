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
    class TransportRepository
    {
            string connectionString = "server=34.91.29.158;user id=root;persistsecurityinfo=True;port=3306;database=lsongulija;password=123456";
            MySqlConnection cnn;

        

            public DataTable displayTransportas()
            {
                DataTable dta = new DataTable();
                try
                {
                    cnn = new MySqlConnection(connectionString);//assign connection. The variable cnn, which is of type SqlConnection is used to establish the connection to the database.
                    cnn.Open();//open connection. we use the Open method of the cnn variable to open a connection to the database.

                    MySqlCommand cmd = new MySqlCommand("Select * from transportas", cnn);//select all from newTestTable

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
            
        public List<Transportas> getTransportList()
        {
            List<Transportas> transportList = new List<Transportas>();
                
            try
            {
                cnn = new MySqlConnection(connectionString);//assign connection. The variable cnn, which is of type SqlConnection is used to establish the connection to the database.
                cnn.Open();//open connection. we use the Open method of the cnn variable to open a connection to the database.

                MySqlCommand cmd = new MySqlCommand("Select * from transportas", cnn);//select all from newTestTable
                MySqlDataReader dataReader = cmd.ExecuteReader();

                if ((dataReader.Read() == true))
                {
                    do
                    {
                        Transportas transportas = new Transportas();
                        int transporto_Id = int.Parse(dataReader["Trans_Id"].ToString());
                        string transporto_Nr = dataReader["Trans_nr"].ToString();
                        string tipas = dataReader["Tipas"].ToString();
                        string spalva = dataReader["Spalva"].ToString();
                        DateTime gamybos_Metai = DateTime.Parse(dataReader["Gamybos_Metai"].ToString());
                        int kaina = int.Parse(dataReader["Kaina"].ToString());
                        string QRCode = dataReader["QrKodas"].ToString();
                        int markes_Id = int.Parse(dataReader["MarkesId"].ToString());

                        transportas.transporto_Id = transporto_Id;
                        transportas.transporto_Nr = transporto_Nr;
                        transportas.tipas = tipas;
                        transportas.spalva = spalva;
                        transportas.gamybos_Metai = gamybos_Metai;
                        transportas.kaina = kaina;
                        transportas.QRCode = QRCode;
                        transportas.markes_Id = markes_Id;



                        transportList.Add(transportas);
                    } while (dataReader.Read() == true);
                    
                 }


            }
            catch (Exception e) { Console.WriteLine(e.Message); }
                
            return transportList;
        }
        
        public Transportas getTransportasByID(int trans_id)
        {
            Transportas transportas = new Transportas();
            try
            {
                cnn = new MySqlConnection(connectionString);//assign connection. The variable cnn, which is of type SqlConnection is used to establish the connection to the database.
                cnn.Open();//open connection. we use the Open method of the cnn variable to open a connection to the database.

                MySqlCommand cmd = new MySqlCommand("Select * from transportas where Trans_Id=@Trans_Id", cnn);//select all from newTestTable
                cmd.Parameters.AddWithValue("@Trans_Id", trans_id);
                MySqlDataReader dataReader = cmd.ExecuteReader();
                if ((dataReader.Read() == true))
                {                  
                    transportas.transporto_Id = int.Parse(dataReader["Trans_Id"].ToString());
                    transportas.transporto_Nr = dataReader["Trans_nr"].ToString();
                    transportas.tipas = dataReader["Tipas"].ToString();
                    transportas.spalva = dataReader["Spalva"].ToString();
                    transportas.gamybos_Metai = DateTime.Parse(dataReader["Gamybos_Metai"].ToString());
                    transportas.kaina = int.Parse(dataReader["Kaina"].ToString());
                    transportas.QRCode = dataReader["QrKodas"].ToString();
                    transportas.markes_Id = int.Parse(dataReader["MarkesId"].ToString());
                }
            }
            catch (Exception ex) { Console.WriteLine(ex.Message); }

            return transportas;
        }
            
            
            //REGISTER STUDENT
            public Transportas InsertTransport(Transportas transportas)//provide transportas object when calling this function
            {
                try
                {


                    //setting new SqlConnection, providing connectionString
                    cnn = new MySqlConnection(connectionString);
                    cnn.Open();//open database

                    //check if user exist
                    MySqlCommand cmd = new MySqlCommand("Select * from transportas where Trans_nr=@Trans_nr", cnn);//to check if username exist we have to select all items with username
                    cmd.Parameters.AddWithValue("@Trans_nr", transportas.transporto_Nr);
                    MySqlDataReader dataReader = cmd.ExecuteReader();//sends SQLCommand.CommandText to the SQLCommand.Connection and builds SqlDataReader
                    if ((dataReader.Read() == true))
                    {
                        Console.WriteLine("Transport with that Number already exists");
                        return null;
                    }
                    else
                    {
                        Console.WriteLine("Transport is free so you can register");
                    }
                    dataReader.Close();//close data reader when it finishes work

                    MySqlCommand cmd1 = new MySqlCommand("Insert into transportas (Trans_nr,Tipas,Spalva,Gamybos_Metai,Kaina,MarkesId,QrKodas) VALUES(@Trans_nr,@Tipas,@Spalva,@Gamybos_Metai,@Kaina,@MarkesId,@QrKodas)", cnn);
                    cmd1.Parameters.AddWithValue("@Trans_nr", transportas.transporto_Nr);
                    cmd1.Parameters.AddWithValue("@Tipas", transportas.tipas);
                    cmd1.Parameters.AddWithValue("@Spalva", transportas.spalva);
                    cmd1.Parameters.AddWithValue("@Gamybos_Metai", transportas.gamybos_Metai);
                    cmd1.Parameters.AddWithValue("@Kaina", transportas.kaina);
                    cmd1.Parameters.AddWithValue("@MarkesId", transportas.markes_Id);
                    cmd1.Parameters.AddWithValue("@QrKodas", transportas.QRCode);
                    cmd1.ExecuteNonQuery();
                    cnn.Close();

                }
                catch (Exception exc)
                {
                    Console.WriteLine(exc);
                }
                return transportas;//return 
            }


        public void UpdateTransportas(Transportas transportas)
        {
            try
            {
                //setting new SqlConnection, providing connectionString
                cnn = new MySqlConnection(connectionString);

                //check if user exist
                MySqlCommand cmd = new MySqlCommand("Update transportas SET Trans_nr=@Trans_nr,Spalva=@Spalva,Kaina=@Kaina WHERE Trans_Id=@Trans_Id", cnn);//to check if username exist we have to select all items with username
                cmd.Parameters.AddWithValue("@Trans_nr", transportas.transporto_Nr);
                cmd.Parameters.AddWithValue("@Spalva", transportas.spalva);
                cmd.Parameters.AddWithValue("@Kaina", transportas.kaina);
                cmd.Parameters.AddWithValue("@Trans_Id", transportas.transporto_Id);
                cnn.Open();
                cmd.ExecuteNonQuery();
                cnn.Close();
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex);
            }
        }


        public void DeleteTransportas(Transportas transportas)
        {
            try
            {
                cnn = new MySqlConnection(connectionString);

                string newSql = "DELETE FROM transportas WHERE transportas.Trans_Id=@id;";

                cnn.Open();//open connection. we use the Open method of the cnn variable to open a connection to the database.
                MySqlCommand cmd = new MySqlCommand(newSql, cnn);//select all from newTestTable
                cmd.Parameters.AddWithValue("@id", transportas.transporto_Id);
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
