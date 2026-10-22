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

        // El filtro a "solo consumers Active" en leap_api (que ya excluye el 97%
        // de las 33k+ notas - historial de consumers Ready4Archive/Archived o
        // que ya ni existen en el catalogo) dejo el dataset lo bastante chico
        // para volver al DataTables nativo: el grid pide todo una sola vez via
        // GetAll() y la busqueda/orden/paginado los maneja el navegador, igual
        // que el comportamiento original antes de pasar a server-side processing.
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public JsonResult GetAll()
        {
            var data = _NotesConsumerModel.Get_NotesxConsumer().Select(n => new
            {
                n.IDNotesxConsumer,
                n.UCI,
                n.ConsumerName,
                Notes = !string.IsNullOrEmpty(n.Notes) && n.Notes.Length >= 100 ? n.Notes.Substring(0, 100) + "..." : n.Notes,
                Active = n.Active ? "True" : "False",
                DateC = n.DateC.HasValue ? n.DateC.Value.ToString("MM/dd/yyyy") : "",
            });

            return Json(data, JsonRequestBehavior.AllowGet);
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
                // leap_api ahora hace swap automatico (desactiva las demas notas
                // del mismo UCI al activar esta) en vez de rechazar - ya no hace
                // falta validar conflicto antes de llamar Add.
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
            // leap_api ahora hace swap automatico (desactiva las demas notas del
            // mismo UCI al activar esta) en vez de rechazar - ya no hace falta
            // validar conflicto antes de llamar Update.
            var valid = _NotesConsumerModel.UpdateNotesxConsumer(id, UCI, Notes, Active, User.Identity.Name);
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