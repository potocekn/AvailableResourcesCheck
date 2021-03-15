using System;
using System.Collections.Generic;
using System.Text;

namespace AvailableResourcesCheck
{
    class InvalidParameterInConfigFile: Exception
    {
        public InvalidParameterInConfigFile() { }

        public InvalidParameterInConfigFile(string name) : base(String.Format("Invalid parameter in config file: {0}", name)) { }
    }
}
