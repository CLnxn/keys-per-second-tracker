using System;
using System.IO;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Text.Json;
using System.Linq;

namespace kpsWindow
{
    class fileHandler
    {
        public static String path;

        public static String jpath;


        public fileHandler()
        {


            String currentpath = Environment.CurrentDirectory;
            Console.WriteLine(currentpath);
            path = currentpath + "\\Resources";
            Console.WriteLine(path);
            if (!File.Exists(path))
            {
                //Console.WriteLine("doesnt exist");

                Directory.CreateDirectory(path);

                createRdmeFile(path);
                createKeyDataDir(path);

            }




        }

        public void createRdmeFile(String localDir)
        {

            String iDir = localDir + "\\readme.txt";

            if (!File.Exists(iDir))
            {
                using (StreamWriter sw = File.CreateText(iDir))
                {
                    sw.WriteLine("You can customise this app's background,icon and button press effects by" +
                        " importing your own pictures of the suitable format (jpg,jpg & png respectively), and renaming them! (bgimg,ico & presseffect respectively).");



                }

            }




        }


        private void createKeyDataDir(String iPath)
        {

            String path = iPath + "\\keyData";
            jpath = path + "\\kd.json";

            if (!File.Exists(path))
            {
               // Console.WriteLine("doesnt exist");

                Directory.CreateDirectory(path);


            }

            if (!File.Exists(jpath))
            {
                File.Create(jpath);



            }



        }


        public static void updateKeyData(int vnoOfKeys, List<Keys> vkeys)
        {


            var fkeyset = readKeyData();

      

            switch (vnoOfKeys)
            {
                case 4:
                        fkeyset.fourk = vkeys;
                    break;
                case 7:
                        fkeyset.sevenk = vkeys;
                    break;



            }


            string jsonStr = JsonSerializer.Serialize(fkeyset);
            Console.WriteLine(jsonStr);
            File.WriteAllText(jpath, jsonStr);
            //Creates a new file, writes the specified string to the file, and then closes the file. If the target file already exists, it is overwritten.




        }
     
        public static fKeySet readKeyData()
        {
          
            


            fKeySet kSet = null;
         
                try
                {
                    string jsonStr = File.ReadAllText(jpath);
                    kSet = JsonSerializer.Deserialize<fKeySet>(jsonStr);
               // Console.WriteLine(kSet.fourk.GroupBy(x => x).Any(xHeader => xHeader.Count() > 1));
                
                if (kSet.fourk.GroupBy(x =>x).Any(xHeader => xHeader.Count() > 1) ||
                    kSet.sevenk.GroupBy(x => x).Any(xHeader => xHeader.Count() > 1)) {
                    Console.WriteLine("duplicates detected"); // shd only occur if the kd.json is mistakenly edited manually
                    throw new Exception();
                   
                }
                }
                catch (Exception)
                {


                    kSet = new fKeySet
                    {
                        sevenk = new List<Keys> { Keys.S, Keys.D, Keys.F, Keys.Space, Keys.J, Keys.K, Keys.L },

                        fourk = new List<Keys> { Keys.D, Keys.F, Keys.J, Keys.K }

                    };
                }
            
            

            return kSet;

        }


      
        public static List<Keys> GetKeys(int noOfKeys) {

            var fkeyset = readKeyData();

            switch (noOfKeys)
            {
                case 4:
                    return fkeyset.fourk;
                case 7:
                    return fkeyset.sevenk;



            }

            return null;
            //as of now only 4 and 7k are defined
        }







    }
        public class fKeySet
        {

            public List<Keys> sevenk { get; set; }

            public List<Keys> fourk { get; set; }



        }




    
}