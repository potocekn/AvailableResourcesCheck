using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;

namespace AvailableResourcesCheck
{
    static class Saver
    {
        public static void SaveLanguages(string path, List<string> languageShortcuts)
        {
            string fileName = path + "languages.txt";
            
            if (File.Exists(fileName))
            {
                var myFile = File.Create(fileName);
                myFile.Close();
            }
          
            using (System.IO.StreamWriter file = new System.IO.StreamWriter(fileName))
            {
                foreach (var shortcut in languageShortcuts)
                {
                    file.WriteLine(new CultureInfo(shortcut).DisplayName);
                }
            }
        }

        public static void SaveResources(string path, List<string> resources)
        {
            string fileName = path + "resources.txt";
            if (!File.Exists(fileName))
            {
                var myFile = File.Create(fileName);
                myFile.Close();                                
            }            

            using (System.IO.StreamWriter file = new System.IO.StreamWriter(fileName))
            {
                foreach (var resource in resources)
                {
                    file.WriteLine(resource);
                }
            }  
        }
    }
}
