using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
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
            string MEDIAWIKI_REQUEST_LANGUAGES_FOR_RESOURCE = "mediawiki/api.php?action=query&format=json&meta=messagegroupstats&mgsgroup=page-";

            string nameChangedspecials = processor.ChangeSpecialCharsInOneResource(name);
            string url = server + MEDIAWIKI_REQUEST_LANGUAGES_FOR_RESOURCE + nameChangedspecials;

            HttpWebRequest myHttpWebRequest = (HttpWebRequest)WebRequest.Create(url);
            myHttpWebRequest.Method = "GET";
                        
            using (HttpWebResponse response = (HttpWebResponse)myHttpWebRequest.GetResponse())
            {
                string responseText;
                using (StreamReader reader = new StreamReader(response.GetResponseStream()))
                {
                    responseText = reader.ReadToEnd();
                }
                ResourceLanguagesResponse deserializedResponse = JsonConvert.DeserializeObject<ResourceLanguagesResponse>(responseText);
                foreach (var messageGroupStats in deserializedResponse.Query.Messagegroupstats)
                {
                    if (messageGroupStats.Translated > 0)
                    {
                        resource.Languages.Add(messageGroupStats.Language);
                    }
                }
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

        public List<LanguageWithResources> DetectResourcesForLanguages(List<ResourceWithLanguages> resources, List<string> languages)
        {
            List<LanguageWithResources> result = new List<LanguageWithResources>();
            foreach (var languageShortcut in languages)
            {
                LanguageWithResources lwr = new LanguageWithResources(new CultureInfo(languageShortcut).DisplayName);
                foreach (var resource in resources)
                {
                    if (resource.Languages.Contains(languageShortcut))
                    {
                        lwr.Resources.Add(resource.Name);
                    }
                }
                result.Add(lwr);
            }           

            return result;
        }
    }
}
