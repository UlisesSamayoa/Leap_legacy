using LEAP.Data;
using Org.BouncyCastle.Asn1.TeleTrust;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

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

        LogModel _log = new LogModel();
        public List<ConsumerModel> Get_Consumer()
        {
            var productos = new List<ConsumerModel>();
            try
            {
                using (var context = new ApplicationDbContext())
                {
                    using (SqlCommand cmd = new SqlCommand("Get_Consumer_allData", context.Connection))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        context.Connection.Open();
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                productos.Add(new ConsumerModel
                                {
                                    IDConsumer = Convert.ToInt32(reader["IDConsumer"]),
                                    UCI = reader["UCI"].ToString(),
                                    specialist1 = Convert.ToInt32(reader["specialist1"]),
                                    specialist2 = Convert.ToInt32(reader["specialist2"]),
                                    specialist3 = Convert.ToInt32(reader["specialist3"]),
                                    PresenterName = reader["PresenterName"].ToString(),
                                    Name = reader["Name"].ToString(),
                                    LastName = reader["LastName"].ToString(),
                                    DateOfBirth = Convert.ToDateTime(reader["DateOfBirth"]),
                                    adjage = Convert.ToDateTime(reader["adjage"]),
                                    Gender = reader["Gender"].ToString(),
                                    Address = reader["Address"].ToString(),
                                    CityID = reader["CityID"].ToString(),
                                    ZipCode = reader["ZipCode"].ToString(),
                                    Phone = reader["Phone"].ToString(),
                                    EmergencyPhone = reader["EmergencyPhone"].ToString(),
                                    ParentName = reader["ParentName"].ToString(),
                                    ParentLastName = reader["ParentLastName"].ToString(),
                                    LanguajeID = Convert.ToInt32(reader["LanguajeID"]),
                                    Reasonforreferral = reader["Reasonforreferral"].ToString(),
                                    AuthID = Convert.ToInt32(reader["AuthID"]),
                                    HoursxWeek = Convert.ToInt32(reader["HoursxWeek"]),
                                    MaxHours = Convert.ToInt32(reader["MaxHours"]),
                                    TerminationNumber = Convert.ToInt32(reader["TerminationNumber"]),
                                    AdditionalEval = reader["AdditionalEval"].ToString(),
                                    InHome = Convert.ToBoolean(reader["InHome"]),
                                    EIWITH = Convert.ToBoolean(reader["EIWITH"]),
                                    OTPT = Convert.ToBoolean(reader["OTPT"]),
                                    initEval = Convert.ToDateTime(reader["initEval"]),
                                    evaldueby = Convert.ToDateTime(reader["evaldueby"]),
                                    Report1 = Convert.ToDateTime(reader["Report1"]),
                                    Report2 = Convert.ToDateTime(reader["Report2"]),
                                    Report3 = Convert.ToDateTime(reader["Report3"]),
                                    Report4 = Convert.ToDateTime(reader["Report4"]),
                                    ReportClose = Convert.ToDateTime(reader["ReportClose"]),
                                    Date = Convert.ToDateTime(reader["Date"]),
                                    ReferredBy = Convert.ToInt32(reader["ReferredBy"]),
                                    evaluation = reader["evaluation"].ToString(),
                                    TypeReporte = reader["TypeReporte"].ToString(),
                                    RegionalID = Convert.ToInt32(reader["RegionalID"]),
                                    ServiceCoordinator = reader["ServiceCoordinator"].ToString(),
                                    Archive = Convert.ToInt32(reader["Archive"]),
                                    Type = reader["Type"].ToString(),
                                    Action = Convert.ToInt32(reader["Action"])

                                });
                            }
                        }
                    }
                }
            }
            catch (Exception _error)
            {
                throw;
            }
            return productos;
        }

        //public List<ReportsModel> Get_Reports_RPTConsumer(string _UCI)
        //{
        //    var productos = new List<ReportsModel>();
        //    try
        //    {
        //        using (var context = new ApplicationDbContext())
        //        {
        //            using (SqlCommand cmd = new SqlCommand("SP_Reports_RPTConsumer", context.Connection))
        //            {
        //                cmd.CommandType = CommandType.StoredProcedure;
        //                cmd.Parameters.AddWithValue("@UCI", _UCI);
        //                context.Connection.Open();
        //                using (SqlDataReader reader = cmd.ExecuteReader())
        //                {
        //                    while (reader.Read())
        //                    {
        //                        productos.Add(new ReportsModel
        //                        {
        //                            Date = Convert.ToDateTime(reader["Date"]),
        //                            Action = Convert.ToInt32(reader["Action"]),
        //                            Name = reader["Name"].ToString(),
        //                            LastName = reader["LastName"].ToString(),
        //                            UCI = reader["UCI"].ToString(),
        //                            DateOfBirth = Convert.ToDateTime(reader["DateOfBirth"]),
        //                            adjage = Convert.ToDateTime(reader["adjage"]),
        //                            Gender = reader["Gender"].ToString(),
        //                            Address = reader["Address"].ToString(),
        //                            CityID = Convert.ToInt32(reader["CityID"]),
        //                            ZipCode = reader["ZipCode"].ToString(),
        //                            Phone = reader["Phone"].ToString(),
        //                            EmergencyPhone = reader["EmergencyPhone"].ToString(),
        //                            ParentName = reader["ParentName"].ToString(),
        //                            ParentLastName = reader["ParentLastName"].ToString(),
        //                            LanguajeID = Convert.ToInt32(reader["LanguajeID"]),
        //                            //other
        //                            Reasonforreferral = reader["Reasonforreferral"].ToString(),
        //                            _From = Convert.ToDateTime(reader["_From"]),
        //                            _To = Convert.ToDateTime(reader["_To"]),
        //                            auth = auth = reader["auth"].ToString(),
        //                            HoursxWeek = Convert.ToInt32(reader["HoursxWeek"]),
        //                            MaxHours = Convert.ToInt32(reader["MaxHours"]),
        //                            //TerminationNumber = Convert.ToInt32(reader["TerminationNumber"]),
        //                            AdditionalEval = reader["AdditionalEval"].ToString(),
        //                            InHome = Convert.ToBoolean(reader["InHome"]),
        //                            //EIWITH = Convert.ToBoolean(reader["EIWITH"]),
        //                            OTPT = Convert.ToBoolean(reader["OTPT"]),
        //                            //CDS
        //                            //Specialty
        //                            //Therapist
        //                            //Program Presenter PresenterName = reader["PresenterName"].ToString(),
        //                            NotesConsumer = reader["Notes"].ToString(),
        //                            RegionalCenter = reader["Notes"].ToString(),
        //                            rc_Phone = reader["Notes"].ToString(),
        //                            rc_Address = reader["Notes"].ToString(),
        //                            Ext = reader["Ext"].ToString(),
        //                            initEval = Convert.ToDateTime(reader["initEval"]),
        //                            evaldueby = Convert.ToDateTime(reader["evaldueby"]),
        //                            Report1 = Convert.ToDateTime(reader["Report1"]),
        //                            Report2 = Convert.ToDateTime(reader["Report2"]),
        //                            Report3 = Convert.ToDateTime(reader["Report3"]),
        //                            Report4 = Convert.ToDateTime(reader["Report4"]),
        //                            ReportClose = Convert.ToDateTime(reader["ReportClose"]),

        //                            //specialist1 = Convert.ToInt32(reader["specialist1"]),
        //                            //specialist2 = Convert.ToInt32(reader["specialist2"]),
        //                            //specialist3 = Convert.ToInt32(reader["specialist3"]),
        //                            //ReferredBy = Convert.ToInt32(reader["ReferredBy"]),
        //                            //evaluation = reader["evaluation"].ToString(),
        //                            //TypeReporte = reader["TypeReporte"].ToString(),
        //                            //RegionalID = Convert.ToInt32(reader["RegionalID"]),
        //                            //ServiceCoordinator = reader["ServiceCoordinator"].ToString(),
        //                            //Archive = Convert.ToBoolean(reader["Archive"]),
        //                            //Type = reader["Type"].ToString(),

        //                        });
        //                    }
        //                }
        //            }
        //        }
        //    }
        //    catch (Exception _error)
        //    {
        //        throw;
        //    }
        //    return productos;
        //}
        public ReportsModel Get_Reports_RPTConsumer(string _UCI)
        {
            var productos = new ReportsModel();
            try
            {
                using (var context = new ApplicationDbContext())
                {
                    using (SqlCommand cmd = new SqlCommand("SP_Reports_Get_RPTConsumer", context.Connection))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@UCI", _UCI);
                        context.Connection.Open();
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {


                                productos.Date = Convert.ToDateTime(reader["Date"]);
                                productos.Action = Convert.ToInt32(reader["Action"]);
                                productos.ConsumerName = reader["Name"].ToString() + " " + reader["LastName"].ToString();
                                productos.Name = reader["Name"].ToString();
                                productos.LastName = reader["LastName"].ToString();
                                productos.UCI = reader["UCI"].ToString();
                                productos.DateOfBirth = Convert.ToDateTime(reader["DateOfBirth"]);
                                productos.adjage = Convert.ToDateTime(reader["adjage"]);
                                productos.Gender = reader["Gender"].ToString();
                                productos.Address = reader["Address"].ToString();
                                productos.CityID = reader["CityID"].ToString();
                                productos.State = reader["State"].ToString();
                                productos.ZipCode = reader["ZipCode"].ToString();
                                productos.Phone = reader["Phone"].ToString();
                                productos.EmergencyPhone = reader["EmergencyPhone"].ToString();
                                productos.ParentName = reader["ParentName"].ToString();
                                productos.ParentLastName = reader["ParentLastName"].ToString();
                                productos.ParentFullName = reader["ParentName"].ToString() + " " + reader["ParentLastName"].ToString();
                                productos.LanguajeID = Convert.ToInt32(reader["LanguajeID"]);
                                productos.LanguajeName = reader["Languaje"].ToString();
                                productos.Reasonforreferral = reader["Reasonforreferral"].ToString();
                                productos._From = Convert.ToDateTime(reader["_From"]);
                                productos._To = Convert.ToDateTime(reader["_To"]);
                                productos.auth = reader["auth"].ToString();
                                productos.HoursxWeek = Convert.ToInt32(reader["HoursxWeek"]);
                                productos.MaxHours = Convert.ToInt32(reader["MaxHours"]);
                                //productos.TerminationNumber = Convert.ToInt32(reader["TerminationNumber"]);
                                productos.AdditionalEval = reader["AdditionalEval"].ToString();
                                productos.InHome = Convert.ToBoolean(reader["InHome"]);
                                //productos.EIWITH = Convert.ToBoolean(reader["EIWITH"]);
                                productos.OTPT = Convert.ToBoolean(reader["OTPT"]);
                                productos.NotesConsumer = reader["Notes"].ToString();
                                productos.RegionalCenter = reader["RegionalCenter"].ToString();
                                productos.rc_Phone = reader["rc_Phone"].ToString();
                                productos.rc_Address = reader["rc_Address"].ToString();
                                productos.Ext = reader["Ext"].ToString();
                                productos.initEval = Convert.ToDateTime(reader["initEval"]);
                                productos.evaldueby = Convert.ToDateTime(reader["evaldueby"]);
                                productos.Report1 = Convert.ToDateTime(reader["Report1"]);
                                productos.Report2 = Convert.ToDateTime(reader["Report2"]);
                                productos.Report3 = Convert.ToDateTime(reader["Report3"]);
                                productos.Report4 = Convert.ToDateTime(reader["Report4"]);
                                productos.Report5 = Convert.ToDateTime(reader["Report5"]);
                                productos.ReportClose = Convert.ToDateTime(reader["ReportClose"]);
                                productos.spe_Name = reader["spe_Name"].ToString();
                                productos.spe_LastName = reader["spe_LastName"].ToString();
                                productos.spe_FullName = reader["spe_Name"].ToString() + " " + reader["spe_LastName"].ToString();
                                productos.PresenterName = reader["Program_PresenterName"].ToString();
                                productos.CDS_name = reader["CDS_name"].ToString();
                                productos.Specialty_name = reader["Specialty_name"].ToString();
                                productos.Therapist_name = reader["Therapist_name"].ToString();
                                productos.TerminationDateEffective= Convert.ToDateTime(reader["TerminationDateEffective"]);
                                productos.c_image = reader["p_image"].ToString();
                                //productos.specialist1 = Convert.ToInt32(reader["specialist1"]);
                                //productos.specialist2 = Convert.ToInt32(reader["specialist2"]);
                                //productos.specialist3 = Convert.ToInt32(reader["specialist3"]);
                                //productos.PresenterName = reader["PresenterName"].ToString();
                                //productos.ReferredBy = Convert.ToInt32(reader["ReferredBy"]);
                                //productos.evaluation = reader["evaluation"].ToString();
                                //productos.TypeReporte = reader["TypeReporte"].ToString();
                                //productos.RegionalID = Convert.ToInt32(reader["RegionalID"]);
                                //productos.ServiceCoordinator = reader["ServiceCoordinator"].ToString();
                                //productos.Archive = Convert.ToBoolean(reader["Archive"]);
                                //productos.Type = reader["Type"].ToString();


                            }
                        }
                    }
                }
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
                using (var context = new ApplicationDbContext())
                {
                    using (SqlCommand cmd = new SqlCommand("SP_Reports_Get_RPTConsumer", context.Connection))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@UCI", _UCI);
                        context.Connection.Open();
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                productos.Add(new ReportsModel
                                {
                                    //IDConsumer = Convert.ToInt32(reader["IDConsumer"]),
                                    Date = Convert.ToDateTime(reader["Date"]),
                                    Action = Convert.ToInt32(reader["Action"]),
                                    ConsumerName = reader["Name"].ToString() + " " + reader["LastName"].ToString(),
                                    Name = reader["Name"].ToString(),
                                    LastName = reader["LastName"].ToString(),
                                    UCI = reader["UCI"].ToString(),
                                    DateOfBirth = Convert.ToDateTime(reader["DateOfBirth"]),
                                    adjage = Convert.ToDateTime(reader["adjage"]),
                                    Gender = reader["Gender"].ToString(),
                                    Address = reader["Address"].ToString(),
                                    CityID = reader["CityID"].ToString(),
                                    State = reader["State"].ToString(),
                                    ZipCode = reader["ZipCode"].ToString(),
                                    Phone = reader["Phone"].ToString(),
                                    EmergencyPhone = reader["EmergencyPhone"].ToString(),
                                    ParentName = reader["ParentName"].ToString(),
                                    ParentLastName = reader["ParentLastName"].ToString(),
                                    ParentFullName = reader["ParentName"].ToString(),
                                    LanguajeID = Convert.ToInt32(reader["LanguajeID"]),
                                    LanguajeName = reader["Languaje"].ToString(),
                                    Reasonforreferral = reader["Reasonforreferral"].ToString(),
                                    _From = Convert.ToDateTime(reader["_From"]),
                                    _To = Convert.ToDateTime(reader["_To"]),
                                    auth = reader["auth"].ToString(),
                                    HoursxWeek = Convert.ToInt32(reader["HoursxWeek"]),
                                    MaxHours = Convert.ToInt32(reader["MaxHours"]),
                                    //productos.TerminationNumber = Convert.ToInt32(reader["TerminationNumber"]);
                                    AdditionalEval = reader["AdditionalEval"].ToString(),
                                    InHome = Convert.ToBoolean(reader["InHome"]),
                                    //productos.EIWITH = Convert.ToBoolean(reader["EIWITH"]);

                                    OTPT = Convert.ToBoolean(reader["OTPT"]),
                                    NotesConsumer = reader["Notes"].ToString(),
                                    RegionalCenter = reader["RegionalCenter"].ToString(),
                                    rc_Phone = reader["rc_Phone"].ToString(),
                                    rc_Address = reader["rc_Address"].ToString(),
                                    Ext = reader["Ext"].ToString(),
                                    initEval = Convert.ToDateTime(reader["initEval"]),
                                    evaldueby = Convert.ToDateTime(reader["evaldueby"]),
                                    Report1 = Convert.ToDateTime(reader["Report1"]),
                                    Report2 = Convert.ToDateTime(reader["Report2"]),
                                    Report3 = Convert.ToDateTime(reader["Report3"]),
                                    Report4 = Convert.ToDateTime(reader["Report4"]),
                                    Report5 = Convert.ToDateTime(reader["Report5"]),
                                    ReportClose = Convert.ToDateTime(reader["ReportClose"]),
                                    spe_Name = reader["spe_Name"].ToString(),
                                    spe_LastName = reader["spe_LastName"].ToString(),
                                    spe_FullName = reader["spe_Name"].ToString() + " " + reader["spe_LastName"].ToString(),
                                    Type = reader["Type"].ToString(),
                                    tel = reader["Tel"].ToString(),
                                    ServiceCoordinator = reader["ServiceCoordinator"].ToString(),
                                    UserC = reader["userC"].ToString(),
                                    email= reader["ParentLastName"].ToString(),
                                    c_image = reader["p_image"].ToString(),
                                    CB = Convert.ToBoolean(reader["CB"]),
                                    PEP=Convert.ToBoolean(reader["PEP"]),
                                    PresenterName = reader["Program_PresenterName"].ToString(),
                                    CDS_name = reader["CDS_name"].ToString(),
                                    Specialty_name = reader["Specialty_name"].ToString(),
                                    Therapist_name = reader["Therapist_name"].ToString(),
                                    TerminationDateEffective = Convert.ToDateTime(reader["TerminationDateEffective"])
                                });
                            }
                        }
                    }
                }
            }
            catch (Exception _error)
            {
                throw;
            }
            return productos;
        }
        //public List<ReportsModel> Get_Reports_RPTConsumerList(string _UCI)
        //{
        //    var productos = new List<ReportsModel>();
        //    try
        //    {
        //        using (var context = new ApplicationDbContext())
        //        {
        //            using (SqlCommand cmd = new SqlCommand("SP_Reports_Get_RPTConsumer", context.Connection))
        //            {
        //                cmd.CommandType = CommandType.StoredProcedure;
        //                cmd.Parameters.AddWithValue("@UCI", _UCI);
        //                context.Connection.Open();
        //                using (SqlDataReader reader = cmd.ExecuteReader())
        //                {
        //                    while (reader.Read())
        //                    {
        //                        productos.Add(new ReportsModel
        //                        {
        //                            //IDConsumer = Convert.ToInt32(reader["IDConsumer"]),
        //                            Date = Convert.ToDateTime(reader["Date"]),
        //                            Action = Convert.ToInt32(reader["Action"]),
        //                            ConsumerName = reader["Name"].ToString() + " " + reader["LastName"].ToString(),
        //                            Name = reader["Name"].ToString(),
        //                            LastName = reader["LastName"].ToString(),
        //                            UCI = reader["UCI"].ToString(),
        //                            DateOfBirth = Convert.ToDateTime(reader["DateOfBirth"]),
        //                            adjage = Convert.ToDateTime(reader["adjage"]),
        //                            Gender = reader["Gender"].ToString(),
        //                            Address = reader["Address"].ToString(),
        //                            CityID = reader["CityID"].ToString(),
        //                            State = reader["State"].ToString(),
        //                            ZipCode = reader["ZipCode"].ToString(),
        //                            Phone = reader["Phone"].ToString(),
        //                            EmergencyPhone = reader["EmergencyPhone"].ToString(),
        //                            ParentName = reader["ParentName"].ToString(),
        //                            ParentLastName = reader["ParentLastName"].ToString(),
        //                            ParentFullName = reader["ParentName"].ToString(),
        //                            LanguajeID = Convert.ToInt32(reader["LanguajeID"]),
        //                            LanguajeName = reader["Languaje"].ToString(),
        //                            Reasonforreferral = reader["Reasonforreferral"].ToString(),
        //                            _From = Convert.ToDateTime(reader["_From"]),
        //                            _To = Convert.ToDateTime(reader["_To"]),
        //                            auth = reader["auth"].ToString(),
        //                            HoursxWeek = Convert.ToInt32(reader["HoursxWeek"]),
        //                            MaxHours = Convert.ToInt32(reader["MaxHours"]),
        //                            //productos.TerminationNumber = Convert.ToInt32(reader["TerminationNumber"]);
        //                            AdditionalEval = reader["AdditionalEval"].ToString(),
        //                            InHome = Convert.ToBoolean(reader["InHome"]),
        //                            //productos.EIWITH = Convert.ToBoolean(reader["EIWITH"]);

        //                            OTPT = Convert.ToBoolean(reader["OTPT"]),
        //                            NotesConsumer = reader["Notes"].ToString(),
        //                            RegionalCenter = reader["RegionalCenter"].ToString(),
        //                            rc_Phone = reader["rc_Phone"].ToString(),
        //                            rc_Address = reader["rc_Address"].ToString(),
        //                            Ext = reader["Ext"].ToString(),
        //                            initEval = Convert.ToDateTime(reader["initEval"]),
        //                            evaldueby = Convert.ToDateTime(reader["evaldueby"]),
        //                            Report1 = Convert.ToDateTime(reader["Report1"]),
        //                            Report2 = Convert.ToDateTime(reader["Report2"]),
        //                            Report3 = Convert.ToDateTime(reader["Report3"]),
        //                            Report4 = Convert.ToDateTime(reader["Report4"]),
        //                            ReportClose = Convert.ToDateTime(reader["ReportClose"]),
        //                            spe_Name = reader["spe_Name"].ToString(),
        //                            spe_LastName = reader["spe_LastName"].ToString(),
        //                            spe_FullName = reader["spe_Name"].ToString() + " " + reader["spe_LastName"].ToString(),
        //                            Type = reader["Type"].ToString(),
        //                            tel = reader["Tel"].ToString(),
        //                            ServiceCoordinator = reader["ServiceCoordinator"].ToString(),
        //                            UserC = reader["userC"].ToString(),
        //                            email = reader["ParentLastName"].ToString(),
        //                            c_image = reader["p_image"].ToString(),
        //                            CB = Convert.ToBoolean(reader["CB"]),
        //                            PEP = Convert.ToBoolean(reader["PEP"])

        //                        });
        //                    }
        //                }
        //            }
        //        }
        //    }
        //    catch (Exception _error)
        //    {
        //        throw;
        //    }
        //    return productos;
        //}
        public List<ReportsModel> GetList_Reports_RPTConsumer(string _uci)
        {
            var productos = new List<ReportsModel>();
            try
            {
                using (var context = new ApplicationDbContext())
                {
                    using (SqlCommand cmd = new SqlCommand("sp_reports_rptconsumer", context.Connection))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@uci", _uci);
                        context.Connection.Open();
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                productos.Add(new ReportsModel
                                {
                                    Date = Convert.ToDateTime(reader["Date"]),
                                    Action = Convert.ToInt32(reader["Action"]),
                                    Name = reader["Name"].ToString(),
                                    LastName = reader["LastName"].ToString(),
                                    ConsumerName = reader["Name"].ToString() + " " + reader["LastName"].ToString(),
                                    UCI = reader["UCI"].ToString(),
                                    DateOfBirth = Convert.ToDateTime(reader["DateOfBirth"]),
                                    adjage = Convert.ToDateTime(reader["adjage"]),
                                    Gender = reader["Gender"].ToString(),
                                    Address = reader["Address"].ToString(),
                                    CityID = reader["CityID"].ToString(),
                                    State = reader["State"].ToString(),
                                    ZipCode = reader["ZipCode"].ToString(),
                                    Phone = reader["Phone"].ToString(),
                                    EmergencyPhone = reader["EmergencyPhone"].ToString(),
                                    ParentName = reader["ParentName"].ToString(),
                                    ParentLastName = reader["ParentLastName"].ToString(),
                                    ParentFullName = reader["ParentName"].ToString() + " " + reader["ParentLastName"].ToString(),
                                    LanguajeID = Convert.ToInt32(reader["LanguajeID"]),
                                    //other
                                    Reasonforreferral = reader["Reasonforreferral"].ToString(),
                                    _From = Convert.ToDateTime(reader["_From"]),
                                    _To = Convert.ToDateTime(reader["_To"]),
                                    auth = auth = reader["auth"].ToString(),
                                    HoursxWeek = Convert.ToInt32(reader["HoursxWeek"]),
                                    MaxHours = Convert.ToInt32(reader["MaxHours"]),
                                    //TerminationNumber = Convert.ToInt32(reader["TerminationNumber"]),
                                    AdditionalEval = reader["AdditionalEval"].ToString(),
                                    InHome = Convert.ToBoolean(reader["InHome"]),
                                    //EIWITH = Convert.ToBoolean(reader["EIWITH"]),
                                    OTPT = Convert.ToBoolean(reader["OTPT"]),
                                    //CDS
                                    //Specialty
                                    //Therapist
                                    //Program Presenter PresenterName = reader["PresenterName"].ToString(),
                                    NotesConsumer = reader["Notes"].ToString(),
                                    RegionalCenter = reader["RegionalCenter"].ToString(),
                                    rc_Phone = reader["rc_Phone"].ToString(),
                                    rc_Address = reader["rc_Address"].ToString(),
                                    Ext = reader["Ext"].ToString(),
                                    initEval = Convert.ToDateTime(reader["initEval"]),
                                    evaldueby = Convert.ToDateTime(reader["evaldueby"]),
                                    Report1 = Convert.ToDateTime(reader["Report1"]),
                                    Report2 = Convert.ToDateTime(reader["Report2"]),
                                    Report3 = Convert.ToDateTime(reader["Report3"]),
                                    Report4 = Convert.ToDateTime(reader["Report4"]),
                                    ReportClose = Convert.ToDateTime(reader["ReportClose"]),
                                    spe_Name = reader["spe_Name"].ToString(),
                                    spe_LastName = reader["spe_LastName"].ToString(),
                                    spe_FullName = reader["spe_Name"].ToString() + " " + reader["spe_LastName"].ToString(),

                                    //specialist1 = Convert.ToInt32(reader["specialist1"]),
                                    //specialist2 = Convert.ToInt32(reader["specialist2"]),
                                    //specialist3 = Convert.ToInt32(reader["specialist3"]),
                                    //ReferredBy = Convert.ToInt32(reader["ReferredBy"]),
                                    //evaluation = reader["evaluation"].ToString(),
                                    //TypeReporte = reader["TypeReporte"].ToString(),
                                    //RegionalID = Convert.ToInt32(reader["RegionalID"]),
                                    //ServiceCoordinator = reader["ServiceCoordinator"].ToString(),
                                    //Archive = Convert.ToBoolean(reader["Archive"]),
                                    //Type = reader["Type"].ToString(),

                                });
                            }
                        }
                    }
                }
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
                using (var context = new ApplicationDbContext())
                {
                    using (SqlCommand cmd = new SqlCommand("sp_reports_rptconsumer", context.Connection))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@uci", _uci);
                        context.Connection.Open();
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                productos.Add(new ReportsModel
                                {
                                    Date = Convert.ToDateTime(reader["Date"]),
                                    Name = reader["Name"].ToString(),
                                    LastName = reader["LastName"].ToString(),
                                    ConsumerName = reader["Name"].ToString() + " " + reader["LastName"].ToString(),
                                    UCI = reader["UCI"].ToString(),
                                    ParentName = reader["ParentName"].ToString(),
                                    ParentLastName = reader["ParentLastName"].ToString(),
                                    ParentFullName = reader["ParentName"].ToString() + " " + reader["ParentLastName"].ToString(),
                                    //HoursxWeek = Convert.ToInt32(reader["HoursxWeek"]),
                                    RegionalCenter = reader["RegionalCenter"].ToString(),
                                    spe_Name = reader["spe_Name"].ToString(),
                                    spe_LastName = reader["spe_LastName"].ToString(),
                                    spe_FullName = reader["spe_Name"].ToString() + " " + reader["spe_LastName"].ToString(),
                                    Type = reader["Type"].ToString(),
                                    c_image = reader["p_image"].ToString(),
                                    //TotalHours = Convert.ToInt32(reader["TotalHours"].ToString()),
                                    //TimesxWeek = reader["hoursxweek"].ToString()
                                });
                            }
                        }
                    }
                }
            }
            catch (Exception _error)
            {
                throw;
            }
            return productos;
        }

        /// <summary>
        /// aqui se hace el retorno de la ciudad en string y no como ID
        /// 
        /// </summary>
        /* <param name="_uci"></param>*/
        /// <returns></returns>
        /// 

        public List<ReportsModel> get_CityStringr(int cityID)
        {
            var productos = new List<ReportsModel>();
            try
            {
                using (var context = new ApplicationDbContext())
                {
                    using (SqlCommand cmd = new SqlCommand("sp_get_CityString", context.Connection))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@cityID", cityID);
                        context.Connection.Open();
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                productos.Add(new ReportsModel
                                {
                                    ciudad = reader["ciudad"].ToString(),
                                
                                });
                            }
                        }
                    }
                }
            }
            catch (Exception _error)
            {
                throw;
            }
            return productos;
        }

        //aqui termina el metodo
        public List<ReportsModel> Get_NotesXConsumer(string _uci)
        {
            var productos = new List<ReportsModel>();
            try
            {
                using (var context = new ApplicationDbContext())
                {
                    using (SqlCommand cmd = new SqlCommand("SP_Reports_Get_NotesXConsumer", context.Connection))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@uci", _uci);
                        context.Connection.Open();
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                productos.Add(new ReportsModel
                                {
                                    Date = Convert.ToDateTime(reader["Date"]),
                                    Name = reader["Name"].ToString(),
                                    LastName = reader["LastName"].ToString(),
                                    ConsumerName = reader["Name"].ToString() + " " + reader["LastName"].ToString(),
                                    UCI = reader["UCI"].ToString(),
                                    NotesConsumer = reader["Notes"].ToString(),
                                    Duration = reader["Duration"].ToString(),
                                    PresentInSession = reader["PresentInSession"].ToString(),
                                    spe_FullName = reader["spe_Name"].ToString() + " " + reader["spe_LastName"].ToString()
                                    //TotalHours = reader["TotalHours"].ToString(),
                                    //TimesxWeek = reader["TimesxWeek"].ToString()
                                });
                            }
                        }
                    }
                }
            }
            catch (Exception _error)
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
                using (var context = new ApplicationDbContext())
                {
                    using (SqlCommand cmd = new SqlCommand("SP_RPT_Birthdays_xMonth", context.Connection))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@anio", year);
                        cmd.Parameters.AddWithValue("@mes", month);
                        context.Connection.Open();
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                productos.Add(new ReportsModel
                                {
                                    // a.IDConsumer,a.UCI,CONCAT(a.NAME,a.LastName) AS ConsumerName, a.DateOfBirth, b.RegionalCenter
                                    IDConsumer = Convert.ToInt32(reader["IDConsumer"]),
                                    UCI = reader["UCI"].ToString(),
                                    ConsumerName = reader["ConsumerName"].ToString(),
                                    DateOfBirth = Convert.ToDateTime(reader["DateOfBirth"]),
                                    RegionalCenter = reader["RegionalCenter"].ToString()
                                    //spe_FullName = reader["spe_FullName"].ToString()
                                }); ;
                            }
                        }
                    }
                }
            }
            catch (Exception _error)
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
                using (var context = new ApplicationDbContext())
                {
                    using (SqlCommand cmd = new SqlCommand("SP_RPT_Birthdays", context.Connection))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@From", _From);
                        cmd.Parameters.AddWithValue("@To", _To);
                        context.Connection.Open();
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                productos.Add(new ReportsModel
                                {
                                    DateOfBirth = Convert.ToDateTime(reader["DateOfBirth"]),
                                    UCI = reader["UCI"].ToString(),
                                    ConsumerName = reader["ConsumerName"].ToString(),
                                    spe_FullName = reader["spe_FullName"].ToString()
                                }); ;
                            }
                        }
                    }
                }
            }
            catch (Exception _error)
            {
                throw;
            }
            return productos;
        }

        public List<ReportsModel> _RptCenter(string regional, string month, string year)
        {
            var productos = new List<ReportsModel>();
            try
            {
                using (var context = new ApplicationDbContext())
                {
                    using (SqlCommand cmd = new SqlCommand("SP_RPT_Centers", context.Connection))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@Center", regional);
                        cmd.Parameters.AddWithValue("@Month", month);
                        cmd.Parameters.AddWithValue("@Year", year);
                        context.Connection.Open();
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                productos.Add(new ReportsModel
                                {
                                    DateOfBirth = Convert.ToDateTime(reader["DateOfBirth"]),
                                    UCI = reader["UCI"].ToString(),
                                    ConsumerName = reader["ConsumerName"].ToString(),
                                    spe_FullName = reader["spe_FullName"].ToString()
                                }); ;
                            }
                        }
                    }
                }
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
                using (var context = new ApplicationDbContext())
                {
                    using (SqlCommand cmd = new SqlCommand("SP_RPT_CDS", context.Connection))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@CDS", Convert.ToInt32(cds));
                        cmd.Parameters.AddWithValue("@Month", month);
                        cmd.Parameters.AddWithValue("@Year", year);
                        context.Connection.Open();
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                productos.Add(new ReportsModel
                                {
                                    ConsumerName = reader["ConsumerName"].ToString(),
                                    DateOfBirth = Convert.ToDateTime(reader["DateOfBirth"]),
                                    RegionalCenter = reader["RegionalCenter"].ToString(),
                                    ServiceCoordinator = reader["ServiceCoordinator"].ToString(),
                                    Report1 = Convert.ToDateTime(reader["Report1"]),
                                    Report2 = Convert.ToDateTime(reader["Report2"]),
                                    Report3 = Convert.ToDateTime(reader["Report3"]),
                                    Report4 = Convert.ToDateTime(reader["Report4"]),
                                    ReportClose = Convert.ToDateTime(reader["ReportClose"]),
                                    spe_FullName = reader["SpecialistName"].ToString(),
                                }); ;
                            }
                        }
                    }
                }
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
                using (var context = new ApplicationDbContext())
                {
                    using (SqlCommand cmd = new SqlCommand("SP_RPT_CDSConsumer", context.Connection))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@CDS", Convert.ToInt32(cds));
                        context.Connection.Open();
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                productos.Add(new ReportsModel
                                {
                                    ConsumerName = reader["ConsumerName"].ToString(),
                                    DateOfBirth = Convert.ToDateTime(reader["DateOfBirth"]),
                                    Address = reader["Address"].ToString(),
                                    CityID = reader["CityID"].ToString(),
                                    HoursxWeek = Convert.ToInt32(reader["HoursxWeek"]),
                                    State = reader["State"].ToString(),
                                    ZipCode = reader["ZipCode"].ToString(),
                                    Phone = reader["Phone"].ToString(),
                                    ServiceCoordinator = reader["ServiceCoordinator"].ToString(),
                                    ParentFullName = reader["ParentName"].ToString(),
                                    LanguajeName = reader["Languaje"].ToString(),
                                    spe_FullName = reader["SpecialistName"].ToString(),
                                }); ;
                            }
                        }
                    }
                }
            }
            catch (Exception _error)
            {
                throw;
            }
            return productos;
        }


        public List<ReportsModel> _Rpt_Data_CentersByMonth(DateTime? _inicio, DateTime? _fin, string _center)
        {
            List<ReportsModel> list = new List<ReportsModel>();
            try
            {
                using (var context = new ApplicationDbContext())
                {
                    using (SqlCommand cmd = new SqlCommand("SP_RPT_CentersByMoth", context.Connection))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@Inicio", Convert.ToDateTime(_inicio));
                        cmd.Parameters.AddWithValue("@Fin", Convert.ToDateTime(_fin));
                        cmd.Parameters.AddWithValue("@Center", Convert.ToInt32(_center));
                        context.Connection.Open();
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                list.Add(new ReportsModel
                                {
                                    ConsumerName = reader["ConsumerName"].ToString(),
                                    MaxHours = Convert.ToInt32(reader["MaxHours"]),
                                    RegionalCenter= reader["center"].ToString()
                                });
                            }
                        }
                    }
                }
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
                using (var context = new ApplicationDbContext())
                {
                    using (SqlCommand cmd = new SqlCommand("SP_RPT_NewConsumersByMoth", context.Connection))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@From", _From);
                        cmd.Parameters.AddWithValue("@To", _To);
                        context.Connection.Open();
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                productos.Add(new ReportsModel
                                {
                                    ConsumerName = reader["ConsumerName"].ToString(),
                                    spe_FullName = reader["Therapist"].ToString(),
                                    CityID = reader["CityID"].ToString(),
                                    DateOfBirth = Convert.ToDateTime(reader["DateOfBirth"]),
                                    RegionalCenter = reader["RegionalCenter"].ToString(),
                                    ServiceCoordinator= reader["ServiceCoordinator"].ToString(),
                                    _From = Convert.ToDateTime(reader["_From"]),
                                    _To = Convert.ToDateTime(reader["_To"]),
                                    MaxHours = Convert.ToInt32(reader["MaxHours"]),
                                    PresenterName = reader["PresenterName"].ToString(),
                                    ParentFullName = reader["CDS"].ToString(),
                                }); ;
                            }
                        }
                    }
                }
            }
            catch (Exception _error)
            {
                throw;
            }
            return productos;
        }

    }
}