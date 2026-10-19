using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BrandWatchElaqat.ViewModel
{
    public class SiteMapper
    {
        public string id { get; set; }
        public string name { get; set; }
        public string shared { get; set; }
        public List<string> domains { get; set; }
    }
}