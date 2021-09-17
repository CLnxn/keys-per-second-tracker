using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Windows.Forms;

namespace kpsWindow
{
    class kpsCalculator
    {
        public static int keysPerNs = 0;

        public static double kps = 0;
        public static double maxkps = 0;
        private kpsForm sform;
        private static bool isCancelled = false;
        List<Thread> threadGroup = new List<Thread>();
        
        public kpsCalculator(kpsForm form) {
            sform = form;
           
            sform.FormClosing += Sform_FormClosing;
            sform.Shown += Sform_FormShowing;
          
           Thread thread = new Thread(() => scheduledDump(0));
            threadGroup.Add(thread);
            thread.Start();


        }

        private void Sform_FormClosing(object sender, FormClosingEventArgs e)
        {

            var threads = this.threadGroup;
            foreach (Thread t in threads) {
                t.Abort();
            
            }
            Console.WriteLine(" sform closing & aborted" );
            isCancelled = true;
            keysPerNs = 0;
            kps = 0;
            maxkps = 0;
            
        }
     
        private void Sform_FormShowing(object o, EventArgs e) {
            isCancelled = false;
            Console.WriteLine("form displayed");
        }

        public void scheduledDump(int threadno) {
            try {
                for (int i = 0; i<1;i++) {

                    Thread.Sleep(1000);
                    if (isCancelled == true)
                    {
                        Thread.CurrentThread.Abort();

                    }
                    kps = keysPerNs/1;
                   
                    if (0<kps && kps<1 ) {
                        kps = 1;
                    }

                    if (kps>maxkps) {
                        maxkps = kps;

                    }

                    Console.WriteLine("i am alive #" + threadno);
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
