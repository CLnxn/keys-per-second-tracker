using System.Collections.Generic;
using System.Drawing;
using System;
using System.Windows.Forms;






namespace kpsWindow
{
    public partial class kpsForm : Form
    {

        
        public PictureBox pBox;
        public Label label = new Label();

        public LinkedList<Button> buttons = new LinkedList<Button>();

        public List<Keys> keys = new List<Keys>();

        public  Button button = new Button();
        public int width = 7*40;
        public int height = 150;
    
        private string bgpath;
        public int noOfKeys;

        private kpsGraphics hgraphics;

        private hookManager hookM;




        public kpsForm(int noOfKeys)
        {
            this.noOfKeys = noOfKeys;

            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            try
            {
                Bitmap bmp = new Bitmap(fileHandler.path + "\\ico.jpg"); //throws exception if ico is missing
                this.Icon = Icon.FromHandle(bmp.GetHicon());
            }
            catch (Exception e) {
                    //maybe implement a default ico.jpg || throw error message popup
            
            }
            this.bgpath = fileHandler.path + "\\bgimg.jpg";

            
           

            pBox = new PictureBox();

         //   this.FormClosing += new FormClosingEventHandler(onFormClosing);
          //      Application.ApplicationExit += new EventHandler(onApplicationExit);
          
             
            InitializeComponents();
            // Subscribe();

         


        }

        internal kpsGraphics Hgraphics { get => hgraphics; set => hgraphics = value; }
        internal hookManager HookM { get =>  hookM; set => hookM = value; }
        
        private void InitializeComponents()
        {
           
             Hgraphics = new kpsGraphics(this.bgpath, this.width, this.height, this);

            this.ClientSize = new Size(this.width,this.height);
        
           kpsbuttonHandler bhandler = new kpsbuttonHandler(this);
            kpsCalculator kpsc = new kpsCalculator(this);
            bhandler.initializeButtonKeys();
           HookM = new hookManager(this, bhandler);

            

            this.Text = "Kps Logger";

            this.MaximizeBox = false;

          

        }


       

      

    }
}
