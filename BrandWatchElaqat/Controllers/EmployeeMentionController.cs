using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Mvc;
using BrandWatchElaqat.Models;
using BrandWatchElaqat.ViewModel;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.Linq;

namespace BrandWatchElaqat.Controllers
{
    [Authorize(Roles = "Employee,Admin")]
    public class EmployeeMentionController : Controller
    {
       private List<MentionMapper> MentionMapperList = new List<MentionMapper>();
       
        private async Task<List<Root>> GetBrandWatchTweeterAuthors(URLPreParer uRLPreParer)
        {
            try
            {
                //string pageNumber = "&page=" + requestResultsParameter._ResultsPage.ToString();
                //uRLPreParer.CompleteParametersForAPI += pageNumber;

                string apiUrl = uRLPreParer.CompleteParametersForTweeterAuthorAPI;
                List<Root> RootList = new List<Root>();

                //string apiUrl = "https://api.brandwatch.com/projects/1998394614/data/mentions?queryId=2003520601&startDate=2025-08-05&endDate=2025-08-06&pageSize=500&category=27522928,38544477";

                using (HttpClient client = new HttpClient())
                {
                    client.BaseAddress = new Uri(apiUrl);
                    client.DefaultRequestHeaders.Add("Authorization", "Bearer " + uRLPreParer.token);
                    client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                    HttpResponseMessage response = await client.GetAsync(apiUrl);

                    if (response.IsSuccessStatusCode)
                    {
                        var data = await response.Content.ReadAsStringAsync();
                        Dictionary<string, object> dictionarydata = JsonConvert.DeserializeObject<Dictionary<string, object>>(data);
                        foreach (KeyValuePair<string, object> entry in dictionarydata)
                        {
                            if (entry.Key == "resultsTotal")
                            {
                                string requestTotalMention = entry.Value.ToString();
                            }


                            if (entry.Key == "results")
                            {
                                string myjson = entry.Value.ToString();
                                //MentionMapperList = JsonConvert.DeserializeObject<List<MentionMapper>>(myjson);
                                RootList.AddRange(JsonConvert.DeserializeObject<List<Root>>(myjson));

                            }

                            
                            if (entry.Key == "resultsPage")
                            {
                                string requestpageresult = entry.Value.ToString();

                            }

                            if (entry.Key == "resultsPageSize")
                            {
                                string requestresultsPageSize = entry.Value.ToString();

                            }
                        }
                    }
                }
                

                return RootList;
            }
            catch (Exception ex)
            {
                var resultdata = ex.Data;
                var resultInner = ex.InnerException;
                var resultMessag = ex.Message;
                throw;
            }



        }

        private async Task<RequestResultsParameter> GetBrandWatchClientProjectMentions(URLPreParer uRLPreParer, RequestResultsParameter requestResultsParameter)
        {
            try
            {
                //string pageNumber = "&page=" + requestResultsParameter._ResultsPage.ToString();
                //uRLPreParer.CompleteParametersForAPI += pageNumber;

                string apiUrl = "";
                if (!String.IsNullOrEmpty(requestResultsParameter._NextCursor))
                {
                    string nextCursor = "&cursor=";
                    apiUrl = uRLPreParer.CompleteParametersForAPIForCursor + nextCursor + requestResultsParameter._NextCursor;
                   // uRLPreParer.CompleteParametersForAPIForCursor = nextCursor + requestResultsParameter._NextCursor;
                   // apiUrl = uRLPreParer.CompleteParametersForAPIForCursor;
                }
                else
                {
                    apiUrl = uRLPreParer.CompleteParametersForAPI;
                }

                requestResultsParameter._NextCursor = "";
                //string apiUrl = "https://api.brandwatch.com/projects/1998394614/data/mentions?queryId=2003520601&startDate=2025-08-05&endDate=2025-08-06&pageSize=500&category=27522928,38544477";

                using (HttpClient client = new HttpClient())
                {
                    client.BaseAddress = new Uri(apiUrl);
                    client.DefaultRequestHeaders.Add("Authorization", "Bearer " + uRLPreParer.token);
                    client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                    HttpResponseMessage response = await client.GetAsync(apiUrl);
                   
                    if (response.IsSuccessStatusCode)
                    {
                        var data = await response.Content.ReadAsStringAsync();
                        Dictionary<string, object> dictionarydata = JsonConvert.DeserializeObject<Dictionary<string, object>>(data);
                        foreach (KeyValuePair<string, object> entry in dictionarydata)
                        {
                            if (entry.Key == "resultsTotal")
                            {
                                string requestTotalMention = entry.Value.ToString();
                                requestResultsParameter._RequestTotalMention = Convert.ToInt32(requestTotalMention);
                            }


                            if (entry.Key == "results")
                            {
                                string myjson = entry.Value.ToString();
                                //MentionMapperList = JsonConvert.DeserializeObject<List<MentionMapper>>(myjson);
                                MentionMapperList.AddRange(JsonConvert.DeserializeObject<List<MentionMapper>>(myjson));
                               
                            }

                            if (entry.Key == "nextCursor")
                            {
                                requestResultsParameter._NextCursor = entry.Value.ToString();
                            }
                            if (entry.Key == "resultsPage")
                            {
                                string requestpageresult = entry.Value.ToString();
                                requestResultsParameter._ResultsPage = Convert.ToInt32(requestpageresult);                                

                            }

                            if (entry.Key == "resultsPageSize")
                            {
                                string requestresultsPageSize = entry.Value.ToString();
                                requestResultsParameter._ReceivedMention += Convert.ToInt32(requestresultsPageSize);

                            }
                        }
                    }

                   
                }
                requestResultsParameter._RequestTotalMention -= requestResultsParameter._ReceivedMention;
                
                return requestResultsParameter;
            }
            catch (Exception ex)
            {
                var resultdata = ex.Data;
                var resultInner = ex.InnerException;
                var resultMessag = ex.Message;
                throw;
            }



        }

        //GET: EmployeeMention
        
        public ActionResult GetMentionsByProjectID(string msg, int id)
        {
            if (id > 0)
            {
                ViewBag.Message = msg;
                var project = ClientProject.GetClientProjectDetails(id);
                if (project == null)
                {
                    msg = Resources.Projects.ProjectIsNotFound;
                    return RedirectToAction("Index", "ClientProject", new { msg});
                }
                return View(project);
            }
            else
            {
                msg = Resources.Mention.ClientOrProjectDetailsAreMissing;
                return RedirectToAction("Index", "ClientProject", new { msg });
            }
          
        }


        // GET: EmployeeMention/Delete Project Mentions
        [HttpGet]
        public ActionResult DeleteAllProjectMentionFromProject(int id)
        {
            string msg = Resources.Projects.ThisDeedIsIrreversible;
            if (id > 0)
            {
                ViewBag.Message = msg;
                var project = ClientProject.GetClientProjectDetails(id);
                if (project == null)
                {
                    msg = Resources.Projects.ProjectIsNotFound;
                    return RedirectToAction("Index", "ClientProject", new { msg });
                }
                return View(project);
            }
            else
            {
                msg = Resources.Mention.ClientOrProjectDetailsAreMissing;
                return RedirectToAction("Index", "ClientProject", new { msg });
            }
        }

        // Post: EmployeeMention/Details/5
        [HttpPost]
        public ActionResult DeleteAllProjectMentionFromProject(ClientProject clientProject)
        {
            Task saveToDataBase = Task.Run(() => ProjectMention.DeleteAllProjectMentionsFromProject(clientProject.Id));
            // var modelId = ProjectMention.DeleteAllProjectMentionsFromProject(clientProject.Id);
            string msg = Resources.DashBoard.MentionsAreDeleting;            
            return RedirectToAction("GetMentionsForProject", new { msg, id = clientProject.Id });
        }
       

       
       
       

        // GET: EmployeeMention/Create
        [HttpGet]
        public ActionResult GetMentionsForProject(string msg, int id)
        {
            var clientProject = ClientProject.GetClientProjectDetails(id);
            if (clientProject != null)
            {
                if (!clientProject.IsActive)
                {
                    msg = Resources.Projects.ProjectIsNotActive;
                    return RedirectToAction("GetMentionsByProjectID", new { msg, id });
                }
               
            }
            ViewBag.Message = msg;
            var model = ProjectMention.PrepareMentionFilterToImportMentionsForProject(id);
            if (model != null)
            {
                List<Root> RootList = new List<Root>();
                model.DashBoard = ProjectMention.PrePareDashBoardVM(MentionMapperList, RootList);
                return View(model);
            }
            msg = Resources.Mention.ClientOrProjectDetailsAreMissing;
            return RedirectToAction("GetMentionsByProjectID", new { msg, id });
        }

        // POST: EmployeeMention/Create
        [HttpPost]
        public async Task<ActionResult> GetMentionsForProject(MentionFilterVM mentionFilterVM)
        {
            string msg = "";
            // TODO: Add insert logic here
            try
            {
                var urlPreParer = ProjectMention.PrepareUrlTobeCaled(mentionFilterVM);               

                if (urlPreParer == null)
                {
                     msg = Resources.Mention.ClientOrProjectDetailsAreMissing;

                    ViewBag.Message = msg;
                    var model = ProjectMention.PrepareMentionFilterToImportMentionsForProject(mentionFilterVM.SelectedClientProjectId); 
                    return RedirectToAction("GetMentionsForProject", new { msg, id = urlPreParer.projectId });

                }

                RequestResultsParameter requestResultsParameter = new RequestResultsParameter();
                requestResultsParameter._RequestTotalMention = 1;

                int page = requestResultsParameter._RequestTotalMention;
                for (int i = 0; i < page; i++)
                {
                    if (page > 0)
                    {
                        requestResultsParameter._ResultsPage = requestResultsParameter._ResultsPage + 1;
                        requestResultsParameter = await GetBrandWatchClientProjectMentions(urlPreParer, requestResultsParameter);
                        page = requestResultsParameter._RequestTotalMention;
                    }

                }
               
                if (MentionMapperList.Any())
                {
                    // Task saveToDataBase = Task.Run(()=>ProjectMention.AddMentionToClientProject(MentionMapperList, urlPreParer.projectId));
                    //var result = ProjectMention.AddMentionToClientProject(MentionMapperList, urlPreParer.projectId);
                     msg = Resources.DashBoard.MentionsImportingIsCompleted;

                    ViewBag.Message = msg;
                    var model = ProjectMention.PrepareMentionFilterToImportMentionsForProject(mentionFilterVM.SelectedClientProjectId);
                    var authorMapperList = await GetBrandWatchTweeterAuthors(urlPreParer);                   
                    model.DashBoard = ProjectMention.PrePareDashBoardVM(MentionMapperList, authorMapperList);
                    return View(model);
                }
                // return RedirectToAction("GetMentionsByProjectID", new { id = urlPreParer.projectId });
                ViewBag.Message = Resources.DashBoard.NoData;
                var _model = ProjectMention.PrepareMentionFilterToImportMentionsForProject(mentionFilterVM.SelectedClientProjectId);
                List<Root> RootList = new List<Root>();
                _model.DashBoard = ProjectMention.PrePareDashBoardVM(MentionMapperList, RootList);
                return View(_model);
            }
            catch (Exception ex)
            {

                msg = ex.Message;
                ViewBag.Message = msg;
                var model = ProjectMention.PrepareMentionFilterToImportMentionsForProject(mentionFilterVM.SelectedClientProjectId);
                List<Root> RootList = new List<Root>();
                model.DashBoard = ProjectMention.PrePareDashBoardVM(MentionMapperList, RootList);
                return View(model);
            }



        }
       

        /// <summary>
        /// Included Category Section
        /// </summary>
        /// <param name="mentionFilterID"></param>
        /// <returns></returns>
        [AcceptVerbs(HttpVerbs.Get | HttpVerbs.Post)]
        public ActionResult GetAllIncludedCategoeries(int mentionFilterID)
        {
            var listOfIncludedCategories = ProjectMention.GetAllIncludedCategories(mentionFilterID);
            return PartialView(listOfIncludedCategories);
        }

        [HttpGet]
        public virtual JsonResult AddCategoryToMentionFilter(int mentionFilterID, int categoryID)
        {
            string msg = "";
            try
            {
                msg = ProjectMention.AddIncludedCategoryToMentionFilter(mentionFilterID, categoryID);
                return Json(msg, JsonRequestBehavior.AllowGet);


            }
            catch (Exception ex)
            {
                msg = ex.Message;
                return Json(msg, JsonRequestBehavior.AllowGet);
            }
        }
        [HttpGet]
        public virtual JsonResult DeleteCategoryFromMentionFilter(int includedCategoryID)
        {
            string msg = "";
            try
            {
                msg = ProjectMention.DeleteIncludedCategoryFromMentionFilter(includedCategoryID);
                return Json(msg, JsonRequestBehavior.AllowGet);


            }
            catch (Exception ex)
            {
                msg = ex.Message;
                return Json(msg, JsonRequestBehavior.AllowGet);
            }
        }

        /// <summary>
        /// Sub Category Section
        /// </summary>
        /// <param name="mentionFilterID"></param>
        /// <returns></returns>

        [AcceptVerbs(HttpVerbs.Get | HttpVerbs.Post)]
        public ActionResult GetAllIncludedSubCategoeries(int mentionFilterID)
        {
            var listOfIncludedSubCategories = ProjectMention.GetAllIncludedSubCategories(mentionFilterID);
            return PartialView(listOfIncludedSubCategories);
        }
        [HttpGet]
        public virtual JsonResult AddSubCategoryToMentionFilter(int mentionFilterID, int subCategoryID)
        {
            string msg = "";
            try
            {
                msg = ProjectMention.AddIncludedSubCategoryToMentionFilter(mentionFilterID, subCategoryID);
                return Json(msg, JsonRequestBehavior.AllowGet);


            }
            catch (Exception ex)
            {
                msg = ex.Message;
                return Json(msg, JsonRequestBehavior.AllowGet);
            }
        }
      
        [HttpGet]
        public virtual JsonResult DeleteSubCategoryFromMentionFilter(int includedSubCategoryID)
        {
            string msg = "";
            try
            {
                msg = ProjectMention.DeleteIncludedSubCategoryFromMentionFilter(includedSubCategoryID);
                return Json(msg, JsonRequestBehavior.AllowGet);


            }
            catch (Exception ex)
            {
                msg = ex.Message;
                return Json(msg, JsonRequestBehavior.AllowGet);
            }
        }


        /// <summary>
        /// Excluded Category Section
        /// </summary>
        /// <param name="mentionFilterID"></param>
        /// <returns></returns>
        [AcceptVerbs(HttpVerbs.Get | HttpVerbs.Post)]
        public ActionResult GetAllExcludedCategoeries(int mentionFilterID)
        {
            var listOfExcludedCategories = ProjectMention.GetAllExcludedCategories(mentionFilterID);
            return PartialView(listOfExcludedCategories);
        }

        [HttpGet]
        public virtual JsonResult AddExcludedCategoryToMentionFilter(int mentionFilterID, int categoryID)
        {
            string msg = "";
            try
            {
                msg = ProjectMention.AddExcludedCategoryToMentionFilter(mentionFilterID, categoryID);
                return Json(msg, JsonRequestBehavior.AllowGet);


            }
            catch (Exception ex)
            {
                msg = ex.Message;
                return Json(msg, JsonRequestBehavior.AllowGet);
            }
        }
        [HttpGet]
        public virtual JsonResult DeleteExcludedCategoryFromMentionFilter(int excludedCategoryID)
        {
            string msg = "";
            try
            {
                msg = ProjectMention.DeleteExcludedCategoryFromMentionFilter(excludedCategoryID);
                return Json(msg, JsonRequestBehavior.AllowGet);


            }
            catch (Exception ex)
            {
                msg = ex.Message;
                return Json(msg, JsonRequestBehavior.AllowGet);
            }
        }

        /// <summary>
        /// Excluded SubCategory Section
        /// </summary>
        /// <param name="mentionFilterID"></param>
        /// <returns></returns>
        [AcceptVerbs(HttpVerbs.Get | HttpVerbs.Post)]
        public ActionResult GetAllExcludedSubCategoeries(int mentionFilterID)
        {
            var listOfExcludedSubCategories = ProjectMention.GetAllExcludedSubCategories(mentionFilterID);
            return PartialView(listOfExcludedSubCategories);
        }

        [HttpGet]
        public virtual JsonResult AddExcludedSubCategoryToMentionFilter(int mentionFilterID, int subCategoryID)
        {
            string msg = "";
            try
            {
                msg = ProjectMention.AddExcludedSubCategoryToMentionFilter(mentionFilterID, subCategoryID);
                return Json(msg, JsonRequestBehavior.AllowGet);


            }
            catch (Exception ex)
            {
                msg = ex.Message;
                return Json(msg, JsonRequestBehavior.AllowGet);
            }
        }
        [HttpGet]
        public virtual JsonResult DeleteExcludedSubCategoryFromMentionFilter(int excludedSubCategoryID)
        {
            string msg = "";
            try
            {
                msg = ProjectMention.DeleteExcludedSubCategoryFromMentionFilter(excludedSubCategoryID);
                return Json(msg, JsonRequestBehavior.AllowGet);


            }
            catch (Exception ex)
            {
                msg = ex.Message;
                return Json(msg, JsonRequestBehavior.AllowGet);
            }
        }


        /// <summary>
        /// Included Queries Section
        /// </summary>
        /// <param name="mentionFilterID"></param>
        /// <returns></returns>
        [AcceptVerbs(HttpVerbs.Get | HttpVerbs.Post)]
        public ActionResult GetAllIncludedQueries(int mentionFilterID)
        {
            var listOfIncludedQueries = ProjectMention.GetAllIncludedQueries(mentionFilterID);
            return PartialView(listOfIncludedQueries);
        }

        [HttpGet]
        public virtual JsonResult AddIncludedQueryToMentionFilter(int mentionFilterID, int queryID)
        {
            string msg = "";
            try
            {
                msg = ProjectMention.AddIncludedQueryToMentionFilter(mentionFilterID, queryID);
                return Json(msg, JsonRequestBehavior.AllowGet);


            }
            catch (Exception ex)
            {
                msg = ex.Message;
                return Json(msg, JsonRequestBehavior.AllowGet);
            }
        }
        [HttpGet]
        public virtual JsonResult DeleteIncludedQueryFromMentionFilter(int includedQueryID)
        {
            string msg = "";
            try
            {
                msg = ProjectMention.DeleteIncludedQueryFromMentionFilter(includedQueryID);
                return Json(msg, JsonRequestBehavior.AllowGet);


            }
            catch (Exception ex)
            {
                msg = ex.Message;
                return Json(msg, JsonRequestBehavior.AllowGet);
            }
        }


        /// <summary>
        /// Included Tags Section
        /// </summary>
        /// <param name="mentionFilterID"></param>
        /// <returns></returns>
        [AcceptVerbs(HttpVerbs.Get | HttpVerbs.Post)]
        public ActionResult GetAllIncludedTags(int mentionFilterID)
        {
            var listOfIncludedTags = ProjectMention.GetAllIncludedTags(mentionFilterID);
            return PartialView(listOfIncludedTags);
        }

        [HttpGet]
        public virtual JsonResult AddIncludedTagToMentionFilter(int mentionFilterID, int tagID)
        {
            string msg = "";
            try
            {
                msg = ProjectMention.AddIncludedTagToMentionFilter(mentionFilterID, tagID);
                return Json(msg, JsonRequestBehavior.AllowGet);


            }
            catch (Exception ex)
            {
                msg = ex.Message;
                return Json(msg, JsonRequestBehavior.AllowGet);
            }
        }
        [HttpGet]
        public virtual JsonResult DeleteIncludedTagFromMentionFilter(int includedTagID)
        {
            string msg = "";
            try
            {
                msg = ProjectMention.DeleteIncludedTagFromMentionFilter(includedTagID);
                return Json(msg, JsonRequestBehavior.AllowGet);


            }
            catch (Exception ex)
            {
                msg = ex.Message;
                return Json(msg, JsonRequestBehavior.AllowGet);
            }
        }

        /// <summary>
        /// Excluded Tags Section
        /// </summary>
        /// <param name="mentionFilterID"></param>
        /// <returns></returns>

        [AcceptVerbs(HttpVerbs.Get | HttpVerbs.Post)]
        public ActionResult GetAllExcludedTags(int mentionFilterID)
        {
            var listOfExcludedTags = ProjectMention.GetAllExcludedTags(mentionFilterID);
            return PartialView(listOfExcludedTags);
        }

        [HttpGet]
        public virtual JsonResult AddExcludedTagToMentionFilter(int mentionFilterID, int excludedTagID)
        {
            string msg = "";
            try
            {
                msg = ProjectMention.AddExcludedTagToMentionFilter(mentionFilterID, excludedTagID);
                return Json(msg, JsonRequestBehavior.AllowGet);


            }
            catch (Exception ex)
            {
                msg = ex.Message;
                return Json(msg, JsonRequestBehavior.AllowGet);
            }
        }
        [HttpGet]
        public virtual JsonResult DeleteExcludedTagFromMentionFilter(int excludedTagID)
        {
            string msg = "";
            try
            {
                msg = ProjectMention.DeleteExcludedTagFromMentionFilter(excludedTagID);
                return Json(msg, JsonRequestBehavior.AllowGet);


            }
            catch (Exception ex)
            {
                msg = ex.Message;
                return Json(msg, JsonRequestBehavior.AllowGet);
            }
        }

        /// <summary>
        /// Included Content Source Section
        /// </summary>
        /// <param name="mentionFilterID"></param>
        /// <returns></returns>

        [AcceptVerbs(HttpVerbs.Get | HttpVerbs.Post)]
        public ActionResult GetAllIncludedContentSources(int mentionFilterID)
        {
            var listOfIncludedContentSources = ProjectMention.GetAllIncludedContentSource(mentionFilterID);
            return PartialView(listOfIncludedContentSources);
        }

        [HttpGet]
        public virtual JsonResult AddIncludedContentToMentionFilter(int mentionFilterID, int contentSourceID)
        {
            string msg = "";
            try
            {
                msg = ProjectMention.AddIncludedContentSourceToMentionFilter(mentionFilterID, contentSourceID);
                return Json(msg, JsonRequestBehavior.AllowGet);


            }
            catch (Exception ex)
            {
                msg = ex.Message;
                return Json(msg, JsonRequestBehavior.AllowGet);
            }
        }
        [HttpGet]
        public virtual JsonResult DeleteIncludedContentSourceFromMentionFilter(int includedContentSourceID)
        {
            string msg = "";
            try
            {
                msg = ProjectMention.DeleteIncludedContentSourceFromMentionFilter(includedContentSourceID);
                return Json(msg, JsonRequestBehavior.AllowGet);


            }
            catch (Exception ex)
            {
                msg = ex.Message;
                return Json(msg, JsonRequestBehavior.AllowGet);
            }
        }

        /// <summary>
        /// Excluded Content Source Section
        /// </summary>
        /// <param name="mentionFilterID"></param>
        /// <returns></returns>

        [AcceptVerbs(HttpVerbs.Get | HttpVerbs.Post)]
        public ActionResult GetAllExcludedContentSources(int mentionFilterID)
        {
            var listOfExcludedContentSources = ProjectMention.GetAllExcludedContentSource(mentionFilterID);
            return PartialView(listOfExcludedContentSources);
        }

        [HttpGet]
        public virtual JsonResult AddExcludedContentToMentionFilter(int mentionFilterID, int contentSourceID)
        {
            string msg = "";
            try
            {
                msg = ProjectMention.AddExcludedContentSourceToMentionFilter(mentionFilterID, contentSourceID);
                return Json(msg, JsonRequestBehavior.AllowGet);


            }
            catch (Exception ex)
            {
                msg = ex.Message;
                return Json(msg, JsonRequestBehavior.AllowGet);
            }
        }
        [HttpGet]
        public virtual JsonResult DeleteExcludedContentSourceFromMentionFilter(int excludedContentSourceID)
        {
            string msg = "";
            try
            {
                msg = ProjectMention.DeleteExcludedContentSourceFromMentionFilter(excludedContentSourceID);
                return Json(msg, JsonRequestBehavior.AllowGet);


            }
            catch (Exception ex)
            {
                msg = ex.Message;
                return Json(msg, JsonRequestBehavior.AllowGet);
            }
        }

        /// <summary>
        /// Included Mention Type Section
        /// </summary>
        /// <param name="mentionFilterID"></param>
        /// <returns></returns>

        [AcceptVerbs(HttpVerbs.Get | HttpVerbs.Post)]
        public ActionResult GetAllIncludedMentionTypes(int mentionFilterID)
        {
            var listOfIncludedMentionTypes = ProjectMention.GetAllIncludedMentionTypes(mentionFilterID);
            return PartialView(listOfIncludedMentionTypes);
        }

        [HttpGet]
        public virtual JsonResult AddIncludedMentionTypeToMentionFilter(int mentionFilterID, int mentionTypeID)
        {
            string msg = "";
            try
            {
                msg = ProjectMention.AddIncludedMentionTypeToMentionFilter(mentionFilterID, mentionTypeID);
                return Json(msg, JsonRequestBehavior.AllowGet);


            }
            catch (Exception ex)
            {
                msg = ex.Message;
                return Json(msg, JsonRequestBehavior.AllowGet);
            }
        }
        [HttpGet]
        public virtual JsonResult DeleteIncludedMentionTypeFromMentionFilter(int includedMentionTypeID)
        {
            string msg = "";
            try
            {
                msg = ProjectMention.DeleteIncludedMentionTypeFromMentionFilter(includedMentionTypeID);
                return Json(msg, JsonRequestBehavior.AllowGet);


            }
            catch (Exception ex)
            {
                msg = ex.Message;
                return Json(msg, JsonRequestBehavior.AllowGet);
            }
        }

        /// <summary>
        /// Excluded Mention Type Section
        /// </summary>
        /// <param name="mentionFilterID"></param>
        /// <returns></returns>

        [AcceptVerbs(HttpVerbs.Get | HttpVerbs.Post)]
        public ActionResult GetAllExcludedMentionTypes(int mentionFilterID)
        {
            var listOfExcludedMentionTypes = ProjectMention.GetAllExcludedMentionTypes(mentionFilterID);
            return PartialView(listOfExcludedMentionTypes);
        }

        [HttpGet]
        public virtual JsonResult AddExcludedMentionTypeToMentionFilter(int mentionFilterID, int mentionTypeID)
        {
            string msg = "";
            try
            {
                msg = ProjectMention.AddExcludedMentionTypeToMentionFilter(mentionFilterID, mentionTypeID);
                return Json(msg, JsonRequestBehavior.AllowGet);


            }
            catch (Exception ex)
            {
                msg = ex.Message;
                return Json(msg, JsonRequestBehavior.AllowGet);
            }
        }
        [HttpGet]
        public virtual JsonResult DeleteExcludedMentionTypeFromMentionFilter(int excludedMentionTypeID)
        {
            string msg = "";
            try
            {
                msg = ProjectMention.DeleteExcludedMentionTypeFromMentionFilter(excludedMentionTypeID);
                return Json(msg, JsonRequestBehavior.AllowGet);


            }
            catch (Exception ex)
            {
                msg = ex.Message;
                return Json(msg, JsonRequestBehavior.AllowGet);
            }
        }

        /// <summary>
        /// Included React Score OverAll Section
        /// </summary>
        /// <param name="mentionFilterID"></param>
        /// <returns></returns>

        [AcceptVerbs(HttpVerbs.Get | HttpVerbs.Post)]
        public ActionResult GetAllIncludedReactScoreOverAll(int mentionFilterID)
        {
            var listOfIncludedReactScoreOverAlls = ProjectMention.GetAllIncludedReactScoreOverAll(mentionFilterID);
            return PartialView(listOfIncludedReactScoreOverAlls);
        }

        [HttpGet]
        public virtual JsonResult AddIncludedReactScoreOverAllToMentionFilter(int mentionFilterID, int reactScoreOverAllID)
        {
            string msg = "";
            try
            {
                msg = ProjectMention.AddIncludedReactScoreOverAllToMentionFilter(mentionFilterID, reactScoreOverAllID);
                return Json(msg, JsonRequestBehavior.AllowGet);


            }
            catch (Exception ex)
            {
                msg = ex.Message;
                return Json(msg, JsonRequestBehavior.AllowGet);
            }
        }
        [HttpGet]
        public virtual JsonResult DeleteIncludedReactScoreOverAllFromMentionFilter(int includedReactScoreOverAllID)
        {
            string msg = "";
            try
            {
                msg = ProjectMention.DeleteIncludedReactScoreOverAllFromMentionFilter(includedReactScoreOverAllID);
                return Json(msg, JsonRequestBehavior.AllowGet);


            }
            catch (Exception ex)
            {
                msg = ex.Message;
                return Json(msg, JsonRequestBehavior.AllowGet);
            }
        }

        /// <summary>
        /// Excluded React Score OverAll Section
        /// </summary>
        /// <param name="mentionFilterID"></param>
        /// <returns></returns>

        [AcceptVerbs(HttpVerbs.Get | HttpVerbs.Post)]
        public ActionResult GetAllExcludedReactScoreOverAll(int mentionFilterID)
        {
            var listOfExcludedReactScoreOverAlls = ProjectMention.GetAllExcludedReactScoreOverAll(mentionFilterID);
            return PartialView(listOfExcludedReactScoreOverAlls);
        }

        [HttpGet]
        public virtual JsonResult AddExcludedReactScoreOverAllToMentionFilter(int mentionFilterID, int reactScoreOverAllID)
        {
            string msg = "";
            try
            {
                msg = ProjectMention.AddExcludedReactScoreOverAllToMentionFilter(mentionFilterID, reactScoreOverAllID);
                return Json(msg, JsonRequestBehavior.AllowGet);


            }
            catch (Exception ex)
            {
                msg = ex.Message;
                return Json(msg, JsonRequestBehavior.AllowGet);
            }
        }
        [HttpGet]
        public virtual JsonResult DeleteExcludedReactScoreOverAllFromMentionFilter(int excludedReactScoreOverAllID)
        {
            string msg = "";
            try
            {
                msg = ProjectMention.DeleteExcludedReactScoreOverAllFromMentionFilter(excludedReactScoreOverAllID);
                return Json(msg, JsonRequestBehavior.AllowGet);


            }
            catch (Exception ex)
            {
                msg = ex.Message;
                return Json(msg, JsonRequestBehavior.AllowGet);
            }
        }

        /// <summary>
        /// Included React Score Harm Section
        /// </summary>
        /// <param name="mentionFilterID"></param>
        /// <returns></returns>

        [AcceptVerbs(HttpVerbs.Get | HttpVerbs.Post)]
        public ActionResult GetAllIncludedReactScoreHarm(int mentionFilterID)
        {
            var listOfIncludedReactScoreHarms = ProjectMention.GetAllIncludedReactScoreHarm(mentionFilterID);
            return PartialView(listOfIncludedReactScoreHarms);
        }

        [HttpGet]
        public virtual JsonResult AddIncludedReactScoreHarmToMentionFilter(int mentionFilterID, int reactScoreHarmID)
        {
            string msg = "";
            try
            {
                msg = ProjectMention.AddIncludedReactScoreHarmToMentionFilter(mentionFilterID, reactScoreHarmID);
                return Json(msg, JsonRequestBehavior.AllowGet);


            }
            catch (Exception ex)
            {
                msg = ex.Message;
                return Json(msg, JsonRequestBehavior.AllowGet);
            }
        }
        [HttpGet]
        public virtual JsonResult DeleteIncludedReactScoreHarmFromMentionFilter(int includedReactScoreHarmID)
        {
            string msg = "";
            try
            {
                msg = ProjectMention.DeleteIncludedReactScoreHarmFromMentionFilter(includedReactScoreHarmID);
                return Json(msg, JsonRequestBehavior.AllowGet);


            }
            catch (Exception ex)
            {
                msg = ex.Message;
                return Json(msg, JsonRequestBehavior.AllowGet);
            }
        }


        /// <summary>
        /// Excluded React Score Harm Section
        /// </summary>
        /// <param name="mentionFilterID"></param>
        /// <returns></returns>

        [AcceptVerbs(HttpVerbs.Get | HttpVerbs.Post)]
        public ActionResult GetAllExcludedReactScoreHarm(int mentionFilterID)
        {
            var listOfExcludedReactScoreHarms = ProjectMention.GetAllExcludedReactScoreHarm(mentionFilterID);
            return PartialView(listOfExcludedReactScoreHarms);
        }

        [HttpGet]
        public virtual JsonResult AddExcludedReactScoreHarmToMentionFilter(int mentionFilterID, int reactScoreHarmID)
        {
            string msg = "";
            try
            {
                msg = ProjectMention.AddExcludedReactScoreHarmToMentionFilter(mentionFilterID, reactScoreHarmID);
                return Json(msg, JsonRequestBehavior.AllowGet);


            }
            catch (Exception ex)
            {
                msg = ex.Message;
                return Json(msg, JsonRequestBehavior.AllowGet);
            }
        }
        [HttpGet]
        public virtual JsonResult DeleteExcludedReactScoreHarmFromMentionFilter(int excludedReactScoreHarmID)
        {
            string msg = "";
            try
            {
                msg = ProjectMention.DeleteExcludedReactScoreHarmFromMentionFilter(excludedReactScoreHarmID);
                return Json(msg, JsonRequestBehavior.AllowGet);


            }
            catch (Exception ex)
            {
                msg = ex.Message;
                return Json(msg, JsonRequestBehavior.AllowGet);
            }
        }


        /// <summary>
        /// Included React Score Emotionality Section
        /// </summary>
        /// <param name="mentionFilterID"></param>
        /// <returns></returns>

        [AcceptVerbs(HttpVerbs.Get | HttpVerbs.Post)]
        public ActionResult GetAllIncludedReactScoreEmotionality(int mentionFilterID)
        {
            var listOfIncludedReactScoreEmotionality = ProjectMention.GetAllIncludedReactScoreEmotionality(mentionFilterID);
            return PartialView(listOfIncludedReactScoreEmotionality);
        }

        [HttpGet]
        public virtual JsonResult AddIncludedReactScoreEmotionalityToMentionFilter(int mentionFilterID, int reactScoreEmotionalityID)
        {
            string msg = "";
            try
            {
                msg = ProjectMention.AddIncludedReactScoreEmotionalityToMentionFilter(mentionFilterID, reactScoreEmotionalityID);
                return Json(msg, JsonRequestBehavior.AllowGet);


            }
            catch (Exception ex)
            {
                msg = ex.Message;
                return Json(msg, JsonRequestBehavior.AllowGet);
            }
        }
        [HttpGet]
        public virtual JsonResult DeleteIncludedReactScoreEmotionalityFromMentionFilter(int includedReactScoreEmotionalityID)
        {
            string msg = "";
            try
            {
                msg = ProjectMention.DeleteIncludedReactScoreEmotionalityFromMentionFilter(includedReactScoreEmotionalityID);
                return Json(msg, JsonRequestBehavior.AllowGet);


            }
            catch (Exception ex)
            {
                msg = ex.Message;
                return Json(msg, JsonRequestBehavior.AllowGet);
            }
        }


        /// <summary>
        /// Excluded React Score Emotionality Section
        /// </summary>
        /// <param name="mentionFilterID"></param>
        /// <returns></returns>

        [AcceptVerbs(HttpVerbs.Get | HttpVerbs.Post)]
        public ActionResult GetAllExcludedReactScoreEmotionality(int mentionFilterID)
        {
            var listOfExcludedReactScoreEmotionality = ProjectMention.GetAllExcludedReactScoreEmotionality(mentionFilterID);
            return PartialView(listOfExcludedReactScoreEmotionality);
        }

        [HttpGet]
        public virtual JsonResult AddExcludedReactScoreEmotionalityToMentionFilter(int mentionFilterID, int reactScoreEmotionalityID)
        {
            string msg = "";
            try
            {
                msg = ProjectMention.AddExcludedReactScoreEmotionalityToMentionFilter(mentionFilterID, reactScoreEmotionalityID);
                return Json(msg, JsonRequestBehavior.AllowGet);


            }
            catch (Exception ex)
            {
                msg = ex.Message;
                return Json(msg, JsonRequestBehavior.AllowGet);
            }
        }
        [HttpGet]
        public virtual JsonResult DeleteExcludedReactScoreEmotionalityFromMentionFilter(int excludedReactScoreEmotionalityID)
        {
            string msg = "";
            try
            {
                msg = ProjectMention.DeleteExcludedReactScoreEmotionalityFromMentionFilter(excludedReactScoreEmotionalityID);
                return Json(msg, JsonRequestBehavior.AllowGet);


            }
            catch (Exception ex)
            {
                msg = ex.Message;
                return Json(msg, JsonRequestBehavior.AllowGet);
            }
        }


        /// <summary>
        /// Included Sentiment Section
        /// </summary>
        /// <param name="mentionFilterID"></param>
        /// <returns></returns>

        [AcceptVerbs(HttpVerbs.Get | HttpVerbs.Post)]
        public ActionResult GetAllIncludedSentiments(int mentionFilterID)
        {
            var listOfIncludedSentiments = ProjectMention.GetAllIncludedSentiments(mentionFilterID);
            return PartialView(listOfIncludedSentiments);
        }

        [HttpGet]
        public virtual JsonResult AddIncludedSentimentToMentionFilter(int mentionFilterID, int sentimentID)
        {
            string msg = "";
            try
            {
                msg = ProjectMention.AddIncludedSentimentToMentionFilter(mentionFilterID, sentimentID);
                return Json(msg, JsonRequestBehavior.AllowGet);


            }
            catch (Exception ex)
            {
                msg = ex.Message;
                return Json(msg, JsonRequestBehavior.AllowGet);
            }
        }
        [HttpGet]
        public virtual JsonResult DeleteIncludedSentimentFromMentionFilter(int includedSentimentID)
        {
            string msg = "";
            try
            {
                msg = ProjectMention.DeleteIncludedSentimentFromMentionFilter(includedSentimentID);
                return Json(msg, JsonRequestBehavior.AllowGet);


            }
            catch (Exception ex)
            {
                msg = ex.Message;
                return Json(msg, JsonRequestBehavior.AllowGet);
            }
        }


        /// <summary>
        /// Included Emotion Section
        /// </summary>
        /// <param name="mentionFilterID"></param>
        /// <returns></returns>

        [AcceptVerbs(HttpVerbs.Get | HttpVerbs.Post)]
        public ActionResult GetAllIncludedEmotions(int mentionFilterID)
        {
            var listOfIncludedEmotions = ProjectMention.GetAllIncludedEmotions(mentionFilterID);
            return PartialView(listOfIncludedEmotions);
        }

        [HttpGet]
        public virtual JsonResult AddIncludedEmotionToMentionFilter(int mentionFilterID, int emotionID)
        {
            string msg = "";
            try
            {
                msg = ProjectMention.AddIncludedEmotionToMentionFilter(mentionFilterID, emotionID);
                return Json(msg, JsonRequestBehavior.AllowGet);


            }
            catch (Exception ex)
            {
                msg = ex.Message;
                return Json(msg, JsonRequestBehavior.AllowGet);
            }
        }
        [HttpGet]
        public virtual JsonResult DeleteIncludedEmotionFromMentionFilter(int includedEmotionID)
        {
            string msg = "";
            try
            {
                msg = ProjectMention.DeleteIncludedEmotionFromMentionFilter(includedEmotionID);
                return Json(msg, JsonRequestBehavior.AllowGet);


            }
            catch (Exception ex)
            {
                msg = ex.Message;
                return Json(msg, JsonRequestBehavior.AllowGet);
            }
        }




        /// <summary>
        /// Excluded Emotions Section
        /// </summary>
        /// <param name="mentionFilterID"></param>
        /// <returns></returns>

        [AcceptVerbs(HttpVerbs.Get | HttpVerbs.Post)]
        public ActionResult GetAllExcludedEmotions(int mentionFilterID)
        {
            var listOfIncludedEmotions = ProjectMention.GetAllExcludedEmotions(mentionFilterID);
            return PartialView(listOfIncludedEmotions);
        }

        [HttpGet]
        public virtual JsonResult AddExcludedEmotionToMentionFilter(int mentionFilterID, int emotionID)
        {
            string msg = "";
            try
            {
                msg = ProjectMention.AddExcludedEmotionToMentionFilter(mentionFilterID, emotionID);
                return Json(msg, JsonRequestBehavior.AllowGet);


            }
            catch (Exception ex)
            {
                msg = ex.Message;
                return Json(msg, JsonRequestBehavior.AllowGet);
            }
        }
        [HttpGet]
        public virtual JsonResult DeleteExcludedEmotionFromMentionFilter(int excludedEmotionID)
        {
            string msg = "";
            try
            {
                msg = ProjectMention.DeleteExcludedEmotionFromMentionFilter(excludedEmotionID);
                return Json(msg, JsonRequestBehavior.AllowGet);


            }
            catch (Exception ex)
            {
                msg = ex.Message;
                return Json(msg, JsonRequestBehavior.AllowGet);
            }
        }


        /// <summary>
        /// Included Social Panal Section
        /// </summary>
        /// <param name="mentionFilterID"></param>
        /// <returns></returns>

        [AcceptVerbs(HttpVerbs.Get | HttpVerbs.Post)]
        public ActionResult GetAllIncludedSocialPanal(int mentionFilterID)
        {
            var listOfIncludedSocialPanals = ProjectMention.GetAllIncludedSocialPanal(mentionFilterID);
            return PartialView(listOfIncludedSocialPanals);
        }

        [HttpGet]
        public virtual JsonResult AddIncludedSocialPanalToMentionFilter(int mentionFilterID, int includeSocialPanalID)
        {
            string msg = "";
            try
            {
                msg = ProjectMention.AddIncludedSocialPanalToMentionFilter(mentionFilterID, includeSocialPanalID);
                return Json(msg, JsonRequestBehavior.AllowGet);


            }
            catch (Exception ex)
            {
                msg = ex.Message;
                return Json(msg, JsonRequestBehavior.AllowGet);
            }
        }
        [HttpGet]
        public virtual JsonResult DeleteIncludedSocialPanalFromMentionFilter(int includedSocialPanalID)
        {
            string msg = "";
            try
            {
                msg = ProjectMention.DeleteIncludedSocialPanalFromMentionFilter(includedSocialPanalID);
                return Json(msg, JsonRequestBehavior.AllowGet);


            }
            catch (Exception ex)
            {
                msg = ex.Message;
                return Json(msg, JsonRequestBehavior.AllowGet);
            }
        }



        /// <summary>
        /// Excluded Social Panal Section
        /// </summary>
        /// <param name="mentionFilterID"></param>
        /// <returns></returns>

        [AcceptVerbs(HttpVerbs.Get | HttpVerbs.Post)]
        public ActionResult GetAllExcludedSocialPanal(int mentionFilterID)
        {
            var listOfExcludedSocialPanals = ProjectMention.GetAllExcludedSocialPanal(mentionFilterID);
            return PartialView(listOfExcludedSocialPanals);
        }

        [HttpGet]
        public virtual JsonResult AddExcludedSocialPanalToMentionFilter(int mentionFilterID, int excludeSocialPanalID)
        {
            string msg = "";
            try
            {
                msg = ProjectMention.AddExcludedSocialPanalToMentionFilter(mentionFilterID, excludeSocialPanalID);
                return Json(msg, JsonRequestBehavior.AllowGet);


            }
            catch (Exception ex)
            {
                msg = ex.Message;
                return Json(msg, JsonRequestBehavior.AllowGet);
            }
        }
        [HttpGet]
        public virtual JsonResult DeleteExcludedSocialPanalFromMentionFilter(int excludedSocialPanalID)
        {
            string msg = "";
            try
            {
                msg = ProjectMention.DeleteExcludedSocialPanalFromMentionFilter(excludedSocialPanalID);
                return Json(msg, JsonRequestBehavior.AllowGet);


            }
            catch (Exception ex)
            {
                msg = ex.Message;
                return Json(msg, JsonRequestBehavior.AllowGet);
            }
        }


        /// <summary>
        /// Included Author Group Section
        /// </summary>
        /// <param name="mentionFilterID"></param>
        /// <returns></returns>

        [AcceptVerbs(HttpVerbs.Get | HttpVerbs.Post)]
        public ActionResult GetAllIncludedAuthorGroup(int mentionFilterID)
        {
            var listOfIncludedAuthorGroups = ProjectMention.GetAllIncludedAuthorGroup(mentionFilterID);
            return PartialView(listOfIncludedAuthorGroups);
        }

        [HttpGet]
        public virtual JsonResult AddIncludedAuthorGroupToMentionFilter(int mentionFilterID, int includeAuthorGroupID)
        {
            string msg = "";
            try
            {
                msg = ProjectMention.AddIncludedAuthorGroupToMentionFilter(mentionFilterID, includeAuthorGroupID);
                return Json(msg, JsonRequestBehavior.AllowGet);


            }
            catch (Exception ex)
            {
                msg = ex.Message;
                return Json(msg, JsonRequestBehavior.AllowGet);
            }
        }
        [HttpGet]
        public virtual JsonResult DeleteIncludedAuthorGroupFromMentionFilter(int includeAuthorGroupID)
        {
            string msg = "";
            try
            {
                msg = ProjectMention.DeleteIncludedAuthorGroupFromMentionFilter(includeAuthorGroupID);
                return Json(msg, JsonRequestBehavior.AllowGet);


            }
            catch (Exception ex)
            {
                msg = ex.Message;
                return Json(msg, JsonRequestBehavior.AllowGet);
            }
        }


        /// <summary>
        /// Excluded Author Group Section
        /// </summary>
        /// <param name="mentionFilterID"></param>
        /// <returns></returns>

        [AcceptVerbs(HttpVerbs.Get | HttpVerbs.Post)]
        public ActionResult GetAllExcludedAuthorGroup(int mentionFilterID)
        {
            var listOfIncludedAuthorGroups = ProjectMention.GetAllExcludedAuthorGroup(mentionFilterID);
            return PartialView(listOfIncludedAuthorGroups);
        }

        [HttpGet]
        public virtual JsonResult AddExcludedAuthorGroupToMentionFilter(int mentionFilterID, int excludeAuthorGroupID)
        {
            string msg = "";
            try
            {
                msg = ProjectMention.AddExcludedAuthorGroupToMentionFilter(mentionFilterID, excludeAuthorGroupID);
                return Json(msg, JsonRequestBehavior.AllowGet);


            }
            catch (Exception ex)
            {
                msg = ex.Message;
                return Json(msg, JsonRequestBehavior.AllowGet);
            }
        }
        [HttpGet]
        public virtual JsonResult DeleteExcludedAuthorGroupFromMentionFilter(int excludeAuthorGroupID)
        {
            string msg = "";
            try
            {
                msg = ProjectMention.DeleteExcludedAuthorGroupFromMentionFilter(excludeAuthorGroupID);
                return Json(msg, JsonRequestBehavior.AllowGet);


            }
            catch (Exception ex)
            {
                msg = ex.Message;
                return Json(msg, JsonRequestBehavior.AllowGet);
            }
        }


        /// <summary>
        /// Included Location Section
        /// </summary>
        /// <param name="mentionFilterID"></param>
        /// <returns></returns>

        [AcceptVerbs(HttpVerbs.Get | HttpVerbs.Post)]
        public ActionResult GetAllIncludedLocation(int mentionFilterID)
        {
            var listOfIncludedLocations = ProjectMention.GetAllIncludedLocaition(mentionFilterID);
            return PartialView(listOfIncludedLocations);
        }

        [HttpGet]
        public virtual JsonResult AddIncludedLocationToMentionFilter(int mentionFilterID, int includeLocationID)
        {
            string msg = "";
            try
            {
                msg = ProjectMention.AddIncludedLocationToMentionFilter(mentionFilterID, includeLocationID);
                return Json(msg, JsonRequestBehavior.AllowGet);


            }
            catch (Exception ex)
            {
                msg = ex.Message;
                return Json(msg, JsonRequestBehavior.AllowGet);
            }
        }
        [HttpGet]
        public virtual JsonResult DeleteIncludedLocationFromMentionFilter(int includeLocaitionID)
        {
            string msg = "";
            try
            {
                msg = ProjectMention.DeleteIncludedLocationFromMentionFilter(includeLocaitionID);
                return Json(msg, JsonRequestBehavior.AllowGet);


            }
            catch (Exception ex)
            {
                msg = ex.Message;
                return Json(msg, JsonRequestBehavior.AllowGet);
            }
        }


        /// <summary>
        /// Excluded Location Section
        /// </summary>
        /// <param name="mentionFilterID"></param>
        /// <returns></returns>

        [AcceptVerbs(HttpVerbs.Get | HttpVerbs.Post)]
        public ActionResult GetAllExcludedLocation(int mentionFilterID)
        {
            var listOfIncludedLocations = ProjectMention.GetAllExcludedLocaition(mentionFilterID);
            return PartialView(listOfIncludedLocations);
        }

        [HttpGet]
        public virtual JsonResult AddExcludedLocationToMentionFilter(int mentionFilterID, int excludeLocationID)
        {
            string msg = "";
            try
            {
                msg = ProjectMention.AddExcludedLocationToMentionFilter(mentionFilterID, excludeLocationID);
                return Json(msg, JsonRequestBehavior.AllowGet);


            }
            catch (Exception ex)
            {
                msg = ex.Message;
                return Json(msg, JsonRequestBehavior.AllowGet);
            }
        }
        [HttpGet]
        public virtual JsonResult DeleteExcludedLocationFromMentionFilter(int excludeLocaitionID)
        {
            string msg = "";
            try
            {
                msg = ProjectMention.DeleteExcludedLocationFromMentionFilter(excludeLocaitionID);
                return Json(msg, JsonRequestBehavior.AllowGet);


            }
            catch (Exception ex)
            {
                msg = ex.Message;
                return Json(msg, JsonRequestBehavior.AllowGet);
            }
        }


        /// <summary>
        /// Included Language Section
        /// </summary>
        /// <param name="mentionFilterID"></param>
        /// <returns></returns>

        [AcceptVerbs(HttpVerbs.Get | HttpVerbs.Post)]
        public ActionResult GetAllIncludedLanguage(int mentionFilterID)
        {
            var listOfIncludedLanguages = ProjectMention.GetAllIncludedLanguage(mentionFilterID);
            return PartialView(listOfIncludedLanguages);
        }

        [HttpGet]
        public virtual JsonResult AddIncludedLanguageToMentionFilter(int mentionFilterID, int languageID)
        {
            string msg = "";
            try
            {
                msg = ProjectMention.AddIncludedLanguageToMentionFilter(mentionFilterID, languageID);
                return Json(msg, JsonRequestBehavior.AllowGet);


            }
            catch (Exception ex)
            {
                msg = ex.Message;
                return Json(msg, JsonRequestBehavior.AllowGet);
            }
        }
        [HttpGet]
        public virtual JsonResult DeleteIncludedLanguageFromMentionFilter(int includeLanguageID)
        {
            string msg = "";
            try
            {
                msg = ProjectMention.DeleteIncludedLanguagFromMentionFilter(includeLanguageID);
                return Json(msg, JsonRequestBehavior.AllowGet);


            }
            catch (Exception ex)
            {
                msg = ex.Message;
                return Json(msg, JsonRequestBehavior.AllowGet);
            }
        }

        /// <summary>
        /// Excluded Language Section
        /// </summary>
        /// <param name="mentionFilterID"></param>
        /// <returns></returns>

        [AcceptVerbs(HttpVerbs.Get | HttpVerbs.Post)]
        public ActionResult GetAllExcludedLanguage(int mentionFilterID)
        {
            var listOfExcludedLanguages = ProjectMention.GetAllExcludedLanguage(mentionFilterID);
            return PartialView(listOfExcludedLanguages);
        }

        [HttpGet]
        public virtual JsonResult AddExcludedLanguageToMentionFilter(int mentionFilterID, int languageID)
        {
            string msg = "";
            try
            {
                msg = ProjectMention.AddExcludedLanguageToMentionFilter(mentionFilterID, languageID);
                return Json(msg, JsonRequestBehavior.AllowGet);


            }
            catch (Exception ex)
            {
                msg = ex.Message;
                return Json(msg, JsonRequestBehavior.AllowGet);
            }
        }
        [HttpGet]
        public virtual JsonResult DeleteExcludedLanguageFromMentionFilter(int excludeLanguageID)
        {
            string msg = "";
            try
            {
                msg = ProjectMention.DeleteExcludedLanguagFromMentionFilter(excludeLanguageID);
                return Json(msg, JsonRequestBehavior.AllowGet);


            }
            catch (Exception ex)
            {
                msg = ex.Message;
                return Json(msg, JsonRequestBehavior.AllowGet);
            }
        }




        /// <summary>
        /// Included Site List Section
        /// </summary>
        /// <param name="mentionFilterID"></param>
        /// <returns></returns>

        [AcceptVerbs(HttpVerbs.Get | HttpVerbs.Post)]
        public ActionResult GetAllIncludedSiteLists(int mentionFilterID)
        {
            var listOfIncludedSiteLists = ProjectMention.GetAllIncludedSiteList(mentionFilterID);
            return PartialView(listOfIncludedSiteLists);
        }

        [HttpGet]
        public virtual JsonResult AddIncludedSiteListToMentionFilter(int mentionFilterID, int siteListID)
        {
            string msg = "";
            try
            {
                msg = ProjectMention.AddIncludedSiteListToMentionFilter(mentionFilterID, siteListID);
                return Json(msg, JsonRequestBehavior.AllowGet);


            }
            catch (Exception ex)
            {
                msg = ex.Message;
                return Json(msg, JsonRequestBehavior.AllowGet);
            }
        }
        [HttpGet]
        public virtual JsonResult DeleteIncludedSiteListFromMentionFilter(int includesiteListID)
        {
            string msg = "";
            try
            {
                msg = ProjectMention.DeleteIncludedSiteListFromMentionFilter(includesiteListID);
                return Json(msg, JsonRequestBehavior.AllowGet);


            }
            catch (Exception ex)
            {
                msg = ex.Message;
                return Json(msg, JsonRequestBehavior.AllowGet);
            }
        }

        /// <summary>
        /// Excluded Site List Section
        /// </summary>
        /// <param name="mentionFilterID"></param>
        /// <returns></returns>

        [AcceptVerbs(HttpVerbs.Get | HttpVerbs.Post)]
        public ActionResult GetAllExcludedSiteLists(int mentionFilterID)
        {
            var listOfExcludedSiteLists = ProjectMention.GetAllExcludedSiteList(mentionFilterID);
            return PartialView(listOfExcludedSiteLists);
        }

        [HttpGet]
        public virtual JsonResult AddExcludedSiteListToMentionFilter(int mentionFilterID, int siteListID)
        {
            string msg = "";
            try
            {
                msg = ProjectMention.AddExcludedSiteListToMentionFilter(mentionFilterID, siteListID);
                return Json(msg, JsonRequestBehavior.AllowGet);


            }
            catch (Exception ex)
            {
                msg = ex.Message;
                return Json(msg, JsonRequestBehavior.AllowGet);
            }
        }
        [HttpGet]
        public virtual JsonResult DeleteExcludedSiteListFromMentionFilter(int excludesiteListID)
        {
            string msg = "";
            try
            {
                msg = ProjectMention.DeleteExcludedSiteListFromMentionFilter(excludesiteListID);
                return Json(msg, JsonRequestBehavior.AllowGet);


            }
            catch (Exception ex)
            {
                msg = ex.Message;
                return Json(msg, JsonRequestBehavior.AllowGet);
            }
        }




        /// <summary>
        /// Included Twitter Mention Type Section
        /// </summary>
        /// <param name="mentionFilterID"></param>
        /// <returns></returns>

        [AcceptVerbs(HttpVerbs.Get | HttpVerbs.Post)]
        public ActionResult GetAllIncludedTwitterMentionTypes(int mentionFilterID)
        {
            var listOfIncludedTwitterMentionTypes = ProjectMention.GetAllIncludedTwitterMentionTypes(mentionFilterID);
            return PartialView(listOfIncludedTwitterMentionTypes);
        }

        [HttpGet]
        public virtual JsonResult AddIncludedTwitterMentionTypeToMentionFilter(int mentionFilterID, int twitterMentionTypeID)
        {
            string msg = "";
            try
            {
                msg = ProjectMention.AddIncludedTwitterMentionTypeToMentionFilter(mentionFilterID, twitterMentionTypeID);
                return Json(msg, JsonRequestBehavior.AllowGet);


            }
            catch (Exception ex)
            {
                msg = ex.Message;
                return Json(msg, JsonRequestBehavior.AllowGet);
            }
        }
        [HttpGet]
        public virtual JsonResult DeleteIncludedTwitterMentionTypeFromMentionFilter(int includeTwitterMentionTypeID)
        {
            string msg = "";
            try
            {
                msg = ProjectMention.DeleteIncludedTwitterMentionTypeFromMentionFilter(includeTwitterMentionTypeID);
                return Json(msg, JsonRequestBehavior.AllowGet);


            }
            catch (Exception ex)
            {
                msg = ex.Message;
                return Json(msg, JsonRequestBehavior.AllowGet);
            }
        }


        /// <summary>
        /// Excluded Twitter Mention Type Section
        /// </summary>
        /// <param name="mentionFilterID"></param>
        /// <returns></returns>

        [AcceptVerbs(HttpVerbs.Get | HttpVerbs.Post)]
        public ActionResult GetAllExcludedTwitterMentionTypes(int mentionFilterID)
        {
            var listOfIncludedTwitterMentionTypes = ProjectMention.GetAllExcludedTwitterMentionTypes(mentionFilterID);
            return PartialView(listOfIncludedTwitterMentionTypes);
        }

        [HttpGet]
        public virtual JsonResult AddExcludedTwitterMentionTypeToMentionFilter(int mentionFilterID, int twitterMentionTypeID)
        {
            string msg = "";
            try
            {
                msg = ProjectMention.AddExcludedTwitterMentionTypeToMentionFilter(mentionFilterID, twitterMentionTypeID);
                return Json(msg, JsonRequestBehavior.AllowGet);


            }
            catch (Exception ex)
            {
                msg = ex.Message;
                return Json(msg, JsonRequestBehavior.AllowGet);
            }
        }
        [HttpGet]
        public virtual JsonResult DeleteExcludedTwitterMentionTypeFromMentionFilter(int excludeTwitterMentionTypeID)
        {
            string msg = "";
            try
            {
                msg = ProjectMention.DeleteExcludedTwitterMentionTypeFromMentionFilter(excludeTwitterMentionTypeID);
                return Json(msg, JsonRequestBehavior.AllowGet);


            }
            catch (Exception ex)
            {
                msg = ex.Message;
                return Json(msg, JsonRequestBehavior.AllowGet);
            }
        }


        /// <summary>
        /// Included Twitter Verification Type Section
        /// </summary>
        /// <param name="mentionFilterID"></param>
        /// <returns></returns>

        [AcceptVerbs(HttpVerbs.Get | HttpVerbs.Post)]
        public ActionResult GetAllIncludedTwitterVerificationTypes(int mentionFilterID)
        {
            var listOfIncludedTwitterVerificationTypes = ProjectMention.GetAllIncludedTwitterVerificationTypes(mentionFilterID);
            return PartialView(listOfIncludedTwitterVerificationTypes);
        }

        [HttpGet]
        public virtual JsonResult AddIncludedTwitterVerificationTypeToMentionFilter(int mentionFilterID, int twitterVerificationTypeID)
        {
            string msg = "";
            try
            {
                msg = ProjectMention.AddIncludedTwitterVerificationTypeToMentionFilter(mentionFilterID, twitterVerificationTypeID);
                return Json(msg, JsonRequestBehavior.AllowGet);


            }
            catch (Exception ex)
            {
                msg = ex.Message;
                return Json(msg, JsonRequestBehavior.AllowGet);
            }
        }
        [HttpGet]
        public virtual JsonResult DeleteIncludedTwitterVerificationTypeFromMentionFilter(int includeTwitterVerificationTypeID)
        {
            string msg = "";
            try
            {
                msg = ProjectMention.DeleteIncludedTwitterVerificationTypeFromMentionFilter(includeTwitterVerificationTypeID);
                return Json(msg, JsonRequestBehavior.AllowGet);


            }
            catch (Exception ex)
            {
                msg = ex.Message;
                return Json(msg, JsonRequestBehavior.AllowGet);
            }
        }

        /// <summary>
        /// Excluded Twitter Verification Type Section
        /// </summary>
        /// <param name="mentionFilterID"></param>
        /// <returns></returns>

        [AcceptVerbs(HttpVerbs.Get | HttpVerbs.Post)]
        public ActionResult GetAllExcludedTwitterVerificationTypes(int mentionFilterID)
        {
            var listOfExcludedTwitterVerificationTypes = ProjectMention.GetAllExcludedTwitterVerificationTypes(mentionFilterID);
            return PartialView(listOfExcludedTwitterVerificationTypes);
        }

        [HttpGet]
        public virtual JsonResult AddExcludedTwitterVerificationTypeToMentionFilter(int mentionFilterID, int twitterVerificationTypeID)
        {
            string msg = "";
            try
            {
                msg = ProjectMention.AddExcludedTwitterVerificationTypeToMentionFilter(mentionFilterID, twitterVerificationTypeID);
                return Json(msg, JsonRequestBehavior.AllowGet);


            }
            catch (Exception ex)
            {
                msg = ex.Message;
                return Json(msg, JsonRequestBehavior.AllowGet);
            }
        }
        [HttpGet]
        public virtual JsonResult DeleteExcludedTwitterVerificationTypeFromMentionFilter(int excludeTwitterVerificationTypeID)
        {
            string msg = "";
            try
            {
                msg = ProjectMention.DeleteExcludedTwitterVerificationTypeFromMentionFilter(excludeTwitterVerificationTypeID);
                return Json(msg, JsonRequestBehavior.AllowGet);


            }
            catch (Exception ex)
            {
                msg = ex.Message;
                return Json(msg, JsonRequestBehavior.AllowGet);
            }
        }


        /// <summary>
        /// Included FaceBook Mention Type Section
        /// </summary>
        /// <param name="mentionFilterID"></param>
        /// <returns></returns>

        [AcceptVerbs(HttpVerbs.Get | HttpVerbs.Post)]
        public ActionResult GetAllIncludedFaceBookMentionTypes(int mentionFilterID)
        {
            var listOfIncludedFaceBookMentionTypes = ProjectMention.GetAllIncludedFaceBookMentionTypes(mentionFilterID);
            return PartialView(listOfIncludedFaceBookMentionTypes);
        }

        [HttpGet]
        public virtual JsonResult AddIncludedFaceBookMentionTypeToMentionFilter(int mentionFilterID, int faceBookMentionTypeID)
        {
            string msg = "";
            try
            {
                msg = ProjectMention.AddIncludedFaceBookMentionTypeToMentionFilter(mentionFilterID, faceBookMentionTypeID);
                return Json(msg, JsonRequestBehavior.AllowGet);


            }
            catch (Exception ex)
            {
                msg = ex.Message;
                return Json(msg, JsonRequestBehavior.AllowGet);
            }
        }
        [HttpGet]
        public virtual JsonResult DeleteIncludedFaceBookMentionTypeFromMentionFilter(int includeFaceBookMentionTypeID)
        {
            string msg = "";
            try
            {
                msg = ProjectMention.DeleteIncludedFaceBookMentionTypeFromMentionFilter(includeFaceBookMentionTypeID);
                return Json(msg, JsonRequestBehavior.AllowGet);


            }
            catch (Exception ex)
            {
                msg = ex.Message;
                return Json(msg, JsonRequestBehavior.AllowGet);
            }
        }


        /// <summary>
        /// Excluded FaceBook Mention Type Section
        /// </summary>
        /// <param name="mentionFilterID"></param>
        /// <returns></returns>

        [AcceptVerbs(HttpVerbs.Get | HttpVerbs.Post)]
        public ActionResult GetAllExcludedFaceBookMentionTypes(int mentionFilterID)
        {
            var listOfExcludedFaceBookMentionTypes = ProjectMention.GetAllExcludedFaceBookMentionTypes(mentionFilterID);
            return PartialView(listOfExcludedFaceBookMentionTypes);
        }

        [HttpGet]
        public virtual JsonResult AddExcludedFaceBookMentionTypeToMentionFilter(int mentionFilterID, int faceBookMentionTypeID)
        {
            string msg = "";
            try
            {
                msg = ProjectMention.AddExcludedFaceBookMentionTypeToMentionFilter(mentionFilterID, faceBookMentionTypeID);
                return Json(msg, JsonRequestBehavior.AllowGet);


            }
            catch (Exception ex)
            {
                msg = ex.Message;
                return Json(msg, JsonRequestBehavior.AllowGet);
            }
        }
        [HttpGet]
        public virtual JsonResult DeleteExcludedFaceBookMentionTypeFromMentionFilter(int excludeFaceBookMentionTypeID)
        {
            string msg = "";
            try
            {
                msg = ProjectMention.DeleteExcludedFaceBookMentionTypeFromMentionFilter(excludeFaceBookMentionTypeID);
                return Json(msg, JsonRequestBehavior.AllowGet);


            }
            catch (Exception ex)
            {
                msg = ex.Message;
                return Json(msg, JsonRequestBehavior.AllowGet);
            }
        }

        /// <summary>
        /// Included Instagram Mention Type Section
        /// </summary>
        /// <param name="mentionFilterID"></param>
        /// <returns></returns>

        [AcceptVerbs(HttpVerbs.Get | HttpVerbs.Post)]
        public ActionResult GetAllIncludedInstagramMentionTypes(int mentionFilterID)
        {
            var listOfIncludedInstagramMentionTypes = ProjectMention.GetAllIncludedInstagramMentionTypes(mentionFilterID);
            return PartialView(listOfIncludedInstagramMentionTypes);
        }

        [HttpGet]
        public virtual JsonResult AddIncludedInstagramMentionTypeToMentionFilter(int mentionFilterID, int instagramMentionTypeID)
        {
            string msg = "";
            try
            {
                msg = ProjectMention.AddIncludedInstagramMentionTypeToMentionFilter(mentionFilterID, instagramMentionTypeID);
                return Json(msg, JsonRequestBehavior.AllowGet);


            }
            catch (Exception ex)
            {
                msg = ex.Message;
                return Json(msg, JsonRequestBehavior.AllowGet);
            }
        }
        [HttpGet]
        public virtual JsonResult DeleteIncludedInstagramMentionTypeFromMentionFilter(int includeInstagramMentionTypeID)
        {
            string msg = "";
            try
            {
                msg = ProjectMention.DeleteIncludedInstagramMentionTypeFromMentionFilter(includeInstagramMentionTypeID);
                return Json(msg, JsonRequestBehavior.AllowGet);


            }
            catch (Exception ex)
            {
                msg = ex.Message;
                return Json(msg, JsonRequestBehavior.AllowGet);
            }
        }


        /// <summary>
        /// Excluded Instagram Mention Type Section
        /// </summary>
        /// <param name="mentionFilterID"></param>
        /// <returns></returns>

        [AcceptVerbs(HttpVerbs.Get | HttpVerbs.Post)]
        public ActionResult GetAllExcludedInstagramMentionTypes(int mentionFilterID)
        {
            var listOfExcludedInstagramMentionTypes = ProjectMention.GetAllExcludedInstagramMentionTypes(mentionFilterID);
            return PartialView(listOfExcludedInstagramMentionTypes);
        }

        [HttpGet]
        public virtual JsonResult AddExcludedInstagramMentionTypeToMentionFilter(int mentionFilterID, int instagramMentionTypeID)
        {
            string msg = "";
            try
            {
                msg = ProjectMention.AddExcludedInstagramMentionTypeToMentionFilter(mentionFilterID, instagramMentionTypeID);
                return Json(msg, JsonRequestBehavior.AllowGet);


            }
            catch (Exception ex)
            {
                msg = ex.Message;
                return Json(msg, JsonRequestBehavior.AllowGet);
            }
        }
        [HttpGet]
        public virtual JsonResult DeleteExcludedInstagramMentionTypeFromMentionFilter(int excludeInstagramMentionTypeID)
        {
            string msg = "";
            try
            {
                msg = ProjectMention.DeleteExcludedInstagramMentionTypeFromMentionFilter(excludeInstagramMentionTypeID);
                return Json(msg, JsonRequestBehavior.AllowGet);


            }
            catch (Exception ex)
            {
                msg = ex.Message;
                return Json(msg, JsonRequestBehavior.AllowGet);
            }
        }


        /// <summary>
        /// Included Subreddit Topics Section
        /// </summary>
        /// <param name="mentionFilterID"></param>
        /// <returns></returns>

        [AcceptVerbs(HttpVerbs.Get | HttpVerbs.Post)]
        public ActionResult GetAllIncludedSubredditTopics(int mentionFilterID)
        {
            var listOfIncludedSubredditTopics = ProjectMention.GetAllIncludedSubredditTopics(mentionFilterID);
            return PartialView(listOfIncludedSubredditTopics);
        }

        [HttpGet]
        public virtual JsonResult AddIncludedSubredditTopicsToMentionFilter(int mentionFilterID, int subredditTopicsID)
        {
            string msg = "";
            try
            {
                msg = ProjectMention.AddIncludedSubredditTopicToMentionFilter(mentionFilterID, subredditTopicsID);
                return Json(msg, JsonRequestBehavior.AllowGet);


            }
            catch (Exception ex)
            {
                msg = ex.Message;
                return Json(msg, JsonRequestBehavior.AllowGet);
            }
        }
        [HttpGet]
        public virtual JsonResult DeleteIncludedSubredditTopicFromMentionFilter(int includeSubredditTopicID)
        {
            string msg = "";
            try
            {
                msg = ProjectMention.DeleteIncludedSubredditTopicFromMentionFilter(includeSubredditTopicID);
                return Json(msg, JsonRequestBehavior.AllowGet);


            }
            catch (Exception ex)
            {
                msg = ex.Message;
                return Json(msg, JsonRequestBehavior.AllowGet);
            }
        }


        /// <summary>
        /// Excluded Subreddit Topics Section
        /// </summary>
        /// <param name="mentionFilterID"></param>
        /// <returns></returns>

        [AcceptVerbs(HttpVerbs.Get | HttpVerbs.Post)]
        public ActionResult GetAllExcludedSubredditTopics(int mentionFilterID)
        {
            var listOfIncludedSubredditTopics = ProjectMention.GetAllExcludedSubredditTopics(mentionFilterID);
            return PartialView(listOfIncludedSubredditTopics);
        }

        [HttpGet]
        public virtual JsonResult AddExcludedSubredditTopicsToMentionFilter(int mentionFilterID, int subredditTopicsID)
        {
            string msg = "";
            try
            {
                msg = ProjectMention.AddExcludedSubredditTopicToMentionFilter(mentionFilterID, subredditTopicsID);
                return Json(msg, JsonRequestBehavior.AllowGet);


            }
            catch (Exception ex)
            {
                msg = ex.Message;
                return Json(msg, JsonRequestBehavior.AllowGet);
            }
        }
        [HttpGet]
        public virtual JsonResult DeleteExcludedSubredditTopicFromMentionFilter(int excludeSubredditTopicID)
        {
            string msg = "";
            try
            {
                msg = ProjectMention.DeleteExcludedSubredditTopicFromMentionFilter(excludeSubredditTopicID);
                return Json(msg, JsonRequestBehavior.AllowGet);


            }
            catch (Exception ex)
            {
                msg = ex.Message;
                return Json(msg, JsonRequestBehavior.AllowGet);
            }
        }


        /// <summary>
        /// Included Twitter AccountTypes Section
        /// </summary>
        /// <param name="mentionFilterID"></param>
        /// <returns></returns>

        [AcceptVerbs(HttpVerbs.Get | HttpVerbs.Post)]
        public ActionResult GetAllIncludedTwitterAccountTypes(int mentionFilterID)
        {
            var listOfIncludedTwitterAccountTypes = ProjectMention.GetAllIncludedTwitterAccountTypes(mentionFilterID);
            return PartialView(listOfIncludedTwitterAccountTypes);
        }

        [HttpGet]
        public virtual JsonResult AddIncludedTwitterAccountTypeToMentionFilter(int mentionFilterID, int twitterAccountTypeID)
        {
            string msg = "";
            try
            {
                msg = ProjectMention.AddIncludedTwitterAccountTypeToMentionFilter(mentionFilterID, twitterAccountTypeID);
                return Json(msg, JsonRequestBehavior.AllowGet);


            }
            catch (Exception ex)
            {
                msg = ex.Message;
                return Json(msg, JsonRequestBehavior.AllowGet);
            }
        }
        [HttpGet]
        public virtual JsonResult DeleteIncludedTwitterAccountTypeFromMentionFilter(int includeTwitterAccountTypeID)
        {
            string msg = "";
            try
            {
                msg = ProjectMention.DeleteIncludedTwitterAccountTypeFromMentionFilter(includeTwitterAccountTypeID);
                return Json(msg, JsonRequestBehavior.AllowGet);


            }
            catch (Exception ex)
            {
                msg = ex.Message;
                return Json(msg, JsonRequestBehavior.AllowGet);
            }
        }


        /// <summary>
        /// Excluded Twitter AccountTypes Section
        /// </summary>
        /// <param name="mentionFilterID"></param>
        /// <returns></returns>

        [AcceptVerbs(HttpVerbs.Get | HttpVerbs.Post)]
        public ActionResult GetAllExcludedTwitterAccountTypes(int mentionFilterID)
        {
            var listOfEXcludedTwitterAccountTypes = ProjectMention.GetAllExcludedTwitterAccountTypes(mentionFilterID);
            return PartialView(listOfEXcludedTwitterAccountTypes);
        }

        [HttpGet]
        public virtual JsonResult AddExcludedTwitterAccountTypeToMentionFilter(int mentionFilterID, int twitterAccountTypeID)
        {
            string msg = "";
            try
            {
                msg = ProjectMention.AddExcludedTwitterAccountTypeToMentionFilter(mentionFilterID, twitterAccountTypeID);
                return Json(msg, JsonRequestBehavior.AllowGet);


            }
            catch (Exception ex)
            {
                msg = ex.Message;
                return Json(msg, JsonRequestBehavior.AllowGet);
            }
        }
        [HttpGet]
        public virtual JsonResult DeleteExcludedTwitterAccountTypeFromMentionFilter(int excludeTwitterAccountTypeID)
        {
            string msg = "";
            try
            {
                msg = ProjectMention.DeleteExcludedTwitterAccountTypeFromMentionFilter(excludeTwitterAccountTypeID);
                return Json(msg, JsonRequestBehavior.AllowGet);


            }
            catch (Exception ex)
            {
                msg = ex.Message;
                return Json(msg, JsonRequestBehavior.AllowGet);
            }
        }

        /// <summary>
        /// Included Twitter Gender Section
        /// </summary>
        /// <param name="mentionFilterID"></param>
        /// <returns></returns>

        [AcceptVerbs(HttpVerbs.Get | HttpVerbs.Post)]
        public ActionResult GetAllIncludedTwitterGenders(int mentionFilterID)
        {
            var listOfIncludedTwitterGenders = ProjectMention.GetAllIncludedTwitterGenders(mentionFilterID);
            return PartialView(listOfIncludedTwitterGenders);
        }

        [HttpGet]
        public virtual JsonResult AddIncludedTwitterGenderToMentionFilter(int mentionFilterID, int twitterGenderID)
        {
            string msg = "";
            try
            {
                msg = ProjectMention.AddIncludedTwitterGenderToMentionFilter(mentionFilterID, twitterGenderID);
                return Json(msg, JsonRequestBehavior.AllowGet);


            }
            catch (Exception ex)
            {
                msg = ex.Message;
                return Json(msg, JsonRequestBehavior.AllowGet);
            }
        }
        [HttpGet]
        public virtual JsonResult DeleteIncludedTwitterGenderFromMentionFilter(int includeTwitterGenderID)
        {
            string msg = "";
            try
            {
                msg = ProjectMention.DeleteIncludedTwitterGenderFromMentionFilter(includeTwitterGenderID);
                return Json(msg, JsonRequestBehavior.AllowGet);


            }
            catch (Exception ex)
            {
                msg = ex.Message;
                return Json(msg, JsonRequestBehavior.AllowGet);
            }
        }

        /// <summary>
        /// Included Twitter Professions Section
        /// </summary>
        /// <param name="mentionFilterID"></param>
        /// <returns></returns>

        [AcceptVerbs(HttpVerbs.Get | HttpVerbs.Post)]
        public ActionResult GetAllIncludedTwitterProfessions(int mentionFilterID)
        {
            var listOfIncludedTwitterProfessions = ProjectMention.GetAllIncludedTwitterProfessions(mentionFilterID);
            return PartialView(listOfIncludedTwitterProfessions);
        }

        [HttpGet]
        public virtual JsonResult AddIncludedTwitterProfessionToMentionFilter(int mentionFilterID, int twitterProfessionID)
        {
            string msg = "";
            try
            {
                msg = ProjectMention.AddIncludedTwitterProfessionToMentionFilter(mentionFilterID, twitterProfessionID);
                return Json(msg, JsonRequestBehavior.AllowGet);


            }
            catch (Exception ex)
            {
                msg = ex.Message;
                return Json(msg, JsonRequestBehavior.AllowGet);
            }
        }
        [HttpGet]
        public virtual JsonResult DeleteIncludedTwitterProfessionFromMentionFilter(int includeTwitterProfessionID)
        {
            string msg = "";
            try
            {
                msg = ProjectMention.DeleteIncludedTwitterProfessionFromMentionFilter(includeTwitterProfessionID);
                return Json(msg, JsonRequestBehavior.AllowGet);


            }
            catch (Exception ex)
            {
                msg = ex.Message;
                return Json(msg, JsonRequestBehavior.AllowGet);
            }
        }


        /// <summary>
        /// Excluded Twitter Professions Section
        /// </summary>
        /// <param name="mentionFilterID"></param>
        /// <returns></returns>

        [AcceptVerbs(HttpVerbs.Get | HttpVerbs.Post)]
        public ActionResult GetAllExcludedTwitterProfessions(int mentionFilterID)
        {
            var listOfIncludedTwitterProfessions = ProjectMention.GetAllExcludedTwitterProfessions(mentionFilterID);
            return PartialView(listOfIncludedTwitterProfessions);
        }

        [HttpGet]
        public virtual JsonResult AddExcludedTwitterProfessionToMentionFilter(int mentionFilterID, int twitterProfessionID)
        {
            string msg = "";
            try
            {
                msg = ProjectMention.AddExcludedTwitterProfessionToMentionFilter(mentionFilterID, twitterProfessionID);
                return Json(msg, JsonRequestBehavior.AllowGet);


            }
            catch (Exception ex)
            {
                msg = ex.Message;
                return Json(msg, JsonRequestBehavior.AllowGet);
            }
        }
        [HttpGet]
        public virtual JsonResult DeleteExcludedTwitterProfessionFromMentionFilter(int excludeTwitterProfessionID)
        {
            string msg = "";
            try
            {
                msg = ProjectMention.DeleteExcludedTwitterProfessionFromMentionFilter(excludeTwitterProfessionID);
                return Json(msg, JsonRequestBehavior.AllowGet);


            }
            catch (Exception ex)
            {
                msg = ex.Message;
                return Json(msg, JsonRequestBehavior.AllowGet);
            }
        }


        /// <summary>
        /// Included Twitter Interests Section
        /// </summary>
        /// <param name="mentionFilterID"></param>
        /// <returns></returns>

        [AcceptVerbs(HttpVerbs.Get | HttpVerbs.Post)]
        public ActionResult GetAllIncludedTwitterInterests(int mentionFilterID)
        {
            var listOfIncludedTwitterInterests = ProjectMention.GetAllIncludedTwitterInterests(mentionFilterID);
            return PartialView(listOfIncludedTwitterInterests);
        }

        [HttpGet]
        public virtual JsonResult AddIncludedTwitterInterestToMentionFilter(int mentionFilterID, int twitterInterestID)
        {
            string msg = "";
            try
            {
                msg = ProjectMention.AddIncludedTwitterInterestToMentionFilter(mentionFilterID, twitterInterestID);
                return Json(msg, JsonRequestBehavior.AllowGet);


            }
            catch (Exception ex)
            {
                msg = ex.Message;
                return Json(msg, JsonRequestBehavior.AllowGet);
            }
        }
        [HttpGet]
        public virtual JsonResult DeleteIncludedTwitterInterestFromMentionFilter(int includeTwitterInterestID)
        {
            string msg = "";
            try
            {
                msg = ProjectMention.DeleteIncludedTwitterInterestFromMentionFilter(includeTwitterInterestID);
                return Json(msg, JsonRequestBehavior.AllowGet);


            }
            catch (Exception ex)
            {
                msg = ex.Message;
                return Json(msg, JsonRequestBehavior.AllowGet);
            }
        }


        /// <summary>
        /// Excluded Twitter Interests Section
        /// </summary>
        /// <param name="mentionFilterID"></param>
        /// <returns></returns>

        [AcceptVerbs(HttpVerbs.Get | HttpVerbs.Post)]
        public ActionResult GetAllExcludedTwitterInterests(int mentionFilterID)
        {
            var listOfIncludedTwitterInterests = ProjectMention.GetAllExcludedTwitterInterests(mentionFilterID);
            return PartialView(listOfIncludedTwitterInterests);
        }

        [HttpGet]
        public virtual JsonResult AddExcludedTwitterInterestToMentionFilter(int mentionFilterID, int twitterInterestID)
        {
            string msg = "";
            try
            {
                msg = ProjectMention.AddExcludedTwitterInterestToMentionFilter(mentionFilterID, twitterInterestID);
                return Json(msg, JsonRequestBehavior.AllowGet);


            }
            catch (Exception ex)
            {
                msg = ex.Message;
                return Json(msg, JsonRequestBehavior.AllowGet);
            }
        }
        [HttpGet]
        public virtual JsonResult DeleteExcludedTwitterInterestFromMentionFilter(int excludeTwitterInterestID)
        {
            string msg = "";
            try
            {
                msg = ProjectMention.DeleteExcludedTwitterInterestFromMentionFilter(excludeTwitterInterestID);
                return Json(msg, JsonRequestBehavior.AllowGet);


            }
            catch (Exception ex)
            {
                msg = ex.Message;
                return Json(msg, JsonRequestBehavior.AllowGet);
            }
        }


        /// <summary>
        /// Included Sub Type Section
        /// </summary>
        /// <param name="mentionFilterID"></param>
        /// <returns></returns>

        [AcceptVerbs(HttpVerbs.Get | HttpVerbs.Post)]
        public ActionResult GetAllIncludedSubTypes(int mentionFilterID)
        {
            var listOfIncludedSubTypes = ProjectMention.GetAllIncludedSubTypes(mentionFilterID);
            return PartialView(listOfIncludedSubTypes);
        }

        [HttpGet]
        public virtual JsonResult AddIncludedSubTypeToMentionFilter(int mentionFilterID, int subTypeID)
        {
            string msg = "";
            try
            {
                msg = ProjectMention.AddIncludedSubTypeToMentionFilter(mentionFilterID, subTypeID);
                return Json(msg, JsonRequestBehavior.AllowGet);


            }
            catch (Exception ex)
            {
                msg = ex.Message;
                return Json(msg, JsonRequestBehavior.AllowGet);
            }
        }
        [HttpGet]
        public virtual JsonResult DeleteIncludedSubTypetFromMentionFilter(int includeSubTypeID)
        {
            string msg = "";
            try
            {
                msg = ProjectMention.DeleteIncludedSubTypeFromMentionFilter(includeSubTypeID);
                return Json(msg, JsonRequestBehavior.AllowGet);


            }
            catch (Exception ex)
            {
                msg = ex.Message;
                return Json(msg, JsonRequestBehavior.AllowGet);
            }
        }

        /// <summary>
        /// Included News Bar Domain Section
        /// </summary>
        /// <param name="mentionFilterID"></param>
        /// <returns></returns>

        [AcceptVerbs(HttpVerbs.Get | HttpVerbs.Post)]
        public ActionResult GetAllIncludedDomains(int mentionFilterID)
        {
            var listOfIncludedDomains = ProjectMention.GetAllIncludedDomains(mentionFilterID);
            return PartialView(listOfIncludedDomains);
        }

        [HttpGet]
        public virtual JsonResult AddIncludedDomainsToMentionFilter(int mentionFilterID, int domainID)
        {
            string msg = "";
            try
            {
                msg = ProjectMention.AddIncludedDomainToMentionFilter(mentionFilterID, domainID);
                return Json(msg, JsonRequestBehavior.AllowGet);


            }
            catch (Exception ex)
            {
                msg = ex.Message;
                return Json(msg, JsonRequestBehavior.AllowGet);
            }
        }
        [HttpGet]
        public virtual JsonResult DeleteIncludedDomainFromMentionFilter(int includeDomainID)
        {
            string msg = "";
            try
            {
                msg = ProjectMention.DeleteIncludedDomainFromMentionFilter(includeDomainID);
                return Json(msg, JsonRequestBehavior.AllowGet);


            }
            catch (Exception ex)
            {
                msg = ex.Message;
                return Json(msg, JsonRequestBehavior.AllowGet);
            }
        }

        /// <summary>
        /// Excluded Sub Type Section
        /// </summary>
        /// <param name="mentionFilterID"></param>
        /// <returns></returns>

        [AcceptVerbs(HttpVerbs.Get | HttpVerbs.Post)]
        public ActionResult GetAllExcludedSubTypes(int mentionFilterID)
        {
            var listOfExcludedSubTypes = ProjectMention.GetAllExcludedSubTypes(mentionFilterID);
            return PartialView(listOfExcludedSubTypes);
        }

        [HttpGet]
        public virtual JsonResult AddExcludedSubTypeToMentionFilter(int mentionFilterID, int subTypeID)
        {
            string msg = "";
            try
            {
                msg = ProjectMention.AddExcludedSubTypeToMentionFilter(mentionFilterID, subTypeID);
                return Json(msg, JsonRequestBehavior.AllowGet);


            }
            catch (Exception ex)
            {
                msg = ex.Message;
                return Json(msg, JsonRequestBehavior.AllowGet);
            }
        }
        [HttpGet]
        public virtual JsonResult DeleteExcludedSubTypetFromMentionFilter(int excludeSubTypeID)
        {
            string msg = "";
            try
            {
                msg = ProjectMention.DeleteExcludedSubTypeFromMentionFilter(excludeSubTypeID);
                return Json(msg, JsonRequestBehavior.AllowGet);


            }
            catch (Exception ex)
            {
                msg = ex.Message;
                return Json(msg, JsonRequestBehavior.AllowGet);
            }
        }
        /// <summary>
        /// Get SelectList Items to puplate the Dropdown lists
        /// </summary>
        /// <param name="clientId", projectId, categoryId, queryId></param>
        /// <returns></returns>

        [HttpGet]
        public virtual JsonResult GetProjectSourceTypes(int projectId)
        {
            var _SourceTypes = ProjectMention.GetProjectSourceTyps(projectId);
            return Json(_SourceTypes, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public virtual JsonResult GetProjectSubTypes(int projectId)
        {
            var _SubTypes = ProjectMention.GetProjectSubTypes(projectId);
            return Json(_SubTypes, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public virtual JsonResult GetProjectTwitterInterests(int projectId)
        {
            var _TwitterInterests = ProjectMention.GetProjectTwitterInterests(projectId);
            return Json(_TwitterInterests, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public virtual JsonResult GetProjectTwitterProfessions(int projectId)
        {
            var _TwitterProfessions = ProjectMention.GetProjectTwitterProfessions(projectId);
            return Json(_TwitterProfessions, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public virtual JsonResult GetProjectTwitterGenders(int projectId)
        {
            var _TwitterGenders = ProjectMention.GetProjectTwitterGenders(projectId);
            return Json(_TwitterGenders, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public virtual JsonResult GetProjectTwitterAccountTypes(int projectId)
        {
            var _TwitterAccountTypes = ProjectMention.GetProjectTwitterAccountTypes(projectId);
            return Json(_TwitterAccountTypes, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public virtual JsonResult GetProjectSubredditTopics(int projectId)
        {
            var _SubredditTopics = ProjectMention.GetProjectSubredditTopics(projectId);
            return Json(_SubredditTopics, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public virtual JsonResult GetProjectRedditNSFWs(int projectId)
        {
            var _RedditNSFWs = ProjectMention.GetProjectRedditNSFWs(projectId);
            return Json(_RedditNSFWs, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public virtual JsonResult GetProjectRedditPolls(int projectId)
        {
            var _RedditPolls = ProjectMention.GetProjectRedditPoll(projectId);
            return Json(_RedditPolls, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public virtual JsonResult GetQueryContentSource(int queryId)
        {
            var _QueryContentSources = ProjectMention.GetQueryContentSources(queryId);
            return Json(_QueryContentSources, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public virtual JsonResult GetQueryLanguages(int queryId)
        {
            var _QueryLanguages = ProjectMention.GetQueryLanguages(queryId);
            return Json(_QueryLanguages, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public virtual JsonResult GetProjectInstagramMentionTypes(int projectId)
        {
            var _InstagramMentionTypes = ProjectMention.GetProjectInstagramMentionType(projectId);
            return Json(_InstagramMentionTypes, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public virtual JsonResult GetProjectFaceBookMentionTypes(int projectId)
        {
            var _FaceBookMentionTypes = ProjectMention.GetProjectFaceBookMentionType(projectId);
            return Json(_FaceBookMentionTypes, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public virtual JsonResult GetProjectFaceBookUserRoels(int projectId)
        {
            var _FaceBookUserRoels = ProjectMention.GetProjectFaceBookUserRoels(projectId);
            return Json(_FaceBookUserRoels, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public virtual JsonResult GetProjectTwitterVerificationTypes(int projectId)
        {
            var TwitterVerificationTypes = ProjectMention.GetProjectTwitterVerificationTypes(projectId);
            return Json(TwitterVerificationTypes, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public virtual JsonResult GetProjectTwitterVerifications(int projectId)
        {
            var TwitterVerifications = ProjectMention.GetProjectTwitterVerifications(projectId);
            return Json(TwitterVerifications, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public virtual JsonResult GetProjectTwitterMentionTypes(int projectId)
        {
            var TwitterMentionTypes = ProjectMention.GetProjectTwitterMentionTypes(projectId);
            return Json(TwitterMentionTypes, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public virtual JsonResult GetProjectMentionStatus(int projectId)
        {
            var _MentionStatuses = ProjectMention.GetProjectMentionStatus(projectId);
            return Json(_MentionStatuses, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public virtual JsonResult GetProjectPriorities(int projectId)
        {
            var Assignements = ProjectMention.GetProjectPriorities(projectId);
            return Json(Assignements, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public virtual JsonResult GetProjectCheckedMentions(int projectId)
        {
            var Assignements = ProjectMention.GetProjectCheckedMentions(projectId);
            return Json(Assignements, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public virtual JsonResult GetProjectAssignements(int projectId)
        {
            var Assignements = ProjectMention.GetProjectAssignements(projectId);
            return Json(Assignements, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public virtual JsonResult GetProjectTwitterUserRole(int projectId)
        {
            var TwitterUserRoles = ProjectMention.GetProjectTwitterUserRole(projectId);
            return Json(TwitterUserRoles, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public virtual JsonResult GetProjectSiteList(int projectId)
        {
            var SiteLists = ProjectMention.GetProjectSiteList(projectId);
            return Json(SiteLists, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public virtual JsonResult GetProjectLanguages(int projectId)
        {
            var Languages = ProjectMention.GetProjectLanguage(projectId);
            return Json(Languages, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public virtual JsonResult GetProjectLocation(int projectId)
        {
            var AuthorGroups = ProjectMention.GetProjectLocation(projectId);
            return Json(AuthorGroups, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public virtual JsonResult GetProjectAuthorGroup(int projectId)
        {
            var AuthorGroups = ProjectMention.GetProjectAuthorGroup(projectId);
            return Json(AuthorGroups, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public virtual JsonResult GetProjectSocialPanal(int projectId)
        {
            var SocialPanals = ProjectMention.GetProjectSocialPanal(projectId);
            return Json(SocialPanals, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public virtual JsonResult GetProjectEmotions(int projectId)
        {
            var Sentiments = ProjectMention.GetProjectEmotion(projectId);
            return Json(Sentiments, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public virtual JsonResult GetProjectSentiment(int projectId)
        {
            var Sentiments = ProjectMention.GetProjectSentiment(projectId);
            return Json(Sentiments, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public virtual JsonResult GetClientProjects(int clientId)
        {
            var Categories = ProjectMention.GetClientProjectList(clientId);
            return Json(Categories, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public virtual JsonResult GetProjectQueries(int projectId)
        {
            var Queries = ProjectMention.GetProjectQueries(projectId);
            return Json(Queries, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public virtual JsonResult GetProjectTags(int projectId)
        {
            var Tags = ProjectMention.GetProjectTags(projectId);
            return Json(Tags, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public virtual JsonResult GetProjectContentSources(int projectId)
        {
            var ContentSources = ProjectMention.GetProjectContentSources(projectId);
            return Json(ContentSources, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public virtual JsonResult GetProjectMentionTypes(int projectId)
        {
            var _contentSources = ProjectMention.GetProjectMentionTypes(projectId);
            return Json(_contentSources, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public virtual JsonResult GetProjectReactScoreOverAll(int projectId)
        {
            var _reactScoreOverAll = ProjectMention.GetProjectReactScoreOverAll(projectId);
            return Json(_reactScoreOverAll, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public virtual JsonResult GetProjectReactScoreHarm(int projectId)
        {
            var _reactScoreHarm = ProjectMention.GetProjectReactScoreHarm(projectId);
            return Json(_reactScoreHarm, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public virtual JsonResult GetProjectReactScoreEmotionality(int projectId)
        {
            var _reactScoreEmotionality = ProjectMention.GetProjectReactScoreEmotionality(projectId);
            return Json(_reactScoreEmotionality, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public virtual JsonResult GetProjectCategories(int projectId)
        {
            var Categories = ProjectMention.GetProjectCategories(projectId);
            return Json(Categories, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public virtual JsonResult GetCategorySubCategories(int categoryId)
        {
            var subCategories = ProjectMention.GetSubCategories(categoryId);          
            return Json(subCategories, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public virtual JsonResult GetExcludedCategorySubCategories(int categoryId)
        {
            var subCategories = ProjectMention.GetSubCategories(categoryId);           
            return Json(subCategories, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public virtual JsonResult GetProjectAllSubCategories(int projectId)
        {
            var subCategories = ProjectMention.GetProjectAllSubCategories(projectId);
            return Json(subCategories, JsonRequestBehavior.AllowGet);
        }

        

        [HttpGet]
        public virtual JsonResult GetProjectRuleTag(int projectId)
        {
            var ruleTags = ProjectMention.GetRuleTags(projectId);
            return Json(ruleTags, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public virtual JsonResult GetProjectGlobalMetrics(int projectId)
        {
            var ruleTags = ProjectMention.GetGlobalMetrics(projectId);
            return Json(ruleTags, JsonRequestBehavior.AllowGet);
        }
    }
}
