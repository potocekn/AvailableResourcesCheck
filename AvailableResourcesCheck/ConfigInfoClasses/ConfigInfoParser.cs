using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace AvailableResourcesCheck
{
    /// <summary>
    /// This class contains all necessary attributes and methods to get configuration information necessary for web requests
    /// </summary>
    class ConfigInfoParser
    {
        string path;

        public ConfigInfoParser(string path)
        {
            this.path = path;
        }

        public ConfigInfo GetConfigInfo()
        {   
            ConfigInfo result = new ConfigInfo();
            using (StreamReader file = new StreamReader(path))
            {
                string line;
                while ((line = file.ReadLine()) != null)
                {
                    if (!line.Contains("\": \""))
                    {
                        continue;
                    }

                    string beginningBefore = line.Split(": ")[0].Trim();
                    string beginning = beginningBefore.Substring(1, beginningBefore.Length - 2);
                    string paramBefore = line.Split(": ")[1].Trim();
                    string param = paramBefore.Substring(1, paramBefore.Length - 3);

                    switch (beginning)
                    {
                        case "ServerDomain":
                            result.Server = param;
                            break;
                        case "ApiJsonPage":
                            result.ApiJsonPage = param;
                            break;
                        case "ApiTranslationLimit":
                            result.ApiTranslationLimit = param;
                            break;
                        case "DetectedLanguagesFileLocation":
                            result.DetectedLanguagesFileLocation = param;
                            break;
                        case "DetectedResourcesFileLocation":
                            result.DetectedResourcesFileLocation = param;
                            break;
                        case "JsonFilesDestinationFolder":
                            result.JsonFilesDestinationFolder = param;
                            break;
                        case "DetectedChangesFileLocation":
                            result.DetectedChangesFileLocation = param;
                            break;
                        default:
                            throw new InvalidParameterInConfigFile(beginning);
                    }
                }
            }            
            return result;
        }
    }
}
