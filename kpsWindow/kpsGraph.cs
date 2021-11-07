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
        private delegate void UpdateColour(bool inDarkMode, bool updateSeriesColour);
        private delegate void UpdateSColour(bool inDarkMode);

        private delegate void CloseDispForm();

        private ChangeSMarker changeMarker;
        private AddSeries addSeries;
        private AddChart addChart;
        private AddArea addArea;
        private UpdateColour updateCColour;
        private UpdateSColour updateSColour;

        private CloseDispForm closeDisp;

        public ChartArea cArea;
        public Series series;
        public Form dispForm;
        kpsForm form;
        int tmaxSize;
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
            this.closeDisp = closeDispForm;
            this.updateCColour = updateGraphColour;
            this.updateSColour = updateSeriesColour;
           
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
            dispForm.Shown += onDispShown;
            dispForm.ShowDialog();

            
           // Console.WriteLine("threads supposedly cleared.");
        }

        private void onDispShown(Object o, EventArgs e) {

       
           dispForm.Focus();
           dispForm.TopMost = true;

        }
        private void updateSeriesColour(bool inDarkMode) {
            if (inDarkMode)
            {
                series.Color = Color.LawnGreen;
            }
            else
            {
                series.Color = Color.Black;
            }
        }
        private void updateGraphColour(bool inDarkMode, bool updateSeriesColour) 
        {
            if (inDarkMode)
            {
                cArea.AxisY.MajorGrid.LineColor = Color.LawnGreen;
                cArea.AxisY.LineColor = Color.LawnGreen;
                cArea.AxisX.LineColor = Color.LawnGreen;
                cArea.AxisX.TitleForeColor = Color.LawnGreen;
                cArea.AxisY.TitleForeColor = Color.LawnGreen;
                cArea.AxisX.LabelStyle.ForeColor = Color.LawnGreen;
                cArea.AxisY.LabelStyle.ForeColor = Color.LawnGreen;
                chart.BackColor = Color.Black;
                
            }
            else
            {
                cArea.AxisY.MajorGrid.LineColor = Color.Black;
                cArea.AxisY.LineColor = Color.Black;
                cArea.AxisX.LineColor = Color.Black;
                cArea.AxisX.TitleForeColor = Color.Black;
                cArea.AxisY.TitleForeColor = Color.Black;
                cArea.AxisX.LabelStyle.ForeColor = Color.Black;
                cArea.AxisY.LabelStyle.ForeColor = Color.Black;
                chart.BackColor = Color.White;
              


            }

            if (updateSeriesColour) 
            {
                this.updateSeriesColour(inDarkMode);
            }
            



        }

        private void addAreatoChart() {
          
            this.cArea.AxisX.Maximum = form.Kcalc.tmaxSize;
            this.cArea.BackColor = Color.Transparent;
            updateGraphColour(LabelHandler.inDarkMode, false);
            if (chart.ChartAreas.Count != 0) {
                chart.ChartAreas.Clear();
            }
        
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
           // cArea.BackColor = Color.Transparent;


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
                        DataPoint dPoint = new DataPoint(i, vlist[vlist.Count - 1 - i]);
                      
                        series.Points.Add(dPoint);
                        

                    }

                    series.ChartType = SeriesChartType.Line;

                    //  s.MarkerStyle = MarkerStyle.Circle;
                  
                }
                else
                {
                   this.series = storeS;
                   
                    
                    Console.WriteLine("USING PREV SESSION");
                    
                }
             
                switchMarkerOnPlayMode();

                //thread safe calls to control based on their creation thread.
                if (chart.InvokeRequired)
                {
                    chart.Invoke(updateSColour, new object[] {LabelHandler.inDarkMode});
                    chart.Invoke(addSeries, new object[] { series });
                    chart.Invoke(addArea);
                  //  Console.WriteLine(usePrevSession ? "strue" : "sfalse");
                    }
                    else
                    {
                        updateSeriesColour(LabelHandler.inDarkMode);
                        addSeriestoChart(series);
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
                if (freezeGraph) {
                    bool prevPlay = kpsbuttonHandler.inPlayMode;
                    bool prevDarkMode = LabelHandler.inDarkMode;
                    while (freezeGraph) {

                        if (prevDarkMode != LabelHandler.inDarkMode) 
                        {
                            if (chart.InvokeRequired) {
                                chart.Invoke(updateCColour, new object[] { LabelHandler.inDarkMode, true}) ;
                            } else {
                                updateGraphColour(LabelHandler.inDarkMode, true);
                            }
                            prevDarkMode = LabelHandler.inDarkMode;
                        }
                        if (prevPlay != kpsbuttonHandler.inPlayMode) {
                            storeS = this.series;
                            usePrevSession = true;
                            break;
                        }

                    }
                   
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
            
            if (dispForm.InvokeRequired) {
                dispForm.Invoke(closeDisp);
                Console.WriteLine("invoked close display.");
            }
            
           
            reset();
           
            Console.WriteLine("kps form  closing in kpsgraph");
            //Unsubscribe(true);
            // base.OnFormClosing(e);


        }

        private void closeDispForm() {
            dispForm.Close();
        
        }




    }


    
}
