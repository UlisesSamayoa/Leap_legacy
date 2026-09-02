using LEAP.Data;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace LEAP.Models
{
    public class LanguajesModel
    {
        public int? IDLanguaje { get; set; }
        public string Languaje { get; set; }
        public string Notes { get; set; }
        public string UserC { get; set; }
        public DateTime? DateC { get; set; }
        public string UserU { get; set; }
        public DateTime? DateU { get; set; }
        public bool _ErrorCode { get; set; }
        LogModel _log = new LogModel();

        public List<LanguajesModel> Get_Languajes()
        {
            var _Languajes_Response = new List<LanguajesModel>();
            try
            {
                _Languajes_Response = ApiClient.Get<List<LanguajesModel>>("languajes");
            }
            catch (Exception)
            {
                _Languajes_Response.Add(new LanguajesModel { _ErrorCode = true });
            }
            return _Languajes_Response;
        }

        public LanguajesModel Get_Languajes_ByID(int _Languaje)
        {
            var _Languajes_Response = new LanguajesModel();
            try
            {
                _Languajes_Response = ApiClient.Get<LanguajesModel>("languajes/" + _Languaje);
                _Languajes_Response._ErrorCode = false;
            }
            catch (Exception)
            {
                _Languajes_Response._ErrorCode = true;
            }
            return _Languajes_Response;
        }

        public bool AddLanguaje(string _Languaje, string _Notes, string _UserName)
        {
            bool response = false;
            try
            {
                ApiClient.Post<LanguajesModel>("languajes", new { Languaje = _Languaje, Notes = _Notes });
                response = true;
                _log._logAction("Create Languaje", "Create a new Languaje, name:" + _Languaje, "AddLanguaje", "LanguajesModel", _UserName);
            }
            catch (Exception _error)
            {
                response = false;
                _log._logError(_error.Message, _error.StackTrace, "AddLanguaje", "LanguajesModel", _UserName);
            }
            return response;
        }

        public bool UpdateLanguaje(string _IDLanguaje, string _Languaje, string _Notes, string _UserName)
        {
            bool response = false;
            try
            {
                ApiClient.Put<LanguajesModel>("languajes/" + _IDLanguaje, new { Languaje = _Languaje, Notes = _Notes });
                response = true;
                _log._logAction("Update Languaje", "Update Languaje, name:" + _Languaje, "UpdateLanguaje", "LanguajesModel", _UserName);
            }
            catch (Exception _error)
            {
                response = false;
                _log._logError(_error.Message, _error.StackTrace, "UpdateLanguaje", "LanguajesModel", _UserName);
            }
            return response;
        }

        public bool DeleteLanguaje(string _IDLanguaje, string _UserName)
        {
            bool response = false;
            try
            {
                ApiClient.Delete("languajes/" + _IDLanguaje);
                response = true;
                _log._logAction("Delete Languajes", "Delete Languajes, id:" + _IDLanguaje, "DeleteLanguaje", "LanguajesModel", _UserName);
            }
            catch (Exception _error)
            {
                response = false;
                _log._logError(_error.Message, _error.StackTrace, "DeleteLanguaje", "LanguajesModel", _UserName);
            }
            return response;
        }
    }
}
