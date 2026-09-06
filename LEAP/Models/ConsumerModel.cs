using LEAP.Data;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace LEAP.Models
{
    public class ConsumerModel
    {
        [Key]
        public int? IDConsumer { get; set; }
        public string UCI { get; set; }
        public int? specialist1 { get; set; }
        public int? specialist2 { get; set; }
        public int? specialist3 { get; set; }
        public string PresenterName { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Age { get; set; }
        public DateTime adjage { get; set; }
        public string Gender { get; set; }
        public string Address { get; set; }
        public string CityID { get; set; }
        public string State { get; set; }
        public string ZipCode { get; set; }
        public string Phone { get; set; }
        public string EmergencyPhone { get; set; }
        public string ParentName { get; set; }
        public string ParentLastName { get; set; }
        public int LanguajeID { get; set; }
        public string Reasonforreferral { get; set; }
        public int AuthID { get; set; }
        public int HoursxWeek { get; set; }
        public int MaxHours { get; set; }
        public int TerminationNumber { get; set; }
        public string AdditionalEval { get; set; }
        public bool InHome { get; set; }
        public bool EIWITH { get; set; }
        public bool OTPT { get; set; }
        public DateTime initEval { get; set; }
        public DateTime evaldueby { get; set; }
        public DateTime Report1 { get; set; }
        public DateTime Report2 { get; set; }
        public DateTime Report3 { get; set; }
        public DateTime Report4 { get; set; }
        public DateTime Report5 { get; set; }
        public DateTime ReportClose { get; set; }
        public DateTime Date { get; set; }
        public int ReferredBy { get; set; }
        public string evaluation { get; set; }
        public string TypeReporte { get; set; }
        public int RegionalID { get; set; }
        public string ServiceCoordinator { get; set; }
        public int Archive { get; set; }
        public string Type { get; set; }
        public int? Action { get; set; }
        public string UserC { get; set; }
        public DateTime DateC { get; set; }
        public string UserU { get; set; }
        public DateTime DateU { get; set; }
        [JsonProperty("p_image")]
        public string c_image { get; set; }
        [JsonProperty("cb")]
        public bool? CB { get; set; }
        [JsonProperty("pep")]
        public bool PEP { get; set; }
        public DateTime TerminationDateEffective { get; set; }
        public string rc { get; set; }

        public bool _ErrorCode { get; set; }

        LogModel _log = new LogModel();

        // Replica Get_Consumer_allData: Archive = 1, mas el nombre del centro
        // regional (antes via JOIN, aqui se resuelve con una segunda llamada).
        public List<ConsumerModel> Get_Consumer()
        {
            var productos = new List<ConsumerModel>();
            try
            {
                productos = ApiClient.Get<List<ConsumerModel>>("consumers?archive=1");
                var centers = ApiClient.Get<List<RegionalCenterModel>>("regional-centers");
                var centerNames = centers.ToDictionary(c => c.IDRegionalCenter, c => c.RegionalCenter);
                foreach (var p in productos)
                {
                    centerNames.TryGetValue(p.RegionalID, out var name);
                    p.rc = name;
                }
            }
            catch (Exception)
            {
                throw;
            }
            return productos;
        }

        // Get_Consumer_byID (la SP original ya no existe en la BD — este metodo
        // no se llama desde ningun lado activo hoy) — se implementa via ?uci=.
        public List<ConsumerModel> Get_Consumer_ByID(string _UCI)
        {
            var productos = new List<ConsumerModel>();
            try
            {
                productos = ApiClient.Get<List<ConsumerModel>>("consumers?uci=" + Uri.EscapeDataString(_UCI));
            }
            catch (Exception)
            {
                throw;
            }
            return productos;
        }

        public ConsumerModel Get_ConsumerByID(int idconsumer)
        {
            var productos = new ConsumerModel();
            try
            {
                productos = ApiClient.Get<ConsumerModel>("consumers/" + idconsumer);
            }
            catch (Exception)
            {
                throw;
            }
            return productos;
        }

        // Get_Consumer_byName (la SP original ya no existe — sin uso activo hoy).
        public List<ConsumerModel> Get_Consumer_ByName(string _name)
        {
            var productos = new List<ConsumerModel>();
            try
            {
                productos = ApiClient.Get<List<ConsumerModel>>("consumers?search=" + Uri.EscapeDataString(_name));
            }
            catch (Exception)
            {
                throw;
            }
            return productos;
        }

        public List<ConsumerModel> Get_Consumer_AllDataByName(string _name)
        {
            var productos = new List<ConsumerModel>();
            try
            {
                productos = ApiClient.Get<List<ConsumerModel>>("consumers?search=" + Uri.EscapeDataString(_name));
            }
            catch (Exception)
            {
                throw;
            }
            return productos;
        }

        public string Create_Consumer(string _UCI, int _specialist1, int _specialist2, int _specialist3, string _PresenterName, string _Name,
            string _LastName, DateTime _DateOfBirth, DateTime _adjage, string _Gender, string _Address, string _CityID, string _ZipCode, string _Phone,
            string _EmergencyPhone, string _ParentName, string _ParentLastName, int _LanguajeID, string _Reasonforreferral, int _AuthID,
            int _HoursxWeek, int _MaxHours, int _TerminationNumber, string _AdditionalEval, bool _InHome, bool _EIWITH, bool _CB, bool _PEP,
            DateTime _initEval, DateTime _evaldueby, DateTime _Report1, DateTime _Report2, DateTime _Report3, DateTime _Report4, DateTime _Report5, DateTime _ReportClose,
            DateTime _Date, int _ReferredBy, string _evaluation, string _TypeReporte, int _RegionalID, string _ServiceCoordinator,
             int _Archive, string _Type, int _Action, string _UserC, DateTime _DateC, string _UserU, DateTime _DateU, bool _OTPT, DateTime _terminationdate, string _State)
        {
            string mensaje = "";
            ApiClient.Post<ConsumerModel>("consumers", new
            {
                UCI = _UCI,
                specialist1 = _specialist1,
                specialist2 = _specialist2,
                specialist3 = _specialist3,
                PresenterName = _PresenterName,
                Name = _Name,
                LastName = _LastName,
                DateOfBirth = _DateOfBirth.ToString("yyyy-MM-dd"),
                adjage = _adjage.ToString("yyyy-MM-dd"),
                Gender = _Gender,
                Address = _Address,
                CityID = _CityID,
                State = _State,
                ZipCode = _ZipCode,
                Phone = _Phone,
                EmergencyPhone = _EmergencyPhone,
                ParentName = _ParentName,
                ParentLastName = _ParentLastName,
                LanguajeID = _LanguajeID,
                Reasonforreferral = _Reasonforreferral,
                AuthID = _AuthID,
                HoursxWeek = _HoursxWeek,
                MaxHours = _MaxHours,
                TerminationNumber = _TerminationNumber,
                AdditionalEval = _AdditionalEval,
                InHome = _InHome,
                EIWITH = _EIWITH,
                OTPT = _OTPT,
                cb = _CB,
                pep = _PEP,
                initEval = _initEval.ToString("yyyy-MM-dd"),
                evaldueby = _evaldueby.ToString("yyyy-MM-dd"),
                Report1 = _Report1,
                Report2 = _Report2,
                Report3 = _Report3,
                Report4 = _Report4,
                Report5 = _Report5,
                ReportClose = _ReportClose,
                Date = DateTime.Now,
                ReferredBy = _ReferredBy,
                evaluation = _evaluation,
                TypeReporte = _TypeReporte,
                RegionalID = _RegionalID,
                ServiceCoordinator = _ServiceCoordinator,
                Archive = _Archive,
                Type = _Type,
                Action = _Action,
                TerminationDateEffective = _terminationdate.ToString("yyyy-MM-dd"),
            });
            return mensaje;
        }

        public List<BirthdaysModel> Get_births(string month)
        {
            var productos = new List<BirthdaysModel>();
            try
            {
                var consumers = ApiClient.Get<List<ConsumerModel>>("consumers?birthday_month=" + month);
                foreach (var c in consumers)
                {
                    productos.Add(new BirthdaysModel
                    {
                        UCI = c.UCI,
                        Month = c.DateOfBirth.ToString("MMMM"),
                        Name = c.Name,
                        LastName = c.LastName,
                        DateOfBirth = c.DateOfBirth.ToString("yyyy-MM-dd"),
                        Type = c.Type,
                    });
                }
            }
            catch (Exception)
            {
                throw;
            }
            return productos;
        }

        public bool DeleteConsumer(string _IDConsumer, string _UserName)
        {
            bool response = false;
            try
            {
                ApiClient.Delete("consumers/" + _IDConsumer);
                response = true;
                _log._logAction("Delete NotesxConsumer", "Delete NotesxConsumer, id:" + _IDConsumer, "DeleteNotesxConsumer", "NotesxConsumerModel", _UserName);
            }
            catch (Exception _error)
            {
                response = false;
                _log._logError(_error.Message, _error.StackTrace, "DeleteNotesxConsumer", "NotesxConsumerModel", _UserName);
            }
            return response;
        }

        public List<ConsumerModel> Get_ConsumerNameByUCI(string uci)
        {
            var productos = new List<ConsumerModel>();
            try
            {
                productos = ApiClient.Get<List<ConsumerModel>>("consumers?uci=" + Uri.EscapeDataString(uci));
            }
            catch (Exception)
            {
                throw;
            }
            return productos;
        }

        public bool AgregarImagenXConsumer(string _UCI, string C_Image, string _UserName)
        {
            bool response = false;
            try
            {
                var match = ApiClient.Get<List<ConsumerModel>>("consumers?uci=" + Uri.EscapeDataString(_UCI)).FirstOrDefault();
                if (match != null)
                {
                    ApiClient.Put<ConsumerModel>("consumers/" + match.IDConsumer, new { UCI = _UCI, p_image = C_Image });
                    response = true;
                }
                _log._logAction("Update IMagen", "Update IMagen, id:" + _UCI, "AgregarImagenXConsumer", "AgregarImagenXConsumer", _UserName);
            }
            catch (Exception _error)
            {
                response = false;
                _log._logError(_error.Message, _error.StackTrace, "AgregarImagenXConsumer", "AgregarImagenXConsumer", _UserName);
            }
            return response;
        }

        public bool ChangeR4A(string _UCI, int _Status, string _UserName)
        {
            bool response = false;
            try
            {
                var match = ApiClient.Get<List<ConsumerModel>>("consumers?uci=" + Uri.EscapeDataString(_UCI)).FirstOrDefault();
                if (match != null)
                {
                    ApiClient.Patch<ConsumerModel>("consumers/" + match.IDConsumer + "/status", new { Archive = _Status });
                    response = true;
                }
                _log._logAction("Update IMagen", "Update IMagen, id:" + _UCI, "AgregarImagenXConsumer", "AgregarImagenXConsumer", _UserName);
            }
            catch (Exception _error)
            {
                response = false;
                _log._logError(_error.Message, _error.StackTrace, "AgregarImagenXConsumer", "AgregarImagenXConsumer", _UserName);
            }
            return response;
        }

        public List<ConsumerModel> Get_Consumer4Status(int status)
        {
            var productos = new List<ConsumerModel>();
            try
            {
                productos = ApiClient.Get<List<ConsumerModel>>("consumers?archive=" + status);
            }
            catch (Exception)
            {
                throw;
            }
            return productos;
        }

        public string Update_Consumer(string _UCI, int? _specialist1, int? _specialist2, int? _specialist3, string _PresenterName, string _Name,
           string _LastName, DateTime _DateOfBirth, DateTime _adjage, string _Gender, string _Address, string _CityID, string _ZipCode, string _Phone,
           string _EmergencyPhone, string _ParentName, string _ParentLastName, int _LanguajeID, string _Reasonforreferral, int _AuthID,
           int _HoursxWeek, int _MaxHours, int _TerminationNumber, string _AdditionalEval, bool _InHome, bool _EIWITH, bool _CB, bool _PEP,
           DateTime _initEval, DateTime _evaldueby, DateTime _Report1, DateTime _Report2, DateTime _Report3, DateTime _Report4, DateTime _Report5, DateTime _ReportClose,
           DateTime _Date, int _ReferredBy, string _evaluation, string _TypeReporte, int _RegionalID, string _ServiceCoordinator,
            int _Archive, string _Type, int _Action, string _UserC, DateTime _DateC, string _UserU, DateTime _DateU, bool _OTPT, DateTime _terminationdate, string _State)
        {
            string mensaje = "";
            if (string.IsNullOrEmpty(_Gender))
            {
                _Gender = "";
            }

            // SP_Update_Consumer actualiza por UCI, no por IDConsumer.
            var match = ApiClient.Get<List<ConsumerModel>>("consumers?uci=" + Uri.EscapeDataString(_UCI)).FirstOrDefault();
            if (match == null)
            {
                throw new InvalidOperationException("No existe un consumer con UCI " + _UCI);
            }

            ApiClient.Put<ConsumerModel>("consumers/" + match.IDConsumer, new
            {
                UCI = _UCI,
                specialist1 = _specialist1,
                specialist2 = _specialist2,
                specialist3 = _specialist3,
                PresenterName = _PresenterName,
                Name = _Name,
                LastName = _LastName,
                DateOfBirth = _DateOfBirth.ToString("yyyy-MM-dd"),
                adjage = _adjage.ToString("yyyy-MM-dd"),
                Gender = _Gender,
                Address = _Address,
                CityID = _CityID,
                State = _State,
                ZipCode = _ZipCode,
                Phone = _Phone,
                EmergencyPhone = _EmergencyPhone,
                ParentName = _ParentName,
                ParentLastName = _ParentLastName,
                LanguajeID = _LanguajeID,
                Reasonforreferral = _Reasonforreferral,
                AuthID = _AuthID,
                HoursxWeek = _HoursxWeek,
                MaxHours = _MaxHours,
                TerminationNumber = _TerminationNumber,
                AdditionalEval = _AdditionalEval,
                InHome = _InHome,
                EIWITH = _EIWITH,
                OTPT = _OTPT,
                cb = _CB,
                pep = _PEP,
                initEval = _initEval.ToString("yyyy-MM-dd"),
                evaldueby = _evaldueby.ToString("yyyy-MM-dd"),
                Report1 = _Report1,
                Report2 = _Report2,
                Report3 = _Report3,
                Report4 = _Report4,
                Report5 = _Report5,
                ReportClose = _ReportClose,
                Date = _Date,
                ReferredBy = _ReferredBy,
                evaluation = _evaluation,
                TypeReporte = _TypeReporte,
                RegionalID = _RegionalID,
                ServiceCoordinator = _ServiceCoordinator,
                Archive = _Archive,
                Type = _Type,
                Action = _Action,
                TerminationDateEffective = _terminationdate.ToString("yyyy-MM-dd"),
            }, includeNulls: true);
            return mensaje;
        }

        public bool ArchiveAll()
        {
            bool response = false;
            try
            {
                ApiClient.Post<object>("consumers/archive-all", null);
                response = true;
            }
            catch (Exception)
            {
                response = false;
            }
            return response;
        }

        // Antes ordenaba por UCI numerico via SQL; la SP real (GetNextConsumerByUCI)
        // en realidad navega por Name/LastName entre los consumers Archive=1 —
        // se replica esa misma logica aqui.
        public ConsumerModel GetNextConsumerByUCI(int _uci)
        {
            return GetNeighborByUCI(_uci.ToString(), next: true);
        }

        public ConsumerModel GetPrevConsumerByUCI(int _uci)
        {
            return GetNeighborByUCI(_uci.ToString(), next: false);
        }

        private ConsumerModel GetNeighborByUCI(string uci, bool next)
        {
            var productos = new ConsumerModel();
            try
            {
                var all = ApiClient.Get<List<ConsumerModel>>("consumers?archive=1");
                var ordered = next
                    ? all.OrderBy(c => c.Name).ThenBy(c => c.LastName).ToList()
                    : all.OrderByDescending(c => c.Name).ThenByDescending(c => c.LastName).ToList();

                var currentIndex = ordered.FindIndex(c => c.UCI == uci);
                if (currentIndex >= 0 && currentIndex + 1 < ordered.Count)
                {
                    var neighbor = ordered[currentIndex + 1];
                    productos.IDConsumer = neighbor.IDConsumer;
                    productos.Name = neighbor.Name;
                }
                else
                {
                    productos.IDConsumer = 0;
                }
            }
            catch (Exception)
            {
                throw;
            }
            return productos;
        }
    }
}
