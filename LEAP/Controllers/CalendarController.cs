using LEAP.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LEAP.Controllers
{
    public class CalendarController : Controller
    {
        EventsModel _EventModel = new EventsModel();
        CalendarModel _CalendarModel = new CalendarModel();
        public ActionResult Index()
        {
            return View();
        }
        public JsonResult ObtenerEventos()
        {
            var eventos = _CalendarModel.Get_Events();
            return Json(eventos, JsonRequestBehavior.AllowGet);
        }
    }
}