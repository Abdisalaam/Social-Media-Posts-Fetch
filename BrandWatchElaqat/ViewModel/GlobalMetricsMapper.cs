using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BrandWatchElaqat.ViewModel
{
    public class GlobalMetricsMapper
    {
        public List<string> sentiments { get; set; }
        public List<string> accountTypes { get; set; }
        public List<string> genders { get; set; }
        public List<ContentSourceMapper> contentSources { get; set; }
        public List<string> subTypes { get; set; }


    }
}