using LEAP.Data;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LEAP.Models
{
    public class RegionalCenterModel
    {
        public int IDRegionalCenter { get; set; }
        public string RegionalCenter { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string ZipCode { get; set; }
        public string Ext { get; set; }
        public string UserC { get; set; }
        public DateTime? DateC { get; set; }
        public string UserU { get; set; }
        public DateTime? DateU { get; set; }
        public string serviceC { get; set; }
        public string telephone { get; set; }
        public bool _ErrorCode { get; set; }
        public int IDServiceC { get; set; }

        LogModel _log = new LogModel();

        public List<RegionalCenterModel> Get_RegionalCenter()
        {
            var _RCenter_Response = new List<RegionalCenterModel>();
            try
            {
                _RCenter_Response = ApiClient.Get<List<RegionalCenterModel>>("regional-centers");
            }
            catch (Exception)
            {
                _RCenter_Response.Add(new RegionalCenterModel { _ErrorCode = true });
            }
            return _RCenter_Response;
        }

        // Combina RegionalCenter + sus ServiceCoordinator (antes era un JOIN en
        // Get_ConsumerByID, un nombre de SP heredado y confuso pero real: por
        // cada coordinador del centro regional se arma una fila).
        public List<RegionalCenterModel> Get_RegionalByID(int _IDRegionalCenter)
        {
            var productos = new List<RegionalCenterModel>();
            try
            {
                var center = ApiClient.Get<RegionalCenterModel>("regional-centers/" + _IDRegionalCenter);
                var coordinators = ApiClient.Get<List<ServiceCoordinatorModel>>("service-coordinators?regional_center_id=" + _IDRegionalCenter);

                foreach (var coord in coordinators)
                {
                    productos.Add(new RegionalCenterModel
                    {
                        IDRegionalCenter = center.IDRegionalCenter,
                        RegionalCenter = center.RegionalCenter,
                        Phone = center.Phone,
                        Address = center.Address,
                        City = center.City,
                        State = center.State,
                        ZipCode = center.ZipCode,
                        Ext = center.Ext,
                        serviceC = coord.NameC,
                        telephone = coord.Tel,
                        IDServiceC = coord.IDServiceC ?? 0,
                    });
                }
            }
            catch (Exception)
            {
                throw;
            }
            return productos;
        }

        public RegionalCenterModel Get_RegionalCenter_ByID(int _RCenterID)
        {
            var _RCenter_Response = new RegionalCenterModel();
            try
            {
                _RCenter_Response = ApiClient.Get<RegionalCenterModel>("regional-centers/" + _RCenterID);
                _RCenter_Response._ErrorCode = false;
            }
            catch (Exception)
            {
                _RCenter_Response._ErrorCode = true;
            }
            return _RCenter_Response;
        }

        public bool AddRegionalCenter(string _RegionalCenter, string _Phone, string _Address, string _City, string _State, string _ZipCode, string _Ext, string _UserName)
        {
            bool response = false;
            try
            {
                ApiClient.Post<RegionalCenterModel>("regional-centers", new
                {
                    RegionalCenter = _RegionalCenter,
                    Phone = _Phone,
                    Address = _Address,
                    City = _City,
                    State = _State,
                    ZipCode = _ZipCode,
                    Ext = _Ext,
                });
                response = true;
                _log._logAction("Create RegionalCenter", "Create a new Regional Center, name:" + _RegionalCenter, "AddRegionalCenter", "RegionalCenterModel", _UserName);
            }
            catch (Exception _error)
            {
                response = false;
                _log._logError(_error.Message, _error.StackTrace, "AddRegionalCenter", "RegionalCenterModel", _UserName);
            }
            return response;
        }

        public bool UpdateRegionalCenter(string _IDRegionalCenter, string _RegionalCenter, string _Phone, string _Address, string _City, string _State, string _ZipCode, string _Ext, string _UserName)
        {
            bool response = false;
            try
            {
                ApiClient.Put<RegionalCenterModel>("regional-centers/" + _IDRegionalCenter, new
                {
                    RegionalCenter = _RegionalCenter,
                    Phone = _Phone,
                    Address = _Address,
                    City = _City,
                    State = _State,
                    ZipCode = _ZipCode,
                    Ext = _Ext,
                });
                response = true;
                _log._logAction("Update RegionalCenter", "Update Regional Center, name:" + _RegionalCenter, "UpdateRegionalCenter", "RegionalCenterModel", _UserName);
            }
            catch (Exception _error)
            {
                response = false;
                _log._logError(_error.Message, _error.StackTrace, "UpdateRegionalCenter", "RegionalCenterModel", _UserName);
            }
            return response;
        }

        public bool DeleteRegionalCenter(string _IDRegionalCenter, string _UserName)
        {
            bool response = false;
            try
            {
                ApiClient.Delete("regional-centers/" + _IDRegionalCenter);
                response = true;
                _log._logAction("Delete RegionalCenter", "Delete Regional Center, id:" + IDRegionalCenter, "DeleteRegionalCenter", "RegionalCenterModel", _UserName);
            }
            catch (Exception _error)
            {
                response = false;
                _log._logError(_error.Message, _error.StackTrace, "DeleteRegionalCenter", "RegionalCenterModel", _UserName);
            }
            return response;
        }
    }
}
