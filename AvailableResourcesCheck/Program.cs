using System;
using System.Collections.Generic;
using System.Net;
using DotNetWikiBot;

namespace AvailableResourcesCheck
{
    class Program
    {
        
        static void Main(string[] args)
        {
            string ESSENTIALS_URL = "https://www.4training.net/mediawiki/api.php?action=query&format=json&list=messagecollection&mcgroup=page-Essentials&mclanguage=cs&mclimit=100";
            ResourcesParser rp = new ResourcesParser(ESSENTIALS_URL);
            List<string> essentials = rp.Parse();
            string MORE_URL = "https://www.4training.net/mediawiki/api.php?action=query&format=json&list=messagecollection&mcgroup=page-More&mclanguage=cs&mclimit=100";

            ResourcesParser rp_more = new ResourcesParser(MORE_URL);
            List<string> more = rp_more.Parse();

            essentials.AddRange(more);
            ResourcesProcessor proc = new ResourcesProcessor();

            proc.ChangeSpecialChars(ref essentials);
            /**/
            for (int i = 0; i < essentials.Count; i++)
            {
                Console.WriteLine(essentials[i]);
            }
            /**/
            Console.WriteLine("=================================");
            LanguagesParser lp = new LanguagesParser("https://www.4training.net/mediawiki/api.php?action=query&format=json&list=messagecollection&mcgroup=page-Languages&mclanguage=cs&mclimit=100");
            List<string> languages = lp.Parse();
            /**/
            for (int i = 0; i < languages.Count; i++)
            {
                Console.WriteLine(languages[i]);
            }
            /**/
            Console.WriteLine("=================================");

            ResourcesLanguagesDetector rd = new ResourcesLanguagesDetector(essentials,languages);
            List<ResourceWithLanguages> res = rd.DetectLanguages();
            /*/
            // Creates an HttpWebRequest for the specified URL.
            HttpWebRequest myHttpWebRequest = (HttpWebRequest)WebRequest.Create("https://www.4training.net/God%27s_Story/w");
            myHttpWebRequest.Method = "GET";
            // Sends the HttpWebRequest and waits for a response.
            try
            {
                HttpWebResponse myHttpWebResponse = (HttpWebResponse)myHttpWebRequest.GetResponse();
            }
            catch (Exception e)
            {
                Console.WriteLine("Nope");
            }
            /**/        
            
        }
    }
}
