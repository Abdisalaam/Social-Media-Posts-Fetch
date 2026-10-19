using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BrandWatchElaqat.ViewModel
{
    public class NewsBarFilterVM
    {
        public int Id { get; set; }
        public string PublicationName { get; set; }
        public string Domain { get; set; }
       
        public string DomainURL { get; set; }
        public HttpPostedFileBase DomainFavIcon { get; set; }
        public byte[] Favicon { get; set; }
       
    }
}