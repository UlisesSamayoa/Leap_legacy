using LEAP.Data;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace LEAP.Models
{
    public class CitiesModel
    {
        [JsonProperty("IDCity")]
        public int? IDCity { get; set; }
        [JsonProperty("City")]
        public string City { get; set; }
        [JsonProperty("State")]
        public string State { get; set; }
        [JsonProperty("UserC")]
        public string UserC { get; set; }
        [JsonProperty("DateC")]
        public DateTime? DateC { get; set; }
        [JsonProperty("UserU")]
        public string UserU { get; set; }
        [JsonProperty("DateU")]
        public DateTime? DateU { get; set; }
        public bool _ErrorCode { get; set; }
        LogModel _log = new LogModel();

        public List<CitiesModel> Get_Cities()
        {
            var _Cities_Response = new List<CitiesModel>();
            try
            {
                _Cities_Response = ApiClient.Get<List<CitiesModel>>("cities");
            }
            catch (Exception)
            {
                _Cities_Response.Add(new CitiesModel { _ErrorCode = true });
            }
            return _Cities_Response;
        }

        public CitiesModel Get_Cities_ByID(int _city)
        {
            var _Cities_Response = new CitiesModel();
            try
            {
                _Cities_Response = ApiClient.Get<CitiesModel>("cities/" + _city);
                _Cities_Response._ErrorCode = false;
            }
            catch (Exception)
            {
                _Cities_Response._ErrorCode = true;
            }
            return _Cities_Response;
        }

        public bool AddCity(string _City, string _State, string _UserName)
        {
            bool response = false;
            try
            {
                ApiClient.Post<CitiesModel>("cities", new { City = _City, State = _State });
                response = true;
                _log._logAction("Create City", "Create a new City, name:" + _City, "AddCity", "CityModel", _UserName);
            }
            catch (Exception _error)
            {
                response = false;
                _log._logError(_error.Message, _error.StackTrace, "AddCity", "CityModel", _UserName);
            }
            return response;
        }

        public bool UpdateCity(string _IDCity, string _City, string _State, string _UserName)
        {
            bool response = false;
            try
            {
                ApiClient.Put<CitiesModel>("cities/" + _IDCity, new { City = _City, State = _State });
                response = true;
                _log._logAction("Update City", "Update City, name:" + _City, "UpdateCity", "CityModel", _UserName);
            }
            catch (Exception _error)
            {
                response = false;
                _log._logError(_error.Message, _error.StackTrace, "UpdateCity", "CityModel", _UserName);
            }
            return response;
        }

        public bool DeleteCity(string _IDCity, string _UserName)
        {
            bool response = false;
            try
            {
                ApiClient.Delete("cities/" + _IDCity);
                response = true;
                _log._logAction("Delete City", "Delete City, id:" + _IDCity, "DeleteCity", "CityModel", _UserName);
            }
            catch (Exception _error)
            {
                response = false;
                _log._logError(_error.Message, _error.StackTrace, "DeleteCity", "CityModel", _UserName);
            }
            return response;
        }
    }
}
