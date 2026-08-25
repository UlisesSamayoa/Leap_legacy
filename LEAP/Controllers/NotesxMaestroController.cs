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
        ConsumerModel _ConsumerModel = new ConsumerModel();
        RegionalCenterModel _RegionalModel = new RegionalCenterModel();
        SpecialistModel _SpecialistModel = new SpecialistModel();
        //private readonly ConsumerModel _CosumerPModel;
        private List<SelectListItem> _NotesList;
        private List<SelectListItem> _RegionalList;
        private List<SelectListItem> _SpecialistList;
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
        public ActionResult Add()
        {
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

            //List<RegionalCenterModel> _Regional = _RegionalModel.Get_RegionalCenter().ToList();
            //_RegionalList = new List<SelectListItem>();
            //foreach (var items in _Regional)
            //{
            //    _RegionalList.Add(new SelectListItem
            //    {
            //        Text = items.RegionalCenter,
            //        Value = items.IDRegionalCenter.ToString()
            //    });
            //}
            //ViewBag.VB_RegionalCenter = _RegionalList;

            List<SpecialistModel> _Specialist = _SpecialistModel.Get_Specialist().ToList();
            _SpecialistList = new List<SelectListItem>();
            foreach (var items in _Specialist)
            {
                _SpecialistList.Add(new SelectListItem
                {
                    Text = items.Name + " " + items.LastName + " - " + items.Specialty,
                    Value = items.IDSpecialist.ToString()
                }) ;
            }
            ViewBag.VB_Specialist = _SpecialistList;
            return View();

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Add(NotesxMaestroModel _NotesC)
        {
            if (ModelState.IsValid)
            {
                    //bool valid = _NotesConsumerModel.AddNotesxMaestro(_NotesC.UCI, _NotesC.RegionalID, _NotesC.SpecialitID,_NotesC.TimesxWeek, _NotesC.Duration,_NotesC.TotalHours,_NotesC.Date,_NotesC.PresentInSession,_NotesC.Notes, User.Identity.Name);
                    bool valid = _NotesConsumerModel.AddNotesxMaestro(_NotesC.UCI, _NotesC.SpecialitID,_NotesC.Date, _NotesC.Duration, _NotesC.PresentInSession,_NotesC.Notes, User.Identity.Name);
                    if (valid)
                    {
                        return RedirectToAction("Index", "NotesxMaestro");
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
        [HttpPost]
        public string AddFromConsumer(string _uci, int _TeacherSpecialist, DateTime _TeacherDate, string _TeacherDuration, string _TeacherPresentInSession, string _TeacherNote)
        {
            if (ModelState.IsValid)
            {
                //bool validActive = _NotesConsumerModel.ValidarNotesConsumerActive(0, _uci, _ConsumerActive, User.Identity.Name);
                //if (validActive)
                //{
                //    return RedirectToAction("Add", "NotesxConsumer", new { error = true });
                //}
                //else
                //{
                bool valid = _NotesConsumerModel.AddNotesxMaestro(_uci, _TeacherSpecialist, _TeacherDate, _TeacherDuration, _TeacherPresentInSession, _TeacherNote, User.Identity.Name);
                if (valid)
                {
                    return "1";
                }
                else
                {
                    return "0";
                }
                //}

            }
            else
            {
                return "0";
            }
        }
        public ActionResult Update(int id, int type, bool? r, bool? error)
        {
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

            //List<RegionalCenterModel> _Regional = _RegionalModel.Get_RegionalCenter().ToList();
            //_RegionalList = new List<SelectListItem>();
            //foreach (var items in _Regional)
            //{
            //    _RegionalList.Add(new SelectListItem
            //    {
            //        Text = items.RegionalCenter,
            //        Value = items.IDRegionalCenter.ToString()
            //    });
            //}
            //ViewBag.VB_RegionalCenter = _RegionalList;

            List<SpecialistModel> _Specialist = _SpecialistModel.Get_Specialist().ToList();
            _SpecialistList = new List<SelectListItem>();
            foreach (var items in _Specialist)
            {
                _SpecialistList.Add(new SelectListItem
                {
                    Text = items.Name + " " + items.LastName + " - " + items.Specialty,
                    Value = items.IDSpecialist.ToString()
                });
            }
            ViewBag.VB_Specialist = _SpecialistList;

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
        //public ActionResult Update(string id, string UCI, int RegionalID,  int SpecialitID, int TimesxWeek, int Duration, int TotalHours, DateTime Date, string PresentInSession, string Notes)
        public ActionResult Update(string id, string UCI,  int SpecialitID, string Duration, DateTime Date, string PresentInSession, string Notes, int type)
        {
            
            //var valid = _NotesConsumerModel.UpdateNotesxMaestro(id, UCI, Notes, Active, User.Identity.Name);
                bool valid = _NotesConsumerModel.UpdateNotesxMaestro(id,UCI,SpecialitID,Duration,  Date, PresentInSession, Notes, User.Identity.Name);
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
                    return RedirectToAction("Delete", "NotesxConsumer", new { id = id, type = type, r = true });
                }
            }
            else
            {
                return View();
            }
        }


    }
}