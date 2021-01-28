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
        string Server { get; }
        string ApiJsonPage { get; }
        string ApiTranslationLimit{ get; }

        public ConfigInfo(string server, string apiJsonPage, string apiTranslationLimit)
        {
            this.Server = server;
            this.ApiJsonPage = apiJsonPage;
            this.ApiTranslationLimit = apiTranslationLimit;
        }

        /// <summary>
        /// This method returns url for api call that requests json response from given page on 4training server
        /// </summary>
        /// <param name="pageName">name of the page we want to create query json format api call</param>
        /// <returns>url for api call</returns>
        public string GetApiCallUrl(string pageName)
        {
            return Server + ApiJsonPage + pageName + ApiTranslationLimit;
        }

        public string GetServer() 
        {
            return Server;
        }
    }
}
