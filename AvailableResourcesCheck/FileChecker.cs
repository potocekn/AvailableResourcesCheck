using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;

namespace AvailableResourcesCheck
{
    /// <summary>
    /// This class contains all necessary attributes and methods, that are responsible for checking local directory 
    /// and its content for json files that contain information about 4training.net resources and their languages.
    /// </summary>
    class FileChecker
    {
        string jsonDirectory;
        string changesDirectory;

        public FileChecker(string jsons, string changes)
        {
            this.jsonDirectory = jsons;
            changesDirectory = changes;
        }
        

        /// <summary>
        /// This method is used to save changed resources to separate file with given file name.
        /// </summary>
        /// <param name="changes">List of changed resource names</param>
        /// <param name="fileName">file where to save the changes</param>
        void SaveChanges(List<string> changes)
        {
            string fileName = changesDirectory + "changes.json";
            if (!File.Exists(fileName))
            {
                var myFile = File.Create(fileName);
                myFile.Close();
            }
            File.WriteAllText(fileName, JsonConvert.SerializeObject(changes));    
        }

        /// <summary>
        /// This method is responsible for saving actual state of 4training resources and their translations.
        /// Checks local directory for json files containing previous checks information. 
        /// If these files do not exist, this method creates them and fills them with actual information.
        /// For existing files content is changed only in case that something changed on 4training server (new translation, ...)
        /// </summary>
        /// <param name="languagesWithResources"></param>
        public void SaveActualState(List<LanguageWithResourcesAndLinks> languagesWithResourcesAndLinks)
        {
            List<string> changes = new List<string>();

            foreach (var languageWithResourcesAndLinks in languagesWithResourcesAndLinks)
            {
                string fileName = jsonDirectory + languageWithResourcesAndLinks.Name + ".json";
                if (File.Exists(fileName))
                {
                    LanguageWithResourcesAndLinks lwr = JsonConvert.DeserializeObject<LanguageWithResourcesAndLinks>(File.ReadAllText(fileName).Trim());
                    foreach (var item in languageWithResourcesAndLinks.Resources)
                    {
                        if (!lwr.Resources.Contains(item))
                        {
                            changes.Add(JsonConvert.SerializeObject(item));
                        }
                    }
                    File.WriteAllText(fileName, JsonConvert.SerializeObject(languageWithResourcesAndLinks));
                    Debug.WriteLine("File {0}{1} exists", languageWithResourcesAndLinks.Name, ".json");                    
                }
                else
                {
                    var myFile = File.Create(fileName);
                    myFile.Close();
                    
                    using (System.IO.StreamWriter file = new System.IO.StreamWriter(fileName))
                    {
                        changes.Add(languageWithResourcesAndLinks.Name);
                        file.Write(JsonConvert.SerializeObject(languageWithResourcesAndLinks));
                    }
                }
            }

            SaveChanges(changes);
        }
    }
}
