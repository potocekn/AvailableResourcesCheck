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
        public static List<LanguageWithResourcesAndLinks> GetLinksToFiles(List<LanguageWithResources> lwr, List<string> languages, List<string> shortcuts)
        {
            List<LanguageWithResourcesAndLinks> result = new List<LanguageWithResourcesAndLinks>();
            
            foreach (var language in languages)
            {
                
                List<ResourceWithLinks> resourcesWithLinks = new List<ResourceWithLinks>();
                int index = languages.IndexOf(language);
                foreach (var item in lwr[index].Resources)
                {
                    ResourceWithLinks rwl = new ResourceWithLinks() { Name = item, PDFLink = "", ODTLink = "" };
                    resourcesWithLinks.Add(rwl);
                }

                var languageWithRes = new LanguageWithResourcesAndLinks() { Name = lwr[index].Name, Resources = resourcesWithLinks };
                if (language != "English")
                {
                    GetPdfLinks(languageWithRes, shortcuts[languages.IndexOf(language)]);
                }
                else 
                {
                    GetPdfLinks(languageWithRes, "de");
                }
                result.Add(languageWithRes);                        
            }

            return result;
        }


        static Root GetRoot(string resource, string language)
        {            
            
                string query = "https://www.4training.net/mediawiki/api.php?action=query&format=json&list=messagecollection&mcgroup=page-" + resource + "&mclanguage=" + language;
                HttpWebRequest myRequest = (HttpWebRequest)WebRequest.Create(query);

            try
            {
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
            }
            catch (Exception e)
            {
                return null;
            }
           
           
        }

        static string GetFileName(Root root, string format, string language)
        {
            foreach (var item in root.query.messagecollection)
            {
                if (language == "English")
                {
                    if (item.definition.Contains(format))
                        return item.definition;
                }
                else
                {
                    if (item.definition.Contains(format))
                        return item.translation;
                }                
            }
            return "";
        }

       

        static void GetPdfLinks(LanguageWithResourcesAndLinks languageWithResourcesAndLinks, string shortcut)
        {
            foreach (var item in languageWithResourcesAndLinks.Resources)
            {
                Root root = GetRoot(item.Name, shortcut);
                if (root == null)
                {
                    item.PDFLink = "";
                    item.ODTLink = "";
                }
                else
                {
                    item.PDFLink = GetFileName(root, ".pdf", languageWithResourcesAndLinks.Name);
                    item.ODTLink = GetFileName(root, ".odt", languageWithResourcesAndLinks.Name);
                }
                
            }
        }
    }
}
