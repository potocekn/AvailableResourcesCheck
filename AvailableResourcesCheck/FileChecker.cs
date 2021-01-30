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
        /// <param name="resource">Resource we would like to transform to json</param>
        /// <returns>json file content for given resource in a form of string</returns>
        string CreateJsonText(ResourceWithLanguages resource)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("{ \"Resource\": { \"Name\": { \"FileName\":\"");
            sb.Append(resource.Name);
            sb.Append("\",} \"Language\": {");
           
            foreach (var language in resource.Languages)
            {
               sb.Append("\"LanguageName\":\"" + language + "\",");
            }

            sb.Append("}}}");

            return sb.ToString();
        }

        void SaveChanges(List<string> changes)
        {
            bool first = true;
            string fileName = @"C:\Users\User\Desktop\rp_folders\changes\changes.txt";

            foreach (var resource in changes)
            {
                Console.WriteLine("Save Changes: {0}", resource);
                if (first)
                {                    
                    Console.WriteLine("Inside first ... resource: {0}", resource);
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
        /// <param name="resources"></param>
        public void SaveActualState(List<ResourceWithLanguages> resources)
        {
            List<string> changes = new List<string>();

            foreach (var resource in resources)
            {
                string fileName = directory + resource.Name + ".json";
                if (File.Exists(fileName))
                {
                    Console.WriteLine("File {0}{1} exists", resource.Name, ".json");
                    string text = System.IO.File.ReadAllText(fileName);
                    foreach (var language in resource.Languages)
                    {
                        if (!text.Contains("\"" + language + "\""))
                        {
                            Console.WriteLine("Changed file {0}", fileName);
                            changes.Add(resource.Name);
                            File.WriteAllText(fileName, CreateJsonText(resource));
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
                        changes.Add(resource.Name);
                        file.Write(CreateJsonText(resource));
                    }
                }
            }

            SaveChanges(changes);
        }
    }
}
