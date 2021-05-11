using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.Windows.Forms;
using TransportoNuoma.Classes;
using TransportoNuoma.Repositories;

namespace TransportoNuoma
{
    public partial class Form1 : Form
    {
        UsersRepository usersRepository;
        public Form1()
        {
            InitializeComponent();

            usersRepository = new UsersRepository();
        }

        static string connectionString = "server=34.91.29.158;user id=root;persistsecurityinfo=True;port=3306;database=lsongulija;password=123456";
        MySqlConnection con = new MySqlConnection(connectionString);

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void loginButton_Click(object sender, EventArgs e)
        {
            if(loginEmail.Text != null && loginEmail.Text!="" && loginPassword.Text != null && loginPassword.Text != "")
            {
                Klientas loggedKlientas = usersRepository.LoginKlientas(loginEmail.Text, loginPassword.Text);

                if (loggedKlientas.email != null && loggedKlientas.email!="")
                {
                   
                    Console.WriteLine(loggedKlientas.email);
                    if(loggedKlientas.isAdmin == 1)
                    {
                        MainFormAdmin mainFormAdmin = new MainFormAdmin(loggedKlientas);
                        this.Hide();
                        mainFormAdmin.ShowDialog();
                        this.Close();
                    }
                    else if(loggedKlientas.isAdmin == 2)
                    {
                        MainFormWorker mainFormWorker = new MainFormWorker(loggedKlientas);
                        this.Hide();
                        mainFormWorker.ShowDialog();
                        this.Close();
                    }
                    else
                    {
                        MainForm mainForm = new MainForm(loggedKlientas);
                        this.Hide();
                        mainForm.ShowDialog();
                        this.Close();
                    }
                    
                }
                else
                {
                    MessageBox.Show("Incorrect email or password");
                    //could redirect to register page or forget email/pass page
                   
                }

            }
 

        }

        private void registerButton_Click(object sender, EventArgs e)
        {
            
        }


        

        private void getClients_Click(object sender, EventArgs e)
        {
            usersRepository.displayClients();
        }

        private void registracionButton_Click(object sender, EventArgs e)
        {
            RegistrationForm registration = new RegistrationForm();
            registration.ShowDialog();
        }
    }
}
