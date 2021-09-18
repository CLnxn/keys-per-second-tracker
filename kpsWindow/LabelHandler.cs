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


            label.Size = new Size(280, 75);
            label.BackColor = Color.Transparent;


            label.Font = new Font(FontFamily.GenericSansSerif, 10);

            label.AutoSize = false;



        }


        public Label getLabel() {

            return this.label;

        }
        public void configureKpsLabel()
        {

         
           

            label.Text = "Your Kps: " + kpsCalculator.kps ;
            
            form.Controls.Add(label);


        }

        public void configureHighestKpsLabel() {

           

           

            label.Text ="Highest Kps: "+ kpsCalculator.maxkps;

            form.Controls.Add(label);
        
        }


        public void configureErrorLabel() {
            label.Text = "Did you format/rename your image according to readme.txt in the resource folder in your current root Directory?";
            label.TextAlign = ContentAlignment.TopCenter;
            label.Location = new Point((int)(form.width - label.Width) / 2, 0);
            form.Controls.Add(label);
        
        
        }

    }
}
