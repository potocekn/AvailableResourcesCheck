using System;
using System.Collections.Generic;
using System.Text;

namespace AvailableResourcesCheck
{
    class InvalidParameterInConfigTxtFile: Exception
    {
        public InvalidParameterInConfigTxtFile() { }

        public InvalidParameterInConfigTxtFile(string name) : base(String.Format("Invalid parameter in config file: {0}", name)) { }
    }
}
