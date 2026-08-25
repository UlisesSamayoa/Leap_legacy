using LEAP.Data;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.BuilderProperties;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection.Emit;
using System.Runtime.InteropServices;
using System.Security.Policy;
using System.Web;
using System.Web.Mvc;
using static System.Data.Entity.Infrastructure.Design.Executor;

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
        //public int CityID { get; set; }
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
        public string c_image { get; set; }
        public bool? CB { get; set;}
        public bool PEP { get; set; }
        public DateTime TerminationDateEffective { get; set; }
        public string rc { get; set; }
        //METODOS
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
                                   //// specialist1 = Convert.ToInt32(reader["specialist1"]),
                                   // specialist2 = Convert.ToInt32(reader["specialist2"]),
                                   // specialist3 = Convert.ToInt32(reader["specialist3"]),
                                   // PresenterName = reader["PresenterName"].ToString(),
                                    Name = reader["Name"].ToString(),
                                   LastName = reader["LastName"].ToString(),
                                    DateOfBirth = Convert.ToDateTime(reader["DateOfBirth"]),
                                   //adjage = Convert.ToDateTime(reader["adjage"]),
                                   // Gender = reader["Gender"].ToString(),
                                   // Address = reader["Address"].ToString(),
                                   // //CityID = Convert.ToInt32(reader["CityID"]),
                                   // CityID = reader["CityID"].ToString(),
                                    ZipCode = reader["ZipCode"].ToString(),
                                    Phone = reader["Phone"].ToString(),
                                    rc = reader["RegionalCenter"].ToString(),
                                    // EmergencyPhone = reader["EmergencyPhone"].ToString(),
                                    ParentName = reader["ParentName"].ToString(),
                                     ParentLastName = reader["ParentLastName"].ToString()
                                    // LanguajeID = Convert.ToInt32(reader["LanguajeID"]),
                                    // Reasonforreferral = reader["Reasonforreferral"].ToString(),
                                    // AuthID = Convert.ToInt32(reader["AuthID"]),
                                    // HoursxWeek = Convert.ToInt32(reader["HoursxWeek"]),
                                    // MaxHours = Convert.ToInt32(reader["MaxHours"]),
                                    // TerminationNumber = Convert.ToInt32(reader["TerminationNumber"]),
                                    // AdditionalEval = reader["AdditionalEval"].ToString(),
                                    // InHome = Convert.ToBoolean(reader["InHome"]),
                                    // //EIWITH = Convert.ToBoolean(reader["EIWITH"]),
                                    // OTPT = Convert.ToBoolean(reader["OTPT"]),
                                    //initEval = Convert.ToDateTime(reader["initEval"]),
                                    //evaldueby = Convert.ToDateTime(reader["evaldueby"]),
                                    // Report1 = Convert.ToDateTime(reader["Report1"]),
                                    //Report2 = Convert.ToDateTime(reader["Report2"]),
                                    // Report3 = Convert.ToDateTime(reader["Report3"]),
                                    // Report4 = Convert.ToDateTime(reader["Report4"]),
                                    // Report5 = Convert.ToDateTime(reader["Report5"]),
                                    // ReportClose = Convert.ToDateTime(reader["ReportClose"]),
                                    // Date = Convert.ToDateTime(reader["Date"]),
                                    // ReferredBy = Convert.ToInt32(reader["ReferredBy"]),
                                    // evaluation = reader["evaluation"].ToString(),
                                    // TypeReporte = reader["TypeReporte"].ToString(),
                                    // RegionalID = Convert.ToInt32(reader["RegionalID"]),
                                    // ServiceCoordinator = reader["ServiceCoordinator"].ToString(),
                                    // Archive = Convert.ToInt32(reader["Archive"]),
                                    // Type = reader["Type"].ToString(),
                                    // Action = Convert.ToInt32(reader["Action"]),
                                    // c_image = reader["p_image"].ToString(),
                                    // CB = Convert.ToBoolean(reader["cb"]),
                                    // PEP = Convert.ToBoolean(reader["pep"])
                                });
                            }
                        }
                        context.Connection.Close();
                    }
                }
            }
            catch (Exception _error)
            {
                throw;
            }
            return productos;
        }

        public List<ConsumerModel> Get_Consumer_ByID(string _UCI)
        {
            var productos = new List<ConsumerModel>();
            try
            {
                using (var context = new ApplicationDbContext())
                {
                    using (SqlCommand cmd = new SqlCommand("Get_Consumer_byID", context.Connection))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@UCI", _UCI);
                        context.Connection.Open();
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                productos.Add(new ConsumerModel
                                {
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
                                    State = reader["State"].ToString(),
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
                                    Report5 = Convert.ToDateTime(reader["Report5"]),
                                    ReportClose = Convert.ToDateTime(reader["ReportClose"]),
                                    Date = Convert.ToDateTime(reader["Date"]),
                                    ReferredBy = Convert.ToInt32(reader["ReferredBy"]),
                                    evaluation = reader["evaluation"].ToString(),
                                    TypeReporte = reader["TypeReporte"].ToString(),
                                    RegionalID = Convert.ToInt32(reader["RegionalID"]),
                                    ServiceCoordinator = reader["ServiceCoordinator"].ToString(),
                                    Archive = Convert.ToInt32(reader["Archive"]),
                                    Type = reader["Type"].ToString(),
                                    Action = Convert.ToInt32(reader["Action"]),
                                    c_image = reader["p_image"].ToString(),
                                    CB = Convert.ToBoolean(reader["cb"]),
                                    PEP = Convert.ToBoolean(reader["pep"]),
                                    UserC = reader["UserC"].ToString()
                                });
                            }
                        }
                        context.Connection.Close();
                    }
                }
            }
            catch (Exception _error)
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
                using (var context = new ApplicationDbContext())
                {
                    using (SqlCommand cmd = new SqlCommand("Get_ConsumerxID", context.Connection))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@IDConsumer", idconsumer);
                        //cmd.Parameters.AddWithValue("@IDConsumer", idconsumer);
                        context.Connection.Open();
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                productos.UCI = reader["UCI"].ToString();
                                productos.IDConsumer = Convert.ToInt32(reader["IDConsumer"]);
                                productos.specialist1 = Convert.ToInt32(reader["specialist1"]);
                                productos.specialist2 = Convert.ToInt32(reader["specialist2"]);
                                productos.specialist3 = Convert.ToInt32(reader["specialist3"]);
                                productos.PresenterName = reader["PresenterName"].ToString();
                                productos.Name = reader["Name"].ToString();
                                productos.LastName = reader["LastName"].ToString();
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
                                productos.LanguajeID = Convert.ToInt32(reader["LanguajeID"]);
                                productos.Reasonforreferral = reader["Reasonforreferral"].ToString();
                                productos.AuthID = Convert.ToInt32(reader["AuthID"]);
                                productos.HoursxWeek = Convert.ToInt32(reader["HoursxWeek"]);
                                productos.MaxHours = Convert.ToInt32(reader["MaxHours"]);
                                productos.TerminationNumber = Convert.ToInt32(reader["TerminationNumber"]);
                                productos.AdditionalEval = reader["AdditionalEval"].ToString();
                                productos.InHome = Convert.ToBoolean(reader["InHome"]);
                                //productos.EIWITH = Convert.ToBoolean(reader["EIWITH"]);
                                productos.OTPT = Convert.ToBoolean(reader["OTPT"]);
                                productos.initEval = Convert.ToDateTime(reader["initEval"]);
                                productos.evaldueby = Convert.ToDateTime(reader["evaldueby"]);
                                productos.Report1 = Convert.ToDateTime(reader["Report1"]);
                                productos.Report2 = Convert.ToDateTime(reader["Report2"]);
                                productos.Report3 = Convert.ToDateTime(reader["Report3"]);
                                productos.Report4 = Convert.ToDateTime(reader["Report4"]);
                                productos.Report5 = Convert.ToDateTime(reader["Report5"]);
                                productos.ReportClose = Convert.ToDateTime(reader["ReportClose"]);
                                productos.Date = Convert.ToDateTime(reader["Date"]);
                                productos.ReferredBy = Convert.ToInt32(reader["ReferredBy"]);
                                productos.evaluation = reader["evaluation"].ToString();
                                productos.TypeReporte = reader["TypeReporte"].ToString();
                                productos.RegionalID = Convert.ToInt32(reader["RegionalID"]);
                                productos.ServiceCoordinator = reader["ServiceCoordinator"].ToString();
                                productos.Archive = Convert.ToInt32(reader["Archive"]);
                                productos.Type = reader["Type"].ToString();
                                productos.Action = Convert.ToInt32(reader["Action"]);
                                productos.c_image = reader["p_image"].ToString();
                                productos.CB = Convert.ToBoolean(reader["cb"]);
                                productos.PEP = Convert.ToBoolean(reader["pep"]);
                                //Agregar nuevo campo
                                productos.TerminationDateEffective = Convert.ToDateTime(reader["TerminationDateEffective"]);
                                productos.UserC = UserC = reader["UserC"].ToString();
                            }
                        }
                        context.Connection.Close();
                    }
                }
            }
            catch (Exception _error)
            {
                throw;
            }
            return productos;
        }

        public List<ConsumerModel> Get_Consumer_ByName(string _name)
        {
            var productos = new List<ConsumerModel>();
            try
            {
                using (var context = new ApplicationDbContext())
                {
                    using (SqlCommand cmd = new SqlCommand("Get_Consumer_byName", context.Connection))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@name", _name);
                        context.Connection.Open();
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                productos.Add(new ConsumerModel
                                {
                                    IDConsumer = Convert.ToInt32(reader["UCI"]),
                                    Name = reader["Name"].ToString(),
                                });
                            }
                        }
                        context.Connection.Close();
                    }
                }
            }
            catch (Exception _error)
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
                using (var context = new ApplicationDbContext())
                {
                    using (SqlCommand cmd = new SqlCommand("SP_Consumer_AllDatabyName", context.Connection))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@NAME", _name);
                        context.Connection.Open();
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                productos.Add(new ConsumerModel
                                {
                                    IDConsumer = Convert.ToInt32(reader["UCI"]),
                                    UCI = reader["UCI"].ToString(),
                                    Name = reader["Name"].ToString(),
                                    LastName = reader["LastName"].ToString(),
                                });
                            }
                        }
                        context.Connection.Close();
                    }
                }
            }
            catch (Exception _error)
            {
                throw;
            }
            return productos;
        }


        public string Create_Consumer(string _UCI, int _specialist1, int _specialist2, int _specialist3, string _PresenterName, string _Name,
            string _LastName, DateTime _DateOfBirth, DateTime _adjage, string _Gender, string _Address, string _CityID, string _ZipCode, string _Phone,
            string _EmergencyPhone, string _ParentName, string _ParentLastName, int _LanguajeID, string _Reasonforreferral, int _AuthID,
            int _HoursxWeek, int _MaxHours, int _TerminationNumber, string _AdditionalEval, bool _InHome, bool _EIWITH, bool _CB,bool _PEP,
            DateTime _initEval, DateTime _evaldueby, DateTime _Report1, DateTime _Report2, DateTime _Report3, DateTime _Report4, DateTime _Report5, DateTime _ReportClose,
            DateTime _Date, int _ReferredBy, string _evaluation, string _TypeReporte, int _RegionalID, string _ServiceCoordinator,
             int _Archive, string _Type, int _Action, string _UserC, DateTime _DateC, string _UserU, DateTime _DateU, bool _OTPT, DateTime _terminationdate,string _State )
        {
            string mensaje = "";
            DateTime now = DateTime.Now;
            try
            {
                using (var context = new ApplicationDbContext())
                {
                    using (SqlCommand cmd = new SqlCommand("SP_Create_Consumer", context.Connection))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@UCI", _UCI);
                        cmd.Parameters.AddWithValue("@specialist1", _specialist1);
                        cmd.Parameters.AddWithValue("@specialist2", _specialist2);
                        cmd.Parameters.AddWithValue("@specialist3", _specialist3);
                        cmd.Parameters.AddWithValue("@PresenterName", _PresenterName);
                        cmd.Parameters.AddWithValue("@Name", _Name);
                        cmd.Parameters.AddWithValue("@LastName", _LastName);
                        cmd.Parameters.AddWithValue("@DateOfBirth", _DateOfBirth);
                        cmd.Parameters.AddWithValue("@adjage", _adjage);
                        cmd.Parameters.AddWithValue("@Gender", _Gender);
                        cmd.Parameters.AddWithValue("@Address", _Address);
                        cmd.Parameters.AddWithValue("@CityID", _CityID);
                        cmd.Parameters.AddWithValue("@State", _State);
                        cmd.Parameters.AddWithValue("@ZipCode", _ZipCode);
                        cmd.Parameters.AddWithValue("@Phone", _Phone);
                        cmd.Parameters.AddWithValue("@EmergencyPhone", _EmergencyPhone);
                        cmd.Parameters.AddWithValue("@ParentName", _ParentName);
                        cmd.Parameters.AddWithValue("@ParentLastName", _ParentLastName);
                        cmd.Parameters.AddWithValue("@LanguajeID", _LanguajeID);
                        cmd.Parameters.AddWithValue("@Reasonforreferral", _Reasonforreferral);
                        cmd.Parameters.AddWithValue("@AuthID", _AuthID);
                        cmd.Parameters.AddWithValue("@HoursxWeek", _HoursxWeek);
                        cmd.Parameters.AddWithValue("@MaxHours", _MaxHours);
                        cmd.Parameters.AddWithValue("@TerminationNumber", _TerminationNumber);
                        cmd.Parameters.AddWithValue("@AdditionalEval", _AdditionalEval);
                        cmd.Parameters.AddWithValue("@InHome", _InHome);
                        //cmd.Parameters.AddWithValue("@EIWITH", _EIWITH);
                        cmd.Parameters.AddWithValue("@OTPT", _OTPT);
                        cmd.Parameters.AddWithValue("@CB", _CB);
                        cmd.Parameters.AddWithValue("@PEP", _PEP);
                        cmd.Parameters.AddWithValue("@initEval", _initEval);
                        cmd.Parameters.AddWithValue("@evaldueby", _evaldueby);
                        cmd.Parameters.AddWithValue("@Report1", _Report1);
                        cmd.Parameters.AddWithValue("@Report2", _Report2);
                        cmd.Parameters.AddWithValue("@Report3", _Report3);
                        cmd.Parameters.AddWithValue("@Report4", _Report4);
                        cmd.Parameters.AddWithValue("@Report5", _Report5);
                        cmd.Parameters.AddWithValue("@ReportClose", _ReportClose);
                        cmd.Parameters.AddWithValue("@Date", DateTime.Now);
                        cmd.Parameters.AddWithValue("@ReferredBy", _ReferredBy);
                        cmd.Parameters.AddWithValue("@evaluation", _evaluation);
                        cmd.Parameters.AddWithValue("@TypeReporte", _TypeReporte);
                        cmd.Parameters.AddWithValue("@RegionalID", _RegionalID);
                        cmd.Parameters.AddWithValue("@ServiceCoordinator", _ServiceCoordinator);
                        cmd.Parameters.AddWithValue("@Archive", _Archive);
                        cmd.Parameters.AddWithValue("@Type", _Type);
                        cmd.Parameters.AddWithValue("@Action", _Action);
                        cmd.Parameters.AddWithValue("@UserC", _UserC);
                        cmd.Parameters.AddWithValue("@DateC", _DateC);
                        cmd.Parameters.AddWithValue("@UserU", _UserU);
                        cmd.Parameters.AddWithValue("@DateU", _DateU);
                        context.Connection.Open();
                        cmd.ExecuteNonQuery();
                        context.Connection.Close();
                    }
                }
            }
            catch (Exception _error)
            {
                _error.Message.ToString();
            }
            return mensaje;
        }


        public List<BirthdaysModel> Get_births(string month)
        {
            var productos = new List<BirthdaysModel>();
            try
            {
                using (var context = new ApplicationDbContext())
                {
                    using (SqlCommand cmd = new SqlCommand("SP_Get_Birthdays", context.Connection))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@month", month);
                        context.Connection.Open();
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                productos.Add(new BirthdaysModel
                                {
                                    UCI = reader["UCI"].ToString(),
                                    Month = reader["Month"].ToString(),
                                    Name = reader["Name"].ToString(),
                                    LastName = reader["LastName"].ToString(),
                                    DateOfBirth = reader["DateOfBirth"].ToString(),
                                    Type = reader["type"].ToString(),
                                });
                            }
                        }
                        context.Connection.Close();
                    }
                }
            }
            catch (Exception _error)
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
                using (var context = new ApplicationDbContext())
                {
                    using (SqlCommand cmd = new SqlCommand("SP_Consumer_Delete", context.Connection))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@IDConsumer", _IDConsumer);
                        context.Connection.Open();
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                        }
                        response = true;
                        context.Connection.Close();
                    }
                }
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
                using (var context = new ApplicationDbContext())
                {
                    using (SqlCommand cmd = new SqlCommand("Get_ConsumerxID", context.Connection))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@IDConsumer", uci);
                        context.Connection.Open();
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                productos.Add(new ConsumerModel
                                {
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
                                    State = reader["State"].ToString(),
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
                        context.Connection.Close();
                    }
                }
            }
            catch (Exception _error)
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
                using (var context = new ApplicationDbContext())
                {
                    using (SqlCommand cmd = new SqlCommand("SP_SubirImagen", context.Connection))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@_UCI", _UCI);
                        cmd.Parameters.AddWithValue("@C_Image", C_Image);
                        context.Connection.Open();
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                        }
                        response = true;
                        context.Connection.Close();
                    }
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
                using (var context = new ApplicationDbContext())
                {
                    using (SqlCommand cmd = new SqlCommand("SP_ChangeStatusConsumer", context.Connection))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@_UCI", _UCI);
                        cmd.Parameters.AddWithValue("@Status", _Status);
                        context.Connection.Open();
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                        }
                        response = true;
                        context.Connection.Close();
                    }
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
                using (var context = new ApplicationDbContext())
                {
                    using (SqlCommand cmd = new SqlCommand("Get_Consumer_allDataStatus", context.Connection))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@Status", status);
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
                                    //CityID = Convert.ToInt32(reader["CityID"]),
                                    CityID = reader["CityID"].ToString(),
                                    State = reader["State"].ToString(),
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
                                    //EIWITH = Convert.ToBoolean(reader["EIWITH"]),
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
                                    Action = Convert.ToInt32(reader["Action"]),
                                    c_image = reader["p_image"].ToString()

                                });
                            }
                        }
                        context.Connection.Close();
                    }
                }
            }
            catch (Exception _error)
            {
                throw;
            }
            return productos;
        }


        public string Update_Consumer(string _UCI, int _specialist1, int _specialist2, int _specialist3, string _PresenterName, string _Name,
           string _LastName, DateTime _DateOfBirth, DateTime _adjage, string _Gender, string _Address, string _CityID, string _ZipCode, string _Phone,
           string _EmergencyPhone, string _ParentName, string _ParentLastName, int _LanguajeID, string _Reasonforreferral, int _AuthID,
           int _HoursxWeek, int _MaxHours, int _TerminationNumber, string _AdditionalEval, bool _InHome, bool _EIWITH, bool _CB, bool _PEP,
           DateTime _initEval, DateTime _evaldueby, DateTime _Report1, DateTime _Report2, DateTime _Report3, DateTime _Report4, DateTime _Report5, DateTime _ReportClose,
           DateTime _Date, int _ReferredBy, string _evaluation, string _TypeReporte, int _RegionalID, string _ServiceCoordinator,
            int _Archive, string _Type, int _Action, string _UserC, DateTime _DateC, string _UserU, DateTime _DateU, bool _OTPT,DateTime _terminationdate, string _State)

        {
            string mensaje = "";
            if (string.IsNullOrEmpty(_Gender)) {
                _Gender = "";
           
            }
            try
            {
                using (var context = new ApplicationDbContext())
                {
                    using (SqlCommand cmd = new SqlCommand("SP_Update_Consumer", context.Connection))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@UCI", _UCI);
                        cmd.Parameters.AddWithValue("@specialist1", _specialist1);
                        cmd.Parameters.AddWithValue("@specialist2", _specialist2);
                        cmd.Parameters.AddWithValue("@specialist3", _specialist3);
                       cmd.Parameters.AddWithValue("@PresenterName", _PresenterName);
                        cmd.Parameters.AddWithValue("@Name", _Name);
                        cmd.Parameters.AddWithValue("@LastName", _LastName);
                        cmd.Parameters.AddWithValue("@DateOfBirth", _DateOfBirth);
                        cmd.Parameters.AddWithValue("@adjage", _adjage);
                        cmd.Parameters.AddWithValue("@Gender", _Gender);
                        cmd.Parameters.AddWithValue("@Address", _Address);
                        cmd.Parameters.AddWithValue("@CityID", _CityID);
                        cmd.Parameters.AddWithValue("@State", _State);
                        cmd.Parameters.AddWithValue("@ZipCode", _ZipCode);
                        cmd.Parameters.AddWithValue("@Phone", _Phone);
                        cmd.Parameters.AddWithValue("@EmergencyPhone", _EmergencyPhone);
                        cmd.Parameters.AddWithValue("@ParentName", _ParentName);
                        cmd.Parameters.AddWithValue("@ParentLastName", _ParentLastName);
                        cmd.Parameters.AddWithValue("@LanguajeID", _LanguajeID);
                        //cmd.Parameters.AddWithValue("@LanguajeID", 1);
                        cmd.Parameters.AddWithValue("@Reasonforreferral", _Reasonforreferral);
                        cmd.Parameters.AddWithValue("@AuthID", _AuthID);
                        cmd.Parameters.AddWithValue("@HoursxWeek", _HoursxWeek);
                        cmd.Parameters.AddWithValue("@MaxHours", _MaxHours);
                        cmd.Parameters.AddWithValue("@TerminationNumber", _TerminationNumber);
                        cmd.Parameters.AddWithValue("@AdditionalEval", _AdditionalEval);
                        cmd.Parameters.AddWithValue("@InHome", _InHome);
                        cmd.Parameters.AddWithValue("@EIWITH", _EIWITH);
                        cmd.Parameters.AddWithValue("@OTPT", _OTPT);
                        cmd.Parameters.AddWithValue("@CB", _CB);
                        cmd.Parameters.AddWithValue("@PEP", _PEP);
                        cmd.Parameters.AddWithValue("@initEval", _initEval);
                        cmd.Parameters.AddWithValue("@evaldueby", _evaldueby);
                        cmd.Parameters.AddWithValue("@Report1", _Report1);
                        cmd.Parameters.AddWithValue("@Report2", _Report2);
                        cmd.Parameters.AddWithValue("@Report3", _Report3);
                        cmd.Parameters.AddWithValue("@Report4", _Report4);
                        cmd.Parameters.AddWithValue("@Report5", _Report5);
                        cmd.Parameters.AddWithValue("@ReportClose", _ReportClose);
                        cmd.Parameters.AddWithValue("@Date", _Date);
                        cmd.Parameters.AddWithValue("@ReferredBy", _ReferredBy);
                        cmd.Parameters.AddWithValue("@evaluation", _evaluation);
                        cmd.Parameters.AddWithValue("@TypeReporte", _TypeReporte);
                        cmd.Parameters.AddWithValue("@RegionalID", _RegionalID);
                        cmd.Parameters.AddWithValue("@ServiceCoordinator", _ServiceCoordinator);
                        cmd.Parameters.AddWithValue("@Archive", _Archive);
                        cmd.Parameters.AddWithValue("@Type", _Type);
                        cmd.Parameters.AddWithValue("@Action", _Action);
                        cmd.Parameters.AddWithValue("@UserC", _UserC);
                        cmd.Parameters.AddWithValue("@DateC", _DateC);
                        cmd.Parameters.AddWithValue("@UserU", _UserU);
                        cmd.Parameters.AddWithValue("@DateU", _DateU);
                        cmd.Parameters.AddWithValue("@TerminationDateEffective", _terminationdate);

                        context.Connection.Open();
                        cmd.ExecuteNonQuery();
                        context.Connection.Close();
                    }
                }
            }
            catch (Exception _error)
            {
                _error.Message.ToString();
            }
            return mensaje;
        }

        public bool ArchiveAll()
        {
            bool response = false;
            try
            {
                using (var context = new ApplicationDbContext())
                {
                    using (SqlCommand cmd = new SqlCommand("Sp_ArchiveAll", context.Connection))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        context.Connection.Open();
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                        }
                        response = true;
                        context.Connection.Close();
                    }
                }
               // _log._logAction("Update IMagen", "Update IMagen, id:" + _UCI, "AgregarImagenXConsumer", "AgregarImagenXConsumer", _UserName);
            }
            catch (Exception _error)
            {
                response = false;
               // _log._logError(_error.Message, _error.StackTrace, "AgregarImagenXConsumer", "AgregarImagenXConsumer", _UserName);
            }
            return response;
        }

        public ConsumerModel GetNextConsumerByUCI(int _uci)
        {
            var productos = new ConsumerModel();
            try
            {
                using (var context = new ApplicationDbContext())
                {
                    using (SqlCommand cmd = new SqlCommand("GetNextConsumerByUCI", context.Connection))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@uci", _uci);
                        context.Connection.Open();
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {

                                productos.IDConsumer = Convert.ToInt32(reader["IDConsumer"]);
                                productos.Name = reader["Name"].ToString();
                                
                            }
                        }
                        context.Connection.Close();
                    }
                }
            }
            catch (Exception _error)
            {
                throw;
            }
            return productos;
        }

        public ConsumerModel GetPrevConsumerByUCI(int _uci)
        {
            var productos = new ConsumerModel();
            try
            {
                using (var context = new ApplicationDbContext())
                {
                    using (SqlCommand cmd = new SqlCommand("GetPreviousConsumerByUCI", context.Connection))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@uci", _uci);
                        context.Connection.Open();
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {

                                productos.IDConsumer = Convert.ToInt32(reader["IDConsumer"]);
                                productos.Name = reader["Name"].ToString();

                            }
                        }
                        context.Connection.Close();
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
