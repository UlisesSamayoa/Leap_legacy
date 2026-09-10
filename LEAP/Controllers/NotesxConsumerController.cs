using LEAP.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LEAP.Controllers
{
    [NotCdsOnly]
    public class NotesxConsumerController : Controller
    {
        NotesxConsumerModel _NotesConsumerModel = new NotesxConsumerModel();
        ConsumerModel _ConsumerModel = new ConsumerModel();
        private readonly ConsumerModel _CosumerPModel;
        private List<SelectListItem> _NotesList;
        public ActionResult Index()
        {
            List<NotesxConsumerModel> List_NotesC = _NotesConsumerModel.Get_NotesxConsumer().OrderByDescending(a=>a.DateC).ToList();
           
            return View(List_NotesC);
        }
        public ActionResult Details(int id)
        {
            var List_NotesC = _NotesConsumerModel.Get_NotesxConsumer_ByID(id);
            return View(List_NotesC);
        }
        public JsonResult Notes_byConsumer(int id)
        {

            List<NotesxConsumerModel> productos = _NotesConsumerModel.Get_NotesxConsumer_ByIDList(id);

            return Json(productos, JsonRequestBehavior.AllowGet);
        }
        [AdminOnly]
        public ActionResult Add(bool? error)
        {
            if (error == true)
            {
                ViewBag.VB_error = "¡Error, this Consumer already has an active note!";
            }


                List<ConsumerModel> _notes = _ConsumerModel.Get_Consumer().ToList();
                _NotesList = new List<SelectListItem>();
                foreach (var items in _notes)
                {
                    _NotesList.Add(new SelectListItem
                    {
                        Text = items.Name,
                        Value = items.UCI
                    });
                }
                ViewBag.VB_Consumer = _NotesList;
                return View();
            
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AdminOnly]
        public ActionResult Add(NotesxConsumerModel _NotesC)
        {
            if (ModelState.IsValid)
            {
                bool validActive = _NotesConsumerModel.ValidarNotesConsumerActive(0,_NotesC.UCI,_NotesC.Active, User.Identity.Name);
                if (validActive)
                {
                    return RedirectToAction("Add", "NotesxConsumer", new { error = true });
                }
                else
                {
                    bool valid = _NotesConsumerModel.AddNotesxConsumer(_NotesC.UCI, _NotesC.Notes, _NotesC.Active, User.Identity.Name);
                    if (valid)
                    {
                        return RedirectToAction("Index", "NotesxConsumer");
                    }
                    else
                    {
                        return View();
                    }
                }
               
            }
            else
            {
                return View();
            }
        }
        [HttpPost]
        [AdminOnly]
        public string AddFromConsumer(string _uci,bool _ConsumerActive, string _ConsumerNotes)
        {
            if (ModelState.IsValid)
            {
                bool validActive = _NotesConsumerModel.ValidarNotesConsumerActive(0, _uci, _ConsumerActive, User.Identity.Name);
                if (validActive)
                {
                    return "2";
                }
                else
                {
                    bool valid = _NotesConsumerModel.AddNotesxConsumer(_uci, _ConsumerNotes, _ConsumerActive, User.Identity.Name);
                    if (valid)
                    {
                        return "1";
                    }
                    else
                    {
                        return "0";
                    }
                }

            }
            else
            {
                return "0";
            }
        }
        [AdminOnly]
        public ActionResult Update(int id,int type, bool? r, bool? error)
        {
            if (error == true)
            {
                ViewBag.VB_error = "¡Error, this Consumer already has an active note!";
            }
            var List_NotesC = _NotesConsumerModel.Get_NotesxConsumer_ByID(id);
            //List<ConsumerModel> _notes = _ConsumerModel.Get_Consumer().ToList();
            //_NotesList = new List<SelectListItem>();
            //foreach (var items in _notes)
            //{
            //    _NotesList.Add(new SelectListItem
            //    {
            //        Text = items.Name,
            //        Value = items.UCI
            //    });
            //}
            //ViewBag.VB_Consumer = _NotesList;
            if (r==true)
            {
                ViewBag.VB_error = "1";
            }
            List_NotesC.type = type;
            return View(List_NotesC);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AdminOnly]
        public ActionResult Update(string id, string UCI, string Notes, bool Active, int type)
        {
            bool validActive = _NotesConsumerModel.ValidarNotesConsumerActive(Convert.ToInt32(id),UCI, Active, User.Identity.Name);
            if (validActive)
            {
                return RedirectToAction("Update", "NotesxConsumer", new {id = id ,type = type,error = true });
            }
            else
            {
            var valid = _NotesConsumerModel.UpdateNotesxConsumer(id, UCI, Notes,Active, User.Identity.Name);
            if (valid)
            {
                    if (type == 0)
                    {
                        return RedirectToAction("Index", "NotesxConsumer");
                    }
                    else
                    {
                        return RedirectToAction("Update", "NotesxConsumer", new { id = id, type = type, r = true });
                    }
                
            }
            else
            {
                return View();
            }
            }
        }
        [AdminOnly]
        public ActionResult Delete(int id, int type, bool? r)
        {
            var List_NotesC = _NotesConsumerModel.Get_NotesxConsumer_ByID(id);
            List_NotesC.type = type;
            if (r == true)
            {
                ViewBag.VB_error = "1";
            }
            return View(List_NotesC);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AdminOnly]
        public ActionResult Delete(string id, int type)
        {
            var valid = _NotesConsumerModel.DeleteNotesxConsumer(id, User.Identity.Name);
            if (valid)
            {
                if (type == 0)
                {
                    return RedirectToAction("Index", "NotesxConsumer");
                }
                else
                {
                    return RedirectToAction("Delete", "NotesxConsumer", new { id = id, type = type,r = true });
                }
            }
            else
            {
                return View();
            }
        }



    }
}