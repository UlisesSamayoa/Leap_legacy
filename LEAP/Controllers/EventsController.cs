using LEAP.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LEAP.Controllers
{
    public class EventsController : Controller
    {
        EventsModel _EventModel = new EventsModel();
        EventTypeModel _EventTypeModel = new EventTypeModel();
        private List<SelectListItem> _EventTypeList;
        public ActionResult Index()
        {
            List<EventsModel> List_Event = _EventModel.Get_Events();
            return View(List_Event);
        }
        [AdminOnly]
        public ActionResult Add()
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
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AdminOnly]
        public ActionResult Add(EventsModel _Event)
        {
            if (ModelState.IsValid)
            {
                bool valid = _EventModel.AddEvent(_Event.Title,_Event.DateEvent,_Event.Description,_Event.IDType, User.Identity.Name);
                if (valid)
                {
                    return RedirectToAction("Index", "Events");
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

            var List_Event = _EventModel.Get_Events_ByID(id);
            return View(List_Event);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AdminOnly]
        public ActionResult Update(string id, string Title, DateTime DateEvent, string Description, int IDType)
        {
            bool valid = _EventModel.UpdateEvent(id,Title,DateEvent,Description,IDType, User.Identity.Name);
            if (valid)
            {
                return RedirectToAction("Index", "Events");
            }
            else
            {
                return View();
            }
        }

        [AdminOnly]
        public ActionResult Delete(int id)
        {
            var List_Event = _EventModel.Get_Events_ByID(id);
            return View(List_Event);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AdminOnly]
        public ActionResult Delete(string id)
        {
            var valid = _EventModel.DeleteEvent(id, User.Identity.Name);
            if (valid)
            {
                return RedirectToAction("Index", "Events");
            }
            else
            {
                return View();
            }
        }

 

    }
}