using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows;
using SystemColors = System.Drawing.SystemColors;
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
        private LabelHandler kpsLh;
        private LabelHandler maxkpsLh;

        private kpsGraphics kpsGraphics;

        public kpsbuttonHandler(kpsForm form)
        {

            this.form = form;
            this.noOfKeys = form.noOfKeys;



            tb = new TextBox();


            // form.KeyDown += new KeyEventHandler(onKeydown);
            // form.KeyUp += new KeyEventHandler(onKeyup);
            form.Select();
            kpsLh = new LabelHandler(form, false);
            maxkpsLh = new LabelHandler(form, true);
           

        }


        public kpsbuttonHandler(kpsGraphics kpsGraphics) : this(kpsGraphics.getForm()) {

            this.kpsGraphics = kpsGraphics;
            

        }


       



        public void initializeButtonKeys()
        {




            int centreX = (int)form.width / 2;
            int centreY = (int)form.height / 2;
            switch (noOfKeys)
            {
                case 4:
                    setKeys();
                    keys.Add(Keys.X);
                    keys.Add(Keys.C);
                    keys.Add(Keys.N);
                    keys.Add(Keys.M);

                    setKeyMap();
                    var vButtons = hmap.Values.ToList();
                    var vKeys = hmap.Keys.ToList();

                    for (int i = 0; i < 4; i++)
                    {
                        Button button = vButtons[i];

                        button.Text = vKeys[i].ToString();

                        //  button.KeyDown += new KeyEventHandler(onKeydown);
                        //  button.KeyUp += new KeyEventHandler(onKeyup);

                        button.Location = new System.Drawing.Point(centreX + (i - 2) * button.Size.Width, centreY);
                        form.Controls.Add(button);
                    }


                    break;
                case 7:
                    setKeys();
                    keys.Add(Keys.S);
                    keys.Add(Keys.D);
                    keys.Add(Keys.F);
                    keys.Add(Keys.Space);
                    keys.Add(Keys.J);
                    keys.Add(Keys.K);
                    keys.Add(Keys.L);






                    setKeyMap();
                    vButtons = hmap.Values.ToList();
                    vKeys = hmap.Keys.ToList();

                    for (int i = 0; i < 7; i++)
                    {
                        Button button = vButtons[i];

                        button.Text = vKeys[i].ToString();

                       // button.KeyDown += new KeyEventHandler(onKeydown);
                        // button.KeyUp += new KeyEventHandler(onKeyup);


                        button.Location = new System.Drawing.Point(40 * 4 + (i - 4) * button.Size.Width, centreY);
                        form.Controls.Add(button);



                    }


                    break;








            }
        }

        //deprecated
        private List<Button> setButtons()
        {

            for (int i = 0; i < noOfKeys; i++)
            {
                Button button = new Button();
                button.Size = new System.Drawing.Size(40, 40);
                buttons.Add(button);
            }

            return buttons;


        }
        private Dictionary<Keys, Button> setKeyMap()
        {

            var vkeys = keys;


            for (int i = 0; i < noOfKeys; i++)
            {
                Button button = new Button();
                button.Size = new System.Drawing.Size(40, 40);
                button.BackColor = Color.DimGray;
                hmap.Add(vkeys[i], button);


            }

            return hmap;
        }
        //used before setKeyMap() is called.
        private List<Keys> setKeys()
        {

            keys = new List<Keys>();
            return keys;
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
            randbutton.BackColor = Color.White;
            randbutton.Text = "Pressed!";
            /// randbutton.BackgroundImage = new Image();


            //**other particular keydown event method may also be attached to this hook,
            //**so if this event is handled, only this keydown event method will run, the rest will not since this has handled the event. thus handled is set to false
            kpsLh.configureKpsLabel();
            maxkpsLh.configureHighestKpsLabel();


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
                
                randbutton.BackColor = Color.DimGray;
                //Console.WriteLine("is pressed.");
                kpsCalculator.keysPerNs++;





            


        }

    }




    
}
