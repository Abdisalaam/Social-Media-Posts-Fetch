using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BrandWatchElaqat.Models;

namespace BrandWatchElaqat.ViewModel
{
    public class MentionFilterVM
    {
        public int MentionFilterID { get; set; }

        /// <summary>
        /// Charts & Lists Data
        /// </summary>
        public DashBoardVM DashBoard { get; set; }




        /// <summary>
        /// Date Range
        /// </summary>
        public string StartDate { get; set; }
        public string EndDate { get; set; }

        public Nullable<int> MaxStartDateRange { get; set; }
        public Nullable<int> MaxEndDateRange { get; set; }
        public bool DefaultFilter { get; set; }


        /// <summary>
        /// Project Filters
        /// </summary>

        [Required(ErrorMessage = "Client is required. Please Select a Project")]
        public int SelectedClientId { get; set; }
        public string ClientName { get; set; }

        public SelectList ClientList { get; set; }

        /// <summary>
        /// Project Filters
        /// </summary>

        [Required(ErrorMessage = "Project is required. Please Select a Project")]
        public int SelectedClientProjectId { get; set; }
        public string ProjectName { get; set; }

        public SelectList ClientProjectList { get; set; }

        /// <summary>
        /// Project Global Metrics (Filter)
        /// </summary>
        public int SelectedClientProjectGlobalMetricsId { get; set; }
        public SelectList ClientProjectGlobalMetricsList { get; set; }

      
        /// <summary>
        /// Project Query
        /// </summary>

        public int SelectedQueryId { get; set; }
        public SelectList QueryList { get; set; }

        /// <summary>
        /// News Bar
        /// </summary>

        public int SelectedIncludedDomainId { get; set; }
        public SelectList DomainList { get; set; }
        public Nullable<int> NewsBarMinVisitors { get; set; }
        public string NewsBarMatchingWordes { get; set; }

        /// <summary>
        /// Project Category and Sub Category
        /// </summary>
        public int SelectedExcludedCategoryId { get; set; }
        public int SelectedIncludedCategoryId { get; set; }
        public SelectList CategoriesList { get; set; }
       

        public int SelectedIncludedSubCategoryId { get; set; }
        public int SelectedEXcludedSubCategoryId { get; set; }
        public SelectList SubCategoriesList { get; set; }
       

        /// <summary>
        /// Project Tags
        /// </summary>

        public int SelectedIncludedRuleTagId { get; set; }
        public int SelectedExcludedRuleTagId { get; set; }
        public SelectList RuleTagsList { get; set; }
        

        /// <summary>
        /// Get Content Source from Project Query Content
        /// </summary>

        public int SelectedIncludedContentSourceId { get; set; }
        public int SelectedExcludedContentSourceId { get; set; }
        public SelectList ContentSourcesList { get; set; }
       

       /// <summary>
       /// Author
       /// </summary>


        public int SelectedIncludedAthorGroupId { get; set; }
        public int SelectedExcludedAthorGroupId { get; set; }
        public SelectList AuthorGroupsList { get; set; }
       
        public string IncludeAuthorName { get; set; }
        public bool ExactMatchIncludeAuthorName { get; set; }
        public string ExcludeAuthorName { get; set; }
        public bool ExactMatchExcludeAuthorName { get; set; }

        /// <summary>
        /// Project Locations
        /// </summary>

        public int SelectedIncludedClientProjectLocationId { get; set; }
        public int SelectedExcludedClientProjectLocationId { get; set; }
        public SelectList ClientProjectLocationList { get; set; }
      

        /// <summary>
        /// Project Language
        /// </summary>

        public int SelectedIncludedQueryLanguageId { get; set; }
        public int SelectedExcludedQueryLanguageId { get; set; }
        public SelectList QueryLanguageList { get; set; }
       

        /// <summary>
        /// Workflow
        /// </summary>

        public Nullable<int> SelectedAssignmentId { get; set; }
        public SelectList AssignmentList { get; set; }
        public Nullable<int> SelectedPriorityId { get; set; }
        public SelectList PriorityList { get; set; }
        public Nullable<int> SelectedCheckedMentionId { get; set; }
        public SelectList CheckedMentionList { get; set; }
        public Nullable<int> SelectedMentionStatusId { get; set; }
        public SelectList MentionStatusList { get; set; }
        public bool StarredMentions { get; set; }


        /// <summary>
        /// Mention Type
        /// </summary>

        public int SelectedIncludedMentionTypeId { get; set; }
        public int SelectedExcludedMentionTypeId { get; set; }
        public SelectList MentionTypeList { get; set; }
       


        /// <summary>
        /// React Score Harm, OverAll and Emotionality
        /// </summary>

        public int SelectedIncludedReactScoreOverallId { get; set; }
        public int SelectedExcludedReactScoreOverallId { get; set; }
        public SelectList ReactScoreOverallList { get; set; }
       

        public int SelectedIncludedReactScoreHarmId { get; set; }
        public int SelectedExcludedReactScoreHarmId { get; set; }
        public SelectList ReactScoreHarmList { get; set; }
       

        public int SelectedIncludedReactScoreEmotionalityId { get; set; }
        public int SelectedExcludedReactScoreEmotionalityId { get; set; }
        public SelectList ReactScoreEmotionalityList { get; set; }
       

        /// <summary>
        /// Engagement Score
        /// </summary>

        public Nullable<int> MaxEngagmentScore { get; set; }
        public Nullable<int> MinEngagmentScore { get; set; }

        /// <summary>
        /// Sentiment
        /// </summary>

        public int SelectedSentimentId { get; set; }
        public SelectList SentimentsList { get; set; }
       

        /// <summary>
        ///  Emotion
        /// </summary>

        public int SelectedIncludedEmotionId { get; set; }
        public int SelectedExcludedEmotionId { get; set; }
        public SelectList EmotionList { get; set; }
       

        /// <summary>
        /// Social Panel
        /// </summary>

        public int SelectedIncludedSocialPanelId { get; set; }
        public int SelectedExcludedSocialPanelId { get; set; }
        public SelectList SocialPanelList { get; set; }
       



        /// <summary>
        /// Site 
        /// </summary>

        public int SelectedIncludedClientProjectSiteId { get; set; }
        public int SelectedExcludedClientProjectSiteId { get; set; }
        public SelectList ClientProjectSiteList { get; set; }
       
        public Nullable<int> MaxSiteMonthlyVisitors { get; set; }
        public Nullable<int> MinSiteMonthlyVisitors { get; set; }
        public Nullable<int> MaxSiteDailyVisitors { get; set; }
        public Nullable<int> MinSiteDailyVisitors { get; set; }

        /// <summary>
        /// Impact and Reach
        /// </summary>

        public Nullable<int> MaxReach { get; set; }
        public Nullable<int> MinReach { get; set; }


        public Nullable<int> MaxImpact { get; set; }
        public Nullable<int> MinImpact { get; set; }

        /// <summary>
        /// Twitter Section
        /// </summary>
        public Nullable<int> TwitterMaxReposts { get; set; }
        public Nullable<int> TwitterMinReposts { get; set; }
        public Nullable<int> TwitterMaxLikes { get; set; }
        public Nullable<int> TwitterMinLikes { get; set; }
        public Nullable<int> TwitterMaxTotalPosts { get; set; }
        public Nullable<int> TwitterMinTotalPosts { get; set; }
        public Nullable<int> TwitterMaxFollowing { get; set; }
        public Nullable<int> TwitterMinFollowing { get; set; }
        public Nullable<int> TwitterMaxFollowers { get; set; }
        public Nullable<int> TwitterMinFollowers { get; set; }
        public Nullable<int> TwitterMaxImpressions { get; set; }
        public Nullable<int> TwitterMinImpressions { get; set; }

        public Nullable<int> SelectedTwitterUserRoleId { get; set; }
        public SelectList TwitterUserRoleList { get; set; }

        public int SelectedIncludedTwitterMentionTypeId { get; set; }
        public int SelectedExcludedTwitterMentionTypeId { get; set; }
        public SelectList TwitterMentionTypeList { get; set; }
      


        public Nullable<int> SelectedTwitterVerificationId { get; set; }
        public SelectList TwitterVerificationList { get; set; }

        public Nullable<int> SelectedIncludedTwitterVerificationTypeId { get; set; }
        public Nullable<int> SelectedExcludedTwitterVerificationTypeId { get; set; }
        public SelectList TwitterVerificationTypeList { get; set; }
       

        public Nullable<int> SelectedIncludedTwitterAccountTypeId { get; set; }
        public Nullable<int> SelectedExcludedTwitterAccountTypeId { get; set; }
        public SelectList TwitterAccountTypesList { get; set; }
       

        public int SelectedIncludedTwitterGenderId { get; set; }
        public SelectList TwitterGendersList { get; set; }


        public Nullable<int> SelectedIncludedTwitterProfessionId { get; set; }
        public Nullable<int> SelectedExcludedTwitterProfessionId { get; set; }
        public SelectList TwitterProfessionsList { get; set; }
       


        public Nullable<int> SelectedIncludedTwitterInterestId { get; set; }
        public Nullable<int> SelectedExcludedTwitterInterestId { get; set; }
        public SelectList TwitterInterestsList { get; set; }
       
        /// <summary>
        /// Source Type is new or Old
        /// </summary>
        public Nullable<int> SelectedSourceTypetId { get; set; }
        public SelectList SourceTypeList { get; set; }

        /// <summary>
        /// SubType
        /// </summary>

        public int SelectedIncludedSubTypeId { get; set; }
        public Nullable<int> SelectedExcludedSubTypeId { get; set; }
        public SelectList SubTypesList { get; set; }
       

        /// <summary>
        /// FaceBook
        /// </summary>

        public Nullable<int> SelectedFaceBookUserRoleId { get; set; }
        public SelectList FaceBookUserRoleList { get; set; }
       


        public Nullable<int> SelectedIncludedFaceBookMentionTypeId { get; set; }
        public Nullable<int> SelectedExcludedFaceBookMentionTypeId { get; set; }
        public SelectList FaceBookMentionTypeList { get; set; }

      
        public Nullable<int> FaceBookMaxLikes { get; set; }
        public Nullable<int> FaceBookMinLikes { get; set; }
        public Nullable<int> FaceBookMaxComments { get; set; }
        public Nullable<int> FaceBookMinComments { get; set; }
        public Nullable<int> FaceBookMaxShares { get; set; }
        public Nullable<int> FaceBookMinShares { get; set; }


        /// <summary>
        /// Instgram
        /// </summary>
        public Nullable<int> SelectedIncludedInstagramMentionTypeId { get; set; }
        public Nullable<int> SelectedExcludedInstagramMentionTypeId { get; set; }
        public SelectList InstagramMentionTypeList { get; set; }
       
        public Nullable<int> InstgramMaxInteractions { get; set; }
        public Nullable<int> InstgramMinInteractions { get; set; }
        public Nullable<int> InstgramMaxLikes { get; set; }
        public Nullable<int> InstgramMinLikes { get; set; }
        public Nullable<int> InstgramMaxComments { get; set; }
        public Nullable<int> InstgramMinComments { get; set; }
        public Nullable<int> InstgramMaxPosts { get; set; }
        public Nullable<int> InstgramMinPosts { get; set; }
        public Nullable<int> InstgramMaxFollowers { get; set; }
        public Nullable<int> InstgramMinFollowers { get; set; }
        public Nullable<int> InstgramMaxFollowing { get; set; }
        public Nullable<int> InstgramMinFollowing { get; set; }

        /// <summary>
        /// Bluesky
        /// </summary>
        public Nullable<int> BlueskyMaxLikes { get; set; }
        public Nullable<int> BlueskyMinLikes { get; set; }
        public Nullable<int> BlueskyMaxReplies { get; set; }
        public Nullable<int> BlueskyMinReplies { get; set; }
        public Nullable<int> BlueskyMaxReposts { get; set; }
        public Nullable<int> BlueskyMinReposts { get; set; }
        public Nullable<int> BlueskyMaxQuotes { get; set; }
        public Nullable<int> BlueskyMinQuotes { get; set; }
        public Nullable<int> BlueskyMaxFollowers { get; set; }
        public Nullable<int> BlueskyMinFollowers { get; set; }
        public Nullable<int> BlueskyMaxFollowing { get; set; }
        public Nullable<int> BlueskyMinFollowing { get; set; }
        public Nullable<int> BlueskyMaxTotalPosts { get; set; }
        public Nullable<int> BlueskyMinTotalPosts { get; set; }

        /// <summary>
        /// Reddit
        /// </summary>
        
        public Nullable<int> RedditMaxScore { get; set; }
        public Nullable<int> RedditMinScore { get; set; }
        public Nullable<int> RedditMaxAuthorKarma { get; set; }
        public Nullable<int> RedditMinAuthorKarma { get; set; }
        public Nullable<int> RedditMaxSubscribers { get; set; }
        public Nullable<int> RedditMinSubscribers { get; set; }
        public Nullable<int> RedditMaxCommentCount { get; set; }
        public Nullable<int> RedditMinCommentCount { get; set; }

        public Nullable<int> SelectedRedditPollId { get; set; }
        public SelectList RedditPollList { get; set; }

        public Nullable<int> SelectedRedditNSFWId { get; set; }
        public SelectList RedditNSFWList { get; set; }

        public string RedditIncludeFilter { get; set; }
        public string RedditExcludeFilter { get; set; }




        public int SelectedIncludedSubredditTopicId { get; set; }
        public int SelectedExcludedSubredditTopicId { get; set; }
        public SelectList SubredditTopicList { get; set; }
       

        /// <summary>
        /// Pulication
        /// </summary>

        public string PulicationtName{ get; set; }















    }
}