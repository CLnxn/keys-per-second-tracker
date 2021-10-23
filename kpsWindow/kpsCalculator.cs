using System;
using System.Collections.Generic;
using System.Threading;
using System.Windows.Forms;
using System.Diagnostics;
namespace kpsWindow
{
    class kpsCalculator
    {
       

        public static double kps, maxkps, keysPerNs, avgkps = 0;
      
        public double totalkeys = 0;

        public List<double> kpsList = new List<double>();
        public List<double> tkpsList = new List<double>();
       
        public Queue<double> kpsQ = new Queue<double>();
        
        public int tmaxSize = 100; //default size of list inclusive of zero index. 
        public int maxSize = 100;

        
        int samplesize = 3;  //number of acceptable in-a-row zeros
        int resetAvgSize = 20; //number of accpetable in-a-row zeros before resetting avgkps (= acceptable seconds of no key press)
        private kpsForm sform;
     

        private LabelHandler kpsLh, maxkpsLh, avgkpsLh;
       


        public delegate void upLabel();
        upLabel update;



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

            update = updateLabels;
            Thread thread = new Thread(() => scheduledDump(0));
            Thread threadloop = new Thread(loopUpdate);
            threadGroup.Add(thread);
           threadGroup.Add(threadloop);
            thread.Start();
            threadloop.Start();


        }
        
        

        private void Sform_FormClosing(object sender, FormClosingEventArgs e)
        {

            forceReset();
            Console.WriteLine(" sform closing & aborted");
        }
        
        public void forceReset() {
            forceReset(false);

        }
        public void forceReset(bool restart) {
            forceReset(restart,true);

        }
        public void forceReset(bool restart, bool resetMax) {
            forceReset(restart,resetMax,true);
        }
        //default param: false,true,true
        public void forceReset(bool restart, bool resetMax,bool resetDList) {
            Console.WriteLine("resetting.");

          
            foreach (Thread t in threadGroup) {
                t.Abort();
            
            }
            threadGroup.Clear();
            tkpsList.Clear();
            if (resetDList) {
                kpsList.Clear();
                
                
            }

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
        

      
        public void updateLabels() {
            kpsLh.configureKpsLabel();
            maxkpsLh.configureHighestKpsLabel();
            avgkpsLh.configureAvgKpsLabel();
          
        }

        public void loopUpdate() {


            int sampleT = 25;
            double localkps = 0;
            
            if (kpsQ.Count == 0)
            {
                for (int i = 0; i < sampleT;i++) {
                    kpsQ.Enqueue(0);

                }
               
            }
            // var vlist = loopList; 

            
            
          
            while (true)
            {
                
                Thread.Sleep(1000/sampleT);
               
                
                //Console.WriteLine("running");
                
                      kpsQ.Enqueue(keysPerNs);
                      double removeVal = kpsQ.Dequeue();
                  localkps += keysPerNs;
                  localkps -= removeVal;
                  //Console.WriteLine(localkps + " kps//kpns: " + keysPerNs);

                  kps = Math.Round(localkps, 1);

                  if (kps > maxkps)
                  {
                      maxkps = kps;

                  }

                  if (sform.InvokeRequired && !kpsbuttonHandler.inConfigMode)
                  {
                      sform.Invoke(update);
                  }
                  keysPerNs = 0;


                  //localkps = 0;
              

  
            }

        }

     

        public void scheduledDump(int threadno) {
          
            
            //try {

                
                int setno = threadno;
                int baseno = 10000;
             
                for (int i = 1; i<baseno+1;i++) {

                
                    Thread.Sleep(1000);

                // kps = keysPerNs/1;




                //  Console.WriteLine($"Thread #{threadno+1} // iteration #" + (threadno * baseno + i));

                    double kpsinstant = kps;
               // Console.WriteLine( "kpsinstant: " +kpsinstant);
                
                    totalkeys += kpsinstant;
                    kpsList.Add(kpsinstant);
                    tkpsList.Add(kpsinstant);
                
                    
                if (kpsList.Count > maxSize) {
                        kpsList.RemoveRange(0,kpsList.Count- maxSize);
                      
                   
                }
                if (tkpsList.Count > maxSize) {

                    tkpsList.RemoveRange(0, tkpsList.Count - maxSize);
                }

               


                if (tkpsList.Count >= samplesize) {

                    int k = 0; // currently number of seconds of no keypress (in-a-row zero kps)
                   
                    var kList = tkpsList;
                        
                        for (int j=0; j< tkpsList.Count;j++) {

                            if (kList[tkpsList.Count - 1 - j] == 0)
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
                            
                            if (k >= resetAvgSize && !kpsbuttonHandler.inPlayMode) 
                            {
                                
                                Thread temp = new Thread(() => forceReset(true,false,false));
                                temp.Start(); 

                            }
                            

                        }
                    try
                    {
                        avgkps = Math.Round(totalkeys / (setno * baseno + i), 1);

                    }
                    catch (Exception e) { Console.WriteLine(e.StackTrace);
                    }


                    
                    }


                    

                  
                
                //do stuff after every 1s:


                try
                    {
                        if (sform.InvokeRequired && !kpsbuttonHandler.inConfigMode) {
                            //sform.Invoke(update);
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

            //} catch (Exception ex) {
               // Console.WriteLine(ex.StackTrace);
                   // Thread.CurrentThread.Abort();

               
                        
                

           
           // }


           
        
        }

        




    }
}
