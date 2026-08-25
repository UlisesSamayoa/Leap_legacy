using LEAP.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LEAP.Controllers
{
    public class RegionalCenterController : Controller
    {
        RegionalCenterModel _RCenter_Model = new RegionalCenterModel();
        //private ApplicationDbContext db = new ApplicationDbContext();
        public ActionResult Index()
        {
            List<RegionalCenterModel> List_RCenter = _RCenter_Model.Get_RegionalCenter();
            return View(List_RCenter);
        }
        public ActionResult Add()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
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
       
        public ActionResult Update(int id)
        {
            var List_RCenter = _RCenter_Model.Get_RegionalCenter_ByID(id);
            return View(List_RCenter);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
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
        public ActionResult Delete(int id)
        {
            var List_RCenter = _RCenter_Model.Get_RegionalCenter_ByID(id);
            return View(List_RCenter);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
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