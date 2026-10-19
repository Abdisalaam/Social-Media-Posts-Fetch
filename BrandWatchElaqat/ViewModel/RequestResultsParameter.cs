using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BrandWatchElaqat.ViewModel
{
    public class RequestResultsParameter
    {
        public int _RequestTotalMention { get; set; }
        public string _NextCursor { get; set; }
        public int _ResultsTotal { get; set; }
        public int _ResultsPage { get; set; }
        public int _ReceivedMention { get; set; }


    }
}