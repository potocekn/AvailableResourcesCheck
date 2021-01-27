using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using DotNetWikiBot;

namespace AvailableResourcesCheck
{
    /// <summary>
    /// This class contains all necessary methods and attributes for getting available training resources on 4training.net
    /// </summary>
    class ResourcesParser: AbstractParser
    {       
        public ResourcesParser(string url)
        {
            this.url = url;
        }

        /// <summary>
        /// This method takes web request response string and extracts resource names that are available at the moment on the 4training.net 
        /// Extracted resources are added into given list of strings named result.
        /// </summary>
        /// <param name="result">list of strings (passed by reference) that holds the resources that are parsed from the response text</param>
        /// <param name="responseText">response of web request in string form</param>
        internal override void Extract(ref List<string> result, string responseText)
        {
            bool wasSquareBracket = false;
            bool startReading = false;
            StringBuilder resource = new StringBuilder();
            int i = 0;

            while (i < responseText.Length)
            {

                if (responseText[i] == '[')
                {
                    if (wasSquareBracket)
                    {
                        startReading = true;
                        i++;
                        continue;
                    }
                    else
                    {
                        wasSquareBracket = true;
                        i++;
                        continue;
                    }
                }

                if (startReading)
                {
                    char j = responseText[i];
                    while (j != '|')
                    {
                        if (j == ']')
                        {
                            resource = new StringBuilder();
                            startReading = false;
                            wasSquareBracket = false;
                            break;
                        }
                        else
                        {
                            resource.Append(j);
                            i++;
                            j = responseText[i];
                        }
                        
                    }

                    if (resource.ToString() != (new StringBuilder()).ToString()) result.Add(resource.ToString());
                    resource = new StringBuilder();
                    startReading = false;
                    wasSquareBracket = false;
                }
                i++;
            }
        }

        /// <summary>
        /// This method is responsible for requesting json response from 4training.net, that would contain all essential available resources.
        /// After the request, the response is parsed and names of all available resources are extracted into list of strings.
        /// </summary>
        /// <returns>list of all available resources on 4training.net</returns>
        public override List<string> Parse()
        {
            HttpWebRequest myRequest = (HttpWebRequest)WebRequest.Create(url);
            List<string> result = new List<string>();

            using (HttpWebResponse response = (HttpWebResponse)myRequest.GetResponse())
            {
                string responseText;
                using (StreamReader reader = new StreamReader(response.GetResponseStream()))
                {
                    responseText = reader.ReadToEnd();
                }

                Extract(ref result, responseText);
            }

            return result;
        }
                
    }
}
