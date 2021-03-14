using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Net;
using System.Text;

namespace AvailableResourcesCheck.ResourceLinksHelpers
{
    public static class LinkExtractionHelper
    {
        public static List<LanguageWithResourcesAndLinks> GetLinksToFiles(string jsondestination, List<string> languages, List<string> shortcuts)
        {
            List<LanguageWithResourcesAndLinks> result = new List<LanguageWithResourcesAndLinks>();
            
            foreach (var language in languages)
            {
                string fileName = jsondestination + language + ".json";
                if (File.Exists(fileName))
                {
                    LanguageWithResources lwr = JsonConvert.DeserializeObject<LanguageWithResources>(File.ReadAllText(fileName).Trim());
                    List<ResourceWithLinks> resourcesWithLinks = new List<ResourceWithLinks>();

                    foreach (var item in lwr.Resources)
                    {
                        ResourceWithLinks rwl = new ResourceWithLinks() { Name = item, PDFLink = "", ODTLink = "" };
                        resourcesWithLinks.Add(rwl);
                    }

                    var languageWithRes = new LanguageWithResourcesAndLinks() { Name = lwr.Name, Resources = resourcesWithLinks };
                    if (language != "English") GetPdfLinks(languageWithRes, shortcuts[languages.IndexOf(language)]);
                    result.Add(languageWithRes);                   
                }
            }

            return result;
        }


        static Root GetRoot(string resource, string language)
        {
            
            
                string query = "https://www.4training.net/mediawiki/api.php?action=query&format=json&list=messagecollection&mcgroup=page-" + resource + "&mclanguage=" + language;
                HttpWebRequest myRequest = (HttpWebRequest)WebRequest.Create(query);

                using (HttpWebResponse response = (HttpWebResponse)myRequest.GetResponse())
                {
                    string responseText;
                    using (StreamReader reader = new StreamReader(response.GetResponseStream()))
                    {
                        responseText = reader.ReadToEnd();
                    }
                    Root root = JsonConvert.DeserializeObject<Root>(responseText);
                    return root;
                }
           
            return new Root();
        }

        static string GetPdfName(Root root, string resourceName)
        {
            foreach (var item in root.query.messagecollection)
            {
                if (item.definition.Contains(".pdf"))
                    return item.translation;
            }
            return "";
        }

       

        static void GetPdfLinks(LanguageWithResourcesAndLinks languageWithResourcesAndLinks, string shortcut)
        {
            foreach (var item in languageWithResourcesAndLinks.Resources)
            {
                Root root = GetRoot(item.Name, shortcut);
                item.PDFLink = GetPdfName(root, item.Name);
            }
        }

        //public static List<string> GetAllAvailableLanguages(string languagesFileLocation)
        //{
        //    List<string> languages = new List<string>();
        //    string fileName = languagesFileLocation + "languages.json";
        //    if (File.Exists(fileName))
        //    {
        //        languages = JsonConvert.DeserializeObject<List<string>>(File.ReadAllText(fileName).Trim());
        //    }
        //    return languages;
        //}
    }
}
