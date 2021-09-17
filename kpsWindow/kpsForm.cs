
using System.Collections.Generic;
using System.Drawing;

using System.Windows.Forms;

using Application = System.Windows.Forms.Application;




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
        public Image img = Image.FromFile("C:\\Users\\proki\\Downloads\\2dscene.jpg");
        public int noOfKeys;

        private kpsGraphics hgraphics;



        public kpsForm(int noOfKeys)
        {
            this.noOfKeys = noOfKeys;

            Bitmap bmp = new Bitmap("C:\\Users\\proki\\Downloads\\kpsicon.jpg");
           

            this.Icon = Icon.FromHandle(bmp.GetHicon());
           

            pBox = new PictureBox();

         //   this.FormClosing += new FormClosingEventHandler(onFormClosing);
          //      Application.ApplicationExit += new EventHandler(onApplicationExit);
          
             
            InitializeComponents();
            // Subscribe();

         


        }

        internal kpsGraphics Hgraphics { get => hgraphics; set => hgraphics = value; }

        private void InitializeComponents()
        {
           
             Hgraphics = new kpsGraphics(@"C:\Users\proki\Downloads\space.jpg", this.width, this.height, this);

            this.ClientSize = new Size(this.width,this.height);
        
            kpsbuttonHandler bhandler = new kpsbuttonHandler(this);
            kpsCalculator kpsc = new kpsCalculator(this);
            bhandler.initializeButtonKeys();
            hookManager hookM = new hookManager(this, bhandler);
            this.Text = "Kps Logger";

            this.MaximizeBox = false;

          

        }


       

      

    }
}
