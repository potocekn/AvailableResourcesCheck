using System;
using System.Collections.Generic;
using System.Text;

namespace AvailableResourcesCheck
{
    class ResourcesProcessor
    {
        public void ChangeSpecialChars(ref List<string> resourceNames)
        {
            if (resourceNames == null) return;

            for (int i = 0; i < resourceNames.Count; i++)
            {
                resourceNames[i] = ChangeSpecialCharsInOneResource(resourceNames[i]);
            }
        }

        public string ChangeSpecialCharsInOneResource(string name)
        {
            string result;
            if (name.Contains('\''))
            {
                name = name.Replace("'", "%27");
            }

            if (name.Contains(' '))
            {
                name = name.Replace(" ", "_");
            }

            result = name;
            return result;
        }

        public void ChangeFileProblematicChars(ref List<ResourceWithLanguages> resources)
        {
            for (int i = 0; i < resources.Count; i++)
            {
                resources[i].Name = resources[i].Name.Replace(":","_");
            }
        }

    }
}
