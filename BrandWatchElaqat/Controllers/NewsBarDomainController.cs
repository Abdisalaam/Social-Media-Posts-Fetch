using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BrandWatchElaqat.Models;
using BrandWatchElaqat.ViewModel;
namespace BrandWatchElaqat.Controllers
{
    [Authorize(Roles = "Employee,Admin")]
    public class NewsBarDomainController : Controller
    {
        // GET: NewsBarFilter
        public ActionResult Index(string msg)
        {
            ViewBag.Message = msg;
            var newsBarFilterList = NewsBarDomain.GetAllNewsBarFilterForProject();
            return View(newsBarFilterList);
        }

        // GET: NewsBarFilter/Details/5
        public ActionResult Details(string msg, int id)
        {
            ViewBag.Message = msg;
            if (id > 0)
            {
                var newsBarFilter = NewsBarDomain.GetNewsBarFilterById(id);
                if (newsBarFilter != null)
                {
                    return View(newsBarFilter);
                }
                else
                {
                    msg = Resources.NewsBarPage.NewsBarIsNotFound;
                    return RedirectToAction("Index", new { msg, });
                }
            }
            else
            {
                msg = Resources.NewsBarPage.NewsBarIsNotFound;
                return RedirectToAction("Index", new { msg, });
            }
        }

        // GET: NewsBarFilter/Create
        public ActionResult Create()
        {
            NewsBarFilterVM newsBarFilterVM = new NewsBarFilterVM();
            return View(newsBarFilterVM);

        }

       // POST: NewsBarFilter/Create
       [HttpPost]
        public ActionResult Create(NewsBarFilterVM newsBarFilterVM)
        {          
           
            try
            {
                var model = NewsBarDomain.AddNewsBarFilter(newsBarFilterVM);               
                return RedirectToAction("Details", new {msg = Resources.NewsBarPage.DomainIsSuccessfullyCreated, id = model });
            }
            catch
            {
                ViewBag.Message = Resources.NewsBarPage.TechnicalError;               
                return View(newsBarFilterVM);
            }
        }

        // GET: NewsBarFilter/Edit/5
        public ActionResult Edit(int id)
        {
            var model = NewsBarDomain.PrepareNewsBarFilterVMToBeEdit(id);
            return View(model);
        }

        // POST: NewsBarFilter/Edit/5
        [HttpPost]
        public ActionResult Edit(NewsBarFilterVM newsBarFilterVM)
        {
            try
            {
               
                var model = NewsBarDomain.UpdateNewsBarFilter(newsBarFilterVM);

                if (model > 0)
                {
                    return RedirectToAction("Details", new { msg = Resources.NewsBarPage.DomainIsSuccessfullyUpdated, id = model });
                }
                string msg = Resources.NewsBarPage.TechnicalError;
                return RedirectToAction("Index", new { msg });
            }
            catch
            {
                return View();
            }
        }

        // GET: NewsBarFilter/Delete/5
        public ActionResult Delete(int id)
        {
            string msg = "";
            ViewBag.Message = Resources.Projects.ThisDeedIsIrreversible;
            if (id > 0)
            {
                var newsBarFilter = NewsBarDomain.GetNewsBarFilterById(id);
                if (newsBarFilter != null)
                {
                    return View(newsBarFilter);
                }
                else
                {
                    msg = Resources.NewsBarPage.NewsBarIsNotFound;
                    return RedirectToAction("Index", "ClientProject", new { msg, });
                }
            }
            else
            {
                msg = Resources.NewsBarPage.NewsBarIsNotFound;
                return RedirectToAction("Index", "ClientProject", new { msg, });
            }
        }

        // POST: NewsBarFilter/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            string msg = "";
            ViewBag.Message = msg;
            try
            {
               

                    var model = NewsBarDomain.DeleteNewsBarFilter(id);


                    if (model)
                    {
                        msg = Resources.NewsBarPage.NewsBarIsSuccessFullyDeleted;
                        return RedirectToAction("Index", new { msg, id = model });
                    }
                    else
                    {
                        msg = Resources.NewsBarPage.NewsBarIsNotFound;
                        return RedirectToAction("Index", "ClientProject", new { msg, });
                    }

               
               

            }
            catch
            {
                msg = Resources.NewsBarPage.TechnicalError;
                return RedirectToAction("Index", new { msg, });
            }
        }

        public ActionResult DeleteAllProjectNewsBars()
        {
            string msg = "";
            ViewBag.Message = Resources.Projects.ThisDeedIsIrreversible;
            var newsBarFilters = NewsBarDomain.GetAllNewsBarFilterForProject();
            if (newsBarFilters != null)
            {
                return View(newsBarFilters);
            }
            else
            {
                msg = Resources.NewsBarPage.NewsBarIsNotFound;
                return RedirectToAction("Index", new { msg, });
            }
        }

        // POST: NewsBarFilter/Delete/5
        [HttpPost]
        public ActionResult DeleteAllProjectNewsBars(FormCollection collection)
        {
            string msg = "";
            ViewBag.Message = Resources.Projects.ThisDeedIsIrreversible;
            try
            {

                var model = NewsBarDomain.DeleteAllNewsBarFilterFromMentionFilter();
                if (model)
                {
                    msg = Resources.NewsBarPage.AllNewsBarAreSuccessFullyDeleted;
                    return RedirectToAction("Index", new { msg, model });

                }
                else
                {
                    msg = Resources.NewsBarPage.TechnicalError;
                    return RedirectToAction("Index", new { msg, });
                }

            }
            catch
            {

                msg = Resources.NewsBarPage.TechnicalError;
                return RedirectToAction("Index", new { msg, });
            }
        }
    }
}
