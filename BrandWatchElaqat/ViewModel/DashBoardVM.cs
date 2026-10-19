using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BrandWatchElaqat.ViewModel
{
    public class DashBoardVM
    {

        public int PositiveSentimentsCount { get; set; }
        public int NeutralSentimentsCount { get; set; }
        public int NegativeSentimentsCount { get; set; }

        public int InternationalMentionCount { get; set; }
        public int RegionalMentionCount { get; set; }
        public int LocalMentionCount { get; set; }
        public int[] ContentSourceSeries { get; set; }
        public string[] ContentSourceLabels { get; set; }
        public string[] ContentSourceColors { get; set; }
        public List<AuthorMapper> AuthorMapperList { get; set; }
        public List<ClientProjectSubCategoryVM> SubcategpryVMList { get; set; }
        public List<DomainVM> DomainVMList { get; set; }
        public List<DashBoardContentSourceVM> DashBoardContentSourceVMist { get; set; }

        public List<AuthorVM> AuthorVMList { get; set; }
        public List<MentionVM> MentionVMList { get; set; }
        public double TotalMentionsCount { get; set; }
        public decimal TotalImpressionsSum { get; set; }
        public decimal TotalEngagementSum { get; set; }
        public decimal TotalReachingsSum { get; set; }
        public int MaleMentionsCount { get; set; }
        public int FemaleMentionsCount { get; set; }
        public int UnkownMentionsCount { get; set; }

        public double MaleMentionsPrecentage { get; set; }
        public double FemaleMentionsPrecentage { get; set; }
        public double UnknownMentionsPrecentage { get; set; }


    }
}