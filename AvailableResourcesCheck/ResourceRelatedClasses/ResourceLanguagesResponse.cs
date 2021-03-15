using System;
using System.Collections.Generic;
using System.Text;

namespace AvailableResourcesCheck
{
    public class ResourceLanguagesResponse
    {
        public string Batchcomplete { get; set; }
        public Warnings Warnings { get; set; }
        public Query Query { get; set; }
    }

    public class MessageGroupStats
    {        
        public int Total { get; set; }
        public int Translated { get; set; }
        public int Fuzzy { get; set; }
        public int Proofread { get; set; }
        public string Code { get; set; }
        public string Language { get; set; }
    }

    public class Warnings
    {
        public MessageGroupStats Messagegroupstats { get; set; }
    }

    public class Query
    {
        public List<MessageGroupStats> Messagegroupstats { get; set; }
    }
}
