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
    public partial class MainFormAdmin : Form
    {
        Klientas klientas;
        TransportRepository transportRepos;
        Markes markesRepository;
       
        public MainFormAdmin(Klientas klientas)
        {
            InitializeComponent();
            this.klientas = klientas;
            transportRepos = new TransportRepository();
            markesRepository = new Markes();
        }

        private void getTransport_Click(object sender, EventArgs e)
        {
            getTransportDisplay();
        }

        private void getTransportDisplay()
        {
            try
            {
                DataTable dta = transportRepos.displayTransportas();
                dataGridView1.DataSource = dta;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }

        }
        private void AddTransportShow_Click(object sender, EventArgs e)
        {
            addPanel.Visible = true;
            updatePanel.Visible = false;

            CleanTransport();
        }

        private void updateTransShow_Click(object sender, EventArgs e)
        {
            updatePanel.Visible = true;
            addPanel.Visible = false;

            CleanTransport();
        }

        private void updateButton_Click(object sender, EventArgs e)
        {
            try
            {
                Transportas transportas = new Transportas();
                transportas.kaina = int.Parse(UpdateTransKaina.Text);
                transportas.transporto_Nr = updateTransNr.Text;
                transportas.spalva = updateTransSpalva.Text;
                transportas.transporto_Id = int.Parse(UpdateTransTransId.Text);

                transportRepos.UpdateTransportas(transportas);
                
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            MessageBox.Show("Succesfully updated");
            getTransportDisplay();
        }
        private void addUpdateButton_Click(object sender, EventArgs e)
        {
            try
            {

                    Transportas transportas = new Transportas();
                    transportas.transporto_Nr = transNr.Text;
                    transportas.tipas = transTipas.Text;
                    transportas.spalva = transSpalva.Text;
                    DateTime gamybDate = DateTime.Parse(transGamybM.Text);
                    transportas.gamybos_Metai = gamybDate.Date;
                    transportas.kaina = int.Parse(transKaina.Text);
                    transportas.markes_Id = int.Parse(transMarkesId.Text);
                    transportas.QRCode = transQrKodas.Text;
                    Transportas insertedTransport = transportRepos.InsertTransport(transportas);
                    if (insertedTransport.spalva != null && insertedTransport.spalva != "")
                    {
                        MessageBox.Show("Succesfully inserted");
                    }
                
            }catch(Exception ex)
            {
                Console.WriteLine(ex);
            }
            getTransportDisplay();


        }

        private void CleanTransport()
        {
            transGamybM.Clear();
            transKaina.Clear();
            transMarkesId.Clear();
            transNr.Clear();
            transTipas.Clear();
            transSpalva.Clear();
            transQrKodas.Clear();
            
        }

        private void addShowMarke_Click(object sender, EventArgs e)
        {
            updateMarkePanel.Visible = false;
            addMarkePanel.Visible = true;
        }

        private void updateShowMarke_Click(object sender, EventArgs e)
        {
            addMarkePanel.Visible = false;
            updateMarkePanel.Visible = true;
        }

        private void getMarke_Click(object sender, EventArgs e)
        {
            getMarkeDisplay();
        }

        private void getMarkeDisplay()
        {
            try
            {
                DataTable dta = markesRepository.displayMarkes();
                dataGridView2.DataSource = dta;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void addMarke_Click(object sender, EventArgs e)
        {
            try
            {
                TransportoMarke tm = new TransportoMarke();
                tm.markes_Pav = addMarkesPav.Text;
                TransportoMarke insertedMarke = markesRepository.InsertMarke(tm);

                if(insertedMarke.markes_Pav!=null && insertedMarke.markes_Pav != "")
                {
                    MessageBox.Show("Marke succesfully inserted");
                }
                else
                {
                    MessageBox.Show("Something went wrong");
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            getMarkeDisplay();
        }

        private void updateMarke_Click(object sender, EventArgs e)
        {
            try
            {
                TransportoMarke tm = new TransportoMarke();
                tm.markes_Id = int.Parse(updateMarkesId.Text);
                tm.markes_Pav = updateMarkesPav.Text;
                markesRepository.UpdateMarke(tm);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            getMarkeDisplay();
        }

        private void adminTestingShow_Click(object sender, EventArgs e)
        {
            AdminTestingScreen admin = new AdminTestingScreen(klientas);
            this.Hide();
            admin.ShowDialog();
            this.Close();
        }

        private void adminDraudimasShow_Click(object sender, EventArgs e)
        {
            AdminDraudimasForm admin = new AdminDraudimasForm(klientas);
            this.Hide();
            admin.ShowDialog();
            this.Close();
        }

        private void adminLokacijaShow_Click(object sender, EventArgs e)
        {
            AdminLokacijaForm admin = new AdminLokacijaForm(klientas);
            this.Hide();
            admin.ShowDialog();
            this.Close();
        }

        private void adminNuomaShow_Click(object sender, EventArgs e)
        {
            AdminNuomaForm testingScreen = new AdminNuomaForm(klientas);
            this.Hide();
            testingScreen.ShowDialog();
            this.Close();
        }

        private void adminKlientasShow_Click(object sender, EventArgs e)
        {
            AdminKlientasForm testingScreen = new AdminKlientasForm(klientas);
            this.Hide();
            testingScreen.ShowDialog();
            this.Close();
        }

        private void adminApmokejimasShow_Click(object sender, EventArgs e)
        {
            AdminApmokejimasGrazForm testingScreen = new AdminApmokejimasGrazForm(klientas);
            this.Hide();
            testingScreen.ShowDialog();
            this.Close();
        }

        private void deleteTrasnportas_Click(object sender, EventArgs e)
        {
            try
            {
                Transportas gl = new Transportas();
                gl.transporto_Id = int.Parse(deleteTrasnportasTransId.Text);
                transportRepos.DeleteTransportas(gl);

                deleteTrasnportasTransId.Clear();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            MessageBox.Show("Deleted succesfully");
            getTransportDisplay();
        }

        private void DeleteMarke_Click(object sender, EventArgs e)
        {
            try
            {
                TransportoMarke gl = new TransportoMarke();
                gl.markes_Id = int.Parse(DeleteMarkeMarkesId.Text);
                markesRepository.DeleteMarke(gl);

                DeleteMarkeMarkesId.Clear();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            MessageBox.Show("Deleted succesfully");
            getMarkeDisplay();
        }
    }
}
