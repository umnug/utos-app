using System;
using System.Collections.Generic;
using System.Text;

namespace UtahOpenSource.DataObjects.UTOSDataModels
{
    public class Talk
    {
        public int talkid { get; set; }
        public string ts { get; set; }
        public string title { get; set; }
        public string track { get; set; }
        public string description { get; set; }
        public int joindin_sessionid { get; set; }
        public int duration { get; set; }
        public string type { get; set; }
        public List<Speaker> speakers { get; set; }
        public string date { get; set; }
        public string time { get; set; }

    }
}
