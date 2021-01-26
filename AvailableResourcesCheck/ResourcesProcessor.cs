using System;
using System.Collections.Generic;
using System.Text;

namespace AvailableResourcesCheck
{
    class ResourcesProcessor
    {
        public void ChangeSpecialChars(ref List<string> resourceNames)
        {
            for (int i = 0; i < resourceNames.Count; i++)
            {
                if (resourceNames[i].Contains('\''))
                {
                    resourceNames[i] = resourceNames[i].Replace("'","%27");
                }

                if (resourceNames[i].Contains(' '))
                {
                    resourceNames[i] = resourceNames[i].Replace(" ", "_");
                }
            }
        }

    }
}
