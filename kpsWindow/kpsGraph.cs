using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Windows.Forms.DataVisualization.Charting;
using System.Windows.Forms;
using System.Drawing;

namespace kpsWindow
{
    class kpsGraph
    {

        public static bool freezeGraph = false;
        public static Series storeS;
        private bool usePrevSession;
      //  public static kpsGraph prevGraph;


        private delegate void AddChart();
        private delegate void AddSeries(Series s);
        private delegate void ChangeSMarker();
        private delegate void AddArea();

        private ChangeSMarker changeMarker;
        private AddSeries addSeries;
        private AddChart addChart;
        private AddArea addArea;

        public ChartArea cArea;
        public Series series;
        kpsForm form;
        int tmaxSize;
        Form dispForm; // used only if createWindow = true
        Chart chart;
        public Thread t;
        Thread t1;
        public List<Thread> threads = new List<Thread>(); //might be useless
        private List<Thread> allThreads = new List<Thread>();



        public kpsGraph(kpsForm form, bool usePrevSession) {
            this.usePrevSession = usePrevSession;
            this.form = form;

            this.addChart = addCharttoDisplay;
            this.addSeries = addSeriestoChart;
            this.addArea = addAreatoChart;
            this.changeMarker = changemarker;
           
            this.tmaxSize = form.Kcalc.tmaxSize;
            dispForm = new Form();
            dispForm.ClientSize = new Size(280, 150);
            dispForm.FormBorderStyle = FormBorderStyle.FixedToolWindow;
            dispForm.MaximizeBox = false;
            dispForm.Text = form.noOfKeys + "-key Graph";
            this.chart = new Chart();
            
            initializeChartArea();



           


            chart.ClientSize = new Size(280, 150);




            

            t = new Thread(initializeSeries);
            threads.Add(t);
            allThreads.Add(t);
            t.Start();




            t1 = new Thread(startDispForm);
            allThreads.Add(t1);
            t1.Start();



            form.FormClosing += onkpsFormClosing;



           
            form.Select();
            Console.WriteLine("finished inst");

        }
        //called only after initialiseseries
        private void addCharttoDisplay() {
            dispForm.Controls.Add(chart);


        }
        
        private void waitToinitSeries() {
            while (freezeGraph) { 
            
            
            }

            initializeSeries();

        
        }

        private void startDispForm() {
                dispForm.FormClosing += onFormClosing;
                
                dispForm.ShowDialog();

            
            Console.WriteLine("threads supposedly cleared.");
        }

        private void addAreatoChart() {
          
            this.cArea.AxisX.Maximum = form.Kcalc.tmaxSize;
            if (chart.ChartAreas.Count != 0) {
                chart.ChartAreas.Clear();
            }
         //  chart.ChartAreas.Clear();
           chart.ChartAreas.Add(this.cArea);

        }
        private void addSeriestoChart(Series s) {
          
                chart.Series.Clear();
                chart.Series.Add(s);
           
        }

    
        private void changemarker() {
            if (!kpsbuttonHandler.inPlayMode) {
                series.MarkerStyle = MarkerStyle.Square;
                return;
            }

            series.MarkerStyle = MarkerStyle.None;
        
        }
        private void initializeChartArea() {
            chart.ChartAreas.Clear();
            this.cArea = new ChartArea();
            cArea.BackColor = Color.Transparent;


            cArea.AxisX.IntervalType = DateTimeIntervalType.Number;

            cArea.AxisX.Minimum = 0;
            cArea.AxisX.Maximum = tmaxSize;
            cArea.AxisX.Title = "Number Seconds ago";



            cArea.AxisY.IntervalType = DateTimeIntervalType.Number;
            cArea.AxisY.Title = "Kps";

            cArea.AxisY.Minimum = 0;
            cArea.AxisY.Maximum = 90;


            cArea.AxisX.MajorGrid.Enabled = false;
            cArea.AxisX.ArrowStyle = AxisArrowStyle.SharpTriangle;
         

            cArea.AxisY.ArrowStyle = AxisArrowStyle.SharpTriangle;
           // chart.ChartAreas.Add(cArea);
          
          

        }


      
        //chart control is also added to form in here:
        public void initializeSeries() {
            int iterAmt = 10000;

            for (int j = 0; j<iterAmt; j++) {


                
                if (!usePrevSession)
                {
                    this.series = new Series();
                    var vlist = form.Kcalc.kpsList;
                    for (int i = 0; i < vlist.Count; i++)
                    {

                        series.Points.Add(new DataPoint(i, vlist[vlist.Count - 1 - i]));


                    }

                    series.ChartType = SeriesChartType.Line;
                   
                    //  s.MarkerStyle = MarkerStyle.Circle;

                    series.Color = Color.Black;

                }
                else
                {
                   this.series = storeS;
                    
                }
                switchMarkerOnPlayMode();

                //thread safe calls to control based on their creation thread.
                if (chart.InvokeRequired)
                {
                    chart.Invoke(addSeries, new Object[] { usePrevSession ? storeS : series });
                    chart.Invoke(addArea);
                  //  Console.WriteLine(usePrevSession ? "strue" : "sfalse");
                    }
                    else
                    {
                        addSeriestoChart(usePrevSession? storeS : series);
                        addAreatoChart();
                   // Console.WriteLine(usePrevSession ? "strue" : "sfalse");

                }
                if (dispForm.InvokeRequired)
                {
                    dispForm.Invoke(addChart);

                }
                else {
                    addCharttoDisplay(); 
                }
                while (freezeGraph) { 
                
                
                }

                if (!freezeGraph) {
                    usePrevSession = false;
                }

                    Thread.Sleep(1000);
                    Console.WriteLine("graph thread #" + j);
               
              

            }
          
                initializeSeries();
            
        }


        public void switchMarkerOnPlayMode() {
            if (chart.InvokeRequired) {

                chart.Invoke(changeMarker);
                return; 
            }

            changemarker();
        
        
        }
        

        public void onFormClosing(Object o, FormClosingEventArgs e) {
            kpsbuttonHandler.isGraphOpen = false;
            Console.WriteLine("Closing  display form");
            //form.Bhandler.kpsGraph = null;
            storeS = this.series;
           
         
            
            foreach (Thread th in threads)
            {
                th.Abort();

            }
            threads.Clear();
            
            
        
        
        }


        public void reset() {
            foreach (Thread th in allThreads)
            {
                th.Abort();

            }
            threads.Clear();


        }

        private void onkpsFormClosing(Object o, FormClosingEventArgs e)
        {
            reset();
            Console.WriteLine(" form closing 1");
            //Unsubscribe(true);
            // base.OnFormClosing(e);


        }






    }


    
}
