using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ES_Eventos_web_app.Models;

namespace ES_Eventos_web_app.Controllers
{
    public class AdminRecursosController : Controller
    {
        private esEventosEntities db = new esEventosEntities();

        // GET: AdminResursos
        public ActionResult Index()
        {
            var recurso = db.Recurso.Include(r => r.TipoRecurso);
            return View(recurso.ToList());
        }

        // GET: AdminResursos/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Recurso recurso = db.Recurso.Find(id);
            if (recurso == null)
            {
                return HttpNotFound();
            }
            return View(recurso);
        }

        // GET: AdminResursos/Create
        public ActionResult Create()
        {
            ViewBag.idTipoRecurso = new SelectList(db.TipoRecurso, "id", "descripcion");
            return View();
        }

        // POST: AdminResursos/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,nombre,correo,telefono,ubicacion,estado,idTipoRecurso")] Recurso recurso)
        {
            if (ModelState.IsValid)
            {
                db.Recurso.Add(recurso);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.idTipoRecurso = new SelectList(db.TipoRecurso, "id", "descripcion", recurso.idTipoRecurso);
            return View(recurso);
        }

        // GET: AdminResursos/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Recurso recurso = db.Recurso.Find(id);
            if (recurso == null)
            {
                return HttpNotFound();
            }
            ViewBag.idTipoRecurso = new SelectList(db.TipoRecurso, "id", "descripcion", recurso.idTipoRecurso);
            return View(recurso);
        }

        // POST: AdminResursos/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,nombre,correo,telefono,ubicacion,estado,idTipoRecurso")] Recurso recurso)
        {
            if (ModelState.IsValid)
            {
                db.Entry(recurso).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.idTipoRecurso = new SelectList(db.TipoRecurso, "id", "descripcion", recurso.idTipoRecurso);
            return View(recurso);
        }

        // GET: AdminResursos/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Recurso recurso = db.Recurso.Find(id);
            if (recurso == null)
            {
                return HttpNotFound();
            }
            return View(recurso);
        }

        // POST: AdminResursos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Recurso recurso = db.Recurso.Find(id);
            db.Recurso.Remove(recurso);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

    }
}
