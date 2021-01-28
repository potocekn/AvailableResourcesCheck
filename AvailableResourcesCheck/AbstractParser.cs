using System;
using System.Collections.Generic;
using System.Text;

namespace AvailableResourcesCheck
{
    /// <summary>
    /// This is an abstract class that contains all common features for different types of parsers used in this project, such as
    /// ResourcesParser and LanguagesParser.
    /// </summary>
    abstract class AbstractParser
    {
        internal string url;

        public abstract List<string> Parse();
        abstract internal void Extract(ref List<string> result, string responseText);
    }
}
