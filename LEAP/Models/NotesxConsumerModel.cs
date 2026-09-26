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

        // Poblado por Newtonsoft cuando leap_api hace eager-load de la relacion
        // consumer() (NotesxConsumerController::index/show en leap_api). Reemplaza
        // el loop de AttachConsumerNames(), que antes hacia una llamada HTTP por
        // cada UCI distinto (con 33k+ notas / miles de UCIs, la pantalla mas lenta
        // de la app).
        public ConsumerRefModel consumer { get; set; }

        public class ConsumerRefModel
        {
            public string Name { get; set; }
            public string LastName { get; set; }
        }

        private static void ApplyConsumerNames(IEnumerable<NotesxConsumerModel> notes)
        {
            foreach (var n in notes)
            {
                if (n.consumer != null)
                {
                    n.ConsumerName = (n.consumer.Name + " " + n.consumer.LastName).Trim();
                }
            }
        }

        public List<NotesxConsumerModel> Get_NotesxConsumer()
        {
            var _NotesxConsumer_Response = new List<NotesxConsumerModel>();
            try
            {
                _NotesxConsumer_Response = ApiClient.Get<List<NotesxConsumerModel>>("notes-x-consumers");
                ApplyConsumerNames(_NotesxConsumer_Response);
                _NotesxConsumer_Response = _NotesxConsumer_Response.OrderByDescending(n => n.DateC).ToList();
            }
            catch (Exception)
            {
                _NotesxConsumer_Response.Add(new NotesxConsumerModel { _ErrorCode = true });
            }
            return _NotesxConsumer_Response;
        }

        // Forma de respuesta del paginate() de Laravel (leap_api). Newtonsoft
        // ignora el resto de campos del paginator (links, from, to, etc.) que no
        // se necesitan aqui.
        public class PagedResult
        {
            public List<NotesxConsumerModel> data { get; set; }
            public int total { get; set; }
            public int current_page { get; set; }
            public int per_page { get; set; }
        }

        // Usado por el grid principal (NotesxConsumerController.GridData, modo
        // server-side de DataTables) - con 33k+ notas, traer TODO de una
        // (Get_NotesxConsumer) hacia que leap_api gastara ~9s solo en
        // json_encode de filas que la pantalla ni siquiera iba a mostrar.
        public PagedResult Get_NotesxConsumer_Paged(int page, int perPage, string search, string sort, string dir)
        {
            var url = "notes-x-consumers?paginate=1&page=" + page + "&per_page=" + perPage;
            if (!string.IsNullOrEmpty(sort))
            {
                url += "&sort=" + Uri.EscapeDataString(sort) + "&dir=" + Uri.EscapeDataString(dir ?? "desc");
            }
            if (!string.IsNullOrEmpty(search))
            {
                url += "&search=" + Uri.EscapeDataString(search);
            }

            var result = ApiClient.Get<PagedResult>(url);
            ApplyConsumerNames(result.data);
            return result;
        }

        public NotesxConsumerModel Get_NotesxConsumer_ByID(int _NotesxConsumer)
        {
            var _NotesxConsumer_Response = new NotesxConsumerModel();
            try
            {
                _NotesxConsumer_Response = ApiClient.Get<NotesxConsumerModel>("notes-x-consumers/" + _NotesxConsumer);
                ApplyConsumerNames(new List<NotesxConsumerModel> { _NotesxConsumer_Response });
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
                ApplyConsumerNames(_NotesxConsumer_Response);
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
    }
}
