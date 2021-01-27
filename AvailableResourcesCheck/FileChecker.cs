using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace AvailableResourcesCheck
{
    class FileChecker
    {
        string directory;

        public FileChecker(string directory)
        {
            this.directory = directory;
        }


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

        public void SaveActualState(List<ResourceWithLanguages> resources)
        {
            foreach (var resource in resources)
            {
                string fileName = directory + resource.Name + ".json";
                if (File.Exists(fileName))
                {
                    Console.WriteLine("File {0}{1} exists", resource.Name, ".json");
                    string text = System.IO.File.ReadAllText(fileName);
                    foreach (var language in resource.Languages)
                    {
                        if (!text.Contains("\"Language\":\"" + language + "\""))
                        {
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
                        file.Write(CreateJsonText(resource));
                    }
                }

            }
        }
    }
}
