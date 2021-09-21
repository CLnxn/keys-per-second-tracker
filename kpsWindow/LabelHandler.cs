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

            
            label.Size = new Size(140,35);
           label.BackColor = Color.Transparent;
           
          
            label.Font = new Font(FontFamily.GenericSansSerif, 12, FontStyle.Bold);
            

            label.AutoSize = false;
            if (isMaxKps)
            {
                label.TextAlign = ContentAlignment.MiddleCenter;
                label.Location = new Point((int) (form.width-label.Width),form.height-label.Size.Height);
            }
            else
            {
                label.TextAlign = ContentAlignment.BottomCenter;
                label.Location = new Point((int) (form.width-label.Width)/ 2, 10);
            }
            

        }
        public LabelHandler(kpsForm form)
        {
            this.form = form;
            this.label = new Label();


           
            label.BackColor = Color.Transparent;


           

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


        public void configurationLabel() {
            label.Font = new Font(FontFamily.GenericSansSerif, 10);
            label.Size = new Size(280, 75);
            label.Text = "Now configuring keys. Press the new keys you want to configure from left to right";
            label.TextAlign = ContentAlignment.MiddleCenter;
            label.Location = new Point((int)(form.width - label.Width) / 2, 0);
            form.Controls.Add(label);
        
        
        }
        public void configureAvgKpsLabel() {
            label.Font = new Font(FontFamily.GenericSansSerif, 12, FontStyle.Bold);
            label.Size = new Size(140, 35);
            label.Text = "Average kps: " + kpsCalculator.avgkps;
          
             label.TextAlign = ContentAlignment.MiddleCenter;
             label.Location = new Point(0,form.height-label.Size.Height);
            form.Controls.Add(label);

        }
    }
}
