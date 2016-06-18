using System;
using System.Collections.Generic;
using System.Text;

namespace UtahOpenSource.DataObjects.UTOSDataModels
{
    public class Speaker
    {
        public int speakerid { get; set; }
        public string name { get; set; }
        public int? cfp_speakerid { get; set; }
        public int? joindin_speakerid { get; set; }
        public string twitter { get; set; }
        public object alt_twitter { get; set; }
        public string gravatar_hash { get; set; }
        public string updated_at { get; set; }
    }
}
