using LEAP.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LEAP.Controllers
{
    public class NotesxMaestroController : Controller
    {
        NotesxMaestroModel _NotesConsumerModel = new NotesxMaestroModel();
        public ActionResult Index()
        {
            List<NotesxMaestroModel> List_NotesC = _NotesConsumerModel.Get_NotesxMaestro();
            return View(List_NotesC);
        }
        public ActionResult Details(int id)
        {
            var List_NotesC = _NotesConsumerModel.Get_NotesxMaestro_ByID(id);
            return View(List_NotesC);
        }
        public JsonResult Notes_byConsumer(int id)
        {

            List<NotesxMaestroModel> productos = _NotesConsumerModel.Get_NotesxMaestro_ByIDList(id);

            return Json(productos, JsonRequestBehavior.AllowGet);
        }
        // Creacion retirada: una visita nueva solo se crea desde leap_client
        // (captura llegada/salida exactas y firma, que este modulo no reproduce).
        // Aqui solo queda Ver/Editar/Borrar.
        public ActionResult Update(int id, int type, bool? r, bool? error)
        {
            var List_NotesC = _NotesConsumerModel.Get_NotesxMaestro_ByID(id);
            if (r == true)
            {
                ViewBag.VB_error = "1";
            }
            List_NotesC.type = type;
            return View(List_NotesC);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Update(string id, DateTime SessionStartedAt, DateTime SessionEndedAt, string PresentInSession, string TimesxWeek, string Notes, string Signature, int type)
        {
            bool valid = _NotesConsumerModel.UpdateNotesxMaestro(id, SessionStartedAt, SessionEndedAt, PresentInSession, TimesxWeek, Notes, Signature, User.Identity.Name);
            if (valid)
            {
                if (type == 0)
                {
                    return RedirectToAction("Index", "NotesxMaestro");
                }
                else
                {
                    return RedirectToAction("Update", "NotesxMaestro", new { id = id, type = type, r = true });
                }

            }
            else
            {
                return View();
            }

        }
        public ActionResult Delete(int id, int type, bool? r)
        {
            var List_NotesC = _NotesConsumerModel.Get_NotesxMaestro_ByID(id);
            List_NotesC.type = type;
            if (r == true)
            {
                ViewBag.VB_error = "1";
            }
            return View(List_NotesC);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(string id, int type)
        {
            var valid = _NotesConsumerModel.DeleteNotesxMaestro(id, User.Identity.Name);
            if (valid)
            {
                if (type == 0)
                {
                    return RedirectToAction("Index", "NotesxMaestro");
                }
                else
                {
                    return RedirectToAction("Delete", "NotesxMaestro", new { id = id, type = type, r = true });
                }
            }
            else
            {
                return View();
            }
        }


    }
}