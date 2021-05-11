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
    public partial class AdminKlientasForm : Form
    {
        Klientas klientas;
        UsersRepository usersRep;
        GalimiNusizengimaiRepository galimiNuzRep;
        NusizengimaiRepository nusizRep;
        ApmokejimasRepository apmokRep;

        public AdminKlientasForm(Klientas klientas)
        {
            InitializeComponent();
            this.klientas = klientas;
            usersRep = new UsersRepository();
            galimiNuzRep = new GalimiNusizengimaiRepository();
            nusizRep = new NusizengimaiRepository();
            apmokRep = new ApmokejimasRepository();
        }

        private void getKlientai_Click(object sender, EventArgs e)
        {
            getKlientaiDisplay();
        }

        private void AddKlientaiShow_Click(object sender, EventArgs e)
        {
            updateKlientaiPanel.Visible = false;
            addKlientaiPanel.Visible = true;
        }

        private void updateKlientaiShow_Click(object sender, EventArgs e)
        {
            addKlientaiPanel.Visible = false;
            updateKlientaiPanel.Visible = true;
        }

        private void addKlientas_Click(object sender, EventArgs e)
        {
            try
            {
                Klientas kl = new Klientas();
                kl.vardas = addKlientasVardas.Text;
                kl.pavarde = addKlientasPavarde.Text;
                kl.email = addKlientasEmail.Text;
                kl.kodas = int.Parse(addKlientasAsmKodas.Text);
                kl.slaptazodis = addKlientasSlapt.Text;

                Klientas insertKl = usersRep.RegisterClient(kl);

                addKlientasVardas.Clear();
                addKlientasPavarde.Clear();
                addKlientasEmail.Clear();
                addKlientasAsmKodas.Clear();
                addKlientasSlapt.Clear();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            MessageBox.Show("Succesfully inserted");
            getKlientaiDisplay();
        }

        private void updateKlientas_Click(object sender, EventArgs e)
        {
            try
            {
                Klientas kl = new Klientas();
                kl.vardas = updateKlientasVardas.Text;
                kl.pavarde = updateKlientasPav.Text;
                kl.email = updateKlientasEmail.Text;
                kl.klientoNr = int.Parse(updateKlientasKlientoNr.Text);
                
                usersRep.UpdateKlientas(kl);

                updateKlientasVardas.Clear();
                updateKlientasPav.Clear();
                updateKlientasEmail.Clear();
                updateKlientasKlientoNr.Clear();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            MessageBox.Show("Succesfully updated");
            getKlientaiDisplay();
        }

        private void getKlientaiDisplay()
        {
            try
            {
                //FOR TESTS
                DataTable dta = usersRep.displayClients();
                dataGridView1.DataSource = dta;

                //FOR TESTS
                DataTable dta1 = galimiNuzRep.displayGalimiNusizengimai();
                dataGridView6.DataSource = dta1;

                //FOR TESTS
                DataTable dta2 = apmokRep.displayApmokejimas();
                dataGridView7.DataSource = dta2;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }



        //GALIMI NUSIZ


        private void getNusizengimai_Click(object sender, EventArgs e)
        {
            getNusizengimaiDisplay();
        }


        private void addNusizengimaiShow_Click(object sender, EventArgs e)
        {
            updateNusizPanel.Visible = false;
            addNusizPanel.Visible = true;
        }

        private void updateNusizengimaiShow_Click(object sender, EventArgs e)
        {
            addNusizPanel.Visible = false;
            updateNusizPanel.Visible = true;
        }



        private void addNusizengimai_Click(object sender, EventArgs e)
        {
            try
            {
                Nusizengimai nz = new Nusizengimai();
                DateTime nusizData = DateTime.Parse(addNusizengimaiNusizData.Text);
                nz.nusizengimo_Data = nusizData.Date;
                nz.kliento_Nr = int.Parse(addNusizengimaiKlientoNr.Text);
                Nusizengimai inserted = nusizRep.InsertNusizengimai(nz);

                addNusizengimaiNusizData.Clear();
                addNusizengimaiKlientoNr.Clear();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            MessageBox.Show("Succesfully inserted");
            getNusizengimaiDisplay();
        }

        private void updateNusizengimai_Click(object sender, EventArgs e)
        {
            try
            {
                Nusizengimai nz = new Nusizengimai();
                DateTime nusizData = DateTime.Parse(updateNusizengimaiNusizData.Text);
                nz.nusizengimo_Data = nusizData.Date;
                nz.kliento_Nr = int.Parse(updateNusizengimaiKlientoNr.Text);
                nz.nusizengimai_Id = int.Parse(updateNusizengimaiNusizId.Text);

                nusizRep.UpdateNusizengimai(nz);

                updateNusizengimaiNusizData.Clear();
                updateNusizengimaiKlientoNr.Clear();
                updateNusizengimaiNusizId.Clear();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            MessageBox.Show("Succesfully updated");
        }





        private void getNusizengimaiDisplay()
        {
            try
            {
                DataTable dta = nusizRep.displayNusizengimai();
                dataGridView2.DataSource = dta;


                DataTable dta1 = usersRep.displayClients();
                dataGridView3.DataSource = dta1;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void deleteNusizengimai_Click(object sender, EventArgs e)
        {
            try
            {
                Nusizengimai gl = new Nusizengimai();
                gl.nusizengimai_Id = int.Parse(deleteNusizengimaiNusizId.Text);
                nusizRep.DeleteNusiz(gl);

                deleteNusizengimaiNusizId.Clear();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            MessageBox.Show("Deleted succesfully");
            getGalimiNusizengimaiDisplay();
            getNusizengimaiDisplay();
        }

        private void getGalimiNusiz_Click(object sender, EventArgs e)
        {
            getGalimiNusizengimaiDisplay();
        }

        private void addGalimiNusizShow_Click(object sender, EventArgs e)
        {
            updateGalimiNusizPanel.Visible = false;
            addGalimiNusizPanel.Visible = true;
        }

        private void updateGalimiNusizShow_Click(object sender, EventArgs e)
        {
            addGalimiNusizPanel.Visible = false;
            updateGalimiNusizPanel.Visible = true;

        }

        private void addGalimiNusiz_Click(object sender, EventArgs e)
        {
            try
            {
                GalimiNusizengimai gn = new GalimiNusizengimai();
                gn.nusizengimo_Pavadinimas = addGalimiNusizNusPav.Text;
                gn.nusizengimo_Id = int.Parse(addGalimiNusizNusizId.Text);

                GalimiNusizengimai ins = galimiNuzRep.InsertGalimiNusizengimai(gn);

                addGalimiNusizNusPav.Clear();
                addGalimiNusizNusizId.Clear();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                
            }
            MessageBox.Show("Inserted succesfully");
            getGalimiNusizengimaiDisplay();
        }

        private void updateGalimiNusiz_Click(object sender, EventArgs e)
        {
            try
            {
                GalimiNusizengimai gn = new GalimiNusizengimai();
                gn.nusizengimo_Pavadinimas = updateGalimiNusizNusPav.Text;
                gn.nusizengimo_Id = int.Parse(updateGalimiNusizNusizId.Text);
                gn.nusizengimo_Kodas = int.Parse(updateGalimiNusizNusizKodas.Text);

                galimiNuzRep.UpdateGalimiNusizengimai(gn);

                updateGalimiNusizNusPav.Clear();
                updateGalimiNusizNusizId.Clear();
                updateGalimiNusizNusizKodas.Clear();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            MessageBox.Show("Updated succesfully");
            getGalimiNusizengimaiDisplay();
        }

        private void getGalimiNusizengimaiDisplay()
        {
            try
            {
                DataTable dta = galimiNuzRep.displayGalimiNusizengimai();
                dataGridView5.DataSource = dta;


                DataTable dta1 = nusizRep.displayNusizengimai();
                dataGridView4.DataSource = dta1;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void deleteGalimiNusiz_Click(object sender, EventArgs e)
        {
            try
            {
                GalimiNusizengimai gl = new GalimiNusizengimai();
                gl.nusizengimo_Kodas = int.Parse(deleteGalimiNusizNusizKodas.Text);
                galimiNuzRep.DeleteGalimiNusiz(gl);


                deleteGalimiNusizNusizKodas.Clear();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            MessageBox.Show("Deleted succesfully");
            getGalimiNusizengimaiDisplay();
        }

        private void backButton_Click(object sender, EventArgs e)
        {
            MainFormAdmin admin = new MainFormAdmin(klientas);
            this.Hide();
            admin.ShowDialog();
            this.Close();
        }

        private void deleteKlientas_Click(object sender, EventArgs e)
        {
            try
            {
                Klientas kl = new Klientas();
                kl.klientoNr = int.Parse(deleteKlientasKlientoNr.Text);
                usersRep.DeleteKlientas(kl);

                deleteKlientasKlientoNr.Clear();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            MessageBox.Show("Deleted succesfully");
            getKlientaiDisplay();
        }
    }
}
