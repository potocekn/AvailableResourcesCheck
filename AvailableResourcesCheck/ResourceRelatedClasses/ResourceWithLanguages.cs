using System;
using System.Collections.Generic;
using System.Text;

namespace AvailableResourcesCheck
{
    /// <summary>
    /// This class is used like a wrapper for all information that we would like to remember for given resource such as name and available translations. 
    /// </summary>
    class ResourceWithLanguages
    {
        public string Name { get; set; }
        public List<string> Languages { get; set; }

        public ResourceWithLanguages(string name)
        {
            this.Name = name;
            this.Languages = new List<string>();
        }

    }
}
