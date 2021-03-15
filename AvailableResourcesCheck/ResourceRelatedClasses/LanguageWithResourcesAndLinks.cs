using System;
using System.Collections.Generic;
using System.Text;

namespace AvailableResourcesCheck
{
    public class LanguageWithResourcesAndLinks
    {
        public string Name { get; set; }
        public List<ResourceWithLinks> Resources { get; set; }
        //public List<string> Resources { get; set; }
        //public List<string> PdfLinks { get; set; }
        //public List<string> OdtLinks { get; set; }

    }
}
