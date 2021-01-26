using System;
using System.Collections.Generic;
using System.Text;

namespace AvailableResourcesCheck
{
    abstract class AbstractParser
    {
        internal string url;

        public abstract List<string> Parse();
        abstract internal void Extract(ref List<string> result, string responseText);
    }
}
