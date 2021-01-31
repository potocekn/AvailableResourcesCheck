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
            string configInfoString = @"C:\Users\User\Desktop\rp_folders\config\config_info.txt";
            string whereToPutJsons = @"C:\Users\User\Desktop\rp_folders\json_test\";
            string whereToPutChanges = @"C:\Users\User\Desktop\rp_folders\changes\changes.txt";

            ConfigInfoParser cip = new ConfigInfoParser(configInfoString);
            ConfigInfo ci = cip.GetConfigInfo();
            Console.WriteLine("=================================");
            
            string ESSENTIALS_URL = ci.GetApiCallUrl("Essentials");
            Console.WriteLine(ESSENTIALS_URL);
            ResourcesParser rp = new ResourcesParser(ESSENTIALS_URL);
            List<string> essentials = rp.Parse();
            string MORE_URL = ci.GetApiCallUrl("More");

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
            LanguagesParser lp = new LanguagesParser(ci.GetApiCallUrl("Languages"));
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
            List<ResourceWithLanguages> res = rd.DetectLanguages(ci.GetServer());
            Console.WriteLine("=================================");
            /**/

            FileChecker fch = new FileChecker(whereToPutJsons);
            proc.ChangeFileProblematicChars(ref res);
            fch.SaveActualState(res, whereToPutChanges);

        }
    }
}
