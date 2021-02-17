using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Net;
using DotNetWikiBot;
using Newtonsoft.Json;

namespace AvailableResourcesCheck
{
    class Program
    {
        
        static void Main(string[] args)
        {           
            string configInfoString = args[0];//@"C:\Users\User\Desktop\rp_folders\config\config_info.txt";
            string whereToPutJsons = args[1]; // @"C:\Users\User\Desktop\rp_folders\json_test\";
            string whereToPutChanges = args[2]; // @"C:\Users\User\Desktop\rp_folders\changes\changes.txt";

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
            proc.ChangeSpecialChars(ref essentials);
            
            for (int i = 0; i < essentials.Count; i++)
            {
                Debug.WriteLine(essentials[i]);
            }          
            Debug.WriteLine("=================================");

            Debug.WriteLine("Languages:");
            LanguagesParser lp = new LanguagesParser(ci.GetApiCallUrl("Languages"));
            List<string> languages = lp.Parse();
            Saver.SaveLanguages(ci.DetectedLanguagesFileLocation,languages);
            
            for (int i = 0; i < languages.Count; i++)
            {
                Debug.WriteLine(languages[i]);
            }     
           
            ResourcesLanguagesDetector rd = new ResourcesLanguagesDetector(essentials,languages);
            List<ResourceWithLanguages> res = rd.DetectLanguages(ci.Server);
            List<LanguageWithResources> lwr = rd.DetectResourcesForLanguages(res,languages);
                        
            FileChecker fch = new FileChecker(whereToPutJsons);
            proc.ChangeFileProblematicChars(ref res);
            fch.SaveActualState(lwr, whereToPutChanges);

        }
    }
}
