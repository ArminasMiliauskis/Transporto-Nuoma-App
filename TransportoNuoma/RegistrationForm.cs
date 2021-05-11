using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.Windows.Forms;
using TransportoNuoma.Classes;
using TransportoNuoma.Repositories;

namespace TransportoNuoma
{
    public partial class RegistrationForm : Form
    {
        UsersRepository usersRepository;
        public RegistrationForm()
        {
            InitializeComponent();
            usersRepository = new UsersRepository();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void registerButton_Click(object sender, EventArgs e)
        {
            if (UserDataCheck.Checked == true) 
            {
                try
                {
                    if (repeatPassword.Text == registerPassword.Text)
                    {
                        //create client object out of textbox values
                        Klientas klientas = new Klientas();
                        klientas.vardas = vardasRegister.Text;
                        klientas.pavarde = pavardeRegister.Text;
                        klientas.kodas = int.Parse(kodasRegister.Text);
                        klientas.email = registerEmail.Text;
                        klientas.slaptazodis = registerPassword.Text;
                        klientas.isAdmin = 0;
                        Klientas registerClient = usersRepository.RegisterClient(klientas);
                        if (registerClient.vardas != null && registerClient.vardas != "")
                        {
                            MessageBox.Show("New user succesfuly registered");
                            this.Close();
                        }
                    }

                }
                catch (Exception exc)
                {
                    MessageBox.Show(exc.ToString());
                }
            }else { MessageBox.Show("Nesutikote su asmens duomenų saugojimu"); }
            

        }
    }
}
