using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;

namespace AvailableResourcesCheck
{
    /// <summary>
    /// This class contains all necessary attributes and methods for detecting all translations that are available for each resuorce
    /// on 4training.net
    /// </summary>
    class ResourcesLanguagesDetector
    {
        List<string> resources;
        List<string> languagesShortcuts;

        public ResourcesLanguagesDetector(List<string> resources, List<string> languagesShortcuts)
        {
            this.resources = resources;
            this.languagesShortcuts = languagesShortcuts;
        }

        /// <summary>
        /// This method tests all possible combinations of "resource name + language" shortcut to determine if translation in given language exists.
        /// </summary>
        /// <param name="name">name of resource that is used in url</param>
        /// <param name="languageShortcuts">all available languages on 4training</param>
        /// <returns>Custom structure that contains name and languages of given resource</returns>
        ResourceWithLanguages DetectLanguagesForResource(string name, List<string> languageShortcuts, string server)
        {
            ResourceWithLanguages resource = new ResourceWithLanguages(name);
            ResourcesProcessor processor = new ResourcesProcessor();

            string nameChangedspecials = processor.ChangeSpecialCharsInOneResource(name);
            string url = server + nameChangedspecials+"/";

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
                url = server + nameChangedspecials + "/";
            }           

            return resource;
        }

        /// <summary>
        /// This method detects languages for all available resources on 4training.net
        /// </summary>
        /// <param name="server">server url</param>
        /// <returns>list of custom structures that contain name and languages of each resource</returns>        
        public List<ResourceWithLanguages> DetectLanguages(string server)
        {
            List<ResourceWithLanguages> result = new List<ResourceWithLanguages>();

            foreach (var resource in resources)
            {
                ResourceWithLanguages resourceWithLanguages = DetectLanguagesForResource(resource,languagesShortcuts,server);
                result.Add(resourceWithLanguages);
            }

            return result;
        }
    }
}
