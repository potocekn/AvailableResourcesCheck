using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;

namespace AvailableResourcesCheck
{
    /// <summary>
    /// This class contains all necessary functions that extract all available languages used on 4training.net
    /// </summary>
    class LanguagesParser: AbstractParser
    {        
        public LanguagesParser(string url) {
            this.url = url;
        }

        /// <summary>
        /// This method is responsible for parsing json response to request for languages section on 4training.net
        /// Shortcuts of all available languages are extracted from the response and saved in a list.
        /// </summary>
        /// <returns>list of shortcuts of all available languages on 4training.net</returns>
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

        /// <summary>
        /// This method searches for language shortcuts in given text and modifies given list of results.
        /// </summary>
        /// <param name="result">list of shortcuts we want to update</param>
        /// <param name="responseText">json reaponse text in which we are searching</param>
        internal override void Extract(ref List<string> result, string responseText)
        {
            int i = 0;
            bool wasHashtag = false;
            string LANGUAGENAME = "languagename:";
            StringBuilder languageShort = new StringBuilder();

            while (i < responseText.Length)
            {
                if (responseText[i] == '#')
                {
                    wasHashtag = true;
                    i++;
                    continue;
                }

                if (wasHashtag)
                {
                    if (responseText.Substring(i, 13) == LANGUAGENAME)
                    {
                        i += LANGUAGENAME.Length;
                        while (responseText[i] != '}')
                        {
                            languageShort.Append(responseText[i]);
                            i++;
                        }
                        result.Add(languageShort.ToString());
                        
                        languageShort = new StringBuilder();
                    }

                    
                    wasHashtag = false;
                    i++;
                }
                i++;
            }
        }
    }
}
