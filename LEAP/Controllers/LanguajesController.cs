using LEAP.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LEAP.Controllers
{
    public class LanguajesController : Controller
    {
        LanguajesModel _LanguajesModel = new LanguajesModel();
        public ActionResult Index()
        {
            List<LanguajesModel> List_Languajes = _LanguajesModel.Get_Languajes();
            return View(List_Languajes);
        }
        public ActionResult Add()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Add(LanguajesModel _Languaje)
        {
            if (ModelState.IsValid)
            {
                bool valid = _LanguajesModel.AddLanguaje(_Languaje.Languaje,_Languaje.Notes, User.Identity.Name);
                if (valid)
                {
                    return RedirectToAction("Index", "Languajes");
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
        public ActionResult Update(int id)
        {
            var List_Languaje = _LanguajesModel.Get_Languajes_ByID(id);
            return View(List_Languaje);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Update(string id, string Languaje, string Notes)
        {
            var valid = _LanguajesModel.UpdateLanguaje(id, Languaje, Notes,User.Identity.Name);
            if (valid)
            {
                return RedirectToAction("Index", "Languajes");
            }
            else
            {
                return View();
            }
        }
        public ActionResult Delete(int id)
        {
            var List_Languaje = _LanguajesModel.Get_Languajes_ByID(id);
            return View(List_Languaje);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(string id)
        {
            var valid = _LanguajesModel.DeleteLanguaje(id, User.Identity.Name);
            if (valid)
            {
                return RedirectToAction("Index", "Languajes");
            }
            else
            {
                return View();
            }
        }
        
    }
}