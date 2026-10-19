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
    public class ClientProjectCategoryController : Controller
    {
        // GET: ClientProjectCategory
        public ActionResult Index()
        {
            var models = ClientProjectCategory.GetAllClientProjectCategory();
            return View(models);
        }

        // GET: ClientProjectCategory/Details/5
        public ActionResult Details(int id)
        {
            var model = ClientProjectCategory.GetClientProjectCategoryDetails(id);
            return View(model);
        }

        // GET: ClientProjectCategory/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ClientProjectCategory/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }


        // GET: ClientProjectCategory/Edit/5
        public ActionResult Edit(int id)
        {
            var model = ClientProjectCategory.PrepareClientProjectCategoryToBeEdit(id);
            return View(model);
        }

        // POST: ClientProjectCategory/Edit/5
        [HttpPost]
        public ActionResult Edit(ClientProjectCategoryVM clientProjectCategoryVM)
        {
            try
            {
                var model = ClientProjectCategory.EditClientProjectCategory(clientProjectCategoryVM);

                return RedirectToAction("Details", new { id = model.Id });
            }
            catch
            {
                return View();
            }
        }




        // GET: ClientProjectSubCategory/Edit/5
        public ActionResult EditSubCategory(int id)
        {
            var model = ClientProjectCategory.PrepareClientProjectSubCategoryVMToBeEdit(id);
            return View(model);
        }

        // POST: ClientProjectSubCategory/Edit/5
        [HttpPost]
        public ActionResult EditSubCategory(ClientProjectSubCategoryVM clientProjectSubCategoryVM)
        {
            try
            {
                var model = ClientProjectCategory.EditClientProjectSubCategory(clientProjectSubCategoryVM);

                return RedirectToAction("Details", new { id = model.Id });
            }
            catch
            {
                return View();
            }
        }

        // GET: ClientProjectCategory/Delete/5
        public ActionResult Delete(int id)
        {
            var model = ClientProjectCategory.GetClientProjectCategoryDetails(id);
            return View(model);
        }

        // POST: ClientProjectCategory/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                var model = ClientProjectCategory.DeleteClientProjectCategory(id);

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }


        // GET: ClientProjectSubCategory/Delete/5
        public ActionResult DeleteSubCategory(int id)
        {
            var model = ClientProjectCategory.GetClientProjectSubCategoryDetails(id);
            return View(model);
        }

        // POST: ClientProjectCategory/Delete/5
        [HttpPost]
        public ActionResult DeleteSubCategory(int id, FormCollection collection)
        {
            try
            {
                var model = ClientProjectCategory.DeleteClientProjectSubCategory(id);

                return RedirectToAction("Details", new { id = model.Id });
            }
            catch
            {
                return View();
            }
        }
    }
}
