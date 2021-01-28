using System;
using System.Collections.Generic;
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
            string[] lines = System.IO.File.ReadAllLines(path);
            string server = lines[0].Split("==")[1].Trim();
            string apiJsonPage = lines[1].Split("==")[1].Trim();
            string apiTranslationLimit = lines[2].Split("==")[1].Trim();
            Console.WriteLine("Server:"+server);
            Console.WriteLine("JsonPage:"+apiJsonPage);
            Console.WriteLine("Translation:" + apiTranslationLimit);
            ConfigInfo result = new ConfigInfo(server,apiJsonPage,apiTranslationLimit);
            return result;
        }
    }
}
