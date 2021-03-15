using System;
using System.Collections.Generic;
using System.Text;

namespace AvailableResourcesCheck
{
    /// <summary>
    /// This class contains methods that are used to modify names of resources according to needed purpouses such as
    /// change of special characters in the resource name.
    /// </summary>
    class ResourcesProcessor
    {
        /// <summary>
        /// This method is responsible for changing name of all resources so that all of them would be in a form that is used in url on 4training.net
        /// </summary>
        /// <param name="resourceNames">list of resource names that we want to modify</param>
        public List<string> ChangeSpecialChars(List<string> resourceNames)
        {
            List<string> results = new List<string>();
            if (resourceNames == null) return null;

            for (int i = 0; i < resourceNames.Count; i++)
            {
                results.Add(ChangeSpecialCharsInOneResource(resourceNames[i]));
            }
            return results;
        }

        /// <summary>
        /// This method is responsible for changing given name into a form that is used in url on 4training.net
        /// </summary>
        /// <param name="name">name that we want to change</param>
        /// <returns>changed name with replaced special characters</returns>
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

        /// <summary>
        /// This method is used when creating files for resources. Alongside with problematic characters used in url it is needed to replace characters 
        /// that would complicate creation of files such as ':'
        /// </summary>
        /// <param name="resources">list of resources we wish to modify</param>
        public void ChangeFileProblematicChars(ref List<ResourceWithLanguages> resources)
        {
            for (int i = 0; i < resources.Count; i++)
            {
                resources[i].Name = resources[i].Name.Replace(":","_");
                resources[i].Name = resources[i].Name.Replace(" ", "_");
            }
        }
    }
}
