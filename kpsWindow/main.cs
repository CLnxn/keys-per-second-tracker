using System;
using System.Windows.Forms;


namespace kpsWindow
{
    class main
    {

       
        [STAThread]
        public static void Main() {
            
            fileHandler fileH = new fileHandler();
         
            kpsForm form = new kpsForm(4);
            form.ShowDialog();

           


        }


       


    }



}
