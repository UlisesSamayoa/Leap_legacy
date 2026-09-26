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

        // La vista ya no precarga los datos (eran 33k+ filas, ~9s solo en
        // json_encode del lado de leap_api) - el grid los pide el mismo por
        // pagina via GridData(), en modo "server-side processing" de DataTables.
        public ActionResult Index()
        {
            return View();
        }

        // Columnas tal cual las declara el DataTable de Index.cshtml (mismo
        // orden), para mapear el indice que manda DataTables en
        // "order[0][column]" al nombre real de columna que entiende leap_api.
        private static readonly string[] GridSortableColumns = { "UCI", null, null, "Active", "DateC" };

        [HttpGet]
        public JsonResult GridData(int draw, int start, int length)
        {
            string search = Request.QueryString["search[value]"];

            string sort = "IDNotesxConsumer";
            string dir = "desc";
            if (int.TryParse(Request.QueryString["order[0][column]"], out int colIndex)
                && colIndex >= 0 && colIndex < GridSortableColumns.Length
                && GridSortableColumns[colIndex] != null)
            {
                sort = GridSortableColumns[colIndex];
                dir = Request.QueryString["order[0][dir]"] == "asc" ? "asc" : "desc";
            }

            int perPage = length > 0 ? length : 50;
            int page = (start / perPage) + 1;

            var result = _NotesConsumerModel.Get_NotesxConsumer_Paged(page, perPage, search, sort, dir);

            var data = result.data.Select(n => new
            {
                n.IDNotesxConsumer,
                n.UCI,
                n.ConsumerName,
                Notes = !string.IsNullOrEmpty(n.Notes) && n.Notes.Length >= 100 ? n.Notes.Substring(0, 100) + "..." : n.Notes,
                Active = n.Active ? "True" : "False",
                DateC = n.DateC.HasValue ? n.DateC.Value.ToString("MM/dd/yyyy") : "",
            });

            return Json(new
            {
                draw,
                recordsTotal = result.total,
                recordsFiltered = result.total,
                data,
            }, JsonRequestBehavior.AllowGet);
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