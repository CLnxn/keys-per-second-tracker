using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;
using Application = System.Windows.Forms.Application;

namespace kpsWindow
{
    class main
    {

       
        [STAThread]
        public static void Main() {
            //i need to write a file handler
          
           // Application.Run(new kpsForm(4));
            Form form = new kpsForm(4);
            form.ShowDialog();
            //kpsApp app = new kpsApp();
            // app.Run();



        }


       


    }



}
