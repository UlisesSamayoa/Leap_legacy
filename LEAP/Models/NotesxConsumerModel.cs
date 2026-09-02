using LEAP.Data;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace LEAP.Models
{
    public class NotesxConsumerModel
    {
        public int? IDNotesxConsumer { get; set; }
        public string UCI { get; set; }
        public string ConsumerName { get; set; }
        public string Notes { get; set; }
        public bool Active { get; set; }
        public string UserC { get; set; }
        public DateTime? DateC { get; set; }
        public string UserU { get; set; }
        public DateTime? DateU { get; set; }
        public bool _ErrorCode { get; set; }
        public int type { get; set; }
        public List<SelectListItem> SelectOptions { get; set; }
        LogModel _log = new LogModel();

        public List<NotesxConsumerModel> Get_NotesxConsumer()
        {
            var _NotesxConsumer_Response = new List<NotesxConsumerModel>();
            try
            {
                _NotesxConsumer_Response = ApiClient.Get<List<NotesxConsumerModel>>("notes-x-consumers");
                AttachConsumerNames(_NotesxConsumer_Response);
                _NotesxConsumer_Response = _NotesxConsumer_Response.OrderByDescending(n => n.DateC).ToList();
            }
            catch (Exception)
            {
                _NotesxConsumer_Response.Add(new NotesxConsumerModel { _ErrorCode = true });
            }
            return _NotesxConsumer_Response;
        }

        public NotesxConsumerModel Get_NotesxConsumer_ByID(int _NotesxConsumer)
        {
            var _NotesxConsumer_Response = new NotesxConsumerModel();
            try
            {
                _NotesxConsumer_Response = ApiClient.Get<NotesxConsumerModel>("notes-x-consumers/" + _NotesxConsumer);
                AttachConsumerNames(new List<NotesxConsumerModel> { _NotesxConsumer_Response });
                _NotesxConsumer_Response._ErrorCode = false;
            }
            catch (Exception)
            {
                _NotesxConsumer_Response._ErrorCode = true;
            }
            return _NotesxConsumer_Response;
        }

        public List<NotesxConsumerModel> Get_NotesxConsumer_ByIDList(int _NotesxConsumer)
        {
            var _NotesxConsumer_Response = new List<NotesxConsumerModel>();
            try
            {
                var consumer = ApiClient.Get<ConsumerModel>("consumers/" + _NotesxConsumer);
                _NotesxConsumer_Response = ApiClient.Get<List<NotesxConsumerModel>>("notes-x-consumers?uci=" + Uri.EscapeDataString(consumer.UCI ?? ""));
                AttachConsumerNames(_NotesxConsumer_Response);
            }
            catch (Exception)
            {
                _NotesxConsumer_Response.Add(new NotesxConsumerModel { _ErrorCode = true });
            }
            return _NotesxConsumer_Response;
        }

        public bool AddNotesxConsumer(string _UCI, string _Notes, bool _Active, string _UserName)
        {
            bool response = false;
            try
            {
                ApiClient.Post<NotesxConsumerModel>("notes-x-consumers", new { UCI = _UCI, Notes = _Notes, Active = _Active });
                response = true;
                _log._logAction("Create NotesxConsumer", "Create a new NotesxConsumer, UCI:" + _UCI, "AddNotesxConsumer", "NotesxConsumerModel", _UserName);
            }
            catch (Exception _error)
            {
                response = false;
                _log._logError(_error.Message, _error.StackTrace, "AddNotesxConsumer", "NotesxConsumerModel", _UserName);
            }
            return response;
        }

        // La regla "una nota activa por UCI" ya se aplica de forma atomica en
        // leap_api al crear/actualizar; aqui se replica el mismo chequeo de
        // solo-lectura que hacia la SP original, para que el flujo del
        // Controller (que llama esto ANTES de Add/Update) no cambie.
        public bool ValidarNotesConsumerActive(int? _ID, string _UCI, bool _Active, string _UserName)
        {
            bool response = false;
            try
            {
                var notes = ApiClient.Get<List<NotesxConsumerModel>>("notes-x-consumers?uci=" + Uri.EscapeDataString(_UCI));
                var activeNote = notes.FirstOrDefault(n => n.Active);

                if (activeNote != null)
                {
                    if (_ID > 0)
                    {
                        response = activeNote.Active == _Active && _Active && _ID != activeNote.IDNotesxConsumer;
                    }
                    else
                    {
                        response = activeNote.Active == _Active && _Active;
                    }
                }
                _log._logAction("validar Active", "Validate a NotesxConsumer, UCI:" + _UCI, "ValidarNotesConsumerActive", "NotesxConsumerModel", _UserName);
            }
            catch (Exception _error)
            {
                response = false;
                _log._logError(_error.Message, _error.StackTrace, "ValidarNotesConsumerActive", "NotesxConsumerModel", _UserName);
            }
            return response;
        }

        public bool UpdateNotesxConsumer(string _IDNotesxConsumer, string _UCI, string _Notes, bool _Active, string _UserName)
        {
            bool response = false;
            try
            {
                ApiClient.Put<NotesxConsumerModel>("notes-x-consumers/" + _IDNotesxConsumer, new { UCI = _UCI, Notes = _Notes, Active = _Active });
                response = true;
                _log._logAction("Update NotesxConsumer", "Update NotesxConsumer, ID:" + _IDNotesxConsumer, "UpdateNotesxConsumer", "NotesxConsumerModel", _UserName);
            }
            catch (Exception _error)
            {
                response = false;
                _log._logError(_error.Message, _error.StackTrace, "UpdateNotesxConsumer", "NotesxConsumerModel", _UserName);
            }
            return response;
        }

        public bool DeleteNotesxConsumer(string _IDNotesxConsumer, string _UserName)
        {
            bool response = false;
            try
            {
                ApiClient.Delete("notes-x-consumers/" + _IDNotesxConsumer);
                response = true;
                _log._logAction("Delete NotesxConsumer", "Delete NotesxConsumer, id:" + _IDNotesxConsumer, "DeleteNotesxConsumer", "NotesxConsumerModel", _UserName);
            }
            catch (Exception _error)
            {
                response = false;
                _log._logError(_error.Message, _error.StackTrace, "DeleteNotesxConsumer", "NotesxConsumerModel", _UserName);
            }
            return response;
        }

        private void AttachConsumerNames(List<NotesxConsumerModel> notes)
        {
            var ucis = notes.Select(n => n.UCI).Where(u => !string.IsNullOrEmpty(u)).Distinct();
            foreach (var uci in ucis)
            {
                var match = ApiClient.Get<List<ConsumerModel>>("consumers?uci=" + Uri.EscapeDataString(uci)).FirstOrDefault();
                if (match == null) continue;
                foreach (var n in notes.Where(n => n.UCI == uci))
                {
                    n.ConsumerName = match.Name + " " + match.LastName;
                }
            }
        }
    }
}
