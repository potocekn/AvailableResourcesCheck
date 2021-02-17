using System;
using System.Collections.Generic;
using System.Text;

namespace AvailableResourcesCheck
{
    class LanguageWithResources
    {
        public string Name { get; set; }
        public List<string> Resources { get; set; }

        public LanguageWithResources(string name)
        {
            this.Name = name;
            this.Resources = new List<string>();
        }

    }
}
