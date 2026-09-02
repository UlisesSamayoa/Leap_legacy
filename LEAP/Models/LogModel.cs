using LEAP.Data;
using System;

namespace LEAP.Models
{
    public class LogModel
    {
        // leap_api valida DetailAction/DetailError, Action y Controller como
        // varchar(200)/varchar(100) (ver LogActionController/LogErrorController).
        // _baseAction/_baseError puede venir de ApiException.Message, que incluye
        // el body crudo de leap_api (con stack trace si APP_DEBUG=true) y facilmente
        // supera 200 caracteres: sin truncar, leap_api responde 422 y el log se
        // pierde en silencio dentro del catch de abajo. FullDetail* es TEXT (sin
        // limite) y sigue llevando el mensaje completo.
        private static string Truncate(string value, int maxLength)
        {
            if (string.IsNullOrEmpty(value) || value.Length <= maxLength) return value;
            return value.Substring(0, maxLength);
        }

        public bool _logAction(string _baseAction, string _fullAction, string _Action, string _Controller, string _user)
        {
            try
            {
                ApiClient.Post<object>("logs-actions", new
                {
                    DetailAction = Truncate(_baseAction, 200),
                    FullDetailAction = _fullAction,
                    Action = Truncate(_Action, 100),
                    Controller = Truncate(_Controller, 100),
                });
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool _logError(string _baseError, string _fullError, string _Action, string _Controller, string _user)
        {
            try
            {
                ApiClient.Post<object>("logs-errors", new
                {
                    DetailError = Truncate(_baseError, 200),
                    FullDetailError = _fullError,
                    Action = Truncate(_Action, 100),
                    Controller = Truncate(_Controller, 100),
                });
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
