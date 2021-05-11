using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using TransportoNuoma.Classes;
using TransportoNuoma.Repositories;
using GMap.NET;
using GMap.NET.MapProviders;
using GMap.NET.WindowsForms;
using GMap.NET.WindowsForms.Markers;
namespace TransportoNuoma
{
    public partial class MainFormWorker : Form
    {
        Klientas klientas;
        KlientoLokacija klientoLokacija = new KlientoLokacija();
        List<GMapMarker> gMapOverlayslist = new List<GMapMarker>();
        KlientoLokacijaRepository klientoLokacijaRepository = new KlientoLokacijaRepository();
        LokacijaRepository lokacijaRepository = new LokacijaRepository();
        TransportRepository transportRepository = new TransportRepository();
        List<Transportas> transportList = new List<Transportas>();
        Lokacija transportoLokacija = new Lokacija();
        PakrovimasRepository pakrovimasRep;
        TransTestRepository transTestRep;

        public MainFormWorker(Klientas klientas)
        {
            InitializeComponent();
            this.klientas = klientas;
            pakrovimasRep = new PakrovimasRepository();
            transTestRep = new TransTestRepository();
            transportList = transportRepository.getTransportList();
            LoadMap();
            createClientMarker();
            addTransMarkers();
        }
        void LoadMap()
        {
            gmap.Overlays.Clear();
            gmap.MapProvider = GMapProviders.GoogleMap;

            gmap.Position = new PointLatLng(54.678175, 25.279267);
            gmap.ShowCenter = false;
            gmap.MinZoom = 1;
            gmap.MaxZoom = 100;
            gmap.Zoom = 14;

        }
        public void createClientMarker()
        {
            klientoLokacija = klientoLokacijaRepository.GetKlientoLokacija(klientas);

            GMapOverlay markers = new GMapOverlay("markers");
            GMapMarker marker = new GMarkerGoogle(
               new PointLatLng(klientoLokacija.koorindatesX, klientoLokacija.koorindatesY),
                    GMarkerGoogleType.red);
            marker.Tag = gMapOverlayslist.Count;
            gMapOverlayslist.Add(marker);
            markers.Markers.Add(marker);
            gmap.Overlays.Add(markers);
            marker.ToolTipText = "hello " + klientas.vardas;
            marker.ToolTipMode = MarkerTooltipMode.OnMouseOver;
        }
        void addTransMarkers()
        {
            foreach (Transportas transportas in transportList)
            {
                transportoLokacija = lokacijaRepository.getTransportoLokacija(transportas);
                
                    GMapOverlay markers = new GMapOverlay("markers");
                    GMapMarker marker = new GMarkerGoogle(
                    new PointLatLng(transportoLokacija.koordinatesX, transportoLokacija.koordinatesY),
                        GMarkerGoogleType.green);

                    marker.Tag = transportas.transporto_Id;
                    gMapOverlayslist.Add(marker);
                    markers.Markers.Add(marker);
                    gmap.Overlays.Add(markers);
                    marker.ToolTipText = String.Format("\nPaspirtuko numeris: {0}\nPaspirtuko spalva: {1}\nPaspirtuko kaina: {2}", transportas.transporto_Nr, transportas.spalva, transportas.kaina);
                    marker.ToolTipMode = MarkerTooltipMode.OnMouseOver;

                
            }
        }

        private void workerButton_Click(object sender, EventArgs e)
        {
            if (!panel2.Visible) { panel2.Visible = true; }
            else { panel2.Visible = false; }
        }

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
            getPakrovimasDisplay();
            MessageBox.Show("Succesfully inserted");
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
                DataTable dta1 = transTestRep.displayTransTest();
                dataGridView5.DataSource = dta1;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }  
}
