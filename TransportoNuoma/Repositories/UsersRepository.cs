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
    class UsersRepository
    {
        string connectionString = "server=34.91.29.158;user id=root;persistsecurityinfo=True;port=3306;database=lsongulija;password=123456";
        MySqlConnection cnn;



        public DataTable displayClients()
        {
            DataTable dta = new DataTable();
            try
            {
                cnn = new MySqlConnection(connectionString);//assign connection. The variable cnn, which is of type SqlConnection is used to establish the connection to the database.
                cnn.Open();//open connection. we use the Open method of the cnn variable to open a connection to the database.

                MySqlCommand cmd = new MySqlCommand("SELECT * FROM klientas", cnn);//select all from newTestTable

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
        public Klientas RegisterClient(Klientas klientas)//provide student object when calling this function
        {
            try
            {


                //setting new SqlConnection, providing connectionString
                cnn = new MySqlConnection(connectionString);
                cnn.Open();//open database

                //check if user exist
                MySqlCommand cmd = new MySqlCommand("Select * from klientas where email=@email", cnn);//to check if username exist we have to select all items with username
                cmd.Parameters.AddWithValue("@email", klientas.email);
                MySqlDataReader dataReader = cmd.ExecuteReader();//sends SQLCommand.CommandText to the SQLCommand.Connection and builds SqlDataReader
                if ((dataReader.Read() == true))//if dataReader.Read is true then it means that it found User with that Username
                {
                    //then User with that Username already exists
                    Console.WriteLine("User with that Username already exists");
                    return null;//if username exist than it will not watch script bellow, it will just return
                }

                else//there is no user with that Username that we want to register
                {
                    //so we can register user
                    Console.WriteLine("Username is free so you can register");
                }
                dataReader.Close();//close data reader when it finishes work

                MySqlCommand cmd1 = new MySqlCommand("Insert into klientas (Vardas,Pavarde,Asmens_Kodas,email,slaptazodis,isAdmin) VALUES(@Vardas,@Pavarde,@Asmens_Kodas,@email,@slaptazodis,@isAdmin)", cnn);
                cmd1.Parameters.AddWithValue("@Vardas", klientas.vardas);
                cmd1.Parameters.AddWithValue("@Pavarde", klientas.pavarde);
                cmd1.Parameters.AddWithValue("@Asmens_Kodas", klientas.kodas);
                cmd1.Parameters.AddWithValue("@email", klientas.email);
                cmd1.Parameters.AddWithValue("@slaptazodis", klientas.slaptazodis);
                cmd1.Parameters.AddWithValue("@isAdmin", klientas.isAdmin);
                cmd1.ExecuteNonQuery();
                cnn.Close();

            }
            catch (Exception exc)
            {
                Console.WriteLine(exc);
            }
            return klientas;//return 
        }







        public Klientas LoginKlientas(string email, string slaptazodis)
        {
            Klientas klientas = new Klientas();
            try
            {

                //STUDENT
                cnn = new MySqlConnection(connectionString);
                MySqlCommand cmd = new MySqlCommand("Select Kliento_nr,Vardas,Pavarde,Email,isAdmin FROM klientas where Email=@Email AND Slaptazodis=@Slaptazodis", cnn);
                //parametrised Sql Command, have to provide values that we wrote
                cmd.Parameters.AddWithValue("@Email", email);
                cmd.Parameters.AddWithValue("@Slaptazodis", slaptazodis);

                cnn.Open();
                MySqlDataReader reader = cmd.ExecuteReader();//we want to read rows that we get with this command
                while (reader.Read())//while reader can read, while there is information/rows of data
                {
                    //get all values from row
                    int Kliento_nr = int.Parse(reader["Kliento_nr"].ToString());
                    string Vardas = reader["Vardas"].ToString();
                    string Pavarde = reader["Pavarde"].ToString();
                    string Email = reader["Email"].ToString();
                    int isAdmin = int.Parse(reader["isAdmin"].ToString());

                    //create client object

                    klientas.klientoNr = Kliento_nr;
                    klientas.vardas = Vardas;
                    klientas.pavarde = Pavarde;
                    klientas.email = Email;
                    klientas.isAdmin = isAdmin;

                }

                cnn.Close();
                
                return klientas;

            }
            catch (Exception exc)
            {
                Console.WriteLine(exc);
                return null;
            }

        }



        public void UpdateKlientas(Klientas klientas)
        {
            try
            {
                //setting new SqlConnection, providing connectionString
                cnn = new MySqlConnection(connectionString);

                //check if user exist
                MySqlCommand cmd = new MySqlCommand("Update klientas SET Email=@Email, Vardas=@Vardas,Pavarde=@Pavarde  WHERE Kliento_nr=@Kliento_nr", cnn);//to check if username exist we have to select all items with username
                cmd.Parameters.AddWithValue("@Email", klientas.email);
                cmd.Parameters.AddWithValue("@Vardas", klientas.vardas);
                cmd.Parameters.AddWithValue("@Pavarde", klientas.pavarde);
                cmd.Parameters.AddWithValue("@Kliento_nr", klientas.klientoNr);
                cnn.Open();
                cmd.ExecuteNonQuery();
                cnn.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }


        public void DeleteKlientas(Klientas klientas)
        {
            try
            {
                cnn = new MySqlConnection(connectionString);

                string newSql = "DELETE FROM klientas WHERE klientas.Kliento_nr=@id;";

                cnn.Open();//open connection. we use the Open method of the cnn variable to open a connection to the database.
                MySqlCommand cmd = new MySqlCommand(newSql, cnn);//select all from newTestTable
                cmd.Parameters.AddWithValue("@id", klientas.klientoNr);
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
