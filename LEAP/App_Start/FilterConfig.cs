using System.Web;
using System.Web.Mvc;

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
}
