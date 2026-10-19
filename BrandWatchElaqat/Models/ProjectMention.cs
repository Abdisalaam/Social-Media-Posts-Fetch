using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using BrandWatchElaqat.ViewModel;

namespace BrandWatchElaqat.Models
{
    public partial class ProjectMention
    {
        private const string BrandWatchToken = "b17c9054-36a6-4128-9ad3-32bc7d3ea0d3";
        private const string BrandWatchBasedURL = "https://api.brandwatch.com/projects/";

        public static List<ProjectMention> GetAllProjectMentions()
        {
            var db = new DBModelBrandWatchContainer();

            return db.ProjectMentions.ToList(); 

        }

        public static List<ProjectMention> GetAllProjectMentionsForProject(int projectID)
        {
            var db = new DBModelBrandWatchContainer();

            return db.ProjectMentions.Where(e => e.ClientProjectId == projectID).ToList();

        }
       
       
        public static bool DeleteAllMentions()
        {
            var db = new DBModelBrandWatchContainer();
            var _Mentions = db.ProjectMentions.ToList();
            var _MentionSubCategoryDetails = db.MentionSubCategoryDetails.ToList();
           
            if (_Mentions.Any())
            {
                try
                {
                   
                    if (_MentionSubCategoryDetails.Any())
                    {
                        db.MentionSubCategoryDetails.RemoveRange(_MentionSubCategoryDetails);
                    }
                    db.ProjectMentions.RemoveRange(_Mentions);
                    db.SaveChanges();
                    return true;
                }
                catch (Exception ex)
                {
                    var meg = ex.Message;
                    throw ex;
                }
               
                
            }
            return false;

        }
        public static int DeleteAllProjectMentionsFromProject(int projectID)
        {
            var db = new DBModelBrandWatchContainer();
            try
            {
                var clientProject = db.ClientProjects.Find(projectID);
                if (clientProject.ProjectMentions.Any())
                {
                    foreach (var _mention in clientProject.ProjectMentions)
                    {
                        if (_mention.MentionSubCategoryDetails.Any())
                        {
                            db.MentionSubCategoryDetails.RemoveRange(_mention.MentionSubCategoryDetails);
                        }
                      
                    }

                    db.ProjectMentions.RemoveRange(clientProject.ProjectMentions);

                    db.SaveChanges();

                }
                return clientProject.Id;

            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        public static ProjectMention GetProjectMentionDetails(int projectMentionID)
        {
            var db = new DBModelBrandWatchContainer();

            return db.ProjectMentions.Find(projectMentionID);
        }

        public static URLPreParer PrepareUrlTobeCaled(MentionFilterVM mentionFilterVM)
        {

            var db = new DBModelBrandWatchContainer();
            try
            {
                MentionFilter mentionFilter = db.MentionFilters.Find(mentionFilterVM.MentionFilterID);

               
                if (mentionFilter != null)
                {
                    AllowedMinAndMaxAndMultiOptionsMentionFilter allowedMinAndMaxAndMultiOptions = null;
                    bool mentionFilterHasAllowedMinAndMax = false;
                    if (mentionFilter.AllowedMinAndMaxAndMultiOptionsMentionFilters.Any())
                    {
                        allowedMinAndMaxAndMultiOptions = mentionFilter.AllowedMinAndMaxAndMultiOptionsMentionFilters.FirstOrDefault();
                        mentionFilterHasAllowedMinAndMax = true;
                    }
                    else
                    {
                        allowedMinAndMaxAndMultiOptions = new AllowedMinAndMaxAndMultiOptionsMentionFilter();
                    }
                    URLPreParer uRLPreParer = new URLPreParer();
                    uRLPreParer.token = BrandWatchToken;
                    if (mentionFilterVM.MaxStartDateRange == null|| mentionFilterVM.MaxStartDateRange < 0)
                    {
                        mentionFilterVM.MaxStartDateRange = 0;
                    }
                    if (mentionFilterVM.MaxEndDateRange == null || mentionFilterVM.MaxEndDateRange < 0)
                    {
                        mentionFilterVM.MaxEndDateRange = 0;
                    }
                    mentionFilter.MaxStartDateRange = mentionFilterVM.MaxStartDateRange;
                    mentionFilter.MaxEndDateRange = mentionFilterVM.MaxEndDateRange;

                    // Get the current date and time
                    DateTime currentDate = DateTime.Now;

                    // Calculate the date 90 days ago
                    DateTime MaxStartDateAllowed = currentDate.AddDays(-mentionFilter.MaxStartDateRange.Value);


                    
                    if (DateTime.TryParse(mentionFilterVM.StartDate, out DateTime startDateTime))
                    {
                        if (startDateTime > MaxStartDateAllowed)
                        {
                            int totalAllowedDate = mentionFilter.MaxStartDateRange.Value + 1;
                            mentionFilterVM.StartDate = currentDate.Date.AddDays(-totalAllowedDate).ToString("yyyy-MM-dd");
                        }
                        else
                        {
                            mentionFilterVM.StartDate = startDateTime.Date.AddDays(-1).ToString("yyyy-MM-dd");

                        }

                    }
                    else
                    {
                        mentionFilterVM.StartDate = currentDate.Date.AddDays(-1).ToString("yyyy-MM-dd");

                    }

                    DateTime MaxEndDateAllowed = currentDate.AddDays(-mentionFilter.MaxEndDateRange.Value);

                    if (DateTime.TryParse(mentionFilterVM.EndDate, out DateTime endDateTime))
                    {
                        if (endDateTime > MaxEndDateAllowed)
                        {
                            mentionFilterVM.EndDate = endDateTime.Date.AddDays(-mentionFilter.MaxEndDateRange.Value).ToString("yyyy-MM-dd");
                        }                      

                    }
                    else
                    {
                        mentionFilterVM.EndDate = currentDate.ToString("yyyy-MM-dd");

                    }


                    string startDate = "&startDate=" + mentionFilterVM.StartDate + "T21:00:00";
                    string endDate = "&endDate=" + mentionFilterVM.EndDate + "T21:00:00";
                    string pageSize = "&pageSize=5000";
                    string authorsLimit = "&limit=10";

                    ClientProject clientProject = db.ClientProjects.Find(mentionFilter.ClientProjectID);
                    uRLPreParer.projectId = clientProject.Id;
                    uRLPreParer.CompleteParametersForAPI = BrandWatchBasedURL + clientProject.BrandwatchProjectID + "/data/mentions?";
                    uRLPreParer.CompleteParametersForTweeterAuthorAPI = BrandWatchBasedURL + clientProject.BrandwatchProjectID + "/data/volume/toptweeters/queries?";

                    // Gets the queries id                   
                    if (mentionFilter.IncludedQueries.Any())
                    {

                        foreach (var _query in mentionFilter.IncludedQueries)
                        {
                            uRLPreParer.queryId += clientProject.ClientProjectQueries.Where(e => e.Id == _query.QueryId).FirstOrDefault().BrandWatchQueryId + ",";
                        }
                        uRLPreParer.queryId = uRLPreParer.queryId.Remove(uRLPreParer.queryId.Length - 1);
                    }
                    else
                    {
                        foreach (var query in clientProject.ClientProjectQueries)
                        {
                            if (query.IsActive)
                            {
                                uRLPreParer.queryId += query.BrandWatchQueryId + ",";
                                IncludedQuery includedQuery = new IncludedQuery();
                                includedQuery.QueryId = query.Id;
                                includedQuery.QueryName = query.QuerieName;
                                mentionFilter.IncludedQueries.Add(includedQuery);
                            }
                        }
                        uRLPreParer.queryId = uRLPreParer.queryId.Remove(uRLPreParer.queryId.Length - 1);
                    }
                    uRLPreParer.queryId = "queryId=" + uRLPreParer.queryId;
                    uRLPreParer.CompleteParametersForAPI += uRLPreParer.queryId + startDate + endDate + pageSize;
                    uRLPreParer.CompleteParametersForTweeterAuthorAPI += uRLPreParer.queryId + startDate + endDate + authorsLimit;
                    uRLPreParer.CompleteParametersForAPIForCursor = uRLPreParer.CompleteParametersForAPI;
                    // Gets the tags

                    if (mentionFilter.IncludedRuleTags.Any())
                    {
                        uRLPreParer.tag = "&tag=";


                        foreach (var _tag in mentionFilter.IncludedRuleTags)
                        {
                            uRLPreParer.tag += clientProject.ProjectRuleTags.Where(e => e.Id == _tag.RuleTagID).FirstOrDefault().BrandWatchRuleTagID + ",";
                        }
                        uRLPreParer.tag = uRLPreParer.tag.Remove(uRLPreParer.tag.Length - 1);
                        uRLPreParer.CompleteParametersForAPI += uRLPreParer.tag;
                    }

                    if (mentionFilter.ExcludedRuleTags.Any())
                    {
                        uRLPreParer.xtag = "&xtag=";


                        foreach (var _xtag in mentionFilter.ExcludedRuleTags)
                        {
                            uRLPreParer.xtag += clientProject.ProjectRuleTags.Where(e => e.Id == _xtag.RuleTagID).FirstOrDefault().BrandWatchRuleTagID + ",";
                        }
                        uRLPreParer.xtag = uRLPreParer.xtag.Remove(uRLPreParer.xtag.Length - 1);
                        uRLPreParer.CompleteParametersForAPI += uRLPreParer.xtag;
                    }
                    // Gets the Parent Category
                    if (mentionFilter.IncludedCategories.Any())
                    {
                        uRLPreParer.parentCategory = "&parentCategory=";


                        foreach (var _parentCategory in mentionFilter.IncludedCategories)
                        {
                            uRLPreParer.parentCategory += clientProject.ClientProjectCategories.Where(e => e.Id == _parentCategory.CategoryID).FirstOrDefault().BrandWatchCategoeryID + ",";
                        }
                        uRLPreParer.parentCategory = uRLPreParer.parentCategory.Remove(uRLPreParer.parentCategory.Length - 1);
                        uRLPreParer.CompleteParametersForAPI += uRLPreParer.parentCategory;
                    }
                    if (mentionFilter.ExcludedCategories.Any())
                    {
                        uRLPreParer.xparentCategory = "&xparentCategory=";


                        foreach (var _xparentCategory in mentionFilter.ExcludedCategories)
                        {
                            uRLPreParer.xparentCategory += clientProject.ClientProjectCategories.Where(e => e.Id == _xparentCategory.CategoryID).FirstOrDefault().BrandWatchCategoeryID + ",";
                        }
                        uRLPreParer.xparentCategory = uRLPreParer.xparentCategory.Remove(uRLPreParer.xparentCategory.Length - 1);
                        uRLPreParer.CompleteParametersForAPI += uRLPreParer.xparentCategory;
                    }
                    if (mentionFilter.IncludedSubCatgories.Any())
                    {
                        uRLPreParer.category = "&category=";


                        foreach (var _category in mentionFilter.IncludedSubCatgories)
                        {
                            uRLPreParer.category +=  db.ClientProjectSubCategories.Where(e => e.Id == _category.SubCateID).FirstOrDefault().BrandWatchSubCategoryID + ",";
                        }
                        uRLPreParer.category = uRLPreParer.category.Remove(uRLPreParer.category.Length - 1);
                        uRLPreParer.CompleteParametersForAPI += uRLPreParer.category;
                    }
                    if (mentionFilter.ExcludedSubCatgories.Any())
                    {
                        uRLPreParer.xcategory = "&xcategory=";


                        foreach (var _xcategory in mentionFilter.ExcludedSubCatgories)
                        {
                            uRLPreParer.xcategory += db.ClientProjectSubCategories.Where(e => e.Id == _xcategory.SubCateID).FirstOrDefault().BrandWatchSubCategoryID + ",";
                        }
                        uRLPreParer.xcategory = uRLPreParer.xcategory.Remove(uRLPreParer.xcategory.Length - 1);
                        uRLPreParer.CompleteParametersForAPI += uRLPreParer.xcategory;
                    }
                    // Content source
                    if (mentionFilter.IncludedContentSources.Any())
                    {
                        uRLPreParer.pageType = "&pageType=";


                        foreach (var _contentSource in mentionFilter.IncludedContentSources)
                        {
                            uRLPreParer.pageType += _contentSource.CountentSourceName.ToLower() + ",";
                        }
                        uRLPreParer.pageType = uRLPreParer.pageType.Remove(uRLPreParer.pageType.Length - 1);
                        uRLPreParer.CompleteParametersForAPI += uRLPreParer.pageType;
                    }
                    if (mentionFilter.ExcludedContentSources.Any())
                    {
                        uRLPreParer.xpageType = "&xpageType=";


                        foreach (var _xcontentSource in mentionFilter.ExcludedContentSources)
                        {
                            uRLPreParer.xpageType += _xcontentSource.CountentSourceName.ToLower() + ",";
                        }
                        uRLPreParer.xpageType = uRLPreParer.xpageType.Remove(uRLPreParer.xpageType.Length - 1);
                        uRLPreParer.CompleteParametersForAPI += uRLPreParer.xpageType;
                    }
                    
                    // Mention Type
                    if (mentionFilter.IncludedMentionTypes.Any())
                    {
                        uRLPreParer.threadEntryType = "&threadEntryType=";


                        foreach (var _mentionType in mentionFilter.IncludedMentionTypes)
                        {
                            uRLPreParer.threadEntryType += _mentionType.MentionTypeName.ToLower() + ",";
                        }
                        uRLPreParer.threadEntryType = uRLPreParer.threadEntryType.Remove(uRLPreParer.threadEntryType.Length - 1);
                        uRLPreParer.CompleteParametersForAPI += uRLPreParer.threadEntryType;
                    }

                    if (mentionFilter.ExcludedMentionTypes.Any())
                    {
                        uRLPreParer.xthreadEntryType = "&xthreadEntryType=";


                        foreach (var _mentionType in mentionFilter.ExcludedMentionTypes)
                        {
                            uRLPreParer.xthreadEntryType += _mentionType.MentionTypeName.ToLower() + ",";
                        }
                        uRLPreParer.xthreadEntryType = uRLPreParer.xthreadEntryType.Remove(uRLPreParer.xthreadEntryType.Length - 1);
                        uRLPreParer.CompleteParametersForAPI += uRLPreParer.xthreadEntryType;
                    }
                    //bool includedReactScore = false;
                    //if (mentionFilter.IncludedReactScoreOveralls.Any())
                    //{

                    //    includedReactScore = true;
                    //    uRLPreParer.reactScoreOverall = "&reactScoreOverall=";


                    //    foreach (var _reactScoreOverall in mentionFilter.IncludedReactScoreOveralls)
                    //    {
                    //        uRLPreParer.reactScoreOverall += _reactScoreOverall.ScoreOverAllName.ToLower() + ",";
                    //    }
                    //    uRLPreParer.reactScoreOverall = uRLPreParer.reactScoreOverall.Remove(uRLPreParer.reactScoreOverall.Length - 1);
                    //    uRLPreParer.CompleteParametersForAPI += uRLPreParer.reactScoreOverall;
                    //}
                    //if (mentionFilter.ExcludedReactScoreOveralls.Any())
                    //{
                    //    uRLPreParer.xreactScoreOverall = "&xreactScoreOverall=";


                    //    foreach (var _reactScoreOverall in mentionFilter.ExcludedReactScoreOveralls)
                    //    {
                    //        uRLPreParer.xreactScoreOverall += _reactScoreOverall.ScoreOverAllName.ToLower() + ",";
                    //    }
                    //    uRLPreParer.xreactScoreOverall = uRLPreParer.xreactScoreOverall.Remove(uRLPreParer.xreactScoreOverall.Length - 1);
                    //    uRLPreParer.CompleteParametersForAPI += uRLPreParer.xreactScoreOverall;
                    //}

                    //if (mentionFilter.IncludedReactScoreHarms.Any())
                    //{
                    //    includedReactScore = true;

                    //    uRLPreParer.reactScoreHarmful = "&reactScoreHarmful=";


                    //    foreach (var _reactScoreHarmful in mentionFilter.IncludedReactScoreHarms)
                    //    {
                    //        uRLPreParer.reactScoreHarmful += _reactScoreHarmful.ScoreHarmName.ToLower() + ",";
                    //    }
                    //    uRLPreParer.reactScoreHarmful = uRLPreParer.reactScoreHarmful.Remove(uRLPreParer.reactScoreHarmful.Length - 1);
                    //    uRLPreParer.CompleteParametersForAPI += uRLPreParer.reactScoreHarmful;
                    //}
                    //if (mentionFilter.ExcludedReactScoreHarms.Any())
                    //{
                    //    uRLPreParer.xreactScoreHarmful = "&xreactScoreHarmful=";


                    //    foreach (var _reactScoreHarmful in mentionFilter.ExcludedReactScoreHarms)
                    //    {
                    //        uRLPreParer.xreactScoreHarmful += _reactScoreHarmful.ScoreHarmName.ToLower() + ",";
                    //    }
                    //    uRLPreParer.xreactScoreHarmful = uRLPreParer.xreactScoreHarmful.Remove(uRLPreParer.xreactScoreHarmful.Length - 1);
                    //    uRLPreParer.CompleteParametersForAPI += uRLPreParer.xreactScoreHarmful;
                    //}

                    //if (mentionFilter.IncludedReactScoreEmotionalities.Any())
                    //{
                    //    includedReactScore = true;

                    //    uRLPreParer.reactScoreEmotionality = "&reactScoreEmotionality=";


                    //    foreach (var _reactScoreEmotionality in mentionFilter.IncludedReactScoreEmotionalities)
                    //    {
                    //        uRLPreParer.reactScoreEmotionality += _reactScoreEmotionality.EmotionalityName.ToLower() + ",";
                    //    }
                    //    uRLPreParer.reactScoreEmotionality = uRLPreParer.reactScoreEmotionality.Remove(uRLPreParer.reactScoreEmotionality.Length - 1);
                    //    uRLPreParer.CompleteParametersForAPI += uRLPreParer.reactScoreEmotionality;
                    //}
                    //if (mentionFilter.ExcludedReactScoreEmotionalities.Any())
                    //{
                    //    uRLPreParer.xreactScoreEmotionality = "&xreactScoreEmotionality=";


                    //    foreach (var _reactScoreEmotionality in mentionFilter.ExcludedReactScoreEmotionalities)
                    //    {
                    //        uRLPreParer.xreactScoreEmotionality += _reactScoreEmotionality.EmotionalityName.ToLower() + ",";
                    //    }
                    //    uRLPreParer.xreactScoreEmotionality = uRLPreParer.xreactScoreEmotionality.Remove(uRLPreParer.xreactScoreEmotionality.Length - 1);
                    //    uRLPreParer.CompleteParametersForAPI += uRLPreParer.xreactScoreEmotionality;
                    //}

                    //if (includedReactScore)
                    //{
                    //    uRLPreParer.CompleteParametersForAPI += "&includeReactScore=true";
                    //}

                    allowedMinAndMaxAndMultiOptions.MinAllowedEngagmentScore = null;

                    // Engagement Score
                    if (mentionFilterVM.MinEngagmentScore > 0)
                    {
                        allowedMinAndMaxAndMultiOptions.MinAllowedEngagmentScore = mentionFilterVM.MinEngagmentScore;
                        uRLPreParer.engagementScoreMin = "&engagementScoreMin=" + mentionFilterVM.MinEngagmentScore.ToString();
                        uRLPreParer.CompleteParametersForAPI += uRLPreParer.engagementScoreMin;
                        uRLPreParer.CompleteParametersForTweeterAuthorAPI += uRLPreParer.engagementScoreMin;

                    }
                    allowedMinAndMaxAndMultiOptions.MaxAllowedEngagmentScore = null;

                    if (mentionFilterVM.MaxEngagmentScore > 0)
                    {
                        allowedMinAndMaxAndMultiOptions.MaxAllowedEngagmentScore = mentionFilterVM.MaxEngagmentScore;
                        uRLPreParer.engagementScoreMax = "&engagementScoreMax=" + mentionFilterVM.MaxEngagmentScore.ToString();
                        uRLPreParer.CompleteParametersForAPI += uRLPreParer.engagementScoreMax;
                        uRLPreParer.CompleteParametersForTweeterAuthorAPI += uRLPreParer.engagementScoreMax;

                    }
                   
                    // Sentiment
                    if (mentionFilter.IncludedSentiments.Any())
                    {
                        uRLPreParer.sentiment = "&sentiment=";


                        foreach (var _sentiment in mentionFilter.IncludedSentiments)
                        {
                            uRLPreParer.sentiment += _sentiment.SentimentName.ToLower() + ",";
                        }
                        uRLPreParer.sentiment = uRLPreParer.sentiment.Remove(uRLPreParer.sentiment.Length - 1);
                        uRLPreParer.CompleteParametersForAPI += uRLPreParer.sentiment;
                    }

                    // Emotions
                    if (mentionFilter.IncludedEmotions.Any())
                    {
                       // includedReactScore = true;

                        uRLPreParer.classifications = "&classifications=";


                        foreach (var _classifications in mentionFilter.IncludedEmotions)
                        {
                            uRLPreParer.classifications += _classifications.EmotionName.ToLower() + ",";
                        }
                        uRLPreParer.classifications = uRLPreParer.classifications.Remove(uRLPreParer.classifications.Length - 1);
                        uRLPreParer.CompleteParametersForAPI += uRLPreParer.classifications;
                    }
                    if (mentionFilter.ExcludedEmotions.Any())
                    {
                        uRLPreParer.xclassifications = "&xclassifications=";


                        foreach (var _classifications in mentionFilter.ExcludedEmotions)
                        {
                            uRLPreParer.xclassifications += _classifications.EmotionName.ToLower() + ",";
                        }
                        uRLPreParer.xclassifications = uRLPreParer.xclassifications.Remove(uRLPreParer.xclassifications.Length - 1);
                        uRLPreParer.CompleteParametersForAPI += uRLPreParer.xclassifications;
                    }
                    // Author
                    allowedMinAndMaxAndMultiOptions.ExactMatchIncludedAuthorName = null;
                    allowedMinAndMaxAndMultiOptions.IncludedAuthorName = null;

                    if (!string.IsNullOrEmpty(mentionFilterVM.IncludeAuthorName))
                    {

                        if (mentionFilterVM.ExactMatchIncludeAuthorName)
                        {
                            allowedMinAndMaxAndMultiOptions.ExactMatchIncludedAuthorName = mentionFilterVM.ExactMatchIncludeAuthorName;
                            allowedMinAndMaxAndMultiOptions.IncludedAuthorName = mentionFilterVM.IncludeAuthorName;
                            uRLPreParer.exactAuthor = "&exactAuthor=" + mentionFilterVM.IncludeAuthorName;
                            uRLPreParer.CompleteParametersForAPI += uRLPreParer.exactAuthor;
                            uRLPreParer.CompleteParametersForTweeterAuthorAPI += uRLPreParer.exactAuthor;
                        }
                        else
                        {
                            allowedMinAndMaxAndMultiOptions.ExactMatchIncludedAuthorName = false;
                            allowedMinAndMaxAndMultiOptions.IncludedAuthorName = mentionFilterVM.IncludeAuthorName;

                            uRLPreParer.author = "&author=" + mentionFilterVM.IncludeAuthorName;
                            uRLPreParer.CompleteParametersForAPI += uRLPreParer.author;
                            uRLPreParer.CompleteParametersForTweeterAuthorAPI += uRLPreParer.author;
                        }
                    }
                    allowedMinAndMaxAndMultiOptions.ExactMatchExcludedAuthorName = null;
                    allowedMinAndMaxAndMultiOptions.ExcludedAuthorName = null;
                    if (!string.IsNullOrEmpty(mentionFilterVM.ExcludeAuthorName))
                    {
                        if (mentionFilterVM.ExactMatchExcludeAuthorName)
                        {
                            allowedMinAndMaxAndMultiOptions.ExactMatchExcludedAuthorName = mentionFilterVM.ExactMatchExcludeAuthorName;
                            allowedMinAndMaxAndMultiOptions.ExcludedAuthorName = mentionFilterVM.ExcludeAuthorName;

                            uRLPreParer.xexactAuthor = "&xexactAuthor=" + mentionFilterVM.ExcludeAuthorName;
                            uRLPreParer.CompleteParametersForAPI += uRLPreParer.xexactAuthor;
                            uRLPreParer.CompleteParametersForTweeterAuthorAPI += uRLPreParer.xexactAuthor;
                        }
                        else
                        {
                            allowedMinAndMaxAndMultiOptions.ExactMatchExcludedAuthorName = false;
                            allowedMinAndMaxAndMultiOptions.ExcludedAuthorName = mentionFilterVM.ExcludeAuthorName;

                            uRLPreParer.xauthor = "&xauthor=" + mentionFilterVM.ExcludeAuthorName;
                            uRLPreParer.CompleteParametersForAPI += uRLPreParer.xauthor;
                            uRLPreParer.CompleteParametersForTweeterAuthorAPI += uRLPreParer.xauthor;
                        }
                    }

                    if (mentionFilter.IncludedAuthorGroups.Any())
                    {
                        uRLPreParer.authorGroup = "&authorGroup=";


                        foreach (var _authorGroup in mentionFilter.IncludedAuthorGroups)
                        {
                            AuthorGroup authorGroup = clientProject.AuthorGroups.Where(a => a.Id == _authorGroup.AuthorGroupID).FirstOrDefault();
                            uRLPreParer.authorGroup += authorGroup.BrandWatchAuthorGroupID + ",";
                        }
                        uRLPreParer.authorGroup = uRLPreParer.authorGroup.Remove(uRLPreParer.authorGroup.Length - 1);
                        uRLPreParer.CompleteParametersForAPI += uRLPreParer.authorGroup;
                    }

                    if (mentionFilter.ExcludedAuthorGroups.Any())
                    {
                        uRLPreParer.xauthorGroup = "&xauthorGroup=";


                        foreach (var _authorGroup in mentionFilter.ExcludedAuthorGroups)
                        {
                            AuthorGroup authorGroup = clientProject.AuthorGroups.Where(a => a.Id == _authorGroup.AuthorGroupID).FirstOrDefault();
                            uRLPreParer.xauthorGroup += authorGroup.BrandWatchAuthorGroupID + ",";
                        }
                        uRLPreParer.xauthorGroup = uRLPreParer.xauthorGroup.Remove(uRLPreParer.xauthorGroup.Length - 1);
                        uRLPreParer.CompleteParametersForAPI += uRLPreParer.xauthorGroup;
                    }
                    // Locations
                    if (mentionFilter.IncludedLocations.Any())
                    {
                        uRLPreParer.locationGroup = "&locationGroup=";


                        foreach (var _location in mentionFilter.IncludedLocations)
                        {
                            ProjectLocation projectLocation = clientProject.ProjectLocations.Where(l => l.Id == _location.LocationID).FirstOrDefault();
                            uRLPreParer.locationGroup += projectLocation.BrandWatchGroupLocationID + ",";
                        }
                        uRLPreParer.locationGroup = uRLPreParer.locationGroup.Remove(uRLPreParer.locationGroup.Length - 1);
                        uRLPreParer.CompleteParametersForAPI += uRLPreParer.locationGroup;
                    }

                    if (mentionFilter.ExcludedLocations.Any())
                    {
                        uRLPreParer.xlocationGroup = "&xlocationGroup=";


                        foreach (var _location in mentionFilter.ExcludedLocations)
                        {
                            ProjectLocation projectLocation = clientProject.ProjectLocations.Where(l => l.Id == _location.LocationID).FirstOrDefault();
                            uRLPreParer.xlocationGroup += projectLocation.BrandWatchGroupLocationID + ",";
                        }
                        uRLPreParer.xlocationGroup = uRLPreParer.xlocationGroup.Remove(uRLPreParer.xlocationGroup.Length - 1);
                        uRLPreParer.CompleteParametersForAPI += uRLPreParer.xlocationGroup;
                    }

                    // Languages
                    if (mentionFilter.IncludedLanguages.Any())
                    {
                        uRLPreParer.language = "&language=";


                        foreach (var _language in mentionFilter.IncludedLanguages)
                        {
                            uRLPreParer.language += _language.LanguageName + ",";
                        }
                        uRLPreParer.language = uRLPreParer.language.Remove(uRLPreParer.language.Length - 1);
                        uRLPreParer.CompleteParametersForAPI += uRLPreParer.language;
                    }
                    if (mentionFilter.ExcludedLanguages.Any())
                    {
                        uRLPreParer.xlanguage = "&xlanguage=";


                        foreach (var _language in mentionFilter.ExcludedLanguages)
                        {
                            uRLPreParer.xlanguage += _language.LanguageName + ",";
                        }
                        uRLPreParer.xlanguage = uRLPreParer.xlanguage.Remove(uRLPreParer.xlanguage.Length - 1);
                        uRLPreParer.CompleteParametersForAPI += uRLPreParer.xlanguage;
                    }

                    // Workflow
                    allowedMinAndMaxAndMultiOptions.Assigned = null;
                    if (mentionFilterVM.SelectedAssignmentId > 0)
                    {
                        allowedMinAndMaxAndMultiOptions.Assigned = mentionFilterVM.SelectedAssignmentId;

                        Assignment assignment = db.Assignments.Find(mentionFilterVM.SelectedAssignmentId);
                        uRLPreParer.assigned = "&assigned=" + assignment.EmployeeUserName;
                        uRLPreParer.CompleteParametersForAPI += uRLPreParer.assigned;
                    }
                    allowedMinAndMaxAndMultiOptions.Checked = null;
                    if (mentionFilterVM.SelectedCheckedMentionId > 0)
                    {
                        allowedMinAndMaxAndMultiOptions.Checked = mentionFilterVM.SelectedCheckedMentionId;
                        CheckedMention checkedMention = db.CheckedMentions.Find(mentionFilterVM.SelectedCheckedMentionId);
                        if (checkedMention.CheckType == "Yes")
                        {
                            uRLPreParer.Checked = "&checked=true";
                            uRLPreParer.CompleteParametersForAPI += uRLPreParer.Checked;

                        }
                       

                    }
                    allowedMinAndMaxAndMultiOptions.Priority = null;
                    if (mentionFilterVM.SelectedPriorityId > 0)
                    {
                        allowedMinAndMaxAndMultiOptions.Priority = mentionFilterVM.SelectedPriorityId;
                        Priority priority = db.Priorities.Find(mentionFilterVM.SelectedPriorityId);
                        uRLPreParer.priority = "&priority=" + priority.PriorityType.ToLower();
                        uRLPreParer.CompleteParametersForAPI += uRLPreParer.priority;

                    }
                    allowedMinAndMaxAndMultiOptions.Status = null;
                    if (mentionFilterVM.SelectedMentionStatusId > 0)
                    {
                        allowedMinAndMaxAndMultiOptions.Status = mentionFilterVM.SelectedMentionStatusId;
                        MentionStatus mentionStatus = db.MentionStatus.Find(mentionFilterVM.SelectedMentionStatusId);
                        uRLPreParer.status = "&status=" + mentionStatus.Status.ToLower();
                        uRLPreParer.CompleteParametersForAPI += uRLPreParer.status;

                    }
                    if ( mentionFilterVM.StarredMentions)
                    {
                        allowedMinAndMaxAndMultiOptions.Starred = true;
                        uRLPreParer.starred = "&starred=true";
                        uRLPreParer.CompleteParametersForAPI += uRLPreParer.starred;
                    }
                    
                    // Sites
                    if (mentionFilter.IncludedClientProjectSites.Any())
                    {
                        uRLPreParer.siteGroup = "&siteGroup=";


                        foreach (var _siteGroup in mentionFilter.IncludedClientProjectSites)
                        {
                            uRLPreParer.siteGroup += clientProject.ClientProjectSites.Where(s => s.Id == _siteGroup.SiteID).FirstOrDefault().BrandWatchSiteID + ",";
                        }
                        uRLPreParer.siteGroup = uRLPreParer.siteGroup.Remove(uRLPreParer.siteGroup.Length - 1);
                        uRLPreParer.CompleteParametersForAPI += uRLPreParer.siteGroup;
                    }

                    if (mentionFilter.ExcludedClientProjectSites.Any())
                    {
                        uRLPreParer.xsiteGroup = "&xsiteGroup=";


                        foreach (var _siteGroup in mentionFilter.ExcludedClientProjectSites)
                        {
                            uRLPreParer.xsiteGroup += clientProject.ClientProjectSites.Where(s => s.Id == _siteGroup.SiteID).FirstOrDefault().BrandWatchSiteID + ",";
                        }
                        uRLPreParer.xsiteGroup = uRLPreParer.xsiteGroup.Remove(uRLPreParer.xsiteGroup.Length - 1);
                        uRLPreParer.CompleteParametersForAPI += uRLPreParer.xsiteGroup;
                    }

                    allowedMinAndMaxAndMultiOptions.NewsBarMinVisitors = null;
                    if (mentionFilterVM.NewsBarMinVisitors > 0)
                    {
                        allowedMinAndMaxAndMultiOptions.NewsBarMinVisitors = mentionFilterVM.NewsBarMinVisitors;
                        
                    }
                    allowedMinAndMaxAndMultiOptions.NewsBarMatchingWordes = null;
                    if (!string.IsNullOrEmpty(mentionFilterVM.NewsBarMatchingWordes))
                    {
                        allowedMinAndMaxAndMultiOptions.NewsBarMatchingWordes = mentionFilterVM.NewsBarMatchingWordes;

                    }
                    allowedMinAndMaxAndMultiOptions.MinAllowedMonthllySiteVisitors = null;
                    if (mentionFilterVM.MinSiteMonthlyVisitors > 0)
                    {
                        allowedMinAndMaxAndMultiOptions.MinAllowedMonthllySiteVisitors = mentionFilterVM.MinSiteMonthlyVisitors;
                        uRLPreParer.monthlyVisitorsMin = "&monthlyVisitorsMin=" + mentionFilterVM.MinSiteMonthlyVisitors.ToString();
                        uRLPreParer.CompleteParametersForAPI += uRLPreParer.monthlyVisitorsMin;
                        uRLPreParer.CompleteParametersForTweeterAuthorAPI += uRLPreParer.monthlyVisitorsMin;
                    }
                    allowedMinAndMaxAndMultiOptions.MaxAllowedMonthllySiteVisitors = null;
                    if (mentionFilterVM.MaxSiteMonthlyVisitors > 0)
                    {
                        allowedMinAndMaxAndMultiOptions.MaxAllowedMonthllySiteVisitors = mentionFilterVM.MaxSiteMonthlyVisitors;

                        uRLPreParer.monthlyVisitorsMax = "&monthlyVisitorsMax=" + mentionFilterVM.MaxSiteMonthlyVisitors.ToString();
                        uRLPreParer.CompleteParametersForAPI += uRLPreParer.monthlyVisitorsMax;
                        uRLPreParer.CompleteParametersForTweeterAuthorAPI += uRLPreParer.monthlyVisitorsMax;

                    }
                    allowedMinAndMaxAndMultiOptions.MinAllowedDaillySiteVisitors = null;
                    if (mentionFilterVM.MinSiteDailyVisitors > 0)
                    {
                        allowedMinAndMaxAndMultiOptions.MinAllowedDaillySiteVisitors = mentionFilterVM.MinSiteDailyVisitors;
                        uRLPreParer.dailyVisitorsMin = "&dailyVisitorsMin=" + mentionFilterVM.MinSiteDailyVisitors.ToString();
                        uRLPreParer.CompleteParametersForAPI += uRLPreParer.dailyVisitorsMin;

                    }
                    allowedMinAndMaxAndMultiOptions.MaxAllowedDaillySiteVisitors = null;
                    if (mentionFilterVM.MaxSiteDailyVisitors > 0)
                    {
                        allowedMinAndMaxAndMultiOptions.MaxAllowedDaillySiteVisitors = mentionFilterVM.MaxSiteDailyVisitors;

                        uRLPreParer.dailyVisitorsMax = "&dailyVisitorsMax=" + mentionFilterVM.MaxSiteDailyVisitors.ToString();
                        uRLPreParer.CompleteParametersForAPI += uRLPreParer.dailyVisitorsMax;
                    }
                    // Reach
                    allowedMinAndMaxAndMultiOptions.MinReach = null;
                    if (mentionFilterVM.MinReach > 0)
                    {
                        allowedMinAndMaxAndMultiOptions.MinReach = mentionFilterVM.MinReach;
                        uRLPreParer.reachEstimateMin = "&reachEstimateMin=" + mentionFilterVM.MinReach.ToString();
                        uRLPreParer.CompleteParametersForAPI += uRLPreParer.reachEstimateMin;
                        uRLPreParer.CompleteParametersForTweeterAuthorAPI += uRLPreParer.reachEstimateMin;
                    }
                    allowedMinAndMaxAndMultiOptions.MaxReach = null;
                    if (mentionFilterVM.MaxReach > 0)
                    {
                        allowedMinAndMaxAndMultiOptions.MaxReach = mentionFilterVM.MaxReach;

                        uRLPreParer.reachEstimateMax = "&reachEstimateMax=" + mentionFilterVM.MaxReach.ToString();
                        uRLPreParer.CompleteParametersForAPI += uRLPreParer.reachEstimateMax;
                        uRLPreParer.CompleteParametersForTweeterAuthorAPI += uRLPreParer.reachEstimateMax;

                    }

                    // Impact
                    allowedMinAndMaxAndMultiOptions.MinImpact = null;
                    if (mentionFilterVM.MinImpact > 0)
                    {
                        allowedMinAndMaxAndMultiOptions.MinImpact = mentionFilterVM.MinImpact;
                        uRLPreParer.impactMin = "&impactMin=" + mentionFilterVM.MinImpact.ToString();
                        uRLPreParer.CompleteParametersForAPI += uRLPreParer.impactMin;
                        uRLPreParer.CompleteParametersForTweeterAuthorAPI += uRLPreParer.impactMin;
                    }
                    allowedMinAndMaxAndMultiOptions.MaxImpact = null;
                    if (mentionFilterVM.MaxImpact > 0)
                    {
                        allowedMinAndMaxAndMultiOptions.MaxImpact = mentionFilterVM.MaxImpact;

                        uRLPreParer.impactMax = "&impactMax=" + mentionFilterVM.MaxImpact.ToString();
                        uRLPreParer.CompleteParametersForAPI += uRLPreParer.impactMax;
                        uRLPreParer.CompleteParametersForTweeterAuthorAPI += uRLPreParer.impactMax;
                    }
                    //Twitter Section
                    allowedMinAndMaxAndMultiOptions.TwitterMinRepost = null;
                    if (mentionFilterVM.TwitterMinReposts > 0)
                    {
                        allowedMinAndMaxAndMultiOptions.TwitterMinRepost = mentionFilterVM.TwitterMinReposts;
                        uRLPreParer.twitterRetweetsMin = "&twitterRetweetsMin=" + mentionFilterVM.TwitterMinReposts.ToString();
                        uRLPreParer.CompleteParametersForAPI += uRLPreParer.twitterRetweetsMin;
                        uRLPreParer.CompleteParametersForTweeterAuthorAPI += uRLPreParer.twitterRetweetsMin;
                    }
                    allowedMinAndMaxAndMultiOptions.TwitterMaxRepost = null;
                    if (mentionFilterVM.TwitterMaxReposts > 0)
                    {
                        allowedMinAndMaxAndMultiOptions.TwitterMaxRepost = mentionFilterVM.TwitterMaxReposts;
                        uRLPreParer.twitterRetweetsMax = "&twitterRetweetsMax=" + mentionFilterVM.TwitterMaxReposts.ToString();
                        uRLPreParer.CompleteParametersForAPI += uRLPreParer.twitterRetweetsMax;
                        uRLPreParer.CompleteParametersForTweeterAuthorAPI += uRLPreParer.twitterRetweetsMax;

                    }
                    allowedMinAndMaxAndMultiOptions.TwitterMinLikes = null;
                    if (mentionFilterVM.TwitterMinLikes > 0)
                    {
                        allowedMinAndMaxAndMultiOptions.TwitterMinLikes = mentionFilterVM.TwitterMinLikes;
                        uRLPreParer.twitterLikeCountMin = "&twitterLikeCountMin=" + mentionFilterVM.TwitterMinLikes.ToString();
                        uRLPreParer.CompleteParametersForAPI += uRLPreParer.twitterLikeCountMin;
                        uRLPreParer.CompleteParametersForTweeterAuthorAPI += uRLPreParer.twitterLikeCountMin;

                    }
                    allowedMinAndMaxAndMultiOptions.TwitterMaxLikes = null;
                    if (mentionFilterVM.TwitterMaxLikes > 0)
                    {
                        allowedMinAndMaxAndMultiOptions.TwitterMaxLikes = mentionFilterVM.TwitterMaxLikes;

                        uRLPreParer.twitterLikeCountMax = "&twitterLikeCountMax=" + mentionFilterVM.TwitterMaxLikes.ToString();
                        uRLPreParer.CompleteParametersForAPI += uRLPreParer.twitterLikeCountMax;
                        uRLPreParer.CompleteParametersForTweeterAuthorAPI += uRLPreParer.twitterLikeCountMax;

                    }
                    allowedMinAndMaxAndMultiOptions.TwitterMinTotalPosts = null;
                    if (mentionFilterVM.TwitterMinTotalPosts > 0)
                    {
                        allowedMinAndMaxAndMultiOptions.TwitterMinTotalPosts = mentionFilterVM.TwitterMinTotalPosts;

                        uRLPreParer.twitterPostCountMin = "&twitterPostCountMin=" + mentionFilterVM.TwitterMinTotalPosts.ToString();
                        uRLPreParer.CompleteParametersForAPI += uRLPreParer.twitterPostCountMin;
                        uRLPreParer.CompleteParametersForTweeterAuthorAPI += uRLPreParer.twitterPostCountMin;

                    }
                    allowedMinAndMaxAndMultiOptions.TwitterMaxTotalPosts = null;
                    if (mentionFilterVM.TwitterMaxTotalPosts > 0)
                    {
                        allowedMinAndMaxAndMultiOptions.TwitterMaxTotalPosts = mentionFilterVM.TwitterMaxTotalPosts;

                        uRLPreParer.twitterPostCountMax = "&twitterPostCountMax=" + mentionFilterVM.TwitterMaxTotalPosts.ToString();
                        uRLPreParer.CompleteParametersForAPI += uRLPreParer.twitterPostCountMax;
                        uRLPreParer.CompleteParametersForTweeterAuthorAPI += uRLPreParer.twitterPostCountMax;
                    }
                    allowedMinAndMaxAndMultiOptions.TwitterMinFollowing = null;
                    if (mentionFilterVM.TwitterMinFollowing > 0)
                    {
                        allowedMinAndMaxAndMultiOptions.TwitterMinFollowing = mentionFilterVM.TwitterMinFollowing;
                        uRLPreParer.twitterFollowingMin = "&twitterFollowingMin=" + mentionFilterVM.TwitterMinFollowing.ToString();
                        uRLPreParer.CompleteParametersForAPI += uRLPreParer.twitterFollowingMin;
                        uRLPreParer.CompleteParametersForTweeterAuthorAPI += uRLPreParer.twitterFollowingMin;

                    }
                    allowedMinAndMaxAndMultiOptions.TwitterMaxFollowing = null;
                    if (mentionFilterVM.TwitterMaxFollowing > 0)
                    {
                        allowedMinAndMaxAndMultiOptions.TwitterMaxFollowing = mentionFilterVM.TwitterMaxFollowing;

                        uRLPreParer.twitterFollowingMax = "&twitterFollowingMax=" + mentionFilterVM.TwitterMaxFollowing.ToString();
                        uRLPreParer.CompleteParametersForAPI += uRLPreParer.twitterFollowingMax;
                        uRLPreParer.CompleteParametersForTweeterAuthorAPI += uRLPreParer.twitterFollowingMax;


                    }
                    allowedMinAndMaxAndMultiOptions.TwitterMinFollowers = null;
                    if (mentionFilterVM.TwitterMinFollowers > 0)
                    {
                        allowedMinAndMaxAndMultiOptions.TwitterMinFollowers = mentionFilterVM.TwitterMinFollowers;

                        uRLPreParer.twitterFollowersMin = "&twitterFollowersMin=" + mentionFilterVM.TwitterMinFollowers.ToString();
                        uRLPreParer.CompleteParametersForAPI += uRLPreParer.twitterFollowersMin;
                        uRLPreParer.CompleteParametersForTweeterAuthorAPI += uRLPreParer.twitterFollowersMin;


                    }
                    allowedMinAndMaxAndMultiOptions.TwitterMaxFollowers = null;
                    if (mentionFilterVM.TwitterMaxFollowers > 0)
                    {
                        allowedMinAndMaxAndMultiOptions.TwitterMaxFollowers = mentionFilterVM.TwitterMaxFollowers;

                        uRLPreParer.twitterFollowersMax = "&twitterFollowersMax=" + mentionFilterVM.TwitterMaxFollowers.ToString();
                        uRLPreParer.CompleteParametersForAPI += uRLPreParer.twitterFollowersMax;
                        uRLPreParer.CompleteParametersForTweeterAuthorAPI += uRLPreParer.twitterFollowersMax;
                    }
                    allowedMinAndMaxAndMultiOptions.TwitterMinImpressions = null;
                    if (mentionFilterVM.TwitterMinImpressions > 0)
                    {
                        allowedMinAndMaxAndMultiOptions.TwitterMinImpressions = mentionFilterVM.TwitterMinImpressions;
                        uRLPreParer.impressionsMin = "&impressionsMin=" + mentionFilterVM.TwitterMinImpressions.ToString();
                        uRLPreParer.CompleteParametersForAPI += uRLPreParer.impressionsMin;
                        uRLPreParer.CompleteParametersForTweeterAuthorAPI += uRLPreParer.impressionsMin;
                    }
                    allowedMinAndMaxAndMultiOptions.TwitterMaxImpressions = null;
                    if (mentionFilterVM.TwitterMaxImpressions > 0)
                    {
                        allowedMinAndMaxAndMultiOptions.TwitterMaxImpressions = mentionFilterVM.TwitterMaxImpressions;

                        uRLPreParer.impressionsMax = "&impressionsMax=" + mentionFilterVM.TwitterMaxImpressions.ToString();
                        uRLPreParer.CompleteParametersForAPI += uRLPreParer.impressionsMax;
                        uRLPreParer.CompleteParametersForTweeterAuthorAPI += uRLPreParer.impressionsMax;
                    }
                    allowedMinAndMaxAndMultiOptions.TwitterUserRole = null;
                    if (mentionFilterVM.SelectedTwitterUserRoleId > 0)
                    {
                        allowedMinAndMaxAndMultiOptions.TwitterUserRole = mentionFilterVM.SelectedTwitterUserRoleId;
                        TwitterUserRole twitterUserRole = db.TwitterUserRoles.Find(mentionFilterVM.SelectedTwitterUserRoleId);
                        uRLPreParer.twitterRole = "&twitterRole=" + twitterUserRole.RoleName.ToLower();
                        uRLPreParer.CompleteParametersForAPI += uRLPreParer.twitterRole;
                        uRLPreParer.CompleteParametersForTweeterAuthorAPI += uRLPreParer.twitterRole;
                    }
                    allowedMinAndMaxAndMultiOptions.TwitterVerification = null;
                    if (mentionFilterVM.SelectedTwitterVerificationId > 0)
                    {
                        allowedMinAndMaxAndMultiOptions.TwitterVerification = mentionFilterVM.SelectedTwitterVerificationId;
                        TwitterVerification twitterVerification = db.TwitterVerifications.Find(mentionFilterVM.SelectedTwitterVerificationId);
                        if (twitterVerification.Verification == "Verified")
                        {
                            uRLPreParer.twitterVerified = "&twitterVerified=true";
                            uRLPreParer.CompleteParametersForAPI += uRLPreParer.twitterVerified;
                            uRLPreParer.CompleteParametersForTweeterAuthorAPI += uRLPreParer.twitterVerified;
                        }
                        

                    }


                    if (mentionFilter.IncludedTwitterVerificationTypes.Any())
                    {
                        uRLPreParer.authorVerifiedType = "&authorVerifiedType=";

                        foreach (var _authorVerifiedType in mentionFilter.IncludedTwitterVerificationTypes)
                        {
                            uRLPreParer.authorVerifiedType += _authorVerifiedType.TwitterVerificationTypeName.ToUpper() + ",";
                        }
                        uRLPreParer.authorVerifiedType = uRLPreParer.authorVerifiedType.Remove(uRLPreParer.authorVerifiedType.Length - 1);
                        uRLPreParer.CompleteParametersForAPI += uRLPreParer.authorVerifiedType;
                        uRLPreParer.CompleteParametersForTweeterAuthorAPI += uRLPreParer.authorVerifiedType;
                    }

                    if (mentionFilter.ExcludedTwitterVerificationTypes.Any())
                    {
                        uRLPreParer.xauthorVerifiedType = "&xauthorVerifiedType=";

                        foreach (var _authorVerifiedType in mentionFilter.ExcludedTwitterVerificationTypes)
                        {
                            uRLPreParer.xauthorVerifiedType += _authorVerifiedType.TwitterVerificationTypeName.ToUpper() + ",";
                        }
                        uRLPreParer.xauthorVerifiedType = uRLPreParer.xauthorVerifiedType.Remove(uRLPreParer.xauthorVerifiedType.Length - 1);
                        uRLPreParer.CompleteParametersForAPI += uRLPreParer.xauthorVerifiedType;
                        uRLPreParer.CompleteParametersForTweeterAuthorAPI += uRLPreParer.xauthorVerifiedType;
                    }

                    //if (mentionFilter.IncludedTwitterMentionTypes.Any())
                    //{
                    //    uRLPreParer.twitterEngagementType = "&twitterEngagementType=";

                    //    foreach (var _twitterEngagementType in mentionFilter.IncludedTwitterMentionTypes)
                    //    {
                    //        uRLPreParer.twitterEngagementType += _twitterEngagementType.TwitterMentionTypeName.ToUpper() + ",";
                    //    }
                    //    uRLPreParer.twitterEngagementType = uRLPreParer.twitterEngagementType.Remove(uRLPreParer.twitterEngagementType.Length - 1);
                    //    uRLPreParer.CompleteParametersForAPI += uRLPreParer.twitterEngagementType;
                    //}
                    //if (mentionFilter.ExcludedTwitterMentionTypes.Any())
                    //{
                    //    uRLPreParer.xtwitterEngagementType = "&xtwitterEngagementType=";

                    //    foreach (var _twitterEngagementType in mentionFilter.ExcludedTwitterMentionTypes)
                    //    {
                    //        uRLPreParer.xtwitterEngagementType += _twitterEngagementType.TwitterMentionTypeName.ToUpper() + ",";
                    //    }
                    //    uRLPreParer.xtwitterEngagementType = uRLPreParer.xtwitterEngagementType.Remove(uRLPreParer.xtwitterEngagementType.Length - 1);
                    //    uRLPreParer.CompleteParametersForAPI += uRLPreParer.xtwitterEngagementType;
                    //}
                   
                    if (mentionFilter.IncludedTwitterAccountTypes.Any())
                    {
                        uRLPreParer.accountType = "&accountType=";

                        foreach (var _accountType in mentionFilter.IncludedTwitterAccountTypes)
                        {
                            uRLPreParer.accountType += _accountType.TwitterAccountTypeName.ToLower() + ",";
                        }
                        uRLPreParer.accountType = uRLPreParer.accountType.Remove(uRLPreParer.accountType.Length - 1);
                        uRLPreParer.CompleteParametersForAPI += uRLPreParer.accountType;
                        uRLPreParer.CompleteParametersForTweeterAuthorAPI += uRLPreParer.accountType;
                    }

                    if (mentionFilter.ExcludedTwitterAccountTypes.Any())
                    {
                        uRLPreParer.xaccountType = "&xaccountType=";

                        foreach (var _accountType in mentionFilter.ExcludedTwitterAccountTypes)
                        {
                            uRLPreParer.xaccountType += _accountType.TwitterAccountTypeName.ToLower() + ",";
                        }
                        uRLPreParer.xaccountType = uRLPreParer.xaccountType.Remove(uRLPreParer.xaccountType.Length - 1);
                        uRLPreParer.CompleteParametersForAPI += uRLPreParer.xaccountType;
                        uRLPreParer.CompleteParametersForTweeterAuthorAPI += uRLPreParer.xaccountType;
                    }

                    if (mentionFilter.IncludedTwitterGenders.Any())
                    {
                        uRLPreParer.gender = "&gender=";

                        foreach (var _gender in mentionFilter.IncludedTwitterGenders)
                        {
                            uRLPreParer.gender += _gender.GenderName.ToLower() + ",";
                        }
                        uRLPreParer.gender = uRLPreParer.gender.Remove(uRLPreParer.gender.Length - 1);
                        uRLPreParer.CompleteParametersForAPI += uRLPreParer.gender;
                        uRLPreParer.CompleteParametersForTweeterAuthorAPI += uRLPreParer.gender;
                    }

                    if (mentionFilter.IncludedTwitterProfessions.Any())
                    {
                        uRLPreParer.profession = "&profession=";

                        foreach (var _profession in mentionFilter.IncludedTwitterProfessions)
                        {
                            uRLPreParer.profession += _profession.ProfessionName + ",";
                        }
                        uRLPreParer.profession = uRLPreParer.profession.Remove(uRLPreParer.profession.Length - 1);
                        uRLPreParer.CompleteParametersForAPI += uRLPreParer.profession;
                        uRLPreParer.CompleteParametersForTweeterAuthorAPI += uRLPreParer.profession;
                    }
                    if (mentionFilter.ExcludedTwitterProfessions.Any())
                    {
                        uRLPreParer.xprofession = "&xprofession=";

                        foreach (var _profession in mentionFilter.ExcludedTwitterProfessions)
                        {
                            uRLPreParer.xprofession += _profession.ProfessionName+ ",";
                        }
                        uRLPreParer.xprofession = uRLPreParer.xprofession.Remove(uRLPreParer.xprofession.Length - 1);
                        uRLPreParer.CompleteParametersForAPI += uRLPreParer.xprofession;
                        uRLPreParer.CompleteParametersForTweeterAuthorAPI += uRLPreParer.xprofession;
                    }

                    if (mentionFilter.IncludedTwitterInterests.Any())
                    {
                        uRLPreParer.interest = "&interest=";

                        foreach (var _interest in mentionFilter.IncludedTwitterInterests)
                        {
                            uRLPreParer.interest += _interest.InterestName + ",";
                        }
                        uRLPreParer.interest = uRLPreParer.interest.Remove(uRLPreParer.interest.Length - 1);
                        uRLPreParer.CompleteParametersForAPI += uRLPreParer.interest;
                        uRLPreParer.CompleteParametersForTweeterAuthorAPI += uRLPreParer.interest;
                    }

                    if (mentionFilter.ExcludedTwitterInterests.Any())
                    {
                        uRLPreParer.xinterest = "&xinterest=";

                        foreach (var _interest in mentionFilter.ExcludedTwitterInterests)
                        {
                            uRLPreParer.xinterest += _interest.InterestName + ",";
                        }
                        uRLPreParer.xinterest = uRLPreParer.xinterest.Remove(uRLPreParer.xinterest.Length - 1);
                        uRLPreParer.CompleteParametersForAPI += uRLPreParer.xinterest;
                        uRLPreParer.CompleteParametersForTweeterAuthorAPI += uRLPreParer.xinterest;
                    }
                    
                    //FaceBook section
                    uRLPreParer.resourceType = "&resourceType=";
                    uRLPreParer.xresourceType = "&xresourceType=";
                    allowedMinAndMaxAndMultiOptions.FaceBookUserRole = null;
                    if (mentionFilterVM.SelectedFaceBookUserRoleId > 0)
                    {
                        allowedMinAndMaxAndMultiOptions.FaceBookUserRole = mentionFilterVM.SelectedFaceBookUserRoleId;
                        FaceBookUserRole faceBookUserRole = db.FaceBookUserRoles.Find(mentionFilterVM.SelectedFaceBookUserRoleId);
                        uRLPreParer.facebookRole = "&facebookRole=" + faceBookUserRole.UserRoleName.ToLower();
                        uRLPreParer.CompleteParametersForAPI += uRLPreParer.facebookRole;
                    }

                    if (mentionFilter.IncludedFaceBookMentionTypes.Any())
                    {

                        foreach (var _faceBookMentionType in mentionFilter.IncludedFaceBookMentionTypes)
                        {
                            uRLPreParer.resourceType += _faceBookMentionType.FaceBookMentionTypeName + ",";
                        }
                        uRLPreParer.resourceType = uRLPreParer.resourceType.Remove(uRLPreParer.resourceType.Length - 1);
                        uRLPreParer.CompleteParametersForAPI += uRLPreParer.resourceType;
                    }

                    if (mentionFilter.ExcludedFaceBookMentionTypes.Any())
                    {
                        

                        foreach (var _faceBookMentionType in mentionFilter.ExcludedFaceBookMentionTypes)
                        {
                            uRLPreParer.xresourceType += _faceBookMentionType.FaceBookMentionTypeName + ",";
                        }
                        uRLPreParer.xresourceType = uRLPreParer.xresourceType.Remove(uRLPreParer.xresourceType.Length - 1);
                        uRLPreParer.CompleteParametersForAPI += uRLPreParer.xresourceType;
                    }
                    allowedMinAndMaxAndMultiOptions.FaceBookMinLikes = null;
                    if (mentionFilterVM.FaceBookMinLikes > 0)
                    {
                        allowedMinAndMaxAndMultiOptions.FaceBookMinLikes = mentionFilterVM.FaceBookMinLikes;
                        uRLPreParer.facebookLikesMin = "&facebookLikesMin=" + mentionFilterVM.FaceBookMinLikes.ToString();
                        uRLPreParer.CompleteParametersForAPI += uRLPreParer.facebookLikesMin;

                    }
                    allowedMinAndMaxAndMultiOptions.FaceBookMaxLikes = null;
                    if (mentionFilterVM.FaceBookMaxLikes > 0)
                    {
                        allowedMinAndMaxAndMultiOptions.FaceBookMaxLikes = mentionFilterVM.FaceBookMaxLikes;

                        uRLPreParer.facebookLikesMax = "&facebookLikesMax=" + mentionFilterVM.FaceBookMaxLikes.ToString();
                        uRLPreParer.CompleteParametersForAPI += uRLPreParer.facebookLikesMax;

                    }
                    allowedMinAndMaxAndMultiOptions.FaceBookMinComments = null;
                    if (mentionFilterVM.FaceBookMinComments > 0)
                    {
                        allowedMinAndMaxAndMultiOptions.FaceBookMinComments = mentionFilterVM.FaceBookMinComments;
                        uRLPreParer.facebookCommentsMin = "&facebookCommentsMin=" + mentionFilterVM.FaceBookMinComments.ToString();
                        uRLPreParer.CompleteParametersForAPI += uRLPreParer.facebookCommentsMin;

                    }
                    allowedMinAndMaxAndMultiOptions.FaceBookMaxComments = null;
                    if (mentionFilterVM.FaceBookMaxComments > 0)
                    {
                        allowedMinAndMaxAndMultiOptions.FaceBookMaxComments = mentionFilterVM.FaceBookMaxComments;

                        uRLPreParer.facebookCommentsMax = "&facebookCommentsMax=" + mentionFilterVM.FaceBookMaxComments.ToString();
                        uRLPreParer.CompleteParametersForAPI += uRLPreParer.facebookCommentsMax;

                    }
                    allowedMinAndMaxAndMultiOptions.FaceBookMinShares = null;
                    if (mentionFilterVM.FaceBookMinShares > 0)
                    {
                        allowedMinAndMaxAndMultiOptions.FaceBookMinShares = mentionFilterVM.FaceBookMinShares;
                        uRLPreParer.facebookSharesMin = "&facebookSharesMin=" + mentionFilterVM.FaceBookMinShares.ToString();
                        uRLPreParer.CompleteParametersForAPI += uRLPreParer.facebookSharesMin;
                    }
                    allowedMinAndMaxAndMultiOptions.FaceBookMaxShares = null;
                    if (mentionFilterVM.FaceBookMaxShares > 0)
                    {
                        allowedMinAndMaxAndMultiOptions.FaceBookMaxShares = mentionFilterVM.FaceBookMaxShares;

                        uRLPreParer.facebookSharesMax = "&facebookSharesMax=" + mentionFilterVM.FaceBookMaxShares.ToString();
                        uRLPreParer.CompleteParametersForAPI += uRLPreParer.facebookSharesMax;

                    }
                    //Instagram Section

                    if (mentionFilter.IncludedInstagramMentionTypes.Any())
                    {                      

                        foreach (var _instagramMentionType in mentionFilter.IncludedInstagramMentionTypes)
                        {
                            uRLPreParer.resourceType += _instagramMentionType.InstagramMentionTypeName + ",";
                        }
                        uRLPreParer.resourceType = uRLPreParer.resourceType.Remove(uRLPreParer.resourceType.Length - 1);
                        uRLPreParer.CompleteParametersForAPI += uRLPreParer.resourceType;
                    }
                    if (mentionFilter.ExcludedInstagramMentionTypes.Any())
                    {

                        foreach (var _instagramMentionType in mentionFilter.ExcludedInstagramMentionTypes)
                        {
                            uRLPreParer.xresourceType += _instagramMentionType.InstagramMentionTypeName + ",";
                        }
                        uRLPreParer.xresourceType = uRLPreParer.xresourceType.Remove(uRLPreParer.xresourceType.Length - 1);
                        uRLPreParer.CompleteParametersForAPI += uRLPreParer.xresourceType;
                    }
                    allowedMinAndMaxAndMultiOptions.InstgramMinInteractions = null;
                    if (mentionFilterVM.InstgramMinInteractions > 0)
                    {
                        allowedMinAndMaxAndMultiOptions.InstgramMinInteractions = mentionFilterVM.InstgramMinInteractions;
                        uRLPreParer.instagramInteractionsMin = "&instagramInteractionsMin=" + mentionFilterVM.InstgramMinInteractions.ToString();
                        uRLPreParer.CompleteParametersForAPI += uRLPreParer.instagramInteractionsMin;
                    }
                    allowedMinAndMaxAndMultiOptions.InstgramMaxInteractions = null;
                    if (mentionFilterVM.InstgramMaxInteractions > 0)
                    {
                        allowedMinAndMaxAndMultiOptions.InstgramMaxInteractions = mentionFilterVM.InstgramMaxInteractions;

                        uRLPreParer.instagramInteractionsMax = "&instagramInteractionsMax=" + mentionFilterVM.InstgramMaxInteractions.ToString();
                        uRLPreParer.CompleteParametersForAPI += uRLPreParer.instagramInteractionsMax;
                    }
                    allowedMinAndMaxAndMultiOptions.InstgramMinLikes = null;
                    if (mentionFilterVM.InstgramMinLikes > 0)
                    {
                        allowedMinAndMaxAndMultiOptions.InstgramMinLikes = mentionFilterVM.InstgramMinLikes;
                        uRLPreParer.instagramLikesMin = "&instagramLikesMin=" + mentionFilterVM.InstgramMinLikes.ToString();
                        uRLPreParer.CompleteParametersForAPI += uRLPreParer.instagramLikesMin;
                    }
                    allowedMinAndMaxAndMultiOptions.InstgramMaxLikes = null;
                    if (mentionFilterVM.InstgramMaxLikes > 0)
                    {
                        allowedMinAndMaxAndMultiOptions.InstgramMaxLikes = mentionFilterVM.InstgramMaxLikes;

                        uRLPreParer.instagramLikesMax = "&instagramLikesMax=" + mentionFilterVM.InstgramMaxLikes.ToString();
                        uRLPreParer.CompleteParametersForAPI += uRLPreParer.instagramLikesMax;
                    }
                    allowedMinAndMaxAndMultiOptions.InstgramMinComments = null;
                    if (mentionFilterVM.InstgramMinComments > 0)
                    {
                        allowedMinAndMaxAndMultiOptions.InstgramMinComments = mentionFilterVM.InstgramMinComments;
                        uRLPreParer.instagramCommentsMin = "&instagramCommentsMin=" + mentionFilterVM.InstgramMinComments.ToString();
                        uRLPreParer.CompleteParametersForAPI += uRLPreParer.instagramCommentsMin;
                    }
                    allowedMinAndMaxAndMultiOptions.InstgramMaxComments = null;
                    if (mentionFilterVM.InstgramMaxComments > 0)
                    {
                        allowedMinAndMaxAndMultiOptions.InstgramMaxComments = mentionFilterVM.InstgramMaxComments;

                        uRLPreParer.instagramCommentsMax = "&instagramCommentsMax=" + mentionFilterVM.InstgramMaxComments.ToString();
                        uRLPreParer.CompleteParametersForAPI += uRLPreParer.instagramCommentsMax;
                    }
                    allowedMinAndMaxAndMultiOptions.InstgramMinPosts = null;
                    if (mentionFilterVM.InstgramMinPosts > 0)
                    {
                        allowedMinAndMaxAndMultiOptions.InstgramMinPosts = mentionFilterVM.InstgramMinPosts;
                        uRLPreParer.instagramPostsMin = "&instagramPostsMin=" + mentionFilterVM.InstgramMinPosts.ToString();
                        uRLPreParer.CompleteParametersForAPI += uRLPreParer.instagramPostsMin;
                    }
                    allowedMinAndMaxAndMultiOptions.InstgramMaxPosts = null;
                    if (mentionFilterVM.InstgramMaxPosts > 0)
                    {
                        allowedMinAndMaxAndMultiOptions.InstgramMaxPosts = mentionFilterVM.InstgramMaxPosts;

                        uRLPreParer.instagramPostsMax = "&instagramPostsMax=" + mentionFilterVM.InstgramMaxPosts.ToString();
                        uRLPreParer.CompleteParametersForAPI += uRLPreParer.instagramPostsMax;
                    }
                    allowedMinAndMaxAndMultiOptions.InstgramMinFollowers = null;
                    if (mentionFilterVM.InstgramMinFollowers > 0)
                    {
                        allowedMinAndMaxAndMultiOptions.InstgramMinFollowers = mentionFilterVM.InstgramMinFollowers;
                        uRLPreParer.instagramFollowersMin = "&instagramFollowersMin=" + mentionFilterVM.InstgramMinFollowers.ToString();
                        uRLPreParer.CompleteParametersForAPI += uRLPreParer.instagramFollowersMin;
                    }
                    allowedMinAndMaxAndMultiOptions.InstgramMaxFollowers = null;
                    if (mentionFilterVM.InstgramMaxFollowers > 0)
                    {
                        allowedMinAndMaxAndMultiOptions.InstgramMaxFollowers = mentionFilterVM.InstgramMaxFollowers;

                        uRLPreParer.instagramFollowersMax = "&instagramFollowersMax=" + mentionFilterVM.InstgramMaxFollowers.ToString();
                        uRLPreParer.CompleteParametersForAPI += uRLPreParer.instagramFollowersMax;
                    }
                    allowedMinAndMaxAndMultiOptions.InstgramMinFollowing = null;
                    if (mentionFilterVM.InstgramMinFollowing > 0)
                    {
                        allowedMinAndMaxAndMultiOptions.InstgramMinFollowing = mentionFilterVM.InstgramMinFollowing;
                        uRLPreParer.instagramFollowingMin = "&instagramFollowingMin=" + mentionFilterVM.InstgramMinFollowing.ToString();
                        uRLPreParer.CompleteParametersForAPI += uRLPreParer.instagramFollowingMin;
                    }
                    allowedMinAndMaxAndMultiOptions.InstgramMaxFollowing = null;
                    if (mentionFilterVM.InstgramMaxFollowing > 0)
                    {
                        allowedMinAndMaxAndMultiOptions.InstgramMaxFollowing = mentionFilterVM.InstgramMaxFollowing;

                        uRLPreParer.instagramFollowingMax = "&instagramFollowingMax=" + mentionFilterVM.InstgramMaxFollowing.ToString();
                        uRLPreParer.CompleteParametersForAPI += uRLPreParer.instagramFollowingMax;
                    }

                    // Bluesky Section
                    //if (mentionFilterVM.BlueskyMinLikes > 0)
                    //{
                    //    uRLPreParer.blueskyLikesMin = "&blueskyLikesMin=" + mentionFilterVM.BlueskyMinLikes.ToString();
                    //    uRLPreParer.CompleteParametersForAPI += uRLPreParer.blueskyLikesMin;
                    //}
                    //if (mentionFilterVM.BlueskyMaxLikes > 0)
                    //{
                    //    uRLPreParer.blueskyLikesMax = "&blueskyLikesMax=" + mentionFilterVM.BlueskyMaxLikes.ToString();
                    //    uRLPreParer.CompleteParametersForAPI += uRLPreParer.blueskyLikesMax;
                    //}

                    //if (mentionFilterVM.BlueskyMinReplies > 0)
                    //{
                    //    uRLPreParer.blueskyLikesMin = "&blueskyLikesMin=" + mentionFilterVM.BlueskyMinLikes.ToString();
                    //    uRLPreParer.CompleteParametersForAPI += uRLPreParer.blueskyLikesMin;
                    //}

                    // reddit Section
                    allowedMinAndMaxAndMultiOptions.RedditMinScores = null;
                    if (mentionFilterVM.RedditMinScore > 0)
                    {
                        allowedMinAndMaxAndMultiOptions.RedditMinScores = mentionFilterVM.RedditMinScore;
                        uRLPreParer.redditScoreMin = "&redditScoreMin=" + mentionFilterVM.RedditMinScore.ToString();
                        uRLPreParer.CompleteParametersForAPI += uRLPreParer.redditScoreMin;
                    }
                    allowedMinAndMaxAndMultiOptions.RedditMaxScores = null;
                    if (mentionFilterVM.RedditMaxScore > 0)
                    {
                        allowedMinAndMaxAndMultiOptions.RedditMaxScores = mentionFilterVM.RedditMaxScore;

                        uRLPreParer.redditScoreMax = "&redditScoreMin=" + mentionFilterVM.RedditMaxScore.ToString();
                        uRLPreParer.CompleteParametersForAPI += uRLPreParer.redditScoreMax;
                    }
                    allowedMinAndMaxAndMultiOptions.RedditMinAuthorKarma = null;
                    if (mentionFilterVM.RedditMinAuthorKarma > 0)
                    {
                        allowedMinAndMaxAndMultiOptions.RedditMinAuthorKarma = mentionFilterVM.RedditMinAuthorKarma;
                        uRLPreParer.redditAuthorKarmaMin = "&redditAuthorKarmaMin=" + mentionFilterVM.RedditMinAuthorKarma.ToString();
                        uRLPreParer.CompleteParametersForAPI += uRLPreParer.redditAuthorKarmaMin;
                    }
                    allowedMinAndMaxAndMultiOptions.RedditMaxAuthorKarma = null;
                    if (mentionFilterVM.RedditMaxAuthorKarma > 0)
                    {
                        allowedMinAndMaxAndMultiOptions.RedditMaxAuthorKarma = mentionFilterVM.RedditMaxAuthorKarma;

                        uRLPreParer.redditAuthorKarmaMax = "&redditAuthorKarmaMax=" + mentionFilterVM.RedditMaxAuthorKarma.ToString();
                        uRLPreParer.CompleteParametersForAPI += uRLPreParer.redditAuthorKarmaMax;
                    }
                    allowedMinAndMaxAndMultiOptions.RedditMinSubscribers = null;
                    if (mentionFilterVM.RedditMinSubscribers > 0)
                    {
                        allowedMinAndMaxAndMultiOptions.RedditMinSubscribers = mentionFilterVM.RedditMinSubscribers;
                        uRLPreParer.subredditSubscribersMin = "&subredditSubscribersMin=" + mentionFilterVM.RedditMinSubscribers.ToString();
                        uRLPreParer.CompleteParametersForAPI += uRLPreParer.subredditSubscribersMin;
                    }
                    allowedMinAndMaxAndMultiOptions.RedditMaxSubscribers = null;
                    if (mentionFilterVM.RedditMaxSubscribers > 0)
                    {
                        allowedMinAndMaxAndMultiOptions.RedditMaxSubscribers = mentionFilterVM.RedditMaxSubscribers;

                        uRLPreParer.subredditSubscribersMax = "&subredditSubscribersMax=" + mentionFilterVM.RedditMaxSubscribers.ToString();
                        uRLPreParer.CompleteParametersForAPI += uRLPreParer.subredditSubscribersMax;
                    }

                    // SubType
                    if (mentionFilter.IncludedSubTypes.Any())
                    {
                        uRLPreParer.subtype = "&subtype=";

                        foreach (var _subtype in mentionFilter.IncludedSubTypes)
                        {
                            uRLPreParer.subtype += _subtype.SourceTypeName.ToLower() + ",";
                        }
                        uRLPreParer.subtype = uRLPreParer.subtype.Remove(uRLPreParer.subtype.Length - 1);
                        uRLPreParer.CompleteParametersForAPI += uRLPreParer.subtype;
                    }
                    if (mentionFilter.ExcludedSubTypes.Any())
                    {
                        uRLPreParer.xsubtype = "&xsubtype=";

                        foreach (var _subtype in mentionFilter.ExcludedSubTypes)
                        {
                            uRLPreParer.xsubtype += _subtype.SourceTypeName.ToLower() + ",";
                        }
                        uRLPreParer.xsubtype = uRLPreParer.xsubtype.Remove(uRLPreParer.xsubtype.Length - 1);
                        uRLPreParer.CompleteParametersForAPI += uRLPreParer.xsubtype;
                    }
                    // Source Type
                    allowedMinAndMaxAndMultiOptions.SourceType = null;
                    if (mentionFilterVM.SelectedSourceTypetId > 0)
                    {
                        
                        SourceType _sourceType = db.SourceTypes.Find(mentionFilterVM.SelectedSourceTypetId);
                        uRLPreParer.sourceType = "&sourceType=" + _sourceType.BWSourceType;                        
                        uRLPreParer.CompleteParametersForAPI += uRLPreParer.sourceType;
                        allowedMinAndMaxAndMultiOptions.SourceType = mentionFilterVM.SelectedSourceTypetId;
                    }
                    if (mentionFilterHasAllowedMinAndMax)
                    {
                        db.Entry(mentionFilter).State = EntityState.Modified;
                        db.SaveChanges();
                    }
                    else
                    {
                        mentionFilter.AllowedMinAndMaxAndMultiOptionsMentionFilters.Add(allowedMinAndMaxAndMultiOptions);
                        db.Entry(mentionFilter).State = EntityState.Modified;
                        db.SaveChanges();
                    }

                    //uRLPreParer.CompleteParametersForAPI += pageSize;
                    return uRLPreParer;
                }

            }
            catch (Exception ex)
            {

                throw ex;
            }
            return null;
        }
        public static MentionFilterVM PrepareMentionFilterToImportMentions()
        {
            var db = new DBModelBrandWatchContainer();

            var elaqatClient = db.ElaqatClients.Where(p => p.IsActive == true).FirstOrDefault();
            if (elaqatClient != null)
            {
                if (elaqatClient.ClientProjects.Where(p => p.IsActive == true).Any())
                {
                    MentionFilterVM mentionFilterVM = new MentionFilterVM();
                    var ClientProject = elaqatClient.ClientProjects.Where(p => p.IsActive == true).FirstOrDefault();
                    if (ClientProject != null)
                    {
                        mentionFilterVM.SelectedClientProjectId = ClientProject.Id;
                        MentionFilter mentionFilter = new MentionFilter
                        {
                            DefaultFilter = false,
                            ClientProjectID = ClientProject.Id
                        };
                        db.MentionFilters.Add(mentionFilter);
                        db.SaveChanges();
                        mentionFilterVM.MentionFilterID = mentionFilter.Id;

                        mentionFilterVM.ClientProjectGlobalMetricsList = new SelectList(ClientProject.GlobalMetrics.Where(p => p.IsActive == true), "Id", "FilterName");
                        mentionFilterVM.ClientProjectList = new SelectList(elaqatClient.ClientProjects.Where(p => p.IsActive == true), "Id", "ProjectName");
                        mentionFilterVM.QueryList = new SelectList(ClientProject.ClientProjectQueries.Where(p => p.IsActive == true), "Id", "QuerieName");

                        mentionFilterVM.ClientProjectSiteList = new SelectList(ClientProject.ClientProjectSites.Where(p => p.IsActive == true), "Id", "SiteName");

                        mentionFilterVM.CategoriesList = new SelectList(ClientProject.ClientProjectCategories.Where(p => p.IsActive == true), "Id", "CategoryName");
                        mentionFilterVM.SubCategoriesList = new SelectList(db.ClientProjectSubCategories.Where(e => e.ClientProjectCategory.ClientProjectId == ClientProject.Id && e.IsActive == true), "Id", "SubCategoryName");
                        mentionFilterVM.AuthorGroupsList = new SelectList(ClientProject.AuthorGroups.Where(p => p.IsActive == true), "Id", "GroupName");
                        mentionFilterVM.ClientProjectLocationList = new SelectList(ClientProject.ProjectLocations.Where(p => p.IsActive == true), "Id", "LocationName");
                        mentionFilterVM.RuleTagsList = new SelectList(ClientProject.ProjectRuleTags.Where(p => p.IsActive == true), "Id", "BrandWatchRuleTagName");
                        mentionFilterVM.ContentSourcesList = new SelectList(db.QueryContentSources.Where(e => e.ClientProjectQuerie.ClientProjectId == ClientProject.Id && e.ClientProjectQuerie.IsActive == true), "Id", "SourceName");
                        mentionFilterVM.QueryLanguageList = new SelectList(db.QueryLanguages.Where(e => e.ClientProjectQuerie.ClientProjectId == ClientProject.Id && e.ClientProjectQuerie.IsActive == true), "Id", "Language");

                        if (ClientProject.ClientProjectQueries.Where(p => p.IsActive == true).Any())
                        {
                            mentionFilterVM.SelectedQueryId = ClientProject.ClientProjectQueries.Where(p => p.IsActive == true).FirstOrDefault().Id;
                        }


                        mentionFilterVM.TwitterAccountTypesList = new SelectList(db.AccountTypes.Where(e => e.GlobalMetric.ClientProjectId == ClientProject.Id && e.Active == true), "Id", "BWAccountType");
                        mentionFilterVM.TwitterGendersList = new SelectList(db.Genders.Where(e => e.GlobalMetric.ClientProjectId == ClientProject.Id && e.Active == true), "Id", "BWGender");
                        mentionFilterVM.TwitterProfessionsList = new SelectList(db.TwitterProfessions.Where(e => e.GlobalMetric.ClientProjectId == ClientProject.Id && e.Active == true), "Id", "Profession");
                        mentionFilterVM.TwitterInterestsList = new SelectList(db.TwitterInterests.Where(e => e.GlobalMetric.ClientProjectId == ClientProject.Id && e.Active == true), "Id", "Interest");

                        mentionFilterVM.ReactScoreOverallList = new SelectList(db.ReactScoreOveralls.Where(e => e.GlobalMetric.ClientProjectId == ClientProject.Id && e.Active == true), "Id", "ScoreType");
                        mentionFilterVM.ReactScoreHarmList = new SelectList(db.ReactScoreHarms.Where(e => e.GlobalMetric.ClientProjectId == ClientProject.Id && e.Active == true), "Id", "ScoreType");
                        mentionFilterVM.ReactScoreEmotionalityList = new SelectList(db.ReactScoreEmotionalities.Where(e => e.GlobalMetric.ClientProjectId == ClientProject.Id && e.Active == true), "Id", "ScoreType");
                        mentionFilterVM.EmotionList = new SelectList(db.Emotions.Where(e => e.GlobalMetric.ClientProjectId == ClientProject.Id && e.Active == true), "Id", "EmotionType");
                        mentionFilterVM.SentimentsList = new SelectList(db.Sentiments.Where(e => e.GlobalMetric.ClientProjectId == ClientProject.Id && e.Active == true), "Id", "BWSentiment");
                        mentionFilterVM.MentionTypeList = new SelectList(db.MentionTypes.Where(e => e.GlobalMetric.ClientProjectId == ClientProject.Id && e.Active == true), "Id", "TypeOfMention");
                        mentionFilterVM.SourceTypeList = new SelectList(db.SourceTypes.Where(e => e.GlobalMetric.ClientProjectId == ClientProject.Id && e.Active == true), "Id", "BWSourceType");
                        mentionFilterVM.SubTypesList = new SelectList(db.SubTypes.Where(e => e.GlobalMetric.ClientProjectId == ClientProject.Id && e.Active == true), "Id", "BWSubType");
                        mentionFilterVM.SocialPanelList = new SelectList(db.SocialPanels.Where(e => e.GlobalMetric.ClientProjectId == ClientProject.Id && e.Active == true), "Id", "PanelName");
                        mentionFilterVM.AssignmentList = new SelectList(db.Assignments.Where(e => e.GlobalMetric.ClientProjectId == ClientProject.Id && e.Active == true), "Id", "EmployeeUserName");
                        mentionFilterVM.PriorityList = new SelectList(db.Priorities.Where(e => e.GlobalMetric.ClientProjectId == ClientProject.Id && e.Active == true), "Id", "PriorityType");
                        mentionFilterVM.MentionStatusList = new SelectList(db.MentionStatus.Where(e => e.GlobalMetric.ClientProjectId == ClientProject.Id && e.Active == true), "Id", "Status");
                        mentionFilterVM.CheckedMentionList = new SelectList(db.CheckedMentions.Where(e => e.GlobalMetric.ClientProjectId == ClientProject.Id && e.Active == true), "Id", "CheckType");
                        mentionFilterVM.TwitterUserRoleList = new SelectList(db.TwitterUserRoles.Where(e => e.GlobalMetric.ClientProjectId == ClientProject.Id && e.Active == true), "Id", "RoleName");
                        mentionFilterVM.TwitterMentionTypeList = new SelectList(db.TwitterMentionTypes.Where(e => e.GlobalMetric.ClientProjectId == ClientProject.Id && e.Active == true), "Id", "MentionTypeOfTwitter");
                        mentionFilterVM.TwitterVerificationList = new SelectList(db.TwitterVerifications.Where(e => e.GlobalMetric.ClientProjectId == ClientProject.Id && e.Active == true), "Id", "Verification");
                        mentionFilterVM.TwitterVerificationTypeList = new SelectList(db.TwitterVerificationTypes.Where(e => e.GlobalMetric.ClientProjectId == ClientProject.Id && e.Active == true), "Id", "VerificationType");
                        mentionFilterVM.FaceBookUserRoleList = new SelectList(db.FaceBookUserRoles.Where(e => e.GlobalMetric.ClientProjectId == ClientProject.Id && e.Active == true), "Id", "UserRoleName");
                        mentionFilterVM.FaceBookMentionTypeList = new SelectList(db.FaceBookMentionTypes.Where(e => e.GlobalMetric.ClientProjectId == ClientProject.Id && e.Active == true), "Id", "MentionTypeOfFaceBook");
                        mentionFilterVM.InstagramMentionTypeList = new SelectList(db.InstagramMentionTypes.Where(e => e.GlobalMetric.ClientProjectId == ClientProject.Id && e.Active == true), "Id", "MentionTypeOfInstgram");
                        mentionFilterVM.RedditPollList = new SelectList(db.RedditPolls.Where(e => e.GlobalMetric.ClientProjectId == ClientProject.Id && e.Active == true), "Id", "PollChooice");
                        mentionFilterVM.SubredditTopicList = new SelectList(db.SubredditTopics.Where(e => e.GlobalMetric.ClientProjectId == ClientProject.Id && e.Active == true), "Id", "TopicName");
                        mentionFilterVM.RedditNSFWList = new SelectList(db.RedditNSFWs.Where(e => e.GlobalMetric.ClientProjectId == ClientProject.Id && e.Active == true), "Id", "NSFWChooice");


                    }
                    mentionFilterVM.ClientList = new SelectList(db.ElaqatClients.Where(p => p.IsActive == true), "Id", "ClientName");

                    return mentionFilterVM;
                }


            }
            return null;
        }

        public static DashBoardVM PrePareDashBoardVM(List<MentionMapper> mentions, List<Root> RootList)
        {
            try
            {
                DashBoardVM dashBoardVM = new DashBoardVM();
                dashBoardVM.AuthorVMList = new List<AuthorVM>();
                dashBoardVM.DashBoardContentSourceVMist = new List<DashBoardContentSourceVM>();
                dashBoardVM.DomainVMList = new List<DomainVM>();
                dashBoardVM.SubcategpryVMList = new List<ClientProjectSubCategoryVM>();
                dashBoardVM.MentionVMList = new List<MentionVM>();
                dashBoardVM.AuthorMapperList = new List<AuthorMapper>();
                string[] countryList = { "Algeria", "Bahrain", "Palestine", "Comoros", "Djibouti", "Egypt", "Iraq", "Jordan", "Kuwait", "Lebanon", "Libya", "Mauritania", "Morocco", "Oman", "Qatar", "Somalia", "Sudan", "Syria", "Tunisia", "United Arab Emirates", "Yemen" };
                foreach (var _root in RootList)
                {

                    AuthorMapper authorMapper = new AuthorMapper();
                    authorMapper.authorName = _root.Data.AuthorName;
                    authorMapper.domain = _root.Data.Domain;
                    authorMapper.impact = Math.Round(_root.Data.Impact, 0).ToString();
                    authorMapper.reachEstimate = _root.Data.TotalReachEstimate.ToString();
                    authorMapper.twitterFollowers = _root.Data.TwitterFollowers.ToString();
                    authorMapper.twitterAvatar = _root.Data.TwitterAvatar;
                    dashBoardVM.AuthorMapperList.Add(authorMapper);

                }
                string[] colors = { "#1877F2", "#7cb9e7", "#FF0033", "#C13584", "#5900a5", "#8F8888", "#055355", "#2ce375", "#8c950a", "#575155", "#7a7b60", "#097917", "#93e909", "#5e7737","#634141", "#f2f508" };
                foreach (var mention in mentions)
                {
                    if (mention.sentiment == "positive")
                    {
                        dashBoardVM.PositiveSentimentsCount++;
                    }
                    if (mention.sentiment == "neutral")
                    {
                        dashBoardVM.NeutralSentimentsCount++;
                    }

                    if (mention.sentiment == "negative")
                    {
                        dashBoardVM.NegativeSentimentsCount++;
                    }
                  

                    if (!string.IsNullOrEmpty(mention.country) && mention.country.Contains("Saudi Arabia"))
                    {
                        dashBoardVM.LocalMentionCount++;
                    }
                    else
                    {
                        if (!string.IsNullOrEmpty(mention.country) && countryList.Contains(mention.country))
                        {
                            dashBoardVM.RegionalMentionCount++;
                        }
                        else
                        {
                            if (!string.IsNullOrEmpty(mention.country))
                            {
                                dashBoardVM.InternationalMentionCount++;
                            }
                        }
                    }


                    bool constenSourceExist = false;
                    if (dashBoardVM.DashBoardContentSourceVMist.Any())
                    {
                        foreach (var contentSource in dashBoardVM.DashBoardContentSourceVMist)
                        {
                            if (contentSource.ContentSourceName == mention.contentSourceName)
                            {
                                contentSource.CountentSourceTypeCount++;
                                constenSourceExist = true;
                            }

                        }

                        if (!constenSourceExist)
                        {
                            DashBoardContentSourceVM dashBoardContentSourceVM = new DashBoardContentSourceVM();
                            dashBoardContentSourceVM.ContentSourceName = mention.contentSourceName;
                            dashBoardContentSourceVM.CountentSourceTypeCount = 1;
                            dashBoardVM.DashBoardContentSourceVMist.Add(dashBoardContentSourceVM);
                        }
                    }
                    else
                    {
                        DashBoardContentSourceVM dashBoardContentSourceVM = new DashBoardContentSourceVM();
                        dashBoardContentSourceVM.ContentSourceName = mention.contentSourceName;
                        dashBoardContentSourceVM.CountentSourceTypeCount = 1;
                        dashBoardVM.DashBoardContentSourceVMist.Add(dashBoardContentSourceVM);

                    }
                    bool categoryExist = false;
                    if (mention.categoryDetails.Any())
                    {
                        foreach (var category in mention.categoryDetails)
                        {
                            foreach (var _category in dashBoardVM.SubcategpryVMList)
                            {
                                if (category.name == _category.SubCategoryName)
                                {
                                    _category.TotalCategoryCount++;
                                    categoryExist = true;
                                }
                            }
                            if (!categoryExist)
                            {
                                ClientProjectSubCategoryVM clientProjectSubCategoryVM = new ClientProjectSubCategoryVM();
                                clientProjectSubCategoryVM.SubCategoryName = category.name;
                                clientProjectSubCategoryVM.TotalCategoryCount++;
                                dashBoardVM.SubcategpryVMList.Add(clientProjectSubCategoryVM);
                            }

                        }
                    }
                    else
                    {
                        foreach (var category in dashBoardVM.SubcategpryVMList)
                        {
                            if (category.SubCategoryName == "Unknown")
                            {
                                category.TotalCategoryCount++;
                                categoryExist = true;
                            }
                        }
                        if (!categoryExist)
                        {
                            ClientProjectSubCategoryVM clientProjectSubCategoryVM = new ClientProjectSubCategoryVM();
                            clientProjectSubCategoryVM.SubCategoryName = "Unknown";
                            clientProjectSubCategoryVM.TotalCategoryCount++;
                            dashBoardVM.SubcategpryVMList.Add(clientProjectSubCategoryVM);
                        }
                    }

                    bool domainExist = false;
                    if (dashBoardVM.DomainVMList.Any())
                    {
                        foreach (var domain in dashBoardVM.DomainVMList)
                        {
                            if (domain.DomainName == mention.domain)
                            {
                                domain.TotalDomainCount++;
                                domainExist = true;
                            }
                        }
                        if (!domainExist)
                        {
                            DomainVM domainVM = new DomainVM();
                            domainVM.ContentSource = mention.contentSource;
                            domainVM.DomainName = mention.domain;
                            domainVM.TotalDomainCount++;
                            dashBoardVM.DomainVMList.Add(domainVM);
                        }
                    }
                    else
                    {
                        DomainVM domainVM = new DomainVM();
                        domainVM.ContentSource = mention.contentSource;
                        domainVM.DomainName = mention.domain;
                        domainVM.TotalDomainCount++;
                        dashBoardVM.DomainVMList.Add(domainVM);

                    }
                    if (mention.gender == "male")
                    {
                        dashBoardVM.MaleMentionsCount++;
                    }
                    if (mention.gender == "female")
                    {
                        dashBoardVM.FemaleMentionsCount++;
                    }
                    if (mention.gender == "unknown")
                    {
                        dashBoardVM.UnkownMentionsCount++;
                    }

                    dashBoardVM.TotalMentionsCount++;
                    dashBoardVM.TotalImpressionsSum += Convert.ToDecimal(mention.impressions);
                    dashBoardVM.TotalEngagementSum += Convert.ToDecimal(mention.engagementScore);
                    dashBoardVM.TotalReachingsSum += Convert.ToDecimal(mention.reachEstimate);
                }
                double totalPrecentage = 100;
                if (dashBoardVM.MaleMentionsCount > 0 && dashBoardVM.TotalMentionsCount > 0)
                {
                    dashBoardVM.MaleMentionsPrecentage = (dashBoardVM.MaleMentionsCount / dashBoardVM.TotalMentionsCount) * totalPrecentage;
                    dashBoardVM.MaleMentionsPrecentage = Math.Round(dashBoardVM.MaleMentionsPrecentage, 0);
                }

                if (dashBoardVM.FemaleMentionsCount > 0 && dashBoardVM.TotalMentionsCount > 0)
                {
                    dashBoardVM.FemaleMentionsPrecentage = (dashBoardVM.FemaleMentionsCount / dashBoardVM.TotalMentionsCount) * totalPrecentage;
                    dashBoardVM.FemaleMentionsPrecentage = Math.Round(dashBoardVM.FemaleMentionsPrecentage, 0);
                }

                if (dashBoardVM.UnkownMentionsCount > 0 && dashBoardVM.TotalMentionsCount > 0)
                {
                    dashBoardVM.UnknownMentionsPrecentage = (dashBoardVM.UnkownMentionsCount / dashBoardVM.TotalMentionsCount) * totalPrecentage;
                    dashBoardVM.UnknownMentionsPrecentage = Math.Round(dashBoardVM.UnknownMentionsPrecentage, 0);
                }

                foreach (var category in dashBoardVM.SubcategpryVMList)
                {
                    if (category.TotalCategoryCount > 0 && dashBoardVM.TotalMentionsCount > 0)
                    {
                        category.TotalCategoryPrecentage = (category.TotalCategoryCount / dashBoardVM.TotalMentionsCount) * totalPrecentage;
                        category.TotalCategoryPrecentage = Math.Round(category.TotalCategoryPrecentage, 0);
                    }
                }
                dashBoardVM.ContentSourceSeries = new int[dashBoardVM.DashBoardContentSourceVMist.Count];
                dashBoardVM.ContentSourceLabels = new string[dashBoardVM.DashBoardContentSourceVMist.Count];
                dashBoardVM.ContentSourceColors = new string[dashBoardVM.DashBoardContentSourceVMist.Count];
                for (int i = 0; i < dashBoardVM.DashBoardContentSourceVMist.Count; i++)
                {
                    dashBoardVM.ContentSourceSeries[i] = dashBoardVM.DashBoardContentSourceVMist[i].CountentSourceTypeCount;
                    dashBoardVM.ContentSourceLabels[i] = dashBoardVM.DashBoardContentSourceVMist[i].ContentSourceName;
                    dashBoardVM.ContentSourceColors[i] = colors[i];
                }
                return dashBoardVM;

            }
            catch (Exception ex)
            {
                var msg = ex.Message;
                throw;
            }
           
        }
        public static MentionFilterVM PrepareMentionFilterToImportMentionsForProject(int clientProjectID)
        {
            var db = new DBModelBrandWatchContainer();          
           
            var ClientProject = db.ClientProjects.Find(clientProjectID);
            if (ClientProject != null)
            {
                MentionFilterVM mentionFilterVM = new MentionFilterVM();
                mentionFilterVM.ClientList = new SelectList(db.ElaqatClients.Where(p => p.IsActive == true), "Id", "ClientName");
                mentionFilterVM.SelectedClientId = ClientProject.ElaqatClient.Id;
                mentionFilterVM.ClientName = ClientProject.ElaqatClient.ClientName;

                mentionFilterVM.SelectedClientProjectId = ClientProject.Id;
                mentionFilterVM.ProjectName = ClientProject.ProjectName;
                MentionFilter mentionFilter = db.MentionFilters.Where(p => p.ClientProjectID == ClientProject.Id && p.DefaultFilter == true).FirstOrDefault();
                if (mentionFilter == null)
                {
                    mentionFilter = new MentionFilter
                    {
                        DefaultFilter = true,
                        ClientProjectID = ClientProject.Id,
                        MaxStartDateRange = 0,
                        MaxEndDateRange = 0
                    };
                    db.MentionFilters.Add(mentionFilter);
                    db.SaveChanges();
                }
                mentionFilterVM.MaxStartDateRange = mentionFilter.MaxStartDateRange;
                mentionFilterVM.MaxEndDateRange = mentionFilter.MaxEndDateRange;
                if (mentionFilter.AllowedMinAndMaxAndMultiOptionsMentionFilters.Any())
                {
                    AllowedMinAndMaxAndMultiOptionsMentionFilter allowedMinAndMax = mentionFilter.AllowedMinAndMaxAndMultiOptionsMentionFilters.FirstOrDefault();

                    mentionFilterVM.BlueskyMaxFollowers = allowedMinAndMax.BlueskyMaxFollowers;
                    mentionFilterVM.BlueskyMinFollowers = allowedMinAndMax.BlueskyMinFollowers;

                    mentionFilterVM.BlueskyMaxLikes = allowedMinAndMax.BlueskyMaxLikes;
                    mentionFilterVM.BlueskyMinLikes = allowedMinAndMax.BlueskyMinLikes;

                    mentionFilterVM.BlueskyMaxQuotes = allowedMinAndMax.BlueskyMaxQuotes;
                    mentionFilterVM.BlueskyMinQuotes = allowedMinAndMax.BlueskyMinQuotes;

                    mentionFilterVM.BlueskyMaxReplies = allowedMinAndMax.BlueskyMaxReplies;
                    mentionFilterVM.BlueskyMinReplies = allowedMinAndMax.BlueskyMinReplies;

                    mentionFilterVM.BlueskyMaxReposts = allowedMinAndMax.BlueskyMaxReposts;
                    mentionFilterVM.BlueskyMinReposts = allowedMinAndMax.BlueskyMinReposts;

                    mentionFilterVM.BlueskyMaxTotalPosts = allowedMinAndMax.BlueskyMaxTotalPosts;
                    mentionFilterVM.BlueskyMinTotalPosts = allowedMinAndMax.BlueskyMinTotalPosts;

                    mentionFilterVM.BlueskyMaxFollowing = allowedMinAndMax.BlueskyMaxFollowing;
                    mentionFilterVM.BlueskyMinFollowing = allowedMinAndMax.BlueskyMinFollowing;

                  

                    mentionFilterVM.FaceBookMaxComments = allowedMinAndMax.FaceBookMaxComments;
                    mentionFilterVM.FaceBookMinComments = allowedMinAndMax.FaceBookMinComments;

                    mentionFilterVM.FaceBookMaxLikes = allowedMinAndMax.FaceBookMaxLikes;
                    mentionFilterVM.FaceBookMinLikes = allowedMinAndMax.FaceBookMinLikes;

                    mentionFilterVM.FaceBookMaxShares = allowedMinAndMax.FaceBookMaxShares;
                    mentionFilterVM.FaceBookMinShares = allowedMinAndMax.FaceBookMinShares;
                    if (allowedMinAndMax.ExactMatchExcludedAuthorName != null)
                    {
                        mentionFilterVM.ExactMatchExcludeAuthorName = allowedMinAndMax.ExactMatchExcludedAuthorName.Value;
                    }
                    if (allowedMinAndMax.ExactMatchIncludedAuthorName != null)
                    {
                        mentionFilterVM.ExactMatchIncludeAuthorName = allowedMinAndMax.ExactMatchIncludedAuthorName.Value;
                    }
                    
                    mentionFilterVM.ExcludeAuthorName = allowedMinAndMax.ExcludedAuthorName;
                    mentionFilterVM.IncludeAuthorName = allowedMinAndMax.IncludedAuthorName;

                   
                    mentionFilterVM.SelectedAssignmentId = allowedMinAndMax.Assigned;
                    mentionFilterVM.SelectedPriorityId = allowedMinAndMax.Priority;
                    mentionFilterVM.SelectedMentionStatusId = allowedMinAndMax.Status;
                    mentionFilterVM.SelectedFaceBookUserRoleId = allowedMinAndMax.FaceBookUserRole;
                    mentionFilterVM.SelectedTwitterUserRoleId = allowedMinAndMax.TwitterUserRole;
                    mentionFilterVM.SelectedTwitterVerificationId = allowedMinAndMax.TwitterVerification;

                    mentionFilterVM.SelectedSourceTypetId = allowedMinAndMax.SourceType;
                    mentionFilterVM.SelectedCheckedMentionId = allowedMinAndMax.Checked;
                    if (allowedMinAndMax.Starred != null)
                    {
                        mentionFilterVM.StarredMentions = allowedMinAndMax.Starred.Value;
                    }
                   

                    mentionFilterVM.InstgramMaxComments = allowedMinAndMax.InstgramMaxComments;
                    mentionFilterVM.InstgramMinComments = allowedMinAndMax.InstgramMinComments;

                    mentionFilterVM.InstgramMaxFollowers = allowedMinAndMax.InstgramMaxFollowers;
                    mentionFilterVM.InstgramMinFollowers = allowedMinAndMax.InstgramMinFollowers;

                    mentionFilterVM.InstgramMaxFollowing = allowedMinAndMax.InstgramMaxFollowing;
                    mentionFilterVM.InstgramMinFollowing = allowedMinAndMax.InstgramMinFollowing;

                    mentionFilterVM.InstgramMaxInteractions = allowedMinAndMax.InstgramMaxInteractions;
                    mentionFilterVM.InstgramMinInteractions = allowedMinAndMax.InstgramMinInteractions;

                    mentionFilterVM.InstgramMaxLikes = allowedMinAndMax.InstgramMaxLikes;
                    mentionFilterVM.InstgramMinLikes = allowedMinAndMax.InstgramMinLikes;

                    mentionFilterVM.InstgramMaxPosts = allowedMinAndMax.InstgramMaxPosts;
                    mentionFilterVM.InstgramMinPosts = allowedMinAndMax.InstgramMinPosts;

                    mentionFilterVM.MaxEngagmentScore = allowedMinAndMax.MaxAllowedEngagmentScore;
                    mentionFilterVM.MinEngagmentScore = allowedMinAndMax.MinAllowedEngagmentScore;

                    mentionFilterVM.MaxImpact = allowedMinAndMax.MaxImpact;
                    mentionFilterVM.MinImpact = allowedMinAndMax.MinImpact;

                    mentionFilterVM.MaxReach = allowedMinAndMax.MaxReach;
                    mentionFilterVM.MinReach = allowedMinAndMax.MinReach;

                    mentionFilterVM.MaxSiteDailyVisitors = allowedMinAndMax.MaxAllowedDaillySiteVisitors;
                    mentionFilterVM.MinSiteDailyVisitors = allowedMinAndMax.MinAllowedDaillySiteVisitors;
                    mentionFilterVM.MaxSiteMonthlyVisitors = allowedMinAndMax.MaxAllowedMonthllySiteVisitors;
                    mentionFilterVM.MinSiteMonthlyVisitors = allowedMinAndMax.MinAllowedMonthllySiteVisitors;

                    mentionFilterVM.RedditMaxAuthorKarma = allowedMinAndMax.RedditMaxAuthorKarma;
                    mentionFilterVM.RedditMinAuthorKarma = allowedMinAndMax.RedditMinAuthorKarma;

                    mentionFilterVM.RedditMaxCommentCount = allowedMinAndMax.RedditMaxCommentCount;
                    mentionFilterVM.RedditMinCommentCount = allowedMinAndMax.RedditMinCommentCount;

                    mentionFilterVM.RedditMaxScore = allowedMinAndMax.RedditMaxScores;
                    mentionFilterVM.RedditMinScore = allowedMinAndMax.RedditMinScores;

                    mentionFilterVM.RedditMaxSubscribers = allowedMinAndMax.RedditMaxSubscribers;
                    mentionFilterVM.RedditMinSubscribers = allowedMinAndMax.RedditMinSubscribers;

                    mentionFilterVM.TwitterMaxFollowers = allowedMinAndMax.TwitterMaxFollowers;
                    mentionFilterVM.TwitterMinFollowers = allowedMinAndMax.TwitterMinFollowers;

                    mentionFilterVM.TwitterMaxFollowing = allowedMinAndMax.TwitterMaxFollowing;
                    mentionFilterVM.TwitterMinFollowing = allowedMinAndMax.TwitterMinFollowing;

                    mentionFilterVM.TwitterMaxImpressions = allowedMinAndMax.TwitterMaxImpressions;
                    mentionFilterVM.TwitterMinImpressions = allowedMinAndMax.TwitterMinImpressions;

                    mentionFilterVM.TwitterMaxLikes = allowedMinAndMax.TwitterMaxLikes;
                    mentionFilterVM.TwitterMinLikes = allowedMinAndMax.TwitterMinLikes;


                    mentionFilterVM.TwitterMaxReposts = allowedMinAndMax.TwitterMaxRepost;
                    mentionFilterVM.TwitterMinReposts = allowedMinAndMax.TwitterMinRepost;

                    mentionFilterVM.TwitterMaxTotalPosts = allowedMinAndMax.TwitterMaxTotalPosts;
                    mentionFilterVM.TwitterMinTotalPosts = allowedMinAndMax.TwitterMinTotalPosts;
                    mentionFilterVM.NewsBarMinVisitors = allowedMinAndMax.NewsBarMinVisitors;
                    mentionFilterVM.NewsBarMatchingWordes = allowedMinAndMax.NewsBarMatchingWordes;


                }
                mentionFilterVM.MentionFilterID = mentionFilter.Id;

                mentionFilterVM.ClientProjectGlobalMetricsList = new SelectList(ClientProject.GlobalMetrics.Where(p => p.IsActive == true), "Id", "FilterName");
                mentionFilterVM.ClientProjectList = new SelectList(db.ClientProjects.Where(p => p.IsActive == true), "Id", "ProjectName");
                mentionFilterVM.QueryList = new SelectList(ClientProject.ClientProjectQueries.Where(p => p.IsActive == true), "Id", "QuerieName");

                mentionFilterVM.ClientProjectSiteList = new SelectList(ClientProject.ClientProjectSites.Where(p => p.IsActive == true), "Id", "SiteName");

                mentionFilterVM.CategoriesList = new SelectList(ClientProject.ClientProjectCategories.Where(p => p.IsActive == true), "Id", "CategoryName");
                mentionFilterVM.SubCategoriesList = new SelectList(db.ClientProjectSubCategories.Where(e => e.ClientProjectCategory.ClientProjectId == ClientProject.Id && e.IsActive == true), "Id", "SubCategoryName");
                mentionFilterVM.AuthorGroupsList = new SelectList(ClientProject.AuthorGroups.Where(p => p.IsActive == true), "Id", "GroupName");
                mentionFilterVM.ClientProjectLocationList = new SelectList(ClientProject.ProjectLocations.Where(p => p.IsActive == true), "Id", "LocationName");
                mentionFilterVM.RuleTagsList = new SelectList(ClientProject.ProjectRuleTags.Where(p => p.IsActive == true), "Id", "BrandWatchRuleTagName");
                mentionFilterVM.ContentSourcesList = new SelectList(db.QueryContentSources.Where(e => e.ClientProjectQuerie.ClientProjectId == ClientProject.Id && e.ClientProjectQuerie.IsActive == true), "Id", "SourceName");
                mentionFilterVM.QueryLanguageList = new SelectList(db.QueryLanguages.Where(e => e.ClientProjectQuerie.ClientProjectId == ClientProject.Id && e.ClientProjectQuerie.IsActive == true), "Id", "Language");
                mentionFilterVM.DomainList = new SelectList(db.NewsBarDomains, "Id", "Domain");

                if (ClientProject.ClientProjectQueries.Where(p => p.IsActive == true).Any())
                {
                    mentionFilterVM.SelectedQueryId = ClientProject.ClientProjectQueries.Where(p => p.IsActive == true).FirstOrDefault().Id;
                }


                mentionFilterVM.TwitterAccountTypesList = new SelectList(db.AccountTypes.Where(e => e.GlobalMetric.ClientProjectId == ClientProject.Id && e.Active == true), "Id", "BWAccountType");
                mentionFilterVM.TwitterGendersList = new SelectList(db.Genders.Where(e => e.GlobalMetric.ClientProjectId == ClientProject.Id && e.Active == true), "Id", "BWGender");
                mentionFilterVM.TwitterProfessionsList = new SelectList(db.TwitterProfessions.Where(e => e.GlobalMetric.ClientProjectId == ClientProject.Id && e.Active == true), "Id", "Profession");
                mentionFilterVM.TwitterInterestsList = new SelectList(db.TwitterInterests.Where(e => e.GlobalMetric.ClientProjectId == ClientProject.Id && e.Active == true), "Id", "Interest");

                mentionFilterVM.ReactScoreOverallList = new SelectList(db.ReactScoreOveralls.Where(e => e.GlobalMetric.ClientProjectId == ClientProject.Id && e.Active == true), "Id", "ScoreType");
                mentionFilterVM.ReactScoreHarmList = new SelectList(db.ReactScoreHarms.Where(e => e.GlobalMetric.ClientProjectId == ClientProject.Id && e.Active == true), "Id", "ScoreType");
                mentionFilterVM.ReactScoreEmotionalityList = new SelectList(db.ReactScoreEmotionalities.Where(e => e.GlobalMetric.ClientProjectId == ClientProject.Id && e.Active == true), "Id", "ScoreType");
                mentionFilterVM.EmotionList = new SelectList(db.Emotions.Where(e => e.GlobalMetric.ClientProjectId == ClientProject.Id && e.Active == true), "Id", "EmotionType");
                mentionFilterVM.SentimentsList = new SelectList(db.Sentiments.Where(e => e.GlobalMetric.ClientProjectId == ClientProject.Id && e.Active == true), "Id", "BWSentiment");
                mentionFilterVM.MentionTypeList = new SelectList(db.MentionTypes.Where(e => e.GlobalMetric.ClientProjectId == ClientProject.Id && e.Active == true), "Id", "TypeOfMention");
                mentionFilterVM.SourceTypeList = new SelectList(db.SourceTypes.Where(e => e.GlobalMetric.ClientProjectId == ClientProject.Id && e.Active == true), "Id", "BWSourceType");
                mentionFilterVM.SubTypesList = new SelectList(db.SubTypes.Where(e => e.GlobalMetric.ClientProjectId == ClientProject.Id && e.Active == true), "Id", "BWSubType");
                mentionFilterVM.SocialPanelList = new SelectList(db.SocialPanels.Where(e => e.GlobalMetric.ClientProjectId == ClientProject.Id && e.Active == true), "Id", "PanelName");
                mentionFilterVM.AssignmentList = new SelectList(db.Assignments.Where(e => e.GlobalMetric.ClientProjectId == ClientProject.Id && e.Active == true), "Id", "EmployeeUserName");
                mentionFilterVM.PriorityList = new SelectList(db.Priorities.Where(e => e.GlobalMetric.ClientProjectId == ClientProject.Id && e.Active == true), "Id", "PriorityType");
                mentionFilterVM.MentionStatusList = new SelectList(db.MentionStatus.Where(e => e.GlobalMetric.ClientProjectId == ClientProject.Id && e.Active == true), "Id", "Status");
                mentionFilterVM.CheckedMentionList = new SelectList(db.CheckedMentions.Where(e => e.GlobalMetric.ClientProjectId == ClientProject.Id && e.Active == true), "Id", "CheckType");
                mentionFilterVM.TwitterUserRoleList = new SelectList(db.TwitterUserRoles.Where(e => e.GlobalMetric.ClientProjectId == ClientProject.Id && e.Active == true), "Id", "RoleName");
                mentionFilterVM.TwitterMentionTypeList = new SelectList(db.TwitterMentionTypes.Where(e => e.GlobalMetric.ClientProjectId == ClientProject.Id && e.Active == true), "Id", "MentionTypeOfTwitter");
                mentionFilterVM.TwitterVerificationList = new SelectList(db.TwitterVerifications.Where(e => e.GlobalMetric.ClientProjectId == ClientProject.Id && e.Active == true), "Id", "Verification");
                mentionFilterVM.TwitterVerificationTypeList = new SelectList(db.TwitterVerificationTypes.Where(e => e.GlobalMetric.ClientProjectId == ClientProject.Id && e.Active == true), "Id", "VerificationType");
                mentionFilterVM.FaceBookUserRoleList = new SelectList(db.FaceBookUserRoles.Where(e => e.GlobalMetric.ClientProjectId == ClientProject.Id && e.Active == true), "Id", "UserRoleName");
                mentionFilterVM.FaceBookMentionTypeList = new SelectList(db.FaceBookMentionTypes.Where(e => e.GlobalMetric.ClientProjectId == ClientProject.Id && e.Active == true), "Id", "MentionTypeOfFaceBook");
                mentionFilterVM.InstagramMentionTypeList = new SelectList(db.InstagramMentionTypes.Where(e => e.GlobalMetric.ClientProjectId == ClientProject.Id && e.Active == true), "Id", "MentionTypeOfInstgram");
                mentionFilterVM.RedditPollList = new SelectList(db.RedditPolls.Where(e => e.GlobalMetric.ClientProjectId == ClientProject.Id && e.Active == true), "Id", "PollChooice");
                mentionFilterVM.SubredditTopicList = new SelectList(db.SubredditTopics.Where(e => e.GlobalMetric.ClientProjectId == ClientProject.Id && e.Active == true), "Id", "TopicName");
                mentionFilterVM.RedditNSFWList = new SelectList(db.RedditNSFWs.Where(e => e.GlobalMetric.ClientProjectId == ClientProject.Id && e.Active == true), "Id", "NSFWChooice");
                return mentionFilterVM;

            }

            return null;
         


            
           
        }

        public static void AddMentionToClientProject(List<MentionMapper> mentionMappers, int clientProjectID)
        {
            var db = new DBModelBrandWatchContainer();

            var clientProject = db.ClientProjects.Find(clientProjectID);
            if (clientProject != null)
            {
                try
                {
                   
                    foreach (var mention in mentionMappers)
                    {
                       
                        if (!String.IsNullOrEmpty(mention.resourceId))
                        {
                            var mentionExist = clientProject.ProjectMentions.Where(m => m.ResourceId == mention.resourceId).FirstOrDefault();
                            if (mentionExist == null)
                            {
                                ProjectMention projectMention = new ProjectMention();
                                projectMention.AccountType = mention.accountType;
                                projectMention.AssetContentId = mention.assetContentId;
                                projectMention.AssetThumbId = mention.assetThumbId;
                                if (!string.IsNullOrEmpty(mention.added) && mention.added.Contains('T'))
                                {
                                    var splitedDate = mention.added.Split('T');
                                    projectMention.AddedDateToBrandWatsh = DateTime.Parse(splitedDate[0]);
                                }
                                else
                                {
                                    projectMention.AddedDateToBrandWatsh = DateTime.Now;

                                }
                               
                                projectMention.Assignment = mention.assignment;
                                projectMention.Author = mention.author;
                                projectMention.AuthorVerifiedType = mention.authorVerifiedType;
                                projectMention.AvatarUrl = mention.avatarUrl;
                                projectMention.BatchId = mention.batchId;

                                projectMention.BlogName = mention.blogName;
                                projectMention.BlueskyAuthorId = mention.blueskyAuthorId;
                                projectMention.BlueskyFollowers = mention.blueskyFollowers;
                                projectMention.BlueskyFollowing = mention.blueskyFollowing;
                                projectMention.BlueskyLikes = mention.blueskyLikes;
                                projectMention.BlueskyPosts = mention.blueskyPosts;
                                projectMention.BlueskyQuotes = mention.blueskyQuotes;
                                projectMention.BlueskyReplies = mention.blueskyReplies;
                                projectMention.BlueskyReposts = mention.blueskyReposts;
                                projectMention.BroadcastMediaUrl = mention.broadcastMediaUrl;
                                projectMention.BroadcastType = mention.broadcastType;
                                projectMention.MediaType = mention.mediaType;
                                projectMention.StationName = mention.stationName;


                                projectMention.Checked = mention.Checked;
                                projectMention.City = mention.city;
                                projectMention.ClientProject = clientProject;
                                projectMention.ClientProjectId = clientProject.Id;
                               
                                projectMention.ContentSource = mention.contentSource;
                                projectMention.ContentSourceName = mention.contentSourceName;
                                projectMention.Continent = mention.continent;
                                projectMention.Country = mention.country;


                                projectMention.DailyVisitors = mention.dailyVisitors;
                                if (mention.displayUrls != null)
                                {
                                    if (mention.displayUrls.Any())
                                    {
                                        foreach (var displayUrl in mention.displayUrls)
                                        {
                                            projectMention.DisplayUrls += "###" + displayUrl;
                                        }
                                    }
                                }

                                projectMention.Domain = mention.domain;


                                projectMention.EngagementType = mention.engagementType;

                                if (mention.expandedUrls != null)
                                {
                                    if (mention.expandedUrls.Any())
                                    {
                                        foreach (var expandedUrls in mention.expandedUrls)
                                        {
                                            projectMention.ExpandedUrls += "###" + expandedUrls;
                                        }
                                    }
                                }



                                projectMention.FacebookAuthorId = mention.facebookAuthorId;
                                projectMention.FacebookComments = mention.facebookComments;
                                projectMention.FacebookLikes = mention.facebookLikes;
                                projectMention.FacebookRole = mention.facebookRole;
                                projectMention.FacebookShares = mention.facebookShares;
                                projectMention.FullName = mention.fullname;


                                projectMention.Gender = mention.gender;
                                projectMention.GUIDNumber = mention.guid;


                                projectMention.Impact = mention.impact;

                                if (mention.insightsHashtag != null)
                                {
                                    if (mention.insightsHashtag.Any())
                                    {
                                        foreach (var hashtag in mention.insightsHashtag)
                                        {
                                            projectMention.InsightsHashtag += "###" + hashtag;
                                        }
                                    }
                                }
                                if (mention.insightsMentioned != null)
                                {
                                    if (mention.insightsMentioned.Any())
                                    {
                                        foreach (var insightMentioned in mention.insightsMentioned)
                                        {
                                            projectMention.InsightsMentioned += "###" + insightMentioned;
                                        }
                                    }
                                }
                                projectMention.Impact = mention.impact;
                                projectMention.Impressions = mention.impressions;
                                projectMention.InstagramCommentCount = mention.instagramCommentCount;
                                projectMention.InstagramFollowerCount = mention.instagramFollowerCount;
                                projectMention.InstagramFollowingCount = mention.instagramFollowingCount;
                                projectMention.InstagramInteractionsCount = mention.instagramInteractionsCount;
                                projectMention.InstagramLikeCount = mention.instagramLikeCount;
                                projectMention.InstagramPostCount = mention.instagramPostCount;

                                if (mention.interest != null)
                                {
                                    if (mention.interest.Any())
                                    {
                                        foreach (var interest in mention.interest)
                                        {
                                            projectMention.Interest += "###" + interest;
                                        }
                                    }
                                }



                                projectMention.ItemReview = mention.itemReview;
                                projectMention.Kicker = mention.kicker;
                                projectMention.Language = mention.language;
                                projectMention.LastAssignmentDate = mention.lastAssignmentDate;
                                projectMention.LinkedinComments = mention.linkedinComments;
                                projectMention.LinkedinEngagement = mention.linkedinEngagement;
                                projectMention.LinkedinImpressions = mention.linkedinImpressions;
                                projectMention.LinkedinLikes = mention.linkedinLikes;
                                projectMention.LinkedinSponsored = mention.linkedinSponsored;
                                projectMention.LinkedinLikes = mention.linkedinLikes;
                                projectMention.LinkedinVideoViews = mention.linkedinVideoViews;
                                projectMention.LocationName = mention.locationName;

                                if (mention.mediaUrls != null)
                                {
                                    if (mention.mediaUrls.Any())
                                    {
                                        foreach (var mediaUrl in mention.mediaUrls)
                                        {
                                            projectMention.MediaUrls += "###" + mediaUrl;
                                        }
                                    }
                                }

                                if (mention.categoryDetails != null)
                                {
                                    if (mention.categoryDetails.Any())
                                    {
                                        foreach (var _categoryDetail in mention.categoryDetails)
                                        {
                                            MentionSubCategoryDetail categoryDetail = new MentionSubCategoryDetail();
                                            categoryDetail.BrandWatchSubCategoryID = _categoryDetail.id;
                                            categoryDetail.SubCategoryName = _categoryDetail.name;
                                            categoryDetail.DisplayName = _categoryDetail.displayName;
                                            categoryDetail.ParentCategoryID = _categoryDetail.parentId;
                                            categoryDetail.ParentCategoryName = _categoryDetail.parentName;


                                            projectMention.MentionSubCategoryDetails.Add(categoryDetail);
                                        }
                                    }
                                }
                               

                               
                                projectMention.MonthlyVisitors = mention.monthlyVisitors;


                                projectMention.OriginalUrl = mention.originalUrl;

                                projectMention.ParentPostId = mention.parentPostId;
                                projectMention.PublicationId = mention.publicationId;
                                projectMention.ParentBlogName = mention.parentBlogName;
                                projectMention.ParentPostIdTumblr = mention.parentPostId;
                                if (!string.IsNullOrEmpty(mention.date) && mention.date.Contains('T'))
                                {
                                    var splitedDate = mention.date.Split('T');
                                    projectMention.PostedDate = DateTime.Parse(splitedDate[0]);
                                }
                                else
                                {
                                    projectMention.PostedDate = DateTime.Now;

                                }
                                projectMention.PubType = mention.pubType;
                                projectMention.PublicationName = mention.publicationName;
                                projectMention.PublisherSubType = mention.publisherSubType;
                                if (mention.professions != null)
                                {
                                    if (mention.professions.Any())
                                    {
                                        foreach (var profession in mention.professions)
                                        {
                                            projectMention.Professions += profession.profession + "###" + profession.jobTitle + ",";
                                        }
                                    }
                                }

                                projectMention.QueryID = mention.queryId;
                                projectMention.QueryName = mention.queryName;

                                projectMention.Rating = mention.rating;
                                projectMention.ReachEstimate = mention.reachEstimate;
                                projectMention.ReceivedDate = DateTime.Now;
                                projectMention.RedditComments = mention.redditComments;
                                projectMention.RedditScore = mention.redditScore;
                                projectMention.RedditAuthorKarma = mention.redditAuthorKarma;
                                projectMention.RedditAuthorAwarderKarma = mention.redditAuthorAwarderKarma;
                                projectMention.Region = mention.region;
                                projectMention.ResourceId = mention.resourceId;
                                projectMention.RetweetOf = mention.retweetOf;
                                projectMention.ReplyTo = mention.replyTo;
                                projectMention.ResourceType = mention.resourceType;
                                projectMention.RootPostId = mention.rootPostId;
                                projectMention.RootBlogName = mention.rootBlogName;


                                projectMention.Sentiment = mention.sentiment;
                                projectMention.SinaWeiboAuthorId = mention.sinaWeiboAuthorId;
                                projectMention.SinaWeiboFollowers = mention.sinaWeiboFollowers;
                                projectMention.SinaWeiboFollowing = mention.sinaWeiboFollowing;
                                projectMention.SinaWeiboBiFollowers = mention.sinaWeiboBiFollowers;
                                projectMention.SinaWeiboPostCount = mention.sinaWeiboPostCount;
                                projectMention.SinaWeiboFavouritesCount = mention.sinaWeiboFavouritesCount;
                                projectMention.SinaWeiboRawLocation = mention.sinaWeiboRawLocation;
                                projectMention.Snippet = mention.snippet;
                                projectMention.Status = mention.status;
                                projectMention.Starred = mention.starred;
                                projectMention.SubType = mention.subtype;
                                projectMention.SubReddit = mention.subreddit;
                                projectMention.SubredditNSFW = mention.subredditNSFW;

                                if (mention.subscriptions != null)
                                {
                                    if (mention.subscriptions.Any())
                                    {
                                        foreach (var subscription in mention.subscriptions)
                                        {
                                            projectMention.Subscriptions += subscription + "###";
                                        }
                                    }
                                }

                                projectMention.SubredditSubscribers = mention.subredditSubscribers;
                                projectMention.SubTitle = mention.subTitle;


                                projectMention.ThreadAuthor = mention.threadAuthor;
                                projectMention.TthreadCreated = mention.threadCreated;
                                projectMention.ThreadEntryType = mention.threadEntryType;
                                projectMention.ThreadId = mention.threadId;
                                projectMention.ThreadURL = mention.threadURL;
                                projectMention.ThreadsViews = mention.threadsViews;
                                projectMention.ThreadsLikes = mention.threadsLikes;
                                projectMention.ThreadsReplies = mention.threadsReplies;
                                projectMention.ThreadsReposts = mention.threadsReposts;
                                projectMention.ThreadsQuotes = mention.threadsQuotes;
                                projectMention.ThreadsShares = mention.threadsShares;

                                projectMention.TiktokComments = mention.tiktokComments;
                                projectMention.TiktokLikes = mention.tiktokLikes;
                                projectMention.TiktokShares = mention.tiktokShares;
                                projectMention.TiktokViews = mention.tiktokViews;
                                projectMention.TiktokReach = mention.tiktokReach;
                                projectMention.TiktokConnectedAccount = mention.tiktokConnectedAccount;

                                projectMention.Title = mention.title;

                                projectMention.TwitterAuthorId = mention.twitterAuthorId;
                                projectMention.TwitterFollowers = mention.twitterFollowers;
                                projectMention.TwitterFollowing = mention.twitterFollowing;
                                projectMention.TwitterLikeCount = mention.twitterLikeCount;
                                projectMention.TwitterPostCount = mention.twitterPostCount;
                                projectMention.TwitterReplyCount = mention.twitterReplyCount;
                                projectMention.TwitterRetweets = mention.twitterRetweets;
                                projectMention.TwitterRole = mention.twitterRole;
                                projectMention.TwitterVerified = mention.twitterVerified;


                                projectMention.UpdatedDate = mention.updated;
                                projectMention.URL = mention.url;

                                projectMention.WebBlogTitle = mention.weblogTitle;
                                projectMention.SubredditNSFW = mention.subredditNSFW;
                                if (mention.subredditTopics != null)
                                {
                                    if (mention.subredditTopics.Any())
                                    {
                                        foreach (var subredditTopic in mention.subredditTopics)
                                        {
                                            projectMention.SubredditTopic += subredditTopic + "###";
                                        }
                                    }
                                }

                                projectMention.PodcastDurationMs = mention.podcastDurationMs;
                                projectMention.PodcastAudienceEstimate = mention.podcastAudienceEstimate;
                                projectMention.PageTypeName = mention.pageTypeName;
                                projectMention.ReactScoreOverall = mention.reactScoreOverall;
                                projectMention.ReactScoreSpam = mention.reactScoreSpam;
                                projectMention.ReactScoreEmotionality = mention.reactScoreEmotionality;
                                projectMention.ReactScoreHarmful = mention.reactScoreHarmful;
                                projectMention.ReactScoreFakeness = mention.reactScoreFakeness;
                                projectMention.EngagementScore = mention.engagementScore;
                                projectMention.EngagementType = mention.engagementType;


                                projectMention.YoutubeDurationMilliseconds = mention.youtubeDurationMilliseconds;
                                projectMention.YoutubeFavourites = mention.youtubeFavourites;
                                projectMention.YoutubeLikes = mention.youtubeLikes;
                                projectMention.YoutubeSubscriberCount = mention.youtubeSubscriberCount;
                                projectMention.YoutubeVideoCount = mention.youtubeVideoCount;
                                projectMention.YoutubeViews = mention.youtubeViews;
                                db.ProjectMentions.Add(projectMention);
                                
                            }
                        }

                    }
                    db.SaveChanges();
                   // return clientProject.Id;
                }
                catch (Exception ex)
                {
                    var resultdata = ex.Data;
                    var resultInner = ex.InnerException;
                    var resultMessag = ex.Message;
                    throw ex;
                }

            }
            //else
            //{
            //   // return clientProject.Id;
            //}

        }


        /// <summary>
        /// Getting Select List Section
        /// </summary>
        /// <param name="clientId"></param>
        /// <param name="projectId"></param>
        /// <returns></returns>

        public static IEnumerable<SelectListItem> GetProjectSourceTyps(int projectId)
        {
            var db = new DBModelBrandWatchContainer();

            IEnumerable<SelectListItem> _SourceType = new SelectList(db.SourceTypes.Where(e => e.GlobalMetric.ClientProjectId == projectId && e.GlobalMetric.IsActive == true), "Id", "BWSourceType");

            return _SourceType;
        }

        public static IEnumerable<SelectListItem> GetProjectSubTypes(int projectId)
        {
            var db = new DBModelBrandWatchContainer();

            IEnumerable<SelectListItem> _SubTypes = new SelectList(db.SubTypes.Where(e => e.GlobalMetric.ClientProjectId == projectId && e.GlobalMetric.IsActive == true), "Id", "BWSubType");

            return _SubTypes;
        }


        public static IEnumerable<SelectListItem> GetProjectTwitterInterests(int projectId)
        {
            var db = new DBModelBrandWatchContainer();

            IEnumerable<SelectListItem> _TwitterInterests = new SelectList(db.TwitterInterests.Where(e => e.GlobalMetric.ClientProjectId == projectId && e.GlobalMetric.IsActive == true), "Id", "Interest");

            return _TwitterInterests;
        }
        public static IEnumerable<SelectListItem> GetProjectTwitterProfessions(int projectId)
        {
            var db = new DBModelBrandWatchContainer();

            IEnumerable<SelectListItem> _TwitterProfessions = new SelectList(db.TwitterProfessions.Where(e => e.GlobalMetric.ClientProjectId == projectId && e.GlobalMetric.IsActive == true), "Id", "Profession");

            return _TwitterProfessions;
        }
        public static IEnumerable<SelectListItem> GetProjectTwitterGenders(int projectId)
        {
            var db = new DBModelBrandWatchContainer();

            IEnumerable<SelectListItem> _TwitterGenders = new SelectList(db.Genders.Where(e => e.GlobalMetric.ClientProjectId == projectId && e.GlobalMetric.IsActive == true), "Id", "BWGender");

            return _TwitterGenders;
        }
        public static IEnumerable<SelectListItem> GetProjectTwitterAccountTypes(int projectId)
        {
            var db = new DBModelBrandWatchContainer();

            IEnumerable<SelectListItem> _TwitterAccountTypes = new SelectList(db.AccountTypes.Where(e => e.GlobalMetric.ClientProjectId == projectId && e.GlobalMetric.IsActive == true), "Id", "BWAccountType");

            return _TwitterAccountTypes;
        }
        public static IEnumerable<SelectListItem> GetProjectSubredditTopics(int projectId)
        {
            var db = new DBModelBrandWatchContainer();

            IEnumerable<SelectListItem> _SubredditTopics = new SelectList(db.SubredditTopics.Where(e => e.GlobalMetric.ClientProjectId == projectId && e.GlobalMetric.IsActive == true), "Id", "TopicName");

            return _SubredditTopics;
        }
        public static IEnumerable<SelectListItem> GetProjectRedditNSFWs(int projectId)
        {
            var db = new DBModelBrandWatchContainer();

            IEnumerable<SelectListItem> _RedditNSFWs = new SelectList(db.RedditNSFWs.Where(e => e.GlobalMetric.ClientProjectId == projectId && e.GlobalMetric.IsActive == true), "Id", "NSFWChooice");

            return _RedditNSFWs;
        }
        public static IEnumerable<SelectListItem> GetProjectRedditPoll(int projectId)
        {
            var db = new DBModelBrandWatchContainer();

            IEnumerable<SelectListItem> _RedditPolls = new SelectList(db.RedditPolls.Where(e => e.GlobalMetric.ClientProjectId == projectId && e.GlobalMetric.IsActive == true), "Id", "PollChooice");

            return _RedditPolls;
        }

        public static IEnumerable<SelectListItem> GetQueryContentSources(int querytId)
        {
            var db = new DBModelBrandWatchContainer();

            IEnumerable<SelectListItem> _ContentSources = new SelectList(db.QueryContentSources.Where(e => e.ClientProjectQuerieId == querytId && e.ClientProjectQuerie.IsActive == true), "Id", "SourceName");

            return _ContentSources;
        }

        public static IEnumerable<SelectListItem> GetQueryLanguages(int querytId)
        {
            var db = new DBModelBrandWatchContainer();

            IEnumerable<SelectListItem> _Languages = new SelectList(db.QueryLanguages.Where(p => p.ClientProjectQuerieId == querytId && p.ClientProjectQuerie.IsActive == true), "Id", "Language");

            return _Languages;
        }
        public static IEnumerable<SelectListItem> GetProjectContentSources(int projectId)
        {
            var db = new DBModelBrandWatchContainer();

            IEnumerable<SelectListItem> _ContentSources = new SelectList(db.QueryContentSources.Where(e => e.ClientProjectQuerie.ClientProjectId == projectId && e.ClientProjectQuerie.IsActive == true), "Id", "SourceName");

            return _ContentSources;
        }

        public static IEnumerable<SelectListItem> GetProjectLanguage(int projectId)
        {
            var db = new DBModelBrandWatchContainer();

            IEnumerable<SelectListItem> _Languages = new SelectList(db.QueryLanguages.Where(p => p.ClientProjectQuerie.ClientProjectId == projectId && p.ClientProjectQuerie.IsActive == true), "Id", "Language");

            return _Languages;
        }

        public static IEnumerable<SelectListItem> GetProjectInstagramMentionType(int projectId)
        {
            var db = new DBModelBrandWatchContainer();

            IEnumerable<SelectListItem> _InstagramMentionTypes = new SelectList(db.InstagramMentionTypes.Where(e => e.GlobalMetric.ClientProjectId == projectId && e.Active == true), "Id", "MentionTypeOfInstgram");

            return _InstagramMentionTypes;
        }

        public static IEnumerable<SelectListItem> GetProjectFaceBookMentionType(int projectId)
        {
            var db = new DBModelBrandWatchContainer();

            IEnumerable<SelectListItem> _FaceBookMentionTypes = new SelectList(db.FaceBookMentionTypes.Where(p => p.GlobalMetric.ClientProjectId == projectId && p.Active == true), "Id", "MentionTypeOfFaceBook");

            return _FaceBookMentionTypes;
        }
        public static IEnumerable<SelectListItem> GetProjectFaceBookUserRoels(int projectId)
        {
            var db = new DBModelBrandWatchContainer();

            IEnumerable<SelectListItem> _FaceBookUserRoels = new SelectList(db.FaceBookUserRoles.Where(p => p.GlobalMetric.ClientProjectId == projectId && p.Active == true), "Id", "UserRoleName");

            return _FaceBookUserRoels;
        }

        public static IEnumerable<SelectListItem> GetProjectTwitterVerificationTypes(int projectId)
        {
            var db = new DBModelBrandWatchContainer();

            IEnumerable<SelectListItem> _TwitterVerificationTypes = new SelectList(db.TwitterVerificationTypes.Where(p => p.GlobalMetric.ClientProjectId == projectId && p.Active == true), "Id", "VerificationType");

            return _TwitterVerificationTypes;
        }
        public static IEnumerable<SelectListItem> GetProjectTwitterVerifications(int projectId)
        {
            var db = new DBModelBrandWatchContainer();

            IEnumerable<SelectListItem> _TwitterVerifications = new SelectList(db.TwitterVerifications.Where(p => p.GlobalMetric.ClientProjectId == projectId && p.Active == true), "Id", "Verification");

            return _TwitterVerifications;
        }
        public static IEnumerable<SelectListItem> GetProjectTwitterMentionTypes(int projectId)
        {
            var db = new DBModelBrandWatchContainer();

            IEnumerable<SelectListItem> _TwitterMentionTypes = new SelectList(db.TwitterMentionTypes.Where(p => p.GlobalMetric.ClientProjectId == projectId && p.Active == true), "Id", "MentionTypeOfTwitter");

            return _TwitterMentionTypes;
        }
        public static IEnumerable<SelectListItem> GetProjectMentionStatus(int projectId)
        {
            var db = new DBModelBrandWatchContainer();

            IEnumerable<SelectListItem> _MentionStatus = new SelectList(db.MentionStatus.Where(p => p.GlobalMetric.ClientProjectId == projectId && p.Active == true), "Id", "Status");

            return _MentionStatus;
        }
        public static IEnumerable<SelectListItem> GetProjectPriorities(int projectId)
        {
            var db = new DBModelBrandWatchContainer();

            IEnumerable<SelectListItem> _Priorities = new SelectList(db.Priorities.Where(p => p.GlobalMetric.ClientProjectId == projectId && p.Active == true), "Id", "PriorityType");

            return _Priorities;
        }
        public static IEnumerable<SelectListItem> GetProjectCheckedMentions(int projectId)
        {
            var db = new DBModelBrandWatchContainer();

            IEnumerable<SelectListItem> _CheckedMentions = new SelectList(db.CheckedMentions.Where(p => p.GlobalMetric.ClientProjectId == projectId && p.Active == true), "Id", "CheckType");

            return _CheckedMentions;
        }
        public static IEnumerable<SelectListItem> GetProjectAssignements(int projectId)
        {
            var db = new DBModelBrandWatchContainer();

            IEnumerable<SelectListItem> _TwitterUserRoles = new SelectList(db.Assignments.Where(p => p.GlobalMetric.ClientProjectId == projectId && p.Active == true), "Id", "EmployeeUserName");

            return _TwitterUserRoles;
        }

        public static IEnumerable<SelectListItem> GetProjectTwitterUserRole(int projectId)
        {
            var db = new DBModelBrandWatchContainer();

            IEnumerable<SelectListItem> _TwitterUserRoles = new SelectList(db.TwitterUserRoles.Where(p => p.GlobalMetric.ClientProjectId == projectId && p.Active == true), "Id", "RoleName");

            return _TwitterUserRoles;
        }

        public static IEnumerable<SelectListItem> GetProjectSiteList(int projectId)
        {
            var db = new DBModelBrandWatchContainer();

            IEnumerable<SelectListItem> _SiteLists = new SelectList(db.ClientProjectSites.Where(p => p.ClientProjectId == projectId && p.IsActive == true), "Id", "SiteName");

            return _SiteLists;
        }


        public static IEnumerable<SelectListItem> GetProjectLocation(int projectId)
        {
            var db = new DBModelBrandWatchContainer();

            IEnumerable<SelectListItem> _Locations = new SelectList(db.ProjectLocations.Where(p => p.ClientProjectId == projectId && p.IsActive == true), "Id", "LocationName");

            return _Locations;
        }
        public static IEnumerable<SelectListItem> GetProjectAuthorGroup(int projectId)
        {
            var db = new DBModelBrandWatchContainer();

            IEnumerable<SelectListItem> _AutourGroups = new SelectList(db.AuthorGroups.Where(p => p.ClientProjectId == projectId && p.IsActive == true), "Id", "GroupName");

            return _AutourGroups;
        }

        public static IEnumerable<SelectListItem> GetProjectSocialPanal(int projectId)
        {
            var db = new DBModelBrandWatchContainer();

            IEnumerable<SelectListItem> _SocialPanal = new SelectList(db.SocialPanels.Where(e => e.GlobalMetric.ClientProjectId == projectId && e.Active == true), "Id", "PanelName");

            return _SocialPanal;
        }
        public static IEnumerable<SelectListItem> GetProjectEmotion(int projectId)
        {
            var db = new DBModelBrandWatchContainer();

            IEnumerable<SelectListItem> _Emotions = new SelectList(db.Emotions.Where(e => e.GlobalMetric.ClientProjectId == projectId && e.Active == true), "Id", "EmotionType");

            return _Emotions;
        }
        public static IEnumerable<SelectListItem> GetProjectSentiment(int projectId)
        {
            var db = new DBModelBrandWatchContainer();

            IEnumerable<SelectListItem> _Sentiments = new SelectList(db.Sentiments.Where(e => e.GlobalMetric.ClientProjectId == projectId && e.Active == true), "Id", "BWSentiment");

            return _Sentiments;
        }

        public static IEnumerable<SelectListItem> GetProjectReactScoreEmotionality(int projectId)
        {
            var db = new DBModelBrandWatchContainer();

            IEnumerable<SelectListItem> _ReactScoreEmotionalities = new SelectList(db.ReactScoreEmotionalities.Where(e => e.GlobalMetric.ClientProjectId == projectId && e.Active == true), "Id", "ScoreType");

            return _ReactScoreEmotionalities;
        }
        public static IEnumerable<SelectListItem> GetProjectReactScoreHarm(int projectId)
        {
            var db = new DBModelBrandWatchContainer();

            IEnumerable<SelectListItem> _ReactScoreHarms = new SelectList(db.ReactScoreHarms.Where(e => e.GlobalMetric.ClientProjectId == projectId && e.Active == true), "Id", "ScoreType");

            return _ReactScoreHarms;
        }
        public static IEnumerable<SelectListItem> GetProjectReactScoreOverAll(int projectId)
        {
            var db = new DBModelBrandWatchContainer();

            IEnumerable<SelectListItem> _ReactScoreOverAlls = new SelectList(db.ReactScoreOveralls.Where(e => e.GlobalMetric.ClientProjectId == projectId && e.Active == true), "Id", "ScoreType");

            return _ReactScoreOverAlls;
        }
        public static IEnumerable<SelectListItem> GetClientProjectList(int clientId)
        {
            var db = new DBModelBrandWatchContainer();

            IEnumerable<SelectListItem> _ClientProjects = new SelectList(db.ClientProjects.Where(e => e.ElaqatClientId == clientId && e.IsActive == true), "Id", "ProjectName");

            return _ClientProjects;
        }
        public static IEnumerable<SelectListItem> GetProjectQueries(int projectId)
        {
            var db = new DBModelBrandWatchContainer();

            IEnumerable<SelectListItem> _Queries = new SelectList(db.ClientProjectQueries.Where(e => e.ClientProjectId == projectId && e.IsActive == true), "Id", "QuerieName");

            return _Queries;
        }
        public static IEnumerable<SelectListItem> GetProjectTags(int projectId)
        {
            var db = new DBModelBrandWatchContainer();

            IEnumerable<SelectListItem> _Tags = new SelectList(db.ProjectRuleTags.Where(e => e.ClientProjectId == projectId && e.IsActive == true), "Id", "BrandWatchRuleTagName");

            return _Tags;
        }

        public static IEnumerable<SelectListItem> GetProjectCategories(int projectId)
        {
            var db = new DBModelBrandWatchContainer();

            IEnumerable<SelectListItem> _Categories = new SelectList(db.ClientProjectCategories.Where(e => e.ClientProjectId == projectId && e.IsActive == true), "Id", "CategoryName");

            return _Categories;
        }

        public static IEnumerable<SelectListItem> GetSubCategories(int categoryId)
        {
            var db = new DBModelBrandWatchContainer();

            IEnumerable<SelectListItem> _SubCategories = new SelectList(db.ClientProjectSubCategories.Where(e => e.ClientProjectCategoryId == categoryId && e.IsActive == true), "Id", "SubCategoryName");

            return _SubCategories;
        }
        public static IEnumerable<SelectListItem> GetProjectAllSubCategories(int projectId)
        {
            var db = new DBModelBrandWatchContainer();

            IEnumerable<SelectListItem> _SubCategories = new SelectList(db.ClientProjectSubCategories.Where(e => e.ClientProjectCategory.ClientProjectId == projectId && e.IsActive == true), "Id", "SubCategoryName");

            return _SubCategories;
        }
       

        public static IEnumerable<SelectListItem> GetRuleTags(int projectId)
        {
            var db = new DBModelBrandWatchContainer();

            IEnumerable<SelectListItem> _RuleTags = new SelectList(db.ProjectRuleTags.Where(e => e.ClientProjectId == projectId && e.IsActive == true), "Id", "BrandWatchRuleTagName");

            return _RuleTags;
        }
        public static IEnumerable<SelectListItem> GetGlobalMetrics(int projectId)
        {
            var db = new DBModelBrandWatchContainer();

            IEnumerable<SelectListItem> _RuleTags = new SelectList(db.GlobalMetrics.Where(e => e.ClientProjectId == projectId && e.IsActive == true), "Id", "FilterName");

            return _RuleTags;
        }

        public static IEnumerable<SelectListItem> GetProjectMentionTypes(int projectId)
        {
            var db = new DBModelBrandWatchContainer();

            IEnumerable<SelectListItem> _MentionTypes = new SelectList(db.MentionTypes.Where(e => e.GlobalMetric.ClientProjectId == projectId && e.Active == true), "Id", "TypeOfMention");

            return _MentionTypes;
        }
        /// <summary>
        /// Adding to Mention Fillter Section
        /// </summary>
        /// <param name="mentionFilterID"></param>
        /// <param name="CategoryID"></param>
        /// <returns></returns>


        public static string AddIncludedSiteListToMentionFilter(int mentionFilterID, int siteListID)
        {
            var db = new DBModelBrandWatchContainer();
            var mentionFilter = db.MentionFilters.Find(mentionFilterID);
            var siteList = db.ClientProjectSites.Find(siteListID);
            string msg = "";
            if (siteList != null && siteList.IsActive == true)
            {
                if (mentionFilter.IncludedClientProjectSites.Any())
                {
                    foreach (var _siteList in mentionFilter.IncludedClientProjectSites)
                    {
                        if (_siteList.SiteID == siteList.Id)
                        {
                            msg = Resources.Mention.SiteListAlreadyIsAdded;
                            return msg;
                        }
                    }
                }
                IncludedClientProjectSite includedSiteList = new IncludedClientProjectSite();
                includedSiteList.SiteID = siteList.Id;
                includedSiteList.SiteName = siteList.SiteName;
                mentionFilter.IncludedClientProjectSites.Add(includedSiteList);
                db.Entry(mentionFilter).State = EntityState.Modified;
                db.SaveChanges();
                msg = Resources.Mention.SiteListNumber + ": " + includedSiteList.Id.ToString() + " - " + Resources.Mention.SiteListName + ": " + includedSiteList.SiteName + " " + Resources.Mention.HasbeenSuccessfullyAdded;


            }
            return msg;
        }

        public static string AddExcludedSiteListToMentionFilter(int mentionFilterID, int siteListID)
        {
            var db = new DBModelBrandWatchContainer();
            var mentionFilter = db.MentionFilters.Find(mentionFilterID);
            var siteList = db.ClientProjectSites.Find(siteListID);
            string msg = "";
            if (siteList != null && siteList.IsActive == true)
            {
                if (mentionFilter.ExcludedClientProjectSites.Any())
                {
                    foreach (var _siteList in mentionFilter.ExcludedClientProjectSites)
                    {
                        if (_siteList.SiteID == siteList.Id)
                        {
                            msg = Resources.Mention.SiteListAlreadyIsAdded;
                            return msg;
                        }
                    }
                }
                ExcludedClientProjectSite excludedSiteList = new ExcludedClientProjectSite();
                excludedSiteList.SiteID = siteList.Id;
                excludedSiteList.SiteName = siteList.SiteName;
                mentionFilter.ExcludedClientProjectSites.Add(excludedSiteList);
                db.Entry(mentionFilter).State = EntityState.Modified;
                db.SaveChanges();
                msg = Resources.Mention.SiteListNumber + ": " + excludedSiteList.Id.ToString() + " - " + Resources.Mention.SiteListName + ": " + excludedSiteList.SiteName + " " + Resources.Mention.HasbeenSuccessfullyAdded;


            }
            return msg;
        }


        public static string AddIncludedLanguageToMentionFilter(int mentionFilterID, int languageID)
        {
            var db = new DBModelBrandWatchContainer();
            var mentionFilter = db.MentionFilters.Find(mentionFilterID);
            var language = db.QueryLanguages.Find(languageID);
            string msg = "";
            if (language != null && language.ClientProjectQuerie.IsActive == true)
            {
                if (mentionFilter.IncludedLanguages.Any())
                {
                    foreach (var _language in mentionFilter.IncludedLanguages)
                    {
                        if (_language.LanguageID == language.Id)
                        {
                            msg = Resources.Mention.LanguageAlreadyIsAdded;
                            return msg;
                        }
                    }
                }
                IncludedLanguage includedLanguage = new IncludedLanguage();
                includedLanguage.LanguageID = language.Id;
                includedLanguage.LanguageName = language.Language;
                mentionFilter.IncludedLanguages.Add(includedLanguage);
                db.Entry(mentionFilter).State = EntityState.Modified;
                db.SaveChanges();
                msg = Resources.Mention.LanguageNumber + ": " + includedLanguage.Id.ToString() + " - " + Resources.Mention.LanguageName + ": " + includedLanguage.LanguageName + " " + Resources.Mention.HasbeenSuccessfullyAdded;


            }
            return msg;
        }
        public static string AddExcludedLanguageToMentionFilter(int mentionFilterID, int languageID)
        {
            var db = new DBModelBrandWatchContainer();
            var mentionFilter = db.MentionFilters.Find(mentionFilterID);
            var language = db.QueryLanguages.Find(languageID);
            string msg = "";
            if (language != null && language.ClientProjectQuerie.IsActive == true)
            {
                if (mentionFilter.ExcludedLanguages.Any())
                {
                    foreach (var _language in mentionFilter.ExcludedLanguages)
                    {
                        if (_language.LanguageID == language.Id)
                        {
                            msg = Resources.Mention.LanguageAlreadyIsAdded;
                            return msg;
                        }
                    }
                }
                ExcludedLanguage excludedLanguage = new ExcludedLanguage();
                excludedLanguage.LanguageID = language.Id;
                excludedLanguage.LanguageName = language.Language;
                mentionFilter.ExcludedLanguages.Add(excludedLanguage);
                db.Entry(mentionFilter).State = EntityState.Modified;
                db.SaveChanges();
                msg = Resources.Mention.LanguageNumber + ": " + excludedLanguage.Id.ToString() + " - " + Resources.Mention.LanguageName + ": " + excludedLanguage.LanguageName + " " + Resources.Mention.HasbeenSuccessfullyAdded;


            }
            return msg;
        }


        public static string AddIncludedLocationToMentionFilter(int mentionFilterID, int locationID)
        {
            var db = new DBModelBrandWatchContainer();
            var mentionFilter = db.MentionFilters.Find(mentionFilterID);
            var location = db.ProjectLocations.Find(locationID);
            string msg = "";
            if (location != null && location.IsActive == true)
            {
                if (mentionFilter.IncludedLocations.Any())
                {
                    foreach (var _location in mentionFilter.IncludedLocations)
                    {
                        if (_location.LocationID == location.Id)
                        {
                            msg = Resources.Mention.LocationIsAlreadyAdded;
                            return msg;
                        }
                    }
                }
                IncludedLocation includedLocation = new IncludedLocation();
                includedLocation.LocationID = location.Id;
                includedLocation.LocationName = location.LocationName;
                mentionFilter.IncludedLocations.Add(includedLocation);
                db.Entry(mentionFilter).State = EntityState.Modified;
                db.SaveChanges();
                msg = Resources.Mention.LocationNumber + ": " + includedLocation.Id.ToString() + " - " + Resources.Mention.LocationName + ": " + includedLocation.LocationName + " " + Resources.Mention.HasbeenSuccessfullyAdded;


            }
            return msg;
        }
        public static string AddExcludedLocationToMentionFilter(int mentionFilterID, int locationID)
        {
            var db = new DBModelBrandWatchContainer();
            var mentionFilter = db.MentionFilters.Find(mentionFilterID);
            var location = db.ProjectLocations.Find(locationID);
            string msg = "";
            if (location != null && location.IsActive == true)
            {
                if (mentionFilter.ExcludedLocations.Any())
                {
                    foreach (var _location in mentionFilter.ExcludedLocations)
                    {
                        if (_location.LocationID == location.Id)
                        {
                            msg = Resources.Mention.LocationIsAlreadyAdded;
                            return msg;
                        }
                    }
                }
                ExcludedLocation excludedLocation = new ExcludedLocation();
                excludedLocation.LocationID = location.Id;
                excludedLocation.LocationName = location.LocationName;
                mentionFilter.ExcludedLocations.Add(excludedLocation);
                db.Entry(mentionFilter).State = EntityState.Modified;
                db.SaveChanges();
                msg = Resources.Mention.LocationNumber + ": " + excludedLocation.Id.ToString() + " - " + Resources.Mention.LocationName + ": " + excludedLocation.LocationName + " " + Resources.Mention.HasbeenSuccessfullyAdded;


            }
            return msg;
        }

        public static string AddIncludedAuthorGroupToMentionFilter(int mentionFilterID, int authorGroupID)
        {
            var db = new DBModelBrandWatchContainer();
            var mentionFilter = db.MentionFilters.Find(mentionFilterID);
            var authorGroup = db.AuthorGroups.Find(authorGroupID);
            string msg = "";
            if (authorGroup != null && authorGroup.IsActive == true)
            {
                if (mentionFilter.IncludedAuthorGroups.Any())
                {
                    foreach (var _authorGroup in mentionFilter.IncludedAuthorGroups)
                    {
                        if (_authorGroup.AuthorGroupID == authorGroup.Id)
                        {
                            msg = Resources.Mention.AuthorListAlreadyIsAdded;
                            return msg;
                        }
                    }
                }
                IncludedAuthorGroup includedAuthorGroup = new IncludedAuthorGroup();
                includedAuthorGroup.AuthorGroupID = authorGroup.Id;
                includedAuthorGroup.AuthorGroupName = authorGroup.GroupName;
                mentionFilter.IncludedAuthorGroups.Add(includedAuthorGroup);
                db.Entry(mentionFilter).State = EntityState.Modified;
                db.SaveChanges();
                msg = Resources.Mention.AuthorListNumber + ": " + includedAuthorGroup.Id.ToString() + " - " + Resources.Mention.AuthorListName + ": " + includedAuthorGroup.AuthorGroupName + " " + Resources.Mention.HasbeenSuccessfullyAdded;


            }
            return msg;
        }
        public static string AddExcludedAuthorGroupToMentionFilter(int mentionFilterID, int authorGroupID)
        {
            var db = new DBModelBrandWatchContainer();
            var mentionFilter = db.MentionFilters.Find(mentionFilterID);
            var authorGroup = db.AuthorGroups.Find(authorGroupID);
            string msg = "";
            if (authorGroup != null && authorGroup.IsActive == true)
            {
                if (mentionFilter.ExcludedAuthorGroups.Any())
                {
                    foreach (var _authorGroup in mentionFilter.ExcludedAuthorGroups)
                    {
                        if (_authorGroup.AuthorGroupID == authorGroup.Id)
                        {
                            msg = Resources.Mention.AuthorListAlreadyIsAdded;
                            return msg;
                        }
                    }
                }
                ExcludedAuthorGroup excludedAuthorGroup = new ExcludedAuthorGroup();
                excludedAuthorGroup.AuthorGroupID = authorGroup.Id;
                excludedAuthorGroup.AuthorGroupName = authorGroup.GroupName;
                mentionFilter.ExcludedAuthorGroups.Add(excludedAuthorGroup);
                db.Entry(mentionFilter).State = EntityState.Modified;
                db.SaveChanges();
                msg = Resources.Mention.AuthorListNumber + ": " + excludedAuthorGroup.Id.ToString() + " - " + Resources.Mention.AuthorListName + ": " + excludedAuthorGroup.AuthorGroupName + " " + Resources.Mention.HasbeenSuccessfullyAdded;


            }
            return msg;
        }
        public static string AddIncludedCategoryToMentionFilter(int mentionFilterID, int CategoryID)
        {
            var db = new DBModelBrandWatchContainer();
            var mentionFilter = db.MentionFilters.Find(mentionFilterID);
            var category = db.ClientProjectCategories.Find(CategoryID);
            string msg = "";
            if (category != null && category.IsActive == true)
            {
                if (mentionFilter.IncludedCategories.Any())
                {
                    foreach (var cat in mentionFilter.IncludedCategories)
                    {
                        if (cat.CategoryID == category.Id)
                        {
                            msg = Resources.Mention.CategoryAlradyIsAdded;
                            return msg;
                        }
                    }
                }
                IncludedCategory includedCategory = new IncludedCategory();
                includedCategory.CategoryID = category.Id;
                includedCategory.CategoryName = category.CategoryName;
                mentionFilter.IncludedCategories.Add(includedCategory);
                db.Entry(mentionFilter).State = EntityState.Modified;
                db.SaveChanges();
                msg = Resources.Mention.CategoryNumber + ": "+ includedCategory.Id.ToString() + " - " + Resources.Projects.CategoryName + ": " + includedCategory.CategoryName + " " + Resources.Mention.HasbeenSuccessfullyAdded;


            }
            return msg;
        }
        public static string AddIncludedSubCategoryToMentionFilter(int mentionFilterID, int subCategoryID)
        {
            var db = new DBModelBrandWatchContainer();
            var mentionFilter = db.MentionFilters.Find(mentionFilterID);
            var subCategory = db.ClientProjectSubCategories.Find(subCategoryID);
            string msg = "";
            if (subCategory != null && subCategory.IsActive == true)
            {
                if (mentionFilter.IncludedSubCatgories.Any())
                {
                    foreach (var subCat in mentionFilter.IncludedSubCatgories)
                    {
                        if (subCat.SubCateID == subCategory.Id)
                        {
                            msg = Resources.Mention.SubCategoryAlradyIsAdded;
                            return msg;
                        }
                    }
                }
                IncludedSubCatgory includedSubCategory = new IncludedSubCatgory();
                includedSubCategory.SubCateID = subCategory.Id;
                includedSubCategory.SubCateName = subCategory.SubCategoryName;
                mentionFilter.IncludedSubCatgories.Add(includedSubCategory);
                db.Entry(mentionFilter).State = EntityState.Modified;
                db.SaveChanges();
                msg = Resources.Mention.SubCategoryNumber + ": " + includedSubCategory.Id.ToString() + " - " + Resources.Projects.SubCategoryName + " " + includedSubCategory.SubCateName + " " + Resources.Mention.HasbeenSuccessfullyAdded;


            }
            return msg;
        }
        public static string AddExcludedCategoryToMentionFilter(int mentionFilterID, int CategoryID)
        {
            var db = new DBModelBrandWatchContainer();
            var mentionFilter = db.MentionFilters.Find(mentionFilterID);
            var category = db.ClientProjectCategories.Find(CategoryID);
            string msg = "";
            if (category != null && category.IsActive == true)
            {
                if (mentionFilter.ExcludedCategories.Any())
                {
                    foreach (var cat in mentionFilter.ExcludedCategories)
                    {
                        if (cat.CategoryID == category.Id)
                        {
                            msg = Resources.Mention.CategoryAlradyIsAdded;
                            return msg;
                        }
                    }
                }
                ExcludedCategory excludedCategory = new ExcludedCategory();
                excludedCategory.CategoryID = category.Id;
                excludedCategory.CategoryName = category.CategoryName;
                mentionFilter.ExcludedCategories.Add(excludedCategory);
                db.Entry(mentionFilter).State = EntityState.Modified;
                db.SaveChanges();
                msg = Resources.Mention.CategoryNumber + ": " + excludedCategory.Id.ToString() + " - " + Resources.Projects.CategoryName + ": " + excludedCategory.CategoryName + " " + Resources.Mention.HasbeenSuccessfullyAdded;


            }
            return msg;
        }
        public static string AddExcludedSubCategoryToMentionFilter(int mentionFilterID, int subCategoryID)
        {
            var db = new DBModelBrandWatchContainer();
            var mentionFilter = db.MentionFilters.Find(mentionFilterID);
            var subCategory = db.ClientProjectSubCategories.Find(subCategoryID);
            string msg = "";
            if (subCategory != null && subCategory.IsActive == true)
            {
                if (mentionFilter.ExcludedSubCatgories.Any())
                {
                    foreach (var subCat in mentionFilter.ExcludedSubCatgories)
                    {
                        if (subCat.SubCateID == subCategory.Id)
                        {
                            msg = Resources.Mention.SubCategoryAlradyIsAdded;
                            return msg;
                        }
                    }
                }
                ExcludedSubCatgory excludedSubCategory = new ExcludedSubCatgory();
                excludedSubCategory.SubCateID = subCategory.Id;
                excludedSubCategory.SubCateName = subCategory.SubCategoryName;
                mentionFilter.ExcludedSubCatgories.Add(excludedSubCategory);
                db.Entry(mentionFilter).State = EntityState.Modified;
                db.SaveChanges();
                msg = Resources.Mention.SubCategoryNumber + ": " + excludedSubCategory.Id.ToString() + " - " + Resources.Projects.SubCategoryName + " " + excludedSubCategory.SubCateName + " " + Resources.Mention.HasbeenSuccessfullyAdded;

            }
            return msg;
        }
        public static string AddIncludedQueryToMentionFilter(int mentionFilterID, int queryID)
        {
            var db = new DBModelBrandWatchContainer();
            var mentionFilter = db.MentionFilters.Find(mentionFilterID);
            var projectQuery = db.ClientProjectQueries.Find(queryID);
            string msg = "";
            if (projectQuery != null && projectQuery.IsActive == true)
            {
                if (mentionFilter.IncludedQueries.Any())
                {
                    foreach (var query in mentionFilter.IncludedQueries)
                    {
                        if (query.QueryId == projectQuery.Id)
                        {
                            msg = Resources.Mention.QueryAlradyIsAdded;
                            return msg;
                        }
                    }
                }
                IncludedQuery includedQuery = new IncludedQuery();
                includedQuery.QueryId = projectQuery.Id;
                includedQuery.QueryName = projectQuery.QuerieName;
                mentionFilter.IncludedQueries.Add(includedQuery);
                db.Entry(mentionFilter).State = EntityState.Modified;
                db.SaveChanges();
                msg = Resources.Mention.QueryNumber + ": " + includedQuery.Id.ToString() + " - " + Resources.Projects.QueryName + ": " + includedQuery.QueryName + " -" + Resources.Mention.HasbeenSuccessfullyAdded;


            }
            return msg;
        }
        public static string AddIncludedTagToMentionFilter(int mentionFilterID, int tagID)
        {
            var db = new DBModelBrandWatchContainer();
            var mentionFilter = db.MentionFilters.Find(mentionFilterID);
            var projectTag = db.ProjectRuleTags.Find(tagID);
            string msg = "";
            if (projectTag != null && projectTag.IsActive == true)
            {
                if (mentionFilter.IncludedRuleTags.Any())
                {
                    foreach (var tag in mentionFilter.IncludedRuleTags)
                    {
                        if (tag.RuleTagID == projectTag.Id)
                        {
                            msg = Resources.Mention.TagAlradyIsAdded;
                            return msg;
                        }
                    }
                }
                IncludedRuleTag includedRuleTag = new IncludedRuleTag();
                includedRuleTag.RuleTagID = projectTag.Id;
                includedRuleTag.RuleTagName= projectTag.BrandWatchRuleTagName;
                mentionFilter.IncludedRuleTags.Add(includedRuleTag);
                db.Entry(mentionFilter).State = EntityState.Modified;
                db.SaveChanges();
                msg = Resources.Mention.TagNumber + ": " + includedRuleTag.Id.ToString() + " - " + Resources.Projects.RuleTagName + ": " + includedRuleTag.RuleTagName + " -" + Resources.Mention.HasbeenSuccessfullyAdded;


            }
            return msg;
        }
        public static string AddExcludedTagToMentionFilter(int mentionFilterID, int excludedTagID)
        {
            var db = new DBModelBrandWatchContainer();
            var mentionFilter = db.MentionFilters.Find(mentionFilterID);
            var projectTag = db.ProjectRuleTags.Find(excludedTagID);
            string msg = "";
            if (projectTag != null && projectTag.IsActive == true)
            {
                if (mentionFilter.ExcludedRuleTags.Any())
                {
                    foreach (var ExcludedTag in mentionFilter.ExcludedRuleTags)
                    {
                        if (ExcludedTag.RuleTagID == projectTag.Id)
                        {
                            msg = Resources.Mention.TagAlradyIsAdded;
                            return msg;
                        }
                    }
                }
                ExcludedRuleTag excludedRuleTag = new ExcludedRuleTag();
                excludedRuleTag.RuleTagID = projectTag.Id;
                excludedRuleTag.RuleTagName = projectTag.BrandWatchRuleTagName;
                mentionFilter.ExcludedRuleTags.Add(excludedRuleTag);
                db.Entry(mentionFilter).State = EntityState.Modified;
                db.SaveChanges();
                msg = Resources.Mention.TagNumber + ": " + excludedRuleTag.Id.ToString() + " - " + Resources.Projects.RuleTagName + ": " + excludedRuleTag.RuleTagName + " -" + Resources.Mention.HasbeenSuccessfullyAdded;


            }
            return msg;
        }
        public static string AddIncludedContentSourceToMentionFilter(int mentionFilterID, int contentSourceID)
        {
            var db = new DBModelBrandWatchContainer();
            var mentionFilter = db.MentionFilters.Find(mentionFilterID);
            var projectContentSource = db.QueryContentSources.Find(contentSourceID);
            string msg = "";
            if (projectContentSource != null && projectContentSource.ClientProjectQuerie.IsActive == true)
            {
                if (mentionFilter.IncludedContentSources.Any())
                {
                    foreach (var contentSource in mentionFilter.IncludedContentSources)
                    {
                        if (contentSource.CountentSourceID == projectContentSource.Id)
                        {
                            msg = Resources.Mention.ContentSourceAlradyIsAdded;
                            return msg;
                        }
                    }
                }
                IncludedContentSource includedContentSource = new IncludedContentSource();
                includedContentSource.CountentSourceID = projectContentSource.Id;
                includedContentSource.CountentSourceName = projectContentSource.SourceName;
                mentionFilter.IncludedContentSources.Add(includedContentSource);
                db.Entry(mentionFilter).State = EntityState.Modified;
                db.SaveChanges();
                msg = Resources.Mention.ContentSourceNumber + ": " + includedContentSource.Id.ToString() + " - " + Resources.Projects.QueryContentSource + ": " + includedContentSource.CountentSourceName + " -" + Resources.Mention.HasbeenSuccessfullyAdded;


            }
            return msg;
        }
        public static string AddExcludedContentSourceToMentionFilter(int mentionFilterID, int contentSourceID)
        {
            var db = new DBModelBrandWatchContainer();
            var mentionFilter = db.MentionFilters.Find(mentionFilterID);
            var projectContentSource = db.QueryContentSources.Find(contentSourceID);
            string msg = "";
            if (projectContentSource != null && projectContentSource.ClientProjectQuerie.IsActive == true)
            {
                if (mentionFilter.ExcludedContentSources.Any())
                {
                    foreach (var contentSource in mentionFilter.ExcludedContentSources)
                    {
                        if (contentSource.CountentSourceID == projectContentSource.Id)
                        {
                            msg = Resources.Mention.ContentSourceAlradyIsAdded;
                            return msg;
                        }
                    }
                }
                ExcludedContentSource excludedContentSource = new ExcludedContentSource();
                excludedContentSource.CountentSourceID = projectContentSource.Id;
                excludedContentSource.CountentSourceName = projectContentSource.SourceName;
                mentionFilter.ExcludedContentSources.Add(excludedContentSource);
                db.Entry(mentionFilter).State = EntityState.Modified;
                db.SaveChanges();
                msg = Resources.Mention.ContentSourceNumber + ": " + excludedContentSource.Id.ToString() + " - " + Resources.Projects.QueryContentSource + ": " + excludedContentSource.CountentSourceName + " -" + Resources.Mention.HasbeenSuccessfullyAdded;


            }
            return msg;
        }
        public static string AddIncludedMentionTypeToMentionFilter(int mentionFilterID, int mentionTypeID)
        {
            var db = new DBModelBrandWatchContainer();
            var mentionFilter = db.MentionFilters.Find(mentionFilterID);
            var mentionType = db.MentionTypes.Find(mentionTypeID);
            string msg = "";
            if (mentionType != null && mentionType.Active == true)
            {
                if (mentionFilter.IncludedMentionTypes.Any())
                {
                    foreach (var _mentionType in mentionFilter.IncludedMentionTypes)
                    {
                        if (_mentionType.MentionTypeID == mentionType.Id)
                        {
                            msg = Resources.Mention.MentionTypeAlradyIsAdded;
                            return msg;
                        }
                    }
                }
                IncludedMentionType includedMentionType = new IncludedMentionType();
                includedMentionType.MentionTypeID = mentionType.Id;
                includedMentionType.MentionTypeName = mentionType.TypeOfMention;
                mentionFilter.IncludedMentionTypes.Add(includedMentionType);
                db.Entry(mentionFilter).State = EntityState.Modified;
                db.SaveChanges();
                msg = Resources.Mention.MentionTypeNumber + ": " + includedMentionType.Id.ToString() + " - " + Resources.Mention.MentionType + ": " + includedMentionType.MentionTypeName + " -" + Resources.Mention.HasbeenSuccessfullyAdded;


            }
            return msg;
        }
        public static string AddExcludedMentionTypeToMentionFilter(int mentionFilterID, int mentionTypeID)
        {
            var db = new DBModelBrandWatchContainer();
            var mentionFilter = db.MentionFilters.Find(mentionFilterID);
            var mentionType = db.MentionTypes.Find(mentionTypeID);
            string msg = "";
            if (mentionType != null && mentionType.Active == true)
            {
                if (mentionFilter.ExcludedMentionTypes.Any())
                {
                    foreach (var _mentionType in mentionFilter.ExcludedMentionTypes)
                    {
                        if (_mentionType.MentionTypeID == mentionType.Id)
                        {
                            msg = Resources.Mention.MentionTypeAlradyIsAdded;
                            return msg;
                        }
                    }
                }
                ExcludedMentionType excludedMentionType = new ExcludedMentionType();
                excludedMentionType.MentionTypeID = mentionType.Id;
                excludedMentionType.MentionTypeName = mentionType.TypeOfMention;
                mentionFilter.ExcludedMentionTypes.Add(excludedMentionType);
                db.Entry(mentionFilter).State = EntityState.Modified;
                db.SaveChanges();
                msg = Resources.Mention.MentionTypeNumber + ": " + excludedMentionType.Id.ToString() + " - " + Resources.Mention.MentionType + ": " + excludedMentionType.MentionTypeName + " -" + Resources.Mention.HasbeenSuccessfullyAdded;


            }
            return msg;
        }
        public static string AddIncludedReactScoreOverAllToMentionFilter(int mentionFilterID, int reactScoreOverAllID)
        {
            var db = new DBModelBrandWatchContainer();
            var mentionFilter = db.MentionFilters.Find(mentionFilterID);
            var reactScoreOverAll = db.ReactScoreOveralls.Find(reactScoreOverAllID);
            string msg = "";
            if (reactScoreOverAll != null && reactScoreOverAll.Active == true)
            {
                if (mentionFilter.IncludedReactScoreOveralls.Any())
                {
                    foreach (var _reactScoreOverAll in mentionFilter.IncludedReactScoreOveralls)
                    {
                        if (_reactScoreOverAll.ScoreOverAllID == reactScoreOverAll.Id)
                        {
                            msg = Resources.Mention.ReactOverAllAlradyIsAdded;
                            return msg;
                        }
                    }
                }
                IncludedReactScoreOverall includedReactScoreOverall = new IncludedReactScoreOverall();
                includedReactScoreOverall.ScoreOverAllID = reactScoreOverAll.Id;
                includedReactScoreOverall.ScoreOverAllName = reactScoreOverAll.ScoreType;
                mentionFilter.IncludedReactScoreOveralls.Add(includedReactScoreOverall);
                db.Entry(mentionFilter).State = EntityState.Modified;
                db.SaveChanges();
                msg = Resources.Mention.OverAllNumber + ": " + includedReactScoreOverall.Id.ToString() + " - " + Resources.Mention.ReactScoreType + ": " + includedReactScoreOverall.ScoreOverAllName + " -" + Resources.Mention.HasbeenSuccessfullyAdded;


            }
            return msg;
        }
        public static string AddExcludedReactScoreOverAllToMentionFilter(int mentionFilterID, int reactScoreOverAllID)
        {
            var db = new DBModelBrandWatchContainer();
            var mentionFilter = db.MentionFilters.Find(mentionFilterID);
            var reactScoreOverAll = db.ReactScoreOveralls.Find(reactScoreOverAllID);
            string msg = "";
            if (reactScoreOverAll != null && reactScoreOverAll.Active == true)
            {
                if (mentionFilter.ExcludedReactScoreOveralls.Any())
                {
                    foreach (var _reactScoreOverAll in mentionFilter.ExcludedReactScoreOveralls)
                    {
                        if (_reactScoreOverAll.ScoreOverAllID == reactScoreOverAll.Id)
                        {
                            msg = Resources.Mention.ReactOverAllAlradyIsAdded;
                            return msg;
                        }
                    }
                }
                ExcludedReactScoreOverall excludedReactScoreOverall = new ExcludedReactScoreOverall();
                excludedReactScoreOverall.ScoreOverAllID = reactScoreOverAll.Id;
                excludedReactScoreOverall.ScoreOverAllName = reactScoreOverAll.ScoreType;
                mentionFilter.ExcludedReactScoreOveralls.Add(excludedReactScoreOverall);
                db.Entry(mentionFilter).State = EntityState.Modified;
                db.SaveChanges();
                msg = Resources.Mention.OverAllNumber + ": " + excludedReactScoreOverall.Id.ToString() + " - " + Resources.Mention.ReactScoreType + ": " + excludedReactScoreOverall.ScoreOverAllName + " -" + Resources.Mention.HasbeenSuccessfullyAdded;


            }
            return msg;
        }
        public static string AddIncludedReactScoreHarmToMentionFilter(int mentionFilterID, int reactScoreHarmID)
        {
            var db = new DBModelBrandWatchContainer();
            var mentionFilter = db.MentionFilters.Find(mentionFilterID);
            var reactScoreHarm = db.ReactScoreHarms.Find(reactScoreHarmID);
            string msg = "";
            if (reactScoreHarm != null && reactScoreHarm.Active == true)
            {
                if (mentionFilter.IncludedReactScoreHarms.Any())
                {
                    foreach (var _reactScoreHarm in mentionFilter.IncludedReactScoreHarms)
                    {
                        if (_reactScoreHarm.ScoreHarmID == reactScoreHarm.Id)
                        {
                            msg = Resources.Mention.ReactHarmAlradyIsAdded;
                            return msg;
                        }
                    }
                }
                IncludedReactScoreHarm includedReactScoreHarm = new IncludedReactScoreHarm();
                includedReactScoreHarm.ScoreHarmID = reactScoreHarm.Id;
                includedReactScoreHarm.ScoreHarmName = reactScoreHarm.ScoreType;
                mentionFilter.IncludedReactScoreHarms.Add(includedReactScoreHarm);
                db.Entry(mentionFilter).State = EntityState.Modified;
                db.SaveChanges();
                msg = Resources.Mention.HarmNumber + ": " + includedReactScoreHarm.Id.ToString() + " - " + Resources.Mention.ReactScoreType + ": " + includedReactScoreHarm.ScoreHarmName + " -" + Resources.Mention.HasbeenSuccessfullyAdded;


            }
            return msg;
        }
        public static string AddExcludedReactScoreHarmToMentionFilter(int mentionFilterID, int reactScoreHarmID)
        {
            var db = new DBModelBrandWatchContainer();
            var mentionFilter = db.MentionFilters.Find(mentionFilterID);
            var reactScoreHarm = db.ReactScoreHarms.Find(reactScoreHarmID);
            string msg = "";
            if (reactScoreHarm != null && reactScoreHarm.Active == true)
            {
                if (mentionFilter.ExcludedReactScoreHarms.Any())
                {
                    foreach (var _reactScoreHarm in mentionFilter.ExcludedReactScoreHarms)
                    {
                        if (_reactScoreHarm.ScoreHarmID == reactScoreHarm.Id)
                        {
                            msg = Resources.Mention.ReactHarmAlradyIsAdded;
                            return msg;
                        }
                    }
                }
                ExcludedReactScoreHarm excludedReactScoreHarm = new ExcludedReactScoreHarm();
                excludedReactScoreHarm.ScoreHarmID = reactScoreHarm.Id;
                excludedReactScoreHarm.ScoreHarmName = reactScoreHarm.ScoreType;
                mentionFilter.ExcludedReactScoreHarms.Add(excludedReactScoreHarm);
                db.Entry(mentionFilter).State = EntityState.Modified;
                db.SaveChanges();
                msg = Resources.Mention.HarmNumber + ": " + excludedReactScoreHarm.Id.ToString() + " - " + Resources.Mention.ReactScoreType + ": " + excludedReactScoreHarm.ScoreHarmName + " -" + Resources.Mention.HasbeenSuccessfullyAdded;


            }
            return msg;
        }
        public static string AddIncludedReactScoreEmotionalityToMentionFilter(int mentionFilterID, int reactScoreEmotionalityID)
        {
            var db = new DBModelBrandWatchContainer();
            var mentionFilter = db.MentionFilters.Find(mentionFilterID);
            var reactScoreEmotionality = db.ReactScoreEmotionalities.Find(reactScoreEmotionalityID);
            string msg = "";
            if (reactScoreEmotionality != null && reactScoreEmotionality.Active == true)
            {
                if (mentionFilter.IncludedReactScoreEmotionalities.Any())
                {
                    foreach (var _reactScoreEmotionality in mentionFilter.IncludedReactScoreEmotionalities)
                    {
                        if (_reactScoreEmotionality.EmotionalityID == reactScoreEmotionality.Id)
                        {
                            msg = Resources.Mention.ReactEmotionalityAlradyIsAdded;
                            return msg;
                        }
                    }
                }
                IncludedReactScoreEmotionality includedReactScoreEmotion = new IncludedReactScoreEmotionality();
                includedReactScoreEmotion.EmotionalityID = reactScoreEmotionality.Id;
                includedReactScoreEmotion.EmotionalityName = reactScoreEmotionality.ScoreType;
                mentionFilter.IncludedReactScoreEmotionalities.Add(includedReactScoreEmotion);
                db.Entry(mentionFilter).State = EntityState.Modified;
                db.SaveChanges();
                msg = Resources.Mention.EmotionalityNumber + ": " + includedReactScoreEmotion.Id.ToString() + " - " + Resources.Mention.ReactScoreType + ": " + includedReactScoreEmotion.EmotionalityName + " -" + Resources.Mention.HasbeenSuccessfullyAdded;


            }
            return msg;
        }
        public static string AddExcludedReactScoreEmotionalityToMentionFilter(int mentionFilterID, int reactScoreEmotionalityID)
        {
            var db = new DBModelBrandWatchContainer();
            var mentionFilter = db.MentionFilters.Find(mentionFilterID);
            var reactScoreEmotionality = db.ReactScoreEmotionalities.Find(reactScoreEmotionalityID);
            string msg = "";
            if (reactScoreEmotionality != null && reactScoreEmotionality.Active == true)
            {
                if (mentionFilter.ExcludedReactScoreEmotionalities.Any())
                {
                    foreach (var _reactScoreEmotionality in mentionFilter.ExcludedReactScoreEmotionalities)
                    {
                        if (_reactScoreEmotionality.EmotionalityID == reactScoreEmotionality.Id)
                        {
                            msg = Resources.Mention.ReactEmotionalityAlradyIsAdded;
                            return msg;
                        }
                    }
                }
                ExcludedReactScoreEmotionality excludedReactScoreEmotion = new ExcludedReactScoreEmotionality();
                excludedReactScoreEmotion.EmotionalityID = reactScoreEmotionality.Id;
                excludedReactScoreEmotion.EmotionalityName = reactScoreEmotionality.ScoreType;
                mentionFilter.ExcludedReactScoreEmotionalities.Add(excludedReactScoreEmotion);
                db.Entry(mentionFilter).State = EntityState.Modified;
                db.SaveChanges();
                msg = Resources.Mention.EmotionalityNumber + ": " + excludedReactScoreEmotion.Id.ToString() + " - " + Resources.Mention.ReactScoreType + ": " + excludedReactScoreEmotion.EmotionalityName + " -" + Resources.Mention.HasbeenSuccessfullyAdded;


            }
            return msg;
        }
        public static string AddIncludedSentimentToMentionFilter(int mentionFilterID, int sentimenID)
        {
            var db = new DBModelBrandWatchContainer();
            var mentionFilter = db.MentionFilters.Find(mentionFilterID);
            var sentiment = db.Sentiments.Find(sentimenID);
            string msg = "";
            if (sentiment != null && sentiment.Active == true)
            {
                if (mentionFilter.IncludedSentiments.Any())
                {
                    foreach (var _reactScoreEmotionality in mentionFilter.IncludedSentiments)
                    {
                        if (_reactScoreEmotionality.SentimentID == sentiment.Id)
                        {
                            msg = Resources.Mention.SentimentAlreadyIsAdded;
                            return msg;
                        }
                    }
                }
                IncludedSentiment includedSentiment = new IncludedSentiment();
                includedSentiment.SentimentID = sentiment.Id;
                includedSentiment.SentimentName = sentiment.BWSentiment;
                mentionFilter.IncludedSentiments.Add(includedSentiment);
                db.Entry(mentionFilter).State = EntityState.Modified;
                db.SaveChanges();
                msg = Resources.Mention.SentimentNumber + ": " + includedSentiment.Id.ToString() + " - " + Resources.Mention.Sentiment + ": " + includedSentiment.SentimentName + " -" + Resources.Mention.HasbeenSuccessfullyAdded;


            }
            return msg;
        }
        public static string AddIncludedEmotionToMentionFilter(int mentionFilterID, int emotionID)
        {
            var db = new DBModelBrandWatchContainer();
            var mentionFilter = db.MentionFilters.Find(mentionFilterID);
            var emotion = db.Emotions.Find(emotionID);
            string msg = "";
            if (emotion != null && emotion.Active == true)
            {
                if (mentionFilter.IncludedEmotions.Any())
                {
                    foreach (var _emotion in mentionFilter.IncludedEmotions)
                    {
                        if (_emotion.EmotionID == emotion.Id)
                        {
                            msg = Resources.Mention.EmotionAlreadyIsAdded;
                            return msg;
                        }
                    }
                }
                IncludedEmotion includedEmotion = new IncludedEmotion();
                includedEmotion.EmotionID = emotion.Id;
                includedEmotion.EmotionName = emotion.EmotionType;
                mentionFilter.IncludedEmotions.Add(includedEmotion);
                db.Entry(mentionFilter).State = EntityState.Modified;
                db.SaveChanges();
                msg = Resources.Mention.EmotionNumber + ": " + includedEmotion.Id.ToString() + " - " + Resources.Mention.Emotion + ": " + includedEmotion.EmotionName + " -" + Resources.Mention.HasbeenSuccessfullyAdded;


            }
            return msg;
        }
        public static string AddExcludedEmotionToMentionFilter(int mentionFilterID, int emotionID)
        {
            var db = new DBModelBrandWatchContainer();
            var mentionFilter = db.MentionFilters.Find(mentionFilterID);
            var emotion = db.Emotions.Find(emotionID);
            string msg = "";
            if (emotion != null && emotion.Active == true)
            {
                if (mentionFilter.ExcludedEmotions.Any())
                {
                    foreach (var _emotion in mentionFilter.ExcludedEmotions)
                    {
                        if (_emotion.EmotionID == emotion.Id)
                        {
                            msg = Resources.Mention.EmotionAlreadyIsAdded;
                            return msg;
                        }
                    }
                }
                ExcludedEmotion excludedEmotion = new ExcludedEmotion();
                excludedEmotion.EmotionID = emotion.Id;
                excludedEmotion.EmotionName = emotion.EmotionType;
                mentionFilter.ExcludedEmotions.Add(excludedEmotion);
                db.Entry(mentionFilter).State = EntityState.Modified;
                db.SaveChanges();
                msg = Resources.Mention.EmotionNumber + ": " + excludedEmotion.Id.ToString() + " - " + Resources.Mention.Emotion + ": " + excludedEmotion.EmotionName + " -" + Resources.Mention.HasbeenSuccessfullyAdded;


            }
            return msg;
        }

        public static string AddIncludedSocialPanalToMentionFilter(int mentionFilterID, int socialPanalID)
        {
            var db = new DBModelBrandWatchContainer();
            var mentionFilter = db.MentionFilters.Find(mentionFilterID);
            var socialPanal = db.SocialPanels.Find(socialPanalID);
            string msg = "";
            if (socialPanal != null && socialPanal.Active == true)
            {
                if (mentionFilter.IncludedSocialPanels.Any())
                {
                    foreach (var _socialPanal in mentionFilter.IncludedSocialPanels)
                    {
                        if (_socialPanal.SocialPanelID == socialPanal.Id)
                        {
                            msg = Resources.Mention.SocialPanalAlradyIsAdded;
                            return msg;
                        }
                    }
                }
                IncludedSocialPanel includedSocialPanal = new IncludedSocialPanel();
                includedSocialPanal.SocialPanelID = socialPanal.Id;
                includedSocialPanal.SocialPanelName = socialPanal.PanelName;
                mentionFilter.IncludedSocialPanels.Add(includedSocialPanal);
                db.Entry(mentionFilter).State = EntityState.Modified;
                db.SaveChanges();
                msg = Resources.Mention.SocialPanelNumber + ": " + includedSocialPanal.Id.ToString() + " - " + Resources.Mention.SocialPanelName + ": " + includedSocialPanal.SocialPanelName + " -" + Resources.Mention.HasbeenSuccessfullyAdded;


            }
            return msg;
        }
        public static string AddExcludedSocialPanalToMentionFilter(int mentionFilterID, int socialPanalID)
        {
            var db = new DBModelBrandWatchContainer();
            var mentionFilter = db.MentionFilters.Find(mentionFilterID);
            var socialPanal = db.SocialPanels.Find(socialPanalID);
            string msg = "";
            if (socialPanal != null && socialPanal.Active == true)
            {
                if (mentionFilter.ExcludedSocialPanels.Any())
                {
                    foreach (var _socialPanal in mentionFilter.ExcludedSocialPanels)
                    {
                        if (_socialPanal.SocialPanelID == socialPanal.Id)
                        {
                            msg = Resources.Mention.SocialPanalAlradyIsAdded;
                            return msg;
                        }
                    }
                }
                ExcludedSocialPanel excludedSocialPanal = new ExcludedSocialPanel();
                excludedSocialPanal.SocialPanelID = socialPanal.Id;
                excludedSocialPanal.SocialPanelName = socialPanal.PanelName;
                mentionFilter.ExcludedSocialPanels.Add(excludedSocialPanal);
                db.Entry(mentionFilter).State = EntityState.Modified;
                db.SaveChanges();
                msg = Resources.Mention.SocialPanelNumber + ": " + excludedSocialPanal.Id.ToString() + " - " + Resources.Mention.SocialPanelName + ": " + excludedSocialPanal.SocialPanelName + " -" + Resources.Mention.HasbeenSuccessfullyAdded;


            }
            return msg;
        }

        public static string AddIncludedTwitterMentionTypeToMentionFilter(int mentionFilterID, int twitterMentionTypelID)
        {
            var db = new DBModelBrandWatchContainer();
            var mentionFilter = db.MentionFilters.Find(mentionFilterID);
            var twitterMentionType = db.TwitterMentionTypes.Find(twitterMentionTypelID);
            string msg = "";
            if (twitterMentionType != null && twitterMentionType.Active == true)
            {
                if (mentionFilter.IncludedTwitterMentionTypes.Any())
                {
                    foreach (var _twitterMentionType in mentionFilter.IncludedTwitterMentionTypes)
                    {
                        if (_twitterMentionType.TwitterMentionTypeID == twitterMentionType.Id)
                        {
                            msg = Resources.Mention.MentionTypeAlradyIsAdded;
                            return msg;
                        }
                    }
                }
                IncludedTwitterMentionType includedTwitterMentionType = new IncludedTwitterMentionType();
                includedTwitterMentionType.TwitterMentionTypeID = twitterMentionType.Id;
                includedTwitterMentionType.TwitterMentionTypeName = twitterMentionType.MentionTypeOfTwitter;
                mentionFilter.IncludedTwitterMentionTypes.Add(includedTwitterMentionType);
                db.Entry(mentionFilter).State = EntityState.Modified;
                db.SaveChanges();
                msg = Resources.Mention.MentionTypeNumber + ": " + includedTwitterMentionType.Id.ToString() + " - " + Resources.Mention.MentionType + ": " + includedTwitterMentionType.TwitterMentionTypeName + " -" + Resources.Mention.HasbeenSuccessfullyAdded;


            }
            return msg;
        }
        public static string AddExcludedTwitterMentionTypeToMentionFilter(int mentionFilterID, int twitterMentionTypelID)
        {
            var db = new DBModelBrandWatchContainer();
            var mentionFilter = db.MentionFilters.Find(mentionFilterID);
            var twitterMentionType = db.TwitterMentionTypes.Find(twitterMentionTypelID);
            string msg = "";
            if (twitterMentionType != null && twitterMentionType.Active == true)
            {
                if (mentionFilter.ExcludedTwitterMentionTypes.Any())
                {
                    foreach (var _twitterMentionType in mentionFilter.ExcludedTwitterMentionTypes)
                    {
                        if (_twitterMentionType.TwitterMentionTypeID == twitterMentionType.Id)
                        {
                            msg = Resources.Mention.MentionTypeAlradyIsAdded;
                            return msg;
                        }
                    }
                }
                ExcludedTwitterMentionType excludedTwitterMentionType = new ExcludedTwitterMentionType();
                excludedTwitterMentionType.TwitterMentionTypeID = twitterMentionType.Id;
                excludedTwitterMentionType.TwitterMentionTypeName = twitterMentionType.MentionTypeOfTwitter;
                mentionFilter.ExcludedTwitterMentionTypes.Add(excludedTwitterMentionType);
                db.Entry(mentionFilter).State = EntityState.Modified;
                db.SaveChanges();
                msg = Resources.Mention.MentionTypeNumber + ": " + excludedTwitterMentionType.Id.ToString() + " - " + Resources.Mention.MentionType + ": " + excludedTwitterMentionType.TwitterMentionTypeName + " -" + Resources.Mention.HasbeenSuccessfullyAdded;


            }
            return msg;
        }

        public static string AddIncludedTwitterVerificationTypeToMentionFilter(int mentionFilterID, int twitterVerificationTypelID)
        {
            var db = new DBModelBrandWatchContainer();
            var mentionFilter = db.MentionFilters.Find(mentionFilterID);
            var twitterVerificationType = db.TwitterVerificationTypes.Find(twitterVerificationTypelID);
            string msg = "";
            if (twitterVerificationType != null && twitterVerificationType.Active == true)
            {
                if (mentionFilter.IncludedTwitterVerificationTypes.Any())
                {
                    foreach (var _twitterVerificationType in mentionFilter.IncludedTwitterVerificationTypes)
                    {
                        if (_twitterVerificationType.TwitterVerificationTypeID == twitterVerificationType.Id)
                        {
                            msg = Resources.Mention.TwitterVerificationTypeAlreadyIsAdded;
                            return msg;
                        }
                    }
                }
                IncludedTwitterVerificationType includedTwitterVerificationType = new IncludedTwitterVerificationType();
                includedTwitterVerificationType.TwitterVerificationTypeID = twitterVerificationType.Id;
                includedTwitterVerificationType.TwitterVerificationTypeName = twitterVerificationType.VerificationType;
                mentionFilter.IncludedTwitterVerificationTypes.Add(includedTwitterVerificationType);
                db.Entry(mentionFilter).State = EntityState.Modified;
                db.SaveChanges();
                msg = Resources.Mention.TwitterVerificationTypeNumber + ": " + includedTwitterVerificationType.Id.ToString() + " - " + Resources.Mention.TwitterVerificationTypeName  + ": " + includedTwitterVerificationType.TwitterVerificationTypeName + " -" + Resources.Mention.HasbeenSuccessfullyAdded;


            }
            return msg;
        }
        public static string AddExcludedTwitterVerificationTypeToMentionFilter(int mentionFilterID, int twitterVerificationTypelID)
        {
            var db = new DBModelBrandWatchContainer();
            var mentionFilter = db.MentionFilters.Find(mentionFilterID);
            var twitterVerificationType = db.TwitterVerificationTypes.Find(twitterVerificationTypelID);
            string msg = "";
            if (twitterVerificationType != null && twitterVerificationType.Active == true)
            {
                if (mentionFilter.ExcludedTwitterVerificationTypes.Any())
                {
                    foreach (var _twitterVerificationType in mentionFilter.ExcludedTwitterVerificationTypes)
                    {
                        if (_twitterVerificationType.TwitterVerificationTypeID == twitterVerificationType.Id)
                        {
                            msg = Resources.Mention.TwitterVerificationTypeAlreadyIsAdded;
                            return msg;
                        }
                    }
                }
                ExcludedTwitterVerificationType excludedTwitterVerificationType = new ExcludedTwitterVerificationType();
                excludedTwitterVerificationType.TwitterVerificationTypeID = twitterVerificationType.Id;
                excludedTwitterVerificationType.TwitterVerificationTypeName = twitterVerificationType.VerificationType;
                mentionFilter.ExcludedTwitterVerificationTypes.Add(excludedTwitterVerificationType);
                db.Entry(mentionFilter).State = EntityState.Modified;
                db.SaveChanges();
                msg = Resources.Mention.TwitterVerificationTypeNumber + ": " + excludedTwitterVerificationType.Id.ToString() + " - " + Resources.Mention.TwitterVerificationTypeName + ": " + excludedTwitterVerificationType.TwitterVerificationTypeName + " -" + Resources.Mention.HasbeenSuccessfullyAdded;


            }
            return msg;
        }


        public static string AddIncludedFaceBookMentionTypeToMentionFilter(int mentionFilterID, int faceBookMentionTypelID)
        {
            var db = new DBModelBrandWatchContainer();
            var mentionFilter = db.MentionFilters.Find(mentionFilterID);
            var faceBookMentionType = db.FaceBookMentionTypes.Find(faceBookMentionTypelID);
            string msg = "";
            if (faceBookMentionType != null && faceBookMentionType.Active == true)
            {
                if (mentionFilter.IncludedFaceBookMentionTypes.Any())
                {
                    foreach (var _faceBookMentionType in mentionFilter.IncludedFaceBookMentionTypes)
                    {
                        if (_faceBookMentionType.FaceBookMentionTypeID == faceBookMentionType.Id)
                        {
                            msg = Resources.Mention.MentionTypeAlradyIsAdded;
                            return msg;
                        }
                    }
                }
                IncludedFaceBookMentionType includedFaceBookMentionType = new IncludedFaceBookMentionType();
                includedFaceBookMentionType.FaceBookMentionTypeID = faceBookMentionType.Id;
                includedFaceBookMentionType.FaceBookMentionTypeName = faceBookMentionType.MentionTypeOfFaceBook;
                mentionFilter.IncludedFaceBookMentionTypes.Add(includedFaceBookMentionType);
                db.Entry(mentionFilter).State = EntityState.Modified;
                db.SaveChanges();
                msg = Resources.Mention.MentionTypeNumber + ": " + includedFaceBookMentionType.Id.ToString() + " - " + Resources.Mention.MentionType + ": " + includedFaceBookMentionType.FaceBookMentionTypeName + " -" + Resources.Mention.HasbeenSuccessfullyAdded;


            }
            return msg;
        }
        public static string AddExcludedFaceBookMentionTypeToMentionFilter(int mentionFilterID, int faceBookMentionTypelID)
        {
            var db = new DBModelBrandWatchContainer();
            var mentionFilter = db.MentionFilters.Find(mentionFilterID);
            var faceBookMentionType = db.FaceBookMentionTypes.Find(faceBookMentionTypelID);
            string msg = "";
            if (faceBookMentionType != null && faceBookMentionType.Active == true)
            {
                if (mentionFilter.ExcludedFaceBookMentionTypes.Any())
                {
                    foreach (var _faceBookMentionType in mentionFilter.ExcludedFaceBookMentionTypes)
                    {
                        if (_faceBookMentionType.FaceBookMentionTypeID == faceBookMentionType.Id)
                        {
                            msg = Resources.Mention.MentionTypeAlradyIsAdded;
                            return msg;
                        }
                    }
                }
                ExcludedFaceBookMentionType excludedFaceBookMentionType = new ExcludedFaceBookMentionType();
                excludedFaceBookMentionType.FaceBookMentionTypeID = faceBookMentionType.Id;
                excludedFaceBookMentionType.FaceBookMentionTypeName = faceBookMentionType.MentionTypeOfFaceBook;
                mentionFilter.ExcludedFaceBookMentionTypes.Add(excludedFaceBookMentionType);
                db.Entry(mentionFilter).State = EntityState.Modified;
                db.SaveChanges();
                msg = Resources.Mention.MentionTypeNumber + ": " + excludedFaceBookMentionType.Id.ToString() + " - " + Resources.Mention.MentionType + ": " + excludedFaceBookMentionType.FaceBookMentionTypeName + " -" + Resources.Mention.HasbeenSuccessfullyAdded;


            }
            return msg;
        }

        public static string AddIncludedInstagramMentionTypeToMentionFilter(int mentionFilterID, int instagramMentionTypelID)
        {
            var db = new DBModelBrandWatchContainer();
            var mentionFilter = db.MentionFilters.Find(mentionFilterID);
            var instagramMentionType = db.InstagramMentionTypes.Find(instagramMentionTypelID);
            string msg = "";
            if (instagramMentionType != null && instagramMentionType.Active == true)
            {
                if (mentionFilter.IncludedInstagramMentionTypes.Any())
                {
                    foreach (var _instagramMentionType in mentionFilter.IncludedInstagramMentionTypes)
                    {
                        if (_instagramMentionType.InstagramMentionTypeID == instagramMentionType.Id)
                        {
                            msg = Resources.Mention.MentionTypeAlradyIsAdded;
                            return msg;
                        }
                    }
                }
                IncludedInstagramMentionType includedInstagramMentionType = new IncludedInstagramMentionType();
                includedInstagramMentionType.InstagramMentionTypeID = instagramMentionType.Id;
                includedInstagramMentionType.InstagramMentionTypeName = instagramMentionType.MentionTypeOfInstgram;
                mentionFilter.IncludedInstagramMentionTypes.Add(includedInstagramMentionType);
                db.Entry(mentionFilter).State = EntityState.Modified;
                db.SaveChanges();
                msg = Resources.Mention.MentionTypeNumber + ": " + includedInstagramMentionType.Id.ToString() + " - " + Resources.Mention.MentionType + ": " + includedInstagramMentionType.InstagramMentionTypeName + " -" + Resources.Mention.HasbeenSuccessfullyAdded;


            }
            return msg;
        }

        public static string AddExcludedInstagramMentionTypeToMentionFilter(int mentionFilterID, int instagramMentionTypelID)
        {
            var db = new DBModelBrandWatchContainer();
            var mentionFilter = db.MentionFilters.Find(mentionFilterID);
            var exstagramMentionType = db.InstagramMentionTypes.Find(instagramMentionTypelID);
            string msg = "";
            if (exstagramMentionType != null && exstagramMentionType.Active == true)
            {
                if (mentionFilter.ExcludedInstagramMentionTypes.Any())
                {
                    foreach (var _instagramMentionType in mentionFilter.ExcludedInstagramMentionTypes)
                    {
                        if (_instagramMentionType.InstagramMentionTypeID == exstagramMentionType.Id)
                        {
                            msg = Resources.Mention.MentionTypeAlradyIsAdded;
                            return msg;
                        }
                    }
                }
                ExcludedInstagramMentionType excludedInstagramMentionType = new ExcludedInstagramMentionType();
                excludedInstagramMentionType.InstagramMentionTypeID = exstagramMentionType.Id;
                excludedInstagramMentionType.InstagramMentionTypeName = exstagramMentionType.MentionTypeOfInstgram;
                mentionFilter.ExcludedInstagramMentionTypes.Add(excludedInstagramMentionType);
                db.Entry(mentionFilter).State = EntityState.Modified;
                db.SaveChanges();
                msg = Resources.Mention.MentionTypeNumber + ": " + excludedInstagramMentionType.Id.ToString() + " - " + Resources.Mention.MentionType + ": " + excludedInstagramMentionType.InstagramMentionTypeName + " -" + Resources.Mention.HasbeenSuccessfullyAdded;


            }
            return msg;
        }


        public static string AddIncludedSubredditTopicToMentionFilter(int mentionFilterID, int subredditTopiclID)
        {
            var db = new DBModelBrandWatchContainer();
            var mentionFilter = db.MentionFilters.Find(mentionFilterID);
            var subredditTopic = db.SubredditTopics.Find(subredditTopiclID);
            string msg = "";
            if (subredditTopic != null && subredditTopic.Active == true)
            {
                if (mentionFilter.IncludedSubredditTopics.Any())
                {
                    foreach (var _subredditTopic in mentionFilter.IncludedSubredditTopics)
                    {
                        if (_subredditTopic.SubredditTopicID == subredditTopic.Id)
                        {
                            msg = Resources.Mention.SubredditTopicAlreadyAdded;
                            return msg;
                        }
                    }
                }
                IncludedSubredditTopic includedSubredditTopic = new IncludedSubredditTopic();
                includedSubredditTopic.SubredditTopicID = subredditTopic.Id;
                includedSubredditTopic.SubredditTopicName = subredditTopic.TopicName;
                mentionFilter.IncludedSubredditTopics.Add(includedSubredditTopic);
                db.Entry(mentionFilter).State = EntityState.Modified;
                db.SaveChanges();
                msg = Resources.Mention.SubredditTopicNumber + ": " + includedSubredditTopic.Id.ToString() + " - " + Resources.Mention.SubredditTopicName + ": " + includedSubredditTopic.SubredditTopicName + " -" + Resources.Mention.HasbeenSuccessfullyAdded;


            }
            return msg;
        }
        public static string AddExcludedSubredditTopicToMentionFilter(int mentionFilterID, int subredditTopiclID)
        {
            var db = new DBModelBrandWatchContainer();
            var mentionFilter = db.MentionFilters.Find(mentionFilterID);
            var subredditTopic = db.SubredditTopics.Find(subredditTopiclID);
            string msg = "";
            if (subredditTopic != null && subredditTopic.Active == true)
            {
                if (mentionFilter.ExcludedSubredditTopics.Any())
                {
                    foreach (var _subredditTopic in mentionFilter.ExcludedSubredditTopics)
                    {
                        if (_subredditTopic.SubredditTopicID == subredditTopic.Id)
                        {
                            msg = Resources.Mention.SubredditTopicAlreadyAdded;
                            return msg;
                        }
                    }
                }
                ExcludedSubredditTopic excludedSubredditTopic = new ExcludedSubredditTopic();
                excludedSubredditTopic.SubredditTopicID = subredditTopic.Id;
                excludedSubredditTopic.SubredditTopicName = subredditTopic.TopicName;
                mentionFilter.ExcludedSubredditTopics.Add(excludedSubredditTopic);
                db.Entry(mentionFilter).State = EntityState.Modified;
                db.SaveChanges();
                msg = Resources.Mention.SubredditTopicNumber + ": " + excludedSubredditTopic.Id.ToString() + " - " + Resources.Mention.SubredditTopicName + ": " + excludedSubredditTopic.SubredditTopicName + " -" + Resources.Mention.HasbeenSuccessfullyAdded;


            }
            return msg;
        }

        public static string AddIncludedTwitterAccountTypeToMentionFilter(int mentionFilterID, int twitterAccountTypeID)
        {
            var db = new DBModelBrandWatchContainer();
            var mentionFilter = db.MentionFilters.Find(mentionFilterID);
            var accountType = db.AccountTypes.Find(twitterAccountTypeID);
            string msg = "";
            if (accountType != null && accountType.Active == true)
            {
                if (mentionFilter.IncludedTwitterAccountTypes.Any())
                {
                    foreach (var _twitterAccountType in mentionFilter.IncludedTwitterAccountTypes)
                    {
                        if (_twitterAccountType.TwitterAccountTypeID == accountType.Id)
                        {
                            msg = Resources.Mention.AccountTypeAlreadyAdded;
                            return msg;
                        }
                    }
                }
                IncludedTwitterAccountTypes includedTwitterAccountType = new IncludedTwitterAccountTypes();
                includedTwitterAccountType.TwitterAccountTypeID = accountType.Id;
                includedTwitterAccountType.TwitterAccountTypeName = accountType.BWAccountType;
                mentionFilter.IncludedTwitterAccountTypes.Add(includedTwitterAccountType);
                db.Entry(mentionFilter).State = EntityState.Modified;
                db.SaveChanges();
                msg = Resources.Mention.AccountTypeNumber + ": " + includedTwitterAccountType.Id.ToString() + " - " + Resources.Mention.AccountTypeName + ": " + includedTwitterAccountType.TwitterAccountTypeName + " -" + Resources.Mention.HasbeenSuccessfullyAdded;


            }
            return msg;
        }
        public static string AddExcludedTwitterAccountTypeToMentionFilter(int mentionFilterID, int twitterAccountTypeID)
        {
            var db = new DBModelBrandWatchContainer();
            var mentionFilter = db.MentionFilters.Find(mentionFilterID);
            var accountType = db.AccountTypes.Find(twitterAccountTypeID);
            string msg = "";
            if (accountType != null && accountType.Active == true)
            {
                if (mentionFilter.ExcludedTwitterAccountTypes.Any())
                {
                    foreach (var _twitterAccountType in mentionFilter.ExcludedTwitterAccountTypes)
                    {
                        if (_twitterAccountType.TwitterAccountTypeID == accountType.Id)
                        {
                            msg = Resources.Mention.AccountTypeAlreadyAdded;
                            return msg;
                        }
                    }
                }
                ExcludedTwitterAccountTypes excludedTwitterAccountType = new ExcludedTwitterAccountTypes();
                excludedTwitterAccountType.TwitterAccountTypeID = accountType.Id;
                excludedTwitterAccountType.TwitterAccountTypeName = accountType.BWAccountType;
                mentionFilter.ExcludedTwitterAccountTypes.Add(excludedTwitterAccountType);
                db.Entry(mentionFilter).State = EntityState.Modified;
                db.SaveChanges();
                msg = Resources.Mention.AccountTypeNumber + ": " + excludedTwitterAccountType.Id.ToString() + " - " + Resources.Mention.AccountTypeName + ": " + excludedTwitterAccountType.TwitterAccountTypeName + " -" + Resources.Mention.HasbeenSuccessfullyAdded;


            }
            return msg;
        }

        public static string AddIncludedTwitterGenderToMentionFilter(int mentionFilterID, int twitterGenderID)
        {
            var db = new DBModelBrandWatchContainer();
            var mentionFilter = db.MentionFilters.Find(mentionFilterID);
            var gender = db.Genders.Find(twitterGenderID);
            string msg = "";
            if (gender != null && gender.Active == true)
            {
                if (mentionFilter.IncludedTwitterGenders.Any())
                {
                    foreach (var _twitterGender in mentionFilter.IncludedTwitterGenders)
                    {
                        if (_twitterGender.GenderID == gender.Id)
                        {
                            msg = Resources.Mention.TwitterGenderAlreadyIsAdded;
                            return msg;
                        }
                    }
                }
                IncludedTwitterGender includedTwitterGender = new IncludedTwitterGender();
                includedTwitterGender.GenderID = gender.Id;
                includedTwitterGender.GenderName = gender.BWGender;
                mentionFilter.IncludedTwitterGenders.Add(includedTwitterGender);
                db.Entry(mentionFilter).State = EntityState.Modified;
                db.SaveChanges();
                msg = Resources.Mention.TwitterGenderNumber + ": " + includedTwitterGender.Id.ToString() + " - " + Resources.Mention.TwitterGenderName + ": " + includedTwitterGender.GenderName + " -" + Resources.Mention.HasbeenSuccessfullyAdded;


            }
            return msg;
        }

        public static string AddIncludedTwitterProfessionToMentionFilter(int mentionFilterID, int twitterProfessionID)
        {
            var db = new DBModelBrandWatchContainer();
            var mentionFilter = db.MentionFilters.Find(mentionFilterID);
            var twitterProfession = db.TwitterProfessions.Find(twitterProfessionID);
            string msg = "";
            if (twitterProfession != null && twitterProfession.Active == true)
            {
                if (mentionFilter.IncludedTwitterProfessions.Any())
                {
                    foreach (var _twitterProfession in mentionFilter.IncludedTwitterProfessions)
                    {
                        if (_twitterProfession.ProfessionID == twitterProfession.Id)
                        {
                            msg = Resources.Mention.TwitterProfessionIsAlreadyAdded;
                            return msg;
                        }
                    }
                }
                IncludedTwitterProfession includedTwitterProfession = new IncludedTwitterProfession();
                includedTwitterProfession.ProfessionID = twitterProfession.Id;
                includedTwitterProfession.ProfessionName = twitterProfession.Profession;
                mentionFilter.IncludedTwitterProfessions.Add(includedTwitterProfession);
                db.Entry(mentionFilter).State = EntityState.Modified;
                db.SaveChanges();
                msg = Resources.Mention.TwitterProfessionNumber + ": " + includedTwitterProfession.Id.ToString() + " - " + Resources.Mention.TwitterProfessionName + ": " + includedTwitterProfession.ProfessionName + " -" + Resources.Mention.HasbeenSuccessfullyAdded;


            }
            return msg;
        }
        public static string AddExcludedTwitterProfessionToMentionFilter(int mentionFilterID, int twitterProfessionID)
        {
            var db = new DBModelBrandWatchContainer();
            var mentionFilter = db.MentionFilters.Find(mentionFilterID);
            var twitterProfession = db.TwitterProfessions.Find(twitterProfessionID);
            string msg = "";
            if (twitterProfession != null && twitterProfession.Active == true)
            {
                if (mentionFilter.ExcludedTwitterProfessions.Any())
                {
                    foreach (var _twitterProfession in mentionFilter.ExcludedTwitterProfessions)
                    {
                        if (_twitterProfession.ProfessionID == twitterProfession.Id)
                        {
                            msg = Resources.Mention.TwitterProfessionIsAlreadyAdded;
                            return msg;
                        }
                    }
                }
                ExcludedTwitterProfession excludedTwitterProfession = new ExcludedTwitterProfession();
                excludedTwitterProfession.ProfessionID = twitterProfession.Id;
                excludedTwitterProfession.ProfessionName = twitterProfession.Profession;
                mentionFilter.ExcludedTwitterProfessions.Add(excludedTwitterProfession);
                db.Entry(mentionFilter).State = EntityState.Modified;
                db.SaveChanges();
                msg = Resources.Mention.TwitterProfessionNumber + ": " + excludedTwitterProfession.Id.ToString() + " - " + Resources.Mention.TwitterProfessionName + ": " + excludedTwitterProfession.ProfessionName + " -" + Resources.Mention.HasbeenSuccessfullyAdded;


            }
            return msg;
        }


        public static string AddIncludedTwitterInterestToMentionFilter(int mentionFilterID, int twitterInterestID)
        {
            var db = new DBModelBrandWatchContainer();
            var mentionFilter = db.MentionFilters.Find(mentionFilterID);
            var twitterInterest = db.TwitterInterests.Find(twitterInterestID);
            string msg = "";
            if (twitterInterest != null && twitterInterest.Active == true)
            {
                if (mentionFilter.IncludedTwitterInterests.Any())
                {
                    foreach (var _twitterInterest in mentionFilter.IncludedTwitterInterests)
                    {
                        if (_twitterInterest.InterestID == twitterInterest.Id)
                        {
                            msg = Resources.Mention.TwitterInterestAlreadyIsAdded;
                            return msg;
                        }
                    }
                }
                IncludedTwitterInterest includedTwitterInterest = new IncludedTwitterInterest();
                includedTwitterInterest.InterestID = twitterInterest.Id;
                includedTwitterInterest.InterestName = twitterInterest.Interest;
                mentionFilter.IncludedTwitterInterests.Add(includedTwitterInterest);
                db.Entry(mentionFilter).State = EntityState.Modified;
                db.SaveChanges();
                msg = Resources.Mention.TwitterInterestNumber + ": " + includedTwitterInterest.Id.ToString() + " - " + Resources.Mention.TwitterInterestName + ": " + includedTwitterInterest.InterestName + " -" + Resources.Mention.HasbeenSuccessfullyAdded;


            }
            return msg;
        }

        public static string AddExcludedTwitterInterestToMentionFilter(int mentionFilterID, int twitterInterestID)
        {
            var db = new DBModelBrandWatchContainer();
            var mentionFilter = db.MentionFilters.Find(mentionFilterID);
            var twitterInterest = db.TwitterInterests.Find(twitterInterestID);
            string msg = "";
            if (twitterInterest != null && twitterInterest.Active == true)
            {
                if (mentionFilter.ExcludedTwitterInterests.Any())
                {
                    foreach (var _twitterInterest in mentionFilter.ExcludedTwitterInterests)
                    {
                        if (_twitterInterest.InterestID == twitterInterest.Id)
                        {
                            msg = Resources.Mention.TwitterInterestAlreadyIsAdded;
                            return msg;
                        }
                    }
                }
                ExcludedTwitterInterest excludedTwitterInterest = new ExcludedTwitterInterest();
                excludedTwitterInterest.InterestID = twitterInterest.Id;
                excludedTwitterInterest.InterestName = twitterInterest.Interest;
                mentionFilter.ExcludedTwitterInterests.Add(excludedTwitterInterest);
                db.Entry(mentionFilter).State = EntityState.Modified;
                db.SaveChanges();
                msg = Resources.Mention.TwitterInterestNumber + ": " + excludedTwitterInterest.Id.ToString() + " - " + Resources.Mention.TwitterInterestName + ": " + excludedTwitterInterest.InterestName + " -" + Resources.Mention.HasbeenSuccessfullyAdded;


            }
            return msg;
        }
        public static string AddIncludedDomainToMentionFilter(int mentionFilterID, int domainID)
        {
            var db = new DBModelBrandWatchContainer();
            var mentionFilter = db.MentionFilters.Find(mentionFilterID);
            var domain = db.NewsBarDomains.Find(domainID);
            string msg = "";
            if (domain != null)
            {
                if (mentionFilter.IncludedDomains.Any())
                {
                    foreach (var _domain in mentionFilter.IncludedDomains)
                    {
                        if (_domain.DomainID == domain.Id)
                        {
                            msg = Resources.Mention.DomainAlreadyIsAdded;
                            return msg;
                        }
                    }
                }
                IncludedDomains includedDomain = new IncludedDomains();
                includedDomain.DomainID = domain.Id;
                includedDomain.Domain = domain.Domain;
                includedDomain.Favicon = domain.Favicon;
                mentionFilter.IncludedDomains.Add(includedDomain);
                db.Entry(mentionFilter).State = EntityState.Modified;
                db.SaveChanges();
                msg = Resources.Mention.DomainNumber + ": " + includedDomain.Id.ToString() + " - " + Resources.Mention.DomainName + ": " + includedDomain.Domain + " -" + Resources.Mention.HasbeenSuccessfullyAdded;


            }
            return msg;
        }

        public static string AddIncludedSubTypeToMentionFilter(int mentionFilterID, int subTypeID)
        {
            var db = new DBModelBrandWatchContainer();
            var mentionFilter = db.MentionFilters.Find(mentionFilterID);
            var subType = db.SubTypes.Find(subTypeID);
            string msg = "";
            if (subType != null && subType.Active == true)
            {
                if (mentionFilter.IncludedSubTypes.Any())
                {
                    foreach (var _subType in mentionFilter.IncludedSubTypes)
                    {
                        if (_subType.SourceTypeID == subType.Id)
                        {
                            msg = Resources.Mention.SubTypeAlreadyIsAdded;
                            return msg;
                        }
                    }
                }
                IncludedSubType includedSubType = new IncludedSubType();
                includedSubType.SourceTypeID = subType.Id;
                includedSubType.SourceTypeName = subType.BWSubType;
                mentionFilter.IncludedSubTypes.Add(includedSubType);
                db.Entry(mentionFilter).State = EntityState.Modified;
                db.SaveChanges();
                msg = Resources.Mention.SubTypeNumber + ": " + includedSubType.Id.ToString() + " - " + Resources.Mention.SubTypeName + ": " + includedSubType.SourceTypeName + " -" + Resources.Mention.HasbeenSuccessfullyAdded;


            }
            return msg;
        }

        public static string AddExcludedSubTypeToMentionFilter(int mentionFilterID, int subTypeID)
        {
            var db = new DBModelBrandWatchContainer();
            var mentionFilter = db.MentionFilters.Find(mentionFilterID);
            var subType = db.SubTypes.Find(subTypeID);
            string msg = "";
            if (subType != null && subType.Active == true)
            {
                if (mentionFilter.ExcludedSubTypes.Any())
                {
                    foreach (var _subType in mentionFilter.ExcludedSubTypes)
                    {
                        if (_subType.SourceTypeID == subType.Id)
                        {
                            msg = Resources.Mention.SubTypeAlreadyIsAdded;
                            return msg;
                        }
                    }
                }
                ExcludedSubType excludedSubType = new ExcludedSubType();
                excludedSubType.SourceTypeID = subType.Id;
                excludedSubType.SourceTypeName = subType.BWSubType;
                mentionFilter.ExcludedSubTypes.Add(excludedSubType);
                db.Entry(mentionFilter).State = EntityState.Modified;
                db.SaveChanges();
                msg = Resources.Mention.SubTypeNumber + ": " + excludedSubType.Id.ToString() + " - " + Resources.Mention.SubTypeName + ": " + excludedSubType.SourceTypeName + " -" + Resources.Mention.HasbeenSuccessfullyAdded;


            }
            return msg;
        }


        /// <summary>
        /// Deleteing From Mention Fillter Section
        /// </summary>
        /// <param name="mentionFilterID"></param>
        /// <param name="CategoryID"></param>
        /// <returns></returns>

        public static string DeleteIncludedDomainFromMentionFilter(int includedDomainID)
        {
            var db = new DBModelBrandWatchContainer();
            var Domain = db.IncludedDomains.Find(includedDomainID);
            string msg = "";
            if (Domain != null)
            {
                msg = Resources.Mention.DomainNumber + ": " + Domain.Id.ToString() + " - " + Resources.Mention.DomainName + " " + Domain.Domain + " " + Resources.Mention.HasbeenSuccessfullyDeleted;
                db.IncludedDomains.Remove(Domain);
                db.SaveChanges();
            }
            return msg;
        }
        public static string DeleteIncludedSubTypeFromMentionFilter(int includedSubTypeID)
        {
            var db = new DBModelBrandWatchContainer();
            var SubType = db.IncludedSubTypes.Find(includedSubTypeID);
            string msg = "";
            if (SubType != null)
            {
                msg = Resources.Mention.SubTypeNumber + ": " + SubType.Id.ToString() + " - " + Resources.Mention.SubTypeName + " " + SubType.SourceTypeName + " " + Resources.Mention.HasbeenSuccessfullyDeleted;
                db.IncludedSubTypes.Remove(SubType);
                db.SaveChanges();
            }
            return msg;
        }

        public static string DeleteExcludedSubTypeFromMentionFilter(int excludedSubTypeID)
        {
            var db = new DBModelBrandWatchContainer();
            var SubType = db.ExcludedSubTypes.Find(excludedSubTypeID);
            string msg = "";
            if (SubType != null)
            {
                msg = Resources.Mention.SubTypeNumber + ": " + SubType.Id.ToString() + " - " + Resources.Mention.SubTypeName + " " + SubType.SourceTypeName + " " + Resources.Mention.HasbeenSuccessfullyDeleted;
                db.ExcludedSubTypes.Remove(SubType);
                db.SaveChanges();
            }
            return msg;
        }


        public static string DeleteIncludedTwitterGenderFromMentionFilter(int includedTwitterGenderID)
        {
            var db = new DBModelBrandWatchContainer();
            var includedTwitterGender = db.IncludedTwitterGenders.Find(includedTwitterGenderID);
            string msg = "";
            if (includedTwitterGender != null)
            {
                msg = Resources.Mention.TwitterGenderNumber + ": " + includedTwitterGender.Id.ToString() + " - " + Resources.Mention.TwitterGenderName + " " + includedTwitterGender.GenderName + " " + Resources.Mention.HasbeenSuccessfullyDeleted;
                db.IncludedTwitterGenders.Remove(includedTwitterGender);
                db.SaveChanges();
            }
            return msg;
        }

        public static string DeleteIncludedTwitterAccountTypeFromMentionFilter(int includedTwitterAccountTypeID)
        {
            var db = new DBModelBrandWatchContainer();
            var includedTwitterAccountType = db.IncludedTwitterAccountTypes.Find(includedTwitterAccountTypeID);
            string msg = "";
            if (includedTwitterAccountType != null)
            {
                msg = Resources.Mention.AccountTypeNumber + ": " + includedTwitterAccountType.Id.ToString() + " - " + Resources.Mention.AccountTypeName + " " + includedTwitterAccountType.TwitterAccountTypeName + " " + Resources.Mention.HasbeenSuccessfullyDeleted;
                db.IncludedTwitterAccountTypes.Remove(includedTwitterAccountType);
                db.SaveChanges();
            }
            return msg;
        }
        public static string DeleteExcludedTwitterAccountTypeFromMentionFilter(int excludedTwitterAccountTypeID)
        {
            var db = new DBModelBrandWatchContainer();
            var excludedTwitterAccountType = db.ExcludedTwitterAccountTypes.Find(excludedTwitterAccountTypeID);
            string msg = "";
            if (excludedTwitterAccountType != null)
            {
                msg = Resources.Mention.AccountTypeNumber + ": " + excludedTwitterAccountType.Id.ToString() + " - " + Resources.Mention.AccountTypeName + " " + excludedTwitterAccountType.TwitterAccountTypeName + " " + Resources.Mention.HasbeenSuccessfullyDeleted;
                db.ExcludedTwitterAccountTypes.Remove(excludedTwitterAccountType);
                db.SaveChanges();
            }
            return msg;
        }


        public static string DeleteIncludedSubredditTopicFromMentionFilter(int includedSubredditTopicID)
        {
            var db = new DBModelBrandWatchContainer();
            var includedSubredditTopic = db.IncludedSubredditTopics.Find(includedSubredditTopicID);
            string msg = "";
            if (includedSubredditTopic != null)
            {
                msg = Resources.Mention.SubredditTopicNumber + ": " + includedSubredditTopic.Id.ToString() + " - " + Resources.Mention.SubredditTopicName + " " + includedSubredditTopic.SubredditTopicName + " " + Resources.Mention.HasbeenSuccessfullyDeleted;
                db.IncludedSubredditTopics.Remove(includedSubredditTopic);
                db.SaveChanges();
            }
            return msg;
        }

        public static string DeleteExcludedSubredditTopicFromMentionFilter(int excludedSubredditTopicID)
        {
            var db = new DBModelBrandWatchContainer();
            var excludedSubredditTopic = db.ExcludedSubredditTopics.Find(excludedSubredditTopicID);
            string msg = "";
            if (excludedSubredditTopic != null)
            {
                msg = Resources.Mention.SubredditTopicNumber + ": " + excludedSubredditTopic.Id.ToString() + " - " + Resources.Mention.SubredditTopicName + " " + excludedSubredditTopic.SubredditTopicName + " " + Resources.Mention.HasbeenSuccessfullyDeleted;
                db.ExcludedSubredditTopics.Remove(excludedSubredditTopic);
                db.SaveChanges();
            }
            return msg;
        }

        public static string DeleteIncludedTwitterVerificationTypeFromMentionFilter(int includeTwitterVerificationTypeID)
        {
            var db = new DBModelBrandWatchContainer();
            var includedTwitterVerificationType = db.IncludedTwitterVerificationTypes.Find(includeTwitterVerificationTypeID);
            string msg = "";
            if (includedTwitterVerificationType != null)
            {
                msg = Resources.Mention.TwitterVerificationTypeNumber + ": " + includedTwitterVerificationType.Id.ToString() + " - " + Resources.Mention.TwitterVerificationTypeName + " " + includedTwitterVerificationType.TwitterVerificationTypeName + " " + Resources.Mention.HasbeenSuccessfullyDeleted;
                db.IncludedTwitterVerificationTypes.Remove(includedTwitterVerificationType);
                db.SaveChanges();
            }
            return msg;
        }
        public static string DeleteExcludedTwitterVerificationTypeFromMentionFilter(int excludeTwitterVerificationTypeID)
        {
            var db = new DBModelBrandWatchContainer();
            var excludedTwitterVerificationType = db.ExcludedTwitterVerificationTypes.Find(excludeTwitterVerificationTypeID);
            string msg = "";
            if (excludedTwitterVerificationType != null)
            {
                msg = Resources.Mention.TwitterVerificationTypeNumber + ": " + excludedTwitterVerificationType.Id.ToString() + " - " + Resources.Mention.TwitterVerificationTypeName + " " + excludedTwitterVerificationType.TwitterVerificationTypeName + " " + Resources.Mention.HasbeenSuccessfullyDeleted;
                db.ExcludedTwitterVerificationTypes.Remove(excludedTwitterVerificationType);
                db.SaveChanges();
            }
            return msg;
        }

        public static string DeleteIncludedSiteListFromMentionFilter(int includeSiteListID)
        {
            var db = new DBModelBrandWatchContainer();
            var includedSiteList = db.IncludedClientProjectSites.Find(includeSiteListID);
            string msg = "";
            if (includedSiteList != null)
            {
                msg = Resources.Mention.SiteListNumber + ": " + includedSiteList.Id.ToString() + " - " + Resources.Mention.SiteListName + " " + includedSiteList.SiteName + " " + Resources.Mention.HasbeenSuccessfullyDeleted;
                db.IncludedClientProjectSites.Remove(includedSiteList);
                db.SaveChanges();
            }
            return msg;
        }

        public static string DeleteExcludedSiteListFromMentionFilter(int excludeSiteListID)
        {
            var db = new DBModelBrandWatchContainer();
            var excludedSiteList = db.ExcludedClientProjectSites.Find(excludeSiteListID);
            string msg = "";
            if (excludedSiteList != null)
            {
                msg = Resources.Mention.SiteListNumber + ": " + excludedSiteList.Id.ToString() + " - " + Resources.Mention.SiteListName + " " + excludedSiteList.SiteName + " " + Resources.Mention.HasbeenSuccessfullyDeleted;
                db.ExcludedClientProjectSites.Remove(excludedSiteList);
                db.SaveChanges();
            }
            return msg;
        }


        public static string DeleteIncludedLanguagFromMentionFilter(int includeLanguageID)
        {
            var db = new DBModelBrandWatchContainer();
            var includedLanguage = db.IncludedLanguages.Find(includeLanguageID);
            string msg = "";
            if (includedLanguage != null)
            {
                msg = Resources.Mention.LanguageNumber + ": " + includedLanguage.Id.ToString() + " - " + Resources.Mention.LanguageName + " " + includedLanguage.LanguageName + " " + Resources.Mention.HasbeenSuccessfullyDeleted;
                db.IncludedLanguages.Remove(includedLanguage);
                db.SaveChanges();
            }
            return msg;
        }
        public static string DeleteExcludedLanguagFromMentionFilter(int excludeLanguageID)
        {
            var db = new DBModelBrandWatchContainer();
            var excludedLanguage = db.ExcludedLanguages.Find(excludeLanguageID);
            string msg = "";
            if (excludedLanguage != null)
            {
                msg = Resources.Mention.LanguageNumber + ": " + excludedLanguage.Id.ToString() + " - " + Resources.Mention.LanguageName + " " + excludedLanguage.LanguageName + " " + Resources.Mention.HasbeenSuccessfullyDeleted;
                db.ExcludedLanguages.Remove(excludedLanguage);
                db.SaveChanges();
            }
            return msg;
        }

        public static string DeleteIncludedAuthorGroupFromMentionFilter(int includeAuthorGroupID)
        {
            var db = new DBModelBrandWatchContainer();
            var includedAuthorGroup = db.IncludedAuthorGroups.Find(includeAuthorGroupID);
            string msg = "";
            if (includedAuthorGroup != null)
            {
                msg = Resources.Mention.AuthorListNumber + ": " + includedAuthorGroup.Id.ToString() + " - " + Resources.Mention.AuthorListName + " " + includedAuthorGroup.AuthorGroupName + " " + Resources.Mention.HasbeenSuccessfullyDeleted;
                db.IncludedAuthorGroups.Remove(includedAuthorGroup);
                db.SaveChanges();
            }
            return msg;
        }
        public static string DeleteExcludedAuthorGroupFromMentionFilter(int excludeAuthorGroupID)
        {
            var db = new DBModelBrandWatchContainer();
            var excludedAuthorGroup = db.ExcludedAuthorGroups.Find(excludeAuthorGroupID);
            string msg = "";
            if (excludedAuthorGroup != null)
            {
                msg = Resources.Mention.AuthorListNumber + ": " + excludedAuthorGroup.Id.ToString() + " - " + Resources.Mention.AuthorListName + " " + excludedAuthorGroup.AuthorGroupName + " " + Resources.Mention.HasbeenSuccessfullyDeleted;
                db.ExcludedAuthorGroups.Remove(excludedAuthorGroup);
                db.SaveChanges();
            }
            return msg;
        }
        public static string DeleteIncludedLocationFromMentionFilter(int includeLocationID)
        {
            var db = new DBModelBrandWatchContainer();
            var includedLocaition = db.IncludedLocations.Find(includeLocationID);
            string msg = "";
            if (includedLocaition != null)
            {
                msg = Resources.Mention.LocationNumber + ": " + includedLocaition.Id.ToString() + " - " + Resources.Mention.LocationName + " " + includedLocaition.LocationName + " " + Resources.Mention.HasbeenSuccessfullyDeleted;
                db.IncludedLocations.Remove(includedLocaition);
                db.SaveChanges();
            }
            return msg;
        }
        public static string DeleteExcludedLocationFromMentionFilter(int excludeLocationID)
        {
            var db = new DBModelBrandWatchContainer();
            var excludedLocaition = db.ExcludedLocations.Find(excludeLocationID);
            string msg = "";
            if (excludedLocaition != null)
            {
                msg = Resources.Mention.LocationNumber + ": " + excludedLocaition.Id.ToString() + " - " + Resources.Mention.LocationName + " " + excludedLocaition.LocationName + " " + Resources.Mention.HasbeenSuccessfullyDeleted;
                db.ExcludedLocations.Remove(excludedLocaition);
                db.SaveChanges();
            }
            return msg;
        }


        public static string DeleteIncludedSocialPanalFromMentionFilter(int includedSocialPanaltID)
        {
            var db = new DBModelBrandWatchContainer();
            var includedSocialPanal = db.IncludedSocialPanels.Find(includedSocialPanaltID);
            string msg = "";
            if (includedSocialPanal != null)
            {
                msg = Resources.Mention.SocialPanelNumber + ": " + includedSocialPanal.Id.ToString() + " - " + Resources.Mention.SocialPanelName + " " + includedSocialPanal.SocialPanelName + " " + Resources.Mention.HasbeenSuccessfullyDeleted;
                db.IncludedSocialPanels.Remove(includedSocialPanal);
                db.SaveChanges();
            }
            return msg;
        }
        public static string DeleteExcludedSocialPanalFromMentionFilter(int excludedSocialPanaltID)
        {
            var db = new DBModelBrandWatchContainer();
            var excludedSocialPanal = db.ExcludedSocialPanels.Find(excludedSocialPanaltID);
            string msg = "";
            if (excludedSocialPanal != null)
            {
                msg = Resources.Mention.SocialPanelNumber + ": " + excludedSocialPanal.Id.ToString() + " - " + Resources.Mention.SocialPanelName + " " + excludedSocialPanal.SocialPanelName + " " + Resources.Mention.HasbeenSuccessfullyDeleted;
                db.ExcludedSocialPanels.Remove(excludedSocialPanal);
                db.SaveChanges();
            }
            return msg;
        }

        public static string DeleteIncludedSentimentFromMentionFilter(int includedSentimentID)
        {
            var db = new DBModelBrandWatchContainer();
            var includedSentiment = db.IncludedSentiments.Find(includedSentimentID);
            string msg = "";
            if (includedSentiment != null)
            {
                msg = Resources.Mention.SentimentNumber + ": " + includedSentiment.Id.ToString() + " - " + Resources.Mention.Sentiment + " " + includedSentiment.SentimentName + " " + Resources.Mention.HasbeenSuccessfullyDeleted;
                db.IncludedSentiments.Remove(includedSentiment);
                db.SaveChanges();
            }
            return msg;
        }

        public static string DeleteIncludedCategoryFromMentionFilter(int includedCategoryID)
        {
            var db = new DBModelBrandWatchContainer();
            var includedCategory = db.IncludedCategories.Find(includedCategoryID);
            string msg = "";
            if (includedCategory != null)
            {
                msg = Resources.Mention.CategoryNumber + ": " + includedCategory.Id.ToString() + " - " + Resources.Projects.CategoryName + " " + includedCategory.CategoryName + " " + Resources.Mention.HasbeenSuccessfullyDeleted;
                db.IncludedCategories.Remove(includedCategory);
                db.SaveChanges();
            }
            return msg;
        }
        public static string DeleteIncludedSubCategoryFromMentionFilter(int includedSubCategoryID)
        {
            var db = new DBModelBrandWatchContainer();
            var includedSubCategory = db.IncludedSubCatgories.Find(includedSubCategoryID);
            string msg = "";
            if (includedSubCategory != null)
            {
                msg = Resources.Mention.SubCategoryNumber + ": " + includedSubCategory.Id.ToString() + " - " + Resources.Projects.SubCategoryName + " " + includedSubCategory.SubCateName + " " + Resources.Mention.HasbeenSuccessfullyDeleted;
                db.IncludedSubCatgories.Remove(includedSubCategory);
                db.SaveChanges();
            }
            return msg;
        }

        public static string DeleteExcludedCategoryFromMentionFilter(int excludedCategoryID)
        {
            var db = new DBModelBrandWatchContainer();
            var excludedCategory = db.ExcludedCategories.Find(excludedCategoryID);
            string msg = "";
            if (excludedCategory != null)
            {
                msg = Resources.Mention.CategoryNumber + ": " + excludedCategory.Id.ToString() + " - " + Resources.Projects.CategoryName + " " + excludedCategory.CategoryName + " " + Resources.Mention.HasbeenSuccessfullyDeleted;
                db.ExcludedCategories.Remove(excludedCategory);
                db.SaveChanges();
            }
            return msg;
        }
        public static string DeleteExcludedSubCategoryFromMentionFilter(int excludedSubCategoryID)
        {
            var db = new DBModelBrandWatchContainer();
            var excludedSubCategory = db.ExcludedSubCatgories.Find(excludedSubCategoryID);
            string msg = "";
            if (excludedSubCategory != null)
            {
                msg = Resources.Mention.SubCategoryNumber + ": " + excludedSubCategory.Id.ToString() + " - " + Resources.Projects.SubCategoryName + " " + excludedSubCategory.SubCateName + " " + Resources.Mention.HasbeenSuccessfullyDeleted;
                db.ExcludedSubCatgories.Remove(excludedSubCategory);
                db.SaveChanges();
            }
            return msg;
        }

        public static string DeleteIncludedQueryFromMentionFilter(int includedQueryID)
        {
            var db = new DBModelBrandWatchContainer();
            var includedQuery = db.IncludedQueries.Find(includedQueryID);
            string msg = "";
            if (includedQuery != null)
            {
                msg = Resources.Mention.QueryNumber + ": " + includedQuery.Id.ToString() + " - " + Resources.Projects.QueryName + ": " + includedQuery.QueryName + " - " + Resources.Mention.HasbeenSuccessfullyDeleted;
                db.IncludedQueries.Remove(includedQuery);
                db.SaveChanges();
            }
            return msg;
        }
        public static string DeleteIncludedTagFromMentionFilter(int includedTagID)
        {
            var db = new DBModelBrandWatchContainer();
            var includedTag = db.IncludedRuleTags.Find(includedTagID);
            string msg = "";
            if (includedTag != null)
            {
                msg = Resources.Mention.TagNumber + ": " + includedTag.Id.ToString() + " - " + Resources.Projects.RuleTagName + ": " + includedTag.RuleTagName + " - " + Resources.Mention.HasbeenSuccessfullyDeleted;
                db.IncludedRuleTags.Remove(includedTag);
                db.SaveChanges();
            }
            return msg;
        }
        public static string DeleteExcludedTagFromMentionFilter(int excludedTagID)
        {
            var db = new DBModelBrandWatchContainer();
            var excludedTag = db.ExcludedRuleTags.Find(excludedTagID);
            string msg = "";
            if (excludedTag != null)
            {
                msg = Resources.Mention.TagNumber + ": " + excludedTag.Id.ToString() + " - " + Resources.Projects.RuleTagName + ": " + excludedTag.RuleTagName + " - " + Resources.Mention.HasbeenSuccessfullyDeleted;
                db.ExcludedRuleTags.Remove(excludedTag);
                db.SaveChanges();
            }
            return msg;
        }
        public static string DeleteIncludedContentSourceFromMentionFilter(int includedContentSourceID)
        {
            var db = new DBModelBrandWatchContainer();
            var includedContentSource = db.IncludedContentSources.Find(includedContentSourceID);
            string msg = "";
            if (includedContentSource != null)
            {
                msg = Resources.Mention.ContentSourceNumber + ": " + includedContentSource.Id.ToString() + " - " + Resources.Projects.QueryContentSource + ": " + includedContentSource.CountentSourceName + " - " + Resources.Mention.HasbeenSuccessfullyDeleted;
                db.IncludedContentSources.Remove(includedContentSource);
                db.SaveChanges();
            }
            return msg;
        }
        public static string DeleteExcludedContentSourceFromMentionFilter(int excludedContentSourceID)
        {
            var db = new DBModelBrandWatchContainer();
            var excludedContentSource = db.ExcludedContentSources.Find(excludedContentSourceID);
            string msg = "";
            if (excludedContentSource != null)
            {
                msg = Resources.Mention.ContentSourceNumber + ": " + excludedContentSource.Id.ToString() + " - " + Resources.Projects.QueryContentSource + ": " + excludedContentSource.CountentSourceName + " - " + Resources.Mention.HasbeenSuccessfullyDeleted;
                db.ExcludedContentSources.Remove(excludedContentSource);
                db.SaveChanges();
            }
            return msg;
        }
        public static string DeleteIncludedMentionTypeFromMentionFilter(int includedMentionTypeID)
        {
            var db = new DBModelBrandWatchContainer();
            var includedMentionType = db.IncludedMentionTypes.Find(includedMentionTypeID);
            string msg = "";
            if (includedMentionType != null)
            {
                msg = Resources.Mention.MentionTypeNumber + ": " + includedMentionType.Id.ToString() + " - " + Resources.Mention.MentionType + ": " + includedMentionType.MentionTypeName + " - " + Resources.Mention.HasbeenSuccessfullyDeleted;
                db.IncludedMentionTypes.Remove(includedMentionType);
                db.SaveChanges();
            }
            return msg;
        }
        public static string DeleteExcludedMentionTypeFromMentionFilter(int excludedMentionTypeID)
        {
            var db = new DBModelBrandWatchContainer();
            var excludedMentionType = db.ExcludedMentionTypes.Find(excludedMentionTypeID);
            string msg = "";
            if (excludedMentionType != null)
            {
                msg = Resources.Mention.MentionTypeNumber + ": " + excludedMentionType.Id.ToString() + " - " + Resources.Mention.MentionType + ": " + excludedMentionType.MentionTypeName + " - " + Resources.Mention.HasbeenSuccessfullyDeleted;
                db.ExcludedMentionTypes.Remove(excludedMentionType);
                db.SaveChanges();
            }
            return msg;
        }

        public static string DeleteIncludedReactScoreOverAllFromMentionFilter(int includedReactScoreOverAllID)
        {
            var db = new DBModelBrandWatchContainer();
            var includedReactScoreOverAll = db.IncludedReactScoreOveralls.Find(includedReactScoreOverAllID);
            string msg = "";
            if (includedReactScoreOverAll != null)
            {
                msg = Resources.Mention.OverAllNumber + ": " + includedReactScoreOverAll.Id.ToString() + " - " + Resources.Mention.ReactScoreType + ": " + includedReactScoreOverAll.ScoreOverAllName + " - " + Resources.Mention.HasbeenSuccessfullyDeleted;
                db.IncludedReactScoreOveralls.Remove(includedReactScoreOverAll);
                db.SaveChanges();
            }
            return msg;
        }
        public static string DeleteExcludedReactScoreOverAllFromMentionFilter(int excludedReactScoreOverAllID)
        {
            var db = new DBModelBrandWatchContainer();
            var excludedReactScoreOverAll = db.ExcludedReactScoreOveralls.Find(excludedReactScoreOverAllID);
            string msg = "";
            if (excludedReactScoreOverAll != null)
            {
                msg = Resources.Mention.OverAllNumber + ": " + excludedReactScoreOverAll.Id.ToString() + " - " + Resources.Mention.ReactScoreType + ": " + excludedReactScoreOverAll.ScoreOverAllName + " - " + Resources.Mention.HasbeenSuccessfullyDeleted;
                db.ExcludedReactScoreOveralls.Remove(excludedReactScoreOverAll);
                db.SaveChanges();
            }
            return msg;
        }

        public static string DeleteIncludedReactScoreHarmFromMentionFilter(int includedReactScoreHarmID)
        {
            var db = new DBModelBrandWatchContainer();
            var includedReactScoreHarm = db.IncludedReactScoreHarms.Find(includedReactScoreHarmID);
            string msg = "";
            if (includedReactScoreHarm != null)
            {
                msg = Resources.Mention.HarmNumber + ": " + includedReactScoreHarm.Id.ToString() + " - " + Resources.Mention.ReactScoreType + ": " + includedReactScoreHarm.ScoreHarmName + " - " + Resources.Mention.HasbeenSuccessfullyDeleted;
                db.IncludedReactScoreHarms.Remove(includedReactScoreHarm);
                db.SaveChanges();
            }
            return msg;
        }
        public static string DeleteExcludedReactScoreHarmFromMentionFilter(int excludedReactScoreHarmID)
        {
            var db = new DBModelBrandWatchContainer();
            var excludedReactScoreHarm = db.ExcludedReactScoreHarms.Find(excludedReactScoreHarmID);
            string msg = "";
            if (excludedReactScoreHarm != null)
            {
                msg = Resources.Mention.HarmNumber + ": " + excludedReactScoreHarm.Id.ToString() + " - " + Resources.Mention.ReactScoreType + ": " + excludedReactScoreHarm.ScoreHarmName + " - " + Resources.Mention.HasbeenSuccessfullyDeleted;
                db.ExcludedReactScoreHarms.Remove(excludedReactScoreHarm);
                db.SaveChanges();
            }
            return msg;
        }
        public static string DeleteIncludedReactScoreEmotionalityFromMentionFilter(int includedReactScoreEmotionalityID)
        {
            var db = new DBModelBrandWatchContainer();
            var includedReactScoreEmotionality = db.IncludedReactScoreEmotionalities.Find(includedReactScoreEmotionalityID);
            string msg = "";
            if (includedReactScoreEmotionality != null)
            {
                msg = Resources.Mention.EmotionalityNumber + ": " + includedReactScoreEmotionality.Id.ToString() + " - " + Resources.Mention.ReactScoreType + ": " + includedReactScoreEmotionality.EmotionalityName + " - " + Resources.Mention.HasbeenSuccessfullyDeleted;
                db.IncludedReactScoreEmotionalities.Remove(includedReactScoreEmotionality);
                db.SaveChanges();
            }
            return msg;
        }
        public static string DeleteExcludedReactScoreEmotionalityFromMentionFilter(int excludedReactScoreEmotionalityID)
        {
            var db = new DBModelBrandWatchContainer();
            var excludedReactScoreEmotionality = db.ExcludedReactScoreEmotionalities.Find(excludedReactScoreEmotionalityID);
            string msg = "";
            if (excludedReactScoreEmotionality != null)
            {
                msg = Resources.Mention.EmotionalityNumber + ": " + excludedReactScoreEmotionality.Id.ToString() + " - " + Resources.Mention.ReactScoreType + ": " + excludedReactScoreEmotionality.EmotionalityName + " - " + Resources.Mention.HasbeenSuccessfullyDeleted;
                db.ExcludedReactScoreEmotionalities.Remove(excludedReactScoreEmotionality);
                db.SaveChanges();
            }
            return msg;
        }

        public static string DeleteIncludedEmotionFromMentionFilter(int includedEmotionID)
        {
            var db = new DBModelBrandWatchContainer();
            var includedEmotion= db.IncludedEmotions.Find(includedEmotionID);
            string msg = "";
            if (includedEmotion != null)
            {
                msg = Resources.Mention.EmotionNumber + ": " + includedEmotion.Id.ToString() + " - " + Resources.Mention.Emotion + ": " + includedEmotion.EmotionName + " - " + Resources.Mention.HasbeenSuccessfullyDeleted;
                db.IncludedEmotions.Remove(includedEmotion);
                db.SaveChanges();
            }
            return msg;
        }

        public static string DeleteExcludedEmotionFromMentionFilter(int excludedEmotionID)
        {
            var db = new DBModelBrandWatchContainer();
            var excludedEmotion = db.ExcludedEmotions.Find(excludedEmotionID);
            string msg = "";
            if (excludedEmotion != null)
            {
                msg = Resources.Mention.EmotionNumber + ": " + excludedEmotion.Id.ToString() + " - " + Resources.Mention.Emotion + ": " + excludedEmotion.EmotionName + " - " + Resources.Mention.HasbeenSuccessfullyDeleted;
                db.ExcludedEmotions.Remove(excludedEmotion);
                db.SaveChanges();
            }
            return msg;
        }

        public static string DeleteIncludedTwitterMentionTypeFromMentionFilter(int includedTwitterMentionTypeID)
        {
            var db = new DBModelBrandWatchContainer();
            var includedTwitterMentionType = db.IncludedTwitterMentionTypes.Find(includedTwitterMentionTypeID);
            string msg = "";
            if (includedTwitterMentionType != null)
            {
                msg = Resources.Mention.MentionTypeNumber + ": " + includedTwitterMentionType.Id.ToString() + " - " + Resources.Mention.MentionType + ": " + includedTwitterMentionType.TwitterMentionTypeName + " - " + Resources.Mention.HasbeenSuccessfullyDeleted;
                db.IncludedTwitterMentionTypes.Remove(includedTwitterMentionType);
                db.SaveChanges();
            }
            return msg;
        }
        public static string DeleteExcludedTwitterMentionTypeFromMentionFilter(int excludedTwitterMentionTypeID)
        {
            var db = new DBModelBrandWatchContainer();
            var excludedTwitterMentionType = db.ExcludedTwitterMentionTypes.Find(excludedTwitterMentionTypeID);
            string msg = "";
            if (excludedTwitterMentionType != null)
            {
                msg = Resources.Mention.MentionTypeNumber + ": " + excludedTwitterMentionType.Id.ToString() + " - " + Resources.Mention.MentionType + ": " + excludedTwitterMentionType.TwitterMentionTypeName + " - " + Resources.Mention.HasbeenSuccessfullyDeleted;
                db.ExcludedTwitterMentionTypes.Remove(excludedTwitterMentionType);
                db.SaveChanges();
            }
            return msg;
        }

        public static string DeleteIncludedFaceBookMentionTypeFromMentionFilter(int includedFaceBookMentionTypeID)
        {
            var db = new DBModelBrandWatchContainer();
            var includedFaceBookMentionType = db.IncludedFaceBookMentionTypes.Find(includedFaceBookMentionTypeID);
            string msg = "";
            if (includedFaceBookMentionType != null)
            {
                msg = Resources.Mention.MentionTypeNumber + ": " + includedFaceBookMentionType.Id.ToString() + " - " + Resources.Mention.MentionType + ": " + includedFaceBookMentionType.FaceBookMentionTypeName + " - " + Resources.Mention.HasbeenSuccessfullyDeleted;
                db.IncludedFaceBookMentionTypes.Remove(includedFaceBookMentionType);
                db.SaveChanges();
            }
            return msg;
        }
        public static string DeleteExcludedFaceBookMentionTypeFromMentionFilter(int excludedFaceBookMentionTypeID)
        {
            var db = new DBModelBrandWatchContainer();
            var excludedFaceBookMentionType = db.ExcludedFaceBookMentionTypes.Find(excludedFaceBookMentionTypeID);
            string msg = "";
            if (excludedFaceBookMentionType != null)
            {
                msg = Resources.Mention.MentionTypeNumber + ": " + excludedFaceBookMentionType.Id.ToString() + " - " + Resources.Mention.MentionType + ": " + excludedFaceBookMentionType.FaceBookMentionTypeName + " - " + Resources.Mention.HasbeenSuccessfullyDeleted;
                db.ExcludedFaceBookMentionTypes.Remove(excludedFaceBookMentionType);
                db.SaveChanges();
            }
            return msg;
        }

        public static string DeleteIncludedInstagramMentionTypeFromMentionFilter(int includedInstagramMentionTypeID)
        {
            var db = new DBModelBrandWatchContainer();
            var includedInstagramMentionType = db.IncludedInstagramMentionTypes.Find(includedInstagramMentionTypeID);
            string msg = "";
            if (includedInstagramMentionType != null)
            {
                msg = Resources.Mention.MentionTypeNumber + ": " + includedInstagramMentionType.Id.ToString() + " - " + Resources.Mention.MentionType + ": " + includedInstagramMentionType.InstagramMentionTypeName + " - " + Resources.Mention.HasbeenSuccessfullyDeleted;
                db.IncludedInstagramMentionTypes.Remove(includedInstagramMentionType);
                db.SaveChanges();
            }
            return msg;
        }
        public static string DeleteExcludedInstagramMentionTypeFromMentionFilter(int excludedInstagramMentionTypeID)
        {
            var db = new DBModelBrandWatchContainer();
            var excludedInstagramMentionType = db.ExcludedInstagramMentionTypes.Find(excludedInstagramMentionTypeID);
            string msg = "";
            if (excludedInstagramMentionType != null)
            {
                msg = Resources.Mention.MentionTypeNumber + ": " + excludedInstagramMentionType.Id.ToString() + " - " + Resources.Mention.MentionType + ": " + excludedInstagramMentionType.InstagramMentionTypeName + " - " + Resources.Mention.HasbeenSuccessfullyDeleted;
                db.ExcludedInstagramMentionTypes.Remove(excludedInstagramMentionType);
                db.SaveChanges();
            }
            return msg;
        }

        public static string DeleteIncludedTwitterProfessionFromMentionFilter(int includedTwitterProfessionID)
        {
            var db = new DBModelBrandWatchContainer();
            var includedTwitterProfession = db.IncludedTwitterProfessions.Find(includedTwitterProfessionID);
            string msg = "";
            if (includedTwitterProfession != null)
            {
                msg = Resources.Mention.TwitterProfessionNumber + ": " + includedTwitterProfession.Id.ToString() + " - " + Resources.Mention.TwitterProfessionName + ": " + includedTwitterProfession.ProfessionName + " - " + Resources.Mention.HasbeenSuccessfullyDeleted;
                db.IncludedTwitterProfessions.Remove(includedTwitterProfession);
                db.SaveChanges();
            }
            return msg;
        }
        public static string DeleteExcludedTwitterProfessionFromMentionFilter(int excludedTwitterProfessionID)
        {
            var db = new DBModelBrandWatchContainer();
            var excludedTwitterProfession = db.ExcludedTwitterProfessions.Find(excludedTwitterProfessionID);
            string msg = "";
            if (excludedTwitterProfession != null)
            {
                msg = Resources.Mention.TwitterProfessionNumber + ": " + excludedTwitterProfession.Id.ToString() + " - " + Resources.Mention.TwitterProfessionName + ": " + excludedTwitterProfession.ProfessionName + " - " + Resources.Mention.HasbeenSuccessfullyDeleted;
                db.ExcludedTwitterProfessions.Remove(excludedTwitterProfession);
                db.SaveChanges();
            }
            return msg;
        }


        public static string DeleteIncludedTwitterInterestFromMentionFilter(int includedTwitterInterestID)
        {
            var db = new DBModelBrandWatchContainer();
            var includedTwitterInterest = db.IncludedTwitterInterests.Find(includedTwitterInterestID);
            string msg = "";
            if (includedTwitterInterest != null)
            {
                msg = Resources.Mention.TwitterInterestNumber + ": " + includedTwitterInterest.Id.ToString() + " - " + Resources.Mention.TwitterInterestName + ": " + includedTwitterInterest.InterestName + " - " + Resources.Mention.HasbeenSuccessfullyDeleted;
                db.IncludedTwitterInterests.Remove(includedTwitterInterest);
                db.SaveChanges();
            }
            return msg;
        }
        public static string DeleteExcludedTwitterInterestFromMentionFilter(int excludedTwitterInterestID)
        {
            var db = new DBModelBrandWatchContainer();
            var excludedTwitterInterest = db.ExcludedTwitterInterests.Find(excludedTwitterInterestID);
            string msg = "";
            if (excludedTwitterInterest != null)
            {
                msg = Resources.Mention.TwitterInterestNumber + ": " + excludedTwitterInterest.Id.ToString() + " - " + Resources.Mention.TwitterInterestName + ": " + excludedTwitterInterest.InterestName + " - " + Resources.Mention.HasbeenSuccessfullyDeleted;
                db.ExcludedTwitterInterests.Remove(excludedTwitterInterest);
                db.SaveChanges();
            }
            return msg;
        }

        /// <summary>
        /// Getting From Mention Fillter Section
        /// </summary>
        /// <param name="mentionFilterID"></param>
        /// <returns></returns>
        public static List<IncludedDomains> GetAllIncludedDomains(int mentionFilterID)
        {
            var db = new DBModelBrandWatchContainer();
            var mentionFilter = db.MentionFilters.Find(mentionFilterID);
            return mentionFilter.IncludedDomains.ToList();
        }
        public static List<IncludedSubType> GetAllIncludedSubTypes(int mentionFilterID)
        {
            var db = new DBModelBrandWatchContainer();
            var mentionFilter = db.MentionFilters.Find(mentionFilterID);
            return mentionFilter.IncludedSubTypes.ToList();
        }
        public static List<ExcludedSubType> GetAllExcludedSubTypes(int mentionFilterID)
        {
            var db = new DBModelBrandWatchContainer();
            var mentionFilter = db.MentionFilters.Find(mentionFilterID);
            return mentionFilter.ExcludedSubTypes.ToList();
        }
        public static List<IncludedTwitterInterest> GetAllIncludedTwitterInterests(int mentionFilterID)
        {
            var db = new DBModelBrandWatchContainer();
            var mentionFilter = db.MentionFilters.Find(mentionFilterID);
            return mentionFilter.IncludedTwitterInterests.ToList();
        }
        public static List<ExcludedTwitterInterest> GetAllExcludedTwitterInterests(int mentionFilterID)
        {
            var db = new DBModelBrandWatchContainer();
            var mentionFilter = db.MentionFilters.Find(mentionFilterID);
            return mentionFilter.ExcludedTwitterInterests.ToList();
        }
        public static List<IncludedTwitterProfession> GetAllIncludedTwitterProfessions(int mentionFilterID)
        {
            var db = new DBModelBrandWatchContainer();
            var mentionFilter = db.MentionFilters.Find(mentionFilterID);
            return mentionFilter.IncludedTwitterProfessions.ToList();
        }
        public static List<ExcludedTwitterProfession> GetAllExcludedTwitterProfessions(int mentionFilterID)
        {
            var db = new DBModelBrandWatchContainer();
            var mentionFilter = db.MentionFilters.Find(mentionFilterID);
            return mentionFilter.ExcludedTwitterProfessions.ToList();
        }
        public static List<IncludedTwitterGender> GetAllIncludedTwitterGenders(int mentionFilterID)
        {
            var db = new DBModelBrandWatchContainer();
            var mentionFilter = db.MentionFilters.Find(mentionFilterID);
            return mentionFilter.IncludedTwitterGenders.ToList();
        }
        public static List<IncludedTwitterAccountTypes> GetAllIncludedTwitterAccountTypes(int mentionFilterID)
        {
            var db = new DBModelBrandWatchContainer();
            var mentionFilter = db.MentionFilters.Find(mentionFilterID);
            return mentionFilter.IncludedTwitterAccountTypes.ToList();
        }
        public static List<ExcludedTwitterAccountTypes> GetAllExcludedTwitterAccountTypes(int mentionFilterID)
        {
            var db = new DBModelBrandWatchContainer();
            var mentionFilter = db.MentionFilters.Find(mentionFilterID);
            return mentionFilter.ExcludedTwitterAccountTypes.ToList();
        }

        public static List<IncludedSubredditTopic> GetAllIncludedSubredditTopics(int mentionFilterID)
        {
            var db = new DBModelBrandWatchContainer();
            var mentionFilter = db.MentionFilters.Find(mentionFilterID);
            return mentionFilter.IncludedSubredditTopics.ToList();
        }
        public static List<ExcludedSubredditTopic> GetAllExcludedSubredditTopics(int mentionFilterID)
        {
            var db = new DBModelBrandWatchContainer();
            var mentionFilter = db.MentionFilters.Find(mentionFilterID);
            return mentionFilter.ExcludedSubredditTopics.ToList();
        }

        public static List<IncludedInstagramMentionType> GetAllIncludedInstagramMentionTypes(int mentionFilterID)
        {
            var db = new DBModelBrandWatchContainer();
            var mentionFilter = db.MentionFilters.Find(mentionFilterID);
            return mentionFilter.IncludedInstagramMentionTypes.ToList();
        }
        public static List<ExcludedInstagramMentionType> GetAllExcludedInstagramMentionTypes(int mentionFilterID)
        {
            var db = new DBModelBrandWatchContainer();
            var mentionFilter = db.MentionFilters.Find(mentionFilterID);
            return mentionFilter.ExcludedInstagramMentionTypes.ToList();
        }

        public static List<IncludedFaceBookMentionType> GetAllIncludedFaceBookMentionTypes(int mentionFilterID)
        {
            var db = new DBModelBrandWatchContainer();
            var mentionFilter = db.MentionFilters.Find(mentionFilterID);
            return mentionFilter.IncludedFaceBookMentionTypes.ToList();
        }
        public static List<ExcludedFaceBookMentionType> GetAllExcludedFaceBookMentionTypes(int mentionFilterID)
        {
            var db = new DBModelBrandWatchContainer();
            var mentionFilter = db.MentionFilters.Find(mentionFilterID);
            return mentionFilter.ExcludedFaceBookMentionTypes.ToList();
        }
        public static List<IncludedTwitterVerificationType> GetAllIncludedTwitterVerificationTypes(int mentionFilterID)
        {
            var db = new DBModelBrandWatchContainer();
            var mentionFilter = db.MentionFilters.Find(mentionFilterID);
            return mentionFilter.IncludedTwitterVerificationTypes.ToList();
        }
        public static List<ExcludedTwitterVerificationType> GetAllExcludedTwitterVerificationTypes(int mentionFilterID)
        {
            var db = new DBModelBrandWatchContainer();
            var mentionFilter = db.MentionFilters.Find(mentionFilterID);
            return mentionFilter.ExcludedTwitterVerificationTypes.ToList();
        }


        public static List<IncludedTwitterMentionType> GetAllIncludedTwitterMentionTypes(int mentionFilterID)
        {
            var db = new DBModelBrandWatchContainer();
            var mentionFilter = db.MentionFilters.Find(mentionFilterID);
            return mentionFilter.IncludedTwitterMentionTypes.ToList();
        }
        public static List<ExcludedTwitterMentionType> GetAllExcludedTwitterMentionTypes(int mentionFilterID)
        {
            var db = new DBModelBrandWatchContainer();
            var mentionFilter = db.MentionFilters.Find(mentionFilterID);
            return mentionFilter.ExcludedTwitterMentionTypes.ToList();
        }
        public static List<IncludedClientProjectSite> GetAllIncludedSiteList(int mentionFilterID)
        {
            var db = new DBModelBrandWatchContainer();
            var mentionFilter = db.MentionFilters.Find(mentionFilterID);
            return mentionFilter.IncludedClientProjectSites.ToList();
        }
        public static List<ExcludedClientProjectSite> GetAllExcludedSiteList(int mentionFilterID)
        {
            var db = new DBModelBrandWatchContainer();
            var mentionFilter = db.MentionFilters.Find(mentionFilterID);
            return mentionFilter.ExcludedClientProjectSites.ToList();
        }


        public static List<IncludedLanguage> GetAllIncludedLanguage(int mentionFilterID)
        {
            var db = new DBModelBrandWatchContainer();
            var mentionFilter = db.MentionFilters.Find(mentionFilterID);
            return mentionFilter.IncludedLanguages.ToList();
        }
        public static List<ExcludedLanguage> GetAllExcludedLanguage(int mentionFilterID)
        {
            var db = new DBModelBrandWatchContainer();
            var mentionFilter = db.MentionFilters.Find(mentionFilterID);
            return mentionFilter.ExcludedLanguages.ToList();
        }

        public static List<IncludedLocation> GetAllIncludedLocaition(int mentionFilterID)
        {
            var db = new DBModelBrandWatchContainer();
            var mentionFilter = db.MentionFilters.Find(mentionFilterID);
            return mentionFilter.IncludedLocations.ToList();
        }
        public static List<ExcludedLocation> GetAllExcludedLocaition(int mentionFilterID)
        {
            var db = new DBModelBrandWatchContainer();
            var mentionFilter = db.MentionFilters.Find(mentionFilterID);
            return mentionFilter.ExcludedLocations.ToList();
        }
        public static List<IncludedAuthorGroup> GetAllIncludedAuthorGroup(int mentionFilterID)
        {
            var db = new DBModelBrandWatchContainer();
            var mentionFilter = db.MentionFilters.Find(mentionFilterID);
            return mentionFilter.IncludedAuthorGroups.ToList();
        }
        public static List<ExcludedAuthorGroup> GetAllExcludedAuthorGroup(int mentionFilterID)
        {
            var db = new DBModelBrandWatchContainer();
            var mentionFilter = db.MentionFilters.Find(mentionFilterID);
            return mentionFilter.ExcludedAuthorGroups.ToList();
        }

        public static List<IncludedCategory> GetAllIncludedCategories(int mentionFilterID)
        {
            var db = new DBModelBrandWatchContainer();
            var mentionFilter = db.MentionFilters.Find(mentionFilterID);
            return mentionFilter.IncludedCategories.ToList();
        }
        public static List<IncludedSubCatgory> GetAllIncludedSubCategories(int mentionFilterID)
        {
            var db = new DBModelBrandWatchContainer();
            var mentionFilter = db.MentionFilters.Find(mentionFilterID);
            return mentionFilter.IncludedSubCatgories.ToList();
        }

        public static List<ExcludedCategory> GetAllExcludedCategories(int mentionFilterID)
        {
            var db = new DBModelBrandWatchContainer();
            var mentionFilter = db.MentionFilters.Find(mentionFilterID);
            return mentionFilter.ExcludedCategories.ToList();
        }
        public static List<ExcludedSubCatgory> GetAllExcludedSubCategories(int mentionFilterID)
        {
            var db = new DBModelBrandWatchContainer();
            var mentionFilter = db.MentionFilters.Find(mentionFilterID);
            return mentionFilter.ExcludedSubCatgories.ToList();
        }


        public static List<IncludedQuery> GetAllIncludedQueries(int mentionFilterID)
        {
            var db = new DBModelBrandWatchContainer();
            var mentionFilter = db.MentionFilters.Find(mentionFilterID);
            return mentionFilter.IncludedQueries.ToList();
        }

        public static List<IncludedRuleTag> GetAllIncludedTags(int mentionFilterID)
        {
            var db = new DBModelBrandWatchContainer();
            var mentionFilter = db.MentionFilters.Find(mentionFilterID);
            return mentionFilter.IncludedRuleTags.ToList();
        }
        public static List<ExcludedRuleTag> GetAllExcludedTags(int mentionFilterID)
        {
            var db = new DBModelBrandWatchContainer();
            var mentionFilter = db.MentionFilters.Find(mentionFilterID);
            return mentionFilter.ExcludedRuleTags.ToList();
        }
        public static List<IncludedContentSource> GetAllIncludedContentSource(int mentionFilterID)
        {
            var db = new DBModelBrandWatchContainer();
            var mentionFilter = db.MentionFilters.Find(mentionFilterID);
            return mentionFilter.IncludedContentSources.ToList();
        }
        public static List<ExcludedContentSource> GetAllExcludedContentSource(int mentionFilterID)
        {
            var db = new DBModelBrandWatchContainer();
            var mentionFilter = db.MentionFilters.Find(mentionFilterID);
            return mentionFilter.ExcludedContentSources.ToList();
        }

        public static List<IncludedMentionType> GetAllIncludedMentionTypes(int mentionFilterID)
        {
            var db = new DBModelBrandWatchContainer();
            var mentionFilter = db.MentionFilters.Find(mentionFilterID);
            return mentionFilter.IncludedMentionTypes.ToList();
        }
        public static List<ExcludedMentionType> GetAllExcludedMentionTypes(int mentionFilterID)
        {
            var db = new DBModelBrandWatchContainer();
            var mentionFilter = db.MentionFilters.Find(mentionFilterID);
            return mentionFilter.ExcludedMentionTypes.ToList();
        }

        public static List<IncludedReactScoreOverall> GetAllIncludedReactScoreOverAll(int mentionFilterID)
        {
            var db = new DBModelBrandWatchContainer();
            var mentionFilter = db.MentionFilters.Find(mentionFilterID);
            return mentionFilter.IncludedReactScoreOveralls.ToList();
        }
        public static List<ExcludedReactScoreOverall> GetAllExcludedReactScoreOverAll(int mentionFilterID)
        {
            var db = new DBModelBrandWatchContainer();
            var mentionFilter = db.MentionFilters.Find(mentionFilterID);
            return mentionFilter.ExcludedReactScoreOveralls.ToList();
        }
        public static List<IncludedReactScoreHarm> GetAllIncludedReactScoreHarm(int mentionFilterID)
        {
            var db = new DBModelBrandWatchContainer();
            var mentionFilter = db.MentionFilters.Find(mentionFilterID);
            return mentionFilter.IncludedReactScoreHarms.ToList();
        }
        public static List<ExcludedReactScoreHarm> GetAllExcludedReactScoreHarm(int mentionFilterID)
        {
            var db = new DBModelBrandWatchContainer();
            var mentionFilter = db.MentionFilters.Find(mentionFilterID);
            return mentionFilter.ExcludedReactScoreHarms.ToList();
        }
        public static List<IncludedReactScoreEmotionality> GetAllIncludedReactScoreEmotionality(int mentionFilterID)
        {
            var db = new DBModelBrandWatchContainer();
            var mentionFilter = db.MentionFilters.Find(mentionFilterID);
            return mentionFilter.IncludedReactScoreEmotionalities.ToList();
        }
        public static List<ExcludedReactScoreEmotionality> GetAllExcludedReactScoreEmotionality(int mentionFilterID)
        {
            var db = new DBModelBrandWatchContainer();
            var mentionFilter = db.MentionFilters.Find(mentionFilterID);
            return mentionFilter.ExcludedReactScoreEmotionalities.ToList();
        }

        public static List<IncludedSentiment> GetAllIncludedSentiments(int mentionFilterID)
        {
            var db = new DBModelBrandWatchContainer();
            var mentionFilter = db.MentionFilters.Find(mentionFilterID);
            return mentionFilter.IncludedSentiments.ToList();
        }

        public static List<IncludedEmotion> GetAllIncludedEmotions(int mentionFilterID)
        {
            var db = new DBModelBrandWatchContainer();
            var mentionFilter = db.MentionFilters.Find(mentionFilterID);
            return mentionFilter.IncludedEmotions.ToList();
        }
        public static List<ExcludedEmotion> GetAllExcludedEmotions(int mentionFilterID)
        {
            var db = new DBModelBrandWatchContainer();
            var mentionFilter = db.MentionFilters.Find(mentionFilterID);
            return mentionFilter.ExcludedEmotions.ToList();
        }
        public static List<IncludedSocialPanel> GetAllIncludedSocialPanal(int mentionFilterID)
        {
            var db = new DBModelBrandWatchContainer();
            var mentionFilter = db.MentionFilters.Find(mentionFilterID);
            return mentionFilter.IncludedSocialPanels.ToList();
        }
        public static List<ExcludedSocialPanel> GetAllExcludedSocialPanal(int mentionFilterID)
        {
            var db = new DBModelBrandWatchContainer();
            var mentionFilter = db.MentionFilters.Find(mentionFilterID);
            return mentionFilter.ExcludedSocialPanels.ToList();
        }


       
    }
}