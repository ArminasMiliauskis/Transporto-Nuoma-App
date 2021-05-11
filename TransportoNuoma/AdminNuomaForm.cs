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
    public partial class AdminNuomaForm : Form
    {
        Klientas klientas;
        NuomaRepository nuomaRep;
        TransportRepository transRep;
        UsersRepository usersRep;
        RezervacijaRepository rezRep;
        LokacijaRepository transLokRep;

        public AdminNuomaForm(Klientas klientas)
        {
            InitializeComponent();
            this.klientas = klientas;
            nuomaRep = new NuomaRepository();
            transRep = new TransportRepository();
            usersRep = new UsersRepository();
            rezRep = new RezervacijaRepository();
            transLokRep = new LokacijaRepository();
        }

        private void backButton_Click(object sender, EventArgs e)
        {
            MainFormAdmin admin = new MainFormAdmin(klientas);
            this.Hide();
            admin.ShowDialog();
            this.Close();
        }

        private void getNuoma_Click(object sender, EventArgs e)
        {
            getNuomaDisplay();
        }

        private void AddNuomaShow_Click(object sender, EventArgs e)
        {
            updateNuomaPanel.Visible = false;
            addNuomaPanel.Visible = true;
        }

        private void updateNuomaShow_Click(object sender, EventArgs e)
        {
            addNuomaPanel.Visible = false;
            updateNuomaPanel.Visible = true;
        }

        private void addNuoma_Click(object sender, EventArgs e)
        {
            try
            {
                
                Nuoma nm = new Nuoma();
                DateTime prDate = DateTime.Today;
                DateTime pabDate = DateTime.Today;
                nm.nuomosPrData = prDate;
                nm.nuomosPradLaik = DateTime.Now.TimeOfDay;
                TimeSpan nmLaikas = new TimeSpan(1,0,0);
                nm.nuomosPabLaik = nm.nuomosPradLaik.Add(nmLaikas);
                nm.nuomosPabData = pabDate;
                nm.transporto_Id = int.Parse(addNuomaTransId.Text);
                nm.kliento_Nr = int.Parse(addNuomaKlientoNr.Text);
                nm.rezId = int.Parse(addNuomaRezId.Text);

                Nuoma insertedNm = nuomaRep.InsertNuoma(nm);

                addNuomaTransId.Clear();
                addNuomaKlientoNr.Clear();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            MessageBox.Show("Succesfully inserted");
            getNuomaDisplay();
        }

        private void updateNuoma_Click(object sender, EventArgs e)
        {
            try
            {
                Nuoma nm = new Nuoma();
                nm.transporto_Id = int.Parse(updateNuomaTransId.Text);
                nm.kliento_Nr = int.Parse(updateNuomaKlientoNr.Text);
                nm.nuomos_Nr = int.Parse(updateNuomaNuomosNr.Text);

                nuomaRep.UpdateNuoma(nm);

                updateNuomaTransId.Clear();
                updateNuomaKlientoNr.Clear();
                updateNuomaNuomosNr.Clear();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            MessageBox.Show("Succesfully updated");
            getNuomaDisplay();
        }

        private void getNuomaDisplay()
        {
            try
            {
                DataTable dta = nuomaRep.displayNuoma();
                dataGridView1.DataSource = dta;

                DataTable dta1 = transRep.displayTransportas();
                dataGridView2.DataSource = dta1;
                //klientai
                
                DataTable dta2 = usersRep.displayClients();
                dataGridView5.DataSource = dta2;

                //rez
                DataTable dta3 = rezRep.displayRezervacija();
                dataGridView6.DataSource = dta3;
                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void getRezervacija_Click(object sender, EventArgs e)
        {
            getRezervacijaDisplay();
        }

        private void addRezervacijaShow_Click(object sender, EventArgs e)
        {
            updateRezervacijaPanel.Visible = false;
            addRezervacijaPanel.Visible = true;
        }

        private void updateRezervacijaShow_Click(object sender, EventArgs e)
        {
            addRezervacijaPanel.Visible = false;
            updateRezervacijaPanel.Visible = true;
        }

        private void addRezervacija_Click(object sender, EventArgs e)
        {
            try
            {
                Rezervacija rezervacija = new Rezervacija();
                TimeSpan rezervacijosLaikas = new TimeSpan(0, 15, 0);
                rezervacija.rezervacijos_Data = DateTime.Today;
                rezervacija.rezervacijosPrad = DateTime.Now.TimeOfDay;
                rezervacija.rezervacijosPab = rezervacija.rezervacijosPrad.Add(rezervacijosLaikas);

                rezervacija.kliento_Id = int.Parse(addRezervacijaKlientoNr.Text);
                rezervacija.lokacijos_Id = int.Parse(addRezervacijaLokId.Text);
                rezervacija.Transporto_Id = int.Parse(addRezervacijaTransId.Text);

                Rezervacija insertedRez = rezRep.InsertRezervacijaAdmin(rezervacija);

                addRezervacijaKlientoNr.Clear();
                addRezervacijaLokId.Clear();
                addRezervacijaTransId.Clear();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            MessageBox.Show("Succesfully inserted");
            getRezervacijaDisplay();
        }

        private void updateRezervacija_Click(object sender, EventArgs e)
        {
            try
            {
                Rezervacija rezervacija = new Rezervacija();

                rezervacija.kliento_Id = int.Parse(updateRezervacijaKlientoNr.Text);
                rezervacija.Transporto_Id = int.Parse(updateRezervacijaTransId.Text);
                rezervacija.rezervacijos_Id = int.Parse(updateRezervacijaRezId.Text);

                rezRep.UpdateRezervacija(rezervacija);

                updateRezervacijaKlientoNr.Clear();
                updateRezervacijaTransId.Clear();
                updateRezervacijaRezId.Clear();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            MessageBox.Show("Succesfully updated");
            getRezervacijaDisplay();
        }

        private void getRezervacijaDisplay()
        {
            try
            {
                DataTable dta = rezRep.displayRezervacija();
                dataGridView4.DataSource = dta;

                DataTable dta1 = transLokRep.displayLokacija();
                dataGridView3.DataSource = dta1;

                DataTable dta2 = usersRep.displayClients();
                dataGridView7.DataSource = dta2;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void deleteRezervacija_Click(object sender, EventArgs e)
        {
            try
            {
                Rezervacija rez = new Rezervacija();
                rez.rezervacijos_Id = int.Parse(deleteRezervacijaRezId.Text);
                int nuomosNr = int.Parse(deleteRezervacijaNuomosNr.Text);
                //PASS NUOMOS NR
                rezRep.DeleteRezervacija(rez, nuomosNr);


                deleteRezervacijaRezId.Clear();
                deleteRezervacijaNuomosNr.Clear();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            MessageBox.Show("Deleted succesfully");
            getRezervacijaDisplay();
            getNuomaDisplay();
        }

        private void DeleteNuoma_Click(object sender, EventArgs e)
        {
            try
            {
                Nuoma nuom = new Nuoma();
                nuom.nuomos_Nr = int.Parse(DeleteNuomaNuomosNr.Text);
                nuomaRep.DeleteNuoma(nuom);


                DeleteNuomaNuomosNr.Clear();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            MessageBox.Show("Deleted succesfully");
            getNuomaDisplay();
        }
    }
}
