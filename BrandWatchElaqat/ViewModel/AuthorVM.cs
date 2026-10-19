using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BrandWatchElaqat.ViewModel
{
    public class AuthorVM
    {
        public string AuthorName { get; set; }
        public string Domain { get; set; }
        public int TotalReachEstimate { get; set; }
        public int TotalFollowers { get; set; }
        public string ImageSource { get; set; }
        public string Volume { get; set; }

    }
}