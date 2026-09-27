using LEAP.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LEAP.Controllers
{
    [NotCdsOnly]
    public class RegionalCenterController : Controller
    {
        RegionalCenterModel _RCenter_Model = new RegionalCenterModel();
        //private ApplicationDbContext db = new ApplicationDbContext();
        public ActionResult Index()
        {
            // Get_RegionalCenter() se comparte con dropdowns de Consumer y
            // ServiceCoordinator Add/Update (no se reordenan ahi) - fecha de
            // ingreso mas reciente primero solo para este grid (decision
            // confirmada con el usuario, aplica a todos los modulos).
            List<RegionalCenterModel> List_RCenter = _RCenter_Model.Get_RegionalCenter().OrderByDescending(r => r.DateC).ToList();
            return View(List_RCenter);
        }
        [AdminOnly]
        public ActionResult Add()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AdminOnly]
        public ActionResult Add(RegionalCenterModel _RCenter)
        {
            if (ModelState.IsValid)
            {
                bool valid = _RCenter_Model.AddRegionalCenter(_RCenter.RegionalCenter,_RCenter.Phone,_RCenter.Address,_RCenter.City,_RCenter.State,_RCenter.ZipCode,_RCenter.Ext, User.Identity.Name);
                if (valid)
                {
                    return RedirectToAction("Index", "RegionalCenter");
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
            var List_RCenter = _RCenter_Model.Get_RegionalCenter_ByID(id);
            return View(List_RCenter);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AdminOnly]
        public ActionResult Update(string id, string RegionalCenter, string Phone, string Address, string City, string State, string ZipCode, string Ext)
        {
            var valid = _RCenter_Model.UpdateRegionalCenter(id,RegionalCenter, Phone, Address, City, State, ZipCode, Ext, User.Identity.Name);
            if (valid)
            {
                return RedirectToAction("Index", "RegionalCenter");
            }
            else
            {
                return View();
            }
        }
        [AdminOnly]
        public ActionResult Delete(int id)
        {
            var List_RCenter = _RCenter_Model.Get_RegionalCenter_ByID(id);
            return View(List_RCenter);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AdminOnly]
        public ActionResult Delete(string id)
        {
            var valid = _RCenter_Model.DeleteRegionalCenter(id,User.Identity.Name);
            if (valid)
            {
                return RedirectToAction("Index", "RegionalCenter");
            }
            else
            {
                return View();
            }
        }


    }
}