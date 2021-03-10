using Newtonsoft.Json;
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
            string fileName = path + "languages.json";
            
            if (File.Exists(fileName))
            {
                var myFile = File.Create(fileName);
                myFile.Close();
            }

            List<string> languages = new List<string>();

            foreach (var shortcut in languageShortcuts)
            {
                languages.Add(new CultureInfo(shortcut).DisplayName);
            }

            File.WriteAllText(fileName, JsonConvert.SerializeObject(languages));
        }

        public static void SaveResources(string path, List<string> resources)
        {
            string fileName = path + "resources.json";
            if (!File.Exists(fileName))
            {
                var myFile = File.Create(fileName);
                myFile.Close();                                
            }
            File.WriteAllText(fileName, JsonConvert.SerializeObject(resources));
        }
    }
}
