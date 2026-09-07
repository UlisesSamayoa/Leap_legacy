using LEAP.Data;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace LEAP.Models
{
    // Alimenta el modulo "NotesXTeacher" (nombres de clase/vistas se dejan
    // igual a proposito para no romper URLs/menus que el staff ya conoce),
    // pero por debajo ahora habla contra "visit-reports" (leap_client), que
    // reemplazo a la tabla legacy NotesxMaestro como fuente de esta pantalla
    // y del reporte de notas por consumer. Creacion queda solo desde la app;
    // aqui unicamente se puede Ver/Editar/Borrar.
    public class NotesxMaestroModel
    {
        public int? IDNotesxMaestro { get; set; }
        public string UCI { get; set; }
        public string ConsumerName { get; set; }
        public int RegionalID { get; set; }
        public string RegionalName { get; set; }
        public string TimesxWeek { get; set; }
        public DateTime Date { get; set; }
        public DateTime DepartureDate { get; set; }
        public string Duration { get; set; }
        public string PresentInSession { get; set; }
        public string Notes { get; set; }
        public string Signature { get; set; }
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

        private class ApiVisit
        {
            public int id;
            public int specialist_id;
            public DateTime? session_started_at, session_ended_at;
            public int? duration_minutes;
            public string present_in_session, times_per_week, notes, signature;
            public ApiConsumer consumer;
            public ApiSpecialist specialist;
        }
        private class ApiConsumer { public string UCI, Name, LastName; public int RegionalID; }
        private class ApiSpecialist { public string Name, LastName; }

        private static string FormatDuration(int? minutes) => minutes.HasValue ? minutes.Value + " min" : "";

        private static NotesxMaestroModel Map(ApiVisit n, string regionalName = null)
        {
            return new NotesxMaestroModel
            {
                IDNotesxMaestro = n.id,
                UCI = n.consumer?.UCI,
                ConsumerName = n.consumer != null ? n.consumer.Name + " " + n.consumer.LastName : "",
                RegionalName = regionalName,
                Date = n.session_started_at ?? default(DateTime),
                DepartureDate = n.session_ended_at ?? default(DateTime),
                Duration = FormatDuration(n.duration_minutes),
                PresentInSession = n.present_in_session,
                TimesxWeek = n.times_per_week,
                Notes = n.notes,
                Signature = n.signature,
                SpecialitID = n.specialist_id,
                SpecialitName = n.specialist != null ? n.specialist.Name + " " + n.specialist.LastName : "",
            };
        }

        // Replica (con otra fuente) SP_NotesxMaestro_AllData.
        public List<NotesxMaestroModel> Get_NotesxMaestro()
        {
            var _NotesxMaestro_Response = new List<NotesxMaestroModel>();
            try
            {
                var raw = ApiClient.Get<List<ApiVisit>>("visit-reports");
                var centers = ApiClient.Get<List<RegionalCenterModel>>("regional-centers");
                var centerNames = centers.ToDictionary(c => c.IDRegionalCenter, c => c.RegionalCenter);

                foreach (var n in raw)
                {
                    string regionalName = null;
                    if (n.consumer != null)
                    {
                        centerNames.TryGetValue(n.consumer.RegionalID, out regionalName);
                    }
                    _NotesxMaestro_Response.Add(Map(n, regionalName));
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
                var n = ApiClient.Get<ApiVisit>("visit-reports/" + _NotesxMaestro);
                _NotesxMaestro_Response = Map(n);
                _NotesxMaestro_Response._ErrorCode = false;
            }
            catch (Exception)
            {
                _NotesxMaestro_Response._ErrorCode = true;
            }
            return _NotesxMaestro_Response;
        }

        public List<NotesxMaestroModel> Get_NotesxMaestro_ByIDList(int _IDConsumer)
        {
            var _NotesxConsumer_Response = new List<NotesxMaestroModel>();
            try
            {
                var raw = ApiClient.Get<List<ApiVisit>>("visit-reports?consumer_id=" + _IDConsumer);
                foreach (var n in raw)
                {
                    _NotesxConsumer_Response.Add(Map(n));
                }
            }
            catch (Exception)
            {
                _NotesxConsumer_Response.Add(new NotesxMaestroModel { _ErrorCode = true });
            }
            return _NotesxConsumer_Response;
        }

        public bool UpdateNotesxMaestro(string _ID, DateTime _SessionStartedAt, DateTime _SessionEndedAt, string _PresentInSession, string _TimesxWeek, string _Notes, string _Signature, string _UserName)
        {
            bool response = false;
            try
            {
                ApiClient.Put<object>("visit-reports/" + _ID, new
                {
                    session_started_at = _SessionStartedAt,
                    session_ended_at = _SessionEndedAt,
                    present_in_session = _PresentInSession,
                    times_per_week = _TimesxWeek,
                    notes = _Notes,
                    signature = _Signature,
                });
                response = true;
                _log._logAction("Update VisitReport", "Update VisitReport (NotesXTeacher), ID:" + _ID, "UpdateNotesxMaestro", "NotesxMaestroModel", _UserName);
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
                ApiClient.Delete("visit-reports/" + _IDNotesxMaestro);
                response = true;
                _log._logAction("Delete VisitReport", "Delete VisitReport (NotesXTeacher), id:" + _IDNotesxMaestro, "DeleteNotesxMaestro", "NotesxMaestroModel", _UserName);
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
