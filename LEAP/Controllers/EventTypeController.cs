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
        EventsModel _EventModel = new EventsModel();
        EventTypeModel _EventTypeModel = new EventTypeModel();
        private List<SelectListItem> _EventTypeList;
        public ActionResult Index()
        {
            List<EventTypeModel> List_Event = _EventTypeModel.Get_EventType();
            return View(List_Event);
        }
        public ActionResult Add()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
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

        public ActionResult Delete(int id)
        {
            var List_Event = _EventTypeModel.Get_EventType_ByID(id);
            return View(List_Event);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(string id)
        {
            bool validDelete = _EventModel.DeleteEventxEvenType(id, User.Identity.Name);
            if (validDelete)
            {
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
            else
            {
                return View();
            }
            
        }
    }
}