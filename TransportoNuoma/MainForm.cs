using GMap.NET;
using GMap.NET.MapProviders;
using GMap.NET.WindowsForms;
using GMap.NET.WindowsForms.Markers;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Timers;
using System.Windows.Forms;
using TransportoNuoma.Classes;
using TransportoNuoma.Repositories;
namespace TransportoNuoma
{
    public partial class MainForm : Form
    {

        int h, m, s;
        Klientas klientas;
        KlientoLokacija klientoLokacija;
        List<GMapMarker> gMapOverlayslist = new List<GMapMarker>();
        KlientoLokacijaRepository klientoLokacijaRepository;
        LokacijaRepository lokacijaRepository = new LokacijaRepository();
        TransportRepository transportRepository = new TransportRepository();
        NuomaRepository nuomaRepository = new NuomaRepository();
        List<Transportas> transportList = new List<Transportas>();
        Lokacija transportoLokacija;
        CancellationTokenSource cts;
        Thread th;
        delegate void CancellationThreadHandler();
        System.Timers.Timer t;
        Transportas rezervuotasTransportas;
        RezervacijaRepository rezervacijaRepository;
        Rezervacija rezervacija = new Rezervacija();
        Nuoma nuoma = new Nuoma();

        public MainForm(Klientas klientas)
        {
            InitializeComponent();
            this.klientas = klientas;
            klientoLokacijaRepository = new KlientoLokacijaRepository();
            rezervuotasTransportas = new Transportas();
            rezervacijaRepository = new RezervacijaRepository();
           
            Console.WriteLine(klientas.klientoNr);
            
            transportList = transportRepository.getTransportList();
            Console.WriteLine(transportList.Count);
            if(nuomaRepository.CheckForActiveNuoma(klientas).Item1 == true)
            {               
                nuoma = nuomaRepository.CheckForActiveNuoma(klientas).Item2;
                loadMap(null, nuoma);
            }
            else if (rezervacijaRepository.CheckForActiveRes(klientas).Item1 == true)
            {
                rezervacija = rezervacijaRepository.CheckForActiveRes(klientas).Item2;
                loadMap(rezervacija,null);
            }
            else
            {
                loadMap(null, null);
            }
            
            
        }
        void loadMap(Rezervacija rezervacija, Nuoma nuoma)
        {
            
            gmap.Overlays.Clear();
            gmap.MapProvider = GMapProviders.GoogleMap;

            gmap.Position = new PointLatLng(54.678175, 25.279267);
            gmap.ShowCenter = false;
            gmap.MinZoom = 1;
            gmap.MaxZoom = 100;
            gmap.Zoom = 14;
            createClientMarker();
            addTransMarkers();
            if (rezervacija != null)
            {
                foreach (GMapMarker marker in gMapOverlayslist)
                {
                    if (Convert.ToInt32(marker.Tag) != rezervacija.Transporto_Id)
                    {
                        if (Convert.ToInt32(marker.Tag) != 0)
                        {
                            marker.IsVisible = false;
                        }
                    }
                    
                }
                int time = Convert.ToInt32(rezervacija.rezervacijosPab.TotalSeconds - DateTime.Now.TimeOfDay.TotalSeconds);
                cts = new CancellationTokenSource();
                CancellationToken ct = cts.Token;
                rezervacijosPanel.Visible = true;
                th = new Thread(() => { CountDownMethod(ct, CancellationMethod, time); });
                th.Start();
            }
            else if (nuoma != null)
            {
                foreach (GMapMarker marker in gMapOverlayslist)
                {
                    if (Convert.ToInt32(marker.Tag) != nuoma.transporto_Id)
                    {
                        if (Convert.ToInt32(marker.Tag) != 0)
                        {
                            marker.IsVisible = false;
                        }
                    }
                }
                rezervacijosPanel.Visible = true;
                TimerMethod();

            }
        }
        void addTransMarkers()
        {
            foreach (Transportas transportas in transportList)
            {
                transportoLokacija = lokacijaRepository.getTransportoLokacija(transportas);
                if(rezervacijaRepository.isTransportasTaken(transportas,klientas) == false && nuomaRepository.CheckIfAvailable(transportas, klientas) == true)
                {
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
        //new PointLatLng(54.679341, 25.279297)


        private void gmap_OnMarkerClick(GMapMarker item, MouseEventArgs e)
        {
            if (Convert.ToInt32(item.Tag) != 0)
            {
                Console.WriteLine(String.Format("Marker {0} was clicked.", item.Tag));
                
                
                switch (MessageBox.Show("Ar norite užsirezervuoti šį paspirtuką?",
                  "Vroom vroom.. :)",
                  MessageBoxButtons.YesNo,
                  MessageBoxIcon.Question))
                {
                    case DialogResult.Yes:
                        
                        rezervuotasTransportas = transportRepository.getTransportasByID(Convert.ToInt32(item.Tag));
                        cts = new CancellationTokenSource();
                        //kuriame cancelation token
                        CancellationToken ct = cts.Token;

                        if (rezervacijaRepository.addNewRezervacija(klientas, rezervuotasTransportas, transportoLokacija) == true)
                        {
                            rezervacija = rezervacijaRepository.getLastReservacija(klientas);
                            rezervacijosPanel.Visible = true;
                            th = new Thread(() => { CountDownMethod(ct, CancellationMethod,900); });
                            th.Start();
                            MessageBox.Show("Rezervacija sekmynga!");

                           
                            foreach(GMapMarker marker in gMapOverlayslist)
                            {
                                if(marker.Tag != item.Tag)
                                {
                                    if(Convert.ToInt32(marker.Tag) != 0)
                                    {
                                        marker.IsVisible = false;
                                    }
                                }
                            }
                            
                        }
                        else { MessageBox.Show("Rezervacija nepavyko :("); }
                        break;

                    case DialogResult.No:

                        break;


                }
            }

        }

        private void CountDownMethod(CancellationToken ct, CancellationThreadHandler handler,int time)
        {
            int m, s;


            for (int i = time; i >= 0; i--)
            {
                try
                {
                    this.BeginInvoke((MethodInvoker)delegate
                    {
                        m = i / 60;
                        s = i % 60;

                        timeLabel.Text = string.Format("{0}:{1}", m.ToString().PadLeft(2, '0'), s.ToString().PadLeft(2, '0'));
                    });

                    Thread.Sleep(1000);
                    if (ct.IsCancellationRequested)//jeigu buvo signalas nutraukti tada funkcija yra cancelenima
                    {
                        if (handler != null)
                        {
                            handler();//iskvieciam metoda handler, kuris pakvies Cancellation metoda

                            return;//padarome return, veiksmas bus atsaukiamas
                                   //mes perduosime viska kitai gijai. kuri atliks visus tuos cancelation dalyduks
                        }
                    }

                }
                catch(Exception ex) { Console.WriteLine(ex.Message); }
                
            }
        }
        private void CancellationMethod()
        {
            //atsaukimas ant kitos gijos. tada ta gija savo darba pabaigs
            Thread th = new Thread(() =>
            {
                Console.WriteLine(Thread.CurrentThread.Name + ": " + "cancelling...");
                Console.WriteLine(Thread.CurrentThread.Name + ": " + "cancelled.");
            });
            th.Name = "CancellationThread";
            th.Start();

        }

        private void CancelButton_Click(object sender, EventArgs e)
        {

        }

        private void cancelButton_Click_1(object sender, EventArgs e)
        {
            if(t == null)
            {
                cts.Cancel();
                rezervacijosPanel.Visible = false;
                rezervacijaRepository.CancelRezervacija(klientas);
                foreach (GMapMarker marker in gMapOverlayslist)
                {
                    if (marker.IsVisible != true)
                    {
                        marker.IsVisible = true;
                    }
                }
            }
            else 
            {
                t.Enabled = false;
                rezervacijosPanel.Visible = false;
                nuomaRepository.CancelNuoma(klientas);
                foreach (GMapMarker marker in gMapOverlayslist)
                {
                    if (marker.IsVisible != true)
                    {
                        marker.IsVisible = true;
                    }
                }
            }
            
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if(cts != null) { cts.Cancel();}            
        }

        private void unlockButton_Click(object sender, EventArgs e)
        {
            cts.Cancel();
            TimerMethod();
            t.Start();                     
        }
        public void TimerMethod()
        {
            priceLabel.Text = string.Format("{0}.00€", rezervuotasTransportas.kaina);
            t = new System.Timers.Timer
            {
                Interval = 1000 // sec


            };
            t.Elapsed += OnTimeEvent;
            nuomaRepository.RegisterNewNuoma(klientas, rezervuotasTransportas, rezervacija);

            t.Start();
        }
        private void OnTimeEvent(object sender, ElapsedEventArgs e)
        {

            s += 1;
            if (s == 60)
            {
                s = 0;
                m += 1;
            }
            if (m == 60)
            {
                m = 0;
                h += 1;
            }
            this.BeginInvoke((MethodInvoker)delegate { timeLabel.Text = String.Format("{0}:{1}:{2}", h.ToString().PadLeft(2, '0'), m.ToString().PadLeft(2, '0'), s.ToString().PadLeft(2, '0')); });
            
        }
        
    }
}
