using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using LEAP.Models;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.Cookies;

namespace LEAP.Controllers
{
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
                var claims = new List<Claim>
                    {
                        new Claim("usuario", username),
                        new Claim(ClaimTypes.Name, username)
                    };

                // Crear el principal
                var identity = new ClaimsIdentity(claims, "ApplicationCookie");
                var principal = new ClaimsPrincipal(identity);

                // Establecer la cookie de autenticación
                var authTicket = new FormsAuthenticationTicket(1, username, DateTime.Now, DateTime.Now.AddMinutes(30), false, string.Join(",", claims));
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