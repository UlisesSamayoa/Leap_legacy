using LEAP.Data;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace LEAP.Models
{
    public class EventTypeModel
    {
        public int? IDEventType { get; set; }
        public string Title { get; set; }
        public string Color { get; set; }
        public string UserC { get; set; }
        public DateTime? DateC { get; set; }
        public string UserU { get; set; }
        public DateTime? DateU { get; set; }
        public bool _ErrorCode { get; set; }

        LogModel _log = new LogModel();

        public List<EventTypeModel> Get_EventType()
        {
            var _EventType_Response = new List<EventTypeModel>();
            try
            {
                _EventType_Response = ApiClient.Get<List<EventTypeModel>>("event-types");
            }
            catch (Exception)
            {
                _EventType_Response.Add(new EventTypeModel { _ErrorCode = true });
            }
            return _EventType_Response;
        }

        public EventTypeModel Get_EventType_ByID(int _EventType)
        {
            var _EventType_Response = new EventTypeModel();
            try
            {
                _EventType_Response = ApiClient.Get<EventTypeModel>("event-types/" + _EventType);
                _EventType_Response._ErrorCode = false;
            }
            catch (Exception)
            {
                _EventType_Response._ErrorCode = true;
            }
            return _EventType_Response;
        }

        public bool AddEventType(string _Title, string _Color, string _UserName)
        {
            bool response = false;
            try
            {
                ApiClient.Post<EventTypeModel>("event-types", new { Title = _Title, Color = _Color });
                response = true;
                _log._logAction("Create Event", "Create a new Event, name:" + _Title, "AddEvent", "EventTypeModel", _UserName);
            }
            catch (Exception _error)
            {
                response = false;
                _log._logError(_error.Message, _error.StackTrace, "AddEvent", "EventTypeModel", _UserName);
            }
            return response;
        }

        public bool UpdateEventType(string _IDEventType, string _Title, string _Color, string _UserName)
        {
            bool response = false;
            try
            {
                ApiClient.Put<EventTypeModel>("event-types/" + _IDEventType, new { Title = _Title, Color = _Color });
                response = true;
                _log._logAction("Update Event", "Update Event, name:" + _Title, "UpdateEvent", "EventTypeModel", _UserName);
            }
            catch (Exception _error)
            {
                response = false;
                _log._logError(_error.Message, _error.StackTrace, "UpdateEvent", "EventTypeModel", _UserName);
            }
            return response;
        }

        public bool DeleteEventType(string _IDEventType, string _UserName)
        {
            bool response = false;
            try
            {
                ApiClient.Delete("event-types/" + _IDEventType);
                response = true;
                _log._logAction("Delete EventType", "Delete EventType, id:" + _IDEventType, "DeleteEvent", "EventTypeModel", _UserName);
            }
            catch (Exception _error)
            {
                response = false;
                _log._logError(_error.Message, _error.StackTrace, "DeleteEvent", "EventTypeModel", _UserName);
            }
            return response;
        }
    }
}
