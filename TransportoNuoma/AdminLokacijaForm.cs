using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TransportoNuoma.Classes;
using TransportoNuoma.Repositories;

namespace TransportoNuoma
{
    public partial class AdminLokacijaForm : Form
    {
        Klientas klientas;
        LokacijaRepository transLokRep;
        TransportRepository transRep;
        KlientoLokacijaRepository klientoLokRep;
        UsersRepository klientoRep;

        public AdminLokacijaForm(Klientas klientas)
        {
            InitializeComponent();
            this.klientas = klientas;
            transLokRep = new LokacijaRepository();
            klientoLokRep = new KlientoLokacijaRepository();
            transRep = new TransportRepository();
            klientoRep = new UsersRepository();

        }

        private void backButton_Click(object sender, EventArgs e)
        {
            MainFormAdmin admin = new MainFormAdmin(klientas);
            this.Hide();
            admin.ShowDialog();
            this.Close();
        }

        private void getTransportLok_Click(object sender, EventArgs e)
        {
            getTransLokDisplay();
        }

        private void addTransportLokShow_Click(object sender, EventArgs e)
        {
            updateTransLokPanel.Visible = false;
            addTransLokPanel.Visible = true;
        }

        private void updateTransLokShow_Click(object sender, EventArgs e)
        {
            addTransLokPanel.Visible = false;
            updateTransLokPanel.Visible = true;
        }

        private void addTransTest_Click(object sender, EventArgs e)
        {
            try
            {
                Lokacija lok = new Lokacija();
                lok.salis = addTransLokSalis.Text;
                lok.miestas = addTransLokMiestas.Text;
                lok.koordinatesX = double.Parse(addTransLokKoordX.Text);
                lok.koordinatesY = double.Parse(addTransLokKoordY.Text);
                lok.transporto_Id = int.Parse(addTransLokTransId.Text);

                Lokacija InsertedLok = transLokRep.InsertLokacija(lok);

                addTransLokSalis.Clear();
                addTransLokMiestas.Clear();
                addTransLokKoordX.Clear();
                addTransLokKoordY.Clear();
                addTransLokTransId.Clear();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            MessageBox.Show("Succesfully inserted");
            getTransLokDisplay();
        }

        private void updateButton_Click(object sender, EventArgs e)
        {
            try
            {
                Lokacija lok = new Lokacija();
                lok.salis = updateTransLokSalis.Text;
                lok.miestas = updateTransLokMiestas.Text;
                lok.koordinatesX = double.Parse(updateTransLokKoordX.Text);
                lok.koordinatesY = double.Parse(updateTransLokKoordY.Text);
                lok.transporto_Id = int.Parse(updateTransLokTransId.Text);
                lok.lokacijos_Id = int.Parse(updateTransLokLokacId.Text);

                transLokRep.UpdateLokacija(lok);

                updateTransLokSalis.Clear();
                updateTransLokMiestas.Clear();
                updateTransLokKoordX.Clear();
                updateTransLokKoordY.Clear();
                updateTransLokTransId.Clear();
                updateTransLokLokacId.Clear();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            MessageBox.Show("Succesfully updated");
            getTransLokDisplay();
        }

        private void getTransLokDisplay()
        {
            try
            {
                DataTable dta = transLokRep.displayLokacija();
                dataGridView1.DataSource = dta;


                DataTable dta1 = transRep.displayTransportas();
                dataGridView2.DataSource = dta1;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }




        //KLIENTO LOKACIJA


        private void getKlientoLok_Click(object sender, EventArgs e)
        {
            getKlientoLokDisplay();
        }

        private void addKlientoLokShow_Click(object sender, EventArgs e)
        {
            updateKlientoLokPanel.Visible = false;
            addKlientoLokPanel.Visible = true;
        }

        private void updateKlientoLokShow_Click(object sender, EventArgs e)
        {
            addKlientoLokPanel.Visible = false;
            updateKlientoLokPanel.Visible = true;
        }

        private void addKlientoLok_Click(object sender, EventArgs e)
        {
            try
            {
                KlientoLokacija kl = new KlientoLokacija();
                kl.salis = addKlientoLokSalis.Text;
                kl.miestas = addKlientoLokMiestas.Text;
                kl.koorindatesX = double.Parse(addKlientoLokKordX.Text);
                kl.koorindatesY = double.Parse(addKlientoLokKordY.Text);
                kl.kliento_Nr = int.Parse(addKlientoLokKlientoNr.Text);

                KlientoLokacija klInserted = klientoLokRep.InsertKlientoLokacija(kl);

                addKlientoLokSalis.Clear();
                addKlientoLokMiestas.Clear();
                addKlientoLokKordX.Clear();
                addKlientoLokKordY.Clear();
                addKlientoLokKlientoNr.Clear();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            MessageBox.Show("Succesfully inserted");
            getKlientoLokDisplay();
        }

        private void updateKlientoLok_Click(object sender, EventArgs e)
        {
            try
            {
                KlientoLokacija kl = new KlientoLokacija();
                kl.salis = updateKlientoLokSalis.Text;
                kl.miestas = updateKlientoLokMiestas.Text;
                kl.koorindatesX = double.Parse(updateKlientoLokKordX.Text);
                kl.koorindatesY = double.Parse(updateKlientoLokKordY.Text);
                kl.kliento_Nr = int.Parse(updateKlientoLokKlientoNr.Text);
                kl.kliento_Lok_Id = int.Parse(updateKlientoLokLokId.Text);

                klientoLokRep.UpdateKlientoLokacija(kl);

                updateKlientoLokSalis.Clear();
                updateKlientoLokMiestas.Clear();
                updateKlientoLokKordX.Clear();
                updateKlientoLokKordY.Clear();
                updateKlientoLokKlientoNr.Clear();
                updateKlientoLokLokId.Clear();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            MessageBox.Show("Succesfully updated");
            getKlientoLokDisplay();
        }

        private void getKlientoLokDisplay()
        {
            try
            {
                DataTable dta = klientoLokRep.displayKlientoLokacija();
                dataGridView4.DataSource = dta;


                DataTable dta1 = klientoRep.displayClients();
                dataGridView3.DataSource = dta1;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void deleteKlientoLok_Click(object sender, EventArgs e)
        {
            try
            {
                KlientoLokacija klok = new KlientoLokacija();
                klok.kliento_Lok_Id = int.Parse(deleteKlientoLokLokId.Text);

                klientoLokRep.DeleteKlientoLok(klok);


                deleteKlientoLokLokId.Clear();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            MessageBox.Show("Deleted succesfully");
            getKlientoLokDisplay();
        }



        private void deleteTransLok_Click(object sender, EventArgs e)
        {
            try
            {
                Lokacija klok = new Lokacija();
                klok.lokacijos_Id = int.Parse(deleteTransLokLokId.Text);
                transLokRep.DeleteLokacija(klok);


                deleteTransLokLokId.Clear();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            MessageBox.Show("Deleted succesfully");
            getTransLokDisplay();
        }
    }
}
