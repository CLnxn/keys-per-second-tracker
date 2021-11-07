using System.Windows.Forms;
using System.Drawing;

namespace kpsWindow
{
    class LabelHandler
    {
        public static bool inDarkMode = false;
        private Label label;
        private kpsForm form;



        public LabelHandler(kpsForm form,bool isMaxKps)
        {
            this.form = form;
            this.label = new Label();

            
            
           label.BackColor = Color.Transparent;
            if (inDarkMode)
            {
                label.ForeColor = Color.LawnGreen;
                form.BackColor = Color.Black;
            }
            else
            {
                label.ForeColor = Color.Black;
                form.BackColor = Color.White;
            }




            label.AutoSize = false;
            if (isMaxKps)
            {
                label.Font = new Font(FontFamily.GenericSansSerif, 12, FontStyle.Bold);
                label.Size = new Size(140, 35);
                label.TextAlign = ContentAlignment.MiddleCenter;
                label.Location = new Point((int) (form.width-label.Width),form.height-label.Size.Height);
            }
            else
            {
                label.Font = new Font(FontFamily.GenericSansSerif, 25, FontStyle.Bold);
                label.Size = new Size(280, 50);
                label.TextAlign = ContentAlignment.MiddleCenter;
                label.Location = new Point((int) (form.width-label.Width)/ 2,30);
                
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

        public void configKpsTxtLabel() {

            label.Font = new Font(FontFamily.GenericSansSerif, 30, FontStyle.Bold);
            label.Size = new Size(100, 40);
            label.TextAlign = ContentAlignment.TopCenter;
            label.Location = new Point((int)(form.width - label.Width) / 2, 0);

            label.Text = "KPS";
            if (inDarkMode)
            {
                label.ForeColor = Color.LawnGreen;
            }
            else
            {
                label.ForeColor = Color.Black;

            }
            form.Controls.Add(label);


        }
        public void configureKpsLabel()
        {


            double localKps = form.Kcalc.kps;

            label.Text = "" + localKps;
            label.ForeColor = kpsToColor(localKps);
            

            form.Controls.Add(label);


        }

        public void configureHighestKpsLabel() {

           

           

            label.Text ="Max: "+ form.Kcalc.maxkps;
            if (inDarkMode)
            {
                label.ForeColor = Color.LawnGreen;
            }
            else
            {
                label.ForeColor = Color.Black;

            }
            form.Controls.Add(label);
        
        }


        public void configurationLabel() {
            label.Font = new Font(FontFamily.GenericSansSerif, 10);
            label.Size = new Size(280, 75);
            label.Text = "Now configuring keys. Press the new keys you want to configure from left to right";
            label.TextAlign = ContentAlignment.MiddleCenter;
            label.Location = new Point((int)(form.width - label.Width) / 2, 0);

            if (inDarkMode)
            {
                label.ForeColor = Color.LawnGreen;
            }
            else
            {
                label.ForeColor = Color.Black;

            }

            form.Controls.Add(label);
        
        
        }
        public void configureAvgKpsLabel() {
            label.Font = new Font(FontFamily.GenericSansSerif, 12, FontStyle.Bold);
            label.Size = new Size(140, 35);
            label.Text = "Avg: " + form.Kcalc.avgkps;

            if (inDarkMode)
            {
                label.ForeColor = Color.LawnGreen;
            }
            else
            {
                label.ForeColor = Color.Black;

            }

            label.TextAlign = ContentAlignment.MiddleCenter;
             label.Location = new Point(0,form.height-label.Size.Height);
            form.Controls.Add(label);

        }


        public Color kpsToColor(double kps) {
            if (kps<20 && kps >= 0)
            {
                return (inDarkMode)? Color.LawnGreen: Color.Black;

            }

            else if (kps < 40 && kps >= 20)
            {
                return (inDarkMode) ? Color.LightBlue: Color.DarkBlue;
            }

            else if (kps < 60 && kps >= 40)
            {
                return (inDarkMode) ? Color.Yellow : Color.Green;
            }

            else if (kps < 80 && kps >= 60)
            {
                return Color.OrangeRed;

            }

            else if (kps >= 80)
            {
                return Color.Red;

            }
            else { return (inDarkMode) ? Color.LawnGreen : Color.Black; }

        }
    }
}
