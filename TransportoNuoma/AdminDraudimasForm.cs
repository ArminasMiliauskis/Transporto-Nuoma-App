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
    public partial class AdminDraudimasForm : Form
    {
        Klientas klientas;
        DraudimoTiekRepository draudTiekRep;
        DraudimasRepository draudimasRep;
        TransportRepository transRep;

        public AdminDraudimasForm(Klientas klientas)
        {
            InitializeComponent();
            this.klientas = klientas;
            draudTiekRep = new DraudimoTiekRepository();
            draudimasRep = new DraudimasRepository();
            transRep = new TransportRepository();
        }

        private void backButton_Click(object sender, EventArgs e)
        {
            MainFormAdmin admin = new MainFormAdmin(klientas);
            this.Hide();
            admin.ShowDialog();
            this.Close();
        }



        //DRAUDIMO TIEKEJAS

        private void getDraudTiek_Click(object sender, EventArgs e)
        {
            getDraudimasTiekDisplay();
        }

        private void addDraudTiekShow_Click(object sender, EventArgs e)
        {
            updateTiekDraudimasPanel.Visible = false;
            addTiekDraudimasPanel.Visible = true;
        }

        private void updateDraudTiekShow_Click(object sender, EventArgs e)
        {
            addTiekDraudimasPanel.Visible = false;
            updateTiekDraudimasPanel.Visible = true;
        }

        private void addDraudTIek_Click(object sender, EventArgs e)
        {
            try
            {
                DraudimoTiekejai dt = new DraudimoTiekejai();
                dt.pavadinimas = addDraudTiekPav.Text;
                DraudimoTiekejai insertedDt = draudTiekRep.InsertDraudimoTiekejai(dt);

                if (insertedDt.pavadinimas != null && insertedDt.pavadinimas != "")
                {
                    MessageBox.Show("Succesfully inserted");
                }

                addDraudTiekPav.Clear();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            getDraudimasTiekDisplay();

        }

        private void updateDraudTIek_Click(object sender, EventArgs e)
        {
            try
            {
                DraudimoTiekejai dt = new DraudimoTiekejai();
                dt.pavadinimas = updateDraudTiekPav.Text;
                dt.tiekejo_Id = int.Parse(updateTiekDraudTiekId.Text);

                draudTiekRep.UpdateDraudimoTiekejai(dt);

                updateDraudTiekPav.Clear();
                updateTiekDraudTiekId.Clear();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            getDraudimasTiekDisplay();
            MessageBox.Show("Succesfully updated");
            
        }

        private void getDraudimasTiekDisplay()
        {
            try
            {
                //FOR TESTS
                DataTable dta = draudTiekRep.displayDraudimoTiekejai();
                dataGridView1.DataSource = dta;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        



        //DRAUDIMAS


        private void getDraudimas_Click(object sender, EventArgs e)
        {
            getDraudimasDisplay();
        }

        private void AddDraudimasShow_Click(object sender, EventArgs e)
        {
            updateDraudimasPanel.Visible = false;
            addDraudimasPanel.Visible = true;
        }

        private void updateTransShow_Click(object sender, EventArgs e)
        {
            addDraudimasPanel.Visible = false;
            updateDraudimasPanel.Visible = true;
        }


        private void addDraudimas_Click(object sender, EventArgs e)
        {
            try
            {
                Draudimas dr = new Draudimas();
                DateTime pradData = DateTime.Parse(addDraudPradData.Text);
                DateTime pabData = DateTime.Parse(addDraudPabData.Text);
                dr.draudPradData = pradData.Date;
                dr.draudPabData = pabData.Date;
                dr.tiekId = int.Parse(addDraudTiekId.Text);
                dr.Trans_Id = int.Parse(addDraudTransId.Text);
                Draudimas insertedDr = draudimasRep.InsertDraudimas(dr);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            MessageBox.Show("Succesfully inserted");
            getDraudimasDisplay();
        }

        private void updateDraudimas_Click(object sender, EventArgs e)
        {
            try
            {
                Draudimas dr = new Draudimas();
                DateTime pradData = DateTime.Parse(updateDraudPradData.Text);
                DateTime pabData = DateTime.Parse(updateDraudPabData.Text);
                dr.draudPradData = pradData.Date;
                dr.draudPabData = pabData.Date;
                dr.tiekId = int.Parse(updateDraudTiekId.Text);
                dr.Trans_Id = int.Parse(updateDraudTransId.Text);
                dr.draudId = int.Parse(updateDraudDraudId.Text);
                draudimasRep.UpdateDraudimas(dr);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            MessageBox.Show("Succesfully updated");
            getDraudimasDisplay();
        }

        private void getDraudimasDisplay()
        {
            try
            {
                //FOR TESTS
                DataTable dta = draudimasRep.displayDraudimas();
                dataGridView3.DataSource = dta;


                DataTable dta1 = draudTiekRep.displayDraudimoTiekejai();
                dataGridView2.DataSource = dta1;

                DataTable dta2 = transRep.displayTransportas();
                dataGridView4.DataSource = dta2;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void deleteDraudTiek_Click(object sender, EventArgs e)
        {
            try
            {
                DraudimoTiekejai gl = new DraudimoTiekejai();
                gl.tiekejo_Id = int.Parse(deleteDraudTiekTiekId.Text);
                draudTiekRep.DeleteDraudTiek(gl);

                deleteDraudTiekTiekId.Clear();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            MessageBox.Show("Deleted succesfully");
            getDraudimasTiekDisplay();
            getDraudimasDisplay();
            
        }

        private void deleteDraudimas_Click(object sender, EventArgs e)
        {
            try
            {
                Draudimas gl = new Draudimas();
                gl.draudId = int.Parse(deleteDraudimasDraudId.Text);
                draudimasRep.DeleteDraud(gl);

                deleteDraudimasDraudId.Clear();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            MessageBox.Show("Deleted succesfully");
            getDraudimasDisplay();
            getDraudimasTiekDisplay();
            
        }
    }
}
