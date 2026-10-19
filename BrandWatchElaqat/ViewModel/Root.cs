using System.Collections.Generic;
using Newtonsoft.Json;

namespace BrandWatchElaqat.ViewModel
{
    public class Root
    {
        [JsonProperty("data")]
        public Data Data { get; set; }

        [JsonProperty("values")]
        public List<Value> Values { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("id")]
        public string Id { get; set; }
    }

    public class Value
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("value")]
        public object ValueData { get; set; } // Can be null, so use object or nullable type

        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("data")]
        public Dictionary<string, object> Data { get; set; }
    }

    public class Data
    {
        [JsonProperty("volume")]
        public int Volume { get; set; }

        [JsonProperty("sentiment")]
        public Sentiment Sentiment { get; set; }

        [JsonProperty("reachEstimate")]
        public int ReachEstimate { get; set; }

        [JsonProperty("totalReachEstimate")]
        public int TotalReachEstimate { get; set; }

        [JsonProperty("monthlyVisitors")]
        public long MonthlyVisitors { get; set; }

        [JsonProperty("countryName")]
        public string CountryName { get; set; }

        [JsonProperty("countryCode")]
        public string CountryCode { get; set; }

        [JsonProperty("authorName")]
        public string AuthorName { get; set; }

        [JsonProperty("authorGender")]
        public string AuthorGender { get; set; }

        [JsonProperty("authorAccountType")]
        public string AuthorAccountType { get; set; }

        [JsonProperty("domain")]
        public string Domain { get; set; }

        [JsonProperty("authorProfessions")]
        public List<object> AuthorProfessions { get; set; }

        [JsonProperty("authorInterest")]
        public List<object> AuthorInterest { get; set; }

        [JsonProperty("authorVolume")]
        public int AuthorVolume { get; set; }

        [JsonProperty("facebookAuthorId")]
        public string FacebookAuthorId { get; set; }

        [JsonProperty("facebookComments")]
        public int FacebookComments { get; set; }

        [JsonProperty("facebookLikes")]
        public int FacebookLikes { get; set; }

        [JsonProperty("facebookPosts")]
        public int FacebookPosts { get; set; }

        [JsonProperty("redditAwardeeKarma")]
        public int RedditAwardeeKarma { get; set; }

        [JsonProperty("redditAwarderKarma")]
        public int RedditAwarderKarma { get; set; }

        [JsonProperty("redditKarma")]
        public int RedditKarma { get; set; }

        [JsonProperty("twitterFollowers")]
        public int TwitterFollowers { get; set; }

        [JsonProperty("twitterFollowing")]
        public int TwitterFollowing { get; set; }

        [JsonProperty("twitterRetweets")]
        public int TwitterRetweets { get; set; }

        [JsonProperty("twitterVerified")]
        public bool TwitterVerified { get; set; }

        [JsonProperty("twitterReplyCount")]
        public int TwitterReplyCount { get; set; }

        [JsonProperty("twitterAtMentions")]
        public int TwitterAtMentions { get; set; }

        [JsonProperty("twitterAvatar")]
        public string TwitterAvatar { get; set; }

        [JsonProperty("twitterPageTitle")]
        public string TwitterPageTitle { get; set; }

        [JsonProperty("twitterTweets")]
        public int TwitterTweets { get; set; }

        [JsonProperty("impact")]
        public double Impact { get; set; }
    }

    public class Sentiment
    {
        [JsonProperty("negative")]
        public int Negative { get; set; }

        [JsonProperty("neutral")]
        public int Neutral { get; set; }

        [JsonProperty("positive")]
        public int Positive { get; set; }
    }

}