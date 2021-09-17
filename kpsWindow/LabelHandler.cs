using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace kpsWindow
{
    class LabelHandler
    {
        private Label label;
        private kpsForm form;
        public LabelHandler(kpsForm form,bool isMaxKps)
        {
            this.form = form;
            this.label = new Label();

            
            label.Size = new Size(280,75);
           label.BackColor = Color.Transparent;
          
            label.Font = new Font("Arial", 20);

            label.AutoSize = false;
            if (isMaxKps)
            {
                label.TextAlign = ContentAlignment.BottomCenter;
                label.Location = new Point((int) (form.width-label.Width)/2,form.height-label.Size.Height);
            }
            else
            {
                label.TextAlign = ContentAlignment.TopCenter;
                label.Location = new Point((int) (form.width-label.Width)/ 2, 0);
            }
            

        }
        public LabelHandler(kpsForm form)
        {
            this.form = form;
            this.label = new Label();


            label.Size = new Size(100, 100);
            label.BackColor = Color.Transparent;

            label.Font = new Font(FontFamily.GenericSansSerif, 20);

            label.AutoSize = false;



        }



        public void configureKpsLabel()
        {

         
           

            label.Text = "Made by Lnzt\n" + kpsCalculator.kps + "kps.";
            
            form.Controls.Add(label);


        }

        public void configureHighestKpsLabel() {

           

           

            label.Text ="Highest Kps: "+ kpsCalculator.maxkps;

            form.Controls.Add(label);
        
        }

    }
}
