using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;

namespace AvailableResourcesCheck
{
    class LanguagesParser: AbstractParser
    {        
        public LanguagesParser(string url) {
            this.url = url;
        }

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
