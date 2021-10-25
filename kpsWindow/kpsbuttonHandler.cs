using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Threading;


using System.Drawing;


namespace kpsWindow
{
    class kpsbuttonHandler
    {

        private int noOfKeys;
        private List<Button> buttons = new List<Button>();
        private List<Keys> keys;

        private Dictionary<Keys, Button> hmap = new Dictionary<Keys, Button>();
        private TextBox tb;

        private kpsForm form;
        public LabelHandler kpsLh, maxkpsLh, configLabel, avgkpsLh;
        private Image img;
        
        private CheckBox cBox;
        private Button graphB,configB, resetB, pModeB, freezeGB, darkButton;

        private bool useImg = true;


        private int bHeight;
        private int bWidth;

        private int configButtonCount = 0;
        public static bool inConfigMode = false;
        public static bool isGraphOpen = false;
        public static bool inPlayMode = true;

        private kpsGraphics kpsGraphics; //only assign this/to this if 2nd overload is used
        public kpsGraph kpsGraph;


        public kpsbuttonHandler(kpsForm form)
        {
            this.bHeight = 40;
            this.bWidth = 40;
            this.form = form;
            this.noOfKeys = form.noOfKeys;
            this.cBox = form.Hgraphics.getCboxWrapper1().getCbox();
            InitLabels(false);

            this.keys = fileHandler.GetKeys(noOfKeys);

            try
            {
                img = Image.FromFile(fileHandler.path + "\\presseffect.png");
                this.img = (Image)new Bitmap(img, new Size(bWidth, bHeight));

              
            }
            catch (Exception) {
             
           
                this.useImg = false;
                
            
            }


           

                    
            tb = new TextBox();


            // form.KeyDown += new KeyEventHandler(onKeydown);
            // form.KeyUp += new KeyEventHandler(onKeyup);
            form.Select();

            
            loadConfigButton();
            loadResetButton();
            loadGraphButton();   //pressing button right before 1st thread starts iterating throws an empty window
            loadPlayModeButton();
            loadFreezeGraphButton();
            loadGUIDarkModeButton();

        }


        public kpsbuttonHandler(kpsGraphics kpsGraphics) : this(kpsGraphics.getForm()) {

            this.kpsGraphics = kpsGraphics;
            

        }








        public void InitLabels(bool reInit) {
            if (reInit)
            {
                /*
                form.Controls.Remove(kpsLh.getLabel());
                form.Controls.Remove(maxkpsLh.getLabel());
                form.Controls.Remove(avgkpsLh.getLabel());
                form.Controls.Remove(configLabel.getLabel());
                */
                form.Kcalc.updateLabels();
                



            }
            else
            {
                kpsLh = new LabelHandler(form, false);
                maxkpsLh = new LabelHandler(form, true);
                avgkpsLh = new LabelHandler(form);
                configLabel = new LabelHandler(form);
            }
        }

        public void reInitButtonKeys() {


            var vButtons = hmap.Values.ToList();

            //delete all the buttons first
            for (int i = 0; i < noOfKeys; i++) {
                form.Controls.Remove(vButtons[i]);
               
            }

            //add back the updated buttons from hmap after setKeymap is called which references an updated keys list updated after the last onconfigKeyup eventhandler is called
            //where configbuttoncount == noOfkeys
           
            initializeButtonKeys();
           
        
        }

        public void initializeButtonKeys()
        {



            setKeyMap();
            Console.WriteLine("reinitialising buttons");
            int centreX = (int)form.width / 2;
            int centreY = (int)form.height / 2;
            switch (noOfKeys)
            {
                case 4:
               
                   
                    var vButtons = hmap.Values.ToList();
                    var vKeys = hmap.Keys.ToList();

                    for (int i = 0; i < 4; i++)
                    {
                      
                        Button button = vButtons[i];

                        button.Text = vKeys[i].ToString();
                        button.Font = new Font(FontFamily.GenericSansSerif, 9);
                        button.ForeColor = Color.LawnGreen;

                        //  button.KeyDown += new KeyEventHandler(onKeydown);
                        //  button.KeyUp += new KeyEventHandler(onKeyup);

                        button.Location = new Point(centreX + (i - 2) * button.Size.Width, centreY);
                        form.Controls.Add(button);
                    }


                    break;
                case 7:
                 




                  
                    vButtons = hmap.Values.ToList();
                    vKeys = hmap.Keys.ToList();

                    for (int i = 0; i < 7; i++)
                    {
                        Button button = vButtons[i];

                        button.Text = vKeys[i].ToString();
                        if (i != 3) {
                            button.Font = new Font(FontFamily.GenericSansSerif, 9);
                        }
                        else { button.Font = new Font(FontFamily.GenericSansSerif, 7); }
                        button.ForeColor = Color.LawnGreen;
                        // button.KeyDown += new KeyEventHandler(onKeydown);
                        // button.KeyUp += new KeyEventHandler(onKeyup);


                        button.Location = new System.Drawing.Point(bWidth * 4 + (i - 4) * button.Size.Width, centreY);
                        form.Controls.Add(button);



                    }


                    break;








            }
        }

        private void loadGUIDarkModeButton() {
            this.darkButton = new Button();
            darkButton.Size = new Size(45, 20);
            darkButton.Text = (LabelHandler.inDarkMode)? "Light":"Dark";

            darkButton.ForeColor = Color.LawnGreen;
            darkButton.BackColor = Color.Black;
            darkButton.Location = new Point(form.width-2*darkButton.Size.Width,2*darkButton.Size.Height);
            darkButton.MouseClick += onMouseClickDMB;
            form.Controls.Add(darkButton);


        }
        private void onMouseClickDMB(Object o, MouseEventArgs e) {
         
            if (!(o is Button)) { return;
            }
            cBox.Select();
            Button darkB = (Button) o;
            if (LabelHandler.inDarkMode)
            {
                form.BackColor = Color.White;
                LabelHandler.inDarkMode = false;
                darkButton.Text = "Dark";

            }
            else {
                form.BackColor = Color.Black;
                LabelHandler.inDarkMode = true;
                darkButton.Text = "Light";
            }

            InitLabels(true);
            
        
        }

        private void loadResetButton()
        {
            
            this.resetB = new Button();
            resetB.Size = new Size(45, 20);
            resetB.Text = "Reset";

            resetB.ForeColor = Color.LawnGreen;
            resetB.BackColor = Color.Black;
           // form.width - resetB.Size.Width
            resetB.Location = new Point(form.width-resetB.Size.Width, 2*resetB.Size.Height);
           
            resetB.MouseClick += onMouseClickReset;

            form.Controls.Add(resetB);


        }

        private void onMouseClickReset(Object o, MouseEventArgs e) {
            Button resetB = null;
            if (o is Button) {
                resetB = (Button)o;
            }

            form.Kcalc.forceReset(true);

           


            this.cBox.Select();

            
        
        
        }
        private Dictionary<Keys, Button> setKeyMap()
        {
            if (hmap.Count != 0)
            {
             
                hmap.Clear();

            }
            var vkeys = keys;
            // Console.WriteLine(keys);
            
                for (int i = 0; i < noOfKeys; i++)
            {
                Button button = new Button();
                button.Size = new Size(bWidth, bHeight);
               button.BackColor = Color.Black;
              
               

                   
                    hmap.Add(vkeys[i], button);
               
            }
            
          
           


            return hmap;
        }



        private void loadGraphButton() {

           graphB = new Button();
          
            graphB.Size = new Size(90, 20);
            graphB.Text = "Graph";

            graphB.ForeColor = Color.LawnGreen;
            graphB.BackColor = Color.Black;
            

            graphB.Location = new Point(0, graphB.Size.Height);

           

            graphB.MouseClick += onGraphButtonClick;

                form.Controls.Add(graphB);
           
        
        
        
        }


        private void onGraphButtonClick(Object o, MouseEventArgs e) {
            // might have to be done on a separate thread
            cBox.Select();
            if (!isGraphOpen && !inConfigMode ) {

                
                Console.WriteLine("opening graph form");
                //when kpsGraph has been opened at least once and thus is not null & graph is frozen
                if (this.kpsGraph != null && kpsGraph.freezeGraph)
                {

                    this.kpsGraph = new kpsGraph(form, true);
                    Console.WriteLine("using prev session");

                }
                else
                {

                    this.kpsGraph = new kpsGraph(form, false);
                    Console.WriteLine("assigning new graph");
                }
                isGraphOpen = true; //if put after instantiating kpsGraph, isGraphOpen will wait for kpsGraph's formclosing event to fire before setting it back to true; problematic
                
                
            }



        }

        

        private void loadPlayModeButton() {
            this.pModeB = new Button();

            pModeB.Size = new Size(45, 20); //45,20 default non kps-button size
                                              // configbutton.Location = new Point(form.width, form.height - configbutton.Height);
            pModeB.Text = "Break";
            pModeB.BackColor = Color.Black;
            pModeB.ForeColor = Color.LawnGreen;
            pModeB.Location = new Point(0, 2*pModeB.Size.Height);

            pModeB.MouseClick += onMouseClickPMB;

            form.Controls.Add(pModeB);



        }

        private void onMouseClickPMB(Object o, MouseEventArgs e) {


            //inPlayMode = true disables resetAvgSize & increases maxSize to 100(seconds)
           
            
            if (!inPlayMode)
            {
                //enter play mode
                pModeB.ForeColor = Color.LawnGreen;
                pModeB.Text = "Break";
                inPlayMode = true;
                
                Console.WriteLine("playmode is true");

                form.Kcalc.tmaxSize = 100;
                

            }
            else {
                //break
                pModeB.ForeColor = Color.LawnGreen;
                pModeB.Text = "Play";
                inPlayMode = false;
                form.Kcalc.tmaxSize = 10;

            }
        
        
        }


        private void loadFreezeGraphButton() {

            this.freezeGB = new Button();

            freezeGB.Size = new Size(90, 20); //45,20 default non kps-button size
                                            // configbutton.Location = new Point(form.width, form.height - configbutton.Height);
            freezeGB.Text = "Freeze";
            freezeGB.BackColor = Color.Black;
            freezeGB.ForeColor = Color.LawnGreen;
            freezeGB.Location = new Point(form.width-freezeGB.Size.Width,  freezeGB.Size.Height);

            freezeGB.MouseClick += onMouseClickFGB;

            form.Controls.Add(freezeGB);

        }
        //one day i need a listener class to dump all the eventhandlers from here into
        private void onMouseClickFGB(Object o , MouseEventArgs e) {
            if (!kpsGraph.freezeGraph)
            {
                kpsGraph.freezeGraph = true;
               
                freezeGB.ForeColor = Color.AliceBlue;
                freezeGB.Text = "Unfreeze";


            }
            else {
                kpsGraph.freezeGraph = false;
                if (kpsGraph != null)
                {
                 
                }
                freezeGB.ForeColor = Color.LawnGreen;
                freezeGB.Text = "Freeze";
            }

        }


        //used before setKeyMap() is called.
        private void loadConfigButton()
        {

             this.configB = new Button();
            configB.Size = new Size(90, 20); //45,20 default non kps-button size
                                                  // configbutton.Location = new Point(form.width, form.height - configbutton.Height);
            configB.Text = "Key Config";
            configB.BackColor = Color.Black;
            configB.ForeColor = Color.LawnGreen;
            configB.Location = new Point(form.width-configB.Size.Width,0);

            configB.MouseClick += onMouseClick;

            form.Controls.Add(configB);

        }

        private void onMouseClick(Object o, MouseEventArgs e)
        {
            if (inConfigMode == false) {
                keys.Clear();
                form.HookM.Unsubscribe(false);

                form.Controls.Remove(kpsLh.getLabel());
                form.Controls.Remove(maxkpsLh.getLabel());
                form.Controls.Remove(cBox); //needs testing
                form.Controls.Remove(avgkpsLh.getLabel());
                form.Controls.Remove(graphB);
                form.Controls.Remove(configB);
                form.Controls.Remove(resetB);
                form.Controls.Remove(pModeB);
                form.Controls.Remove(freezeGB);
                form.Controls.Remove(darkButton);


                configLabel.configurationLabel();
                form.HookM.SubscribeConfig();
                inConfigMode = true;
            }






        }



        public void onKeydown(Object o, KeyEventArgs e)
        {
           
            var vKeys = hmap.Keys.ToList();
            var vButtons = hmap.Values.ToList();

            Button randbutton = new Button();

            if (e.KeyCode == vKeys[0])
            {

                randbutton = vButtons[0];

            }
            else if (e.KeyCode == vKeys[1])
            {
                randbutton = vButtons[1];

            }
            else if (e.KeyCode == vKeys[2])
            {
                randbutton = vButtons[2];

            }
            else if (e.KeyCode == vKeys[3])
            {   

                randbutton = vButtons[3];

            }
            else if (noOfKeys == 7)
            {
                if (e.KeyCode == vKeys[4])
                {

                    randbutton = vButtons[4];

                }
                else if (e.KeyCode == vKeys[5])
                {
                    randbutton = vButtons[5];

                }
                else if (e.KeyCode == vKeys[6])
                {
                    randbutton = vButtons[6];

                }




            }

            //calculations must be done on keyup method to prevent spam increase in kps when holding down key.
            //  randbutton.BackColor = Color.Transparent;
            if (useImg)
            {
                randbutton.BackgroundImage = img;
            }
            else {
                randbutton.ForeColor = Color.White;
            }
            





            //**other particular keydown event method may also be attached to this hook,
            //**so if this event is handled, only this keydown event method will run, the rest will not since this has handled the event. thus handled is set to false
           
            
            

        }

        //event handler 'onkeypress' is only used for configuring keys
        public void onConfigKeyUp(object o, KeyEventArgs e) {

            

            Console.WriteLine(keys);
            Keys eKey = e.KeyCode;
            if (keys.Contains(eKey)) {
                Console.WriteLine("duplicated detected");
                return;
               
            }


            var vbuttons = hmap.Values.ToList();
            keys.Add(eKey);
            vbuttons[configButtonCount].Select();
            configButtonCount++;
            
           // Console.WriteLine(keys);
            Console.WriteLine(eKey);
            

            if (configButtonCount == noOfKeys) {
                Console.WriteLine(configButtonCount);
                form.HookM.unSubscribeConfig();

                form.Controls.Remove(configLabel.getLabel());
                reInitButtonKeys();
                form.Controls.Add(cBox);
                form.Controls.Add(graphB);
                form.Controls.Add(configB);
                form.Controls.Add(resetB);
                form.Controls.Add(pModeB);
                form.Controls.Add(freezeGB);
                form.Controls.Add(darkButton);
                cBox.Select();
                form.Controls.Add(kpsLh.getLabel());
                form.Controls.Add(maxkpsLh.getLabel());
                form.Controls.Add(avgkpsLh.getLabel());
                


                form.HookM.Subscribe();

                fileHandler.updateKeyData(noOfKeys,keys);
                

                configButtonCount = 0;
                inConfigMode = false;
                keys.Clear();

            }
        
        
        
        
        }
        public void onKeyup(object o, KeyEventArgs e)
        {

            var vKeys = hmap.Keys.ToList();
            var vButtons = hmap.Values.ToList();

            Button randbutton = new Button();
            if (!vKeys.Contains(e.KeyCode)) { return; }
            if (e.KeyCode == vKeys[0])
            {
                randbutton = vButtons[0];
                randbutton.Text = vKeys[0].ToString();

            }
            else if (e.KeyCode == vKeys[1])
            {
                randbutton = vButtons[1];
                randbutton.Text = vKeys[1].ToString();
            }
            else if (e.KeyCode == vKeys[2])
            {
                randbutton = vButtons[2];
                randbutton.Text = vKeys[2].ToString();
            }


            else if (e.KeyCode == vKeys[3])
            {
                randbutton = vButtons[3];
                randbutton.Text = vKeys[3].ToString();
            }
            else if (noOfKeys == 7)
            {

                if (e.KeyCode == vKeys[4])
                {

                    randbutton = vButtons[4];
                    randbutton.Text = vKeys[4].ToString();
                }
                else if (e.KeyCode == vKeys[5])
                {
                    randbutton = vButtons[5];
                    randbutton.Text = vKeys[5].ToString();
                }
                else if (e.KeyCode == vKeys[6])
                {
                    randbutton = vButtons[6];
                    randbutton.Text = vKeys[6].ToString();
                }




            }
            
                randbutton.BackColor = Color.Black;
            if (useImg) {
                randbutton.BackgroundImage = null;
            }
            else { randbutton.ForeColor = Color.LawnGreen;
            }
            //Console.WriteLine("is pressed.");
            form.Kcalc.keysPerNs++;





            


        }

      


     

    }




    
}
