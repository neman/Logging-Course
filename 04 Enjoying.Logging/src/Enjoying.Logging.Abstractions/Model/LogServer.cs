using System.Collections.Generic;

namespace Enjoying.Logging.Abstractions
{
    public class LogServer : CommonOptions
    {
        public List<Servers> Servers { get; set; }       
    }

    public class Servers
    {
        public string Name { get; set; }
        public string Url { get; set; }
    }
}
