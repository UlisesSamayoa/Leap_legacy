using LEAP.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LEAP.Controllers
{
    [AdminOnly]
    public class UsersController : Controller
    {
        UsersModel _UsersModel = new UsersModel();
        public ActionResult Index()
        {
            List<UsersModel> List_users = _UsersModel.Get_Users();
            return View(List_users);
        }
        public ActionResult Add()
        {
            return View();
        }
        public ActionResult AddUsers(UsersModel _Users)
        {
            if (ModelState.IsValid)
            {
                bool valid = _UsersModel.AddUsers(_Users.Name, _Users.LastName, _Users.DateOfBirth,_Users.UserName,_Users.Phone, _Users.Email,_Users.Type_User,_Users.Password,User.Identity.Name, _Users.SpecialistId);
                if (valid)
                {
                    return RedirectToAction("Index", "Users");
                }
                else
                {
                    return RedirectToAction("Add", "Users");
                }
            }
            else
            {
                return View();
            }
        }
        public ActionResult Change(int id)
        {
            var List_users = _UsersModel.Get_UsersById(id);
            return View(List_users);
        }
        public ActionResult ChangeUser(string id,string Password)
        {
            var valid = _UsersModel.ChangeUser(id, Password, User.Identity.Name);
            if (valid)
            {
                return RedirectToAction("Index", "Users");
            }
            else
            {
                return RedirectToAction("Change", "Users", new { id = id });
            }
        }
        public ActionResult Update(int id)
        {
            var List_users = _UsersModel.Get_UsersById(id);
            return View(List_users);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UpdateUser(string id, UsersModel _Users)
        {
            var valid = _UsersModel.UpdateUsers(id, _Users.Name, _Users.LastName, _Users.DateOfBirth,_Users.UserName, _Users.Phone, _Users.Email, _Users.Type_User, User.Identity.Name, _Users.SpecialistId);
            if (valid)
            {
                return RedirectToAction("Index", "Users");
            }
            else
            {
                return RedirectToAction("Update", "Users", new { id = id});
            }
        }
        public ActionResult Delete(int id)
        {
            var List_users = _UsersModel.Get_UsersById(id);
            return View(List_users);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteUser(string id)
        {
            var valid = _UsersModel.DeleteUser(id, User.Identity.Name);
            if (valid)
            {
                return RedirectToAction("Index", "Users");
            }
            else
            {
                return RedirectToAction("Delete", "Users", new { id = id });
            }
        }


    }
}