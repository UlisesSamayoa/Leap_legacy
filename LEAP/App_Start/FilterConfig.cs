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

    // Veta el controller completo salvo para Admin - ni Operador ni CDS pueden
    // entrar (usado en UsersController, gestionar cuentas queda exclusivo de
    // Admin). Corre despues de AjaxAwareAuthorizeAttribute, asi que el usuario
    // ya esta autenticado cuando se evalua esto.
    public class AdminOnlyAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (!AuthContext.IsAdmin)
            {
                DenyAccess(filterContext);
                return;
            }

            base.OnActionExecuting(filterContext);
        }

        internal static void DenyAccess(ActionExecutingContext filterContext)
        {
            if (filterContext.HttpContext.Request.IsAjaxRequest())
            {
                filterContext.Result = new HttpStatusCodeResult(403);
            }
            else
            {
                filterContext.Result = new RedirectResult("~/Home/Index");
            }
        }
    }

    // Veta el controller completo solo para CDS - Admin y Operador si entran
    // (usado en Specialist, RegionalCenter, Languajes, Cities,
    // ServiceCoordinator: catalogos/operativo que Operador si puede
    // administrar, a diferencia de Users que queda solo para Admin).
    public class NotCdsOnlyAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (AuthContext.IsCds)
            {
                AdminOnlyAttribute.DenyAccess(filterContext);
                return;
            }

            base.OnActionExecuting(filterContext);
        }
    }
}
