using LEAP.Data;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LEAP.Models
{
    public class EventsModel
    {
        public int? IDEvent { get; set; }
        public string Title { get; set; }
        public DateTime DateEvent { get; set; }
        public string Description { get; set; }
        public int IDType { get; set; }
        public string TypeName { get; set; }
        public string Color { get; set; }
        public string UserC { get; set; }
        public DateTime? DateC { get; set; }
        public string UserU { get; set; }
        public DateTime? DateU { get; set; }
        public bool _ErrorCode { get; set; }

        LogModel _log = new LogModel();

        private class ApiEvent
        {
            public int IDEvent;
            public string Title;
            public DateTime? DateEvent;
            public string Description;
            public int? IDType;
            public EventTypeModel eventType;
        }

        public List<EventsModel> Get_Events()
        {
            var _Events_Response = new List<EventsModel>();
            try
            {
                var raw = ApiClient.Get<List<ApiEvent>>("events");
                foreach (var e in raw)
                {
                    _Events_Response.Add(new EventsModel
                    {
                        IDEvent = e.IDEvent,
                        Title = e.Title,
                        DateEvent = e.DateEvent ?? default(DateTime),
                        Description = e.Description,
                        IDType = e.IDType ?? 0,
                        TypeName = e.eventType?.Title,
                        Color = e.eventType?.Color,
                        _ErrorCode = false,
                    });
                }
            }
            catch (Exception)
            {
                _Events_Response.Add(new EventsModel { _ErrorCode = true });
            }
            return _Events_Response;
        }

        public EventsModel Get_Events_ByID(int _Events)
        {
            var _Events_Response = new EventsModel();
            try
            {
                var e = ApiClient.Get<ApiEvent>("events/" + _Events);
                _Events_Response.IDEvent = e.IDEvent;
                _Events_Response.Title = e.Title;
                _Events_Response.DateEvent = e.DateEvent ?? default(DateTime);
                _Events_Response.Description = e.Description;
                _Events_Response.IDType = e.IDType ?? 0;
                _Events_Response.TypeName = e.eventType?.Title;
                _Events_Response.Color = e.eventType?.Color;
                _Events_Response._ErrorCode = false;
            }
            catch (Exception)
            {
                _Events_Response._ErrorCode = true;
            }
            return _Events_Response;
        }

        public EventsModel Get_Events_ByEventType(string _EventType)
        {
            var _Events_Response = new EventsModel();
            try
            {
                var e = ApiClient.Get<List<ApiEvent>>("events?type=" + _EventType).FirstOrDefault();
                if (e != null)
                {
                    _Events_Response.IDEvent = e.IDEvent;
                    _Events_Response.Title = e.Title;
                    _Events_Response.DateEvent = e.DateEvent ?? default(DateTime);
                    _Events_Response.Description = e.Description;
                    _Events_Response.IDType = e.IDType ?? 0;
                    _Events_Response.TypeName = e.eventType?.Title;
                    _Events_Response.Color = e.eventType?.Color;
                }
                _Events_Response._ErrorCode = false;
            }
            catch (Exception)
            {
                _Events_Response._ErrorCode = true;
            }
            return _Events_Response;
        }

        public bool AddEvent(string _Title, DateTime _DateEvent, string _Description, int _Type, string _UserName)
        {
            bool response = false;
            try
            {
                ApiClient.Post<object>("events", new { Title = _Title, DateEvent = _DateEvent, Description = _Description, IDType = _Type });
                response = true;
                _log._logAction("Create Event", "Create a new Event, name:" + _Title, "AddEvent", "EventsModel", _UserName);
            }
            catch (Exception _error)
            {
                response = false;
                _log._logError(_error.Message, _error.StackTrace, "AddEvent", "EventsModel", _UserName);
            }
            return response;
        }

        public bool UpdateEvent(string _IDEvent, string _Title, DateTime _DateEvent, string _Description, int _Type, string _UserName)
        {
            bool response = false;
            try
            {
                ApiClient.Put<object>("events/" + _IDEvent, new { Title = _Title, DateEvent = _DateEvent, Description = _Description, IDType = _Type });
                response = true;
                _log._logAction("Update Event", "Update Event, name:" + _Title, "UpdateEvent", "EventsModel", _UserName);
            }
            catch (Exception _error)
            {
                response = false;
                _log._logError(_error.Message, _error.StackTrace, "UpdateEvent", "EventsModel", _UserName);
            }
            return response;
        }

        public bool DeleteEvent(string _IDEvent, string _UserName)
        {
            bool response = false;
            try
            {
                ApiClient.Delete("events/" + _IDEvent);
                response = true;
                _log._logAction("Delete Events", "Delete Events, id:" + _IDEvent, "DeleteEvent", "EventsModel", _UserName);
            }
            catch (Exception _error)
            {
                response = false;
                _log._logError(_error.Message, _error.StackTrace, "DeleteEvent", "EventsModel", _UserName);
            }
            return response;
        }

        // SP_Events_Delete_EventType en realidad borra por IDEvent (mismo
        // comportamiento que DeleteEvent) — ver nota en leap_api routes/api.php.
        public bool DeleteEventxEvenType(string _EventType, string _UserName)
        {
            bool response = false;
            try
            {
                ApiClient.Delete("events/" + _EventType);
                response = true;
                _log._logAction("Delete Events", "Delete Events, id:" + _EventType, "DeleteEvent", "EventsModel", _UserName);
            }
            catch (Exception _error)
            {
                response = false;
                _log._logError(_error.Message, _error.StackTrace, "DeleteEvent", "EventsModel", _UserName);
            }
            return response;
        }
    }
}
