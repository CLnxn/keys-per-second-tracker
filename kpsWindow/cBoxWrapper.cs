using System;
using System.Windows.Forms;
using System.Drawing;
namespace kpsWindow
{
    class cBoxWrapper
    {

       private CheckBox cBox;
        kpsForm form;


        




        public cBoxWrapper(kpsForm form, string defText) 
        {
            this.form = form;

            cBox = new CheckBox();

            cBox.Appearance = Appearance.Button;
            cBox.BackColor = Color.Black;
            cBox.ForeColor = Color.LawnGreen;
           
            cBox.Size = new System.Drawing.Size(45,20);
            cBox.AutoCheck = false;
            cBox.Location = new Point(0,0);
            //cBox.Location = new System.Drawing.Point(0,form.height-cBox.Size.Height);
            cBox.AutoSize = false;
            cBox.ThreeState = false;
            cBox.Text = defText;

            // cBox.CheckStateChanged += onCheckedChanged;

            cBox.MouseClick += onMouseClick1;
            //  cBox.CheckedChanged += onCheckedChanged;
            // cBox.KeyPress += onKeyPress;
            //   cBox.KeyDown += onKeyDown;

            form.Controls.Add(cBox);

        }

       

     
        private void onMouseClick1(Object o, MouseEventArgs e)
        {
            CheckBox box = null;

            if (o is CheckBox && o != null)
            {
                box = (CheckBox)o;
            }
            else
            {
                throw new NullReferenceException();
            }


            //4 is default
            if (form.noOfKeys == 7)
            {

                kpsForm nform = new kpsForm(4);
                form.Visible = false;
                form.Close();
                form.Dispose();
                setCboxText(7, nform);
                nform.ShowDialog();


                //    Application.Run(new kpsForm(4));

            }
            else if (form.noOfKeys == 4)
            {
                // form.Close();
                kpsForm nform = new kpsForm(7);
                form.Visible = false;
                form.Close();
                form.Dispose();
                setCboxText(4, nform);
                nform.ShowDialog();
                // Application.Run(new kpsForm(7));


            }
        }

        //event below isnt used as it wont work with autocheck = false; alt+tab auto focuses on checkbox control and if autosize = true,
        //then form will keep switching b/w 7k n 4k modes on spacebar press. TLDR use mouseclick
            private void onCheckedChanged(Object o, EventArgs e) {

            CheckBox box = null;

            if (o is CheckBox && o != null)
            {
                box = (CheckBox)o;
            }
            else {
                throw new NullReferenceException();
            }

            
            //4 is default
            if (box.Checked && form.noOfKeys == 7)
            {
                
               kpsForm nform = new kpsForm(4);
                form.Visible = false;
                form.Close();
                form.Dispose();
                setCboxText(7, nform);
                nform.ShowDialog();


                //    Application.Run(new kpsForm(4));

            }
            else if (box.Checked && form.noOfKeys == 4)
            {
               // form.Close();
                kpsForm nform = new kpsForm(7);
                form.Visible = false;
                form.Close();
                form.Dispose();
             setCboxText(4, nform);
                nform.ShowDialog();
               // Application.Run(new kpsForm(7));


            }

            
           

        
        
        
        }
        private void setCboxText(int toNoOfKeys, kpsForm newForm) {
            kpsGraphics graphics = newForm.Hgraphics;
            CheckBox nBox = graphics.getCboxWrapper1().cBox;
            switch (toNoOfKeys) {

                
                case 4:
                  

                    nBox.Text = "4 keys";

                    newForm.Controls.Add(nBox);
                    break;
                case 7:

                   
                    

                    nBox.Text = "7 keys";

                    newForm.Controls.Add(nBox);
                    break;
            }
        
        }

        public CheckBox getCbox() {

            return this.cBox;

        }





    }
}
