using LEAP.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LEAP.Controllers
{
    [NotCdsOnly]
    public class ServiceCoordinatorController : Controller
    {
        ServiceCoordinatorModel _ServiceCModel = new ServiceCoordinatorModel();
        RegionalCenterModel _regional = new RegionalCenterModel();
        public ActionResult Index()
        {
            List<ServiceCoordinatorModel> List_servicesC = _ServiceCModel.Get_ServiceC();
            return View(List_servicesC);
        }
        [AdminOnly]
        public ActionResult Add()
        {
            return View();

        }
        public string GetRegionals()
        {

            string salida = "";
            List<RegionalCenterModel> dr = _regional.Get_RegionalCenter();

            foreach (var item in dr)
            {
                salida += item.IDRegionalCenter + "," + item.RegionalCenter + "," + item.Address + "," + item.City + "," + item.State + "," + item.ZipCode + "," + item.Phone + "," + item.serviceC + "," + item.telephone + ";" + Environment.NewLine;


            }

            return salida;

        }
        [AdminOnly]
        public ActionResult Update(int id)
        {
            var List_service = _ServiceCModel.Get_Services_ByID(id);
            return View(List_service);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [AdminOnly]
        public ActionResult Update(int id, string NameC, string Tel, int Regional)
        {
            var valid = _ServiceCModel.UpdateS(id, NameC, Tel, Regional, User.Identity.Name);
            if (valid)
            {
                return RedirectToAction("Index", "ServiceCoordinator");
            }
            else
            {
                return View();
            }
        }

        [AdminOnly]
        public ActionResult insertS(int regional, string sc, string tel)
        {
            var valid = _ServiceCModel.Create(regional, sc, tel);
            if (valid)
            {
                return RedirectToAction("Index", "ServiceCoordinator");
            }
            else
            {
                return View();
            }
        }

        [AdminOnly]
        public ActionResult Delete(int id)
        {
            var List_service = _ServiceCModel.Get_Services_ByID(id);
            return View(List_service);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [AdminOnly]
        public ActionResult Delete(string id)
        {
            var valid = _ServiceCModel.DeleteServiceC(Convert.ToInt32(id), User.Identity.Name);
            if (valid)
            {
                return RedirectToAction("Index", "ServiceCoordinator");
            }
            else
            {
                return View();
            }
        }
    }

}