using LEAP.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LEAP.Controllers
{
    [NotCdsOnly]
    public class SpecialistController : Controller
    {
        SpecialistModel _SpecialistModel = new SpecialistModel();
        public ActionResult Index()
        {
            List<SpecialistModel> List_Specialist = _SpecialistModel.Get_Specialist();
            return View(List_Specialist);
        }
        [AdminOnly]
        public ActionResult Add()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AdminOnly]
        public ActionResult Add(SpecialistModel _Specialist)
        {
            if (ModelState.IsValid)
            {
                bool valid = _SpecialistModel.AddSpecialist(_Specialist.Name, _Specialist.LastName, _Specialist.Phone, _Specialist.Email, _Specialist.Address, _Specialist.City, _Specialist.State, _Specialist.ZipCode, _Specialist.HourlyRate, _Specialist.Specialty, User.Identity.Name);
                if (valid)
                {
                    return RedirectToAction("Index", "Specialist");
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
            var List_Specialist = _SpecialistModel.Get_Specialist_ByID(id);
            return View(List_Specialist);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AdminOnly]
        public ActionResult Update(string id, string Name, string LastName, string Phone, string Email, string Address, string City, string State, string ZipCode, string HourlyRate, string Specialty)
        {
            var valid = _SpecialistModel.UpdateSpecialist(id, Name, LastName, Phone, Email, Address, City, State, ZipCode, HourlyRate, Specialty, User.Identity.Name);
            if (valid)
            {
                return RedirectToAction("Index", "Specialist");
            }
            else
            {
                return View();
            }
        }
        [AdminOnly]
        public ActionResult Delete(int id)
        {
            var List_Specialist = _SpecialistModel.Get_Specialist_ByID(id);
            return View(List_Specialist);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AdminOnly]
        public ActionResult Delete(string id)
        {
            var valid = _SpecialistModel.DeleteSpecialist(id, User.Identity.Name);
            if (valid)
            {
                return RedirectToAction("Index", "Specialist");
            }
            else
            {
                return View();
            }
        }


    }
}