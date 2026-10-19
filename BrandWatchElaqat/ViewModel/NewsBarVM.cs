using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BrandWatchElaqat.ViewModel
{
    public class NewsBarVM
    {
        public HttpPostedFileBase Favicon { get; set; }
        public string PuplicationName { get; set; }
        public int SiteMinMonthlyVisitors { get; set; }
        public int SiteMaxMonthlyVisitors { get; set; }

        public int SiteMinDailyVisitors { get; set; }
        public int SiteMaxDailyVisitors { get; set; }

        public string ContainsThisKeyWord { get; set; }
        public string DomainURL { get; set; }
        public string SiteDomain { get; set; }
    }
}