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

        public ResourcesLanguagesDetector(List<string> resources)
        {
            this.resources = resources;
        }

        ResourceWithLanguages DetectLanguagesForResource(string name)
        {
            ResourceWithLanguages resource = new ResourceWithLanguages(name);
            ResourcesProcessor processor = new ResourcesProcessor();

            string nameChangedspecials = processor.ChangeSpecialCharsInOneResource(name);
            string url = "https://www.4training.net/mediawiki/api.php?action=query&format=json&list=messagecollection&mcgroup=page-"+nameChangedspecials+"&mclanguage=cs&mclimit=100";

            HttpWebRequest myRequest = (HttpWebRequest)WebRequest.Create(url);
            
            using (HttpWebResponse response = (HttpWebResponse)myRequest.GetResponse())
            {
                string responseText;
                using (StreamReader reader = new StreamReader(response.GetResponseStream()))
                {
                    responseText = reader.ReadToEnd();
                }
                                
            }

            return resource;
        }

        public List<ResourceWithLanguages> DetectLanguages()
        {
            List<ResourceWithLanguages> result = new List<ResourceWithLanguages>();
            
                        
            return result;
        }
    }
}
