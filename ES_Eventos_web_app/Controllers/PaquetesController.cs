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
    public class PaquetesController : Controller
    {
        private esEventosEntities db = new esEventosEntities();

        private int ID { get; set; }

        // GET: Paquetes
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
                    pId = p.id,
                    pNombre = p.nombre,
                    pPrecio = p.precio,
                    pDescripcion = p.descripcion,
                    rId = r.id,
                    rNombre = r.nombre,
                    rCorreo = r.correo,
                    rTelefono = r.telefono,
                    rUbicacion = r.ubicacion,
                    rEstado = r.estado,
                    rTipo = r.idTipoRecurso
                };
            return View(paquete.ToList());
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
                db.Cliente.Find(1) /*aqui va el idCliente */
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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CrearReservacion([Bind(Include = "id,fecha,idCliente,idPaquete")] Reservacion reservacion)
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
