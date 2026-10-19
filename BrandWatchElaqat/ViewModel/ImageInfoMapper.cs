using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BrandWatchElaqat.ViewModel
{
    public class ImageInfoMapper
    {
        public string url { get; set; }
        public string md5 { get; set; }
        public List<ObjectMapper> objects { get; set; }

    }
}