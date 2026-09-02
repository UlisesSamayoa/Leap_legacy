using LEAP.Data;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace LEAP.Models
{
    public class NotesxMaestroModel
    {
        public int? IDNotesxMaestro { get; set; }
        public string UCI { get; set; }
        public string ConsumerName { get; set; }
        public int RegionalID { get; set; }
        public string RegionalName { get; set; }
        public string TimesxWeek { get; set; }
        public DateTime Date { get; set; }
        public string Duration { get; set; }
        public string PresentInSession { get; set; }
        public string Notes { get; set; }
        public int SpecialitID { get; set; }
        public string SpecialitName { get; set; }
        public string TotalHours { get; set; }
        public string UserC { get; set; }
        public DateTime? DateC { get; set; }
        public string UserU { get; set; }
        public DateTime? DateU { get; set; }
        public int type { get; set; }
        public bool _ErrorCode { get; set; }
        public List<SelectListItem> SelectOptions { get; set; }
        LogModel _log = new LogModel();

        private class ApiNote
        {
            public int IDNotesxMaestro;
            public string UCI;
            public string PresentInSession;
            public string Notes;
            public int? SpecialitID;
            public DateTime? DateC, DateU, Date;
            public string UserC, UserU, Duration;
            public ApiConsumer consumer;
            public ApiSpecialist specialist;
        }
        private class ApiConsumer { public string Name, LastName; public int RegionalID; }
        private class ApiSpecialist { public string Name, LastName; }

        // Replica SP_NotesxMaestro_AllData (join Consumer+Specialist+RegionalCenter).
        public List<NotesxMaestroModel> Get_NotesxMaestro()
        {
            var _NotesxMaestro_Response = new List<NotesxMaestroModel>();
            try
            {
                var raw = ApiClient.Get<List<ApiNote>>("notes-x-maestros");
                var centers = ApiClient.Get<List<RegionalCenterModel>>("regional-centers");
                var centerNames = centers.ToDictionary(c => c.IDRegionalCenter, c => c.RegionalCenter);

                foreach (var n in raw)
                {
                    string regionalName = null;
                    if (n.consumer != null)
                    {
                        centerNames.TryGetValue(n.consumer.RegionalID, out regionalName);
                    }
                    _NotesxMaestro_Response.Add(new NotesxMaestroModel
                    {
                        IDNotesxMaestro = n.IDNotesxMaestro,
                        UCI = n.UCI,
                        ConsumerName = n.consumer != null ? n.consumer.Name + " " + n.consumer.LastName : "",
                        RegionalName = regionalName,
                        Date = n.Date ?? default(DateTime),
                        Duration = n.Duration,
                        PresentInSession = n.PresentInSession,
                        Notes = n.Notes,
                        SpecialitID = n.SpecialitID ?? 0,
                        SpecialitName = n.specialist != null ? n.specialist.Name + " " + n.specialist.LastName : "",
                    });
                }
            }
            catch (Exception)
            {
                _NotesxMaestro_Response.Add(new NotesxMaestroModel { _ErrorCode = true });
            }
            return _NotesxMaestro_Response;
        }

        public NotesxMaestroModel Get_NotesxMaestro_ByID(int _NotesxMaestro)
        {
            var _NotesxMaestro_Response = new NotesxMaestroModel();
            try
            {
                var n = ApiClient.Get<ApiNote>("notes-x-maestros/" + _NotesxMaestro);
                _NotesxMaestro_Response.IDNotesxMaestro = n.IDNotesxMaestro;
                _NotesxMaestro_Response.UCI = n.UCI;
                _NotesxMaestro_Response.ConsumerName = n.consumer != null ? n.consumer.Name + " " + n.consumer.LastName : "";
                _NotesxMaestro_Response.Date = n.Date ?? default(DateTime);
                _NotesxMaestro_Response.Duration = n.Duration;
                _NotesxMaestro_Response.PresentInSession = n.PresentInSession;
                _NotesxMaestro_Response.Notes = n.Notes;
                _NotesxMaestro_Response.SpecialitID = n.SpecialitID ?? 0;
                _NotesxMaestro_Response.SpecialitName = n.specialist != null ? n.specialist.Name + " " + n.specialist.LastName : "";
                _NotesxMaestro_Response._ErrorCode = false;
            }
            catch (Exception)
            {
                _NotesxMaestro_Response._ErrorCode = true;
            }
            return _NotesxMaestro_Response;
        }

        public List<NotesxMaestroModel> Get_NotesxMaestro_ByIDList(int _NotesxMaestro)
        {
            var _NotesxConsumer_Response = new List<NotesxMaestroModel>();
            try
            {
                var consumer = ApiClient.Get<ConsumerModel>("consumers/" + _NotesxMaestro);
                var raw = ApiClient.Get<List<ApiNote>>("notes-x-maestros?uci=" + Uri.EscapeDataString(consumer.UCI ?? ""));
                foreach (var n in raw)
                {
                    _NotesxConsumer_Response.Add(new NotesxMaestroModel
                    {
                        IDNotesxMaestro = n.IDNotesxMaestro,
                        UCI = n.UCI,
                        ConsumerName = n.consumer != null ? n.consumer.Name + " " + n.consumer.LastName : "",
                        Date = n.Date ?? default(DateTime),
                        Duration = n.Duration,
                        PresentInSession = n.PresentInSession,
                        Notes = n.Notes,
                        SpecialitID = n.SpecialitID ?? 0,
                        SpecialitName = n.specialist != null ? n.specialist.Name + " " + n.specialist.LastName : "",
                    });
                }
            }
            catch (Exception)
            {
                _NotesxConsumer_Response.Add(new NotesxMaestroModel { _ErrorCode = true });
            }
            return _NotesxConsumer_Response;
        }

        public bool AddNotesxMaestro(string _UCI, int _SpecialitID, DateTime? _Date, string _Duration, string _PresentInSession, string _Notes, string _UserName)
        {
            bool response = false;
            try
            {
                ApiClient.Post<object>("notes-x-maestros", new
                {
                    UCI = _UCI,
                    SpecialitID = _SpecialitID,
                    Date = _Date,
                    Duration = _Duration,
                    PresentInSession = _PresentInSession,
                    Notes = _Notes,
                });
                response = true;
                _log._logAction("Create NotesxMaestro", "Create a new NotesxMaestro, UCI:" + _UCI, "AddNotesxMaestro", "NotesxMaestroModel", _UserName);
            }
            catch (Exception _error)
            {
                response = false;
                _log._logError(_error.Message, _error.StackTrace, "AddNotesxMaestro", "NotesxMaestroModel", _UserName);
            }
            return response;
        }

        public bool UpdateNotesxMaestro(string _IDNotesxMaestro, string _UCI, int _SpecialitID, string _Duration, DateTime _Date, string _PresentInSession, string _Notes, string _UserName)
        {
            bool response = false;
            try
            {
                ApiClient.Put<object>("notes-x-maestros/" + _IDNotesxMaestro, new
                {
                    UCI = _UCI,
                    SpecialitID = _SpecialitID,
                    Date = _Date,
                    Duration = _Duration,
                    PresentInSession = _PresentInSession,
                    Notes = _Notes,
                });
                response = true;
                _log._logAction("Update NotesxMaestro", "Update NotesxMaestro, ID:" + _IDNotesxMaestro, "UpdateNotesxMaestro", "NotesxMaestroModel", _UserName);
            }
            catch (Exception _error)
            {
                response = false;
                _log._logError(_error.Message, _error.StackTrace, "UpdateNotesxMaestro", "NotesxMaestroModel", _UserName);
            }
            return response;
        }

        public bool DeleteNotesxMaestro(string _IDNotesxMaestro, string _UserName)
        {
            bool response = false;
            try
            {
                ApiClient.Delete("notes-x-maestros/" + _IDNotesxMaestro);
                response = true;
                _log._logAction("Delete NotesxMaestro", "Delete NotesxMaestro, id:" + _IDNotesxMaestro, "DeleteNotesxMaestro", "NotesxMaestroModel", _UserName);
            }
            catch (Exception _error)
            {
                response = false;
                _log._logError(_error.Message, _error.StackTrace, "DeleteNotesxMaestro", "NotesxMaestroModel", _UserName);
            }
            return response;
        }
    }
}
