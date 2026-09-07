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

        // Datos que LoginController guarda (serializados) en el
        // FormsAuthenticationTicket para adjuntarlos/leerlos en llamadas
        // futuras — token para ApiClient, Type_User/specialist para AuthContext.
        public string TicketData { get; private set; }

        private class ApiLoginResponse
        {
            [JsonProperty("token")]
            public string Token { get; set; }

            [JsonProperty("user")]
            public ApiUserResponse User { get; set; }
        }

        private class ApiUserResponse
        {
            [JsonProperty("Type_User")]
            public string TypeUser { get; set; }

            [JsonProperty("specialist_id")]
            public int? SpecialistId { get; set; }

            [JsonProperty("specialist")]
            public ApiSpecialistResponse Specialist { get; set; }
        }

        private class ApiSpecialistResponse
        {
            [JsonProperty("Name")]
            public string Name { get; set; }

            [JsonProperty("LastName")]
            public string LastName { get; set; }
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

                if (string.IsNullOrEmpty(response?.Token))
                {
                    return false;
                }

                TicketData = JsonConvert.SerializeObject(new AuthTicketData
                {
                    Token = response.Token,
                    TypeUser = response.User?.TypeUser,
                    SpecialistId = response.User?.SpecialistId,
                    SpecialistName = response.User?.Specialist != null
                        ? (response.User.Specialist.Name + " " + response.User.Specialist.LastName).Trim()
                        : null,
                });
                return true;
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
