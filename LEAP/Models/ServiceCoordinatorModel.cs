using LEAP.Data;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace LEAP.Models
{
    public class ServiceCoordinatorModel
    {
        public int? IDServiceC { get; set; }
        public string IDRegionalcenter { get; set; }
        public string NameC { get; set; }
        public string Tel { get; set; }

        public bool _ErrorCode { get; set; }
        LogModel _log = new LogModel();

        public List<ServiceCoordinatorModel> Get_ServiceC()
        {
            var _ServiceC_Response = new List<ServiceCoordinatorModel>();
            try
            {
                _ServiceC_Response = ApiClient.Get<List<ServiceCoordinatorModel>>("service-coordinators");
            }
            catch (Exception)
            {
                _ServiceC_Response.Add(new ServiceCoordinatorModel { _ErrorCode = true });
            }
            return _ServiceC_Response;
        }

        public List<ServiceCoordinatorModel> Get_ServiceCbyID(int id)
        {
            var _ServiceC_Response = new List<ServiceCoordinatorModel>();
            try
            {
                _ServiceC_Response.Add(ApiClient.Get<ServiceCoordinatorModel>("service-coordinators/" + id));
            }
            catch (Exception)
            {
                _ServiceC_Response.Add(new ServiceCoordinatorModel { _ErrorCode = true });
            }
            return _ServiceC_Response;
        }

        public ServiceCoordinatorModel Get_Services_ByID(int id)
        {
            var ServiceResponse = new ServiceCoordinatorModel();
            try
            {
                ServiceResponse = ApiClient.Get<ServiceCoordinatorModel>("service-coordinators/" + id);
            }
            catch (Exception)
            {
                ServiceResponse._ErrorCode = true;
            }
            return ServiceResponse;
        }

        public bool UpdateS(int IDServiceC, string NameC, string Tel, int Regional, string _UserName)
        {
            bool response = false;
            try
            {
                ApiClient.Put<ServiceCoordinatorModel>("service-coordinators/" + IDServiceC, new
                {
                    IDRegionalcenter = Regional,
                    NameC = NameC,
                    Tel = Tel,
                });
                response = true;
                _log._logAction("Update ServiceCoordinator", "Update Service Coordinator, name:" + NameC, "UpdateS", "ServiceCoordinatorModel", _UserName);
            }
            catch (Exception _error)
            {
                response = false;
                _log._logError(_error.Message, _error.StackTrace, "UpdateS", "ServiceCoordinatorModel", _UserName);
            }
            return response;
        }

        public bool DeleteServiceC(int IDServiceC, string _UserName)
        {
            bool response = false;
            try
            {
                ApiClient.Delete("service-coordinators/" + IDServiceC);
                response = true;
                _log._logAction("Delete ServiceCoordinator", "Delete Service Coordinator, id:" + IDServiceC, "DeleteServiceC", "ServiceCoordinatorModel", _UserName);
            }
            catch (Exception _error)
            {
                response = false;
                _log._logError(_error.Message, _error.StackTrace, "DeleteServiceC", "ServiceCoordinatorModel", _UserName);
            }
            return response;
        }

        public bool Create(int regional, string NameC, string Tel)
        {
            bool response = false;
            try
            {
                ApiClient.Post<ServiceCoordinatorModel>("service-coordinators", new
                {
                    IDRegionalcenter = regional,
                    NameC = NameC,
                    Tel = Tel,
                });
                response = true;
            }
            catch (Exception)
            {
                response = false;
            }
            return response;
        }
    }
}
