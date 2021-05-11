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
    public partial class AdminApmokejimasGrazForm : Form
    {
        Klientas klientas;
        ApmokejimasRepository apmokRep;
        NuomaRepository nuomRep;

        public AdminApmokejimasGrazForm(Klientas klientas)
        {
            InitializeComponent();
            this.klientas = klientas;
            apmokRep = new ApmokejimasRepository();
            nuomRep = new NuomaRepository();

        }

        

        private void getApmokejimas_Click(object sender, EventArgs e)
        {
            getApmokejimasDisplay();
        }

        private void AddApmokejimasShow_Click(object sender, EventArgs e)
        {
            updateApmokejimasPanel.Visible = false;
            addApmokejimasPanel.Visible = true;
        }

        private void updateApmokejimasShow_Click(object sender, EventArgs e)
        {
            addApmokejimasPanel.Visible = false;
            updateApmokejimasPanel.Visible = true;
        }

        private void addApmokejimas_Click(object sender, EventArgs e)
        {
            try
            {
                Apmokejimas ap = new Apmokejimas();
                DateTime apData = DateTime.Parse(addApmokejimasApData.Text);
                ap.saskaitos_Nr = addApmokejimasSasNr.Text;
                ap.apmokejimo_Suma = int.Parse(addApmokejimasApmokSuma.Text);
                ap.nuomos_Nr = int.Parse(addApmokejimasNuomosNr.Text);
                ap.apmok_data = apData.Date;

                Apmokejimas inserted = apmokRep.InsertApmokejimas(ap);

                addApmokejimasApData.Clear();
                addApmokejimasSasNr.Clear();
                addApmokejimasApmokSuma.Clear();
                addApmokejimasNuomosNr.Clear();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            MessageBox.Show("Succesfully inserted");
            getApmokejimasDisplay();
        }

        private void updateApmokejimas_Click(object sender, EventArgs e)
        {
            try
            {
                Apmokejimas ap = new Apmokejimas();
                DateTime apData = DateTime.Parse(updateApmokejimasApData.Text);
                ap.apmok_data = apData.Date;
                ap.apmok_Id = int.Parse(updateApmokejimasApmokNr.Text);

                apmokRep.UpdateApmokejimas(ap);

                updateApmokejimasApData.Clear();
                updateApmokejimasApmokNr.Clear();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            MessageBox.Show("Succesfully updated");
            getApmokejimasDisplay();
        }

        private void getApmokejimasDisplay()
        {
            try
            {
                //FOR TESTS
                DataTable dta = apmokRep.displayApmokejimas();
                dataGridView1.DataSource = dta;

                //FOR TESTS
                DataTable dta1 = nuomRep.displayNuoma();
                dataGridView2.DataSource = dta1;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void deleteApmokejimas_Click(object sender, EventArgs e)
        {
            try
            {
                Apmokejimas gl = new Apmokejimas();
                gl.apmok_Id = int.Parse(deleteApmokejimasApmokNr.Text);
                apmokRep.DeleteApmokejimas(gl);

                deleteApmokejimasApmokNr.Clear();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            MessageBox.Show("Deleted succesfully");
            getApmokejimasDisplay();
        }

        private void backButton_Click(object sender, EventArgs e)
        {
            MainFormAdmin admin = new MainFormAdmin(klientas);
            this.Hide();
            admin.ShowDialog();
            this.Close();
        }
    }
}
