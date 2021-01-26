using System;
using System.Collections.Generic;
using DotNetWikiBot;

namespace AvailableResourcesCheck
{
    class Program
    {
        static void Main(string[] args)
        {
            ResourcesParser rp = new ResourcesParser("https://www.4training.net/mediawiki/api.php?action=query&format=json&list=messagecollection&mcgroup=page-Essentials&mclanguage=cs&mclimit=100");
            List<string> essentials = rp.Parse();
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
            Console.WriteLine(languages.Count);
        }
    }
}
