﻿using System.Collections.Generic;
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

        private kpsbuttonHandler bhandler;

        private kpsCalculator kpsc;


        public kpsForm(int noOfKeys)
        {
            this.noOfKeys = noOfKeys;

            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            
            this.TopMost = true;
            
            try
            {
                Bitmap bmp = new Bitmap(fileHandler.path + "\\ico.jpg"); //throws exception if ico is missing
                this.Icon = Icon.FromHandle(bmp.GetHicon());
            }
            catch (Exception) {
                    //maybe implement a default ico.jpg || throw error message popup
            
            }
            this.bgpath = fileHandler.path + "\\bgimg.jpg";
            /**
             * code below makes window transparent albeit ugly GUI
             * SetStyle(ControlStyles.SupportsTransparentBackColor, true);
             * this.TransparencyKey = Color.Transparent;
            */
            pBox = new PictureBox();

         //   this.FormClosing += new FormClosingEventHandler(onFormClosing);
          //      Application.ApplicationExit += new EventHandler(onApplicationExit);
          
             
            InitializeComponents();
            // Subscribe();

         


        }

        internal kpsGraphics Hgraphics { get => hgraphics; set => hgraphics = value; }
        internal hookManager HookM { get =>  hookM; set => hookM = value; }

        internal kpsbuttonHandler Bhandler { get => bhandler; set => bhandler = value; }

        internal kpsCalculator Kcalc { get => kpsc; set => kpsc = value; }
        
        private void InitializeComponents()
        {
           
             Hgraphics = new kpsGraphics(this.bgpath, this.width, this.height, this);

            this.ClientSize = new Size(this.width,this.height);
        
           bhandler = new kpsbuttonHandler(this);
            bhandler.initializeButtonKeys();
            HookM = new hookManager(this, bhandler);
           kpsc = new kpsCalculator(this);
          

            

            this.Text = "Kps Logger";

            this.MaximizeBox = false;

          

        }


       // protected override void OnPaintBackground(PaintEventArgs e) { /* Ignore */ }











    }
}
