using LEAP.Data;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LEAP.Models
{
    public class AuthorizationModel
    {
        public int? IDAuth { get; set; }
        public string auth { get; set; }
        public string UCI { get; set; }
        public string ConsumerName { get; set; }
        public DateTime _From { get; set; }
        public DateTime _To { get; set; }
        public string _From_s { get; set; }
        public string _To_s { get; set; }
        public string UserC { get; set; }
        public DateTime? DateC { get; set; }
        public string UserU { get; set; }
        public DateTime? DateU { get; set; }
        public bool _ErrorCode { get; set; }
        public int type { get; set; }
        public int estado { get; set; }
        LogModel _log = new LogModel();
        public string regionalC { get; set; }

        // Replica SP_Authorization_AllData: join con Consumer (+ RegionalCenter)
        // resuelto client-side, ordenado por ConsumerName igual que el original.
        public List<AuthorizationModel> Get_Authorizations()
        {
            var _Auth_Response = new List<AuthorizationModel>();
            try
            {
                _Auth_Response = ApiClient.Get<List<AuthorizationModel>>("authorizations");
                var consumers = ApiClient.Get<List<ConsumerModel>>("consumers");
                var centers = ApiClient.Get<List<RegionalCenterModel>>("regional-centers");
                var byUci = consumers.Where(c => c.UCI != null).ToDictionary(c => c.UCI, c => c);
                var centerNames = centers.ToDictionary(c => c.IDRegionalCenter, c => c.RegionalCenter);

                foreach (var a in _Auth_Response)
                {
                    if (byUci.TryGetValue(a.UCI, out var c))
                    {
                        a.ConsumerName = c.LastName + " ," + c.Name;
                        centerNames.TryGetValue(c.RegionalID, out var rc);
                        a.regionalC = rc;
                    }
                    a._From_s = a._From == default(DateTime) ? "" : a._From.ToString("yyyy-MM-dd");
                    a._To_s = a._To == default(DateTime) ? "" : a._To.ToString("yyyy-MM-dd");
                }
                _Auth_Response = _Auth_Response.OrderBy(a => a.ConsumerName).ToList();
            }
            catch (Exception)
            {
                _Auth_Response.Add(new AuthorizationModel { _ErrorCode = true });
            }
            return _Auth_Response;
        }

        public AuthorizationModel Get_Authorizations_ByID(int _auth)
        {
            var _Auth_Response = new AuthorizationModel();
            try
            {
                _Auth_Response = ApiClient.Get<AuthorizationModel>("authorizations/" + _auth);
                var match = ApiClient.Get<List<ConsumerModel>>("consumers?uci=" + Uri.EscapeDataString(_Auth_Response.UCI ?? "")).FirstOrDefault();
                if (match != null)
                {
                    _Auth_Response.ConsumerName = match.Name + " " + match.LastName;
                }
                _Auth_Response._From_s = _Auth_Response._From.ToString("yyyy-MM-dd");
                _Auth_Response._To_s = _Auth_Response._To.ToString("yyyy-MM-dd");
                _Auth_Response._ErrorCode = false;
            }
            catch (Exception)
            {
                _Auth_Response._ErrorCode = true;
            }
            return _Auth_Response;
        }

        // SP_Authorization_AllDataByConsumer en realidad filtra por UCI (el
        // parametro se llama @ID pero es varchar) — se preserva ese contrato.
        public List<AuthorizationModel> Get_Authorizations_ByConsumer(int _auth)
        {
            var _Auth_Response = new List<AuthorizationModel>();
            try
            {
                _Auth_Response = ApiClient.Get<List<AuthorizationModel>>("authorizations?uci=" + _auth);
                var match = ApiClient.Get<List<ConsumerModel>>("consumers?uci=" + _auth).FirstOrDefault();
                foreach (var a in _Auth_Response)
                {
                    if (match != null)
                    {
                        a.ConsumerName = match.Name + " " + match.LastName;
                    }
                    a._From_s = a._From.ToString("yyyy-MM-dd");
                    a._To_s = a._To.ToString("yyyy-MM-dd");
                    a._ErrorCode = false;
                }
            }
            catch (Exception)
            {
                _Auth_Response.Add(new AuthorizationModel { _ErrorCode = true });
            }
            return _Auth_Response;
        }

        public bool AddAuthorization(string _UCI, string _Authorization, DateTime? _From, DateTime? _To, int estado, string _UserName)
        {
            bool response = false;
            if (_Authorization == null)
            {
                _Authorization = "";
            }
            try
            {
                ApiClient.Post<AuthorizationModel>("authorizations", new
                {
                    UCI = _UCI,
                    auth = _Authorization,
                    _From = _From?.ToString("yyyy-MM-dd"),
                    _To = _To?.ToString("yyyy-MM-dd"),
                    estado = estado.ToString(),
                });
                response = true;
                _log._logAction("Create Authorization", "Create a new Authorization, name:" + _Authorization, "AddAuthorization", "AuthorizationModel", _UserName);
            }
            catch (Exception _error)
            {
                response = false;
                _log._logError(_error.Message, _error.StackTrace, "AddAuthorization", "AuthorizationModel", _UserName);
            }
            return response;
        }

        public bool UpdateAuthorization(string _IDAuthorization, string _UCI, string _Auth, DateTime? _From, DateTime? _To, int estado, string _UserName)
        {
            bool response = false;
            try
            {
                ApiClient.Put<AuthorizationModel>("authorizations/" + _IDAuthorization, new
                {
                    UCI = _UCI,
                    auth = _Auth,
                    _From = _From?.ToString("yyyy-MM-dd"),
                    _To = _To?.ToString("yyyy-MM-dd"),
                    estado = estado.ToString(),
                });
                response = true;
                _log._logAction("Update Authorization", "Update Authorization, name:" + _Auth, "UpdateAuthorization", "AuthorizationModel", _UserName);
            }
            catch (Exception _error)
            {
                response = false;
                _log._logError(_error.Message, _error.StackTrace, "UpdateAuthorization", "AuthorizationModel", _UserName);
            }
            return response;
        }

        public bool DeleteAuthorization(string _IDAuthorization, string _UserName)
        {
            bool response = false;
            try
            {
                ApiClient.Delete("authorizations/" + _IDAuthorization);
                response = true;
                _log._logAction("Delete Authorization", "Delete Authorizations, id:" + _IDAuthorization, "DeleteAuthorization", "AuthorizationModel", _UserName);
            }
            catch (Exception _error)
            {
                response = false;
                _log._logError(_error.Message, _error.StackTrace, "DeleteAuthorization", "AuthorizationModel", _UserName);
            }
            return response;
        }
    }
}
