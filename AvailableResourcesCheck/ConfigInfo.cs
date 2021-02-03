using System;
using System.Collections.Generic;
using System.Text;

namespace AvailableResourcesCheck
{
    /// <summary>
    /// This class contains configuration information necessary for http web requests to 4training.net server
    /// </summary>
    class ConfigInfo
    {
        public string Server { get; set; }
        public string ApiJsonPage { get; set; }
        public string ApiTranslationLimit { get; set; }
        public string DetectedLanguagesFileLocation { get; set; }
        public string DetectedResourcesFileLocation { get; set; }

        public ConfigInfo() { }
     
        /// <summary>
        /// This method returns url for api call that requests json response from given page on 4training server
        /// </summary>
        /// <param name="pageName">name of the page we want to create query json format api call</param>
        /// <returns>url for api call</returns>
        public string GetApiCallUrl(string pageName)
        {
            return Server + ApiJsonPage + pageName + ApiTranslationLimit;
        }    
    }
}
