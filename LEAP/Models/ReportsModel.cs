using LEAP.Data;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LEAP.Models
{
    public class ReportsModel
    {
        public int IDConsumer { get; set; }
        public string UCI { get; set; }
        public int specialist1 { get; set; }

        public int specialist2 { get; set; }

        public int specialist3 { get; set; }
        public string PresenterName { get; set; }

        public string ConsumerName { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public int Age { get; set; }
        public DateTime adjage { get; set; }
        public string Gender { get; set; }
        public string Address { get; set; }
        //public int CityID { get; set; }
        public string CityID { get; set; }
        public string State { get; set; }
        public string ZipCode { get; set; }
        public string Phone { get; set; }
        public string EmergencyPhone { get; set; }
        public string ParentName { get; set; }
        public string ParentLastName { get; set; }
        public string ParentFullName { get; set; }
        public int LanguajeID { get; set; }
        public string LanguajeName { get; set; }
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
        public string ServiceCoordinatorID { get; set; }
        public bool Archive { get; set; }
        public string Type { get; set; }
        public int Action { get; set; }
        public bool Active { get; set; }
        public string UserC { get; set; }
        public DateTime DateC { get; set; }
        public string UserU { get; set; }
        public DateTime DateU { get; set; }
        //Authotization
        public DateTime _From { get; set; }
        public DateTime _To { get; set; }
        public string auth { get; set; }
        //NotesxConsumer
        public string NotesConsumer { get; set; }
        //REGIONAL CENTER
        public string RegionalCenter { get; set; }
        public string rc_Phone { get; set; }
        public string rc_Address { get; set; }
        public string Ext { get; set; }
        //specialist
        public string spe_Name { get; set; }
        public string spe_LastName { get; set; }
        public string spe_FullName { get; set; }
        //public int Duration { get; set; }
        public string Duration { get; set; }
        public string PresentInSession { get; set; }
        public string TotalHours { get; set; }
        public string TimesxWeek { get; set; }
        public string c_image { get; set; }
        public bool CB { get; set; }
        public bool PEP { get; set; }
        public string email { get; set; }
        public string tel { get; set; }
        public string CDS_name { get; set; }
        public string Specialty_name { get; set; }
        public string Therapist_name { get; set; }
        public DateTime TerminationDateEffective { get; set; }

        public string ciudad { get; set; }

        // CenterReport: horas brindadas por dia del mes (clave = dia del mes, 1-31),
        // solo trae los dias que tuvieron alguna visita registrada - viene de
        // leap_api ReportController::centersByMonth.
        public Dictionary<int, int> DayHours { get; set; }

        LogModel _log = new LogModel();

        // Trae el shape completo desde el endpoint colapsado de leap_api (ver
        // ReportController::consumerReport en leap_api) y arma las 3 variantes
        // que existian por separado (esta, Get_Reports_RPTConsumerList y
        // GetList_Reports_RPTNotesConsumer) a partir del mismo resultado.
        private ReportsModel FetchConsumerReport(string _UCI)
        {
            return ApiClient.Get<ReportsModel>("reports/consumer/" + _UCI);
        }

        public ReportsModel Get_Reports_RPTConsumer(string _UCI)
        {
            var productos = new ReportsModel();
            try
            {
                productos = FetchConsumerReport(_UCI);
                productos.ConsumerName = productos.Name + " " + productos.LastName;
                productos.ParentFullName = productos.ParentName + " " + productos.ParentLastName;
                productos.spe_FullName = productos.spe_Name + " " + productos.spe_LastName;
                // _RptConsumerPartial.cshtml compara _baseServiCoor contra el ID que
                // devuelve GetRegionalByID (datos[9], IDServiceC) para preseleccionar
                // el select del coordinador - necesita el ID crudo, no el nombre
                // (NameC) que llevan Get_Reports_RPTConsumerList/GetList_Reports_RPTNotesConsumer
                // para los PDFs.
                productos.ServiceCoordinator = productos.ServiceCoordinatorID;
            }
            catch (Exception _error)
            {
                _error.Message.ToString();
            }
            return productos;
        }
        public List<ReportsModel> Get_Reports_RPTConsumerList(string _UCI)
        {
            var productos = new List<ReportsModel>();
            try
            {
                var row = FetchConsumerReport(_UCI);
                row.ConsumerName = row.Name + " " + row.LastName;
                row.ParentFullName = row.ParentName; // replica el original: sin concatenar LastName aqui
                row.spe_FullName = row.spe_Name + " " + row.spe_LastName;
                row.email = row.ParentLastName; // el original reusa ParentLastName como email
                productos.Add(row);
            }
            catch (Exception _error)
            {
                throw;
            }
            return productos;
        }
        public List<ReportsModel> GetList_Reports_RPTNotesConsumer(string _uci)
        {
            var productos = new List<ReportsModel>();
            try
            {
                var row = FetchConsumerReport(_uci);
                row.ConsumerName = row.Name + " " + row.LastName;
                row.ParentFullName = row.ParentName + " " + row.ParentLastName;
                row.spe_FullName = row.spe_Name + " " + row.spe_LastName;
                productos.Add(row);
            }
            catch (Exception _error)
            {
                throw;
            }
            return productos;
        }

        public List<ReportsModel> Get_NotesXConsumer(string _uci)
        {
            var productos = new List<ReportsModel>();
            try
            {
                productos = ApiClient.Get<List<ReportsModel>>("reports/notes-x-consumer/" + _uci);
                foreach (var row in productos)
                {
                    row.ConsumerName = row.Name + " " + row.LastName;
                    row.spe_FullName = row.spe_Name + " " + row.spe_LastName;
                }
            }
            catch (Exception)
            {
                throw;
            }
            return productos;
        }
        //=====================================================================//
        //============== NUEVO METODO DE REPORTE DE CUMPLEAÑEROS DEL MES ======//
        //=====================================================================//
        public List<ReportsModel> _RptBirthday_XMes(int month, int year)
        {
            var productos = new List<ReportsModel>();
            try
            {
                productos = ApiClient.Get<List<ReportsModel>>("reports/birthdays?month=" + month + "&year=" + year);
            }
            catch (Exception e)
            {
                throw;
            }
            return productos;
        }
        //ESTE REPORTE DE DESACTIVARA Y PARA EVITAR CONFUCIONES NO SE ELIMINARA EL METODO
        public List<ReportsModel> _RptBirthday(DateTime _From, DateTime _To)
        {
            var productos = new List<ReportsModel>();
            try
            {
                productos = ApiClient.Get<List<ReportsModel>>("reports/birthdays-range?from=" + _From.ToString("yyyy-MM-dd") + "&to=" + _To.ToString("yyyy-MM-dd"));
            }
            catch (Exception _error)
            {
                throw;
            }
            return productos;
        }

        public List<ReportsModel> _RptByCDS(string cds, int month, int year)
        {
            var productos = new List<ReportsModel>();
            try
            {
                productos = ApiClient.Get<List<ReportsModel>>("reports/cds?cds=" + cds + "&month=" + month + "&year=" + year);
            }
            catch (Exception _error)
            {
                throw;
            }
            return productos;
        }

        public List<ReportsModel> _RptByCDSConsumer(string cds)
        {
            var productos = new List<ReportsModel>();
            try
            {
                productos = ApiClient.Get<List<ReportsModel>>("reports/cds-consumers?cds=" + cds);
            }
            catch (Exception _error)
            {
                throw;
            }
            return productos;
        }


        // Reporte de customers ACTIVOS HOY (Archive=1) de un centro, con las horas
        // brindadas cada dia del mes/anio pedido (leap_api ReportController::
        // centersByMonth). Un intento anterior de filtrar tambien por consumers.Date
        // dentro del mes se revirtio (dejaba el reporte vacio en meses sin altas
        // nuevas) - decision confirmada con el usuario: el mes/anio solo se usa para
        // ubicar las visitas en la grilla de dias, no para filtrar que consumers
        // aparecen. La firma quedo (int month, int year, string _center) de una
        // migracion anterior; el unico caller es CenterReport en ReportsController.cs.
        public List<ReportsModel> _Rpt_Data_CentersByMonth(int month, int year, string _center)
        {
            List<ReportsModel> list = new List<ReportsModel>();
            try
            {
                list = ApiClient.Get<List<ReportsModel>>("reports/centers-by-month?center=" + _center + "&month=" + month + "&year=" + year);
            }
            catch (Exception _error)
            {
                throw;
            }
            var sortedlist = list.OrderBy(x => x.ConsumerName).ToList();
            return sortedlist;
        }

        // Igual que _Rpt_Data_CentersByMonth pero agrupado por CDS (specialist1) en
        // vez de por Regional Center (leap_api ReportController::cdsVisitsByMonth) -
        // mismo criterio de "activos hoy" (Archive=1) sin filtrar por mes.
        public List<ReportsModel> _Rpt_Data_CDSVisitsByMonth(int month, int year, string _cds)
        {
            List<ReportsModel> list = new List<ReportsModel>();
            try
            {
                list = ApiClient.Get<List<ReportsModel>>("reports/cds-visits-by-month?cds=" + _cds + "&month=" + month + "&year=" + year);
            }
            catch (Exception _error)
            {
                throw;
            }
            var sortedlist = list.OrderBy(x => x.ConsumerName).ToList();
            return sortedlist;
        }
        public List<ReportsModel> _NewConsumerXDate(DateTime _From, DateTime _To)
        {
            var productos = new List<ReportsModel>();
            try
            {
                productos = ApiClient.Get<List<ReportsModel>>("reports/new-consumers?from=" + _From.ToString("yyyy-MM-dd") + "&to=" + _To.ToString("yyyy-MM-dd"));
            }
            catch (Exception _error)
            {
                throw;
            }
            return productos;
        }

    }
}