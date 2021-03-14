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
        
        static void Main(string[] args)
        {           
            string configInfoString = args[0];
            string whereToPutJsons = args[1]; 
            string whereToPutChanges = args[2]; 

            ConfigInfoParser cip = new ConfigInfoParser(configInfoString);
            ConfigInfo ci = cip.GetConfigInfo();
            Debug.WriteLine("=================================");
            
            string ESSENTIALS_URL = ci.GetApiCallUrl("Essentials");
            
            ResourcesParser rp = new ResourcesParser(ESSENTIALS_URL);
            List<string> essentials = rp.Parse();

            string MORE_URL = ci.GetApiCallUrl("More");            
            ResourcesParser rp_more = new ResourcesParser(MORE_URL);
            List<string> more = rp_more.Parse();
            essentials.AddRange(more);

            Saver.SaveResources(ci.DetectedResourcesFileLocation, essentials);

            ResourcesProcessor proc = new ResourcesProcessor();
            Debug.WriteLine("Resources:");
            
            for (int i = 0; i < essentials.Count; i++)
            {
                Debug.WriteLine(essentials[i]);
            }          
            Debug.WriteLine("=================================");

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

            var withLinks = LinkExtractionHelper.GetLinksToFiles(whereToPutJsons, languagesFullNames, languages);

            FileChecker fch = new FileChecker(whereToPutJsons);
            fch.SaveActualState(lwr, whereToPutChanges);

        }
    }
}
