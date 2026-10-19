using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BrandWatchElaqat.ViewModel
{
    public class CategoryMapper
    {
        public string id { get; set; }
        public string name { get; set; }
        public string displayName { get; set; }
        public string multiple { get; set; }
        public List<string> queryIds { get; set; }
        public string creatorId { get; set; }
        public string modifierId { get; set; }
        public List<SubCategoryMapper> children { get; set; }
       
    }
}