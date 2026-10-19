using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BrandWatchElaqat.ViewModel
{
    public class MentionVM
    {
        public string MentionTitle { get; set; }
        public string MentionDate { get; set; }
        public int MentionType { get; set; }
        public int TotalEngagement { get; set; }
        public string ContentSource { get; set; }
    }
}