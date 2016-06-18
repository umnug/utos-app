using System;
using System.Collections.Generic;
using System.Text;

namespace UtahOpenSource.DataObjects.UTOSDataModels
{
    class Sponsor
    {
        public int sponID { get; set; }
        public string companyName { get; set; }
        public string sponsorLevel { get; set; }
        public string companyDescription { get; set; }
        public string logoPath { get; set; }
        public string siteURL { get; set; }
        public string boothLocation { get; set; }

    }
}
