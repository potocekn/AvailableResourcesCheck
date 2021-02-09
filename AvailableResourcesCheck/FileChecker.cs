using System;
using System.Collections.Generic;
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
        string directory;

        public FileChecker(string directory)
        {
            this.directory = directory;
        }

        /// <summary>
        /// This method creates custom json string, that represents content of local json file describing actual state of the resource
        /// and its translations.
        /// </summary>
        /// <param name="languageWithResource">Resource we would like to transform to json</param>
        /// <returns>json file content for given resource in a form of string</returns>
        string CreateText(LanguageWithResources languageWithResource)
        {
            StringBuilder sb = new StringBuilder();
                       
            foreach (var resource in languageWithResource.Resources)
            {
               sb.Append(resource + "\n");
            }

            return sb.ToString();
        }

        /// <summary>
        /// This method is used to save changed resources to separate file with given file name.
        /// </summary>
        /// <param name="changes">List of changed resource names</param>
        /// <param name="fileName">file where to save the changes</param>
        void SaveChanges(List<string> changes, string fileName)
        {
            bool first = true;
            
            foreach (var resource in changes)
            {
                Console.WriteLine("Save Changes: {0}", resource);
                if (first)
                {       
                    File.WriteAllText(fileName, resource + '\n');             
                    first = false;                                   
                }
                else
                {                   
                    File.AppendAllText(fileName, resource + '\n');                    
                }
            }           
        }

        /// <summary>
        /// This method is responsible for saving actual state of 4training resources and their translations.
        /// Checks local directory for json files containing previous checks information. 
        /// If these files do not exist, this method creates them and fills them with actual information.
        /// For existing files content is changed only in case that something changed on 4training server (new translation, ...)
        /// </summary>
        /// <param name="languagesWithResources"></param>
        public void SaveActualState(List<LanguageWithResources> languagesWithResources, string whereToSaveChanges)
        {
            List<string> changes = new List<string>();

            foreach (var languageWithResources in languagesWithResources)
            {
                string fileName = directory + languageWithResources.Name + ".txt";
                if (File.Exists(fileName))
                {
                    Console.WriteLine("File {0}{1} exists", languageWithResources.Name, ".txt");
                    string text = System.IO.File.ReadAllText(fileName);
                    foreach (var resource in languageWithResources.Resources)
                    {
                        if (!text.Contains(resource))
                        {
                            Console.WriteLine("Changed file {0}", fileName);
                            changes.Add(languageWithResources.Name);
                            File.WriteAllText(fileName, CreateText(languageWithResources));
                            break;
                        }
                    }
                }
                else
                {
                    var myFile = File.Create(fileName);
                    myFile.Close();
                    
                    using (System.IO.StreamWriter file = new System.IO.StreamWriter(fileName))
                    {
                        changes.Add(languageWithResources.Name);
                        file.Write(CreateText(languageWithResources));
                    }
                }
            }

            SaveChanges(changes, whereToSaveChanges);
        }
    }
}
