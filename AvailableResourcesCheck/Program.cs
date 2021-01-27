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

            Console.WriteLine("Resources:");
            proc.ChangeSpecialChars(ref essentials);
            /**/
            for (int i = 0; i < essentials.Count; i++)
            {
                Console.WriteLine(essentials[i]);
            }
            /**/
            Console.WriteLine("=================================");
            Console.WriteLine("Languages:");
            LanguagesParser lp = new LanguagesParser("https://www.4training.net/mediawiki/api.php?action=query&format=json&list=messagecollection&mcgroup=page-Languages&mclanguage=cs&mclimit=100");
            List<string> languages = lp.Parse();
            /**/
            for (int i = 0; i < languages.Count; i++)
            {
                Console.WriteLine(languages[i]);
            }
            /**/
            Console.WriteLine("=================================");
            /**/
            Console.WriteLine("Pages:");
            ResourcesLanguagesDetector rd = new ResourcesLanguagesDetector(essentials,languages);
            List<ResourceWithLanguages> res = rd.DetectLanguages();
            Console.WriteLine("=================================");
            /**/

            FileChecker fch = new FileChecker(@"C:\Users\User\Desktop\json_test\");
            proc.ChangeFileProblematicChars(ref res);
            fch.SaveActualState(res);

        }
    }
}
