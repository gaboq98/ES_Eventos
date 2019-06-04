using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;
using ES_Eventos_web_app.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;

namespace ES_Eventos_web_app.Controllers
{
    public class ReservacionesController : Controller
    {
        private esEventosEntities db = new esEventosEntities();
        private int clientID { get; set; }

        // GET: Reservaciones
        public ActionResult Index(int? id)
        {
            var reservacion = db.Reservacion.Include(r => r.Cliente).Include(r => r.Paquete);
            var idRes = reservacion.Where(r => r.idCliente == id);
            return View(idRes.ToList());
        }

        // GET: Reservaciones/Details/5
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

        // GET: Reservaciones/Create
        public ActionResult Create(int? id, int? paquete)
        {
            if (id != null)
            {
                List<object> list = new List<object>();
                list.Add(db.Cliente.Find(id));
                IEnumerable<object> en = list;
                ViewBag.idCliente = new SelectList(en, "id", "nombre");
            } else
            {
                ViewBag.idCliente= new SelectList(db.Cliente, "id", "nombre");
            }
            if (paquete != null)
            {
                List<object> list2 = new List<object>();
                list2.Add(db.Paquete.Find(paquete));
                IEnumerable<object> en2 = list2;
                ViewBag.idPaquete = new SelectList(en2, "id", "nombre");
            } else
            {
                ViewBag.idPaquete = new SelectList(db.Paquete, "id", "nombre");
            }
            return View();
        }

        // POST: Reservaciones/Create
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
                // Send email
                var paq = db.Paquete.Find(reservacion.idPaquete);
                SendMail("Creación de reserva", "Se creaó con exito la reservación del paquete " + paq.nombre + ", el día " + DateTime.Now.ToString("dd MMMM yyyy HH:mm:ss"));

                return RedirectToAction("Index");
            }

            ViewBag.idCliente = new SelectList(db.Cliente, "id", "nombre", reservacion.idCliente);
            ViewBag.idPaquete = new SelectList(db.Paquete, "id", "nombre", reservacion.idPaquete);
            return View(reservacion);
        }

        // GET: Reservaciones/Edit/5
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

        // POST: Reservaciones/Edit/5
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
                // Send email
                var paq = db.Paquete.Find(reservacion.idPaquete);
                SendMail("Modificación de reserva", "Se modificó con exito la reservación del paquete " + paq.nombre + ", el día " + DateTime.Now.ToString("dd MMMM yyyy HH:mm:ss"));
                return RedirectToAction("Index");
            }
            ViewBag.idCliente = new SelectList(db.Cliente, "id", "nombre", reservacion.idCliente);
            ViewBag.idPaquete = new SelectList(db.Paquete, "id", "nombre", reservacion.idPaquete);
            return View(reservacion);
        }


        // GET: Reservaciones/Delete/5
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

        // POST: Reservaciones/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Reservacion reservacion = db.Reservacion.Find(id);
            db.Reservacion.Remove(reservacion);
            db.SaveChanges();
            // Send email
            var paq = db.Paquete.Find(reservacion.idPaquete);
            SendMail("Eliminación de reserva", "Se eliminó con exito la reservación del paquete " + paq.nombre + ", el día " + DateTime.Now.ToString("dd MMMM yyyy HH:mm:ss"));
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

        // GET: Reservaciones
        public ActionResult Historial()
        {
            var reservacion = db.Reservacion.Include(r => r.Cliente).Include(r => r.Paquete);
            var idRes = reservacion.Where(r => r.idCliente == 1); // aqui va el idCliente
            return View(idRes.ToList());
        }

        private void SendMail(string subject, string body)
        {
            MailMessage mail = new MailMessage();
            SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");
            mail.From = new MailAddress("eseventos420@gmail.com");
            ApplicationUser user = System.Web.HttpContext.Current.GetOwinContext()
            .GetUserManager<ApplicationUserManager>()
            .FindById(System.Web.HttpContext.Current.User.Identity.GetUserId());
            mail.To.Add(user.Email);
            mail.Subject = subject;
            mail.Body = body;
            SmtpServer.Port = 587;
            SmtpServer.Credentials = new System.Net.NetworkCredential("eseventos420@gmail.com", "reque.420");
            SmtpServer.EnableSsl = true;
            SmtpServer.Send(mail);
        }

    }
}
