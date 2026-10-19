using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BrandWatchElaqat.ViewModel
{
    public class CategoryDetailsMapper
    {
        public string id { get; set; }
        public string name { get; set; }
        public string displayName { get; set; }
        public string parentId { get; set; }
        public string parentName { get; set; }

    }
}