using System;
using System.Collections.Generic;
using System.Text;

namespace AvailableResourcesCheck
{
    public class ResourcesResponse
    {
        public string Batchcomplete { get; set; }
        public QueryResources Query { get; set; }
    }

    public class Metadata
    {
        public int Resultsize { get; set; }
        public int Remaining { get; set; }
    }

    public class Messagecollection
    {
        public string Key { get; set; }
        public string Definition { get; set; }
        public object Translation { get; set; }
        public string Title { get; set; }
        public string PrimaryGroup { get; set; }
    }

    public class QueryResources
    {
        public Metadata Metadata { get; set; }
        public List<Messagecollection> Messagecollection { get; set; }
    }
   
}
