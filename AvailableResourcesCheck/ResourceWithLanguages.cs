using System;
using System.Collections.Generic;
using System.Text;

namespace AvailableResourcesCheck
{
    class ResourceWithLanguages
    {
        string Name { get; set; }
        List<string> Languages { get; set; }

        public ResourceWithLanguages(string name)
        {
            this.Name = name;
            this.Languages = new List<string>();
        }

        public ResourceWithLanguages(string name, List<string> languages)
        {
            this.Name = name;
            this.Languages = languages;
        }
    }
}
