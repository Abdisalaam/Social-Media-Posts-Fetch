using System.Collections.Generic;
using Newtonsoft.Json;
namespace BrandWatchElaqat.ViewModel
{
    public class AuthorDataMapper
    {
        [JsonProperty("data")]
        public List<AuthorMapper> Data { get; set; }

    }

}