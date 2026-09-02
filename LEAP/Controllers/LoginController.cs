using System;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using LEAP.Models;

namespace LEAP.Controllers
{
    [AllowAnonymous]
    public class LoginController : Controller
    {
        LoginModel _login = new LoginModel();
        public ActionResult Login()
        {
            if (Request.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home"); 
            }
            return View();
        }

        [HttpPost]
        public ActionResult ValidateLogin(string username, string Pass_word)
        {
            bool IsValidUser = _login.ValidateLogin(username, Pass_word);
            if (IsValidUser)
            {
                // El token de leap_api viaja en userData del ticket (encriptado
                // dentro de la cookie de auth) para no depender de sesion de
                // servidor — ApiClient lo lee de ahi en cada llamada.
                // El timeout viene de <forms timeout="..."> en Web.config (2880 min)
                // en vez de un valor fijo, para que coincida con la duracion de
                // sesion declarada ahi y con el sliding expiration del modulo de
                // Forms Authentication (se renueva solo con cada request).
                var authTicket = new FormsAuthenticationTicket(1, username, DateTime.Now, DateTime.Now.AddMinutes(FormsAuthentication.Timeout.TotalMinutes), false, _login.ApiToken);
                string encryptedTicket = FormsAuthentication.Encrypt(authTicket);
                var authCookie = new HttpCookie(FormsAuthentication.FormsCookieName, encryptedTicket);
                Response.Cookies.Add(authCookie);

                return RedirectToAction("Index", "Home");
            }
            else
            {
                ModelState.AddModelError("", "Nombre de usuario o contraseña incorrectos.");
                return RedirectToAction("Login", "Login");
            }
        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login", "Login");
        }
    }
}