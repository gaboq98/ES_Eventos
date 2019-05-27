using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebApplication1.Controllers
{
    public class RecursoController : Controller
    {
        // GET: Recurso
        public ActionResult Index()
        {
            ViewData["Test"] = 100;
            return View();
        }

        // GET: Recurso/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Recurso/Create
        public ActionResult Create()
        {
            return View("../Home/Index");
        }

        // POST: Recurso/Create
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

        // GET: Recurso/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Recurso/Edit/5
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

        // GET: Recurso/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Recurso/Delete/5
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
