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
    public class RecursoXPaquetesController : Controller
    {
        private esEventosEntities db = new esEventosEntities();

        // GET: RecursoXPaquetes
        public ActionResult Index()
        {
            var recursoXPaquete = db.RecursoXPaquete.Include(r => r.Paquete).Include(r => r.Recurso);
            return View(recursoXPaquete.ToList());
        }

        // GET: RecursoXPaquetes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RecursoXPaquete recursoXPaquete = db.RecursoXPaquete.Find(id);
            if (recursoXPaquete == null)
            {
                return HttpNotFound();
            }
            return View(recursoXPaquete);
        }

        // GET: RecursoXPaquetes/Create
        public ActionResult Create(int idR)
        {
            ViewBag.id = idR;
            List<object> list = new List<object>
            {
                db.Recurso.Find(idR)
            };
            IEnumerable<object> en = list;
            ViewBag.idPaquete = new SelectList(db.Paquete, "id", "nombre");
            ViewBag.idRecurso = new SelectList(en, "id", "nombre");
            return View();
        }

        // POST: RecursoXPaquetes/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,idPaquete,idRecurso")] RecursoXPaquete recursoXPaquete)
        {
            if (ModelState.IsValid)
            {
                db.RecursoXPaquete.Add(recursoXPaquete);
                db.SaveChanges();
                return RedirectToAction("Index", "AdminRecursos");
            }

            ViewBag.idPaquete = new SelectList(db.Paquete, "id", "nombre", recursoXPaquete.idPaquete);
            ViewBag.idRecurso = new SelectList(db.Recurso, "id", "nombre", recursoXPaquete.idRecurso);
            return View(recursoXPaquete);
        }
        
        // GET: RecursoXPaquetes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RecursoXPaquete recursoXPaquete = db.RecursoXPaquete.Find(id);
            if (recursoXPaquete == null)
            {
                return HttpNotFound();
            }
            ViewBag.idPaquete = new SelectList(db.Paquete, "id", "nombre", recursoXPaquete.idPaquete);
            ViewBag.idRecurso = new SelectList(db.Recurso, "id", "nombre", recursoXPaquete.idRecurso);
            return View(recursoXPaquete);
        }

        // POST: RecursoXPaquetes/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,idPaquete,idRecurso")] RecursoXPaquete recursoXPaquete)
        {
            if (ModelState.IsValid)
            {
                db.Entry(recursoXPaquete).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.idPaquete = new SelectList(db.Paquete, "id", "nombre", recursoXPaquete.idPaquete);
            ViewBag.idRecurso = new SelectList(db.Recurso, "id", "nombre", recursoXPaquete.idRecurso);
            return View(recursoXPaquete);
        }

        // GET: RecursoXPaquetes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RecursoXPaquete recursoXPaquete = db.RecursoXPaquete.Find(id);
            if (recursoXPaquete == null)
            {
                return HttpNotFound();
            }
            return View(recursoXPaquete);
        }

        // POST: RecursoXPaquetes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            RecursoXPaquete recursoXPaquete = db.RecursoXPaquete.Find(id);
            db.RecursoXPaquete.Remove(recursoXPaquete);
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
