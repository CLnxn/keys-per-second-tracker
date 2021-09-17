using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;

namespace kpsWindow
{
    class kpsGraphics
    {
        private Bitmap bgImg;
        //private HashSet<Bitmap> keyImgMap;
        private kpsForm form;

        private cBoxWrapper cBw;
        
        
        public kpsGraphics(string imgpath,int width, int height, kpsForm form) {
            bgImg = new Bitmap(Image.FromFile(imgpath), new Size(width,height)); 
            this.form = form;
            
            displayDefBg();
            loadCheckBox();
        
        }

        private void displayDefBg() {
            if (bgImg != null)
            {
                //Console.WriteLine("img not null");
               
            }
            else { //Console.WriteLine("img is null");
                   }
            
            form.BackgroundImage = (Image)bgImg;
            
            
                form.pBox.Image = (Image) bgImg;

        }
        private void loadCheckBox() {
            this.cBw = new cBoxWrapper(form);

        }

        public cBoxWrapper getCboxWrapper() {

            return this.cBw;

        }

        public kpsForm getForm() {

            return this.form;

        }


       




     
        



    }
}
