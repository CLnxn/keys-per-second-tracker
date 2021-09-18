using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace kpsWindow
{
    class fileHandler
    {
       public static String path;
        public fileHandler()
        {
            String currentpath = Environment.CurrentDirectory;
            Console.WriteLine(currentpath);
             path =currentpath + "\\Resources";
            Console.WriteLine(path);
            if (!File.Exists(path))
            {
                Console.WriteLine("doesnt exist");

                Directory.CreateDirectory(path);

               createRdmeFile(path);

            }

           


        }

        public void createRdmeFile(String localDir)
        {

            String iDir = localDir + "\\readme.txt";

            if (!File.Exists(iDir)) {
                using (StreamWriter sw = File.CreateText(iDir))
                {
                    sw.WriteLine("You can customise this app's background,icon and button press effects by" +
                        " importing your own pictures of the suitable format (jpg,jpg & png respectively), and renaming them! (bgimg,ico & presseffect respectively).");



                }            
            
            }



    
        }

    }

    
}
