using LEAP.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace LEAP.Models
{
    public class EventsModel
    {
        public int? IDEvent { get; set; }
        public string Title { get; set; }
        public DateTime DateEvent { get; set; }
        public string Description { get; set; }
        public int IDType { get; set; }
        public string TypeName { get; set; }
        public string Color { get; set; }
        public string UserC { get; set; }
        public DateTime? DateC { get; set; }
        public string UserU { get; set; }
        public DateTime? DateU { get; set; }
        public bool _ErrorCode { get; set; }

        LogModel _log = new LogModel();
        public List<EventsModel> Get_Events()
        {
            var _RCenter_Response = new List<EventsModel>();
            try
            {
                using (var context = new ApplicationDbContext())
                {
                    using (SqlCommand cmd = new SqlCommand("SP_Events_allData", context.Connection))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        context.Connection.Open();
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                _RCenter_Response.Add(new EventsModel
                                {
                                    IDEvent = Convert.ToInt32(reader["IDEvent"]),
                                    Title = reader["Title"].ToString(),
                                    DateEvent = Convert.ToDateTime(reader["DateEvent"].ToString()),
                                    Description = reader["Description"].ToString(),
                                    IDType = Convert.ToInt32(reader["IDType"].ToString()),
                                    TypeName = reader["TypeName"].ToString(),
                                    Color = reader["Color"].ToString(),
                                    _ErrorCode = false
                                });
                            }
                        }
                    }
                }
            }
            catch (Exception _error)
            {
                _RCenter_Response.Add(new EventsModel { _ErrorCode = true });
            }
            return _RCenter_Response;
        }
        public EventsModel Get_Events_ByID(int _Events)
        {
            var _Events_Response = new EventsModel();
            try
            {
                using (var context = new ApplicationDbContext())
                {
                    using (SqlCommand cmd = new SqlCommand("SP_Events_AllDataByID", context.Connection))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@ID", _Events);
                        context.Connection.Open();
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                _Events_Response.IDEvent = Convert.ToInt32(reader["IDEvent"]);
                                _Events_Response.Title = reader["Title"].ToString();
                                _Events_Response.DateEvent = Convert.ToDateTime(reader["DateEvent"].ToString());
                                _Events_Response.Description = reader["Description"].ToString();
                                _Events_Response.IDType = Convert.ToInt32(reader["IDType"]);
                                _Events_Response.TypeName = reader["TypeName"].ToString();
                                _Events_Response.Color = reader["Color"].ToString();
                                _Events_Response._ErrorCode = false;
                            }
                        }
                    }
                }
            }
            catch (Exception _error)
            {
                _Events_Response._ErrorCode = true;
            }
            return _Events_Response;
        }
        public EventsModel Get_Events_ByEventType(string _EventType)
        {
            var _Events_Response = new EventsModel();
            try
            {
                using (var context = new ApplicationDbContext())
                {
                    using (SqlCommand cmd = new SqlCommand("SP_Events_AllDataByEventType", context.Connection))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@ID", _EventType);
                        context.Connection.Open();
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                _Events_Response.IDEvent = Convert.ToInt32(reader["IDEvent"]);
                                _Events_Response.Title = reader["Title"].ToString();
                                _Events_Response.DateEvent = Convert.ToDateTime(reader["DateEvent"].ToString());
                                _Events_Response.Description = reader["Description"].ToString();
                                _Events_Response.IDType = Convert.ToInt32(reader["IDType"]);
                                _Events_Response.TypeName = reader["TypeName"].ToString();
                                _Events_Response.Color = reader["Color"].ToString();
                                _Events_Response._ErrorCode = false;
                            }
                        }
                    }
                }
            }
            catch (Exception _error)
            {
                _Events_Response._ErrorCode = true;
            }
            return _Events_Response;
        }

        public bool AddEvent(string _Title,DateTime _DateEvent,string _Description,int _Type, string _UserName)
        {
            bool response = false;
            try
            {
                using (var context = new ApplicationDbContext())
                {
                    using (SqlCommand cmd = new SqlCommand("SP_Events_Create", context.Connection))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@Title", _Title);
                        cmd.Parameters.AddWithValue("@DateEvent", _DateEvent);
                        cmd.Parameters.AddWithValue("@Description", _Description);
                        cmd.Parameters.AddWithValue("@IDType", _Type);
                        cmd.Parameters.AddWithValue("@UserC", _UserName);
                        cmd.Parameters.AddWithValue("@DateC", DateTime.Today);
                        context.Connection.Open();
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                        }
                        response = true;
                    }
                }
                _log._logAction("Create Event", "Create a new Event, name:" + _Title, "AddEvent", "EventsModel", _UserName);
            }
            catch (Exception _error)
            {
                response = false;
                _log._logError(_error.Message, _error.StackTrace, "AddEvent", "EventsModel", _UserName);
            }
            return response;
        }
        public bool UpdateEvent(string _IDEvent, string _Title, DateTime _DateEvent, string _Description, int _Type, string _UserName)
        {
            bool response = false;
            try
            {
                using (var context = new ApplicationDbContext())
                {
                    using (SqlCommand cmd = new SqlCommand("SP_Events_Update", context.Connection))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@IDEvent", _IDEvent);
                        cmd.Parameters.AddWithValue("@Title", _Title);
                        cmd.Parameters.AddWithValue("@DateEvent", _DateEvent);
                        cmd.Parameters.AddWithValue("@Description", _Description);
                        cmd.Parameters.AddWithValue("@IDType", _Type);
                        cmd.Parameters.AddWithValue("@UserU", _UserName);
                        cmd.Parameters.AddWithValue("@DateU", DateTime.Today);
                        context.Connection.Open();
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                        }
                        response = true;
                    }
                }
                _log._logAction("Update Event", "Update Event, name:" + _Title, "UpdateEvent", "EventsModel", _UserName);
            }
            catch (Exception _error)
            {
                response = false;
                _log._logError(_error.Message, _error.StackTrace, "UpdateEvent", "EventsModel", _UserName);
            }
            return response;
        }
        public bool DeleteEvent(string _IDEvent, string _UserName)
        {
            bool response = false;
            try
            {
                using (var context = new ApplicationDbContext())
                {
                    using (SqlCommand cmd = new SqlCommand("SP_Events_Delete", context.Connection))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@IDEvent", _IDEvent);
                        context.Connection.Open();
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                        }
                        response = true;
                    }
                }
                _log._logAction("Delete Events", "Delete Events, id:" + _IDEvent, "DeleteEvent", "EventsModel", _UserName);
            }
            catch (Exception _error)
            {
                response = false;
                _log._logError(_error.Message, _error.StackTrace, "DeleteEvent", "EventsModel", _UserName);
            }
            return response;
        }
        public bool DeleteEventxEvenType(string _EventType, string _UserName)
        {
            bool response = false;
            try
            {
                using (var context = new ApplicationDbContext())
                {
                    using (SqlCommand cmd = new SqlCommand("SP_Events_Delete_EventType", context.Connection))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@IDEventType", _EventType);
                        context.Connection.Open();
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                        }
                        response = true;
                    }
                }
                _log._logAction("Delete Events", "Delete Events, id:" + _EventType, "DeleteEvent", "EventsModel", _UserName);
            }
            catch (Exception _error)
            {
                response = false;
                _log._logError(_error.Message, _error.StackTrace, "DeleteEvent", "EventsModel", _UserName);
            }
            return response;
        }


    }
}