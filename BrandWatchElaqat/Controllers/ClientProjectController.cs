using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Mvc;
using BrandWatchElaqat.Models;
using BrandWatchElaqat.ViewModel;
using Newtonsoft.Json;
namespace BrandWatchElaqat.Controllers
{
    [Authorize(Roles = "Employee,Admin")]
    public class ClientProjectController : Controller
    {
        public async Task<ActionResult> GetBrandWatchClientProjects()
        {

            string apiUrl = "https://api.brandwatch.com/projects";
            string token = "b17c9054-36a6-4128-9ad3-32bc7d3ea0d3";
            List<ProjectMapper> brandWatchClientProjectsList = new List<ProjectMapper>();
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(apiUrl);
                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                var response = await client.GetAsync(apiUrl);
                if (response.IsSuccessStatusCode)
                {
                    var data = await response.Content.ReadAsStringAsync();
                    Dictionary<string, object> dictionarydata = JsonConvert.DeserializeObject<Dictionary<string, object>>(data);
                    foreach (KeyValuePair<string, object> entry in dictionarydata)
                    {

                        if (entry.Key == "results")
                        {
                            string myjson = entry.Value.ToString();
                            brandWatchClientProjectsList = JsonConvert.DeserializeObject<List<ProjectMapper>>(myjson);

                        }
                    }


                }


            }
            ClientProject.AddNewBrandWatchProjects(brandWatchClientProjectsList);
            return RedirectToAction("IndexOfBrandWatchClientProject");
        }

        public async Task<ActionResult> GetBrandWatchClientProjectCategoeries(int id)
        {


            var clientProject = ClientProject.GetClientProjectDetails(id);
            string apiUrl = "https://api.brandwatch.com/projects/" + clientProject.BrandwatchProjectID + "/categories";
            string token = "b17c9054-36a6-4128-9ad3-32bc7d3ea0d3";

            List<CategoryMapper> CategoriesList = new List<CategoryMapper>();
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(apiUrl);
                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                var response = await client.GetAsync(apiUrl);
                if (response.IsSuccessStatusCode)
                {
                    var data = await response.Content.ReadAsStringAsync();                   
                    Dictionary<string, object> dictionarydata = JsonConvert.DeserializeObject<Dictionary<string, object>>(data);
                    foreach (KeyValuePair<string, object> entry in dictionarydata)
                    {
                        
                        if (entry.Key == "results")
                        {
                            string myjson = entry.Value.ToString();
                            CategoriesList = JsonConvert.DeserializeObject<List<CategoryMapper>>(myjson);
                            
                        }
                    }
                   
                   
                   

                }


            }
           // var globalMetricsAreAdded = GetBrandWatchGlobalMetricsForClientProject(clientProject.Id);
            var model = ClientProject.AddCategoriesToClientProject(CategoriesList, clientProject.Id);

            return RedirectToAction("Details", new { id = model.Id });
        }

        public async Task<ActionResult> GetBrandWatchClientProjectQueries(int id)
        {

           

            var clientProject = ClientProject.GetClientProjectDetails(id);
            string apiUrl = "https://api.brandwatch.com/projects/" + clientProject.BrandwatchProjectID + "/queries";
            string token = "b17c9054-36a6-4128-9ad3-32bc7d3ea0d3";

            List<QueryMapper> QueryList = new List<QueryMapper>();
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(apiUrl);
                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                var response = await client.GetAsync(apiUrl);
                if (response.IsSuccessStatusCode)
                {
                    var data = await response.Content.ReadAsStringAsync();
                    Dictionary<string, object> dictionarydata = JsonConvert.DeserializeObject<Dictionary<string, object>>(data);
                    foreach (KeyValuePair<string, object> entry in dictionarydata)
                    {

                        if (entry.Key == "results")
                        {
                            string myjson = entry.Value.ToString();
                            QueryList = JsonConvert.DeserializeObject<List<QueryMapper>>(myjson);

                        }
                    }
                }


            }
            var model = ClientProject.AddQueriesToClientProject(QueryList, clientProject.Id);

            return RedirectToAction("Details", new { id = clientProject.Id });
        }

        public async Task<ActionResult> GetBrandWatchClientProjectAuthorGroup(int id)
        {



            var clientProject = ClientProject.GetClientProjectDetails(id);
            string apiUrl = "https://api.brandwatch.com/projects/" + clientProject.BrandwatchProjectID + "/group/author/summary";
            string token = "b17c9054-36a6-4128-9ad3-32bc7d3ea0d3";

            List<AuthorGroupMapper> AuthorGroupList = new List<AuthorGroupMapper>();
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(apiUrl);
                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                var response = await client.GetAsync(apiUrl);
                if (response.IsSuccessStatusCode)
                {
                    var data = await response.Content.ReadAsStringAsync();
                    Dictionary<string, object> dictionarydata = JsonConvert.DeserializeObject<Dictionary<string, object>>(data);
                    foreach (KeyValuePair<string, object> entry in dictionarydata)
                    {

                        if (entry.Key == "results")
                        {
                            string myjson = entry.Value.ToString();
                            AuthorGroupList = JsonConvert.DeserializeObject<List<AuthorGroupMapper>>(myjson);

                        }
                    }
                }


            }
            var model = ClientProject.AddAuthorGroupToClientProject(AuthorGroupList, clientProject.Id);

            return RedirectToAction("Details", new { id = clientProject.Id });
        }

        public async Task<ActionResult> GetBrandWatchClientProjectLocation(int id)
        {

            var clientProject = ClientProject.GetClientProjectDetails(id);
            string apiUrl = "https://api.brandwatch.com/projects/" + clientProject.BrandwatchProjectID + "/group/location/summary";
            string token = "b17c9054-36a6-4128-9ad3-32bc7d3ea0d3";

            List<LocationMapper> LocationGroupList = new List<LocationMapper>();
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(apiUrl);
                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                var response = await client.GetAsync(apiUrl);
                if (response.IsSuccessStatusCode)
                {
                    var data = await response.Content.ReadAsStringAsync();
                    Dictionary<string, object> dictionarydata = JsonConvert.DeserializeObject<Dictionary<string, object>>(data);
                    foreach (KeyValuePair<string, object> entry in dictionarydata)
                    {

                        if (entry.Key == "results")
                        {
                            string myjson = entry.Value.ToString();
                            LocationGroupList = JsonConvert.DeserializeObject<List<LocationMapper>>(myjson);

                        }
                    }
                }


            }
            var model = ClientProject.AddLocationGroupToClientProject(LocationGroupList, clientProject.Id);

            return RedirectToAction("Details", new { id = clientProject.Id });
        }

        public async Task<ActionResult> GetBrandWatchClientProjectRuleTag(int id)
        {

            var clientProject = ClientProject.GetClientProjectDetails(id);
            string apiUrl = "https://api.brandwatch.com/projects/" + clientProject.BrandwatchProjectID + "/ruletags";
            string token = "b17c9054-36a6-4128-9ad3-32bc7d3ea0d3";

            List<ProjectRuleTagMapper> RuleTagMapperList = new List<ProjectRuleTagMapper>();
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(apiUrl);
                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                var response = await client.GetAsync(apiUrl);
                if (response.IsSuccessStatusCode)
                {
                    var data = await response.Content.ReadAsStringAsync();
                    Dictionary<string, object> dictionarydata = JsonConvert.DeserializeObject<Dictionary<string, object>>(data);
                    foreach (KeyValuePair<string, object> entry in dictionarydata)
                    {

                        if (entry.Key == "results")
                        {
                            string myjson = entry.Value.ToString();
                            RuleTagMapperList = JsonConvert.DeserializeObject<List<ProjectRuleTagMapper>>(myjson);

                        }
                    }
                }


            }
            var model = ClientProject.AddProjectRuleTagToClientProject(RuleTagMapperList, clientProject.Id);

            return RedirectToAction("Details", new { id = clientProject.Id });
        }
        public async Task<ActionResult> GetBrandWatchGlobalMetricsForClientProject(int id)
        {
            //var clientProject = ClientProject.GetClientProjectDetails(projectId);
            string apiUrl = "https://api.brandwatch.com/metrics";
            string token = "b17c9054-36a6-4128-9ad3-32bc7d3ea0d3";

            GlobalMetricsMapper GlobalMetrics = new GlobalMetricsMapper();
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(apiUrl);
                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                var response = await client.GetAsync(apiUrl);
                if (response.IsSuccessStatusCode)
                {
                    var data = await response.Content.ReadAsStringAsync();
                    GlobalMetrics = JsonConvert.DeserializeObject<GlobalMetricsMapper>(data);
                    Dictionary<string, object> dictionarydata = JsonConvert.DeserializeObject<Dictionary<string, object>>(data);
                    foreach (KeyValuePair<string, object> entry in dictionarydata)
                    {

                        if (entry.Key == "results")
                        {
                            string myjson = entry.Value.ToString();
                            GlobalMetrics = JsonConvert.DeserializeObject<GlobalMetricsMapper>(myjson);
                        }
                    }
                }
            }
            var model = ClientProject.AddGlobalMetricsToClientProject(GlobalMetrics, id);
            return RedirectToAction("Details", new { id = model.Id });
        }
        public async Task<ActionResult> GetBrandWatchClientProjectSites(int id)
        {

            var clientProject = ClientProject.GetClientProjectDetails(id);
            string apiUrl = "https://api.brandwatch.com/projects/" + clientProject.BrandwatchProjectID + "/group/site/summary";
            string token = "b17c9054-36a6-4128-9ad3-32bc7d3ea0d3";

            List<SiteMapper> SiteGroupList = new List<SiteMapper>();
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(apiUrl);
                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                var response = await client.GetAsync(apiUrl);
                if (response.IsSuccessStatusCode)
                {
                    var data = await response.Content.ReadAsStringAsync();
                    Dictionary<string, object> dictionarydata = JsonConvert.DeserializeObject<Dictionary<string, object>>(data);
                    foreach (KeyValuePair<string, object> entry in dictionarydata)
                    {

                        if (entry.Key == "results")
                        {
                            string myjson = entry.Value.ToString();
                            SiteGroupList = JsonConvert.DeserializeObject<List<SiteMapper>>(myjson);

                        }
                    }
                }


            }
            var model = ClientProject.AddSitesToClientProject(SiteGroupList, clientProject.Id);

            return RedirectToAction("Details", new { id = clientProject.Id });
        }


        // GET: ClientProject
        public ActionResult Index(string msg)
        {
            ViewBag.Message = msg;
            var model = ClientProject.GetAllClientsProjects();
            return View(model);
        }

        public ActionResult IndexOfBrandWatchClientProject()
        {
            var model = ClientProject.GetAllBrandWatchClientsProjects();
            return View(model);
        }
       
        // GET: ClientProject/Details/5
        public ActionResult Details(string msg, int id)
        {
            
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

      
        public ActionResult AssignProjectToClient(int id)
        {
            string msg = "";
            if (id > 0)
            {
                ViewBag.Message = msg;
                var model = ClientProject.PrepareClientProjectToBeAssigned(id);
                if (model == null)
                {
                    msg = Resources.Projects.ProjectIsNotFound;
                    return RedirectToAction("Index", "ClientProject", new { msg });
                }
                return View(model);
            }
            else
            {
                msg = Resources.Mention.ClientOrProjectDetailsAreMissing;
                return RedirectToAction("Index", "ClientProject", new { msg });
            }
            
           
        }

        // POST: ClientProject/Create
        [HttpPost]
        public ActionResult AssignProjectToClient(ClientProjectVM clientProjectVM)
        {
            try
            {
                var model = ClientProject.AssigneNewClientProjectToElaqatClient(clientProjectVM);
                 
                
                if (model == null)
                {
                    var _model = ClientProject.PrepareClientProjectToBeAssigned(clientProjectVM.Id);
                    ViewBag.Message = Resources.Projects.TechnicalFailure;
                    return View(_model);
                }
                
                return RedirectToAction("Details", "ElaqatClient", new { id = model.ElaqatClientId });
            }
            catch
            {
                var _model = ClientProject.PrepareClientProjectToBeAssigned(clientProjectVM.Id);
                ViewBag.Message = Resources.Projects.TechnicalFailure;
                return View(_model);
            }
        }

        public ActionResult AssignImprtedProjectToClient(string msg, int id)
        {
            
           
            if (id > 0)
            {
                ViewBag.Message = msg;
                var model = ClientProject.PrepareImportedClientProjectToBeAssigned(id);
                if (model == null)
                {
                    msg = Resources.Projects.ProjectIsNotFound;
                    return RedirectToAction("Index", "ClientProject", new { msg });
                }
                return View(model);
            }
            else
            {
                msg = Resources.Mention.ClientOrProjectDetailsAreMissing;
                return RedirectToAction("Index", "ClientProject", new { msg });
            }
        }

        // POST: ClientProject/Create
        [HttpPost]
        public ActionResult AssignImprtedProjectToClient(ClientProjectVM clientProjectVM)
        {
            try
            {
                var modelID = ClientProject.AssigneImportedClientProjectToElaqatClient(clientProjectVM);
                if (modelID < 1)
                {
                    var model = ClientProject.PrepareImportedClientProjectToBeAssigned(clientProjectVM.Id);
                    ViewBag.Message = Resources.Projects.TechnicalFailure;
                    return View(model);
                }
                var AddGlobalMetrics = GetBrandWatchGlobalMetricsForClientProject(modelID);
                return RedirectToAction("Details", new { id = modelID });
            }
            catch
            {
                var model = ClientProject.PrepareImportedClientProjectToBeAssigned(clientProjectVM.Id);
                ViewBag.Message = Resources.Projects.TechnicalFailure;
                return View(model);
            }
        }
      

        
        
        // GET: ClientProject/Create
        public ActionResult Create(int id)
        {
            string msg = "";
            
            if (id > 0)
            {
                
                var model = ClientProject.PrepareClientProjectToBeAdded(id);
                if (model == null)
                {
                    msg = Resources.Mention.ClientOrProjectDetailsAreMissing;
                    return RedirectToAction("Index", "ClientProject", new { msg });
                }
                return View(model);
            }
            else
            {
                msg = Resources.Mention.ClientOrProjectDetailsAreMissing;
                return RedirectToAction("Index", "ClientProject", new { msg });
            }
        }

        // POST: ClientProject/Create
        [HttpPost]
        public ActionResult Create(ClientProjectVM clientProjectVM)
        {
            string msg = "";
            try
            {
                var model = ClientProject.AddNewClientProjectToElaqatClient(clientProjectVM);
               
                if (model == null)
                {
                    var _model = ClientProject.PrepareClientProjectToBeAdded(clientProjectVM.SelectedElaqatClientId);
                    msg = Resources.Mention.ClientOrProjectDetailsAreMissing;
                    return View(_model);
                }
                
                return RedirectToAction("Details", "ElaqatClient", new { id = model.ElaqatClientId });
            }
            catch(Exception ex)
            {
                var _model = ClientProject.PrepareClientProjectToBeAdded(clientProjectVM.SelectedElaqatClientId);
                msg = Resources.Projects.TechnicalFailure;
                return View(_model);
            }
        }      

        // GET: ClientProject/Edit/5
        public ActionResult Edit(int id)
        {
            string msg = "";
           
            if (id > 0)
            {

                var model = ClientProject.PrepareElaqatClientClientProjectToBeEdit(id);
                if (model == null)
                {
                    msg = Resources.Projects.ProjectIsNotFound;
                    return RedirectToAction("Index", "ClientProject", new { msg });
                }
                return View(model);
            }
            else
            {
                msg = Resources.Mention.ClientOrProjectDetailsAreMissing;
                return RedirectToAction("Index", "ClientProject", new { msg });
            }
        }

        // POST: ClientProject/Edit/5
        [HttpPost]
        public ActionResult Edit(ClientProjectVM clientProjectVM)
        {
            string msg = "";
            try
            {
                var model = ClientProject.EditClientProject(clientProjectVM);
                if (model == null)
                {
                    var _model = ClientProject.PrepareClientProjectToBeAdded(clientProjectVM.SelectedElaqatClientId);
                    msg = Resources.Mention.ClientOrProjectDetailsAreMissing;
                    return View(_model);
                }
                return RedirectToAction("Details", new { id = model.Id });
            }
            catch
            {
                var _model = ClientProject.PrepareElaqatClientClientProjectToBeEdit(clientProjectVM.Id);
                msg = Resources.Projects.TechnicalFailure;
                return View(_model);
            }
        }

       
    }
}
