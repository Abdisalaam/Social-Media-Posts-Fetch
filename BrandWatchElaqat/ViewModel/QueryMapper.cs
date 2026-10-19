using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BrandWatchElaqat.ViewModel
{
    public class QueryMapper
    {
        public string id { get; set; }
        public string name { get; set; }
        public string displayName { get; set; }
        public string description { get; set; }
        public string creationDate { get; set; }
        public string lastModificationDate { get; set; }
        public List<string> languages { get; set; }
        public string type { get; set; }
        public List<string> highlightTerms { get; set; }
        public string lastModifiedUsername { get; set; }
        public string booleanQuery { get; set; }
        public string monitorId { get; set; }
        public string startDate { get; set; }
        public string percentComplete { get; set; }
        public string samplePercentage { get; set; }
        public string userRequestedSampling { get; set; }
        public string sampled { get; set; }
        public string queryLimitUsage { get; set; }
        public string locationFilter { get; set; }
        public string imageFilter { get; set; }
        public string trackedPageFilter { get; set; }
        public List<string> contentSources { get; set; }
        public List<AudienceMapper> audienceLists { get; set; }
        public List<string> exclusionSnippets { get; set; } 
        public string active { get; set; }
        public string state { get; set; }
        public string projectId { get; set; }
        public List<string> sharedWithProjectIds { get; set; }
        public string sharedFromOtherProject { get; set; }
        public string sentimentClassifierVersion { get; set; }
        public string trendsEnabled { get; set; }
    }
}