using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;

namespace AvailableResourcesCheck
{
    class ResourcesLanguagesDetector
    {
        List<string> resources;
        List<string> languagesShortcuts;

        public ResourcesLanguagesDetector(List<string> resources, List<string> languagesShortcuts)
        {
            this.resources = resources;
            this.languagesShortcuts = languagesShortcuts;
        }

        ResourceWithLanguages DetectLanguagesForResource(string name, List<string> languageShortcuts)
        {
            ResourceWithLanguages resource = new ResourceWithLanguages(name);
            ResourcesProcessor processor = new ResourcesProcessor();

            string nameChangedspecials = processor.ChangeSpecialCharsInOneResource(name);
            string url = "https://www.4training.net/" + nameChangedspecials+"/";

            for (int i = 0; i < languageShortcuts.Count; i++)
            {
                url += languageShortcuts[i];
                HttpWebRequest myHttpWebRequest = (HttpWebRequest)WebRequest.Create(url);
                myHttpWebRequest.Method = "GET";
                try
                {
                    HttpWebResponse myHttpWebResponse = (HttpWebResponse)myHttpWebRequest.GetResponse();
                    resource.Languages.Add(languageShortcuts[i]);
                    Console.WriteLine("Yes: " + name + " ---- " + languageShortcuts[i]);
                }
                catch (Exception e)
                {                   
                    Console.WriteLine("Nope: "+name+" ---- "+ languageShortcuts[i]);
                }
                url = "https://www.4training.net/" + nameChangedspecials + "/";
            }           

            return resource;
        }

        public List<ResourceWithLanguages> DetectLanguages()
        {
            List<ResourceWithLanguages> result = new List<ResourceWithLanguages>();

            foreach (var resource in resources)
            {
                ResourceWithLanguages resourceWithLanguages = DetectLanguagesForResource(resource,languagesShortcuts);
                result.Add(resourceWithLanguages);
            }

                return result;
        }
    }
}
