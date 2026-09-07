using System.Web;
using System.Web.Mvc;
using LEAP.Data;

namespace LEAP
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
            filters.Add(new AjaxAwareAuthorizeAttribute());
        }
    }

    // AuthorizeAttribute normal siempre devuelve HttpUnauthorizedResult (401), y
    // luego FormsAuthenticationModule intercepta CUALQUIER 401 en la respuesta y
    // lo convierte en 302 al login antes de que llegue al cliente — sin importar
    // si la peticion era $.ajax o de pagina completa. Eso rompe en silencio las
    // llamadas AJAX (GetRegionals, insertS, etc.): jQuery sigue el redirect y el
    // success handler recibe el HTML del login en vez del JSON/texto esperado.
    // SuppressFormsAuthenticationRedirect debe fijarse ANTES de que termine el
    // filtro (poner el flag en Application_EndRequest llega demasiado tarde, el
    // orden de los modulos no lo garantiza), asi que se hace aqui mismo.
    public class AjaxAwareAuthorizeAttribute : AuthorizeAttribute
    {
        protected override void HandleUnauthorizedRequest(System.Web.Mvc.AuthorizationContext filterContext)
        {
            if (filterContext.HttpContext.Request.IsAjaxRequest())
            {
                filterContext.HttpContext.Response.SuppressFormsAuthenticationRedirect = true;
                filterContext.Result = new HttpStatusCodeResult(401);
            }
            else
            {
                base.HandleUnauthorizedRequest(filterContext);
            }
        }
    }

    // Veta el controller completo para un usuario Operador (Type_User = "2",
    // ver AuthContext.IsOperador) — usado en UsersController/SpecialistController,
    // que quedan reservados a admin. Corre despues de AjaxAwareAuthorizeAttribute,
    // asi que el usuario ya esta autenticado cuando se evalua esto.
    public class AdminOnlyAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (AuthContext.IsOperador)
            {
                if (filterContext.HttpContext.Request.IsAjaxRequest())
                {
                    filterContext.Result = new HttpStatusCodeResult(403);
                }
                else
                {
                    filterContext.Result = new RedirectResult("~/Home/Index");
                }
                return;
            }

            base.OnActionExecuting(filterContext);
        }
    }
}
