using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Net;
using Newtonsoft.Json;
using AvailableResourcesCheck.ResourceLinksHelpers;

namespace AvailableResourcesCheck
{
    class Program
    {
        static void WriteList(string name, List<string> list)
        {
            Debug.WriteLine(name);

            for (int i = 0; i < list.Count; i++)
            {
                Debug.WriteLine(list[i]);
            }
            Debug.WriteLine("=================================");
        }

        static void Main(string[] args)
        {
            if (args.Length != 1)
            {
                Console.WriteLine("Wrong number of parametres - ({0}) ... one parameter expected", args.Length);
                return;
            }
            string configInfoString = args[0];
            
            ConfigInfoParser cip = new ConfigInfoParser(configInfoString);
            ConfigInfo ci = cip.GetConfigInfo();
            Debug.WriteLine("=================================");
            
            string ESSENTIALS_URL = ci.GetApiCallUrl("Essentials");
            
            ResourcesParser rp = new ResourcesParser(ESSENTIALS_URL);
            List<string> essentials = rp.Parse();
            essentials.Remove("God's Story");
            essentials.Add("God's Story (five fingers)");
            essentials.Add("God's Story (first and last sacrifice)");
            string MORE_URL = ci.GetApiCallUrl("More");            
            ResourcesParser rp_more = new ResourcesParser(MORE_URL);
            List<string> more = rp_more.Parse();
            essentials.AddRange(more);

            Saver.SaveResources(ci.DetectedResourcesFileLocation, essentials);
                        
            WriteList("Resources", essentials);

            Debug.WriteLine("Languages:");
            LanguagesParser lp = new LanguagesParser(ci.GetApiCallUrl("Languages"));
            List<string> languages = lp.Parse();
            Saver.SaveLanguages(ci.DetectedLanguagesFileLocation,languages);

            List<string> languagesFullNames = new List<string>();
            for (int i = 0; i < languages.Count; i++)
            {
                Debug.WriteLine(languages[i]);
                languagesFullNames.Add((new CultureInfo(languages[i])).DisplayName);
            }     
           
            ResourcesLanguagesDetector rd = new ResourcesLanguagesDetector(essentials,languages);
            List<ResourceWithLanguages> res = rd.DetectLanguages(ci.Server);
            List<LanguageWithResources> lwr = rd.DetectResourcesForLanguages(res,languages);

            var withLinks = LinkExtractionHelper.GetLinksToFiles(lwr, languagesFullNames, languages);

            FileChecker fch = new FileChecker(ci.JsonFilesDestinationFolder, ci.DetectedChangesFileLocation);
            fch.SaveActualState(withLinks);

        }
    }
}
