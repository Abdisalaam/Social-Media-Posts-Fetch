using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BrandWatchElaqat.ViewModel
{
    public class ProjectMentionVM
    {
        public int Id { get; set; }
        public string AccountType { get; set; }
        public string Title { get; set; }
        public string Snippet { get; set; }
        public string AddedDate { get; set; }
        public string Assignment { get; set; }
        public string Author { get; set; }
        public string MediaType { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string Region { get; set; }
        public string Continent { get; set; }
        public string DailyVisitors { get; set; }
        public string Domain { get; set; }
        public string Gender { get; set; }
        public string GUIDNumber { get; set; }
        public string Impressions { get; set; }
        public string Interest { get; set; }
        public string Language { get; set; }
        public string MonthlyVisitors { get; set; }
        public string PageType { get; set; }
        public string PageTypeName { get; set; }
        public string ContentSource { get; set; }
        public string ResourceId { get; set; }
        public string QueryID { get; set; }
        public string QueryName { get; set; }
        public string ReachEstimate { get; set; }
        public string Sentiment { get; set; }
        public string URL { get; set; }
        public string Impact { get; set; }
        public string OriginalUrl { get; set; }
        public System.DateTime ReceivedDate { get; set; }
        public int ClientProjectSubCategoryId { get; set; }
        public bool ReleasMentionToClient { get; set; }

        [Required(ErrorMessage = "Project is required. Please Select a Project")]
        public int SelectedSubCategoryId { get; set; }
        public SelectList SubCategoryList { get; set; }


        public int SelectedReleaseMentionId { get; set; }
        public SelectList ReleaseList { get; set; }
    }
}
