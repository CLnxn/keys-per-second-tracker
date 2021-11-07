using System;
using Gma.System.MouseKeyHook;
using System.Windows.Forms;
using System.Threading;
using System.Collections.Generic;

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
           
           
          
        
           // form.MouseDown += onFormMouseUp;
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


        private void GlobalHookMouseClick(object sender, MouseEventArgs e)
        {
           
          
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
               
                //form.MouseUp -= onFormMouseUp;
                Console.WriteLine(" form closing hookm");
                Unsubscribe(true);
                // base.OnFormClosing(e);


            }





        


        }
    
}
