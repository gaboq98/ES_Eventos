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
    public class AdminReservacionesController : Controller
    {
        private esEventosEntities db = new esEventosEntities();

        // GET: AdminReservaciones
        public ActionResult Index()
        {
            var reservacion = db.Reservacion.Include(r => r.Cliente).Include(r => r.Paquete);
            return View(reservacion.ToList());
        }

        // GET: AdminReservaciones/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Reservacion reservacion = db.Reservacion.Find(id);
            if (reservacion == null)
            {
                return HttpNotFound();
            }
            return View(reservacion);
        }

        // GET: AdminReservaciones/Create
        public ActionResult Create()
        {
            ViewBag.idCliente = new SelectList(db.Cliente, "id", "nombre");
            ViewBag.idPaquete = new SelectList(db.Paquete, "id", "nombre");
            return View();
        }

        // POST: AdminReservaciones/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,fecha,idCliente,idPaquete")] Reservacion reservacion)
        {
            if (ModelState.IsValid)
            {
                db.Reservacion.Add(reservacion);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.idCliente = new SelectList(db.Cliente, "id", "nombre", reservacion.idCliente);
            ViewBag.idPaquete = new SelectList(db.Paquete, "id", "nombre", reservacion.idPaquete);
            return View(reservacion);
        }

        // GET: AdminReservaciones/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Reservacion reservacion = db.Reservacion.Find(id);
            if (reservacion == null)
            {
                return HttpNotFound();
            }
            ViewBag.idCliente = new SelectList(db.Cliente, "id", "nombre", reservacion.idCliente);
            ViewBag.idPaquete = new SelectList(db.Paquete, "id", "nombre", reservacion.idPaquete);
            return View(reservacion);
        }

        // POST: AdminReservaciones/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,fecha,idCliente,idPaquete")] Reservacion reservacion)
        {
            if (ModelState.IsValid)
            {
                db.Entry(reservacion).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.idCliente = new SelectList(db.Cliente, "id", "nombre", reservacion.idCliente);
            ViewBag.idPaquete = new SelectList(db.Paquete, "id", "nombre", reservacion.idPaquete);
            return View(reservacion);
        }

        // GET: AdminReservaciones/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Reservacion reservacion = db.Reservacion.Find(id);
            if (reservacion == null)
            {
                return HttpNotFound();
            }
            return View(reservacion);
        }

        // POST: AdminReservaciones/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Reservacion reservacion = db.Reservacion.Find(id);
            db.Reservacion.Remove(reservacion);
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
