using LEAP.Data;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace LEAP.Models
{
    public class SpecialistModel
    {
        public int? IDSpecialist { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string ZipCode { get; set; }
        public string HourlyRate { get; set; }
        public string Specialty { get; set; }
        public string UserC { get; set; }
        public DateTime? DateC { get; set; }
        public string UserU { get; set; }
        public DateTime? DateU { get; set; }
        public bool _ErrorCode { get; set; }

        LogModel _log = new LogModel();

        public List<SpecialistModel> Get_Specialist()
        {
            var _RCenter_Response = new List<SpecialistModel>();
            try
            {
                _RCenter_Response = ApiClient.Get<List<SpecialistModel>>("specialists");
            }
            catch (Exception)
            {
                _RCenter_Response.Add(new SpecialistModel { _ErrorCode = true });
            }
            return _RCenter_Response;
        }

        public SpecialistModel Get_Specialist_ByID(int _Specialist)
        {
            var _specialist_Response = new SpecialistModel();
            try
            {
                _specialist_Response = ApiClient.Get<SpecialistModel>("specialists/" + _Specialist);
                _specialist_Response._ErrorCode = false;
            }
            catch (Exception)
            {
                _specialist_Response._ErrorCode = true;
            }
            return _specialist_Response;
        }

        public bool AddSpecialist(string _Name, string _LastName, string _Phone, string _Email, string _Address, string _City, string _State, string _ZipCode, string _HourlyRate, string Specialty, string _UserName)
        {
            bool response = false;
            try
            {
                ApiClient.Post<SpecialistModel>("specialists", new
                {
                    Name = _Name,
                    LastName = _LastName,
                    Phone = _Phone,
                    Email = _Email,
                    Address = _Address,
                    City = _City,
                    State = _State,
                    ZipCode = _ZipCode,
                    HourlyRate = _HourlyRate,
                    Specialty = Specialty,
                });
                response = true;
                _log._logAction("Create Specialist", "Create a new Specialist, name:" + _Name + " " + _LastName, "AddSpecialist", "SpecialistModel", _UserName);
            }
            catch (Exception _error)
            {
                response = false;
                _log._logError(_error.Message, _error.StackTrace, "AddSpecialist", "SpecialistModel", _UserName);
            }
            return response;
        }

        public bool UpdateSpecialist(string _IDSpecialist, string _Name, string _LastName, string _Phone, string _Email, string _Address, string _City, string _State, string _ZipCode, string _HourlyRate, string Specialty, string _UserName)
        {
            bool response = false;
            try
            {
                ApiClient.Put<SpecialistModel>("specialists/" + _IDSpecialist, new
                {
                    Name = _Name,
                    LastName = _LastName,
                    Phone = _Phone,
                    Email = _Email,
                    Address = _Address,
                    City = _City,
                    State = _State,
                    ZipCode = _ZipCode,
                    HourlyRate = _HourlyRate,
                    Specialty = Specialty,
                });
                response = true;
                _log._logAction("Update Specialist", "Update Specialist, name:" + Name + " " + LastName, "UpdateSpecialist", "SpecialistModel", _UserName);
            }
            catch (Exception _error)
            {
                response = false;
                _log._logError(_error.Message, _error.StackTrace, "UpdateSpecialist", "SpecialistModel", _UserName);
            }
            return response;
        }

        public bool DeleteSpecialist(string _IDSpecialist, string _UserName)
        {
            bool response = false;
            try
            {
                ApiClient.Delete("specialists/" + _IDSpecialist);
                response = true;
                _log._logAction("Delete Specialist", "Delete Specialist, id:" + IDSpecialist, "DeleteSpecialist", "SpecialistModel", _UserName);
            }
            catch (Exception _error)
            {
                response = false;
                _log._logError(_error.Message, _error.StackTrace, "DeleteSpecialist", "SpecialistModel", _UserName);
            }
            return response;
        }
    }
}
