using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ES_Eventos_web_app.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult OpenReservaciones()
        {
            return RedirectToAction("Index", "Reservaciones", new { id = 1 });
        }

    }
}