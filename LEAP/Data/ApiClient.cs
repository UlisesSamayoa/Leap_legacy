using Newtonsoft.Json;
using System;
using System.Configuration;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Security;

namespace LEAP.Data
{
    // Lanzada cuando leap_api responde con un status de error (4xx/5xx).
    // Trae el body crudo (normalmente JSON con "message"/"errors") para que
    // el Model que la atrapa pueda decidir que hacer, igual que antes hacia
    // con las excepciones de SqlCommand.
    public class ApiException : Exception
    {
        public int StatusCode { get; }
        public string ResponseBody { get; }

        public ApiException(int statusCode, string responseBody)
            : base("leap_api respondio " + statusCode + ": " + responseBody)
        {
            StatusCode = statusCode;
            ResponseBody = responseBody;
        }
    }

    // Reemplaza a ApplicationDbContext: en vez de abrir un SqlConnection local,
    // habla por HTTP contra leap_api. El token del usuario logueado se lee del
    // FormsAuthenticationTicket (donde LoginController lo guarda al iniciar
    // sesion) y se adjunta a cada request como Bearer token, para que leap_api
    // registre UserC/UserU con la identidad real de quien hizo la accion.
    public static class ApiClient
    {
        private static readonly HttpClient _http = new HttpClient
        {
            BaseAddress = new Uri(ConfigurationManager.AppSettings["LeapApiBaseUrl"])
        };

        public static string CurrentToken
        {
            get
            {
                var identity = HttpContext.Current?.User?.Identity as FormsIdentity;
                return identity?.Ticket?.UserData;
            }
        }

        public static T Get<T>(string path)
        {
            return SendAsync<T>(HttpMethod.Get, path, null).GetAwaiter().GetResult();
        }

        public static T Post<T>(string path, object body)
        {
            return SendAsync<T>(HttpMethod.Post, path, body).GetAwaiter().GetResult();
        }

        public static T Put<T>(string path, object body, bool includeNulls = false)
        {
            return SendAsync<T>(HttpMethod.Put, path, body, includeNulls: includeNulls).GetAwaiter().GetResult();
        }

        public static T Patch<T>(string path, object body)
        {
            return SendAsync<T>(new HttpMethod("PATCH"), path, body).GetAwaiter().GetResult();
        }

        public static void Delete(string path)
        {
            SendAsync<object>(HttpMethod.Delete, path, null).GetAwaiter().GetResult();
        }

        // Login no lleva token propio (es el que lo obtiene) — se llama igual
        // que el resto, simplemente no hay CurrentToken todavia en ese momento.
        public static T PostWithToken<T>(string path, object body, string token)
        {
            return SendAsync<T>(HttpMethod.Post, path, body, token).GetAwaiter().GetResult();
        }

        private static async Task<T> SendAsync<T>(HttpMethod method, string path, object body, string tokenOverride = null, bool includeNulls = false)
        {
            using (var request = new HttpRequestMessage(method, path))
            {
                var token = tokenOverride ?? CurrentToken;
                if (!string.IsNullOrEmpty(token))
                {
                    request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
                }
                request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                if (body != null)
                {
                    var json = JsonConvert.SerializeObject(body, new JsonSerializerSettings
                    {
                        NullValueHandling = includeNulls ? NullValueHandling.Include : NullValueHandling.Ignore,
                    });
                    request.Content = new StringContent(json, Encoding.UTF8, "application/json");
                }

                using (var response = await _http.SendAsync(request).ConfigureAwait(false))
                {
                    var content = await response.Content.ReadAsStringAsync().ConfigureAwait(false);

                    if (!response.IsSuccessStatusCode)
                    {
                        throw new ApiException((int)response.StatusCode, content);
                    }

                    if (string.IsNullOrWhiteSpace(content))
                    {
                        return default(T);
                    }

                    // Muchos Models usan DateTime/int/bool NO nullable (para no
                    // tener que tocar todas las Views), pero las columnas en BD
                    // si aceptan NULL. Sin este handler, un solo campo null
                    // (ej. DateOfBirth sin capturar) aborta la deserializacion
                    // de la lista COMPLETA. Con esto, solo esa propiedad se deja
                    // en su valor default y el resto del objeto/lista se arma bien.
                    return JsonConvert.DeserializeObject<T>(content, new JsonSerializerSettings
                    {
                        Error = (sender, args) => { args.ErrorContext.Handled = true; },
                    });
                }
            }
        }
    }
}
