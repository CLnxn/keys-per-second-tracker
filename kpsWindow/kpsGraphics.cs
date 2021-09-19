using System;
using System.Drawing;


namespace kpsWindow
{
    class kpsGraphics
    {
        private Bitmap bgImg;
        //private HashSet<Bitmap> keyImgMap;
        private kpsForm form;

        private cBoxWrapper cBw1;

        private cBoxWrapper cBw2;
        
        
        public kpsGraphics(string imgpath,int width, int height, kpsForm form) {

                this.form = form;
            try
            {
                bgImg = new Bitmap(Image.FromFile(imgpath), new Size(width, height)); //throws error if bgimg is missing
                

                displayDefBg();
            }
            catch (Exception e) {
                //maybe implement a default bgimg.jpg || throw error message popup
            }

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
            this.cBw1 = new cBoxWrapper(form, "7 keys");

        }
     
     

        public kpsForm getForm() {

            return this.form;

        }

        public cBoxWrapper getCboxWrapper1()
        {

            return this.cBw1;
        }



       


       




     
        



    }
}
