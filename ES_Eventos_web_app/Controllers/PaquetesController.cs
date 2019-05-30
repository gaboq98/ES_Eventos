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
    public class PaquetesController : Controller
    {
        private esEventosEntities db = new esEventosEntities();

        private int ID { get; set; }

        // GET: Paquetes
        public ActionResult Index()
        {
            ViewBag.ID = 1;
            return View(db.Paquete.ToList());
        }

        // GET: Paquetes/Details/5
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

        [HttpPost]
        public ActionResult Details(int idP)
        {
            return RedirectToAction("CrearReservacion", "Paquetes", new { idP = idP});
        }

        public ActionResult CrearReservacion(int idP)
        {
            List<object> list = new List<object>
            {
                db.Cliente.Find(1)
            };
            IEnumerable<object> en = list;
            ViewBag.idCliente = new SelectList(en, "id", "nombre");

            List<object> list2 = new List<object>
            {
                db.Paquete.Find(idP)
            };
            IEnumerable<object> en2 = list2;
            ViewBag.idPaquete = new SelectList(en2, "id", "nombre");

            return View();
        }

        /*
        [HttpPost]
        public ActionResult CrearReservacion()
        {
            if (paquete == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            return RedirectToAction("Create", "Reservaciones", new { id = 1, paquete=paquete});
        }
        */
    }
}
