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
        private delegate void AddChart();
        private delegate void AddSeries(Series s);
        private AddSeries addSeries;
        private AddChart addChart;
        kpsForm form;
        int maxSize;
        Form dispForm; // used only if createWindow = true
        Chart chart;
        Thread t;
      


        public kpsGraph(kpsForm form, bool createWindow) {

            
            this.chart = new Chart();
            this.form = form;
            this.addChart = addCharttoDisplay;
            this.addSeries = addSeriestoChart;



            this.maxSize = form.Kcalc.maxSize;
            dispForm = new Form();
            dispForm.ClientSize = new Size(280, 150);
            dispForm.FormBorderStyle = FormBorderStyle.FixedToolWindow;
            dispForm.MaximizeBox = false;


            chart.ClientSize = new Size(280, 150);




            initializeChartArea();

            t = new Thread(initializeSeries);
            t.Start();
            











            if (createWindow)
            {

                dispForm.FormClosing += onFormClosing;

                dispForm.ShowDialog();

            }
            form.Select();

        }
        //called only after initialiseseries
        private void addCharttoDisplay() {
            dispForm.Controls.Add(chart);
         

        }
        private void addSeriestoChart(Series s) {
            chart.Series.Clear();
            chart.Series.Add(s);
        }
        private void initializeChartArea() {

            ChartArea cArea = new ChartArea();
            cArea.BackColor = Color.Transparent;


            cArea.AxisX.IntervalType = DateTimeIntervalType.Number;

            cArea.AxisX.Minimum = 0;
            cArea.AxisX.Maximum = maxSize-1;
            cArea.AxisX.Title = "Number Seconds ago";



            cArea.AxisY.IntervalType = DateTimeIntervalType.Number;
            cArea.AxisY.Title = "Kps";

            cArea.AxisY.Minimum = 0;
            cArea.AxisY.Maximum = 90;


            cArea.AxisX.MajorGrid.Enabled = false;
            cArea.AxisX.ArrowStyle = AxisArrowStyle.SharpTriangle;
         

            cArea.AxisY.ArrowStyle = AxisArrowStyle.SharpTriangle;
            chart.ChartAreas.Add(cArea);
        }



        //chart control is also added to form in here:
        public void initializeSeries() {
            int iterAmt = 10000;

            for (int j = 0; j<iterAmt; j++) {

                Series s = new Series();
                var vlist = form.Kcalc.kpsList;
                for (int i = 0; i < vlist.Count; i++)
                {

                    s.Points.Add(new DataPoint(i, vlist[vlist.Count - 1 - i]));


                }

                s.ChartType = SeriesChartType.Line;
                s.MarkerStyle = MarkerStyle.Circle;
                s.Color = Color.Black;
                
                if (chart.InvokeRequired)
                {
                    chart.Invoke(addSeries, new Object[] { s });
                }
                else {
                    addSeriestoChart(s);

                }
                if (dispForm.InvokeRequired) {
                    dispForm.Invoke(addChart);
                }
                Thread.Sleep(1000);
                Console.WriteLine("graph thread #" + j);
                while (freezeGraph)
                {
                   
                    //Console.WriteLine("graph is frozen"); //gg console spam lmao
                }
            }
          
                initializeSeries();
            
        }

        

        public void onFormClosing(Object o, FormClosingEventArgs e) {
            kpsbuttonHandler.isGraphOpen = false;
            Console.WriteLine("Closing  display form");

            this.t.Abort();
        
        
        }

      



    }


    
}
