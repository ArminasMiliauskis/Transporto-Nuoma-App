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
    public partial class AdminTestingScreen : Form
    {
        Klientas klientas;
        TransTestRepository transTestRep;
        TransportRepository transportRep;
        GalimiTestaiRepository galimiTestRep;
        PakrovimasRepository pakrovimasRep;
        public AdminTestingScreen(Klientas klientas)
        {
            InitializeComponent();
            this.klientas = klientas;
            transTestRep = new TransTestRepository();
            transportRep = new TransportRepository();
            galimiTestRep = new GalimiTestaiRepository();
            pakrovimasRep = new PakrovimasRepository();
        }

        private void backButton_Click(object sender, EventArgs e)
        {
            MainFormAdmin admin = new MainFormAdmin(klientas);
            this.Hide();
            admin.ShowDialog();
            this.Close();
        }

        private void getTransport_Click(object sender, EventArgs e)
        {
            try
            {
                getTransTest();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        private void addTransTest_Click(object sender, EventArgs e)
        {
            try
            {
                TransportoTestai test = new TransportoTestai();
                DateTime testoData = DateTime.Parse(addTransTestData.Text);
                test.test_Data = testoData.Date;
                test.trans_Id = int.Parse(addTransTestTransId.Text);
                TransportoTestai insertedTest = transTestRep.InsertTransTest(test);

                addTransTestData.Clear();
                addTransTestTransId.Clear();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            MessageBox.Show("Succesfully inserted");
            getTransTest();
        }

        private void updateButton_Click(object sender, EventArgs e)
        {
            try
            {
                TransportoTestai test = new TransportoTestai();
                DateTime testoData = DateTime.Parse(updateTransTestData.Text);
                test.test_Data = testoData.Date;
                test.trans_Id = int.Parse(updateTransTestTransId.Text);
                test.test_Id = int.Parse(updateTransTestTestId.Text);
                transTestRep.UpdateTransTest(test);

                updateTransTestData.Clear();
                updateTransTestTransId.Clear();
                updateTransTestTestId.Clear();

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            MessageBox.Show("Succesfully updated");
            getTransTest();
        }

        private void AddTransportShow_Click(object sender, EventArgs e)
        {
            updateTransTestPanel.Visible = false;
            addTransTestPanel.Visible = true;

        }

        private void updateTransShow_Click(object sender, EventArgs e)
        {
            addTransTestPanel.Visible = false;
            updateTransTestPanel.Visible = true;
        }


        void getTransTest()
        {
            try
            {
                //FOR TESTS
                DataTable dta = transTestRep.displayTransTest();
                dataGridView1.DataSource = dta;

                //FOR TRANSPORT
                DataTable dta1 = transportRep.displayTransportas();
                dataGridView2.DataSource = dta1;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }


        //GALIMI TEST


        private void addGalimiTestShow_Click(object sender, EventArgs e)
        {
            updateGalimiTestPanel.Visible = false;
            addGalimiTestPanel.Visible = true;
        }

        private void updateGalimiTestShow_Click(object sender, EventArgs e)
        {
            addGalimiTestPanel.Visible = false;
            updateGalimiTestPanel.Visible = true;
        }

        private void getGalimiTest_Click(object sender, EventArgs e)
        {
            try
            {
                getGalimiTestDisplay();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }


        private void addGalimiTest_Click(object sender, EventArgs e)
        {
            try
            {
                GalimiTestai gt = new GalimiTestai();
                gt.testPav = addGalimiTestTestPav.Text;
                gt.testId = int.Parse(addGalimiTestTestId.Text);

                GalimiTestai insertedGalimiTest = galimiTestRep.InsertGalimiTestai(gt);
                if(insertedGalimiTest.testPav!=null && insertedGalimiTest.testPav != "")
                {
                    MessageBox.Show("Succesfully inserted");
                }

                addGalimiTestTestPav.Clear();
                addGalimiTestTestId.Clear();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            getGalimiTestDisplay();
        }

        private void updateGalimiTest_Click(object sender, EventArgs e)
        {
            try
            {
                GalimiTestai gt = new GalimiTestai();
                gt.testPav = updateGalimiTestTestPav.Text;
                gt.testId = int.Parse(updateGalimiTestTestId.Text);
                gt.testoKodas = int.Parse(updateGalimiTestTestoKodas.Text);

                galimiTestRep.UpdateGalimiTestai(gt);

                updateGalimiTestTestPav.Clear();
                updateGalimiTestTestId.Clear();
                updateGalimiTestTestoKodas.Clear();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            MessageBox.Show("Succesfully updated");
            getGalimiTestDisplay();
        }


        void getGalimiTestDisplay()
        {
            try
            {
                //FOR TESTS
                DataTable dta = galimiTestRep.displayGalimiTestai();
                dataGridView4.DataSource = dta;

                //FOR TRANSPORT
                DataTable dta1 = transportRep.displayTransportas();
                dataGridView3.DataSource = dta1;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }





        //PAKROVIMAS

        private void getPakrovimas_Click(object sender, EventArgs e)
        {
            try
            {
                getPakrovimasDisplay();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void addPakrovimasShow_Click(object sender, EventArgs e)
        {
            updatePakrovimasPanel.Visible = false;
            addPakrovimasPanel.Visible = true;
        }

        private void updatePakrovimasShow_Click(object sender, EventArgs e)
        {
            addPakrovimasPanel.Visible = false;
            updatePakrovimasPanel.Visible = true;
        }

        private void addPakrovimas_Click(object sender, EventArgs e)
        {
            try
            {
                Pakrovimas pakrovimas = new Pakrovimas();
                pakrovimas.pakrovimo_Dydis = int.Parse(addPakrovimasPakrovDydis.Text);
                pakrovimas.transporto_Id = int.Parse(addPakrovimasTransId.Text);
                Pakrovimas insertedPakrovimas = pakrovimasRep.InsertPakrovimas(pakrovimas);

                addPakrovimasPakrovDydis.Clear();
                addPakrovimasTransId.Clear();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            MessageBox.Show("Succesfully inserted");
            getPakrovimasDisplay();
            
        }

        private void updatePakrovimas_Click(object sender, EventArgs e)
        {
            try
            {
                Pakrovimas pakrovimas = new Pakrovimas();
                pakrovimas.pakrovimo_Dydis = int.Parse(updatePakrovimasPakrovDyd.Text);
                pakrovimas.transporto_Id = int.Parse(updatePakrovimasTransId.Text);
                pakrovimas.pakrovimo_Nr = int.Parse(updatePakrovimasPakrId.Text);
                pakrovimasRep.UpdatePakrovimas(pakrovimas);

                updatePakrovimasPakrovDyd.Clear();
                updatePakrovimasTransId.Clear();
                updatePakrovimasPakrId.Clear();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            getPakrovimasDisplay();
            MessageBox.Show("Succesfully updated");
        }

        private void getPakrovimasDisplay()
        {
            try
            {
                //FOR TESTS
                DataTable dta = pakrovimasRep.displayPakrovimas();
                dataGridView6.DataSource = dta;

                //FOR TRANSPORT
                DataTable dta1 = transportRep.displayTransportas();
                dataGridView5.DataSource = dta1;
            }catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void deleteTransTest_Click(object sender, EventArgs e)
        {
            try
            {
                TransportoTestai gl = new TransportoTestai();
                gl.test_Id = int.Parse(deleteTransTestTestId.Text);
                transTestRep.DeleteTransTest(gl);

                deleteTransTestTestId.Clear();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            MessageBox.Show("Deleted succesfully");
            getTransTest();
            getGalimiTestDisplay();
        }

        private void deleteGalimTest_Click(object sender, EventArgs e)
        {
            try
            {
                GalimiTestai gl = new GalimiTestai();
                gl.testoKodas = int.Parse(deleteGalTestTestoKodas.Text);
                galimiTestRep.DeleteGalimiTestai(gl);

                deleteGalTestTestoKodas.Clear();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            MessageBox.Show("Deleted succesfully");
            getGalimiTestDisplay();
            getTransTest();
            
        }

        private void DeletePakrovimas_Click(object sender, EventArgs e)
        {
            try
            {
                Pakrovimas gl = new Pakrovimas();
                gl.pakrovimo_Nr = int.Parse(DeletePakrovimasPakrovNr.Text);
                pakrovimasRep.DeletePakrovimas(gl);

                DeletePakrovimasPakrovNr.Clear();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            MessageBox.Show("Deleted succesfully");
            getPakrovimasDisplay();
        }
    }
}
