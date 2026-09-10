using LEAP.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LEAP.Controllers
{
    public class AuthorizationController : Controller
    {
        AuthorizationModel _AuthModel = new AuthorizationModel();
        ConsumerModel _ConsumerModel = new ConsumerModel();
        private List<SelectListItem> _NotesList;
        public ActionResult Index()
        {
            List<AuthorizationModel> List_Auth = _AuthModel.Get_Authorizations();
            return View(List_Auth);
        }
        [AdminOnly]
        public ActionResult Add()
        {
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
        public ActionResult Add(AuthorizationModel _Authorization)
        {
            //auth
            if (_Authorization.IDAuth==null)
            {
                _Authorization.IDAuth = 0;
            }
            else
            {
                _Authorization.IDAuth = _Authorization.IDAuth;
            }
            //to
            
            if (_Authorization._To == null || _Authorization._To.ToString("MM-dd-yyyy") == "01-01-0001")
            {
                _Authorization._To = DateTime.Parse("1900-01-01");
            }
            else
            {
                _Authorization._To = _Authorization._To;
            }

            //bool valid = _AuthModel.AddAuthorization(_Authorization.UCI,_Authorization.auth, _Authorization._From,_Authorization._To, User.Identity.Name);
            bool valid = _AuthModel.AddAuthorization(_Authorization.UCI, _Authorization.auth, _Authorization._From, _Authorization._To, _Authorization.estado, User.Identity.Name);
            if (valid)
                {
                    return RedirectToAction("Index", "Authorization");
                }
                else
                {
                    return View();
                }
            
            
        }
        [HttpPost]
        [AdminOnly]
        public string AddFromConsumer(AuthorizationModel _Authorization)
        {
            if (ModelState.IsValid)
            {
                //bool valid = _AuthModel.AddAuthorization(_Authorization.UCI, _Authorization.auth, _Authorization._From, _Authorization._To, User.Identity.Name);
                bool valid = _AuthModel.AddAuthorization(_Authorization.UCI, _Authorization.auth, _Authorization._From, _Authorization._To, _Authorization.estado, User.Identity.Name);
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
        public ActionResult Update(int id)
        {
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

            var List_Authorization = _AuthModel.Get_Authorizations_ByID(id);
        
            return View(List_Authorization);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AdminOnly]
        //public ActionResult Update(string id, string UCI, string auth, DateTime _From, DateTime _To)
        public ActionResult Update(string id, string UCI, string auth, DateTime _From, DateTime _To, int estado, int type)
        //public ActionResult Update(string id, AuthorizationModel _Model)
        {
            var valid = _AuthModel.UpdateAuthorization(id, UCI, auth, _From, _To, estado, User.Identity.Name);
            //var valid = _AuthModel.UpdateAuthorization(id, UCI,auth, _From,_To, User.Identity.Name);
            if (valid)
            {
              
                    return RedirectToAction("Index", "Authorization");
               
             }
            else
            {
                return View();
            }
        }
        [HttpPost]
        [AdminOnly]
        public string UpdateFromConsumer(AuthorizationModel _Authorization)
        {
            if (ModelState.IsValid)
            {
                bool valid = _AuthModel.UpdateAuthorization(_Authorization.IDAuth.ToString(), _Authorization.UCI, _Authorization.auth, _Authorization._From, _Authorization._To, _Authorization.estado, User.Identity.Name);
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
        public ActionResult Delete(int id)
        {
            var List_Authorization = _AuthModel.Get_Authorizations_ByID(id);

            return View(List_Authorization);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AdminOnly]
        public ActionResult Delete(string id)
        {
            var valid = _AuthModel.DeleteAuthorization(id, User.Identity.Name);
            if (valid)
            {
               
                    return RedirectToAction("Index", "Authorization");
               
            }
            else
            {
                return View();
            }
        }

        //public ActionResult Get_NameByUCI(string uci)
        //{
        //    var List_Authorization = _ConsumerModel.Get_ConsumerNameByUCI(uci);
        //    return View(List_Authorization);
        //}
        public JsonResult Get_NameByUCI(string uci)
        {

            List<ConsumerModel> consumer = _ConsumerModel.Get_ConsumerNameByUCI(uci);

            return Json(consumer, JsonRequestBehavior.AllowGet);
        }

        public JsonResult Auth_byConsumer(int id)
        {

            List<AuthorizationModel> auth = _AuthModel.Get_Authorizations_ByConsumer(id);

            return Json(auth, JsonRequestBehavior.AllowGet);
        }

    }
}