using BrandWatchElaqat.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BrandWatchElaqat.Controllers
{
    [Authorize(Roles = "Employee,Admin")]
    public class ClientProjectQuerieController : Controller
    {
        // GET: ClientProjectQuerie
        public ActionResult Index()
        {
            var models = ClientProjectQuerie.GetAllClientProjectQuerie();
            return View(models);
        }

        // GET: ClientProjectQuerie/Details/5
        public ActionResult Details(int id)
        {
            var model = ClientProjectQuerie.GetClientProjectQuerieDetails(id);
            return View(model);
        }

        // GET: ClientProjectQuerie/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ClientProjectQuerie/Create
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

        // GET: ClientProjectQuerie/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: ClientProjectQuerie/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: ClientProjectQuerie/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: ClientProjectQuerie/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
