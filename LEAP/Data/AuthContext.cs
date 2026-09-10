using System;
using System.Web;
using System.Web.Security;
using Newtonsoft.Json;

namespace LEAP.Data
{
    // Lee el AuthTicketData guardado en el FormsAuthenticationTicket por
    // LoginController, mismo criterio que ApiClient.CurrentToken ya usaba para
    // el token crudo.
    public static class AuthContext
    {
        public static AuthTicketData Current
        {
            get
            {
                var identity = HttpContext.Current?.User?.Identity as FormsIdentity;
                var raw = identity?.Ticket?.UserData;
                if (string.IsNullOrEmpty(raw))
                {
                    return null;
                }

                try
                {
                    return JsonConvert.DeserializeObject<AuthTicketData>(raw);
                }
                catch (Exception)
                {
                    // Cookie emitida antes de este cambio: UserData todavia es el
                    // token crudo (no es JSON valido). Devolver null hace que
                    // ApiClient no mande bearer token, leap_api responde 401, y el
                    // usuario vuelve a loguearse de forma natural.
                    return null;
                }
            }
        }

        // Type_User: "1" Admin (acceso total, incluida gestion de Users), "3"
        // Operador (acceso total EXCEPTO gestionar Users), "2" CDS (solo su
        // propia data). Cualquier otro valor (legacy libre como "Admin"/"Staff"
        // o vacio) se trata como Admin - mismo criterio de leap_api (User.php).
        public static bool IsCds
        {
            get { return Current?.TypeUser == "2"; }
        }

        public static bool IsOperador
        {
            get { return Current?.TypeUser == "3"; }
        }

        public static bool IsAdmin
        {
            get { return !IsCds && !IsOperador; }
        }
    }
}
