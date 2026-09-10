using LEAP.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LEAP.Controllers
{
    public class EventTypeController : Controller
    {
        EventTypeModel _EventTypeModel = new EventTypeModel();
        private List<SelectListItem> _EventTypeList;
        public ActionResult Index()
        {
            List<EventTypeModel> List_Event = _EventTypeModel.Get_EventType();
            return View(List_Event);
        }
        [AdminOnly]
        public ActionResult Add()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AdminOnly]
        public ActionResult Add(EventTypeModel _Event)
        {
            if (ModelState.IsValid)
            {
                bool valid = _EventTypeModel.AddEventType(_Event.Title, _Event.Color, User.Identity.Name);
                if (valid)
                {
                    return RedirectToAction("Index", "EventType");
                }
                else
                {
                    return View();
                }
            }
            else
            {
                return View();
            }
        }
        [AdminOnly]
        public ActionResult Update(int id, bool? error)
        {
            List<EventTypeModel> _notes = _EventTypeModel.Get_EventType().ToList();
            _EventTypeList = new List<SelectListItem>();
            foreach (var items in _notes)
            {
                _EventTypeList.Add(new SelectListItem
                {
                    Text = items.Title,
                    Value = items.IDEventType.ToString()
                });
            }
            ViewBag.VB_EventType = _EventTypeList;

            var List_Event = _EventTypeModel.Get_EventType_ByID(id);
            return View(List_Event);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AdminOnly]
        public ActionResult Update(string id, string Title, string Color)
        {
            bool valid = _EventTypeModel.UpdateEventType(id, Title, Color, User.Identity.Name);
            if (valid)
            {
                return RedirectToAction("Index", "EventType");
            }
            else
            {
                return View();
            }
        }

        [AdminOnly]
        public ActionResult Delete(int id)
        {
            var List_Event = _EventTypeModel.Get_EventType_ByID(id);
            return View(List_Event);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AdminOnly]
        public ActionResult Delete(string id)
        {
            // Antes esto primero borraba "events/" + id (un Event cuyo ID
            // coincidiera con el del EventType, sin relacion real entre ambos —
            // podia borrar un Event de otro registro por pura coincidencia de
            // IDs) antes de intentar borrar el EventType. No existe FK entre
            // events.IDType y event_types.IDEventType en la BD (es un int suelto,
            // ver database/migrations en leap_api), asi que ese paso no era
            // necesario para poder borrar el EventType.
            var valid = _EventTypeModel.DeleteEventType(id, User.Identity.Name);
            if (valid)
            {
                return RedirectToAction("Index", "EventType");
            }
            else
            {
                return View();
            }
        }
    }
}