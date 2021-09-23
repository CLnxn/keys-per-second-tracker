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

        public List<double> kpsList = new List<double>();
        public int maxSize = 11; //size of list inclusive of zero index. 
        int samplesize = 3;  //number of acceptable in-a-row zeros
        int resetAvgSize = 10; //number of accpetable in-a-row zeros before resetting avgkps
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
           


            this.kpsLh = sform.Bhandler.kpsLh;
            this.maxkpsLh = sform.Bhandler.maxkpsLh;
            this.avgkpsLh = sform.Bhandler.avgkpsLh;


            Thread thread = new Thread(() => scheduledDump(0));
            threadGroup.Add(thread);
            thread.Start();


        }
        
        

        private void Sform_FormClosing(object sender, FormClosingEventArgs e)
        {

            forceReset(true,false);
            Console.WriteLine(" sform closing & aborted");
        }


        public void forceReset(bool resetMax, bool restart) {
            Console.WriteLine("resetting.");

          var threads = this.threadGroup;
            foreach (Thread t in threads) {
                t.Abort();
            
            }
            threadGroup.Clear();
            

            kpsList.Clear();
         
            keysPerNs = 0;
            kps = 0;
            totalkeys = 0;
            if (resetMax) {
                maxkps = 0;
            }
            avgkps = 0;
            sform.FormClosing -= Sform_FormClosing;
            if (restart) {
                start();
                Console.WriteLine("resetting done.");
            }
           

        }
        

      
        private void updateLabels() {
            kpsLh.configureKpsLabel();
            maxkpsLh.configureHighestKpsLabel();
            avgkpsLh.configureAvgKpsLabel();
          
        }
        

     

        public void scheduledDump(int threadno) {
          
            
            try {

                upLabel update = updateLabels;
                int setno = threadno;
                int baseno = 10000;
             
                for (int i = 1; i<baseno+1;i++) {

                  
                    Thread.Sleep(1000);
                  
                    kps = keysPerNs/1;

                    totalkeys += kps;


                    Console.WriteLine($"Thread #{threadno+1} // iteration #" + (threadno * baseno + i));

                    if (0<kps && kps<1 ) {
                        kps = 1;
                    }



                    kpsList.Add(kps);
                   
                    if (kpsList.Count > maxSize) {
                        kpsList.RemoveRange(0,kpsList.Count- maxSize);
                    }
                   
                    if (kpsList.Count >= samplesize) {
                        int k = 0;
                       
                        var kList = kpsList;
                        
                        for (int j=0; j< kpsList.Count;j++) {

                            if (kList[kList.Count - 1 - j] == 0)
                            {

                                k++;

                            }
                            else { 
                                break;
                            }
                        


                        }
                        // if there are at least samplesize zeros in a row, avgkps calculation will ignore the current zero and the totaltime denominator is unchanged.
                        if (k >= samplesize)
                        {
                            i--;
                            if (k == resetAvgSize) {
                                Thread temp = new Thread(() => forceReset(false, true));
                                temp.Start();
                                     
                            }

                        }
                        
                           
                            avgkps = Math.Round(totalkeys / (setno * baseno + i), 1);
                        


                    
                    }


                    

                    if (kps>maxkps) {
                        maxkps = kps;

                    }


                    //do stuff after every 1s:


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
