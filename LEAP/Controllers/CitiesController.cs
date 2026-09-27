using LEAP.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LEAP.Controllers
{
    [NotCdsOnly]
    public class CitiesController : Controller
    {
        CitiesModel _CitiesModel = new CitiesModel();
        public ActionResult Index()
        {
            // Get_Cities() se comparte con el dropdown de Consumer Add/Update (no se
            // reordena ahi) - fecha de ingreso mas reciente primero solo para este
            // grid (decision confirmada con el usuario, aplica a todos los modulos).
            List<CitiesModel> List_Cities = _CitiesModel.Get_Cities().OrderByDescending(c => c.DateC).ToList();
            return View(List_Cities);
        }
        [AdminOnly]
        public ActionResult Add()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AdminOnly]
        public ActionResult Add(CitiesModel _City)
        {
            if (ModelState.IsValid)
            {
                bool valid = _CitiesModel.AddCity(_City.City, _City.State, User.Identity.Name);
                if (valid)
                {
                    return RedirectToAction("Index", "Cities");
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
        public ActionResult Update(int id)
        {
            var List_Cities = _CitiesModel.Get_Cities_ByID(id);
            return View(List_Cities);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AdminOnly]
        public ActionResult Update(string id, string City, string State)
        {
            var valid = _CitiesModel.UpdateCity(id, City, State, User.Identity.Name);
            if (valid)
            {
                return RedirectToAction("Index", "Cities");
            }
            else
            {
                return View();
            }
        }
        [AdminOnly]
        public ActionResult Delete(int id)
        {
            var List_Cities = _CitiesModel.Get_Cities_ByID(id);
            return View(List_Cities);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AdminOnly]
        public ActionResult Delete(string id)
        {
            var valid = _CitiesModel.DeleteCity(id, User.Identity.Name);
            if (valid)
            {
                return RedirectToAction("Index", "Cities");
            }
            else
            {
                return View();
            }
        }



    }
}