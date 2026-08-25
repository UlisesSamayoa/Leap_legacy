using LEAP.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace LEAP.Models
{
    public class EventTypeModel
    {
        public int? IDEventType { get; set; }
        public string Title { get; set; }
        public string Color { get; set; }
        public string UserC { get; set; }
        public DateTime? DateC { get; set; }
        public string UserU { get; set; }
        public DateTime? DateU { get; set; }
        public bool _ErrorCode { get; set; }

        LogModel _log = new LogModel();

        public List<EventTypeModel> Get_EventType()
        {
            var _Languajes_Response = new List<EventTypeModel>();
            try
            {
                using (var context = new ApplicationDbContext())
                {
                    using (SqlCommand cmd = new SqlCommand("SP_EventType_AllData", context.Connection))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        context.Connection.Open();
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                _Languajes_Response.Add(new EventTypeModel
                                {
                                    IDEventType = Convert.ToInt32(reader["IDEventType"]),
                                    Title = reader["Title"].ToString(),
                                    Color = reader["Color"].ToString(),
                                });
                            }
                        }
                    }
                }
            }
            catch (Exception _error)
            {
                _Languajes_Response.Add(new EventTypeModel { _ErrorCode = true });
            }
            return _Languajes_Response;
        }

        public EventTypeModel Get_EventType_ByID(int _EventType)
        {
            var _EventType_Response = new EventTypeModel();
            try
            {
                using (var context = new ApplicationDbContext())
                {
                    using (SqlCommand cmd = new SqlCommand("SP_EventType_AllDataByID", context.Connection))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@ID", _EventType);
                        context.Connection.Open();
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                _EventType_Response.IDEventType = Convert.ToInt32(reader["IDEventType"]);
                                _EventType_Response.Title = reader["Title"].ToString();
                                _EventType_Response.Color = reader["Color"].ToString();
                                _EventType_Response._ErrorCode = false;
                            }
                        }
                    }
                }
            }
            catch (Exception _error)
            {
                _EventType_Response._ErrorCode = true;
            }
            return _EventType_Response;
        }

        public bool AddEventType(string _Title, string _Color, string _UserName)
        {
            bool response = false;
            try
            {
                using (var context = new ApplicationDbContext())
                {
                    using (SqlCommand cmd = new SqlCommand("SP_EventType_Create", context.Connection))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@Title", _Title);
                        cmd.Parameters.AddWithValue("@Color", _Color);
                        cmd.Parameters.AddWithValue("@UserC", _UserName);
                        cmd.Parameters.AddWithValue("@DateC", DateTime.Today);
                        context.Connection.Open();
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                        }
                        response = true;
                    }
                }
                _log._logAction("Create Event", "Create a new Event, name:" + _Title, "AddEvent", "EventTypeModel", _UserName);
            }
            catch (Exception _error)
            {
                response = false;
                _log._logError(_error.Message, _error.StackTrace, "AddEvent", "EventTypeModel", _UserName);
            }
            return response;
        }
        public bool UpdateEventType(string _IDEventType, string _Title, string _Color, string _UserName)
        {
            bool response = false;
            try
            {
                using (var context = new ApplicationDbContext())
                {
                    using (SqlCommand cmd = new SqlCommand("SP_EventType_Update", context.Connection))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@IDEventType", _IDEventType);
                        cmd.Parameters.AddWithValue("@Title", _Title);
                        cmd.Parameters.AddWithValue("@Color", _Color);
                        cmd.Parameters.AddWithValue("@UserU", _UserName);
                        cmd.Parameters.AddWithValue("@DateU", DateTime.Today);
                        context.Connection.Open();
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                        }
                        response = true;
                    }
                }
                _log._logAction("Update Event", "Update Event, name:" + _Title, "UpdateEvent", "EventTypeModel", _UserName);
            }
            catch (Exception _error)
            {
                response = false;
                _log._logError(_error.Message, _error.StackTrace, "UpdateEvent", "EventTypeModel", _UserName);
            }
            return response;
        }
        public bool DeleteEventType(string _IDEventType, string _UserName)
        {
            bool response = false;
            try
            {
                using (var context = new ApplicationDbContext())
                {
                    using (SqlCommand cmd = new SqlCommand("SP_EventType_Delete", context.Connection))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@IDEventType", _IDEventType);
                        context.Connection.Open();
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                        }
                        response = true;
                    }
                }
                _log._logAction("Delete EventType", "Delete EventType, id:" + _IDEventType, "DeleteEvent", "EventTypeModel", _UserName);
            }
            catch (Exception _error)
            {
                response = false;
                _log._logError(_error.Message, _error.StackTrace, "DeleteEvent", "EventTypeModel", _UserName);
            }
            return response;
        }


    }
}