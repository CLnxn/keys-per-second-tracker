using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
namespace kpsWindow
{
    class cBoxWrapper
    {

        CheckBox cBox;
        kpsForm form;
        public cBoxWrapper(kpsForm form) 
        {
            this.form = form;

            cBox = new CheckBox();

            cBox.Appearance = Appearance.Button;
            cBox.Size = new System.Drawing.Size(45,20);
            cBox.AutoCheck = true;
            //cBox.Location = new System.Drawing.Point(0,form.height-cBox.Size.Height);
            cBox.AutoSize = false;
            cBox.ThreeState = false;
            cBox.Text = "7 keys";

            // cBox.CheckStateChanged += onCheckedChanged;
         

            cBox.CheckedChanged += onCheckedChanged;
           // cBox.KeyPress += onKeyPress;
         //   cBox.KeyDown += onKeyDown;
          
            form.Controls.Add(cBox);

        }
      
       
      
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
            CheckBox nBox = graphics.getCboxWrapper().cBox;
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

        private void playCheckClickEffect() {
            
           

        
        
        }





    }
}
