using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BrandWatchElaqat.ViewModel;

namespace BrandWatchElaqat.Models
{
    public partial class ClientProject
    {
        public static List<ClientProject> GetAllClientsProjects()
        {
            var db = new DBModelBrandWatchContainer();           

            return db.ClientProjects.ToList(); 

        }
        public static List<BrandWatchClientProject> GetAllBrandWatchClientsProjects()
        {
            var db = new DBModelBrandWatchContainer();
            List<BrandWatchClientProject> clientProject = db.BrandWatchClientProjects.ToList();

            return clientProject;

        }


        public static ClientProject GetClientProjectDetails(int clientProjectID)
        {
            var db = new DBModelBrandWatchContainer();

            return db.ClientProjects.Find(clientProjectID);
        }
      



        public static ClientProject AssigneNewClientProjectToElaqatClient(ClientProjectVM clientProjectVM)
        {
            var db = new DBModelBrandWatchContainer();
            var client = db.ElaqatClients.Find(clientProjectVM.SelectedElaqatClientId);
            //if (client.ClientProjects.Any())
            //{
            //    foreach (var _clientProject in client.ClientProjects)
            //    {
            //        if (_clientProject.BrandwatchProjectID == clientProjectVM.ProjectBrandWatchID)
            //        {
            //            return _clientProject;
            //        }
            //    }
            //}
            var braandWatchProject = db.BrandWatchClientProjects.Find(clientProjectVM.Id);
            ClientProject clientProject = new ClientProject();
            clientProject.ProjectName = braandWatchProject.ProjectName;
            clientProject.ElaqatClientId = client.Id;
            clientProject.ProjectCreationDate = braandWatchProject.ProjectCreationDate;
            clientProject.BrandWatchClientId = braandWatchProject.BrandWatchClientId;
            clientProject.BrandWatchClientName = braandWatchProject.BrandWatchClientName;
            clientProject.BrandwatchProjectID = braandWatchProject.BrandwatchProjectID;
            clientProject.ShowMentionsStartingFrom = 3;
            clientProject.Description = braandWatchProject.Description;
            clientProject.IsActive = true;
            db.ClientProjects.Add(clientProject);
            db.SaveChanges();
            return clientProject;
        }

        public static int AssigneImportedClientProjectToElaqatClient(ClientProjectVM clientProjectVM)
        {
            var db = new DBModelBrandWatchContainer();
            var _tempClientProject = db.ClientProjects.Find(clientProjectVM.Id);
            var client = db.ElaqatClients.Find(clientProjectVM.SelectedElaqatClientId);
            if (client == null || _tempClientProject == null)
            {
                return 0;
            }
            ClientProject clientProject = new ClientProject();
            clientProject.ProjectName = _tempClientProject.ProjectName;
            clientProject.ElaqatClientId = clientProjectVM.SelectedElaqatClientId;
            clientProject.ProjectCreationDate = _tempClientProject.ProjectCreationDate;
            clientProject.BrandWatchClientId = _tempClientProject.BrandWatchClientId;
            clientProject.BrandWatchClientName = _tempClientProject.BrandWatchClientName;
            clientProject.BrandwatchProjectID = _tempClientProject.BrandwatchProjectID;
            clientProject.ShowMentionsStartingFrom = _tempClientProject.ShowMentionsStartingFrom;
            clientProject.Description = _tempClientProject.Description;
            clientProject.IsActive = true;
            db.ClientProjects.Add(clientProject);

            if (_tempClientProject.AuthorGroups.Any())
            {
                List<AuthorGroup> AuthorGroupList = new List<AuthorGroup>();
                foreach (var _authorGroup in _tempClientProject.AuthorGroups)
                {
                    AuthorGroup authorGroup = new AuthorGroup();
                    authorGroup.BrandWatchAuthorGroupID = _authorGroup.BrandWatchAuthorGroupID;
                    authorGroup.GroupName = _authorGroup.GroupName;
                    authorGroup.IsActive = _authorGroup.IsActive;
                    authorGroup.Shared = _authorGroup.Shared;
                    authorGroup.ClientProjectId = clientProject.Id;
                    AuthorGroupList.Add(authorGroup);
                }
                db.AuthorGroups.AddRange(AuthorGroupList);

            }
            if (_tempClientProject.ClientProjectCategories.Any())
            {
                List<ClientProjectCategory> ClientProjectCategoryList = new List<ClientProjectCategory>();
                foreach (var _clientProjectCategory in _tempClientProject.ClientProjectCategories)
                {
                    ClientProjectCategory clientProjectCategory = new ClientProjectCategory();
                    clientProjectCategory.BrandWatchCategoeryID = _clientProjectCategory.BrandWatchCategoeryID;
                    clientProjectCategory.CategoryName = _clientProjectCategory.CategoryName;
                    clientProjectCategory.IsActive = _clientProjectCategory.IsActive;
                    clientProjectCategory.ClientProjectId = clientProject.Id;                 


                    if (_clientProjectCategory.ClientProjectSubCategories.Any())
                    {
                       // List<ClientProjectSubCategory> ClientProjectSubCategoryList = new List<ClientProjectSubCategory>();
                        foreach (var _subCategory in _clientProjectCategory.ClientProjectSubCategories)
                        {
                            ClientProjectSubCategory clientProjectSubCategory = new ClientProjectSubCategory();
                            clientProjectSubCategory.BrandWatchSubCategoryID = _subCategory.BrandWatchSubCategoryID;
                            clientProjectSubCategory.IsActive = _subCategory.IsActive;
                            clientProjectSubCategory.OrderingNumber = _subCategory.OrderingNumber;
                            clientProjectSubCategory.SubCategoryName = _subCategory.SubCategoryName;
                            clientProjectSubCategory.ClientProjectCategory = clientProjectCategory;
                            clientProjectCategory.ClientProjectSubCategories.Add(clientProjectSubCategory);
                        }
                        
                    }
                    if (_clientProjectCategory.ClintProjectCtegoryQueryIDs.Any())
                    {
                        foreach (var _queryId in _clientProjectCategory.ClintProjectCtegoryQueryIDs)
                        {
                            ClintProjectCtegoryQueryID clintProjectCtegoryQueryID = new ClintProjectCtegoryQueryID();
                            clintProjectCtegoryQueryID.BrandWatchQueryID = _queryId.BrandWatchQueryID;
                            clintProjectCtegoryQueryID.ClientProjectCategory = clientProjectCategory;
                            clientProjectCategory.ClintProjectCtegoryQueryIDs.Add(clintProjectCtegoryQueryID);
                        }

                    }
                    ClientProjectCategoryList.Add(clientProjectCategory);


                }
                db.ClientProjectCategories.AddRange(ClientProjectCategoryList);

            }
            if (_tempClientProject.ClientProjectQueries.Any())
            {
                List<ClientProjectQuerie> ClientProjectQuerieList = new List<ClientProjectQuerie>();
                foreach (var _clientProjectQueries in _tempClientProject.ClientProjectQueries)
                {
                    ClientProjectQuerie clientProjectQuerie = new ClientProjectQuerie();
                    clientProjectQuerie.Active = _clientProjectQueries.Active;
                    clientProjectQuerie.BooleanQuery = _clientProjectQueries.BooleanQuery;
                    clientProjectQuerie.BrandWatchQueryId = _clientProjectQueries.BrandWatchQueryId;
                    clientProjectQuerie.CreationDate = _clientProjectQueries.CreationDate;
                    clientProjectQuerie.Description = _clientProjectQueries.Description;
                    clientProjectQuerie.DisplayName = _clientProjectQueries.DisplayName;
                    clientProjectQuerie.IsActive = _clientProjectQueries.IsActive;
                    clientProjectQuerie.LastModificationDate = _clientProjectQueries.LastModificationDate;
                    clientProjectQuerie.LastModifiedUsername = _clientProjectQueries.LastModifiedUsername;
                    clientProjectQuerie.QuerieName = _clientProjectQueries.QuerieName;
                    clientProjectQuerie.StartDate = _clientProjectQueries.StartDate;
                    clientProjectQuerie.State = _clientProjectQueries.State;
                    clientProjectQuerie.ClientProjectId = clientProject.Id;
                    
                   
                    if (_clientProjectQueries.QueryContentSources.Any())
                    {
                        foreach (var _contentSource in _clientProjectQueries.QueryContentSources)
                        {
                            QueryContentSource queryContentSource = new QueryContentSource();
                            queryContentSource.SourceName = _contentSource.SourceName;
                            queryContentSource.ClientProjectQuerie = clientProjectQuerie;
                            clientProjectQuerie.QueryContentSources.Add(queryContentSource);
                        }

                    }

                    if (_clientProjectQueries.QueryLanguages.Any())
                    {
                        foreach (var _language in _clientProjectQueries.QueryLanguages)
                        {
                            QueryLanguage queryLanguage = new QueryLanguage();
                            queryLanguage.Language = _language.Language;
                            queryLanguage.ClientProjectQuerie = clientProjectQuerie;
                            clientProjectQuerie.QueryLanguages.Add(queryLanguage);
                        }

                    }

                    if (_clientProjectQueries.QueryHighLightTerms.Any())
                    {
                        foreach (var _highLight in _clientProjectQueries.QueryHighLightTerms)
                        {
                            QueryHighLightTerm queryHighLightTerm = new QueryHighLightTerm();
                            queryHighLightTerm.HighLightTerm = _highLight.HighLightTerm;
                            queryHighLightTerm.ClientProjectQuerie = clientProjectQuerie;
                            clientProjectQuerie.QueryHighLightTerms.Add(queryHighLightTerm);
                        }

                    }
                    ClientProjectQuerieList.Add(clientProjectQuerie);
                }
                db.ClientProjectQueries.AddRange(ClientProjectQuerieList);
            }
            if (_tempClientProject.ClientProjectSites.Any())
            {
                List<ClientProjectSite> ClientProjectSiteList = new List<ClientProjectSite>();
                foreach (var _site in _tempClientProject.ClientProjectSites)
                {
                    ClientProjectSite clientProjectSite = new ClientProjectSite();
                    clientProjectSite.BrandWatchSiteID = _site.BrandWatchSiteID;
                    clientProjectSite.IsActive = _site.IsActive;
                    clientProjectSite.Shared = _site.Shared;
                    clientProjectSite.SiteName = _site.SiteName;
                    clientProjectSite.ClientProjectId = clientProject.Id;

                    if (_site.SiteDomains.Any())
                    {
                        foreach (var _domain in _site.SiteDomains)
                        {
                            SiteDomain siteDomain = new SiteDomain();
                            siteDomain.DomainURL = _domain.DomainURL;
                            clientProjectSite.SiteDomains.Add(siteDomain);
                        }
                    }
                    ClientProjectSiteList.Add(clientProjectSite);

                }
                db.ClientProjectSites.AddRange(ClientProjectSiteList);

            }

            if (_tempClientProject.ProjectLocations.Any())
            {
                List<ProjectLocation> ProjectLocationList = new List<ProjectLocation>();
                foreach (var _location in _tempClientProject.ProjectLocations)
                {
                    ProjectLocation projectLocation = new ProjectLocation();
                    projectLocation.BrandWatchGroupLocationID = _location.BrandWatchGroupLocationID;
                    projectLocation.IsActive = _location.IsActive;
                    projectLocation.LocationName = _location.LocationName;
                    projectLocation.Shared = _location.Shared;
                    projectLocation.ClientProjectId = clientProject.Id;
                    ProjectLocationList.Add(projectLocation);
                    
                }
                db.ProjectLocations.AddRange(ProjectLocationList);

            }

            if (_tempClientProject.ProjectRuleTags.Any())
            {
                List<ProjectRuleTag> ProjectRuleTagList = new List<ProjectRuleTag>();
                foreach (var _ruleTag in _tempClientProject.ProjectRuleTags)
                {
                    ProjectRuleTag projectRuleTag = new ProjectRuleTag();
                    projectRuleTag.BrandWatchRuleTagID = _ruleTag.BrandWatchRuleTagID;
                    projectRuleTag.BrandWatchRuleTagName = _ruleTag.BrandWatchRuleTagName;
                    projectRuleTag.IsActive = _ruleTag.IsActive;
                    projectRuleTag.ClientProjectId = clientProject.Id;
                    ProjectRuleTagList.Add(projectRuleTag);
                }
                db.ProjectRuleTags.AddRange(ProjectRuleTagList);

            }
            db.SaveChanges();

            return clientProject.Id;
        }



        public static ClientProject EditClientProject(ClientProjectVM clientProjectVM)
        {
            var db = new DBModelBrandWatchContainer();
            ClientProject clientProject = db.ClientProjects.Find(clientProjectVM.Id);
            ElaqatClient elaqatClient = db.ElaqatClients.Find(clientProjectVM.SelectedElaqatClientId);
            clientProject.ProjectName = clientProjectVM.ProjectName;
            clientProject.ProjectCreationDate = clientProjectVM.ProjectCreationDate;
            clientProject.BrandwatchProjectID = clientProjectVM.ProjectBrandWatchID;
            clientProject.Description = clientProjectVM.Description;
            clientProject.BrandWatchClientId = clientProjectVM.BrandWatchClientId;
            clientProject.BrandWatchClientName = clientProjectVM.BrandWatchClientName;
            clientProject.ElaqatClient = elaqatClient;
            clientProject.ElaqatClientId = elaqatClient.Id;
            clientProject.ShowMentionsStartingFrom = clientProjectVM.ShowMentionsStartingFrom;
            clientProject.IsActive = clientProjectVM.IsActive;
            db.Entry(clientProject).State = EntityState.Modified;
            db.SaveChanges();
            return clientProject;
        }


        private static bool UpdateBrandWathProject(ProjectMapper project, int brandWatchClientProjectID)
        {
            var db = new DBModelBrandWatchContainer();
            var brandWatchClientProject = db.BrandWatchClientProjects.Find(brandWatchClientProjectID);
            brandWatchClientProject.BrandWatchClientId = project.clientId;
            brandWatchClientProject.BrandWatchClientName = project.clientName;
            brandWatchClientProject.BrandwatchProjectID = project.id;
            brandWatchClientProject.Description = project.description;
            if (project.creationDate.Contains("T"))
            {
                var splittedDate = project.creationDate.Split('T');
                project.creationDate = splittedDate[0];
            }
            brandWatchClientProject.ProjectCreationDate = project.creationDate;
            brandWatchClientProject.ProjectName = project.name;

            db.Entry(brandWatchClientProject).State = EntityState.Modified;
            db.SaveChanges();
            return true;
        }

        private static bool UpdateClientProjectCategory(CategoryMapper categoryMapper, int ClientProjectCategoryID)
        {
            var db = new DBModelBrandWatchContainer();
            ClientProjectCategory clientProjectCategory = db.ClientProjectCategories.Find(ClientProjectCategoryID);
            clientProjectCategory.BrandWatchCategoeryID = categoryMapper.id;
            clientProjectCategory.CategoryName = categoryMapper.name;
            List<string> listQueryIDs = new List<string>();
            List<SubCategoryMapper> subCategoryMappers = new List<SubCategoryMapper>();
            if (categoryMapper.children.Any())
            {
                foreach (var subCategory in categoryMapper.children)
                {
                    subCategoryMappers.Add(subCategory);
                    foreach (var clientProjectSubCategory in clientProjectCategory.ClientProjectSubCategories)
                    {
                        if (subCategory.id == clientProjectSubCategory.BrandWatchSubCategoryID)
                        {
                            clientProjectSubCategory.SubCategoryName = subCategory.name;
                            clientProjectSubCategory.BrandWatchSubCategoryID = subCategory.id;
                            clientProjectSubCategory.OrderingNumber = subCategory.ordering;
                            subCategoryMappers.Remove(subCategory);
                        }
                    }
                    
                   
                }

            }
            if (categoryMapper.queryIds.Any())
            {
                foreach (var queryId in categoryMapper.queryIds)
                {
                    listQueryIDs.Add(queryId);
                    foreach (var categoryQueryID in clientProjectCategory.ClintProjectCtegoryQueryIDs)
                    {
                        if (queryId == categoryQueryID.BrandWatchQueryID)
                        {
                            categoryQueryID.BrandWatchQueryID = queryId;
                            listQueryIDs.Remove(queryId);
                        }

                    }                   
                }

            }
            if (listQueryIDs.Any())
            {
                foreach (var queryId in listQueryIDs)
                {
                    ClintProjectCtegoryQueryID clintProjectCtegoryQueryID = new ClintProjectCtegoryQueryID();
                    clintProjectCtegoryQueryID.BrandWatchQueryID = queryId;
                    clientProjectCategory.ClintProjectCtegoryQueryIDs.Add(clintProjectCtegoryQueryID);
                }
               
            }
            if (subCategoryMappers.Any())
            {
                foreach (var subCategory in subCategoryMappers)
                {
                    ClientProjectSubCategory clientProjectSubCategory = new ClientProjectSubCategory();
                    clientProjectSubCategory.SubCategoryName = subCategory.name;
                    clientProjectSubCategory.BrandWatchSubCategoryID = subCategory.id;
                    clientProjectSubCategory.OrderingNumber = subCategory.ordering;
                    clientProjectSubCategory.IsActive = true;
                    clientProjectCategory.ClientProjectSubCategories.Add(clientProjectSubCategory);

                }

            }

            db.Entry(clientProjectCategory).State = EntityState.Modified;
            db.SaveChanges();
            return true;
        }



        private static bool UpdateClientProjeAuthorGroup(AuthorGroupMapper authorGroupMapper, int ClientProjectAuthorGroupID)
        {
            var db = new DBModelBrandWatchContainer();
            AuthorGroup authorGgroup = db.AuthorGroups.Find(ClientProjectAuthorGroupID);
            authorGgroup.BrandWatchAuthorGroupID = authorGroupMapper.id;
            authorGgroup.GroupName = authorGroupMapper.name;
            authorGgroup.Shared = authorGroupMapper.shared;

            db.Entry(authorGgroup).State = EntityState.Modified;
            db.SaveChanges();
            return true;
        }


        private static bool UpdateClientProjectQuery(QueryMapper queryMapper, int clientProjectQueryID)
        {
            var db = new DBModelBrandWatchContainer();

            ClientProjectQuerie clientProjectQuerie = db.ClientProjectQueries.Find(clientProjectQueryID);

            db.QueryContentSources.RemoveRange(clientProjectQuerie.QueryContentSources);
            db.QueryHighLightTerms.RemoveRange(clientProjectQuerie.QueryHighLightTerms);
            db.QueryLanguages.RemoveRange(clientProjectQuerie.QueryLanguages);
            db.SaveChanges();

            db = new DBModelBrandWatchContainer();
            clientProjectQuerie = db.ClientProjectQueries.Find(clientProjectQueryID);
            if (queryMapper.state == "active" || queryMapper.active == "true")
            {
                clientProjectQuerie.IsActive = true;
            }
            else
            {
                clientProjectQuerie.IsActive = false;
            }
            clientProjectQuerie.Active = queryMapper.active;
            clientProjectQuerie.BooleanQuery = queryMapper.booleanQuery;
            clientProjectQuerie.BrandWatchQueryId = queryMapper.id;
            if (queryMapper.creationDate.Contains("T"))
            {
                var splittedDate = queryMapper.creationDate.Split('T');
                queryMapper.creationDate = splittedDate[0];
            }
            clientProjectQuerie.CreationDate = queryMapper.creationDate;
            clientProjectQuerie.Description = queryMapper.description;
            clientProjectQuerie.DisplayName = queryMapper.displayName;
            if (queryMapper.lastModificationDate.Contains("T"))
            {
                var splittedDate = queryMapper.lastModificationDate.Split('T');
                queryMapper.lastModificationDate = splittedDate[0];
            }
            clientProjectQuerie.LastModificationDate = queryMapper.lastModificationDate;
            clientProjectQuerie.LastModifiedUsername = queryMapper.lastModifiedUsername;
            clientProjectQuerie.QuerieName = queryMapper.name;
            if (queryMapper.startDate.Contains("T"))
            {
                var splittedDate = queryMapper.startDate.Split('T');
                queryMapper.startDate = splittedDate[0];
            }
            clientProjectQuerie.StartDate = queryMapper.startDate;
            clientProjectQuerie.State = queryMapper.state;
            if (queryMapper.contentSources.Any())
            {
                foreach (var _contentSource in queryMapper.contentSources)
                {
                    QueryContentSource queryContentSource = new QueryContentSource();
                    queryContentSource.SourceName = _contentSource;
                    clientProjectQuerie.QueryContentSources.Add(queryContentSource);
                }
            }
            if (queryMapper.highlightTerms.Any())
            {
                foreach (var _heiglightTerm in queryMapper.highlightTerms)
                {
                    QueryHighLightTerm queryHighLightTerm = new QueryHighLightTerm();
                    queryHighLightTerm.HighLightTerm = _heiglightTerm;
                    clientProjectQuerie.QueryHighLightTerms.Add(queryHighLightTerm);
                }
            }
            if (queryMapper.languages.Any())
            {
                foreach (var _language in queryMapper.languages)
                {
                    QueryLanguage queryLanguage = new QueryLanguage();
                    queryLanguage.Language = _language;
                    clientProjectQuerie.QueryLanguages.Add(queryLanguage);
                }
            }
            db.Entry(clientProjectQuerie).State = EntityState.Modified;
            db.SaveChanges();

            return true;
        }


        private static bool UpdateClientProjeLocationGroup(LocationMapper locationGroupMapper, int ClientProjectLocationID)
        {
            var db = new DBModelBrandWatchContainer();
            ProjectLocation projectLocation = db.ProjectLocations.Find(ClientProjectLocationID);
            projectLocation.BrandWatchGroupLocationID = locationGroupMapper.id;
            projectLocation.LocationName = locationGroupMapper.name;
            projectLocation.Shared = locationGroupMapper.shared;
            db.Entry(projectLocation).State = EntityState.Modified;
            db.SaveChanges();
            return true;
        }


        private static bool UpdateClientProjeRuleTag(ProjectRuleTagMapper projectRuleTagMapper, int projectRuleTagID)
        {
            var db = new DBModelBrandWatchContainer();
            ProjectRuleTag projectRuleTag = db.ProjectRuleTags.Find(projectRuleTagID);
            projectRuleTag.BrandWatchRuleTagID = projectRuleTagMapper.id;
            projectRuleTag.BrandWatchRuleTagName = projectRuleTagMapper.name;

            db.Entry(projectRuleTag).State = EntityState.Modified;
            db.SaveChanges();
            return true;
        }



        public static ClientProject AddNewClientProjectToElaqatClient(ClientProjectVM clientProjectVM)
        {
            var db = new DBModelBrandWatchContainer();
            var client = db.ElaqatClients.Find(clientProjectVM.SelectedElaqatClientId);
            var brandWatchProject = db.BrandWatchClientProjects.Find(clientProjectVM.SelectedClientProjectId);
           
            ClientProject clientProject = new ClientProject();
            clientProject.BrandWatchClientId = brandWatchProject.BrandWatchClientId;
            clientProject.BrandWatchClientName = brandWatchProject.BrandWatchClientName;
            clientProject.BrandwatchProjectID = brandWatchProject.BrandwatchProjectID;
            clientProject.Description = brandWatchProject.Description;
            clientProject.ElaqatClient = client;
            clientProject.ElaqatClientId = client.Id;
            clientProject.ShowMentionsStartingFrom = 3;
            clientProject.ProjectCreationDate = brandWatchProject.ProjectCreationDate;
            clientProject.ProjectName = brandWatchProject.ProjectName;
            clientProject.IsActive = true;

            db.ClientProjects.Add(clientProject);
            db.SaveChanges();


            return clientProject;
        }

        public static List<BrandWatchClientProject> AddNewBrandWatchProjects(List<ProjectMapper> projectMapper)
        {
            var db = new DBModelBrandWatchContainer();
            var currentBrandWatchProjects = db.BrandWatchClientProjects.ToList();
            List<BrandWatchClientProject> _brandWatchclientProjects = new List<BrandWatchClientProject>();
            List<ProjectMapper> _projectMapper = new List<ProjectMapper>();
            if (projectMapper.Any())
            {

                foreach (var _project in projectMapper)
                {
                    _projectMapper.Add(_project);
                    foreach (var exisitProject in currentBrandWatchProjects)
                    {
                        if (_project.id == exisitProject.BrandwatchProjectID)
                        {
                             UpdateBrandWathProject(_project, exisitProject.Id);
                            _projectMapper.Remove(_project);
                        }
                    }
                }
            }
            if (_projectMapper.Any())
            {
                foreach (var project in _projectMapper)
                {
                    BrandWatchClientProject brandWatchClientProject = new BrandWatchClientProject();
                    brandWatchClientProject.BrandWatchClientId = project.clientId;
                    brandWatchClientProject.BrandWatchClientName = project.clientName;
                    brandWatchClientProject.BrandwatchProjectID = project.id;
                    brandWatchClientProject.Description = project.description;
                    if (project.creationDate.Contains("T"))
                    {
                        var splittedDate = project.creationDate.Split('T');
                        project.creationDate = splittedDate[0];
                    }
                    brandWatchClientProject.ProjectCreationDate = project.creationDate;
                    brandWatchClientProject.ProjectName = project.name;
                    _brandWatchclientProjects.Add(brandWatchClientProject);

                }
                db.BrandWatchClientProjects.AddRange(_brandWatchclientProjects);
                db.SaveChanges();
            }

            return db.BrandWatchClientProjects.ToList();
        }


        public static ClientProject AddCategoriesToClientProject(List<CategoryMapper> categoryMappers, int clientProjectID)
        {
            var db = new DBModelBrandWatchContainer();
            List<CategoryMapper> localcategoryMappersList = new List<CategoryMapper>();
            var clientProject = db.ClientProjects.Find(clientProjectID);
            if (categoryMappers.Any())
            {

                foreach (var categoryMapper in categoryMappers)
                {
                    localcategoryMappersList.Add(categoryMapper);
                    foreach (var _clientCategory in clientProject.ClientProjectCategories)
                    {
                        if (categoryMapper.id == _clientCategory.BrandWatchCategoeryID)
                        {
                            UpdateClientProjectCategory(categoryMapper, _clientCategory.Id);
                            localcategoryMappersList.Remove(categoryMapper);
                        }
                    }
                }
            }

            if (localcategoryMappersList.Any())
            {

                foreach (var localCategory in localcategoryMappersList)
                {
                    ClientProjectCategory clientProjectCategory = new ClientProjectCategory();
                    clientProjectCategory.BrandWatchCategoeryID = localCategory.id;
                    clientProjectCategory.CategoryName = localCategory.name;
                    clientProjectCategory.IsActive = true;
                    if (localCategory.children.Any())
                    {
                        foreach (var subCategory in localCategory.children)
                        {
                            ClientProjectSubCategory clientProjectSubCategory = new ClientProjectSubCategory();
                            clientProjectSubCategory.SubCategoryName = subCategory.name;
                            clientProjectSubCategory.BrandWatchSubCategoryID = subCategory.id;
                            clientProjectSubCategory.OrderingNumber = subCategory.ordering;
                            clientProjectSubCategory.IsActive = true;
                            clientProjectCategory.ClientProjectSubCategories.Add(clientProjectSubCategory);
                        }

                    }
                    if (localCategory.queryIds.Any())
                    {
                        foreach (var queryId in localCategory.queryIds)
                        {
                            ClintProjectCtegoryQueryID clintProjectCtegoryQueryID = new ClintProjectCtegoryQueryID();
                            clintProjectCtegoryQueryID.BrandWatchQueryID = queryId;
                            clientProjectCategory.ClintProjectCtegoryQueryIDs.Add(clintProjectCtegoryQueryID);
                        }

                    }

                    clientProject.ClientProjectCategories.Add(clientProjectCategory);
                }
                db.Entry(clientProject).State = EntityState.Modified;
                db.SaveChanges();
            }

            return clientProject;
        }





        public static ClientProject AddQueriesToClientProject(List<QueryMapper> queryMappers, int clientProjectID)
        {
            var db = new DBModelBrandWatchContainer();
            List<QueryMapper> localQueryMappersList = new List<QueryMapper>();
            var clientProject = db.ClientProjects.Find(clientProjectID);
            if (queryMappers.Any())
            {

                foreach (var queryMapper in queryMappers)
                {
                    localQueryMappersList.Add(queryMapper);
                    foreach (var _clientQuery in clientProject.ClientProjectQueries)
                    {
                        if (queryMapper.id == _clientQuery.BrandWatchQueryId)
                        {
                            UpdateClientProjectQuery(queryMapper, _clientQuery.Id);
                            localQueryMappersList.Remove(queryMapper);
                        }
                    }
                }
            }

            if (localQueryMappersList.Any())
            {

                foreach (var localQyery in localQueryMappersList)
                {
                    ClientProjectQuerie clientProjectQuerie = new ClientProjectQuerie();
                    if (localQyery.contentSources.Any())
                    {
                        foreach (var _contentSource in localQyery.contentSources)
                        {
                            QueryContentSource queryContentSource = new QueryContentSource();
                            queryContentSource.SourceName = _contentSource;
                            clientProjectQuerie.QueryContentSources.Add(queryContentSource);
                        }
                    }
                    if (localQyery.highlightTerms.Any())
                    {
                        foreach (var _heiglightTerm in localQyery.highlightTerms)
                        {
                            QueryHighLightTerm queryHighLightTerm = new QueryHighLightTerm();
                            queryHighLightTerm.HighLightTerm = _heiglightTerm;
                            clientProjectQuerie.QueryHighLightTerms.Add(queryHighLightTerm);
                        }
                    }
                    if (localQyery.languages.Any())
                    {
                        foreach (var _language in localQyery.languages)
                        {
                            QueryLanguage queryLanguage = new QueryLanguage();
                            queryLanguage.Language = _language;
                            clientProjectQuerie.QueryLanguages.Add(queryLanguage);
                        }
                    }

                    clientProjectQuerie.Active = localQyery.active;
                    clientProjectQuerie.BooleanQuery = localQyery.booleanQuery;
                    clientProjectQuerie.BrandWatchQueryId = localQyery.id;
                    clientProjectQuerie.Description = localQyery.description;
                    clientProjectQuerie.DisplayName = localQyery.displayName;
                    if (localQyery.creationDate.Contains("T"))
                    {
                        var splittedDate = localQyery.creationDate.Split('T');
                        localQyery.creationDate = splittedDate[0];
                    }
                    clientProjectQuerie.CreationDate = localQyery.creationDate;
                    if (localQyery.lastModificationDate.Contains("T"))
                    {
                        var splittedDate = localQyery.lastModificationDate.Split('T');
                        localQyery.lastModificationDate = splittedDate[0];
                    }
                    clientProjectQuerie.LastModificationDate = localQyery.lastModificationDate;
                    clientProjectQuerie.LastModifiedUsername = localQyery.lastModifiedUsername;
                    clientProjectQuerie.QuerieName = localQyery.name;
                    if (localQyery.startDate.Contains("T"))
                    {
                        var splittedDate = localQyery.startDate.Split('T');
                        localQyery.startDate = splittedDate[0];
                    }
                    clientProjectQuerie.StartDate = localQyery.startDate;
                    clientProjectQuerie.State = localQyery.state;
                    if (localQyery.state == "active" || localQyery.active == "true")
                    {
                        clientProjectQuerie.IsActive = true;
                    }
                    else
                    {
                        clientProjectQuerie.IsActive = false;
                    }
                   clientProject.ClientProjectQueries.Add(clientProjectQuerie);
                }
                db.Entry(clientProject).State = EntityState.Modified;
                db.SaveChanges();
            }

            return clientProject;
        }



        public static ClientProject AddAuthorGroupToClientProject(List<AuthorGroupMapper> authorGroupMapperList, int clientProjectID)
        {
            var db = new DBModelBrandWatchContainer();
            List<AuthorGroupMapper> localAuthorGroupMapperList = new List<AuthorGroupMapper>();
            var clientProject = db.ClientProjects.Find(clientProjectID);
            if (authorGroupMapperList.Any())
            {

                foreach (var authorGroupMapper in authorGroupMapperList)
                {
                    localAuthorGroupMapperList.Add(authorGroupMapper);
                    foreach (var _authorGroup in clientProject.AuthorGroups)
                    {
                        if (authorGroupMapper.id == _authorGroup.BrandWatchAuthorGroupID)
                        {
                            UpdateClientProjeAuthorGroup(authorGroupMapper, _authorGroup.Id);
                            localAuthorGroupMapperList.Remove(authorGroupMapper);
                        }
                    }
                }
            }

            if (localAuthorGroupMapperList.Any())
            {

                foreach (var localAuthor in localAuthorGroupMapperList)
                {
                    AuthorGroup clientProjectAuthorGroup = new AuthorGroup();
                    clientProjectAuthorGroup.BrandWatchAuthorGroupID = localAuthor.id;
                    clientProjectAuthorGroup.GroupName = localAuthor.name;
                    clientProjectAuthorGroup.Shared = localAuthor.shared;
                    clientProjectAuthorGroup.IsActive = true;


                    clientProject.AuthorGroups.Add(clientProjectAuthorGroup);
                }
                db.Entry(clientProject).State = EntityState.Modified;
                db.SaveChanges();
            }

            return clientProject;
        }
        

        public static ClientProject AddLocationGroupToClientProject(List<LocationMapper> locationGroupMapperList, int clientProjectID)
        {
            var db = new DBModelBrandWatchContainer();
            List<LocationMapper> localLocationGroupMapperList = new List<LocationMapper>();
            var clientProject = db.ClientProjects.Find(clientProjectID);
            if (locationGroupMapperList.Any())
            {

                foreach (var locationGroupMapper in locationGroupMapperList)
                {
                    localLocationGroupMapperList.Add(locationGroupMapper);
                    foreach (var _locationGroup in clientProject.ProjectLocations)
                    {
                        if (locationGroupMapper.id == _locationGroup.BrandWatchGroupLocationID)
                        {
                            UpdateClientProjeLocationGroup(locationGroupMapper, _locationGroup.Id);
                            localLocationGroupMapperList.Remove(locationGroupMapper);
                        }
                    }
                }
            }

            if (localLocationGroupMapperList.Any())
            {

                foreach (var _location in localLocationGroupMapperList)
                {
                    ProjectLocation projectLocation = new ProjectLocation();
                    projectLocation.BrandWatchGroupLocationID = _location.id;
                    projectLocation.LocationName = _location.name;
                    projectLocation.Shared = _location.shared;
                    projectLocation.IsActive = true;


                    clientProject.ProjectLocations.Add(projectLocation);
                }
                db.Entry(clientProject).State = EntityState.Modified;
                db.SaveChanges();
            }

            return clientProject;
        }

        public static ClientProject AddProjectRuleTagToClientProject(List<ProjectRuleTagMapper> projectRuleTagMapperList, int clientProjectID)
        {
            var db = new DBModelBrandWatchContainer();
            List<ProjectRuleTagMapper> localProjectRuleTagMapperList = new List<ProjectRuleTagMapper>();
            var clientProject = db.ClientProjects.Find(clientProjectID);
            if (projectRuleTagMapperList.Any())
            {

                foreach (var projectRuleTagMapper in projectRuleTagMapperList)
                {
                    localProjectRuleTagMapperList.Add(projectRuleTagMapper);
                    foreach (var _projectRuleTag in clientProject.ProjectRuleTags)
                    {
                        if (projectRuleTagMapper.id == _projectRuleTag.BrandWatchRuleTagID)
                        {
                            UpdateClientProjeRuleTag(projectRuleTagMapper, _projectRuleTag.Id);
                            localProjectRuleTagMapperList.Remove(projectRuleTagMapper);
                        }
                    }
                }
            }

            if (localProjectRuleTagMapperList.Any())
            {

                foreach (var _ruleTag in localProjectRuleTagMapperList)
                {
                    ProjectRuleTag projectRuleTag = new ProjectRuleTag();
                    projectRuleTag.BrandWatchRuleTagID = _ruleTag.id;
                    projectRuleTag.BrandWatchRuleTagName = _ruleTag.name;
                    projectRuleTag.IsActive = true;


                    clientProject.ProjectRuleTags.Add(projectRuleTag);
                }
                db.Entry(clientProject).State = EntityState.Modified;
                db.SaveChanges();
            }

            return clientProject;
        }

        public static ClientProject AddGlobalMetricsToClientProject(GlobalMetricsMapper globalMetricsMapper, int clientProjectID)
        {
            var db = new DBModelBrandWatchContainer();
            var clientProject = db.ClientProjects.Find(clientProjectID);
            
            if (globalMetricsMapper != null && clientProject.GlobalMetrics.Count == 0)
            {
                var globalMetric = new GlobalMetric();
                globalMetric.IsActive = true;
                globalMetric.FilterName = clientProject.ProjectName + " - " + clientProject.BrandwatchProjectID + " - Filters";
                foreach (var globalMetricAccountType in globalMetricsMapper.accountTypes)
                {
                    AccountType accountType = new AccountType
                    {
                        Active = true,
                        BWAccountType = globalMetricAccountType
                    };
                    globalMetric.AccountTypes.Add(accountType);
                }

                foreach (var globalMetricSentiment in globalMetricsMapper.sentiments)
                {
                    Sentiment sentiment = new Sentiment
                    {
                        Active = true,
                        BWSentiment = globalMetricSentiment
                    };
                    globalMetric.Sentiments.Add(sentiment);
                }

                foreach (var globalMetricCountentSource in globalMetricsMapper.contentSources)
                {
                    ContentSource contentSource = new ContentSource
                    {
                        Active = true,
                        BWContentSource = globalMetricCountentSource.name,
                        BWContentSourceID = globalMetricCountentSource.id
                    };
                    globalMetric.ContentSources.Add(contentSource);
                }

                foreach (var globalMetricGender in globalMetricsMapper.genders)
                {
                    Gender gender = new Gender
                    {
                        Active = true,
                        BWGender = globalMetricGender
                    };
                    globalMetric.Genders.Add(gender);
                }

                foreach (var globalMetricSubType in globalMetricsMapper.subTypes)
                {
                    SubType subType = new SubType
                    {
                        Active = true,
                        BWSubType = globalMetricSubType
                    };
                    globalMetric.SubTypes.Add(subType);
                }

                SourceType sourceType = new SourceType
                {
                    Active = true,
                    BWSourceType = "new"
                };
                globalMetric.SourceTypes.Add(sourceType);

                MentionType mentionType = new MentionType
                {
                    Active = true,
                    TypeOfMention = "reply"
                };
                globalMetric.MentionTypes.Add(mentionType);

                MentionType mentionType1 = new MentionType
                {
                    Active = true,
                    TypeOfMention = "post"
                };
                globalMetric.MentionTypes.Add(mentionType1);

                MentionType mentionType2 = new MentionType
                {
                    Active = true,
                    TypeOfMention = "share"
                };
                globalMetric.MentionTypes.Add(mentionType2);


                ReactScoreOverall reactScoreOverall = new ReactScoreOverall
                {
                    Active = true,
                    ScoreType = "High"
                };
                globalMetric.ReactScoreOveralls.Add(reactScoreOverall);


                ReactScoreOverall reactScoreOverall1 = new ReactScoreOverall
                {
                    Active = true,
                    ScoreType = "Medium"
                };
                globalMetric.ReactScoreOveralls.Add(reactScoreOverall1);

                ReactScoreOverall reactScoreOverall2 = new ReactScoreOverall
                {
                    Active = true,
                    ScoreType = "Low"
                };
                globalMetric.ReactScoreOveralls.Add(reactScoreOverall2);


                ReactScoreHarm reactScoreHarm = new ReactScoreHarm
                {
                    Active = true,
                    ScoreType = "High"
                };
                globalMetric.ReactScoreHarms.Add(reactScoreHarm);

                ReactScoreHarm reactScoreHarm1 = new ReactScoreHarm
                {
                    Active = true,
                    ScoreType = "Medium"
                };
                globalMetric.ReactScoreHarms.Add(reactScoreHarm1);
                ReactScoreHarm reactScoreHarm2 = new ReactScoreHarm
                {
                    Active = true,
                    ScoreType = "Low"
                };
                globalMetric.ReactScoreHarms.Add(reactScoreHarm2);

                ReactScoreEmotionality reactScoreEmotionality = new ReactScoreEmotionality
                {
                    Active = true,
                    ScoreType = "High"
                };
                globalMetric.ReactScoreEmotionalities.Add(reactScoreEmotionality);

                ReactScoreEmotionality reactScoreEmotionality1 = new ReactScoreEmotionality
                {
                    Active = true,
                    ScoreType = "Medium"
                };
                globalMetric.ReactScoreEmotionalities.Add(reactScoreEmotionality1);

                ReactScoreEmotionality reactScoreEmotionality2 = new ReactScoreEmotionality
                {
                    Active = true,
                    ScoreType = "Low"
                };
                globalMetric.ReactScoreEmotionalities.Add(reactScoreEmotionality2);

                Emotion emotion = new Emotion
                {
                    Active = true,
                    EmotionType = "Anger"
                };
                globalMetric.Emotions.Add(emotion);

                Emotion emotion1 = new Emotion
                {
                    Active = true,
                    EmotionType = "Disgust"
                };
                globalMetric.Emotions.Add(emotion1);

                Emotion emotion2 = new Emotion
                {
                    Active = true,
                    EmotionType = "Fear"
                };
                globalMetric.Emotions.Add(emotion2);

                Emotion emotion3 = new Emotion
                {
                    Active = true,
                    EmotionType = "Joy"
                };
                globalMetric.Emotions.Add(emotion3);

                Emotion emotion4 = new Emotion
                {
                    Active = true,
                    EmotionType = "Sadness"
                };
                globalMetric.Emotions.Add(emotion4);

                Emotion emotion5 = new Emotion
                {
                    Active = true,
                    EmotionType = "Surprise"
                };
                globalMetric.Emotions.Add(emotion5);


                SocialPanel socialPanel = new SocialPanel
                {
                    Active = true,
                    PanelName = "Baby Boomers (EN)"
                };
                globalMetric.SocialPanels.Add(socialPanel);

                SocialPanel socialPanel1 = new SocialPanel
                {
                    Active = true,
                    PanelName = "Execs/Business leader"
                };
                globalMetric.SocialPanels.Add(socialPanel1);

                SocialPanel socialPanel2 = new SocialPanel
                {
                    Active = true,
                    PanelName = "Gen X (EN)"
                };
                globalMetric.SocialPanels.Add(socialPanel2);

                SocialPanel socialPanel3 = new SocialPanel
                {
                    Active = true,
                    PanelName = "Gen Y (EN)"
                };
                globalMetric.SocialPanels.Add(socialPanel3);

                SocialPanel socialPanel4 = new SocialPanel
                {
                    Active = true,
                    PanelName = "Gen Z (EN)"
                };
                globalMetric.SocialPanels.Add(socialPanel4);

                SocialPanel socialPanel5 = new SocialPanel
                {
                    Active = true,
                    PanelName = "Gender - Men (EN)"
                };
                globalMetric.SocialPanels.Add(socialPanel5);

                SocialPanel socialPanel6 = new SocialPanel
                {
                    Active = true,
                    PanelName = "Gender - Women (EN)"
                };
                globalMetric.SocialPanels.Add(socialPanel6);

                SocialPanel socialPanel7 = new SocialPanel
                {
                    Active = true,
                    PanelName = "Left Wing Politicians (EN)"
                };
                globalMetric.SocialPanels.Add(socialPanel7);

                SocialPanel socialPanel8 = new SocialPanel
                {
                    Active = true,
                    PanelName = "Reddit Interest - Animals"
                };
                globalMetric.SocialPanels.Add(socialPanel8);

                SocialPanel socialPanel9 = new SocialPanel
                {
                    Active = true,
                    PanelName = "Reddit Interest - Arts & Design"
                };
                globalMetric.SocialPanels.Add(socialPanel9);

                SocialPanel socialPanel10 = new SocialPanel
                {
                    Active = true,
                    PanelName = "Reddit Interest - Automotive"
                };
                globalMetric.SocialPanels.Add(socialPanel10);

                SocialPanel socialPanel11 = new SocialPanel
                {
                    Active = true,
                    PanelName = "Reddit Interest - Business & Finance"
                };
                globalMetric.SocialPanels.Add(socialPanel11);

                SocialPanel socialPanel12 = new SocialPanel
                {
                    Active = true,
                    PanelName = "Reddit Interest - Entertainment"
                };
                globalMetric.SocialPanels.Add(socialPanel12);

                SocialPanel socialPanel13 = new SocialPanel
                {
                    Active = true,
                    PanelName = "Reddit Interest - Family & Relationships"
                };
                globalMetric.SocialPanels.Add(socialPanel13);

                SocialPanel socialPanel14 = new SocialPanel
                {
                    Active = true,
                    PanelName = "Reddit Interest - Food & Drinks"
                };
                globalMetric.SocialPanels.Add(socialPanel14);

                SocialPanel socialPanel15 = new SocialPanel
                {
                    Active = true,
                    PanelName = "Reddit Interest - Gaming"
                };
                globalMetric.SocialPanels.Add(socialPanel15);

                SocialPanel socialPanel16 = new SocialPanel
                {
                    Active = true,
                    PanelName = "Reddit Interest - Healthy Living"
                };
                globalMetric.SocialPanels.Add(socialPanel16);

                SocialPanel socialPanel17 = new SocialPanel
                {
                    Active = true,
                    PanelName = "Reddit Interest - News & Education"
                };
                globalMetric.SocialPanels.Add(socialPanel17);

                SocialPanel socialPanel18 = new SocialPanel
                {
                    Active = true,
                    PanelName = "Reddit Interest - Sports"
                };
                globalMetric.SocialPanels.Add(socialPanel18);

                SocialPanel socialPanel19 = new SocialPanel
                {
                    Active = true,
                    PanelName = "Reddit Interest - Style & Fashion"
                };
                globalMetric.SocialPanels.Add(socialPanel19);

                SocialPanel socialPanel20 = new SocialPanel
                {
                    Active = true,
                    PanelName = "Reddit Interest - Technology & Computing"
                };
                globalMetric.SocialPanels.Add(socialPanel20);

                SocialPanel socialPanel21 = new SocialPanel
                {
                    Active = true,
                    PanelName = "Reddit Interest - Television & Film"
                };
                globalMetric.SocialPanels.Add(socialPanel21);

                SocialPanel socialPanel22 = new SocialPanel
                {
                    Active = true,
                    PanelName = "Reddit Interest - Travel"
                };
                globalMetric.SocialPanels.Add(socialPanel22);

                SocialPanel socialPanel23 = new SocialPanel
                {
                    Active = true,
                    PanelName = "Reddit Interest - Right Wing Politicians"
                };
                globalMetric.SocialPanels.Add(socialPanel23);


                SocialPanel socialPanel24 = new SocialPanel
                {
                    Active = true,
                    PanelName = "Reddit Interest - Vegans"
                };
                globalMetric.SocialPanels.Add(socialPanel24);


                Assignment assignment = new Assignment
                {
                    EmployeeUserName = "a.abdulrazaq@elaqat.com.sa",
                    Active = true
                };
                globalMetric.Assignments.Add(assignment);

                Assignment assignment1 = new Assignment
                {
                    EmployeeUserName = "a.alalawi@elaqat.com.sa",
                    Active = true
                };
                globalMetric.Assignments.Add(assignment1);

                Assignment assignment2 = new Assignment
                {
                    EmployeeUserName = "A.alshuraihi@elaqat.com.sa",
                    Active = true
                };
                globalMetric.Assignments.Add(assignment2);

                Assignment assignment3 = new Assignment
                {
                    EmployeeUserName = "a.asiri@elaqat.com.sa",
                    Active = true
                };
                globalMetric.Assignments.Add(assignment3);

                Assignment assignment4 = new Assignment
                {
                    EmployeeUserName = "a.attaullah@elaqat.com.sa",
                    Active = true
                };
                globalMetric.Assignments.Add(assignment4);

                Assignment assignment5 = new Assignment
                {
                    EmployeeUserName = "ab.alfawaz@hrsd.gov.sa",
                    Active = true
                };
                globalMetric.Assignments.Add(assignment5);

                Assignment assignment6 = new Assignment
                {
                    EmployeeUserName = "b.omar@elaqat.com.sa",
                    Active = true
                };
                globalMetric.Assignments.Add(assignment6);

                Assignment assignment7 = new Assignment
                {
                    EmployeeUserName = "Hlaby.m@gmail.com",
                    Active = true
                };
                globalMetric.Assignments.Add(assignment7);

                Assignment assignment8 = new Assignment
                {
                    EmployeeUserName = "jbustji@hrsd.gov.sa",
                    Active = true
                };
                globalMetric.Assignments.Add(assignment8);

                Assignment assignment9 = new Assignment
                {
                    EmployeeUserName = "khulood@elaqat.com.sa",
                    Active = true
                };
                globalMetric.Assignments.Add(assignment9);

                Assignment assignment10 = new Assignment
                {
                    EmployeeUserName = "m.alrizqi@hrsd.gov.sa",
                    Active = true
                };
                globalMetric.Assignments.Add(assignment10);

                Assignment assignment11 = new Assignment
                {
                    EmployeeUserName = "media@elaqat.com.sa",
                    Active = true
                };
                globalMetric.Assignments.Add(assignment11);

                Assignment assignment12 = new Assignment
                {
                    EmployeeUserName = "n.alkathiri@elaqat.com.sa",
                    Active = true
                };
                globalMetric.Assignments.Add(assignment12);

                Assignment assignment13 = new Assignment
                {
                    EmployeeUserName = "r.ibrahim@elaqat.com.sa",
                    Active = true
                };
                globalMetric.Assignments.Add(assignment13);

                Assignment assignment14 = new Assignment
                {
                    EmployeeUserName = "rjalshehri@hrsd.gov.sa",
                    Active = true
                };
                globalMetric.Assignments.Add(assignment14);

                Assignment assignment15 = new Assignment
                {
                    EmployeeUserName = "s.alzubairi@elaqat.com.sa",
                    Active = true
                };
                globalMetric.Assignments.Add(assignment15);

                Assignment assignment16 = new Assignment
                {
                    EmployeeUserName = "saalenezy@hrsd.gov.sa",
                    Active = true
                };
                globalMetric.Assignments.Add(assignment16);


                CheckedMention checkedMention = new CheckedMention
                {
                    CheckType = "Yes",
                    Active = true

                };
                globalMetric.CheckedMentions.Add(checkedMention);

                CheckedMention checkedMention1 = new CheckedMention
                {
                    CheckType = "No",
                    Active = true

                };
                globalMetric.CheckedMentions.Add(checkedMention1);


                Priority priority = new Priority
                {
                    PriorityType = "High",
                    Active = true
                };
                globalMetric.Priorities.Add(priority);


                Priority priority1 = new Priority
                {
                    PriorityType = "Medium",
                    Active = true
                };
                globalMetric.Priorities.Add(priority1);

                Priority priority2 = new Priority
                {
                    PriorityType = "Low",
                    Active = true
                };
                globalMetric.Priorities.Add(priority2);

              

                MentionStatus mentionStatus1 = new MentionStatus
                {
                    Status = "Open",
                    Active = true
                };
                globalMetric.MentionStatus.Add(mentionStatus1);

                MentionStatus mentionStatus2 = new MentionStatus
                {
                    Status = "Pending",
                    Active = true
                };
                globalMetric.MentionStatus.Add(mentionStatus2);

                MentionStatus mentionStatus3 = new MentionStatus
                {
                    Status = "Closed",
                    Active = true
                };
                globalMetric.MentionStatus.Add(mentionStatus3);


                TwitterUserRole twitterUserRole = new TwitterUserRole
                {
                    Active = true,
                    RoleName = "Owner"
                };
                globalMetric.TwitterUserRoles.Add(twitterUserRole);


                TwitterUserRole twitterUserRole1 = new TwitterUserRole
                {
                    Active = true,
                    RoleName = "Audience"
                };
                globalMetric.TwitterUserRoles.Add(twitterUserRole1);

                TwitterMentionType twitterMentionType = new TwitterMentionType
                {
                    Active = true,
                    MentionTypeOfTwitter = "POST"
                };
                globalMetric.TwitterMentionTypes.Add(twitterMentionType);


                TwitterMentionType twitterMentionType1 = new TwitterMentionType
                {
                    Active = true,
                    MentionTypeOfTwitter = "QUOTE "
                };
                globalMetric.TwitterMentionTypes.Add(twitterMentionType1);


                TwitterMentionType twitterMentionType2 = new TwitterMentionType
                {
                    Active = true,
                    MentionTypeOfTwitter = "REPLY"
                };
                globalMetric.TwitterMentionTypes.Add(twitterMentionType2);


                TwitterMentionType twitterMentionType3 = new TwitterMentionType
                {
                    Active = true,
                    MentionTypeOfTwitter = "REPOST"
                };
                globalMetric.TwitterMentionTypes.Add(twitterMentionType3);


                TwitterVerification twitterVerification = new TwitterVerification
                {
                    Active = true,
                    Verification = "Verified"
                };
                globalMetric.TwitterVerifications.Add(twitterVerification);

                TwitterVerification twitterVerification1 = new TwitterVerification
                {
                    Active = true,
                    Verification = "Unverified"
                };
                globalMetric.TwitterVerifications.Add(twitterVerification1);


                TwitterVerificationType twitterVerificationType = new TwitterVerificationType
                {
                    Active = true,
                    VerificationType = "Blue"
                };
                globalMetric.TwitterVerificationTypes.Add(twitterVerificationType);

                TwitterVerificationType twitterVerificationType1 = new TwitterVerificationType
                {
                    Active = true,
                    VerificationType = "Business"
                };
                globalMetric.TwitterVerificationTypes.Add(twitterVerificationType1);

                TwitterVerificationType twitterVerificationType2 = new TwitterVerificationType
                {
                    Active = true,
                    VerificationType = "Government"
                };
                globalMetric.TwitterVerificationTypes.Add(twitterVerificationType2);


                FaceBookUserRole faceBookUserRole = new FaceBookUserRole
                {
                    Active = true,
                    UserRoleName = "Page Owner"
                };
                globalMetric.FaceBookUserRoles.Add(faceBookUserRole);


                FaceBookUserRole faceBookUserRole1 = new FaceBookUserRole
                {
                    Active = true,
                    UserRoleName = "Audience"
                };
                globalMetric.FaceBookUserRoles.Add(faceBookUserRole1);


                FaceBookMentionType faceBookMentionType = new FaceBookMentionType
                {
                    Active = true,
                    MentionTypeOfFaceBook = "public-facebook-comment"
                };
                globalMetric.FaceBookMentionTypes.Add(faceBookMentionType);

                FaceBookMentionType faceBookMentionType1 = new FaceBookMentionType
                {
                    Active = true,
                    MentionTypeOfFaceBook = "dark-facebook-comment"
                };
                globalMetric.FaceBookMentionTypes.Add(faceBookMentionType1);

                FaceBookMentionType faceBookMentionType2 = new FaceBookMentionType
                {
                    Active = true,
                    MentionTypeOfFaceBook = "dark-facebook-post"
                };
                globalMetric.FaceBookMentionTypes.Add(faceBookMentionType2);

                FaceBookMentionType faceBookMentionType3 = new FaceBookMentionType
                {
                    Active = true,
                    MentionTypeOfFaceBook = "public-facebook-post"
                };
                globalMetric.FaceBookMentionTypes.Add(faceBookMentionType3);


                InstagramMentionType instagramMentionType = new InstagramMentionType
                {
                    Active = true,
                    MentionTypeOfInstgram = "instagram-graph-at-mentioned"
                };
                globalMetric.InstagramMentionTypes.Add(instagramMentionType);

                InstagramMentionType instagramMentionType1 = new InstagramMentionType
                {
                    Active = true,
                    MentionTypeOfInstgram = "instagram-graph-ad"
                };
                globalMetric.InstagramMentionTypes.Add(instagramMentionType1);

                InstagramMentionType instagramMentionType2 = new InstagramMentionType
                {
                    Active = true,
                    MentionTypeOfInstgram = "instagram-graph-hashtag"
                };
                globalMetric.InstagramMentionTypes.Add(instagramMentionType2);

                InstagramMentionType instagramMentionType3 = new InstagramMentionType
                {
                    Active = true,
                    MentionTypeOfInstgram = "instagram-graph-non-owned"
                };
                globalMetric.InstagramMentionTypes.Add(instagramMentionType3);

                InstagramMentionType instagramMentionType4 = new InstagramMentionType
                {
                    Active = true,
                    MentionTypeOfInstgram = "instagram-graph-owned"
                };
                globalMetric.InstagramMentionTypes.Add(instagramMentionType4);

                InstagramMentionType instagramMentionType5 = new InstagramMentionType
                {
                    Active = true,
                    MentionTypeOfInstgram = "instagram-graph-tagged"
                };
                globalMetric.InstagramMentionTypes.Add(instagramMentionType5);


                RedditPoll redditPoll = new RedditPoll
                {
                    Active = true,
                    PollChooice= "Yes",
                };
                globalMetric.RedditPolls.Add(redditPoll);

                RedditPoll redditPoll1 = new RedditPoll
                {
                    Active = true,
                    PollChooice = "No",
                };
                globalMetric.RedditPolls.Add(redditPoll1);

                RedditNSFW redditNsfw = new RedditNSFW
                {
                    Active = true,
                    NSFWChooice = "Yes",
                };
                globalMetric.RedditNSFWs.Add(redditNsfw);

                RedditNSFW redditNsfw1 = new RedditNSFW
                {
                    Active = true,
                    NSFWChooice = "No",
                };
                globalMetric.RedditNSFWs.Add(redditNsfw1);


                SubredditTopic subredditTopic = new SubredditTopic
                {
                    Active = true,
                    TopicName = "3D Printing"
                };
                globalMetric.SubredditTopics.Add(subredditTopic);

                SubredditTopic subredditTopic1 = new SubredditTopic
                {
                    Active = true,
                    TopicName = "Accessories & Jewelry"
                };
                globalMetric.SubredditTopics.Add(subredditTopic1);

                SubredditTopic subredditTopic2 = new SubredditTopic
                {
                    Active = true,
                    TopicName = "Action Games"
                };
                globalMetric.SubredditTopics.Add(subredditTopic2);

                SubredditTopic subredditTopic3 = new SubredditTopic
                {
                    Active = true,
                    TopicName = "Action Movies & Series"
                };
                globalMetric.SubredditTopics.Add(subredditTopic3);

                SubredditTopic subredditTopic4 = new SubredditTopic
                {
                    Active = true,
                    TopicName = "Activism"
                };
                globalMetric.SubredditTopics.Add(subredditTopic4);


                SubredditTopic subredditTopic5 = new SubredditTopic
                {
                    Active = true,
                    TopicName = "Addiction Support"
                };
                globalMetric.SubredditTopics.Add(subredditTopic5);

                SubredditTopic subredditTopic6 = new SubredditTopic
                {
                    Active = true,
                    TopicName = "Adventure Games"
                };
                globalMetric.SubredditTopics.Add(subredditTopic6);

                SubredditTopic subredditTopic7 = new SubredditTopic
                {
                    Active = true,
                    TopicName = "Amazing"
                };
                globalMetric.SubredditTopics.Add(subredditTopic7);

                SubredditTopic subredditTopic8 = new SubredditTopic
                {
                    Active = true,
                    TopicName = "Animals & Pets"
                };
                globalMetric.SubredditTopics.Add(subredditTopic8);

                SubredditTopic subredditTopic9 = new SubredditTopic
                {
                    Active = true,
                    TopicName = "Animated Movies & Series"
                };
                globalMetric.SubredditTopics.Add(subredditTopic9);

                TwitterProfession twitterProfession = new TwitterProfession
                {
                    Active = true,
                    Profession = "Artist"
                };
                globalMetric.TwitterProfessions.Add(twitterProfession);

                TwitterProfession twitterProfession1 = new TwitterProfession
                {
                    Active = true,
                    Profession = "Executive"
                };
                globalMetric.TwitterProfessions.Add(twitterProfession1);
                TwitterProfession twitterProfession2 = new TwitterProfession
                {
                    Active = true,
                    Profession = "Health practitioner"
                };
                globalMetric.TwitterProfessions.Add(twitterProfession2);
                TwitterProfession twitterProfession3 = new TwitterProfession
                {
                    Active = true,
                    Profession = "Journalist"
                };
                globalMetric.TwitterProfessions.Add(twitterProfession3);
                TwitterProfession twitterProfession4 = new TwitterProfession
                {
                    Active = true,
                    Profession = "Legal"
                };
                globalMetric.TwitterProfessions.Add(twitterProfession4);
                TwitterProfession twitterProfession5 = new TwitterProfession
                {
                    Active = true,
                    Profession = "Politician"
                };
                globalMetric.TwitterProfessions.Add(twitterProfession5);
                TwitterProfession twitterProfession6 = new TwitterProfession
                {
                    Active = true,
                    Profession = "Sales/Marketing/PR"
                };
                globalMetric.TwitterProfessions.Add(twitterProfession6);
                TwitterProfession twitterProfession7 = new TwitterProfession
                {
                    Active = true,
                    Profession = "Scientist & Researcher"
                };
                globalMetric.TwitterProfessions.Add(twitterProfession7);
                TwitterProfession twitterProfession8 = new TwitterProfession
                {
                    Active = true,
                    Profession = "Software developer & IT"
                };
                globalMetric.TwitterProfessions.Add(twitterProfession8);
                TwitterProfession twitterProfession9 = new TwitterProfession
                {
                    Active = true,
                    Profession = "Sportpersons & Trainer"
                };
                globalMetric.TwitterProfessions.Add(twitterProfession9);
                TwitterProfession twitterProfession10 = new TwitterProfession
                {
                    Active = true,
                    Profession = "Student"
                };
                globalMetric.TwitterProfessions.Add(twitterProfession10);
                TwitterProfession twitterProfession11 = new TwitterProfession
                {
                    Active = true,
                    Profession = "Teacher & Lecturer"
                };
                globalMetric.TwitterProfessions.Add(twitterProfession11);


                TwitterInterest twitterInterest = new TwitterInterest
                {
                    Active = true,
                    Interest = "Animals & Pets"
                };
                globalMetric.TwitterInterests.Add(twitterInterest);

                TwitterInterest twitterInterest1 = new TwitterInterest
                {
                    Active = true,
                    Interest = "Automotive"
                };
                globalMetric.TwitterInterests.Add(twitterInterest1);

                TwitterInterest twitterInterest2 = new TwitterInterest
                {
                    Active = true,
                    Interest = "Beauty/Health & Fitness"
                };
                globalMetric.TwitterInterests.Add(twitterInterest2);

                TwitterInterest twitterInterest3 = new TwitterInterest
                {
                    Active = true,
                    Interest = "Books"
                };
                globalMetric.TwitterInterests.Add(twitterInterest3);

                TwitterInterest twitterInterest4 = new TwitterInterest
                {
                    Active = true,
                    Interest = "Business"
                };
                globalMetric.TwitterInterests.Add(twitterInterest4);

                TwitterInterest twitterInterest5 = new TwitterInterest
                {
                    Active = true,
                    Interest = "Environment"
                };
                globalMetric.TwitterInterests.Add(twitterInterest5);

                TwitterInterest twitterInterest6 = new TwitterInterest
                {
                    Active = true,
                    Interest = "Family & Parenting"
                };
                globalMetric.TwitterInterests.Add(twitterInterest6);

                TwitterInterest twitterInterest7 = new TwitterInterest
                {
                    Active = true,
                    Interest = "Fashion"
                };
                globalMetric.TwitterInterests.Add(twitterInterest7);

                TwitterInterest twitterInterest8 = new TwitterInterest
                {
                    Active = true,
                    Interest = "Fine arts"
                };
                globalMetric.TwitterInterests.Add(twitterInterest8);

                TwitterInterest twitterInterest9 = new TwitterInterest
                {
                    Active = true,
                    Interest = "Food & Drinks"
                };
                globalMetric.TwitterInterests.Add(twitterInterest9);


                TwitterInterest twitterInterest10 = new TwitterInterest
                {
                    Active = true,
                    Interest = "Games"
                };
                globalMetric.TwitterInterests.Add(twitterInterest10);

                TwitterInterest twitterInterest11 = new TwitterInterest
                {
                    Active = true,
                    Interest = "Movies"
                };
                globalMetric.TwitterInterests.Add(twitterInterest11);
                TwitterInterest twitterInterest12 = new TwitterInterest
                {
                    Active = true,
                    Interest = "Music"
                };
                globalMetric.TwitterInterests.Add(twitterInterest12);
                TwitterInterest twitterInterest13 = new TwitterInterest
                {
                    Active = true,
                    Interest = "Photo & Video"
                };
                globalMetric.TwitterInterests.Add(twitterInterest13);
                TwitterInterest twitterInterest14 = new TwitterInterest
                {
                    Active = true,
                    Interest = "Politics"
                };
                globalMetric.TwitterInterests.Add(twitterInterest14);
                TwitterInterest twitterInterest15 = new TwitterInterest
                {
                    Active = true,
                    Interest = "Science"
                };
                globalMetric.TwitterInterests.Add(twitterInterest15);
                TwitterInterest twitterInterest16 = new TwitterInterest
                {
                    Active = true,
                    Interest = "Shopping"
                };
                globalMetric.TwitterInterests.Add(twitterInterest16);
                TwitterInterest twitterInterest17 = new TwitterInterest
                {
                    Active = true,
                    Interest = "Sports"
                };
                globalMetric.TwitterInterests.Add(twitterInterest17);
                TwitterInterest twitterInterest18 = new TwitterInterest
                {
                    Active = true,
                    Interest = "Technology"
                };
                globalMetric.TwitterInterests.Add(twitterInterest18);

                TwitterInterest twitterInterest19 = new TwitterInterest
                {
                    Active = true,
                    Interest = "Travel"
                };
                globalMetric.TwitterInterests.Add(twitterInterest19);

                TwitterInterest twitterInterest20 = new TwitterInterest
                {
                    Active = true,
                    Interest = "TV"
                };
                globalMetric.TwitterInterests.Add(twitterInterest20);

                globalMetric.ClientProject = clientProject;
                globalMetric.ClientProjectId = clientProject.Id;
                db.GlobalMetrics.Add(globalMetric);
                db.Entry(clientProject).State = EntityState.Modified;
                db.SaveChanges();
            }
           



            return clientProject;
        }


        public static ClientProject AddSitesToClientProject(List<SiteMapper> siteMappers, int clientProjectID)
        {
            var db = new DBModelBrandWatchContainer();
            List<SiteMapper> localSiteMappersList = new List<SiteMapper>();
            var clientProject = db.ClientProjects.Find(clientProjectID);
            if (siteMappers.Any())
            {
                foreach (var siteGroup in clientProject.ClientProjectSites)
                {
                    db.SiteDomains.RemoveRange(siteGroup.SiteDomains);
                }
                
                db.ClientProjectSites.RemoveRange(clientProject.ClientProjectSites);
              
                foreach (var site in siteMappers)
                {
                    ClientProjectSite clientProjectSite = new ClientProjectSite();
                    clientProjectSite.BrandWatchSiteID = site.id;
                    clientProjectSite.SiteName = site.name;
                    clientProjectSite.Shared = site.shared;
                    clientProjectSite.IsActive = true;

                    foreach (var domain in site.domains)
                    {
                        SiteDomain siteDomain = new SiteDomain();
                        siteDomain.DomainURL = domain;
                        clientProjectSite.SiteDomains.Add(siteDomain);
                    }
                    clientProject.ClientProjectSites.Add(clientProjectSite);
                }
                db.Entry(clientProject).State = EntityState.Modified;
                db.SaveChanges();
            }          

            return clientProject;
        }



        public static ClientProjectVM PrepareElaqatClientClientProjectToBeEdit(int clientProjectId)
        {
            var db = new DBModelBrandWatchContainer();
            ClientProject clientProject = db.ClientProjects.Find(clientProjectId);
            ClientProjectVM clientProjectVM = new ClientProjectVM();
            clientProjectVM.Id = clientProject.Id;
            clientProjectVM.ProjectName = clientProject.ProjectName;
            clientProjectVM.ProjectBrandWatchID = clientProject.BrandwatchProjectID;
            clientProjectVM.ProjectCreationDate = clientProject.ProjectCreationDate;
            clientProjectVM.BrandWatchClientName = clientProject.BrandWatchClientName;
            clientProjectVM.Description = clientProject.Description;
            clientProjectVM.BrandWatchClientId = clientProject.BrandWatchClientId;
            clientProjectVM.ElaqatClientName = clientProject.ElaqatClient.ClientName;
            clientProjectVM.IsActive = clientProject.IsActive;
            clientProjectVM.ShowMentionsStartingFrom = clientProject.ShowMentionsStartingFrom;
            clientProjectVM.ClientList = new SelectList(db.ElaqatClients, "Id", "ClientName");
            clientProjectVM.SelectedElaqatClientId = clientProject.ElaqatClientId;
            return clientProjectVM; 
        }
        public static ClientProjectVM PrepareClientProjectToBeAdded(int clientId)
        {
            var db = new DBModelBrandWatchContainer();
            var client = db.ElaqatClients.Find(clientId);
            ClientProjectVM clientProjectVM = new ClientProjectVM();
            clientProjectVM.ClientList = new SelectList(db.ElaqatClients, "Id", "ClientName");
            clientProjectVM.ClientProjectList = new SelectList(db.BrandWatchClientProjects, "Id", "ProjectName");
            clientProjectVM.SelectedElaqatClientId = client.Id;            
            return clientProjectVM; ;
        }

        public static ClientProjectVM PrepareClientProjectToBeAssigned(int brandWatchProjectId)
        {
            var db = new DBModelBrandWatchContainer();
            var clientproject = db.BrandWatchClientProjects.Find(brandWatchProjectId);            
            ClientProjectVM clientProjectVM = new ClientProjectVM();
            clientProjectVM.Id = clientproject.Id;
            clientProjectVM.ClientList = new SelectList(db.ElaqatClients, "Id", "ClientName");
            clientProjectVM.ProjectName = clientproject.ProjectName;
            clientProjectVM.ProjectBrandWatchID = clientproject.BrandwatchProjectID;
            clientProjectVM.ProjectCreationDate = clientproject.ProjectCreationDate;

            return clientProjectVM; ;
        }

        public static ClientProjectVM PrepareImportedClientProjectToBeAssigned(int ProjectId)
        {
            var db = new DBModelBrandWatchContainer();
            var clientproject = db.ClientProjects.Find(ProjectId);
            ClientProjectVM clientProjectVM = new ClientProjectVM();
            clientProjectVM.Id = clientproject.Id;
            clientProjectVM.ClientList = new SelectList(db.ElaqatClients, "Id", "ClientName");
            clientProjectVM.ProjectName = clientproject.ProjectName;
            clientProjectVM.ProjectBrandWatchID = clientproject.BrandwatchProjectID;
            clientProjectVM.ProjectCreationDate = clientproject.ProjectCreationDate;

            return clientProjectVM; ;
        }


       
    }
}