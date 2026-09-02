using LEAP.Data;
using Newtonsoft.Json;
using System;

namespace LEAP.Models
{
    public class LoginModel
    {
        public string userID { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string username { get; set; }
        public string Pass_word { get; set; }

        // Token que devuelve leap_api al loguearse — LoginController lo guarda
        // en el FormsAuthenticationTicket para adjuntarlo en llamadas futuras.
        public string ApiToken { get; private set; }

        private class ApiLoginResponse
        {
            [JsonProperty("token")]
            public string Token { get; set; }
        }

        public bool ValidateLogin(string _user, string _pwd)
        {
            try
            {
                var response = ApiClient.PostWithToken<ApiLoginResponse>("login", new
                {
                    UserName = _user,
                    password = _pwd,
                }, token: null);

                ApiToken = response?.Token;
                return !string.IsNullOrEmpty(ApiToken);
            }
            catch (ApiException)
            {
                // Credenciales invalidas (422) u otro error de leap_api.
                return false;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
