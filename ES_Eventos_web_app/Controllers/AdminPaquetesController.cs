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
    public class AdminPaquetesController : Controller
    {
        private esEventosEntities db = new esEventosEntities();

        // GET: AdminPaquetes
        public ActionResult Index()
        {
            ViewBag.recursos = db.TipoRecurso.ToList();
            var paquete = //db.Paquete.Include(p => p.RecursoXPaquete);
                from p in db.Paquete
                from rxp in db.RecursoXPaquete
                from r in db.Recurso
                where p.id == rxp.idPaquete
                where rxp.idRecurso == r.id
                select new PaqueteHolder
                {
                    id = p.id,
                    nombre = p.nombre,
                    precio = p.precio,
                    descripcion = p.descripcion,
                    idTipoRecurso = r.idTipoRecurso
                };
            return View(paquete.ToList());
        }

        // GET: AdminPaquetes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Paquete paquete = db.Paquete.Find(id);
            if (paquete == null)
            {
                return HttpNotFound();
            }
            return View(paquete);
        }

        // GET: AdminPaquetes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: AdminPaquetes/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,nombre,precio,descripcion")] Paquete paquete)
        {
            if (ModelState.IsValid)
            {
                db.Paquete.Add(paquete);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(paquete);
        }

        // GET: AdminPaquetes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Paquete paquete = db.Paquete.Find(id);
            if (paquete == null)
            {
                return HttpNotFound();
            }
            return View(paquete);
        }

        // POST: AdminPaquetes/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,nombre,precio,descripcion")] Paquete paquete)
        {
            if (ModelState.IsValid)
            {
                db.Entry(paquete).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(paquete);
        }

        // GET: AdminPaquetes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Paquete paquete = db.Paquete.Find(id);
            if (paquete == null)
            {
                return HttpNotFound();
            }
            return View(paquete);
        }

        // POST: AdminPaquetes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Paquete paquete = db.Paquete.Find(id);
            db.Paquete.Remove(paquete);
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
