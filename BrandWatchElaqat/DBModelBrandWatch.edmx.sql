
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 09/20/2025 00:02:32
-- Generated from EDMX file: C:\Users\Sutum06\Documents\Visual Studio 2017\Projects\BrandWatchElaqat\BrandWatchElaqat\DBModelBrandWatch.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [ElaqatBrandwatch];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK_ElaqatEmployeeEmployeeActionHistory]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[EmployeeActionHistories] DROP CONSTRAINT [FK_ElaqatEmployeeEmployeeActionHistory];
GO
IF OBJECT_ID(N'[dbo].[FK_ClientProjectClientProjectQuerie]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[ClientProjectQueries] DROP CONSTRAINT [FK_ClientProjectClientProjectQuerie];
GO
IF OBJECT_ID(N'[dbo].[FK_ClientProjectClientProjectCategory]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[ClientProjectCategories] DROP CONSTRAINT [FK_ClientProjectClientProjectCategory];
GO
IF OBJECT_ID(N'[dbo].[FK_ClientProjectCategoryClientProjectSubCategory]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[ClientProjectSubCategories] DROP CONSTRAINT [FK_ClientProjectCategoryClientProjectSubCategory];
GO
IF OBJECT_ID(N'[dbo].[FK_ElaqatClientClientProject]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[ClientProjects] DROP CONSTRAINT [FK_ElaqatClientClientProject];
GO
IF OBJECT_ID(N'[dbo].[FK_ClientProjectCategoryClintProjectCtegoryQueryID]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[ClintProjectCtegoryQueryIDs] DROP CONSTRAINT [FK_ClientProjectCategoryClintProjectCtegoryQueryID];
GO
IF OBJECT_ID(N'[dbo].[FK_ClientProjectQuerieQueryHighLightTerm]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[QueryHighLightTerms] DROP CONSTRAINT [FK_ClientProjectQuerieQueryHighLightTerm];
GO
IF OBJECT_ID(N'[dbo].[FK_ClientProjectQuerieQueryContentSource]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[QueryContentSources] DROP CONSTRAINT [FK_ClientProjectQuerieQueryContentSource];
GO
IF OBJECT_ID(N'[dbo].[FK_ClientProjectQuerieQueryLanguage]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[QueryLanguages] DROP CONSTRAINT [FK_ClientProjectQuerieQueryLanguage];
GO
IF OBJECT_ID(N'[dbo].[FK_ClientProjectAuthorGroup]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[AuthorGroups] DROP CONSTRAINT [FK_ClientProjectAuthorGroup];
GO
IF OBJECT_ID(N'[dbo].[FK_ClientProjectProjectLocation]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[ProjectLocations] DROP CONSTRAINT [FK_ClientProjectProjectLocation];
GO
IF OBJECT_ID(N'[dbo].[FK_ClientProjectProjectRuleTag]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[ProjectRuleTags] DROP CONSTRAINT [FK_ClientProjectProjectRuleTag];
GO
IF OBJECT_ID(N'[dbo].[FK_GlobalMetricAccountType]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[AccountTypes] DROP CONSTRAINT [FK_GlobalMetricAccountType];
GO
IF OBJECT_ID(N'[dbo].[FK_GlobalMetricSentiment]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Sentiments] DROP CONSTRAINT [FK_GlobalMetricSentiment];
GO
IF OBJECT_ID(N'[dbo].[FK_GlobalMetricGender]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Genders] DROP CONSTRAINT [FK_GlobalMetricGender];
GO
IF OBJECT_ID(N'[dbo].[FK_GlobalMetricContentSource]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[ContentSources] DROP CONSTRAINT [FK_GlobalMetricContentSource];
GO
IF OBJECT_ID(N'[dbo].[FK_GlobalMetricSubType]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[SubTypes] DROP CONSTRAINT [FK_GlobalMetricSubType];
GO
IF OBJECT_ID(N'[dbo].[FK_GlobalMetricSourceType]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[SourceTypes] DROP CONSTRAINT [FK_GlobalMetricSourceType];
GO
IF OBJECT_ID(N'[dbo].[FK_ClientProjectProjectMention]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[ProjectMentions] DROP CONSTRAINT [FK_ClientProjectProjectMention];
GO
IF OBJECT_ID(N'[dbo].[FK_ProjectMentionMentionSubCategoryDetail]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[MentionSubCategoryDetails] DROP CONSTRAINT [FK_ProjectMentionMentionSubCategoryDetail];
GO
IF OBJECT_ID(N'[dbo].[FK_GlobalMetricMentionType]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[MentionTypes] DROP CONSTRAINT [FK_GlobalMetricMentionType];
GO
IF OBJECT_ID(N'[dbo].[FK_GlobalMetricEmotion]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Emotions] DROP CONSTRAINT [FK_GlobalMetricEmotion];
GO
IF OBJECT_ID(N'[dbo].[FK_GlobalMetricMentionStatus]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[MentionStatus] DROP CONSTRAINT [FK_GlobalMetricMentionStatus];
GO
IF OBJECT_ID(N'[dbo].[FK_GlobalMetricPriority]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Priorities] DROP CONSTRAINT [FK_GlobalMetricPriority];
GO
IF OBJECT_ID(N'[dbo].[FK_GlobalMetricTwitterUserRole]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[TwitterUserRoles] DROP CONSTRAINT [FK_GlobalMetricTwitterUserRole];
GO
IF OBJECT_ID(N'[dbo].[FK_GlobalMetricTwitterMentionType]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[TwitterMentionTypes] DROP CONSTRAINT [FK_GlobalMetricTwitterMentionType];
GO
IF OBJECT_ID(N'[dbo].[FK_GlobalMetricTwitterVerificationType]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[TwitterVerificationTypes] DROP CONSTRAINT [FK_GlobalMetricTwitterVerificationType];
GO
IF OBJECT_ID(N'[dbo].[FK_GlobalMetricFaceBookUserRole]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[FaceBookUserRoles] DROP CONSTRAINT [FK_GlobalMetricFaceBookUserRole];
GO
IF OBJECT_ID(N'[dbo].[FK_GlobalMetricFaceBookMentionType]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[FaceBookMentionTypes] DROP CONSTRAINT [FK_GlobalMetricFaceBookMentionType];
GO
IF OBJECT_ID(N'[dbo].[FK_GlobalMetricInstagramMentionType]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[InstagramMentionTypes] DROP CONSTRAINT [FK_GlobalMetricInstagramMentionType];
GO
IF OBJECT_ID(N'[dbo].[FK_GlobalMetricSubredditTopics]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[SubredditTopics] DROP CONSTRAINT [FK_GlobalMetricSubredditTopics];
GO
IF OBJECT_ID(N'[dbo].[FK_GlobalMetricSocialPanel]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[SocialPanels] DROP CONSTRAINT [FK_GlobalMetricSocialPanel];
GO
IF OBJECT_ID(N'[dbo].[FK_GlobalMetricAssignment]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Assignments] DROP CONSTRAINT [FK_GlobalMetricAssignment];
GO
IF OBJECT_ID(N'[dbo].[FK_GlobalMetricCheckedMention]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[CheckedMentions] DROP CONSTRAINT [FK_GlobalMetricCheckedMention];
GO
IF OBJECT_ID(N'[dbo].[FK_ClientProjectGlobalMetric]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[GlobalMetrics] DROP CONSTRAINT [FK_ClientProjectGlobalMetric];
GO
IF OBJECT_ID(N'[dbo].[FK_GlobalMetricReactScoreOverall]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[ReactScoreOveralls] DROP CONSTRAINT [FK_GlobalMetricReactScoreOverall];
GO
IF OBJECT_ID(N'[dbo].[FK_ClientProjectClientProjectSites]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[ClientProjectSites] DROP CONSTRAINT [FK_ClientProjectClientProjectSites];
GO
IF OBJECT_ID(N'[dbo].[FK_ClientProjectSitesSiteDomain]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[SiteDomains] DROP CONSTRAINT [FK_ClientProjectSitesSiteDomain];
GO
IF OBJECT_ID(N'[dbo].[FK_GlobalMetricRedditNSFW]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[RedditNSFWs] DROP CONSTRAINT [FK_GlobalMetricRedditNSFW];
GO
IF OBJECT_ID(N'[dbo].[FK_GlobalMetricRedditPoll]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[RedditPolls] DROP CONSTRAINT [FK_GlobalMetricRedditPoll];
GO
IF OBJECT_ID(N'[dbo].[FK_GlobalMetricTwitterProfession]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[TwitterProfessions] DROP CONSTRAINT [FK_GlobalMetricTwitterProfession];
GO
IF OBJECT_ID(N'[dbo].[FK_GlobalMetricTwitterInterest]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[TwitterInterests] DROP CONSTRAINT [FK_GlobalMetricTwitterInterest];
GO
IF OBJECT_ID(N'[dbo].[FK_GlobalMetricTwitterVerification]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[TwitterVerifications] DROP CONSTRAINT [FK_GlobalMetricTwitterVerification];
GO
IF OBJECT_ID(N'[dbo].[FK_MentionFilterIncludedCategory]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[IncludedCategories] DROP CONSTRAINT [FK_MentionFilterIncludedCategory];
GO
IF OBJECT_ID(N'[dbo].[FK_MentionFilterExcludedCategory]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[ExcludedCategories] DROP CONSTRAINT [FK_MentionFilterExcludedCategory];
GO
IF OBJECT_ID(N'[dbo].[FK_MentionFilterIncludedQuery]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[IncludedQueries] DROP CONSTRAINT [FK_MentionFilterIncludedQuery];
GO
IF OBJECT_ID(N'[dbo].[FK_MentionFilterExcludedQuery]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[ExcludedQueries] DROP CONSTRAINT [FK_MentionFilterExcludedQuery];
GO
IF OBJECT_ID(N'[dbo].[FK_MentionFilterIncludedSubCatgory]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[IncludedSubCatgories] DROP CONSTRAINT [FK_MentionFilterIncludedSubCatgory];
GO
IF OBJECT_ID(N'[dbo].[FK_MentionFilterExcludedSubCatgory]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[ExcludedSubCatgories] DROP CONSTRAINT [FK_MentionFilterExcludedSubCatgory];
GO
IF OBJECT_ID(N'[dbo].[FK_MentionFilterIncludedRuleTag]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[IncludedRuleTags] DROP CONSTRAINT [FK_MentionFilterIncludedRuleTag];
GO
IF OBJECT_ID(N'[dbo].[FK_MentionFilterExcludedRuleTag]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[ExcludedRuleTags] DROP CONSTRAINT [FK_MentionFilterExcludedRuleTag];
GO
IF OBJECT_ID(N'[dbo].[FK_MentionFilterIncludedContentSource]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[IncludedContentSources] DROP CONSTRAINT [FK_MentionFilterIncludedContentSource];
GO
IF OBJECT_ID(N'[dbo].[FK_MentionFilterExcludedContentSource]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[ExcludedContentSources] DROP CONSTRAINT [FK_MentionFilterExcludedContentSource];
GO
IF OBJECT_ID(N'[dbo].[FK_MentionFilterIncludedAuthorGroup]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[IncludedAuthorGroups] DROP CONSTRAINT [FK_MentionFilterIncludedAuthorGroup];
GO
IF OBJECT_ID(N'[dbo].[FK_MentionFilterExcludedAuthorGroup]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[ExcludedAuthorGroups] DROP CONSTRAINT [FK_MentionFilterExcludedAuthorGroup];
GO
IF OBJECT_ID(N'[dbo].[FK_MentionFilterIncludedLocation]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[IncludedLocations] DROP CONSTRAINT [FK_MentionFilterIncludedLocation];
GO
IF OBJECT_ID(N'[dbo].[FK_MentionFilterExcludedLocation]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[ExcludedLocations] DROP CONSTRAINT [FK_MentionFilterExcludedLocation];
GO
IF OBJECT_ID(N'[dbo].[FK_MentionFilterIncludedLanguage]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[IncludedLanguages] DROP CONSTRAINT [FK_MentionFilterIncludedLanguage];
GO
IF OBJECT_ID(N'[dbo].[FK_MentionFilterExcludedLanguage]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[ExcludedLanguages] DROP CONSTRAINT [FK_MentionFilterExcludedLanguage];
GO
IF OBJECT_ID(N'[dbo].[FK_MentionFilterIncludedMentionType]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[IncludedMentionTypes] DROP CONSTRAINT [FK_MentionFilterIncludedMentionType];
GO
IF OBJECT_ID(N'[dbo].[FK_MentionFilterExcludedMentionType]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[ExcludedMentionTypes] DROP CONSTRAINT [FK_MentionFilterExcludedMentionType];
GO
IF OBJECT_ID(N'[dbo].[FK_MentionFilterIncludedReactScoreOverall]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[IncludedReactScoreOveralls] DROP CONSTRAINT [FK_MentionFilterIncludedReactScoreOverall];
GO
IF OBJECT_ID(N'[dbo].[FK_MentionFilterExcludedReactScoreOverall]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[ExcludedReactScoreOveralls] DROP CONSTRAINT [FK_MentionFilterExcludedReactScoreOverall];
GO
IF OBJECT_ID(N'[dbo].[FK_MentionFilterIncludedReactScoreHarm]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[IncludedReactScoreHarms] DROP CONSTRAINT [FK_MentionFilterIncludedReactScoreHarm];
GO
IF OBJECT_ID(N'[dbo].[FK_MentionFilterExcludedReactScoreHarm]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[ExcludedReactScoreHarms] DROP CONSTRAINT [FK_MentionFilterExcludedReactScoreHarm];
GO
IF OBJECT_ID(N'[dbo].[FK_MentionFilterIncludedReactScoreEmotionality]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[IncludedReactScoreEmotionalities] DROP CONSTRAINT [FK_MentionFilterIncludedReactScoreEmotionality];
GO
IF OBJECT_ID(N'[dbo].[FK_MentionFilterExcludedReactScoreEmotionality]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[ExcludedReactScoreEmotionalities] DROP CONSTRAINT [FK_MentionFilterExcludedReactScoreEmotionality];
GO
IF OBJECT_ID(N'[dbo].[FK_MentionFilterIncludedSentiment]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[IncludedSentiments] DROP CONSTRAINT [FK_MentionFilterIncludedSentiment];
GO
IF OBJECT_ID(N'[dbo].[FK_MentionFilterIncludedEmotion]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[IncludedEmotions] DROP CONSTRAINT [FK_MentionFilterIncludedEmotion];
GO
IF OBJECT_ID(N'[dbo].[FK_MentionFilterExcludedEmotion]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[ExcludedEmotions] DROP CONSTRAINT [FK_MentionFilterExcludedEmotion];
GO
IF OBJECT_ID(N'[dbo].[FK_MentionFilterIncludedSocialPanel]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[IncludedSocialPanels] DROP CONSTRAINT [FK_MentionFilterIncludedSocialPanel];
GO
IF OBJECT_ID(N'[dbo].[FK_MentionFilterExcludedSocialPanel]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[ExcludedSocialPanels] DROP CONSTRAINT [FK_MentionFilterExcludedSocialPanel];
GO
IF OBJECT_ID(N'[dbo].[FK_MentionFilterIncludedClientProjectSite]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[IncludedClientProjectSites] DROP CONSTRAINT [FK_MentionFilterIncludedClientProjectSite];
GO
IF OBJECT_ID(N'[dbo].[FK_MentionFilterExcludedClientProjectSite]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[ExcludedClientProjectSites] DROP CONSTRAINT [FK_MentionFilterExcludedClientProjectSite];
GO
IF OBJECT_ID(N'[dbo].[FK_MentionFilterIncludedTwitterMentionType]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[IncludedTwitterMentionTypes] DROP CONSTRAINT [FK_MentionFilterIncludedTwitterMentionType];
GO
IF OBJECT_ID(N'[dbo].[FK_MentionFilterExcludedTwitterMentionType]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[ExcludedTwitterMentionTypes] DROP CONSTRAINT [FK_MentionFilterExcludedTwitterMentionType];
GO
IF OBJECT_ID(N'[dbo].[FK_MentionFilterIncludedTwitterVerificationType]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[IncludedTwitterVerificationTypes] DROP CONSTRAINT [FK_MentionFilterIncludedTwitterVerificationType];
GO
IF OBJECT_ID(N'[dbo].[FK_MentionFilterExcludedTwitterVerificationType]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[ExcludedTwitterVerificationTypes] DROP CONSTRAINT [FK_MentionFilterExcludedTwitterVerificationType];
GO
IF OBJECT_ID(N'[dbo].[FK_MentionFilterIncludedTwitterAccountTypes]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[IncludedTwitterAccountTypes] DROP CONSTRAINT [FK_MentionFilterIncludedTwitterAccountTypes];
GO
IF OBJECT_ID(N'[dbo].[FK_MentionFilterExcludedTwitterAccountTypes]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[ExcludedTwitterAccountTypes] DROP CONSTRAINT [FK_MentionFilterExcludedTwitterAccountTypes];
GO
IF OBJECT_ID(N'[dbo].[FK_MentionFilterIncludedTwitterGender]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[IncludedTwitterGenders] DROP CONSTRAINT [FK_MentionFilterIncludedTwitterGender];
GO
IF OBJECT_ID(N'[dbo].[FK_MentionFilterIncludedTwitterProfession]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[IncludedTwitterProfessions] DROP CONSTRAINT [FK_MentionFilterIncludedTwitterProfession];
GO
IF OBJECT_ID(N'[dbo].[FK_MentionFilterIncludedTwitterInterest]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[IncludedTwitterInterests] DROP CONSTRAINT [FK_MentionFilterIncludedTwitterInterest];
GO
IF OBJECT_ID(N'[dbo].[FK_MentionFilterExcludedTwitterInterest]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[ExcludedTwitterInterests] DROP CONSTRAINT [FK_MentionFilterExcludedTwitterInterest];
GO
IF OBJECT_ID(N'[dbo].[FK_MentionFilterIncludedSubType]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[IncludedSubTypes] DROP CONSTRAINT [FK_MentionFilterIncludedSubType];
GO
IF OBJECT_ID(N'[dbo].[FK_MentionFilterExcludedSubType]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[ExcludedSubTypes] DROP CONSTRAINT [FK_MentionFilterExcludedSubType];
GO
IF OBJECT_ID(N'[dbo].[FK_MentionFilterIncludedFaceBookMentionType]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[IncludedFaceBookMentionTypes] DROP CONSTRAINT [FK_MentionFilterIncludedFaceBookMentionType];
GO
IF OBJECT_ID(N'[dbo].[FK_MentionFilterIncludedInstagramMentionType]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[IncludedInstagramMentionTypes] DROP CONSTRAINT [FK_MentionFilterIncludedInstagramMentionType];
GO
IF OBJECT_ID(N'[dbo].[FK_MentionFilterExcludedInstagramMentionType]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[ExcludedInstagramMentionTypes] DROP CONSTRAINT [FK_MentionFilterExcludedInstagramMentionType];
GO
IF OBJECT_ID(N'[dbo].[FK_MentionFilterIncludedSubredditTopic]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[IncludedSubredditTopics] DROP CONSTRAINT [FK_MentionFilterIncludedSubredditTopic];
GO
IF OBJECT_ID(N'[dbo].[FK_MentionFilterExcludedSubredditTopic]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[ExcludedSubredditTopics] DROP CONSTRAINT [FK_MentionFilterExcludedSubredditTopic];
GO
IF OBJECT_ID(N'[dbo].[FK_GlobalMetricReactScoreEmotionality]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[ReactScoreEmotionalities] DROP CONSTRAINT [FK_GlobalMetricReactScoreEmotionality];
GO
IF OBJECT_ID(N'[dbo].[FK_GlobalMetricReactScoreHarm]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[ReactScoreHarms] DROP CONSTRAINT [FK_GlobalMetricReactScoreHarm];
GO
IF OBJECT_ID(N'[dbo].[FK_MentionFilterExcludedFaceBookMentionType]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[ExcludedFaceBookMentionTypes] DROP CONSTRAINT [FK_MentionFilterExcludedFaceBookMentionType];
GO
IF OBJECT_ID(N'[dbo].[FK_MentionFilterExcludedTwitterProfession]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[ExcludedTwitterProfessions] DROP CONSTRAINT [FK_MentionFilterExcludedTwitterProfession];
GO
IF OBJECT_ID(N'[dbo].[FK_MentionFilterAllowedMinAndMaxAndMultiOptionsMentionFilter]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[AllowedMinAndMaxAndMultiOptionsMentionFilters] DROP CONSTRAINT [FK_MentionFilterAllowedMinAndMaxAndMultiOptionsMentionFilter];
GO
IF OBJECT_ID(N'[dbo].[FK_ClientProjectClientProjectTwitterTopAuthor]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[ClientProjectTwitterTopAuthors] DROP CONSTRAINT [FK_ClientProjectClientProjectTwitterTopAuthor];
GO
IF OBJECT_ID(N'[dbo].[FK_MentionFilterIncludedDomains]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[IncludedDomains] DROP CONSTRAINT [FK_MentionFilterIncludedDomains];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[ElaqatClients]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ElaqatClients];
GO
IF OBJECT_ID(N'[dbo].[ElaqatEmployees]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ElaqatEmployees];
GO
IF OBJECT_ID(N'[dbo].[ClientProjects]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ClientProjects];
GO
IF OBJECT_ID(N'[dbo].[MediaPuplisheds]', 'U') IS NOT NULL
    DROP TABLE [dbo].[MediaPuplisheds];
GO
IF OBJECT_ID(N'[dbo].[NewsBarFilters]', 'U') IS NOT NULL
    DROP TABLE [dbo].[NewsBarFilters];
GO
IF OBJECT_ID(N'[dbo].[EmployeeActionHistories]', 'U') IS NOT NULL
    DROP TABLE [dbo].[EmployeeActionHistories];
GO
IF OBJECT_ID(N'[dbo].[BrandWatchClientProjects]', 'U') IS NOT NULL
    DROP TABLE [dbo].[BrandWatchClientProjects];
GO
IF OBJECT_ID(N'[dbo].[ProjectMentions]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ProjectMentions];
GO
IF OBJECT_ID(N'[dbo].[ClientProjectCategories]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ClientProjectCategories];
GO
IF OBJECT_ID(N'[dbo].[ClientProjectSubCategories]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ClientProjectSubCategories];
GO
IF OBJECT_ID(N'[dbo].[ClientProjectQueries]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ClientProjectQueries];
GO
IF OBJECT_ID(N'[dbo].[ClintProjectCtegoryQueryIDs]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ClintProjectCtegoryQueryIDs];
GO
IF OBJECT_ID(N'[dbo].[QueryContentSources]', 'U') IS NOT NULL
    DROP TABLE [dbo].[QueryContentSources];
GO
IF OBJECT_ID(N'[dbo].[QueryHighLightTerms]', 'U') IS NOT NULL
    DROP TABLE [dbo].[QueryHighLightTerms];
GO
IF OBJECT_ID(N'[dbo].[QueryLanguages]', 'U') IS NOT NULL
    DROP TABLE [dbo].[QueryLanguages];
GO
IF OBJECT_ID(N'[dbo].[Sentiments]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Sentiments];
GO
IF OBJECT_ID(N'[dbo].[GlobalMetrics]', 'U') IS NOT NULL
    DROP TABLE [dbo].[GlobalMetrics];
GO
IF OBJECT_ID(N'[dbo].[AccountTypes]', 'U') IS NOT NULL
    DROP TABLE [dbo].[AccountTypes];
GO
IF OBJECT_ID(N'[dbo].[Genders]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Genders];
GO
IF OBJECT_ID(N'[dbo].[ContentSources]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ContentSources];
GO
IF OBJECT_ID(N'[dbo].[SubTypes]', 'U') IS NOT NULL
    DROP TABLE [dbo].[SubTypes];
GO
IF OBJECT_ID(N'[dbo].[SourceTypes]', 'U') IS NOT NULL
    DROP TABLE [dbo].[SourceTypes];
GO
IF OBJECT_ID(N'[dbo].[AuthorGroups]', 'U') IS NOT NULL
    DROP TABLE [dbo].[AuthorGroups];
GO
IF OBJECT_ID(N'[dbo].[ProjectLocations]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ProjectLocations];
GO
IF OBJECT_ID(N'[dbo].[ProjectRuleTags]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ProjectRuleTags];
GO
IF OBJECT_ID(N'[dbo].[MentionSubCategoryDetails]', 'U') IS NOT NULL
    DROP TABLE [dbo].[MentionSubCategoryDetails];
GO
IF OBJECT_ID(N'[dbo].[MentionTypes]', 'U') IS NOT NULL
    DROP TABLE [dbo].[MentionTypes];
GO
IF OBJECT_ID(N'[dbo].[ReactScoreOveralls]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ReactScoreOveralls];
GO
IF OBJECT_ID(N'[dbo].[Emotions]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Emotions];
GO
IF OBJECT_ID(N'[dbo].[MentionStatus]', 'U') IS NOT NULL
    DROP TABLE [dbo].[MentionStatus];
GO
IF OBJECT_ID(N'[dbo].[Priorities]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Priorities];
GO
IF OBJECT_ID(N'[dbo].[TwitterUserRoles]', 'U') IS NOT NULL
    DROP TABLE [dbo].[TwitterUserRoles];
GO
IF OBJECT_ID(N'[dbo].[TwitterMentionTypes]', 'U') IS NOT NULL
    DROP TABLE [dbo].[TwitterMentionTypes];
GO
IF OBJECT_ID(N'[dbo].[TwitterVerificationTypes]', 'U') IS NOT NULL
    DROP TABLE [dbo].[TwitterVerificationTypes];
GO
IF OBJECT_ID(N'[dbo].[FaceBookUserRoles]', 'U') IS NOT NULL
    DROP TABLE [dbo].[FaceBookUserRoles];
GO
IF OBJECT_ID(N'[dbo].[FaceBookMentionTypes]', 'U') IS NOT NULL
    DROP TABLE [dbo].[FaceBookMentionTypes];
GO
IF OBJECT_ID(N'[dbo].[InstagramMentionTypes]', 'U') IS NOT NULL
    DROP TABLE [dbo].[InstagramMentionTypes];
GO
IF OBJECT_ID(N'[dbo].[SubredditTopics]', 'U') IS NOT NULL
    DROP TABLE [dbo].[SubredditTopics];
GO
IF OBJECT_ID(N'[dbo].[SocialPanels]', 'U') IS NOT NULL
    DROP TABLE [dbo].[SocialPanels];
GO
IF OBJECT_ID(N'[dbo].[Assignments]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Assignments];
GO
IF OBJECT_ID(N'[dbo].[CheckedMentions]', 'U') IS NOT NULL
    DROP TABLE [dbo].[CheckedMentions];
GO
IF OBJECT_ID(N'[dbo].[RedditPolls]', 'U') IS NOT NULL
    DROP TABLE [dbo].[RedditPolls];
GO
IF OBJECT_ID(N'[dbo].[ReactScoreHarms]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ReactScoreHarms];
GO
IF OBJECT_ID(N'[dbo].[ReactScoreEmotionalities]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ReactScoreEmotionalities];
GO
IF OBJECT_ID(N'[dbo].[ClientProjectSites]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ClientProjectSites];
GO
IF OBJECT_ID(N'[dbo].[SiteDomains]', 'U') IS NOT NULL
    DROP TABLE [dbo].[SiteDomains];
GO
IF OBJECT_ID(N'[dbo].[RedditNSFWs]', 'U') IS NOT NULL
    DROP TABLE [dbo].[RedditNSFWs];
GO
IF OBJECT_ID(N'[dbo].[TwitterProfessions]', 'U') IS NOT NULL
    DROP TABLE [dbo].[TwitterProfessions];
GO
IF OBJECT_ID(N'[dbo].[TwitterInterests]', 'U') IS NOT NULL
    DROP TABLE [dbo].[TwitterInterests];
GO
IF OBJECT_ID(N'[dbo].[TwitterVerifications]', 'U') IS NOT NULL
    DROP TABLE [dbo].[TwitterVerifications];
GO
IF OBJECT_ID(N'[dbo].[MentionFilters]', 'U') IS NOT NULL
    DROP TABLE [dbo].[MentionFilters];
GO
IF OBJECT_ID(N'[dbo].[IncludedCategories]', 'U') IS NOT NULL
    DROP TABLE [dbo].[IncludedCategories];
GO
IF OBJECT_ID(N'[dbo].[ExcludedCategories]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ExcludedCategories];
GO
IF OBJECT_ID(N'[dbo].[IncludedQueries]', 'U') IS NOT NULL
    DROP TABLE [dbo].[IncludedQueries];
GO
IF OBJECT_ID(N'[dbo].[ExcludedQueries]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ExcludedQueries];
GO
IF OBJECT_ID(N'[dbo].[IncludedSubCatgories]', 'U') IS NOT NULL
    DROP TABLE [dbo].[IncludedSubCatgories];
GO
IF OBJECT_ID(N'[dbo].[ExcludedSubCatgories]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ExcludedSubCatgories];
GO
IF OBJECT_ID(N'[dbo].[IncludedRuleTags]', 'U') IS NOT NULL
    DROP TABLE [dbo].[IncludedRuleTags];
GO
IF OBJECT_ID(N'[dbo].[ExcludedRuleTags]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ExcludedRuleTags];
GO
IF OBJECT_ID(N'[dbo].[IncludedContentSources]', 'U') IS NOT NULL
    DROP TABLE [dbo].[IncludedContentSources];
GO
IF OBJECT_ID(N'[dbo].[ExcludedContentSources]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ExcludedContentSources];
GO
IF OBJECT_ID(N'[dbo].[IncludedAuthorGroups]', 'U') IS NOT NULL
    DROP TABLE [dbo].[IncludedAuthorGroups];
GO
IF OBJECT_ID(N'[dbo].[ExcludedAuthorGroups]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ExcludedAuthorGroups];
GO
IF OBJECT_ID(N'[dbo].[IncludedLocations]', 'U') IS NOT NULL
    DROP TABLE [dbo].[IncludedLocations];
GO
IF OBJECT_ID(N'[dbo].[ExcludedLocations]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ExcludedLocations];
GO
IF OBJECT_ID(N'[dbo].[IncludedLanguages]', 'U') IS NOT NULL
    DROP TABLE [dbo].[IncludedLanguages];
GO
IF OBJECT_ID(N'[dbo].[ExcludedLanguages]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ExcludedLanguages];
GO
IF OBJECT_ID(N'[dbo].[IncludedMentionTypes]', 'U') IS NOT NULL
    DROP TABLE [dbo].[IncludedMentionTypes];
GO
IF OBJECT_ID(N'[dbo].[ExcludedMentionTypes]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ExcludedMentionTypes];
GO
IF OBJECT_ID(N'[dbo].[IncludedReactScoreOveralls]', 'U') IS NOT NULL
    DROP TABLE [dbo].[IncludedReactScoreOveralls];
GO
IF OBJECT_ID(N'[dbo].[ExcludedReactScoreOveralls]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ExcludedReactScoreOveralls];
GO
IF OBJECT_ID(N'[dbo].[IncludedReactScoreHarms]', 'U') IS NOT NULL
    DROP TABLE [dbo].[IncludedReactScoreHarms];
GO
IF OBJECT_ID(N'[dbo].[ExcludedReactScoreHarms]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ExcludedReactScoreHarms];
GO
IF OBJECT_ID(N'[dbo].[IncludedReactScoreEmotionalities]', 'U') IS NOT NULL
    DROP TABLE [dbo].[IncludedReactScoreEmotionalities];
GO
IF OBJECT_ID(N'[dbo].[ExcludedReactScoreEmotionalities]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ExcludedReactScoreEmotionalities];
GO
IF OBJECT_ID(N'[dbo].[IncludedSentiments]', 'U') IS NOT NULL
    DROP TABLE [dbo].[IncludedSentiments];
GO
IF OBJECT_ID(N'[dbo].[IncludedEmotions]', 'U') IS NOT NULL
    DROP TABLE [dbo].[IncludedEmotions];
GO
IF OBJECT_ID(N'[dbo].[ExcludedEmotions]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ExcludedEmotions];
GO
IF OBJECT_ID(N'[dbo].[IncludedSocialPanels]', 'U') IS NOT NULL
    DROP TABLE [dbo].[IncludedSocialPanels];
GO
IF OBJECT_ID(N'[dbo].[ExcludedSocialPanels]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ExcludedSocialPanels];
GO
IF OBJECT_ID(N'[dbo].[IncludedClientProjectSites]', 'U') IS NOT NULL
    DROP TABLE [dbo].[IncludedClientProjectSites];
GO
IF OBJECT_ID(N'[dbo].[ExcludedClientProjectSites]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ExcludedClientProjectSites];
GO
IF OBJECT_ID(N'[dbo].[IncludedTwitterMentionTypes]', 'U') IS NOT NULL
    DROP TABLE [dbo].[IncludedTwitterMentionTypes];
GO
IF OBJECT_ID(N'[dbo].[ExcludedTwitterMentionTypes]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ExcludedTwitterMentionTypes];
GO
IF OBJECT_ID(N'[dbo].[IncludedTwitterVerificationTypes]', 'U') IS NOT NULL
    DROP TABLE [dbo].[IncludedTwitterVerificationTypes];
GO
IF OBJECT_ID(N'[dbo].[ExcludedTwitterVerificationTypes]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ExcludedTwitterVerificationTypes];
GO
IF OBJECT_ID(N'[dbo].[IncludedTwitterAccountTypes]', 'U') IS NOT NULL
    DROP TABLE [dbo].[IncludedTwitterAccountTypes];
GO
IF OBJECT_ID(N'[dbo].[ExcludedTwitterAccountTypes]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ExcludedTwitterAccountTypes];
GO
IF OBJECT_ID(N'[dbo].[IncludedTwitterGenders]', 'U') IS NOT NULL
    DROP TABLE [dbo].[IncludedTwitterGenders];
GO
IF OBJECT_ID(N'[dbo].[IncludedTwitterProfessions]', 'U') IS NOT NULL
    DROP TABLE [dbo].[IncludedTwitterProfessions];
GO
IF OBJECT_ID(N'[dbo].[ExcludedTwitterProfessions]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ExcludedTwitterProfessions];
GO
IF OBJECT_ID(N'[dbo].[IncludedTwitterInterests]', 'U') IS NOT NULL
    DROP TABLE [dbo].[IncludedTwitterInterests];
GO
IF OBJECT_ID(N'[dbo].[ExcludedTwitterInterests]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ExcludedTwitterInterests];
GO
IF OBJECT_ID(N'[dbo].[IncludedSubTypes]', 'U') IS NOT NULL
    DROP TABLE [dbo].[IncludedSubTypes];
GO
IF OBJECT_ID(N'[dbo].[ExcludedSubTypes]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ExcludedSubTypes];
GO
IF OBJECT_ID(N'[dbo].[IncludedFaceBookMentionTypes]', 'U') IS NOT NULL
    DROP TABLE [dbo].[IncludedFaceBookMentionTypes];
GO
IF OBJECT_ID(N'[dbo].[ExcludedFaceBookMentionTypes]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ExcludedFaceBookMentionTypes];
GO
IF OBJECT_ID(N'[dbo].[IncludedInstagramMentionTypes]', 'U') IS NOT NULL
    DROP TABLE [dbo].[IncludedInstagramMentionTypes];
GO
IF OBJECT_ID(N'[dbo].[ExcludedInstagramMentionTypes]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ExcludedInstagramMentionTypes];
GO
IF OBJECT_ID(N'[dbo].[IncludedSubredditTopics]', 'U') IS NOT NULL
    DROP TABLE [dbo].[IncludedSubredditTopics];
GO
IF OBJECT_ID(N'[dbo].[ExcludedSubredditTopics]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ExcludedSubredditTopics];
GO
IF OBJECT_ID(N'[dbo].[AllowedMinAndMaxAndMultiOptionsMentionFilters]', 'U') IS NOT NULL
    DROP TABLE [dbo].[AllowedMinAndMaxAndMultiOptionsMentionFilters];
GO
IF OBJECT_ID(N'[dbo].[ClientProjectTwitterTopAuthors]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ClientProjectTwitterTopAuthors];
GO
IF OBJECT_ID(N'[dbo].[IncludedDomains]', 'U') IS NOT NULL
    DROP TABLE [dbo].[IncludedDomains];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'ElaqatClients'
CREATE TABLE [dbo].[ElaqatClients] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [ClientName] nvarchar(max)  NOT NULL,
    [CompanyName] nvarchar(max)  NULL,
    [PhoneNumber] nvarchar(max)  NULL,
    [RegisterDate] datetime  NULL,
    [ClientEmail] nvarchar(max)  NOT NULL,
    [ClientRegisterID] nvarchar(max)  NOT NULL,
    [Adress] nvarchar(max)  NULL,
    [ContactPersonName] nvarchar(max)  NULL,
    [ClientRole] nvarchar(max)  NOT NULL,
    [IsActive] bit  NOT NULL,
    [IsPasswordUpdated] bit  NULL
);
GO

-- Creating table 'ElaqatEmployees'
CREATE TABLE [dbo].[ElaqatEmployees] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [EmployeeName] nvarchar(max)  NOT NULL,
    [RegisterDate] datetime  NULL,
    [PhoneNumber] nvarchar(max)  NULL,
    [EmployeeRegisterID] nvarchar(max)  NOT NULL,
    [EmployeeEmail] nvarchar(max)  NOT NULL,
    [EmployeeRole] nvarchar(max)  NOT NULL,
    [IsActive] bit  NOT NULL,
    [IsPasswordUpdated] bit  NULL
);
GO

-- Creating table 'ClientProjects'
CREATE TABLE [dbo].[ClientProjects] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [ProjectName] nvarchar(max)  NOT NULL,
    [ProjectCreationDate] nvarchar(max)  NOT NULL,
    [BrandwatchProjectID] nvarchar(max)  NOT NULL,
    [Description] nvarchar(max)  NULL,
    [BrandWatchClientId] nvarchar(max)  NULL,
    [BrandWatchClientName] nvarchar(max)  NULL,
    [ElaqatClientId] int  NOT NULL,
    [ShowMentionsStartingFrom] int  NOT NULL,
    [IsActive] bit  NOT NULL
);
GO

-- Creating table 'MediaPuplisheds'
CREATE TABLE [dbo].[MediaPuplisheds] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [MediaType] nvarchar(max)  NOT NULL,
    [MediaURL] nvarchar(max)  NOT NULL,
    [MediaDate] datetime  NOT NULL,
    [ProjectCategoryId] int  NOT NULL
);
GO

-- Creating table 'NewsBarDomains'
CREATE TABLE [dbo].[NewsBarDomains] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [PublicationName] nvarchar(max)  NULL,
    [Domain] nvarchar(max)  NOT NULL,
    [DomainURL] nvarchar(max)  NULL,
    [Favicon] varbinary(max)  NULL
);
GO

-- Creating table 'EmployeeActionHistories'
CREATE TABLE [dbo].[EmployeeActionHistories] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [ActionName] nvarchar(max)  NOT NULL,
    [ActionDate] datetime  NOT NULL,
    [ActionRecord] nvarchar(max)  NOT NULL,
    [ElaqatEmployeeId] int  NOT NULL
);
GO

-- Creating table 'BrandWatchClientProjects'
CREATE TABLE [dbo].[BrandWatchClientProjects] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [ProjectName] nvarchar(max)  NOT NULL,
    [ProjectCreationDate] nvarchar(max)  NOT NULL,
    [BrandwatchProjectID] nvarchar(max)  NOT NULL,
    [Description] nvarchar(max)  NULL,
    [BrandWatchClientId] nvarchar(max)  NULL,
    [BrandWatchClientName] nvarchar(max)  NULL
);
GO

-- Creating table 'ProjectMentions'
CREATE TABLE [dbo].[ProjectMentions] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [AccountType] nvarchar(max)  NULL,
    [AssetContentId] nvarchar(max)  NULL,
    [AssetThumbId] nvarchar(max)  NULL,
    [Snippet] nvarchar(max)  NULL,
    [AddedDateToBrandWatsh] datetime  NOT NULL,
    [Assignment] nvarchar(max)  NULL,
    [Author] nvarchar(max)  NULL,
    [AuthorVerifiedType] nvarchar(max)  NULL,
    [AvatarUrl] nvarchar(max)  NULL,
    [BatchId] nvarchar(max)  NULL,
    [BlogName] nvarchar(max)  NULL,
    [BlueskyAuthorId] nvarchar(max)  NULL,
    [BlueskyFollowers] nvarchar(max)  NULL,
    [BlueskyFollowing] nvarchar(max)  NULL,
    [BlueskyLikes] nvarchar(max)  NULL,
    [BlueskyPosts] nvarchar(max)  NULL,
    [BlueskyQuotes] nvarchar(max)  NULL,
    [BlueskyReplies] nvarchar(max)  NULL,
    [BlueskyReposts] nvarchar(max)  NULL,
    [BroadcastMediaUrl] nvarchar(max)  NULL,
    [BroadcastType] nvarchar(max)  NULL,
    [MediaType] nvarchar(max)  NULL,
    [StationName] nvarchar(max)  NULL,
    [Checked] bit  NULL,
    [City] nvarchar(max)  NULL,
    [Country] nvarchar(max)  NULL,
    [Continent] nvarchar(max)  NULL,
    [ContentSource] nvarchar(max)  NULL,
    [ContentSourceName] nvarchar(max)  NULL,
    [PostedDate] datetime  NOT NULL,
    [DailyVisitors] nvarchar(max)  NULL,
    [DisplayUrls] nvarchar(max)  NULL,
    [MediaUrls] nvarchar(max)  NULL,
    [Domain] nvarchar(max)  NULL,
    [EngagementType] nvarchar(max)  NULL,
    [ExpandedUrls] nvarchar(max)  NULL,
    [FacebookAuthorId] nvarchar(max)  NULL,
    [FacebookComments] nvarchar(max)  NULL,
    [FacebookLikes] nvarchar(max)  NULL,
    [FacebookRole] nvarchar(max)  NULL,
    [FacebookShares] nvarchar(max)  NULL,
    [Gender] nvarchar(max)  NULL,
    [FullName] nvarchar(max)  NULL,
    [ItemReview] nvarchar(max)  NULL,
    [InsightsHashtag] nvarchar(max)  NULL,
    [InsightsMentioned] nvarchar(max)  NULL,
    [LastAssignmentDate] nvarchar(max)  NULL,
    [Kicker] nvarchar(max)  NULL,
    [PubType] nvarchar(max)  NULL,
    [SinaWeiboAuthorId] nvarchar(max)  NULL,
    [SinaWeiboFollowers] nvarchar(max)  NULL,
    [SinaWeiboFollowing] nvarchar(max)  NULL,
    [SinaWeiboBiFollowers] nvarchar(max)  NULL,
    [SinaWeiboPostCount] nvarchar(max)  NULL,
    [SinaWeiboFavouritesCount] nvarchar(max)  NULL,
    [SinaWeiboRawLocation] nvarchar(max)  NULL,
    [GUIDNumber] nvarchar(max)  NULL,
    [Impact] nvarchar(max)  NULL,
    [Impressions] nvarchar(max)  NULL,
    [InstagramCommentCount] nvarchar(max)  NULL,
    [InstagramFollowerCount] nvarchar(max)  NULL,
    [InstagramFollowingCount] nvarchar(max)  NULL,
    [InstagramInteractionsCount] nvarchar(max)  NULL,
    [InstagramLikeCount] nvarchar(max)  NULL,
    [InstagramPostCount] nvarchar(max)  NULL,
    [LinkedinComments] nvarchar(max)  NULL,
    [LinkedinEngagement] nvarchar(max)  NULL,
    [LinkedinImpressions] nvarchar(max)  NULL,
    [LinkedinShares] nvarchar(max)  NULL,
    [LinkedinLikes] nvarchar(max)  NULL,
    [LinkedinSponsored] nvarchar(max)  NULL,
    [LinkedinVideoViews] nvarchar(max)  NULL,
    [LocationName] nvarchar(max)  NULL,
    [MediaFilter] nvarchar(max)  NULL,
    [ParentPostId] nvarchar(max)  NULL,
    [PublisherSubType] nvarchar(max)  NULL,
    [ParentPostIdTumblr] nvarchar(max)  NULL,
    [Interest] nvarchar(max)  NULL,
    [Language] nvarchar(max)  NULL,
    [MonthlyVisitors] nvarchar(max)  NULL,
    [ResourceId] nvarchar(max)  NULL,
    [QueryID] nvarchar(max)  NULL,
    [QueryName] nvarchar(max)  NULL,
    [ReachEstimate] nvarchar(max)  NULL,
    [Sentiment] nvarchar(max)  NULL,
    [URL] nvarchar(max)  NULL,
    [OriginalUrl] nvarchar(max)  NULL,
    [Favicon] varbinary(max)  NULL,
    [ReceivedDate] datetime  NOT NULL,
    [ClientProjectId] int  NOT NULL,
    [Title] nvarchar(max)  NULL,
    [ParentBlogName] nvarchar(max)  NULL,
    [Professions] nvarchar(max)  NULL,
    [Rating] nvarchar(max)  NULL,
    [RedditComments] nvarchar(max)  NULL,
    [RedditScore] nvarchar(max)  NULL,
    [RedditAuthorAwarderKarma] nvarchar(max)  NULL,
    [RootPostId] nvarchar(max)  NULL,
    [RootBlogName] nvarchar(max)  NULL,
    [SubReddit] nvarchar(max)  NULL,
    [ThreadAuthor] nvarchar(max)  NULL,
    [TthreadCreated] nvarchar(max)  NULL,
    [Region] nvarchar(max)  NULL,
    [RetweetOf] nvarchar(max)  NULL,
    [Status] nvarchar(max)  NULL,
    [SubType] nvarchar(max)  NULL,
    [WeblogTitle] nvarchar(max)  NULL,
    [ThreadURL] nvarchar(max)  NULL,
    [ThreadId] nvarchar(max)  NULL,
    [ThreadsViews] nvarchar(max)  NULL,
    [ThreadsLikes] nvarchar(max)  NULL,
    [ThreadsReplies] nvarchar(max)  NULL,
    [ThreadsReposts] nvarchar(max)  NULL,
    [ThreadsQuotes] nvarchar(max)  NULL,
    [ThreadsShares] nvarchar(max)  NULL,
    [ThreadEntryType] nvarchar(max)  NULL,
    [TwitterAuthorId] nvarchar(max)  NULL,
    [TwitterFollowers] nvarchar(max)  NULL,
    [TwitterFollowing] nvarchar(max)  NULL,
    [TwitterLikeCount] nvarchar(max)  NULL,
    [TwitterPostCount] nvarchar(max)  NULL,
    [TwitterReplyCount] nvarchar(max)  NULL,
    [TwitterRetweets] nvarchar(max)  NULL,
    [TwitterRole] nvarchar(max)  NULL,
    [TwitterVerified] nvarchar(max)  NULL,
    [UpdatedDate] nvarchar(max)  NULL,
    [WebBlogTitle] nvarchar(max)  NULL,
    [TiktokComments] nvarchar(max)  NULL,
    [TiktokLikes] nvarchar(max)  NULL,
    [TiktokViews] nvarchar(max)  NULL,
    [TiktokReach] nvarchar(max)  NULL,
    [TiktokShares] nvarchar(max)  NULL,
    [YoutubeViews] nvarchar(max)  NULL,
    [YoutubeLikes] nvarchar(max)  NULL,
    [YoutubeComments] nvarchar(max)  NULL,
    [YoutubeFavourites] nvarchar(max)  NULL,
    [YoutubeDurationMilliseconds] nvarchar(max)  NULL,
    [YoutubeSubscriberCount] nvarchar(max)  NULL,
    [YoutubeVideoCount] nvarchar(max)  NULL,
    [PodcastDurationMs] nvarchar(max)  NULL,
    [PodcastAudienceEstimate] nvarchar(max)  NULL,
    [PublicationId] nvarchar(max)  NULL,
    [PublicationName] nvarchar(max)  NULL,
    [ReactScoreOverall] nvarchar(max)  NULL,
    [ReactScoreSpam] nvarchar(max)  NULL,
    [ReactScoreEmotionality] nvarchar(max)  NULL,
    [ReactScoreHarmful] nvarchar(max)  NULL,
    [ReactScoreFakeness] nvarchar(max)  NULL,
    [PageTypeName] nvarchar(max)  NULL,
    [EngagementScore] nvarchar(max)  NULL,
    [Tags] nvarchar(max)  NULL,
    [SubTitle] nvarchar(max)  NULL,
    [Subscriptions] nvarchar(max)  NULL,
    [SubredditSubscribers] nvarchar(max)  NULL,
    [SubredditNSFW] nvarchar(max)  NULL,
    [ResourceType] nvarchar(max)  NULL,
    [RedditAuthorKarma] nvarchar(max)  NULL,
    [ReplyTo] nvarchar(max)  NULL,
    [Interests] nvarchar(max)  NULL,
    [Starred] bit  NULL,
    [SubredditTopic] nvarchar(max)  NULL,
    [TiktokConnectedAccount] nvarchar(max)  NULL
);
GO

-- Creating table 'ClientProjectCategories'
CREATE TABLE [dbo].[ClientProjectCategories] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [CategoryName] nvarchar(max)  NOT NULL,
    [BrandWatchCategoeryID] nvarchar(max)  NOT NULL,
    [ClientProjectId] int  NOT NULL,
    [IsActive] bit  NOT NULL
);
GO

-- Creating table 'ClientProjectSubCategories'
CREATE TABLE [dbo].[ClientProjectSubCategories] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [BrandWatchSubCategoryID] nvarchar(max)  NOT NULL,
    [SubCategoryName] nvarchar(max)  NOT NULL,
    [OrderingNumber] nvarchar(max)  NOT NULL,
    [ClientProjectCategoryId] int  NOT NULL,
    [IsActive] bit  NOT NULL
);
GO

-- Creating table 'ClientProjectQueries'
CREATE TABLE [dbo].[ClientProjectQueries] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [BrandWatchQueryId] nvarchar(max)  NOT NULL,
    [QuerieName] nvarchar(max)  NOT NULL,
    [DisplayName] nvarchar(max)  NULL,
    [CreationDate] nvarchar(max)  NOT NULL,
    [StartDate] nvarchar(max)  NULL,
    [State] nvarchar(max)  NOT NULL,
    [Description] nvarchar(max)  NULL,
    [BooleanQuery] nvarchar(max)  NOT NULL,
    [LastModificationDate] nvarchar(max)  NULL,
    [Active] nvarchar(max)  NULL,
    [LastModifiedUsername] nvarchar(max)  NULL,
    [ClientProjectId] int  NOT NULL,
    [IsActive] bit  NOT NULL
);
GO

-- Creating table 'ClintProjectCtegoryQueryIDs'
CREATE TABLE [dbo].[ClintProjectCtegoryQueryIDs] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [BrandWatchQueryID] nvarchar(max)  NOT NULL,
    [ClientProjectCategoryId] int  NOT NULL
);
GO

-- Creating table 'QueryContentSources'
CREATE TABLE [dbo].[QueryContentSources] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [SourceName] nvarchar(max)  NOT NULL,
    [ClientProjectQuerieId] int  NOT NULL
);
GO

-- Creating table 'QueryHighLightTerms'
CREATE TABLE [dbo].[QueryHighLightTerms] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [HighLightTerm] nvarchar(max)  NOT NULL,
    [ClientProjectQuerieId] int  NOT NULL
);
GO

-- Creating table 'QueryLanguages'
CREATE TABLE [dbo].[QueryLanguages] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Language] nvarchar(max)  NOT NULL,
    [ClientProjectQuerieId] int  NOT NULL
);
GO

-- Creating table 'Sentiments'
CREATE TABLE [dbo].[Sentiments] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [BWSentiment] nvarchar(max)  NOT NULL,
    [Active] bit  NOT NULL,
    [GlobalMetricId] int  NOT NULL
);
GO

-- Creating table 'GlobalMetrics'
CREATE TABLE [dbo].[GlobalMetrics] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [FilterName] nvarchar(max)  NOT NULL,
    [IsActive] bit  NOT NULL,
    [ClientProjectId] int  NOT NULL
);
GO

-- Creating table 'AccountTypes'
CREATE TABLE [dbo].[AccountTypes] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [BWAccountType] nvarchar(max)  NOT NULL,
    [Active] bit  NOT NULL,
    [GlobalMetricId] int  NOT NULL
);
GO

-- Creating table 'Genders'
CREATE TABLE [dbo].[Genders] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [BWGender] nvarchar(max)  NOT NULL,
    [Active] bit  NOT NULL,
    [GlobalMetricId] int  NOT NULL
);
GO

-- Creating table 'ContentSources'
CREATE TABLE [dbo].[ContentSources] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [BWContentSourceID] nvarchar(max)  NOT NULL,
    [BWContentSource] nvarchar(max)  NOT NULL,
    [Active] bit  NOT NULL,
    [GlobalMetricId] int  NOT NULL
);
GO

-- Creating table 'SubTypes'
CREATE TABLE [dbo].[SubTypes] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [BWSubType] nvarchar(max)  NOT NULL,
    [Active] bit  NOT NULL,
    [GlobalMetricId] int  NOT NULL
);
GO

-- Creating table 'SourceTypes'
CREATE TABLE [dbo].[SourceTypes] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [BWSourceType] nvarchar(max)  NOT NULL,
    [Active] bit  NOT NULL,
    [GlobalMetricId] int  NOT NULL
);
GO

-- Creating table 'AuthorGroups'
CREATE TABLE [dbo].[AuthorGroups] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [BrandWatchAuthorGroupID] nvarchar(max)  NOT NULL,
    [GroupName] nvarchar(max)  NOT NULL,
    [Shared] nvarchar(max)  NOT NULL,
    [IsActive] bit  NOT NULL,
    [ClientProjectId] int  NOT NULL
);
GO

-- Creating table 'ProjectLocations'
CREATE TABLE [dbo].[ProjectLocations] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [BrandWatchGroupLocationID] nvarchar(max)  NOT NULL,
    [LocationName] nvarchar(max)  NOT NULL,
    [Shared] nvarchar(max)  NOT NULL,
    [IsActive] bit  NOT NULL,
    [ClientProjectId] int  NOT NULL
);
GO

-- Creating table 'ProjectRuleTags'
CREATE TABLE [dbo].[ProjectRuleTags] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [BrandWatchRuleTagID] nvarchar(max)  NOT NULL,
    [BrandWatchRuleTagName] nvarchar(max)  NOT NULL,
    [IsActive] bit  NOT NULL,
    [ClientProjectId] int  NOT NULL
);
GO

-- Creating table 'MentionSubCategoryDetails'
CREATE TABLE [dbo].[MentionSubCategoryDetails] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [BrandWatchSubCategoryID] nvarchar(max)  NOT NULL,
    [SubCategoryName] nvarchar(max)  NOT NULL,
    [DisplayName] nvarchar(max)  NULL,
    [ParentCategoryID] nvarchar(max)  NOT NULL,
    [ParentCategoryName] nvarchar(max)  NOT NULL,
    [ProjectMentionId] int  NOT NULL
);
GO

-- Creating table 'MentionTypes'
CREATE TABLE [dbo].[MentionTypes] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [TypeOfMention] nvarchar(max)  NOT NULL,
    [Active] bit  NOT NULL,
    [GlobalMetricId] int  NOT NULL
);
GO

-- Creating table 'ReactScoreOveralls'
CREATE TABLE [dbo].[ReactScoreOveralls] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [ScoreType] nvarchar(max)  NOT NULL,
    [Active] bit  NOT NULL,
    [GlobalMetricId] int  NOT NULL
);
GO

-- Creating table 'Emotions'
CREATE TABLE [dbo].[Emotions] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [EmotionType] nvarchar(max)  NOT NULL,
    [Active] bit  NOT NULL,
    [GlobalMetricId] int  NOT NULL
);
GO

-- Creating table 'MentionStatus'
CREATE TABLE [dbo].[MentionStatus] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Status] nvarchar(max)  NOT NULL,
    [Active] bit  NOT NULL,
    [GlobalMetricId] int  NOT NULL
);
GO

-- Creating table 'Priorities'
CREATE TABLE [dbo].[Priorities] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [PriorityType] nvarchar(max)  NOT NULL,
    [Active] bit  NOT NULL,
    [GlobalMetricId] int  NOT NULL
);
GO

-- Creating table 'TwitterUserRoles'
CREATE TABLE [dbo].[TwitterUserRoles] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Active] bit  NOT NULL,
    [RoleName] nvarchar(max)  NOT NULL,
    [GlobalMetricId] int  NOT NULL
);
GO

-- Creating table 'TwitterMentionTypes'
CREATE TABLE [dbo].[TwitterMentionTypes] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [MentionTypeOfTwitter] nvarchar(max)  NOT NULL,
    [Active] bit  NOT NULL,
    [GlobalMetricId] int  NOT NULL
);
GO

-- Creating table 'TwitterVerificationTypes'
CREATE TABLE [dbo].[TwitterVerificationTypes] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [VerificationType] nvarchar(max)  NOT NULL,
    [Active] bit  NOT NULL,
    [GlobalMetricId] int  NOT NULL
);
GO

-- Creating table 'FaceBookUserRoles'
CREATE TABLE [dbo].[FaceBookUserRoles] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [UserRoleName] nvarchar(max)  NOT NULL,
    [Active] bit  NOT NULL,
    [GlobalMetricId] int  NOT NULL
);
GO

-- Creating table 'FaceBookMentionTypes'
CREATE TABLE [dbo].[FaceBookMentionTypes] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [MentionTypeOfFaceBook] nvarchar(max)  NOT NULL,
    [Active] bit  NOT NULL,
    [GlobalMetricId] int  NOT NULL
);
GO

-- Creating table 'InstagramMentionTypes'
CREATE TABLE [dbo].[InstagramMentionTypes] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [MentionTypeOfInstgram] nvarchar(max)  NOT NULL,
    [Active] bit  NOT NULL,
    [GlobalMetricId] int  NOT NULL
);
GO

-- Creating table 'SubredditTopics'
CREATE TABLE [dbo].[SubredditTopics] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [TopicName] nvarchar(max)  NOT NULL,
    [Active] bit  NOT NULL,
    [GlobalMetricId] int  NOT NULL
);
GO

-- Creating table 'SocialPanels'
CREATE TABLE [dbo].[SocialPanels] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [PanelName] nvarchar(max)  NOT NULL,
    [Active] bit  NOT NULL,
    [GlobalMetricId] int  NOT NULL
);
GO

-- Creating table 'Assignments'
CREATE TABLE [dbo].[Assignments] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [EmployeeUserName] nvarchar(max)  NOT NULL,
    [Active] bit  NOT NULL,
    [GlobalMetricId] int  NOT NULL
);
GO

-- Creating table 'CheckedMentions'
CREATE TABLE [dbo].[CheckedMentions] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [CheckType] nvarchar(max)  NOT NULL,
    [Active] bit  NOT NULL,
    [GlobalMetricId] int  NOT NULL
);
GO

-- Creating table 'RedditPolls'
CREATE TABLE [dbo].[RedditPolls] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Active] bit  NOT NULL,
    [PollChooice] nvarchar(max)  NOT NULL,
    [GlobalMetricId] int  NOT NULL
);
GO

-- Creating table 'ReactScoreHarms'
CREATE TABLE [dbo].[ReactScoreHarms] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [ScoreType] nvarchar(max)  NOT NULL,
    [Active] bit  NOT NULL,
    [GlobalMetricId] int  NOT NULL
);
GO

-- Creating table 'ReactScoreEmotionalities'
CREATE TABLE [dbo].[ReactScoreEmotionalities] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [ScoreType] nvarchar(max)  NOT NULL,
    [Active] bit  NOT NULL,
    [GlobalMetricId] int  NOT NULL
);
GO

-- Creating table 'ClientProjectSites'
CREATE TABLE [dbo].[ClientProjectSites] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [BrandWatchSiteID] nvarchar(max)  NOT NULL,
    [SiteName] nvarchar(max)  NOT NULL,
    [Shared] nvarchar(max)  NULL,
    [IsActive] bit  NOT NULL,
    [ClientProjectId] int  NOT NULL
);
GO

-- Creating table 'SiteDomains'
CREATE TABLE [dbo].[SiteDomains] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [DomainURL] nvarchar(max)  NOT NULL,
    [ClientProjectSitesId] int  NOT NULL
);
GO

-- Creating table 'RedditNSFWs'
CREATE TABLE [dbo].[RedditNSFWs] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Active] bit  NOT NULL,
    [NSFWChooice] nvarchar(max)  NOT NULL,
    [GlobalMetricId] int  NOT NULL
);
GO

-- Creating table 'TwitterProfessions'
CREATE TABLE [dbo].[TwitterProfessions] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Profession] nvarchar(max)  NOT NULL,
    [Active] bit  NOT NULL,
    [GlobalMetricId] int  NOT NULL
);
GO

-- Creating table 'TwitterInterests'
CREATE TABLE [dbo].[TwitterInterests] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Active] bit  NOT NULL,
    [Interest] nvarchar(max)  NOT NULL,
    [GlobalMetricId] int  NOT NULL
);
GO

-- Creating table 'TwitterVerifications'
CREATE TABLE [dbo].[TwitterVerifications] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Verification] nvarchar(max)  NOT NULL,
    [GlobalMetricId] int  NOT NULL,
    [Active] bit  NOT NULL
);
GO

-- Creating table 'MentionFilters'
CREATE TABLE [dbo].[MentionFilters] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [DefaultFilter] bit  NULL,
    [MaxStartDateRange] int  NULL,
    [MaxEndDateRange] int  NULL,
    [ClientProjectID] int  NOT NULL
);
GO

-- Creating table 'IncludedCategories'
CREATE TABLE [dbo].[IncludedCategories] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [CategoryID] int  NOT NULL,
    [CategoryName] nvarchar(max)  NOT NULL,
    [MentionFilterId] int  NOT NULL
);
GO

-- Creating table 'ExcludedCategories'
CREATE TABLE [dbo].[ExcludedCategories] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [CategoryID] int  NOT NULL,
    [CategoryName] nvarchar(max)  NOT NULL,
    [MentionFilterId] int  NOT NULL
);
GO

-- Creating table 'IncludedQueries'
CREATE TABLE [dbo].[IncludedQueries] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [QueryId] int  NOT NULL,
    [QueryName] nvarchar(max)  NOT NULL,
    [MentionFilterId] int  NOT NULL
);
GO

-- Creating table 'ExcludedQueries'
CREATE TABLE [dbo].[ExcludedQueries] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [QueryId] int  NOT NULL,
    [QueryName] nvarchar(max)  NOT NULL,
    [MentionFilterId] int  NOT NULL
);
GO

-- Creating table 'IncludedSubCatgories'
CREATE TABLE [dbo].[IncludedSubCatgories] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [SubCateID] int  NOT NULL,
    [SubCateName] nvarchar(max)  NOT NULL,
    [MentionFilterId] int  NOT NULL
);
GO

-- Creating table 'ExcludedSubCatgories'
CREATE TABLE [dbo].[ExcludedSubCatgories] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [SubCateID] int  NOT NULL,
    [SubCateName] nvarchar(max)  NOT NULL,
    [MentionFilterId] int  NOT NULL
);
GO

-- Creating table 'IncludedRuleTags'
CREATE TABLE [dbo].[IncludedRuleTags] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [RuleTagID] int  NOT NULL,
    [RuleTagName] nvarchar(max)  NOT NULL,
    [MentionFilterId] int  NOT NULL
);
GO

-- Creating table 'ExcludedRuleTags'
CREATE TABLE [dbo].[ExcludedRuleTags] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [RuleTagID] int  NOT NULL,
    [RuleTagName] nvarchar(max)  NOT NULL,
    [MentionFilterId] int  NOT NULL
);
GO

-- Creating table 'IncludedContentSources'
CREATE TABLE [dbo].[IncludedContentSources] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [CountentSourceID] int  NOT NULL,
    [CountentSourceName] nvarchar(max)  NOT NULL,
    [MentionFilterId] int  NOT NULL
);
GO

-- Creating table 'ExcludedContentSources'
CREATE TABLE [dbo].[ExcludedContentSources] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [CountentSourceID] int  NOT NULL,
    [CountentSourceName] nvarchar(max)  NOT NULL,
    [MentionFilterId] int  NOT NULL
);
GO

-- Creating table 'IncludedAuthorGroups'
CREATE TABLE [dbo].[IncludedAuthorGroups] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [AuthorGroupID] int  NOT NULL,
    [AuthorGroupName] nvarchar(max)  NOT NULL,
    [MentionFilterId] int  NOT NULL
);
GO

-- Creating table 'ExcludedAuthorGroups'
CREATE TABLE [dbo].[ExcludedAuthorGroups] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [AuthorGroupID] int  NOT NULL,
    [AuthorGroupName] nvarchar(max)  NOT NULL,
    [MentionFilterId] int  NOT NULL
);
GO

-- Creating table 'IncludedLocations'
CREATE TABLE [dbo].[IncludedLocations] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [LocationID] int  NOT NULL,
    [LocationName] nvarchar(max)  NOT NULL,
    [MentionFilterId] int  NOT NULL
);
GO

-- Creating table 'ExcludedLocations'
CREATE TABLE [dbo].[ExcludedLocations] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [LocationID] int  NOT NULL,
    [LocationName] nvarchar(max)  NOT NULL,
    [MentionFilterId] int  NOT NULL
);
GO

-- Creating table 'IncludedLanguages'
CREATE TABLE [dbo].[IncludedLanguages] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [LanguageID] int  NOT NULL,
    [LanguageName] nvarchar(max)  NOT NULL,
    [MentionFilterId] int  NOT NULL
);
GO

-- Creating table 'ExcludedLanguages'
CREATE TABLE [dbo].[ExcludedLanguages] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [LanguageID] int  NOT NULL,
    [LanguageName] nvarchar(max)  NOT NULL,
    [MentionFilterId] int  NOT NULL
);
GO

-- Creating table 'IncludedMentionTypes'
CREATE TABLE [dbo].[IncludedMentionTypes] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [MentionTypeID] int  NOT NULL,
    [MentionTypeName] nvarchar(max)  NOT NULL,
    [MentionFilterId] int  NOT NULL
);
GO

-- Creating table 'ExcludedMentionTypes'
CREATE TABLE [dbo].[ExcludedMentionTypes] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [MentionTypeID] int  NOT NULL,
    [MentionTypeName] nvarchar(max)  NOT NULL,
    [MentionFilterId] int  NOT NULL
);
GO

-- Creating table 'IncludedReactScoreOveralls'
CREATE TABLE [dbo].[IncludedReactScoreOveralls] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [ScoreOverAllID] int  NOT NULL,
    [ScoreOverAllName] nvarchar(max)  NOT NULL,
    [MentionFilterId] int  NOT NULL
);
GO

-- Creating table 'ExcludedReactScoreOveralls'
CREATE TABLE [dbo].[ExcludedReactScoreOveralls] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [ScoreOverAllID] int  NOT NULL,
    [ScoreOverAllName] nvarchar(max)  NOT NULL,
    [MentionFilterId] int  NOT NULL
);
GO

-- Creating table 'IncludedReactScoreHarms'
CREATE TABLE [dbo].[IncludedReactScoreHarms] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [ScoreHarmID] int  NOT NULL,
    [ScoreHarmName] nvarchar(max)  NOT NULL,
    [MentionFilterId] int  NOT NULL
);
GO

-- Creating table 'ExcludedReactScoreHarms'
CREATE TABLE [dbo].[ExcludedReactScoreHarms] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [ScoreHarmID] int  NOT NULL,
    [ScoreHarmName] nvarchar(max)  NOT NULL,
    [MentionFilterId] int  NOT NULL
);
GO

-- Creating table 'IncludedReactScoreEmotionalities'
CREATE TABLE [dbo].[IncludedReactScoreEmotionalities] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [EmotionalityID] int  NOT NULL,
    [EmotionalityName] nvarchar(max)  NOT NULL,
    [MentionFilterId] int  NOT NULL
);
GO

-- Creating table 'ExcludedReactScoreEmotionalities'
CREATE TABLE [dbo].[ExcludedReactScoreEmotionalities] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [EmotionalityID] int  NOT NULL,
    [EmotionalityName] nvarchar(max)  NOT NULL,
    [MentionFilterId] int  NOT NULL
);
GO

-- Creating table 'IncludedSentiments'
CREATE TABLE [dbo].[IncludedSentiments] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [SentimentID] int  NOT NULL,
    [SentimentName] nvarchar(max)  NOT NULL,
    [MentionFilterId] int  NOT NULL
);
GO

-- Creating table 'IncludedEmotions'
CREATE TABLE [dbo].[IncludedEmotions] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [EmotionID] int  NOT NULL,
    [EmotionName] nvarchar(max)  NOT NULL,
    [MentionFilterId] int  NOT NULL
);
GO

-- Creating table 'ExcludedEmotions'
CREATE TABLE [dbo].[ExcludedEmotions] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [EmotionID] int  NOT NULL,
    [EmotionName] nvarchar(max)  NOT NULL,
    [MentionFilterId] int  NOT NULL
);
GO

-- Creating table 'IncludedSocialPanels'
CREATE TABLE [dbo].[IncludedSocialPanels] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [SocialPanelID] int  NOT NULL,
    [SocialPanelName] nvarchar(max)  NOT NULL,
    [MentionFilterId] int  NOT NULL
);
GO

-- Creating table 'ExcludedSocialPanels'
CREATE TABLE [dbo].[ExcludedSocialPanels] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [SocialPanelID] int  NOT NULL,
    [SocialPanelName] nvarchar(max)  NOT NULL,
    [MentionFilterId] int  NOT NULL
);
GO

-- Creating table 'IncludedClientProjectSites'
CREATE TABLE [dbo].[IncludedClientProjectSites] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [SiteID] int  NOT NULL,
    [SiteName] nvarchar(max)  NOT NULL,
    [MentionFilterId] int  NOT NULL
);
GO

-- Creating table 'ExcludedClientProjectSites'
CREATE TABLE [dbo].[ExcludedClientProjectSites] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [SiteID] int  NOT NULL,
    [SiteName] nvarchar(max)  NOT NULL,
    [MentionFilterId] int  NOT NULL
);
GO

-- Creating table 'IncludedTwitterMentionTypes'
CREATE TABLE [dbo].[IncludedTwitterMentionTypes] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [TwitterMentionTypeID] int  NOT NULL,
    [TwitterMentionTypeName] nvarchar(max)  NOT NULL,
    [MentionFilterId] int  NOT NULL
);
GO

-- Creating table 'ExcludedTwitterMentionTypes'
CREATE TABLE [dbo].[ExcludedTwitterMentionTypes] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [TwitterMentionTypeID] int  NOT NULL,
    [TwitterMentionTypeName] nvarchar(max)  NOT NULL,
    [MentionFilterId] int  NOT NULL
);
GO

-- Creating table 'IncludedTwitterVerificationTypes'
CREATE TABLE [dbo].[IncludedTwitterVerificationTypes] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [TwitterVerificationTypeID] int  NOT NULL,
    [TwitterVerificationTypeName] nvarchar(max)  NOT NULL,
    [MentionFilterId] int  NOT NULL
);
GO

-- Creating table 'ExcludedTwitterVerificationTypes'
CREATE TABLE [dbo].[ExcludedTwitterVerificationTypes] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [TwitterVerificationTypeID] int  NOT NULL,
    [TwitterVerificationTypeName] nvarchar(max)  NOT NULL,
    [MentionFilterId] int  NOT NULL
);
GO

-- Creating table 'IncludedTwitterAccountTypes'
CREATE TABLE [dbo].[IncludedTwitterAccountTypes] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [TwitterAccountTypeID] int  NOT NULL,
    [TwitterAccountTypeName] nvarchar(max)  NOT NULL,
    [MentionFilterId] int  NOT NULL
);
GO

-- Creating table 'ExcludedTwitterAccountTypes'
CREATE TABLE [dbo].[ExcludedTwitterAccountTypes] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [TwitterAccountTypeID] int  NOT NULL,
    [TwitterAccountTypeName] nvarchar(max)  NOT NULL,
    [MentionFilterId] int  NOT NULL
);
GO

-- Creating table 'IncludedTwitterGenders'
CREATE TABLE [dbo].[IncludedTwitterGenders] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [GenderID] int  NOT NULL,
    [GenderName] nvarchar(max)  NOT NULL,
    [MentionFilterId] int  NOT NULL
);
GO

-- Creating table 'IncludedTwitterProfessions'
CREATE TABLE [dbo].[IncludedTwitterProfessions] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [ProfessionID] int  NOT NULL,
    [ProfessionName] nvarchar(max)  NOT NULL,
    [MentionFilterId] int  NOT NULL
);
GO

-- Creating table 'ExcludedTwitterProfessions'
CREATE TABLE [dbo].[ExcludedTwitterProfessions] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [ProfessionID] int  NOT NULL,
    [ProfessionName] nvarchar(max)  NOT NULL,
    [MentionFilterId] int  NOT NULL
);
GO

-- Creating table 'IncludedTwitterInterests'
CREATE TABLE [dbo].[IncludedTwitterInterests] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [InterestID] int  NOT NULL,
    [InterestName] nvarchar(max)  NOT NULL,
    [MentionFilterId] int  NOT NULL
);
GO

-- Creating table 'ExcludedTwitterInterests'
CREATE TABLE [dbo].[ExcludedTwitterInterests] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [InterestID] int  NOT NULL,
    [InterestName] nvarchar(max)  NOT NULL,
    [MentionFilterId] int  NOT NULL
);
GO

-- Creating table 'IncludedSubTypes'
CREATE TABLE [dbo].[IncludedSubTypes] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [SourceTypeID] int  NOT NULL,
    [SourceTypeName] nvarchar(max)  NOT NULL,
    [MentionFilterId] int  NOT NULL
);
GO

-- Creating table 'ExcludedSubTypes'
CREATE TABLE [dbo].[ExcludedSubTypes] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [SourceTypeID] int  NOT NULL,
    [SourceTypeName] nvarchar(max)  NOT NULL,
    [MentionFilterId] int  NOT NULL
);
GO

-- Creating table 'IncludedFaceBookMentionTypes'
CREATE TABLE [dbo].[IncludedFaceBookMentionTypes] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [FaceBookMentionTypeID] int  NOT NULL,
    [FaceBookMentionTypeName] nvarchar(max)  NOT NULL,
    [MentionFilterId] int  NOT NULL
);
GO

-- Creating table 'ExcludedFaceBookMentionTypes'
CREATE TABLE [dbo].[ExcludedFaceBookMentionTypes] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [FaceBookMentionTypeID] int  NOT NULL,
    [FaceBookMentionTypeName] nvarchar(max)  NOT NULL,
    [MentionFilterId] int  NOT NULL
);
GO

-- Creating table 'IncludedInstagramMentionTypes'
CREATE TABLE [dbo].[IncludedInstagramMentionTypes] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [InstagramMentionTypeID] int  NOT NULL,
    [InstagramMentionTypeName] nvarchar(max)  NOT NULL,
    [MentionFilterId] int  NOT NULL
);
GO

-- Creating table 'ExcludedInstagramMentionTypes'
CREATE TABLE [dbo].[ExcludedInstagramMentionTypes] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [InstagramMentionTypeID] int  NOT NULL,
    [InstagramMentionTypeName] nvarchar(max)  NOT NULL,
    [MentionFilterId] int  NOT NULL
);
GO

-- Creating table 'IncludedSubredditTopics'
CREATE TABLE [dbo].[IncludedSubredditTopics] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [SubredditTopicID] int  NOT NULL,
    [SubredditTopicName] nvarchar(max)  NOT NULL,
    [MentionFilterId] int  NOT NULL
);
GO

-- Creating table 'ExcludedSubredditTopics'
CREATE TABLE [dbo].[ExcludedSubredditTopics] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [SubredditTopicID] int  NOT NULL,
    [SubredditTopicName] nvarchar(max)  NOT NULL,
    [MentionFilterId] int  NOT NULL
);
GO

-- Creating table 'AllowedMinAndMaxAndMultiOptionsMentionFilters'
CREATE TABLE [dbo].[AllowedMinAndMaxAndMultiOptionsMentionFilters] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [MinAllowedEngagmentScore] int  NULL,
    [MaxAllowedEngagmentScore] int  NULL,
    [MinAllowedTwitterFollowers] int  NULL,
    [MaxAllowedTwitterFollowers] int  NULL,
    [MinAllowedInstgramFollowers] int  NULL,
    [MaxAllowedInstgramFollowers] int  NULL,
    [MaxAllowedFaceBookFollowers] int  NULL,
    [MinAllowedFaceBookFollowers] int  NULL,
    [MaxAllowedMonthllySiteVisitors] int  NULL,
    [MinAllowedMonthllySiteVisitors] int  NULL,
    [MaxAllowedDaillySiteVisitors] int  NULL,
    [MinAllowedDaillySiteVisitors] int  NULL,
    [MaxReach] int  NULL,
    [MinReach] int  NULL,
    [MaxImpact] int  NULL,
    [MinImpact] int  NULL,
    [TwitterMaxRepost] int  NULL,
    [TwitterMinRepost] int  NULL,
    [TwitterMaxLikes] int  NULL,
    [TwitterMinLikes] int  NULL,
    [TwitterMaxTotalPosts] int  NULL,
    [TwitterMinTotalPosts] int  NULL,
    [TwitterMaxFollowing] int  NULL,
    [TwitterMaxFollowers] int  NULL,
    [TwitterMinFollowers] int  NULL,
    [TwitterMinFollowing] int  NULL,
    [TwitterMaxImpressions] int  NULL,
    [TwitterMinImpressions] int  NULL,
    [FaceBookMaxLikes] int  NULL,
    [FaceBookMinLikes] int  NULL,
    [FaceBookMaxComments] int  NULL,
    [FaceBookMinComments] int  NULL,
    [FaceBookMaxShares] int  NULL,
    [FaceBookMinShares] int  NULL,
    [InstgramMaxInteractions] int  NULL,
    [InstgramMinInteractions] int  NULL,
    [InstgramMaxLikes] int  NULL,
    [InstgramMinLikes] int  NULL,
    [InstgramMaxComments] int  NULL,
    [InstgramMinComments] int  NULL,
    [InstgramMaxPosts] int  NULL,
    [InstgramMinPosts] int  NULL,
    [InstgramMaxFollowers] int  NULL,
    [InstgramMinFollowers] int  NULL,
    [InstgramMaxFollowing] int  NULL,
    [InstgramMinFollowing] int  NULL,
    [RedditMaxScores] int  NULL,
    [RedditMinScores] int  NULL,
    [RedditMaxAuthorKarma] int  NULL,
    [RedditMinAuthorKarma] int  NULL,
    [RedditMaxSubscribers] int  NULL,
    [RedditMinSubscribers] int  NULL,
    [RedditMaxCommentCount] int  NULL,
    [RedditMinCommentCount] int  NULL,
    [BlueskyMaxLikes] int  NULL,
    [BlueskyMinLikes] int  NULL,
    [BlueskyMaxReplies] int  NULL,
    [BlueskyMinReplies] int  NULL,
    [BlueskyMaxReposts] int  NULL,
    [BlueskyMinReposts] int  NULL,
    [BlueskyMaxQuotes] int  NULL,
    [BlueskyMinQuotes] int  NULL,
    [BlueskyMaxFollowers] int  NULL,
    [BlueskyMinFollowers] int  NULL,
    [BlueskyMaxFollowing] int  NULL,
    [BlueskyMinFollowing] int  NULL,
    [BlueskyMaxTotalPosts] int  NULL,
    [BlueskyMinTotalPosts] int  NULL,
    [Assigned] int  NULL,
    [IncludedAuthorName] nvarchar(max)  NULL,
    [ExcludedAuthorName] nvarchar(max)  NULL,
    [Priority] int  NULL,
    [Status] int  NULL,
    [TwitterUserRole] int  NULL,
    [FaceBookUserRole] int  NULL,
    [TwitterVerification] int  NULL,
    [SourceType] int  NULL,
    [Checked] int  NULL,
    [Starred] bit  NULL,
    [ExactMatchIncludedAuthorName] bit  NULL,
    [ExactMatchExcludedAuthorName] bit  NULL,
    [MentionFilterId] int  NOT NULL,
    [NewsBarMatchingWordes] nvarchar(max)  NULL,
    [NewsBarMinVisitors] int  NULL
);
GO

-- Creating table 'ClientProjectTwitterTopAuthors'
CREATE TABLE [dbo].[ClientProjectTwitterTopAuthors] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [AuthorName] nvarchar(max)  NOT NULL,
    [AuthorDomain] nvarchar(max)  NOT NULL,
    [AuthorImpact] float  NOT NULL,
    [AuthorReachEstimate] int  NOT NULL,
    [AuthorTwitterFollowers] int  NOT NULL,
    [AuthorAvatarURL] nvarchar(max)  NOT NULL,
    [AuthorID] nvarchar(max)  NOT NULL,
    [ClientProjectId] int  NOT NULL
);
GO

-- Creating table 'IncludedDomains'
CREATE TABLE [dbo].[IncludedDomains] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Domain] nvarchar(max)  NOT NULL,
    [Favicon] varbinary(max)  NULL,
    [MentionFilterId] int  NOT NULL,
    [DomainID] int  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [Id] in table 'ElaqatClients'
ALTER TABLE [dbo].[ElaqatClients]
ADD CONSTRAINT [PK_ElaqatClients]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'ElaqatEmployees'
ALTER TABLE [dbo].[ElaqatEmployees]
ADD CONSTRAINT [PK_ElaqatEmployees]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'ClientProjects'
ALTER TABLE [dbo].[ClientProjects]
ADD CONSTRAINT [PK_ClientProjects]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'MediaPuplisheds'
ALTER TABLE [dbo].[MediaPuplisheds]
ADD CONSTRAINT [PK_MediaPuplisheds]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'NewsBarDomains'
ALTER TABLE [dbo].[NewsBarDomains]
ADD CONSTRAINT [PK_NewsBarDomains]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'EmployeeActionHistories'
ALTER TABLE [dbo].[EmployeeActionHistories]
ADD CONSTRAINT [PK_EmployeeActionHistories]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'BrandWatchClientProjects'
ALTER TABLE [dbo].[BrandWatchClientProjects]
ADD CONSTRAINT [PK_BrandWatchClientProjects]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'ProjectMentions'
ALTER TABLE [dbo].[ProjectMentions]
ADD CONSTRAINT [PK_ProjectMentions]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'ClientProjectCategories'
ALTER TABLE [dbo].[ClientProjectCategories]
ADD CONSTRAINT [PK_ClientProjectCategories]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'ClientProjectSubCategories'
ALTER TABLE [dbo].[ClientProjectSubCategories]
ADD CONSTRAINT [PK_ClientProjectSubCategories]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'ClientProjectQueries'
ALTER TABLE [dbo].[ClientProjectQueries]
ADD CONSTRAINT [PK_ClientProjectQueries]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'ClintProjectCtegoryQueryIDs'
ALTER TABLE [dbo].[ClintProjectCtegoryQueryIDs]
ADD CONSTRAINT [PK_ClintProjectCtegoryQueryIDs]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'QueryContentSources'
ALTER TABLE [dbo].[QueryContentSources]
ADD CONSTRAINT [PK_QueryContentSources]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'QueryHighLightTerms'
ALTER TABLE [dbo].[QueryHighLightTerms]
ADD CONSTRAINT [PK_QueryHighLightTerms]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'QueryLanguages'
ALTER TABLE [dbo].[QueryLanguages]
ADD CONSTRAINT [PK_QueryLanguages]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Sentiments'
ALTER TABLE [dbo].[Sentiments]
ADD CONSTRAINT [PK_Sentiments]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'GlobalMetrics'
ALTER TABLE [dbo].[GlobalMetrics]
ADD CONSTRAINT [PK_GlobalMetrics]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'AccountTypes'
ALTER TABLE [dbo].[AccountTypes]
ADD CONSTRAINT [PK_AccountTypes]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Genders'
ALTER TABLE [dbo].[Genders]
ADD CONSTRAINT [PK_Genders]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'ContentSources'
ALTER TABLE [dbo].[ContentSources]
ADD CONSTRAINT [PK_ContentSources]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'SubTypes'
ALTER TABLE [dbo].[SubTypes]
ADD CONSTRAINT [PK_SubTypes]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'SourceTypes'
ALTER TABLE [dbo].[SourceTypes]
ADD CONSTRAINT [PK_SourceTypes]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'AuthorGroups'
ALTER TABLE [dbo].[AuthorGroups]
ADD CONSTRAINT [PK_AuthorGroups]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'ProjectLocations'
ALTER TABLE [dbo].[ProjectLocations]
ADD CONSTRAINT [PK_ProjectLocations]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'ProjectRuleTags'
ALTER TABLE [dbo].[ProjectRuleTags]
ADD CONSTRAINT [PK_ProjectRuleTags]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'MentionSubCategoryDetails'
ALTER TABLE [dbo].[MentionSubCategoryDetails]
ADD CONSTRAINT [PK_MentionSubCategoryDetails]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'MentionTypes'
ALTER TABLE [dbo].[MentionTypes]
ADD CONSTRAINT [PK_MentionTypes]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'ReactScoreOveralls'
ALTER TABLE [dbo].[ReactScoreOveralls]
ADD CONSTRAINT [PK_ReactScoreOveralls]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Emotions'
ALTER TABLE [dbo].[Emotions]
ADD CONSTRAINT [PK_Emotions]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'MentionStatus'
ALTER TABLE [dbo].[MentionStatus]
ADD CONSTRAINT [PK_MentionStatus]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Priorities'
ALTER TABLE [dbo].[Priorities]
ADD CONSTRAINT [PK_Priorities]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'TwitterUserRoles'
ALTER TABLE [dbo].[TwitterUserRoles]
ADD CONSTRAINT [PK_TwitterUserRoles]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'TwitterMentionTypes'
ALTER TABLE [dbo].[TwitterMentionTypes]
ADD CONSTRAINT [PK_TwitterMentionTypes]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'TwitterVerificationTypes'
ALTER TABLE [dbo].[TwitterVerificationTypes]
ADD CONSTRAINT [PK_TwitterVerificationTypes]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'FaceBookUserRoles'
ALTER TABLE [dbo].[FaceBookUserRoles]
ADD CONSTRAINT [PK_FaceBookUserRoles]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'FaceBookMentionTypes'
ALTER TABLE [dbo].[FaceBookMentionTypes]
ADD CONSTRAINT [PK_FaceBookMentionTypes]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'InstagramMentionTypes'
ALTER TABLE [dbo].[InstagramMentionTypes]
ADD CONSTRAINT [PK_InstagramMentionTypes]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'SubredditTopics'
ALTER TABLE [dbo].[SubredditTopics]
ADD CONSTRAINT [PK_SubredditTopics]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'SocialPanels'
ALTER TABLE [dbo].[SocialPanels]
ADD CONSTRAINT [PK_SocialPanels]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Assignments'
ALTER TABLE [dbo].[Assignments]
ADD CONSTRAINT [PK_Assignments]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'CheckedMentions'
ALTER TABLE [dbo].[CheckedMentions]
ADD CONSTRAINT [PK_CheckedMentions]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'RedditPolls'
ALTER TABLE [dbo].[RedditPolls]
ADD CONSTRAINT [PK_RedditPolls]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'ReactScoreHarms'
ALTER TABLE [dbo].[ReactScoreHarms]
ADD CONSTRAINT [PK_ReactScoreHarms]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'ReactScoreEmotionalities'
ALTER TABLE [dbo].[ReactScoreEmotionalities]
ADD CONSTRAINT [PK_ReactScoreEmotionalities]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'ClientProjectSites'
ALTER TABLE [dbo].[ClientProjectSites]
ADD CONSTRAINT [PK_ClientProjectSites]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'SiteDomains'
ALTER TABLE [dbo].[SiteDomains]
ADD CONSTRAINT [PK_SiteDomains]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'RedditNSFWs'
ALTER TABLE [dbo].[RedditNSFWs]
ADD CONSTRAINT [PK_RedditNSFWs]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'TwitterProfessions'
ALTER TABLE [dbo].[TwitterProfessions]
ADD CONSTRAINT [PK_TwitterProfessions]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'TwitterInterests'
ALTER TABLE [dbo].[TwitterInterests]
ADD CONSTRAINT [PK_TwitterInterests]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'TwitterVerifications'
ALTER TABLE [dbo].[TwitterVerifications]
ADD CONSTRAINT [PK_TwitterVerifications]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'MentionFilters'
ALTER TABLE [dbo].[MentionFilters]
ADD CONSTRAINT [PK_MentionFilters]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'IncludedCategories'
ALTER TABLE [dbo].[IncludedCategories]
ADD CONSTRAINT [PK_IncludedCategories]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'ExcludedCategories'
ALTER TABLE [dbo].[ExcludedCategories]
ADD CONSTRAINT [PK_ExcludedCategories]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'IncludedQueries'
ALTER TABLE [dbo].[IncludedQueries]
ADD CONSTRAINT [PK_IncludedQueries]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'ExcludedQueries'
ALTER TABLE [dbo].[ExcludedQueries]
ADD CONSTRAINT [PK_ExcludedQueries]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'IncludedSubCatgories'
ALTER TABLE [dbo].[IncludedSubCatgories]
ADD CONSTRAINT [PK_IncludedSubCatgories]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'ExcludedSubCatgories'
ALTER TABLE [dbo].[ExcludedSubCatgories]
ADD CONSTRAINT [PK_ExcludedSubCatgories]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'IncludedRuleTags'
ALTER TABLE [dbo].[IncludedRuleTags]
ADD CONSTRAINT [PK_IncludedRuleTags]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'ExcludedRuleTags'
ALTER TABLE [dbo].[ExcludedRuleTags]
ADD CONSTRAINT [PK_ExcludedRuleTags]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'IncludedContentSources'
ALTER TABLE [dbo].[IncludedContentSources]
ADD CONSTRAINT [PK_IncludedContentSources]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'ExcludedContentSources'
ALTER TABLE [dbo].[ExcludedContentSources]
ADD CONSTRAINT [PK_ExcludedContentSources]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'IncludedAuthorGroups'
ALTER TABLE [dbo].[IncludedAuthorGroups]
ADD CONSTRAINT [PK_IncludedAuthorGroups]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'ExcludedAuthorGroups'
ALTER TABLE [dbo].[ExcludedAuthorGroups]
ADD CONSTRAINT [PK_ExcludedAuthorGroups]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'IncludedLocations'
ALTER TABLE [dbo].[IncludedLocations]
ADD CONSTRAINT [PK_IncludedLocations]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'ExcludedLocations'
ALTER TABLE [dbo].[ExcludedLocations]
ADD CONSTRAINT [PK_ExcludedLocations]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'IncludedLanguages'
ALTER TABLE [dbo].[IncludedLanguages]
ADD CONSTRAINT [PK_IncludedLanguages]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'ExcludedLanguages'
ALTER TABLE [dbo].[ExcludedLanguages]
ADD CONSTRAINT [PK_ExcludedLanguages]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'IncludedMentionTypes'
ALTER TABLE [dbo].[IncludedMentionTypes]
ADD CONSTRAINT [PK_IncludedMentionTypes]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'ExcludedMentionTypes'
ALTER TABLE [dbo].[ExcludedMentionTypes]
ADD CONSTRAINT [PK_ExcludedMentionTypes]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'IncludedReactScoreOveralls'
ALTER TABLE [dbo].[IncludedReactScoreOveralls]
ADD CONSTRAINT [PK_IncludedReactScoreOveralls]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'ExcludedReactScoreOveralls'
ALTER TABLE [dbo].[ExcludedReactScoreOveralls]
ADD CONSTRAINT [PK_ExcludedReactScoreOveralls]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'IncludedReactScoreHarms'
ALTER TABLE [dbo].[IncludedReactScoreHarms]
ADD CONSTRAINT [PK_IncludedReactScoreHarms]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'ExcludedReactScoreHarms'
ALTER TABLE [dbo].[ExcludedReactScoreHarms]
ADD CONSTRAINT [PK_ExcludedReactScoreHarms]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'IncludedReactScoreEmotionalities'
ALTER TABLE [dbo].[IncludedReactScoreEmotionalities]
ADD CONSTRAINT [PK_IncludedReactScoreEmotionalities]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'ExcludedReactScoreEmotionalities'
ALTER TABLE [dbo].[ExcludedReactScoreEmotionalities]
ADD CONSTRAINT [PK_ExcludedReactScoreEmotionalities]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'IncludedSentiments'
ALTER TABLE [dbo].[IncludedSentiments]
ADD CONSTRAINT [PK_IncludedSentiments]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'IncludedEmotions'
ALTER TABLE [dbo].[IncludedEmotions]
ADD CONSTRAINT [PK_IncludedEmotions]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'ExcludedEmotions'
ALTER TABLE [dbo].[ExcludedEmotions]
ADD CONSTRAINT [PK_ExcludedEmotions]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'IncludedSocialPanels'
ALTER TABLE [dbo].[IncludedSocialPanels]
ADD CONSTRAINT [PK_IncludedSocialPanels]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'ExcludedSocialPanels'
ALTER TABLE [dbo].[ExcludedSocialPanels]
ADD CONSTRAINT [PK_ExcludedSocialPanels]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'IncludedClientProjectSites'
ALTER TABLE [dbo].[IncludedClientProjectSites]
ADD CONSTRAINT [PK_IncludedClientProjectSites]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'ExcludedClientProjectSites'
ALTER TABLE [dbo].[ExcludedClientProjectSites]
ADD CONSTRAINT [PK_ExcludedClientProjectSites]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'IncludedTwitterMentionTypes'
ALTER TABLE [dbo].[IncludedTwitterMentionTypes]
ADD CONSTRAINT [PK_IncludedTwitterMentionTypes]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'ExcludedTwitterMentionTypes'
ALTER TABLE [dbo].[ExcludedTwitterMentionTypes]
ADD CONSTRAINT [PK_ExcludedTwitterMentionTypes]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'IncludedTwitterVerificationTypes'
ALTER TABLE [dbo].[IncludedTwitterVerificationTypes]
ADD CONSTRAINT [PK_IncludedTwitterVerificationTypes]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'ExcludedTwitterVerificationTypes'
ALTER TABLE [dbo].[ExcludedTwitterVerificationTypes]
ADD CONSTRAINT [PK_ExcludedTwitterVerificationTypes]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'IncludedTwitterAccountTypes'
ALTER TABLE [dbo].[IncludedTwitterAccountTypes]
ADD CONSTRAINT [PK_IncludedTwitterAccountTypes]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'ExcludedTwitterAccountTypes'
ALTER TABLE [dbo].[ExcludedTwitterAccountTypes]
ADD CONSTRAINT [PK_ExcludedTwitterAccountTypes]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'IncludedTwitterGenders'
ALTER TABLE [dbo].[IncludedTwitterGenders]
ADD CONSTRAINT [PK_IncludedTwitterGenders]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'IncludedTwitterProfessions'
ALTER TABLE [dbo].[IncludedTwitterProfessions]
ADD CONSTRAINT [PK_IncludedTwitterProfessions]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'ExcludedTwitterProfessions'
ALTER TABLE [dbo].[ExcludedTwitterProfessions]
ADD CONSTRAINT [PK_ExcludedTwitterProfessions]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'IncludedTwitterInterests'
ALTER TABLE [dbo].[IncludedTwitterInterests]
ADD CONSTRAINT [PK_IncludedTwitterInterests]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'ExcludedTwitterInterests'
ALTER TABLE [dbo].[ExcludedTwitterInterests]
ADD CONSTRAINT [PK_ExcludedTwitterInterests]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'IncludedSubTypes'
ALTER TABLE [dbo].[IncludedSubTypes]
ADD CONSTRAINT [PK_IncludedSubTypes]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'ExcludedSubTypes'
ALTER TABLE [dbo].[ExcludedSubTypes]
ADD CONSTRAINT [PK_ExcludedSubTypes]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'IncludedFaceBookMentionTypes'
ALTER TABLE [dbo].[IncludedFaceBookMentionTypes]
ADD CONSTRAINT [PK_IncludedFaceBookMentionTypes]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'ExcludedFaceBookMentionTypes'
ALTER TABLE [dbo].[ExcludedFaceBookMentionTypes]
ADD CONSTRAINT [PK_ExcludedFaceBookMentionTypes]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'IncludedInstagramMentionTypes'
ALTER TABLE [dbo].[IncludedInstagramMentionTypes]
ADD CONSTRAINT [PK_IncludedInstagramMentionTypes]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'ExcludedInstagramMentionTypes'
ALTER TABLE [dbo].[ExcludedInstagramMentionTypes]
ADD CONSTRAINT [PK_ExcludedInstagramMentionTypes]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'IncludedSubredditTopics'
ALTER TABLE [dbo].[IncludedSubredditTopics]
ADD CONSTRAINT [PK_IncludedSubredditTopics]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'ExcludedSubredditTopics'
ALTER TABLE [dbo].[ExcludedSubredditTopics]
ADD CONSTRAINT [PK_ExcludedSubredditTopics]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'AllowedMinAndMaxAndMultiOptionsMentionFilters'
ALTER TABLE [dbo].[AllowedMinAndMaxAndMultiOptionsMentionFilters]
ADD CONSTRAINT [PK_AllowedMinAndMaxAndMultiOptionsMentionFilters]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'ClientProjectTwitterTopAuthors'
ALTER TABLE [dbo].[ClientProjectTwitterTopAuthors]
ADD CONSTRAINT [PK_ClientProjectTwitterTopAuthors]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'IncludedDomains'
ALTER TABLE [dbo].[IncludedDomains]
ADD CONSTRAINT [PK_IncludedDomains]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [ElaqatEmployeeId] in table 'EmployeeActionHistories'
ALTER TABLE [dbo].[EmployeeActionHistories]
ADD CONSTRAINT [FK_ElaqatEmployeeEmployeeActionHistory]
    FOREIGN KEY ([ElaqatEmployeeId])
    REFERENCES [dbo].[ElaqatEmployees]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_ElaqatEmployeeEmployeeActionHistory'
CREATE INDEX [IX_FK_ElaqatEmployeeEmployeeActionHistory]
ON [dbo].[EmployeeActionHistories]
    ([ElaqatEmployeeId]);
GO

-- Creating foreign key on [ClientProjectId] in table 'ClientProjectQueries'
ALTER TABLE [dbo].[ClientProjectQueries]
ADD CONSTRAINT [FK_ClientProjectClientProjectQuerie]
    FOREIGN KEY ([ClientProjectId])
    REFERENCES [dbo].[ClientProjects]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_ClientProjectClientProjectQuerie'
CREATE INDEX [IX_FK_ClientProjectClientProjectQuerie]
ON [dbo].[ClientProjectQueries]
    ([ClientProjectId]);
GO

-- Creating foreign key on [ClientProjectId] in table 'ClientProjectCategories'
ALTER TABLE [dbo].[ClientProjectCategories]
ADD CONSTRAINT [FK_ClientProjectClientProjectCategory]
    FOREIGN KEY ([ClientProjectId])
    REFERENCES [dbo].[ClientProjects]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_ClientProjectClientProjectCategory'
CREATE INDEX [IX_FK_ClientProjectClientProjectCategory]
ON [dbo].[ClientProjectCategories]
    ([ClientProjectId]);
GO

-- Creating foreign key on [ClientProjectCategoryId] in table 'ClientProjectSubCategories'
ALTER TABLE [dbo].[ClientProjectSubCategories]
ADD CONSTRAINT [FK_ClientProjectCategoryClientProjectSubCategory]
    FOREIGN KEY ([ClientProjectCategoryId])
    REFERENCES [dbo].[ClientProjectCategories]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_ClientProjectCategoryClientProjectSubCategory'
CREATE INDEX [IX_FK_ClientProjectCategoryClientProjectSubCategory]
ON [dbo].[ClientProjectSubCategories]
    ([ClientProjectCategoryId]);
GO

-- Creating foreign key on [ElaqatClientId] in table 'ClientProjects'
ALTER TABLE [dbo].[ClientProjects]
ADD CONSTRAINT [FK_ElaqatClientClientProject]
    FOREIGN KEY ([ElaqatClientId])
    REFERENCES [dbo].[ElaqatClients]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_ElaqatClientClientProject'
CREATE INDEX [IX_FK_ElaqatClientClientProject]
ON [dbo].[ClientProjects]
    ([ElaqatClientId]);
GO

-- Creating foreign key on [ClientProjectCategoryId] in table 'ClintProjectCtegoryQueryIDs'
ALTER TABLE [dbo].[ClintProjectCtegoryQueryIDs]
ADD CONSTRAINT [FK_ClientProjectCategoryClintProjectCtegoryQueryID]
    FOREIGN KEY ([ClientProjectCategoryId])
    REFERENCES [dbo].[ClientProjectCategories]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_ClientProjectCategoryClintProjectCtegoryQueryID'
CREATE INDEX [IX_FK_ClientProjectCategoryClintProjectCtegoryQueryID]
ON [dbo].[ClintProjectCtegoryQueryIDs]
    ([ClientProjectCategoryId]);
GO

-- Creating foreign key on [ClientProjectQuerieId] in table 'QueryHighLightTerms'
ALTER TABLE [dbo].[QueryHighLightTerms]
ADD CONSTRAINT [FK_ClientProjectQuerieQueryHighLightTerm]
    FOREIGN KEY ([ClientProjectQuerieId])
    REFERENCES [dbo].[ClientProjectQueries]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_ClientProjectQuerieQueryHighLightTerm'
CREATE INDEX [IX_FK_ClientProjectQuerieQueryHighLightTerm]
ON [dbo].[QueryHighLightTerms]
    ([ClientProjectQuerieId]);
GO

-- Creating foreign key on [ClientProjectQuerieId] in table 'QueryContentSources'
ALTER TABLE [dbo].[QueryContentSources]
ADD CONSTRAINT [FK_ClientProjectQuerieQueryContentSource]
    FOREIGN KEY ([ClientProjectQuerieId])
    REFERENCES [dbo].[ClientProjectQueries]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_ClientProjectQuerieQueryContentSource'
CREATE INDEX [IX_FK_ClientProjectQuerieQueryContentSource]
ON [dbo].[QueryContentSources]
    ([ClientProjectQuerieId]);
GO

-- Creating foreign key on [ClientProjectQuerieId] in table 'QueryLanguages'
ALTER TABLE [dbo].[QueryLanguages]
ADD CONSTRAINT [FK_ClientProjectQuerieQueryLanguage]
    FOREIGN KEY ([ClientProjectQuerieId])
    REFERENCES [dbo].[ClientProjectQueries]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_ClientProjectQuerieQueryLanguage'
CREATE INDEX [IX_FK_ClientProjectQuerieQueryLanguage]
ON [dbo].[QueryLanguages]
    ([ClientProjectQuerieId]);
GO

-- Creating foreign key on [ClientProjectId] in table 'AuthorGroups'
ALTER TABLE [dbo].[AuthorGroups]
ADD CONSTRAINT [FK_ClientProjectAuthorGroup]
    FOREIGN KEY ([ClientProjectId])
    REFERENCES [dbo].[ClientProjects]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_ClientProjectAuthorGroup'
CREATE INDEX [IX_FK_ClientProjectAuthorGroup]
ON [dbo].[AuthorGroups]
    ([ClientProjectId]);
GO

-- Creating foreign key on [ClientProjectId] in table 'ProjectLocations'
ALTER TABLE [dbo].[ProjectLocations]
ADD CONSTRAINT [FK_ClientProjectProjectLocation]
    FOREIGN KEY ([ClientProjectId])
    REFERENCES [dbo].[ClientProjects]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_ClientProjectProjectLocation'
CREATE INDEX [IX_FK_ClientProjectProjectLocation]
ON [dbo].[ProjectLocations]
    ([ClientProjectId]);
GO

-- Creating foreign key on [ClientProjectId] in table 'ProjectRuleTags'
ALTER TABLE [dbo].[ProjectRuleTags]
ADD CONSTRAINT [FK_ClientProjectProjectRuleTag]
    FOREIGN KEY ([ClientProjectId])
    REFERENCES [dbo].[ClientProjects]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_ClientProjectProjectRuleTag'
CREATE INDEX [IX_FK_ClientProjectProjectRuleTag]
ON [dbo].[ProjectRuleTags]
    ([ClientProjectId]);
GO

-- Creating foreign key on [GlobalMetricId] in table 'AccountTypes'
ALTER TABLE [dbo].[AccountTypes]
ADD CONSTRAINT [FK_GlobalMetricAccountType]
    FOREIGN KEY ([GlobalMetricId])
    REFERENCES [dbo].[GlobalMetrics]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_GlobalMetricAccountType'
CREATE INDEX [IX_FK_GlobalMetricAccountType]
ON [dbo].[AccountTypes]
    ([GlobalMetricId]);
GO

-- Creating foreign key on [GlobalMetricId] in table 'Sentiments'
ALTER TABLE [dbo].[Sentiments]
ADD CONSTRAINT [FK_GlobalMetricSentiment]
    FOREIGN KEY ([GlobalMetricId])
    REFERENCES [dbo].[GlobalMetrics]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_GlobalMetricSentiment'
CREATE INDEX [IX_FK_GlobalMetricSentiment]
ON [dbo].[Sentiments]
    ([GlobalMetricId]);
GO

-- Creating foreign key on [GlobalMetricId] in table 'Genders'
ALTER TABLE [dbo].[Genders]
ADD CONSTRAINT [FK_GlobalMetricGender]
    FOREIGN KEY ([GlobalMetricId])
    REFERENCES [dbo].[GlobalMetrics]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_GlobalMetricGender'
CREATE INDEX [IX_FK_GlobalMetricGender]
ON [dbo].[Genders]
    ([GlobalMetricId]);
GO

-- Creating foreign key on [GlobalMetricId] in table 'ContentSources'
ALTER TABLE [dbo].[ContentSources]
ADD CONSTRAINT [FK_GlobalMetricContentSource]
    FOREIGN KEY ([GlobalMetricId])
    REFERENCES [dbo].[GlobalMetrics]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_GlobalMetricContentSource'
CREATE INDEX [IX_FK_GlobalMetricContentSource]
ON [dbo].[ContentSources]
    ([GlobalMetricId]);
GO

-- Creating foreign key on [GlobalMetricId] in table 'SubTypes'
ALTER TABLE [dbo].[SubTypes]
ADD CONSTRAINT [FK_GlobalMetricSubType]
    FOREIGN KEY ([GlobalMetricId])
    REFERENCES [dbo].[GlobalMetrics]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_GlobalMetricSubType'
CREATE INDEX [IX_FK_GlobalMetricSubType]
ON [dbo].[SubTypes]
    ([GlobalMetricId]);
GO

-- Creating foreign key on [GlobalMetricId] in table 'SourceTypes'
ALTER TABLE [dbo].[SourceTypes]
ADD CONSTRAINT [FK_GlobalMetricSourceType]
    FOREIGN KEY ([GlobalMetricId])
    REFERENCES [dbo].[GlobalMetrics]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_GlobalMetricSourceType'
CREATE INDEX [IX_FK_GlobalMetricSourceType]
ON [dbo].[SourceTypes]
    ([GlobalMetricId]);
GO

-- Creating foreign key on [ClientProjectId] in table 'ProjectMentions'
ALTER TABLE [dbo].[ProjectMentions]
ADD CONSTRAINT [FK_ClientProjectProjectMention]
    FOREIGN KEY ([ClientProjectId])
    REFERENCES [dbo].[ClientProjects]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_ClientProjectProjectMention'
CREATE INDEX [IX_FK_ClientProjectProjectMention]
ON [dbo].[ProjectMentions]
    ([ClientProjectId]);
GO

-- Creating foreign key on [ProjectMentionId] in table 'MentionSubCategoryDetails'
ALTER TABLE [dbo].[MentionSubCategoryDetails]
ADD CONSTRAINT [FK_ProjectMentionMentionSubCategoryDetail]
    FOREIGN KEY ([ProjectMentionId])
    REFERENCES [dbo].[ProjectMentions]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_ProjectMentionMentionSubCategoryDetail'
CREATE INDEX [IX_FK_ProjectMentionMentionSubCategoryDetail]
ON [dbo].[MentionSubCategoryDetails]
    ([ProjectMentionId]);
GO

-- Creating foreign key on [GlobalMetricId] in table 'MentionTypes'
ALTER TABLE [dbo].[MentionTypes]
ADD CONSTRAINT [FK_GlobalMetricMentionType]
    FOREIGN KEY ([GlobalMetricId])
    REFERENCES [dbo].[GlobalMetrics]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_GlobalMetricMentionType'
CREATE INDEX [IX_FK_GlobalMetricMentionType]
ON [dbo].[MentionTypes]
    ([GlobalMetricId]);
GO

-- Creating foreign key on [GlobalMetricId] in table 'Emotions'
ALTER TABLE [dbo].[Emotions]
ADD CONSTRAINT [FK_GlobalMetricEmotion]
    FOREIGN KEY ([GlobalMetricId])
    REFERENCES [dbo].[GlobalMetrics]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_GlobalMetricEmotion'
CREATE INDEX [IX_FK_GlobalMetricEmotion]
ON [dbo].[Emotions]
    ([GlobalMetricId]);
GO

-- Creating foreign key on [GlobalMetricId] in table 'MentionStatus'
ALTER TABLE [dbo].[MentionStatus]
ADD CONSTRAINT [FK_GlobalMetricMentionStatus]
    FOREIGN KEY ([GlobalMetricId])
    REFERENCES [dbo].[GlobalMetrics]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_GlobalMetricMentionStatus'
CREATE INDEX [IX_FK_GlobalMetricMentionStatus]
ON [dbo].[MentionStatus]
    ([GlobalMetricId]);
GO

-- Creating foreign key on [GlobalMetricId] in table 'Priorities'
ALTER TABLE [dbo].[Priorities]
ADD CONSTRAINT [FK_GlobalMetricPriority]
    FOREIGN KEY ([GlobalMetricId])
    REFERENCES [dbo].[GlobalMetrics]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_GlobalMetricPriority'
CREATE INDEX [IX_FK_GlobalMetricPriority]
ON [dbo].[Priorities]
    ([GlobalMetricId]);
GO

-- Creating foreign key on [GlobalMetricId] in table 'TwitterUserRoles'
ALTER TABLE [dbo].[TwitterUserRoles]
ADD CONSTRAINT [FK_GlobalMetricTwitterUserRole]
    FOREIGN KEY ([GlobalMetricId])
    REFERENCES [dbo].[GlobalMetrics]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_GlobalMetricTwitterUserRole'
CREATE INDEX [IX_FK_GlobalMetricTwitterUserRole]
ON [dbo].[TwitterUserRoles]
    ([GlobalMetricId]);
GO

-- Creating foreign key on [GlobalMetricId] in table 'TwitterMentionTypes'
ALTER TABLE [dbo].[TwitterMentionTypes]
ADD CONSTRAINT [FK_GlobalMetricTwitterMentionType]
    FOREIGN KEY ([GlobalMetricId])
    REFERENCES [dbo].[GlobalMetrics]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_GlobalMetricTwitterMentionType'
CREATE INDEX [IX_FK_GlobalMetricTwitterMentionType]
ON [dbo].[TwitterMentionTypes]
    ([GlobalMetricId]);
GO

-- Creating foreign key on [GlobalMetricId] in table 'TwitterVerificationTypes'
ALTER TABLE [dbo].[TwitterVerificationTypes]
ADD CONSTRAINT [FK_GlobalMetricTwitterVerificationType]
    FOREIGN KEY ([GlobalMetricId])
    REFERENCES [dbo].[GlobalMetrics]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_GlobalMetricTwitterVerificationType'
CREATE INDEX [IX_FK_GlobalMetricTwitterVerificationType]
ON [dbo].[TwitterVerificationTypes]
    ([GlobalMetricId]);
GO

-- Creating foreign key on [GlobalMetricId] in table 'FaceBookUserRoles'
ALTER TABLE [dbo].[FaceBookUserRoles]
ADD CONSTRAINT [FK_GlobalMetricFaceBookUserRole]
    FOREIGN KEY ([GlobalMetricId])
    REFERENCES [dbo].[GlobalMetrics]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_GlobalMetricFaceBookUserRole'
CREATE INDEX [IX_FK_GlobalMetricFaceBookUserRole]
ON [dbo].[FaceBookUserRoles]
    ([GlobalMetricId]);
GO

-- Creating foreign key on [GlobalMetricId] in table 'FaceBookMentionTypes'
ALTER TABLE [dbo].[FaceBookMentionTypes]
ADD CONSTRAINT [FK_GlobalMetricFaceBookMentionType]
    FOREIGN KEY ([GlobalMetricId])
    REFERENCES [dbo].[GlobalMetrics]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_GlobalMetricFaceBookMentionType'
CREATE INDEX [IX_FK_GlobalMetricFaceBookMentionType]
ON [dbo].[FaceBookMentionTypes]
    ([GlobalMetricId]);
GO

-- Creating foreign key on [GlobalMetricId] in table 'InstagramMentionTypes'
ALTER TABLE [dbo].[InstagramMentionTypes]
ADD CONSTRAINT [FK_GlobalMetricInstagramMentionType]
    FOREIGN KEY ([GlobalMetricId])
    REFERENCES [dbo].[GlobalMetrics]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_GlobalMetricInstagramMentionType'
CREATE INDEX [IX_FK_GlobalMetricInstagramMentionType]
ON [dbo].[InstagramMentionTypes]
    ([GlobalMetricId]);
GO

-- Creating foreign key on [GlobalMetricId] in table 'SubredditTopics'
ALTER TABLE [dbo].[SubredditTopics]
ADD CONSTRAINT [FK_GlobalMetricSubredditTopics]
    FOREIGN KEY ([GlobalMetricId])
    REFERENCES [dbo].[GlobalMetrics]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_GlobalMetricSubredditTopics'
CREATE INDEX [IX_FK_GlobalMetricSubredditTopics]
ON [dbo].[SubredditTopics]
    ([GlobalMetricId]);
GO

-- Creating foreign key on [GlobalMetricId] in table 'SocialPanels'
ALTER TABLE [dbo].[SocialPanels]
ADD CONSTRAINT [FK_GlobalMetricSocialPanel]
    FOREIGN KEY ([GlobalMetricId])
    REFERENCES [dbo].[GlobalMetrics]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_GlobalMetricSocialPanel'
CREATE INDEX [IX_FK_GlobalMetricSocialPanel]
ON [dbo].[SocialPanels]
    ([GlobalMetricId]);
GO

-- Creating foreign key on [GlobalMetricId] in table 'Assignments'
ALTER TABLE [dbo].[Assignments]
ADD CONSTRAINT [FK_GlobalMetricAssignment]
    FOREIGN KEY ([GlobalMetricId])
    REFERENCES [dbo].[GlobalMetrics]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_GlobalMetricAssignment'
CREATE INDEX [IX_FK_GlobalMetricAssignment]
ON [dbo].[Assignments]
    ([GlobalMetricId]);
GO

-- Creating foreign key on [GlobalMetricId] in table 'CheckedMentions'
ALTER TABLE [dbo].[CheckedMentions]
ADD CONSTRAINT [FK_GlobalMetricCheckedMention]
    FOREIGN KEY ([GlobalMetricId])
    REFERENCES [dbo].[GlobalMetrics]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_GlobalMetricCheckedMention'
CREATE INDEX [IX_FK_GlobalMetricCheckedMention]
ON [dbo].[CheckedMentions]
    ([GlobalMetricId]);
GO

-- Creating foreign key on [ClientProjectId] in table 'GlobalMetrics'
ALTER TABLE [dbo].[GlobalMetrics]
ADD CONSTRAINT [FK_ClientProjectGlobalMetric]
    FOREIGN KEY ([ClientProjectId])
    REFERENCES [dbo].[ClientProjects]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_ClientProjectGlobalMetric'
CREATE INDEX [IX_FK_ClientProjectGlobalMetric]
ON [dbo].[GlobalMetrics]
    ([ClientProjectId]);
GO

-- Creating foreign key on [GlobalMetricId] in table 'ReactScoreOveralls'
ALTER TABLE [dbo].[ReactScoreOveralls]
ADD CONSTRAINT [FK_GlobalMetricReactScoreOverall]
    FOREIGN KEY ([GlobalMetricId])
    REFERENCES [dbo].[GlobalMetrics]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_GlobalMetricReactScoreOverall'
CREATE INDEX [IX_FK_GlobalMetricReactScoreOverall]
ON [dbo].[ReactScoreOveralls]
    ([GlobalMetricId]);
GO

-- Creating foreign key on [ClientProjectId] in table 'ClientProjectSites'
ALTER TABLE [dbo].[ClientProjectSites]
ADD CONSTRAINT [FK_ClientProjectClientProjectSites]
    FOREIGN KEY ([ClientProjectId])
    REFERENCES [dbo].[ClientProjects]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_ClientProjectClientProjectSites'
CREATE INDEX [IX_FK_ClientProjectClientProjectSites]
ON [dbo].[ClientProjectSites]
    ([ClientProjectId]);
GO

-- Creating foreign key on [ClientProjectSitesId] in table 'SiteDomains'
ALTER TABLE [dbo].[SiteDomains]
ADD CONSTRAINT [FK_ClientProjectSitesSiteDomain]
    FOREIGN KEY ([ClientProjectSitesId])
    REFERENCES [dbo].[ClientProjectSites]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_ClientProjectSitesSiteDomain'
CREATE INDEX [IX_FK_ClientProjectSitesSiteDomain]
ON [dbo].[SiteDomains]
    ([ClientProjectSitesId]);
GO

-- Creating foreign key on [GlobalMetricId] in table 'RedditNSFWs'
ALTER TABLE [dbo].[RedditNSFWs]
ADD CONSTRAINT [FK_GlobalMetricRedditNSFW]
    FOREIGN KEY ([GlobalMetricId])
    REFERENCES [dbo].[GlobalMetrics]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_GlobalMetricRedditNSFW'
CREATE INDEX [IX_FK_GlobalMetricRedditNSFW]
ON [dbo].[RedditNSFWs]
    ([GlobalMetricId]);
GO

-- Creating foreign key on [GlobalMetricId] in table 'RedditPolls'
ALTER TABLE [dbo].[RedditPolls]
ADD CONSTRAINT [FK_GlobalMetricRedditPoll]
    FOREIGN KEY ([GlobalMetricId])
    REFERENCES [dbo].[GlobalMetrics]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_GlobalMetricRedditPoll'
CREATE INDEX [IX_FK_GlobalMetricRedditPoll]
ON [dbo].[RedditPolls]
    ([GlobalMetricId]);
GO

-- Creating foreign key on [GlobalMetricId] in table 'TwitterProfessions'
ALTER TABLE [dbo].[TwitterProfessions]
ADD CONSTRAINT [FK_GlobalMetricTwitterProfession]
    FOREIGN KEY ([GlobalMetricId])
    REFERENCES [dbo].[GlobalMetrics]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_GlobalMetricTwitterProfession'
CREATE INDEX [IX_FK_GlobalMetricTwitterProfession]
ON [dbo].[TwitterProfessions]
    ([GlobalMetricId]);
GO

-- Creating foreign key on [GlobalMetricId] in table 'TwitterInterests'
ALTER TABLE [dbo].[TwitterInterests]
ADD CONSTRAINT [FK_GlobalMetricTwitterInterest]
    FOREIGN KEY ([GlobalMetricId])
    REFERENCES [dbo].[GlobalMetrics]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_GlobalMetricTwitterInterest'
CREATE INDEX [IX_FK_GlobalMetricTwitterInterest]
ON [dbo].[TwitterInterests]
    ([GlobalMetricId]);
GO

-- Creating foreign key on [GlobalMetricId] in table 'TwitterVerifications'
ALTER TABLE [dbo].[TwitterVerifications]
ADD CONSTRAINT [FK_GlobalMetricTwitterVerification]
    FOREIGN KEY ([GlobalMetricId])
    REFERENCES [dbo].[GlobalMetrics]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_GlobalMetricTwitterVerification'
CREATE INDEX [IX_FK_GlobalMetricTwitterVerification]
ON [dbo].[TwitterVerifications]
    ([GlobalMetricId]);
GO

-- Creating foreign key on [MentionFilterId] in table 'IncludedCategories'
ALTER TABLE [dbo].[IncludedCategories]
ADD CONSTRAINT [FK_MentionFilterIncludedCategory]
    FOREIGN KEY ([MentionFilterId])
    REFERENCES [dbo].[MentionFilters]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_MentionFilterIncludedCategory'
CREATE INDEX [IX_FK_MentionFilterIncludedCategory]
ON [dbo].[IncludedCategories]
    ([MentionFilterId]);
GO

-- Creating foreign key on [MentionFilterId] in table 'ExcludedCategories'
ALTER TABLE [dbo].[ExcludedCategories]
ADD CONSTRAINT [FK_MentionFilterExcludedCategory]
    FOREIGN KEY ([MentionFilterId])
    REFERENCES [dbo].[MentionFilters]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_MentionFilterExcludedCategory'
CREATE INDEX [IX_FK_MentionFilterExcludedCategory]
ON [dbo].[ExcludedCategories]
    ([MentionFilterId]);
GO

-- Creating foreign key on [MentionFilterId] in table 'IncludedQueries'
ALTER TABLE [dbo].[IncludedQueries]
ADD CONSTRAINT [FK_MentionFilterIncludedQuery]
    FOREIGN KEY ([MentionFilterId])
    REFERENCES [dbo].[MentionFilters]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_MentionFilterIncludedQuery'
CREATE INDEX [IX_FK_MentionFilterIncludedQuery]
ON [dbo].[IncludedQueries]
    ([MentionFilterId]);
GO

-- Creating foreign key on [MentionFilterId] in table 'ExcludedQueries'
ALTER TABLE [dbo].[ExcludedQueries]
ADD CONSTRAINT [FK_MentionFilterExcludedQuery]
    FOREIGN KEY ([MentionFilterId])
    REFERENCES [dbo].[MentionFilters]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_MentionFilterExcludedQuery'
CREATE INDEX [IX_FK_MentionFilterExcludedQuery]
ON [dbo].[ExcludedQueries]
    ([MentionFilterId]);
GO

-- Creating foreign key on [MentionFilterId] in table 'IncludedSubCatgories'
ALTER TABLE [dbo].[IncludedSubCatgories]
ADD CONSTRAINT [FK_MentionFilterIncludedSubCatgory]
    FOREIGN KEY ([MentionFilterId])
    REFERENCES [dbo].[MentionFilters]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_MentionFilterIncludedSubCatgory'
CREATE INDEX [IX_FK_MentionFilterIncludedSubCatgory]
ON [dbo].[IncludedSubCatgories]
    ([MentionFilterId]);
GO

-- Creating foreign key on [MentionFilterId] in table 'ExcludedSubCatgories'
ALTER TABLE [dbo].[ExcludedSubCatgories]
ADD CONSTRAINT [FK_MentionFilterExcludedSubCatgory]
    FOREIGN KEY ([MentionFilterId])
    REFERENCES [dbo].[MentionFilters]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_MentionFilterExcludedSubCatgory'
CREATE INDEX [IX_FK_MentionFilterExcludedSubCatgory]
ON [dbo].[ExcludedSubCatgories]
    ([MentionFilterId]);
GO

-- Creating foreign key on [MentionFilterId] in table 'IncludedRuleTags'
ALTER TABLE [dbo].[IncludedRuleTags]
ADD CONSTRAINT [FK_MentionFilterIncludedRuleTag]
    FOREIGN KEY ([MentionFilterId])
    REFERENCES [dbo].[MentionFilters]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_MentionFilterIncludedRuleTag'
CREATE INDEX [IX_FK_MentionFilterIncludedRuleTag]
ON [dbo].[IncludedRuleTags]
    ([MentionFilterId]);
GO

-- Creating foreign key on [MentionFilterId] in table 'ExcludedRuleTags'
ALTER TABLE [dbo].[ExcludedRuleTags]
ADD CONSTRAINT [FK_MentionFilterExcludedRuleTag]
    FOREIGN KEY ([MentionFilterId])
    REFERENCES [dbo].[MentionFilters]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_MentionFilterExcludedRuleTag'
CREATE INDEX [IX_FK_MentionFilterExcludedRuleTag]
ON [dbo].[ExcludedRuleTags]
    ([MentionFilterId]);
GO

-- Creating foreign key on [MentionFilterId] in table 'IncludedContentSources'
ALTER TABLE [dbo].[IncludedContentSources]
ADD CONSTRAINT [FK_MentionFilterIncludedContentSource]
    FOREIGN KEY ([MentionFilterId])
    REFERENCES [dbo].[MentionFilters]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_MentionFilterIncludedContentSource'
CREATE INDEX [IX_FK_MentionFilterIncludedContentSource]
ON [dbo].[IncludedContentSources]
    ([MentionFilterId]);
GO

-- Creating foreign key on [MentionFilterId] in table 'ExcludedContentSources'
ALTER TABLE [dbo].[ExcludedContentSources]
ADD CONSTRAINT [FK_MentionFilterExcludedContentSource]
    FOREIGN KEY ([MentionFilterId])
    REFERENCES [dbo].[MentionFilters]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_MentionFilterExcludedContentSource'
CREATE INDEX [IX_FK_MentionFilterExcludedContentSource]
ON [dbo].[ExcludedContentSources]
    ([MentionFilterId]);
GO

-- Creating foreign key on [MentionFilterId] in table 'IncludedAuthorGroups'
ALTER TABLE [dbo].[IncludedAuthorGroups]
ADD CONSTRAINT [FK_MentionFilterIncludedAuthorGroup]
    FOREIGN KEY ([MentionFilterId])
    REFERENCES [dbo].[MentionFilters]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_MentionFilterIncludedAuthorGroup'
CREATE INDEX [IX_FK_MentionFilterIncludedAuthorGroup]
ON [dbo].[IncludedAuthorGroups]
    ([MentionFilterId]);
GO

-- Creating foreign key on [MentionFilterId] in table 'ExcludedAuthorGroups'
ALTER TABLE [dbo].[ExcludedAuthorGroups]
ADD CONSTRAINT [FK_MentionFilterExcludedAuthorGroup]
    FOREIGN KEY ([MentionFilterId])
    REFERENCES [dbo].[MentionFilters]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_MentionFilterExcludedAuthorGroup'
CREATE INDEX [IX_FK_MentionFilterExcludedAuthorGroup]
ON [dbo].[ExcludedAuthorGroups]
    ([MentionFilterId]);
GO

-- Creating foreign key on [MentionFilterId] in table 'IncludedLocations'
ALTER TABLE [dbo].[IncludedLocations]
ADD CONSTRAINT [FK_MentionFilterIncludedLocation]
    FOREIGN KEY ([MentionFilterId])
    REFERENCES [dbo].[MentionFilters]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_MentionFilterIncludedLocation'
CREATE INDEX [IX_FK_MentionFilterIncludedLocation]
ON [dbo].[IncludedLocations]
    ([MentionFilterId]);
GO

-- Creating foreign key on [MentionFilterId] in table 'ExcludedLocations'
ALTER TABLE [dbo].[ExcludedLocations]
ADD CONSTRAINT [FK_MentionFilterExcludedLocation]
    FOREIGN KEY ([MentionFilterId])
    REFERENCES [dbo].[MentionFilters]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_MentionFilterExcludedLocation'
CREATE INDEX [IX_FK_MentionFilterExcludedLocation]
ON [dbo].[ExcludedLocations]
    ([MentionFilterId]);
GO

-- Creating foreign key on [MentionFilterId] in table 'IncludedLanguages'
ALTER TABLE [dbo].[IncludedLanguages]
ADD CONSTRAINT [FK_MentionFilterIncludedLanguage]
    FOREIGN KEY ([MentionFilterId])
    REFERENCES [dbo].[MentionFilters]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_MentionFilterIncludedLanguage'
CREATE INDEX [IX_FK_MentionFilterIncludedLanguage]
ON [dbo].[IncludedLanguages]
    ([MentionFilterId]);
GO

-- Creating foreign key on [MentionFilterId] in table 'ExcludedLanguages'
ALTER TABLE [dbo].[ExcludedLanguages]
ADD CONSTRAINT [FK_MentionFilterExcludedLanguage]
    FOREIGN KEY ([MentionFilterId])
    REFERENCES [dbo].[MentionFilters]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_MentionFilterExcludedLanguage'
CREATE INDEX [IX_FK_MentionFilterExcludedLanguage]
ON [dbo].[ExcludedLanguages]
    ([MentionFilterId]);
GO

-- Creating foreign key on [MentionFilterId] in table 'IncludedMentionTypes'
ALTER TABLE [dbo].[IncludedMentionTypes]
ADD CONSTRAINT [FK_MentionFilterIncludedMentionType]
    FOREIGN KEY ([MentionFilterId])
    REFERENCES [dbo].[MentionFilters]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_MentionFilterIncludedMentionType'
CREATE INDEX [IX_FK_MentionFilterIncludedMentionType]
ON [dbo].[IncludedMentionTypes]
    ([MentionFilterId]);
GO

-- Creating foreign key on [MentionFilterId] in table 'ExcludedMentionTypes'
ALTER TABLE [dbo].[ExcludedMentionTypes]
ADD CONSTRAINT [FK_MentionFilterExcludedMentionType]
    FOREIGN KEY ([MentionFilterId])
    REFERENCES [dbo].[MentionFilters]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_MentionFilterExcludedMentionType'
CREATE INDEX [IX_FK_MentionFilterExcludedMentionType]
ON [dbo].[ExcludedMentionTypes]
    ([MentionFilterId]);
GO

-- Creating foreign key on [MentionFilterId] in table 'IncludedReactScoreOveralls'
ALTER TABLE [dbo].[IncludedReactScoreOveralls]
ADD CONSTRAINT [FK_MentionFilterIncludedReactScoreOverall]
    FOREIGN KEY ([MentionFilterId])
    REFERENCES [dbo].[MentionFilters]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_MentionFilterIncludedReactScoreOverall'
CREATE INDEX [IX_FK_MentionFilterIncludedReactScoreOverall]
ON [dbo].[IncludedReactScoreOveralls]
    ([MentionFilterId]);
GO

-- Creating foreign key on [MentionFilterId] in table 'ExcludedReactScoreOveralls'
ALTER TABLE [dbo].[ExcludedReactScoreOveralls]
ADD CONSTRAINT [FK_MentionFilterExcludedReactScoreOverall]
    FOREIGN KEY ([MentionFilterId])
    REFERENCES [dbo].[MentionFilters]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_MentionFilterExcludedReactScoreOverall'
CREATE INDEX [IX_FK_MentionFilterExcludedReactScoreOverall]
ON [dbo].[ExcludedReactScoreOveralls]
    ([MentionFilterId]);
GO

-- Creating foreign key on [MentionFilterId] in table 'IncludedReactScoreHarms'
ALTER TABLE [dbo].[IncludedReactScoreHarms]
ADD CONSTRAINT [FK_MentionFilterIncludedReactScoreHarm]
    FOREIGN KEY ([MentionFilterId])
    REFERENCES [dbo].[MentionFilters]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_MentionFilterIncludedReactScoreHarm'
CREATE INDEX [IX_FK_MentionFilterIncludedReactScoreHarm]
ON [dbo].[IncludedReactScoreHarms]
    ([MentionFilterId]);
GO

-- Creating foreign key on [MentionFilterId] in table 'ExcludedReactScoreHarms'
ALTER TABLE [dbo].[ExcludedReactScoreHarms]
ADD CONSTRAINT [FK_MentionFilterExcludedReactScoreHarm]
    FOREIGN KEY ([MentionFilterId])
    REFERENCES [dbo].[MentionFilters]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_MentionFilterExcludedReactScoreHarm'
CREATE INDEX [IX_FK_MentionFilterExcludedReactScoreHarm]
ON [dbo].[ExcludedReactScoreHarms]
    ([MentionFilterId]);
GO

-- Creating foreign key on [MentionFilterId] in table 'IncludedReactScoreEmotionalities'
ALTER TABLE [dbo].[IncludedReactScoreEmotionalities]
ADD CONSTRAINT [FK_MentionFilterIncludedReactScoreEmotionality]
    FOREIGN KEY ([MentionFilterId])
    REFERENCES [dbo].[MentionFilters]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_MentionFilterIncludedReactScoreEmotionality'
CREATE INDEX [IX_FK_MentionFilterIncludedReactScoreEmotionality]
ON [dbo].[IncludedReactScoreEmotionalities]
    ([MentionFilterId]);
GO

-- Creating foreign key on [MentionFilterId] in table 'ExcludedReactScoreEmotionalities'
ALTER TABLE [dbo].[ExcludedReactScoreEmotionalities]
ADD CONSTRAINT [FK_MentionFilterExcludedReactScoreEmotionality]
    FOREIGN KEY ([MentionFilterId])
    REFERENCES [dbo].[MentionFilters]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_MentionFilterExcludedReactScoreEmotionality'
CREATE INDEX [IX_FK_MentionFilterExcludedReactScoreEmotionality]
ON [dbo].[ExcludedReactScoreEmotionalities]
    ([MentionFilterId]);
GO

-- Creating foreign key on [MentionFilterId] in table 'IncludedSentiments'
ALTER TABLE [dbo].[IncludedSentiments]
ADD CONSTRAINT [FK_MentionFilterIncludedSentiment]
    FOREIGN KEY ([MentionFilterId])
    REFERENCES [dbo].[MentionFilters]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_MentionFilterIncludedSentiment'
CREATE INDEX [IX_FK_MentionFilterIncludedSentiment]
ON [dbo].[IncludedSentiments]
    ([MentionFilterId]);
GO

-- Creating foreign key on [MentionFilterId] in table 'IncludedEmotions'
ALTER TABLE [dbo].[IncludedEmotions]
ADD CONSTRAINT [FK_MentionFilterIncludedEmotion]
    FOREIGN KEY ([MentionFilterId])
    REFERENCES [dbo].[MentionFilters]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_MentionFilterIncludedEmotion'
CREATE INDEX [IX_FK_MentionFilterIncludedEmotion]
ON [dbo].[IncludedEmotions]
    ([MentionFilterId]);
GO

-- Creating foreign key on [MentionFilterId] in table 'ExcludedEmotions'
ALTER TABLE [dbo].[ExcludedEmotions]
ADD CONSTRAINT [FK_MentionFilterExcludedEmotion]
    FOREIGN KEY ([MentionFilterId])
    REFERENCES [dbo].[MentionFilters]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_MentionFilterExcludedEmotion'
CREATE INDEX [IX_FK_MentionFilterExcludedEmotion]
ON [dbo].[ExcludedEmotions]
    ([MentionFilterId]);
GO

-- Creating foreign key on [MentionFilterId] in table 'IncludedSocialPanels'
ALTER TABLE [dbo].[IncludedSocialPanels]
ADD CONSTRAINT [FK_MentionFilterIncludedSocialPanel]
    FOREIGN KEY ([MentionFilterId])
    REFERENCES [dbo].[MentionFilters]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_MentionFilterIncludedSocialPanel'
CREATE INDEX [IX_FK_MentionFilterIncludedSocialPanel]
ON [dbo].[IncludedSocialPanels]
    ([MentionFilterId]);
GO

-- Creating foreign key on [MentionFilterId] in table 'ExcludedSocialPanels'
ALTER TABLE [dbo].[ExcludedSocialPanels]
ADD CONSTRAINT [FK_MentionFilterExcludedSocialPanel]
    FOREIGN KEY ([MentionFilterId])
    REFERENCES [dbo].[MentionFilters]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_MentionFilterExcludedSocialPanel'
CREATE INDEX [IX_FK_MentionFilterExcludedSocialPanel]
ON [dbo].[ExcludedSocialPanels]
    ([MentionFilterId]);
GO

-- Creating foreign key on [MentionFilterId] in table 'IncludedClientProjectSites'
ALTER TABLE [dbo].[IncludedClientProjectSites]
ADD CONSTRAINT [FK_MentionFilterIncludedClientProjectSite]
    FOREIGN KEY ([MentionFilterId])
    REFERENCES [dbo].[MentionFilters]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_MentionFilterIncludedClientProjectSite'
CREATE INDEX [IX_FK_MentionFilterIncludedClientProjectSite]
ON [dbo].[IncludedClientProjectSites]
    ([MentionFilterId]);
GO

-- Creating foreign key on [MentionFilterId] in table 'ExcludedClientProjectSites'
ALTER TABLE [dbo].[ExcludedClientProjectSites]
ADD CONSTRAINT [FK_MentionFilterExcludedClientProjectSite]
    FOREIGN KEY ([MentionFilterId])
    REFERENCES [dbo].[MentionFilters]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_MentionFilterExcludedClientProjectSite'
CREATE INDEX [IX_FK_MentionFilterExcludedClientProjectSite]
ON [dbo].[ExcludedClientProjectSites]
    ([MentionFilterId]);
GO

-- Creating foreign key on [MentionFilterId] in table 'IncludedTwitterMentionTypes'
ALTER TABLE [dbo].[IncludedTwitterMentionTypes]
ADD CONSTRAINT [FK_MentionFilterIncludedTwitterMentionType]
    FOREIGN KEY ([MentionFilterId])
    REFERENCES [dbo].[MentionFilters]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_MentionFilterIncludedTwitterMentionType'
CREATE INDEX [IX_FK_MentionFilterIncludedTwitterMentionType]
ON [dbo].[IncludedTwitterMentionTypes]
    ([MentionFilterId]);
GO

-- Creating foreign key on [MentionFilterId] in table 'ExcludedTwitterMentionTypes'
ALTER TABLE [dbo].[ExcludedTwitterMentionTypes]
ADD CONSTRAINT [FK_MentionFilterExcludedTwitterMentionType]
    FOREIGN KEY ([MentionFilterId])
    REFERENCES [dbo].[MentionFilters]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_MentionFilterExcludedTwitterMentionType'
CREATE INDEX [IX_FK_MentionFilterExcludedTwitterMentionType]
ON [dbo].[ExcludedTwitterMentionTypes]
    ([MentionFilterId]);
GO

-- Creating foreign key on [MentionFilterId] in table 'IncludedTwitterVerificationTypes'
ALTER TABLE [dbo].[IncludedTwitterVerificationTypes]
ADD CONSTRAINT [FK_MentionFilterIncludedTwitterVerificationType]
    FOREIGN KEY ([MentionFilterId])
    REFERENCES [dbo].[MentionFilters]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_MentionFilterIncludedTwitterVerificationType'
CREATE INDEX [IX_FK_MentionFilterIncludedTwitterVerificationType]
ON [dbo].[IncludedTwitterVerificationTypes]
    ([MentionFilterId]);
GO

-- Creating foreign key on [MentionFilterId] in table 'ExcludedTwitterVerificationTypes'
ALTER TABLE [dbo].[ExcludedTwitterVerificationTypes]
ADD CONSTRAINT [FK_MentionFilterExcludedTwitterVerificationType]
    FOREIGN KEY ([MentionFilterId])
    REFERENCES [dbo].[MentionFilters]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_MentionFilterExcludedTwitterVerificationType'
CREATE INDEX [IX_FK_MentionFilterExcludedTwitterVerificationType]
ON [dbo].[ExcludedTwitterVerificationTypes]
    ([MentionFilterId]);
GO

-- Creating foreign key on [MentionFilterId] in table 'IncludedTwitterAccountTypes'
ALTER TABLE [dbo].[IncludedTwitterAccountTypes]
ADD CONSTRAINT [FK_MentionFilterIncludedTwitterAccountTypes]
    FOREIGN KEY ([MentionFilterId])
    REFERENCES [dbo].[MentionFilters]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_MentionFilterIncludedTwitterAccountTypes'
CREATE INDEX [IX_FK_MentionFilterIncludedTwitterAccountTypes]
ON [dbo].[IncludedTwitterAccountTypes]
    ([MentionFilterId]);
GO

-- Creating foreign key on [MentionFilterId] in table 'ExcludedTwitterAccountTypes'
ALTER TABLE [dbo].[ExcludedTwitterAccountTypes]
ADD CONSTRAINT [FK_MentionFilterExcludedTwitterAccountTypes]
    FOREIGN KEY ([MentionFilterId])
    REFERENCES [dbo].[MentionFilters]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_MentionFilterExcludedTwitterAccountTypes'
CREATE INDEX [IX_FK_MentionFilterExcludedTwitterAccountTypes]
ON [dbo].[ExcludedTwitterAccountTypes]
    ([MentionFilterId]);
GO

-- Creating foreign key on [MentionFilterId] in table 'IncludedTwitterGenders'
ALTER TABLE [dbo].[IncludedTwitterGenders]
ADD CONSTRAINT [FK_MentionFilterIncludedTwitterGender]
    FOREIGN KEY ([MentionFilterId])
    REFERENCES [dbo].[MentionFilters]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_MentionFilterIncludedTwitterGender'
CREATE INDEX [IX_FK_MentionFilterIncludedTwitterGender]
ON [dbo].[IncludedTwitterGenders]
    ([MentionFilterId]);
GO

-- Creating foreign key on [MentionFilterId] in table 'IncludedTwitterProfessions'
ALTER TABLE [dbo].[IncludedTwitterProfessions]
ADD CONSTRAINT [FK_MentionFilterIncludedTwitterProfession]
    FOREIGN KEY ([MentionFilterId])
    REFERENCES [dbo].[MentionFilters]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_MentionFilterIncludedTwitterProfession'
CREATE INDEX [IX_FK_MentionFilterIncludedTwitterProfession]
ON [dbo].[IncludedTwitterProfessions]
    ([MentionFilterId]);
GO

-- Creating foreign key on [MentionFilterId] in table 'IncludedTwitterInterests'
ALTER TABLE [dbo].[IncludedTwitterInterests]
ADD CONSTRAINT [FK_MentionFilterIncludedTwitterInterest]
    FOREIGN KEY ([MentionFilterId])
    REFERENCES [dbo].[MentionFilters]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_MentionFilterIncludedTwitterInterest'
CREATE INDEX [IX_FK_MentionFilterIncludedTwitterInterest]
ON [dbo].[IncludedTwitterInterests]
    ([MentionFilterId]);
GO

-- Creating foreign key on [MentionFilterId] in table 'ExcludedTwitterInterests'
ALTER TABLE [dbo].[ExcludedTwitterInterests]
ADD CONSTRAINT [FK_MentionFilterExcludedTwitterInterest]
    FOREIGN KEY ([MentionFilterId])
    REFERENCES [dbo].[MentionFilters]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_MentionFilterExcludedTwitterInterest'
CREATE INDEX [IX_FK_MentionFilterExcludedTwitterInterest]
ON [dbo].[ExcludedTwitterInterests]
    ([MentionFilterId]);
GO

-- Creating foreign key on [MentionFilterId] in table 'IncludedSubTypes'
ALTER TABLE [dbo].[IncludedSubTypes]
ADD CONSTRAINT [FK_MentionFilterIncludedSubType]
    FOREIGN KEY ([MentionFilterId])
    REFERENCES [dbo].[MentionFilters]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_MentionFilterIncludedSubType'
CREATE INDEX [IX_FK_MentionFilterIncludedSubType]
ON [dbo].[IncludedSubTypes]
    ([MentionFilterId]);
GO

-- Creating foreign key on [MentionFilterId] in table 'ExcludedSubTypes'
ALTER TABLE [dbo].[ExcludedSubTypes]
ADD CONSTRAINT [FK_MentionFilterExcludedSubType]
    FOREIGN KEY ([MentionFilterId])
    REFERENCES [dbo].[MentionFilters]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_MentionFilterExcludedSubType'
CREATE INDEX [IX_FK_MentionFilterExcludedSubType]
ON [dbo].[ExcludedSubTypes]
    ([MentionFilterId]);
GO

-- Creating foreign key on [MentionFilterId] in table 'IncludedFaceBookMentionTypes'
ALTER TABLE [dbo].[IncludedFaceBookMentionTypes]
ADD CONSTRAINT [FK_MentionFilterIncludedFaceBookMentionType]
    FOREIGN KEY ([MentionFilterId])
    REFERENCES [dbo].[MentionFilters]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_MentionFilterIncludedFaceBookMentionType'
CREATE INDEX [IX_FK_MentionFilterIncludedFaceBookMentionType]
ON [dbo].[IncludedFaceBookMentionTypes]
    ([MentionFilterId]);
GO

-- Creating foreign key on [MentionFilterId] in table 'IncludedInstagramMentionTypes'
ALTER TABLE [dbo].[IncludedInstagramMentionTypes]
ADD CONSTRAINT [FK_MentionFilterIncludedInstagramMentionType]
    FOREIGN KEY ([MentionFilterId])
    REFERENCES [dbo].[MentionFilters]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_MentionFilterIncludedInstagramMentionType'
CREATE INDEX [IX_FK_MentionFilterIncludedInstagramMentionType]
ON [dbo].[IncludedInstagramMentionTypes]
    ([MentionFilterId]);
GO

-- Creating foreign key on [MentionFilterId] in table 'ExcludedInstagramMentionTypes'
ALTER TABLE [dbo].[ExcludedInstagramMentionTypes]
ADD CONSTRAINT [FK_MentionFilterExcludedInstagramMentionType]
    FOREIGN KEY ([MentionFilterId])
    REFERENCES [dbo].[MentionFilters]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_MentionFilterExcludedInstagramMentionType'
CREATE INDEX [IX_FK_MentionFilterExcludedInstagramMentionType]
ON [dbo].[ExcludedInstagramMentionTypes]
    ([MentionFilterId]);
GO

-- Creating foreign key on [MentionFilterId] in table 'IncludedSubredditTopics'
ALTER TABLE [dbo].[IncludedSubredditTopics]
ADD CONSTRAINT [FK_MentionFilterIncludedSubredditTopic]
    FOREIGN KEY ([MentionFilterId])
    REFERENCES [dbo].[MentionFilters]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_MentionFilterIncludedSubredditTopic'
CREATE INDEX [IX_FK_MentionFilterIncludedSubredditTopic]
ON [dbo].[IncludedSubredditTopics]
    ([MentionFilterId]);
GO

-- Creating foreign key on [MentionFilterId] in table 'ExcludedSubredditTopics'
ALTER TABLE [dbo].[ExcludedSubredditTopics]
ADD CONSTRAINT [FK_MentionFilterExcludedSubredditTopic]
    FOREIGN KEY ([MentionFilterId])
    REFERENCES [dbo].[MentionFilters]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_MentionFilterExcludedSubredditTopic'
CREATE INDEX [IX_FK_MentionFilterExcludedSubredditTopic]
ON [dbo].[ExcludedSubredditTopics]
    ([MentionFilterId]);
GO

-- Creating foreign key on [GlobalMetricId] in table 'ReactScoreEmotionalities'
ALTER TABLE [dbo].[ReactScoreEmotionalities]
ADD CONSTRAINT [FK_GlobalMetricReactScoreEmotionality]
    FOREIGN KEY ([GlobalMetricId])
    REFERENCES [dbo].[GlobalMetrics]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_GlobalMetricReactScoreEmotionality'
CREATE INDEX [IX_FK_GlobalMetricReactScoreEmotionality]
ON [dbo].[ReactScoreEmotionalities]
    ([GlobalMetricId]);
GO

-- Creating foreign key on [GlobalMetricId] in table 'ReactScoreHarms'
ALTER TABLE [dbo].[ReactScoreHarms]
ADD CONSTRAINT [FK_GlobalMetricReactScoreHarm]
    FOREIGN KEY ([GlobalMetricId])
    REFERENCES [dbo].[GlobalMetrics]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_GlobalMetricReactScoreHarm'
CREATE INDEX [IX_FK_GlobalMetricReactScoreHarm]
ON [dbo].[ReactScoreHarms]
    ([GlobalMetricId]);
GO

-- Creating foreign key on [MentionFilterId] in table 'ExcludedFaceBookMentionTypes'
ALTER TABLE [dbo].[ExcludedFaceBookMentionTypes]
ADD CONSTRAINT [FK_MentionFilterExcludedFaceBookMentionType]
    FOREIGN KEY ([MentionFilterId])
    REFERENCES [dbo].[MentionFilters]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_MentionFilterExcludedFaceBookMentionType'
CREATE INDEX [IX_FK_MentionFilterExcludedFaceBookMentionType]
ON [dbo].[ExcludedFaceBookMentionTypes]
    ([MentionFilterId]);
GO

-- Creating foreign key on [MentionFilterId] in table 'ExcludedTwitterProfessions'
ALTER TABLE [dbo].[ExcludedTwitterProfessions]
ADD CONSTRAINT [FK_MentionFilterExcludedTwitterProfession]
    FOREIGN KEY ([MentionFilterId])
    REFERENCES [dbo].[MentionFilters]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_MentionFilterExcludedTwitterProfession'
CREATE INDEX [IX_FK_MentionFilterExcludedTwitterProfession]
ON [dbo].[ExcludedTwitterProfessions]
    ([MentionFilterId]);
GO

-- Creating foreign key on [MentionFilterId] in table 'AllowedMinAndMaxAndMultiOptionsMentionFilters'
ALTER TABLE [dbo].[AllowedMinAndMaxAndMultiOptionsMentionFilters]
ADD CONSTRAINT [FK_MentionFilterAllowedMinAndMaxAndMultiOptionsMentionFilter]
    FOREIGN KEY ([MentionFilterId])
    REFERENCES [dbo].[MentionFilters]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_MentionFilterAllowedMinAndMaxAndMultiOptionsMentionFilter'
CREATE INDEX [IX_FK_MentionFilterAllowedMinAndMaxAndMultiOptionsMentionFilter]
ON [dbo].[AllowedMinAndMaxAndMultiOptionsMentionFilters]
    ([MentionFilterId]);
GO

-- Creating foreign key on [ClientProjectId] in table 'ClientProjectTwitterTopAuthors'
ALTER TABLE [dbo].[ClientProjectTwitterTopAuthors]
ADD CONSTRAINT [FK_ClientProjectClientProjectTwitterTopAuthor]
    FOREIGN KEY ([ClientProjectId])
    REFERENCES [dbo].[ClientProjects]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_ClientProjectClientProjectTwitterTopAuthor'
CREATE INDEX [IX_FK_ClientProjectClientProjectTwitterTopAuthor]
ON [dbo].[ClientProjectTwitterTopAuthors]
    ([ClientProjectId]);
GO

-- Creating foreign key on [MentionFilterId] in table 'IncludedDomains'
ALTER TABLE [dbo].[IncludedDomains]
ADD CONSTRAINT [FK_MentionFilterIncludedDomains]
    FOREIGN KEY ([MentionFilterId])
    REFERENCES [dbo].[MentionFilters]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_MentionFilterIncludedDomains'
CREATE INDEX [IX_FK_MentionFilterIncludedDomains]
ON [dbo].[IncludedDomains]
    ([MentionFilterId]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------