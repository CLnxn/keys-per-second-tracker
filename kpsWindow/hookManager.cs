using System;
using Gma.System.MouseKeyHook;
using System.Windows.Forms;

namespace kpsWindow
{
    class hookManager
    {

        private kpsForm form;
        private IKeyboardMouseEvents m_GlobalHook = Hook.GlobalEvents();
        private kpsbuttonHandler kpsHbutton;
        public hookManager(kpsForm form, kpsbuttonHandler kpsHbutton) {

            this.form = form;
            this.kpsHbutton = kpsHbutton;

            Application.ApplicationExit += onApplicationExit;
            form.FormClosing += onFormClosing;

            Subscribe();
        
        }

        public void Subscribe()
        {
            // Note: for the application hook, use the Hook.AppEvents() instead
            
            
            

       //     m_GlobalHook.MouseDownExt += GlobalHookMouseDownExt; currently not useful
            m_GlobalHook.KeyDown += kpsHbutton.onKeydown;
            m_GlobalHook.KeyUp += kpsHbutton.onKeyup;
        }


        public void SubscribeConfig() {
            m_GlobalHook.KeyUp += kpsHbutton.onConfigKeyUp;
        
        
        }

        public void unSubscribeConfig() {
            m_GlobalHook.KeyUp -= kpsHbutton.onConfigKeyUp;
           
        }
        
        
        private void GlobalHookMouseDownExt(object sender, MouseEventExtArgs e)
        {
            Console.WriteLine("MouseDown: \t{0}; \t System Timestamp: \t{1}", e.Button, e.Timestamp);

            // uncommenting the following line will suppress the middle mouse button click
            // if (e.Buttons == MouseButtons.Middle) { e.Handled = true; }
        }

        public void Unsubscribe(bool dispose)
        {
            //m_GlobalHook.MouseDownExt -= GlobalHookMouseDownExt;  currently not useful
            m_GlobalHook.KeyDown -= kpsHbutton.onKeydown;
            m_GlobalHook.KeyUp -= kpsHbutton.onKeyup;
            if (dispose) {
                m_GlobalHook.Dispose();
            }
        }

        private void onApplicationExit(Object o, EventArgs e)
        {

            Console.WriteLine(" app closing");
            //  Unsubscribe();



        }

        private void onFormClosing(Object o, FormClosingEventArgs e)
        {
            
            Console.WriteLine(" form closing hookm");
            Unsubscribe(true);
            // base.OnFormClosing(e);


        }

    }
}
