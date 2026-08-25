using LEAP.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
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
        //VALIDAR SI NO ES MEJOR UN SOLO CAMPO DATETIME
        public DateTime Date { get; set; }
        //public string Time { get; set; }
        //VALIDAR SI NO ES MEJOR UN SOLO CAMPO DATETIME
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
        public List<NotesxMaestroModel> Get_NotesxMaestro()
        {
            var _NotesxMaestro_Response = new List<NotesxMaestroModel>();
            try
            {
                using (var context = new ApplicationDbContext())
                {
                    using (SqlCommand cmd = new SqlCommand("SP_NotesxMaestro_AllData", context.Connection))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        context.Connection.Open();
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                _NotesxMaestro_Response.Add(new NotesxMaestroModel
                                {
                                    IDNotesxMaestro = Convert.ToInt32(reader["IDNotesxMaestro"]),
                                    UCI = reader["UCI"].ToString(),
                                    ConsumerName = reader["ConsumerName"].ToString(),
                                    //RegionalID = Convert.ToInt32(reader["RegionalID"]),
                                    RegionalName = reader["RegionalName"].ToString(),
                                    //TimesxWeek = reader["TimesxWeek"].ToString(),
                                    Date = Convert.ToDateTime(reader["Date"]),
                                    Duration = reader["Duration"].ToString(),
                                    PresentInSession = reader["PresentInSession"].ToString(),
                                    Notes = reader["Notes"].ToString(),
                                    SpecialitID = Convert.ToInt32(reader["SpecialitID"]),
                                    SpecialitName = reader["SpecialitName"].ToString(),
                                    //TotalHours = reader["TotalHours"].ToString()
                                });
                            }
                        }
                    }
                }
            }
            catch (Exception _error)
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
                using (var context = new ApplicationDbContext())
                {
                    using (SqlCommand cmd = new SqlCommand("SP_NotesxMaestro_AllDataByID", context.Connection))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@ID", _NotesxMaestro);
                        context.Connection.Open();
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                _NotesxMaestro_Response.IDNotesxMaestro = Convert.ToInt32(reader["IDNotesxMaestro"]);
                                _NotesxMaestro_Response.UCI = reader["UCI"].ToString();
                                _NotesxMaestro_Response.ConsumerName = reader["ConsumerName"].ToString();
                                //_NotesxMaestro_Response.RegionalID = Convert.ToInt32(reader["RegionalID"]);
                                //_NotesxMaestro_Response.RegionalName = reader["RegionalName"].ToString();
                                //_NotesxMaestro_Response.TimesxWeek = reader["TimesxWeek"].ToString();
                                _NotesxMaestro_Response.Date = Convert.ToDateTime(reader["Date"]);
                                _NotesxMaestro_Response.Duration = reader["Duration"].ToString();
                                _NotesxMaestro_Response.PresentInSession = reader["PresentInSession"].ToString();
                                _NotesxMaestro_Response.Notes = reader["Notes"].ToString();
                                _NotesxMaestro_Response.SpecialitID = Convert.ToInt32(reader["SpecialitID"]);
                                _NotesxMaestro_Response.SpecialitName = reader["SpecialitName"].ToString();
                                //_NotesxMaestro_Response.TotalHours = reader["TotalHours"].ToString();
                                _NotesxMaestro_Response._ErrorCode = false;
                            }
                        }
                    }
                }
            }
            catch (Exception _error)
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
                using (var context = new ApplicationDbContext())
                {
                    using (SqlCommand cmd = new SqlCommand("SP_NotesxMaestro_AllDataByID_Consumer", context.Connection))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@ID", _NotesxMaestro);
                        context.Connection.Open();
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                _NotesxConsumer_Response.Add(new NotesxMaestroModel
                                {
                                    IDNotesxMaestro = Convert.ToInt32(reader["IDNotesxMaestro"]),
                                    UCI = reader["UCI"].ToString(),
                                    ConsumerName = reader["ConsumerName"].ToString(),
                                    //RegionalID = Convert.ToInt32(reader["RegionalID"]),
                                    //RegionalName = reader["RegionalName"].ToString(),
                                    //TimesxWeek = reader["TimesxWeek"].ToString(),
                                    Date = Convert.ToDateTime(reader["Date"]),
                                    Duration = reader["Duration"].ToString(),
                                    PresentInSession = reader["PresentInSession"].ToString(),
                                    Notes = reader["Notes"].ToString(),
                                    SpecialitID = Convert.ToInt32(reader["SpecialitID"]),
                                    SpecialitName = reader["SpecialitName"].ToString(),
                                });
                            }
                        }
                    }
                }
            }
            catch (Exception _error)
            {
                _NotesxConsumer_Response.Add(new NotesxMaestroModel { _ErrorCode = true });
            }
            return _NotesxConsumer_Response;
        }

        //Agregar
        //public bool AddNotesxMaestro(string _UCI, int _RegionalID , int _SpecialitID,string _TimesxWeek,string _Duration, string _TotalHours, DateTime _Date, string _PresentInSession, string _Notes, string _UserName)
        public bool AddNotesxMaestro(string _UCI , int _SpecialitID, DateTime? _Date, string _Duration, string _PresentInSession, string _Notes, string _UserName)
        {
            bool response = false;
            try
            {
                using (var context = new ApplicationDbContext())
                {
                    using (SqlCommand cmd = new SqlCommand("SP_NotesxMaestro_Create", context.Connection))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@UCI", _UCI);
                        //cmd.Parameters.AddWithValue("@RegionalID", _RegionalID);
                        cmd.Parameters.AddWithValue("@SpecialitID", _SpecialitID);
                        //cmd.Parameters.AddWithValue("@TimesxWeek", _TimesxWeek);
                        cmd.Parameters.AddWithValue("@Duration", _Duration);
                        //cmd.Parameters.AddWithValue("@TotalHours", _TotalHours);
                        cmd.Parameters.AddWithValue("@Date", _Date);
                        cmd.Parameters.AddWithValue("@PresentInSession", _PresentInSession);
                        cmd.Parameters.AddWithValue("@Notes", _Notes);
                        cmd.Parameters.AddWithValue("@UserC", _UserName);
                        cmd.Parameters.AddWithValue("@DateC", DateTime.Today);
                        context.Connection.Open();
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                        }
                        response = true;
                    }
                }
                _log._logAction("Create NotesxMaestro", "Create a new NotesxMaestro, UCI:" + _UCI, "AddNotesxMaestro", "NotesxMaestroModel", _UserName);
            }
            catch (Exception _error)
            {
                response = false;
                _log._logError(_error.Message, _error.StackTrace, "AddNotesxMaestro", "NotesxMaestroModel", _UserName);
            }
            return response;
        }
        
        //public bool UpdateNotesxMaestro(string _IDNotesxMaestro, string _UCI, int _RegionalID, int _SpecialitID, int _TimesxWeek, int _Duration, int _TotalHours, DateTime _Date, string _PresentInSession, string _Notes, string _UserName)
        public bool UpdateNotesxMaestro(string _IDNotesxMaestro, string _UCI, int _SpecialitID,string _Duration, DateTime _Date, string _PresentInSession, string _Notes, string _UserName)
        {
            bool response = false;
            try
            {
                using (var context = new ApplicationDbContext())
                {
                    using (SqlCommand cmd = new SqlCommand("SP_NotesxMaestro_Update", context.Connection))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@IDNotesxMaestro", _IDNotesxMaestro);
                        //cmd.Parameters.AddWithValue("@RegionalID", _RegionalID);
                        cmd.Parameters.AddWithValue("@SpecialitID", _SpecialitID);
                        //cmd.Parameters.AddWithValue("@TimesxWeek", _TimesxWeek);
                        cmd.Parameters.AddWithValue("@Duration", _Duration);
                        //cmd.Parameters.AddWithValue("@TotalHours", _TotalHours);
                        cmd.Parameters.AddWithValue("@Date", _Date);
                        cmd.Parameters.AddWithValue("@PresentInSession", _PresentInSession);
                        cmd.Parameters.AddWithValue("@Notes", _Notes);
                        cmd.Parameters.AddWithValue("@UserU", _UserName);
                        cmd.Parameters.AddWithValue("@DateU", DateTime.Today);
                        context.Connection.Open();
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                        }
                        response = true;
                    }
                }
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
                using (var context = new ApplicationDbContext())
                {
                    using (SqlCommand cmd = new SqlCommand("SP_NotesxMaestro_Delete", context.Connection))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@IDNotesxMaestro", _IDNotesxMaestro);
                        context.Connection.Open();
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                        }
                        response = true;
                    }
                }
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