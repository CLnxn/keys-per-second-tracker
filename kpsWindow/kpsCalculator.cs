using System;
using System.Collections.Generic;
using System.Threading;
using System.Windows.Forms;

namespace kpsWindow
{
    class kpsCalculator
    {
       

        public static double kps, maxkps, keysPerNs, avgkps = 0;
      
        public double totalkeys = 0;

        private kpsForm sform;


        private LabelHandler kpsLh, maxkpsLh, avgkpsLh;


       
        public delegate void upLabel();

        
       

        List<Thread> threadGroup = new List<Thread>();
        
        public kpsCalculator(kpsForm form) {
            sform = form;

            start();
          
        }

        public void start() {

          

            sform.FormClosing += Sform_FormClosing;
            // sform.Shown += Sform_FormShowing;


            this.kpsLh = sform.Bhandler.kpsLh;
            this.maxkpsLh = sform.Bhandler.maxkpsLh;
            this.avgkpsLh = sform.Bhandler.avgkpsLh;


            Thread thread = new Thread(() => scheduledDump(0));
            threadGroup.Add(thread);
            thread.Start();


        }
        
        

        private void Sform_FormClosing(object sender, FormClosingEventArgs e)
        {

            forceReset();
        }


        public void forceReset() { 

          var threads = this.threadGroup;
            foreach (Thread t in threads) {
                t.Abort();
            
            }
            threadGroup.Clear();
            Console.WriteLine(" sform closing & aborted" );
         
            keysPerNs = 0;
            kps = 0;
            totalkeys = 0;
            maxkps = 0;
            avgkps = 0;
        
        
        
        }
        

        //object o, EventArgs e
        private void Sform_FormShowing() {
         
            Console.WriteLine("form displayed");
        }
        private void updateLabels() {
            kpsLh.configureKpsLabel();
            maxkpsLh.configureHighestKpsLabel();
            avgkpsLh.configureAvgKpsLabel();
        
        }
        public void scheduledDump(int threadno) {
            try {
                int baseno = 10000;
                for (int i = 1; i<baseno+1;i++) {

                    Thread.Sleep(1000);
                  
                    kps = keysPerNs/1;
                    totalkeys += kps;
                    avgkps = Math.Round(totalkeys / (threadno*baseno + i),1);
                    
                    Console.WriteLine($"Thread #{threadno+1} // iteration #" + (threadno * baseno + i));

                    if (0<kps && kps<1 ) {
                        kps = 1;
                    }

                    if (kps>maxkps) {
                        maxkps = kps;

                    }

                    //do stuff after every 1s:
                    


                    upLabel update = updateLabels;

                    try
                    {
                        if (sform.InvokeRequired && !kpsbuttonHandler.inConfigMode) {
                            sform.Invoke(update);
                        }
                        


                    }
                    catch (Exception e) {

                        Console.WriteLine(e.StackTrace);

                    }
               
                   
                  //  Console.WriteLine(kpsLh.getLabel().Text);
               // maxkpsLh.configureHighestKpsLabel();

                    //reset 
                    
                    keysPerNs = 0;
                  



                }
                scheduledDump(threadno + 1);
               // Thread tcontinued = new Thread(() => scheduledDump(threadno+1));
               // threadGroup.Add(tcontinued);
               // tcontinued.Start();

            } catch (Exception ex) {
                if (ex.InnerException is ThreadInterruptedException)
                {
                    Thread.CurrentThread.Abort();

                }
                else if (ex.InnerException is NullReferenceException)
                {
                    Console.WriteLine("Ending process");
                }
                        
                

           
            }


           
        
        }

        




    }
}
