using LEAP.Data;
using System;
using System.Collections.Generic;

namespace LEAP.Models
{
    public class CalendarModel
    {
        public int EventoId { get; set; }
        public string title { get; set; }
        public string start { get; set; }
        public string start_o { get; set; }
        public string end { get; set; }
        public string description { get; set; }
        public string color { get; set; }
        public bool _ErrorCode { get; set; }

        private class ApiEvent
        {
            public int IDEvent;
            public string Title;
            public DateTime? DateEvent;
            public string Description;
            public EventTypeModel eventType;
        }

        public List<CalendarModel> Get_Events()
        {
            var _RCenter_Response = new List<CalendarModel>();
            try
            {
                var raw = ApiClient.Get<List<ApiEvent>>("events");
                foreach (var e in raw)
                {
                    var when = e.DateEvent ?? default(DateTime);
                    _RCenter_Response.Add(new CalendarModel
                    {
                        EventoId = e.IDEvent,
                        title = e.Title,
                        start = when.ToString("yyyy-MM-ddTHH:mm:ss"),
                        start_o = when.ToString("yyyy-MM-dd"),
                        end = when.ToString("yyyy-MM-ddTHH:mm:ss"),
                        description = e.Description,
                        color = e.eventType?.Color,
                        _ErrorCode = false,
                    });
                }
            }
            catch (Exception)
            {
                _RCenter_Response.Add(new CalendarModel { _ErrorCode = true });
            }
            return _RCenter_Response;
        }
    }
}
