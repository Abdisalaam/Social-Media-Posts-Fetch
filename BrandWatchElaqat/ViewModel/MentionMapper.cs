using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BrandWatchElaqat.ViewModel
{
    public class MentionMapper
    {
        public string assetContentId { get; set; }
        public string assetThumbId { get; set; }

        public string accountType { get; set; }
       
        public string added { get; set; }
        public string assignment { get; set; }
        public string author { get; set; }
        public string authorVerifiedType { get; set; }
        public string avatarUrl { get; set; }
        public string batchId { get; set; }
        public string blogName { get; set; }
        public string blueskyAuthorId { get; set; }
        public string blueskyFollowers { get; set; }
        public string blueskyFollowing { get; set; }
        public string blueskyLikes { get; set; }

        public string blueskyPosts { get; set; }
        public string blueskyQuotes { get; set; }
        public string blueskyReplies { get; set; }

        public string blueskyReposts { get; set; }

        public string broadcastMediaUrl { get; set; }
        public string isSyndicated { get; set; }
        public string airType { get; set; }
        public string broadcastType { get; set; }
        public string mediaType { get; set; }
        public string stationName { get; set; }
        public string adValue { get; set; }


        public List<int> categories { get; set; }
        public List<CategoryDetailsMapper> categoryDetails { get; set; }
        public string categoryMetrics { get; set; }

        public Nullable<bool> Checked { get; set; }

        public string city { get; set; }
        public string continent { get; set; }
        public string country { get; set; }
        public string region { get; set; }
        public string date { get; set; }
        public string dailyVisitors { get; set; }
        public List<string> displayUrls { get; set; }
        public string domain { get; set; }
        public string engagementType { get; set; }
     
        public List<string> expandedUrls { get; set; }
        public string facebookAuthorId { get; set; }
        public string facebookComments { get; set; }
        public string facebookLikes { get; set; }
        public string facebookRole { get; set; }
        public string facebookShares { get; set; }
        public string facebookSubtype { get; set; }

        public string fullname { get; set; }
      
        public string gender { get; set; }
        public string guid { get; set; }
        public string impressions { get; set; }
        public List<string> insightsHashtag { get; set; }
        public List<string> insightsMentioned { get; set; }
        public string instagramCommentCount { get; set; }
        public string instagramFollowerCount { get; set; }
        public string instagramFollowingCount { get; set; }
        public string instagramInteractionsCount { get; set; }
        public string instagramLikeCount { get; set; }
        public string instagramPostCount { get; set; }
        public List<string> interest { get; set; }
        public string itemReview { get; set; }
        public string kicker { get; set; }
        public string language { get; set; }
        public string lastAssignmentDate { get; set; }
        public string latitude { get; set; }
        public string linkedinComments { get; set; }
        public string linkedinEngagement { get; set; }
        public string linkedinImpressions { get; set; }
        public string linkedinLikes { get; set; }
        public string linkedinShares { get; set; }
        public string linkedinSponsored { get; set; }
        public string linkedinVideoViews { get; set; }

        public string locationName { get; set; }
        public string longitude { get; set; }
        public List<MatchPositionsMapper> matchPositions { get; set; }
        public string mediaFilter { get; set; }

        public List<string> mediaUrls { get; set; }

        public string monthlyVisitors { get; set; }
        public string originalUrl { get; set; }


        public string parentPostId { get; set; }
        public string parentBlogName { get; set; }
        public string priority { get; set; }
        public List<ProfessionsMapper> professions { get; set; }
        public string pubType { get; set; }
        public string publisherSubType { get; set; }
        public string queryId { get; set; }
        public string queryName { get; set; }
        public string rating { get; set; }
        public string reachEstimate { get; set; }

        public string redditScore { get; set; }
        public string redditComments { get; set; }
        public string redditAuthorKarma { get; set; }
        public string redditAuthorAwarderKarma { get; set; }

        public string replyTo { get; set; }
        public string resourceType { get; set; }


        public string retweetOf { get; set; }
        public string rootPostId { get; set; }
        public string rootBlogName { get; set; }

        public string sentiment { get; set; }
        public string sinaWeiboAuthorId { get; set; }
        public string sinaWeiboFollowers { get; set; }
        public string sinaWeiboFollowing { get; set; }
        public string sinaWeiboBiFollowers { get; set; }
        public string sinaWeiboPostCount { get; set; }
        public string sinaWeiboFavouritesCount { get; set; }
        public string sinaWeiboRawLocation { get; set; }

        public List<string> shortUrls { get; set; }
        public string snippet { get; set; }

        public Nullable<bool> starred { get; set; }
        public string status { get; set; }
        public string subreddit { get; set; }
        public string subredditSubscribers { get; set; }
        public List<string> subscriptions { get; set; }
        public string subTitle { get; set; }

        public string subtype { get; set; }
        public List<string> tags { get; set; }
        public string threadAuthor { get; set; }
        public string threadCreated { get; set; }
        public string threadEntryType { get; set; }
        public string threadId { get; set; }
        public string threadURL { get; set; }
        public string threadsViews { get; set; }
        public string threadsLikes { get; set; }
        public string threadsReplies { get; set; }
        public string threadsReposts { get; set; }
        public string threadsQuotes { get; set; }
        public string threadsShares { get; set; }

        public string title { get; set; }
        public string twitterAuthorId { get; set; }
        public string twitterFollowers { get; set; }
        public string twitterFollowing { get; set; }
        public string twitterLikeCount { get; set; }

        public string twitterPostCount { get; set; }

        public string twitterReplyCount { get; set; }
        public string twitterRetweets { get; set; }
        public string twitterRole { get; set; }
        public string twitterVerified { get; set; }
        public string updated { get; set; }
        public string url { get; set; }
        public string viewership { get; set; }

        public string publicationName { get; set; }
        public List<string> copyright { get; set; }

        public string weblogTitle { get; set; }

        public string tiktokComments { get; set; }
        public string tiktokLikes { get; set; }
        public string tiktokShares { get; set; }
        public string tiktokViews { get; set; }
        public string tiktokReach { get; set; }




        public string subredditNSFW { get; set; }
        public string redditPostFlair { get; set; }
        public string redditAuthorFlair { get; set; }
        public List<string> subredditTopics { get; set; }
        public string redditSpoiler { get; set; }
        public string publicationId { get; set; }
        public string youtubeViews { get; set; }
        public string youtubeLikes { get; set; }
        public string youtubeComments { get; set; }
        public string youtubeFavourites { get; set; }
        public string youtubeDurationMilliseconds { get; set; }
        public string youtubeSubscriberCount { get; set; }
        public string youtubeVideoCount { get; set; }
        public string podcastDurationMs { get; set; }
        public string podcastAudienceEstimate { get; set; }
        public string contentSourceName { get; set; }
        public string contentSource { get; set; }
        public string pageTypeName { get; set; }

        public string error { get; set; }
        public string reactScoreOverall { get; set; }
        public string reactScoreSpam { get; set; }
        public string reactScoreEmotionality { get; set; }
        public string reactScoreHarmful { get; set; }
        public string reactScoreFakeness { get; set; }
        public string engagementScore { get; set; }

        public string redacted { get; set; }
        public string redactedFields { get; set; }
        public string redactionReason { get; set; }
        public string sharedFromOtherProject { get; set; }

        public string tiktokConnectedAccount { get; set; }
        public List<EntityInfoMapper> entityInfo { get; set; }
        public List<ImageInfoMapper> imageInfo { get; set; }

        public string resourceId { get; set; }
        public string impact { get; set; }




    }
}