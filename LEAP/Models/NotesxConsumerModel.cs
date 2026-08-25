using LEAP.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
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
                using (var context = new ApplicationDbContext())
                {
                    using (SqlCommand cmd = new SqlCommand("SP_NotesxConsumer_AllData", context.Connection))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        context.Connection.Open();
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                _NotesxConsumer_Response.Add(new NotesxConsumerModel
                                {
                                    IDNotesxConsumer = Convert.IsDBNull(reader["IDNotesxConsumer"]) ? 0 : (int)reader["IDNotesxConsumer"] ,
                                    UCI = Convert.IsDBNull(reader["UCI"]) ? "" : (string)reader["UCI"],
                                    ConsumerName = Convert.IsDBNull(reader["ConsumerName"]) ? "" : (string)reader["ConsumerName"],
                                    DateC = reader["DateC"] == DBNull.Value? DateTime.MinValue : Convert.ToDateTime(reader["DateC"]),
                                    Notes = Convert.IsDBNull(reader["Notes"]) ? "" : (string)reader["Notes"],
                                    Active = reader["Active"] == DBNull.Value ? false : (bool)reader["Active"]
                                });
                            }
                        }
                    }
                }
            }
            catch (Exception _error)
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
                using (var context = new ApplicationDbContext())
                {
                    using (SqlCommand cmd = new SqlCommand("SP_NotesxConsumer_AllDataByID", context.Connection))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@ID", _NotesxConsumer);
                        context.Connection.Open();
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                _NotesxConsumer_Response.IDNotesxConsumer = Convert.ToInt32(reader["IDNotesxConsumer"]);
                                _NotesxConsumer_Response.UCI = reader["UCI"].ToString();
                                _NotesxConsumer_Response.ConsumerName = reader["ConsumerName"].ToString();
                                _NotesxConsumer_Response.Notes = reader["Notes"].ToString();
                                _NotesxConsumer_Response.Active = Convert.ToBoolean(reader["Active"]);
                                _NotesxConsumer_Response._ErrorCode = false;
                            }
                        }
                    }
                }
            }
            catch (Exception _error)
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
                using (var context = new ApplicationDbContext())
                {
                    using (SqlCommand cmd = new SqlCommand("SP_NotesxConsumer_AllDataByID_Consumer", context.Connection))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@ID", _NotesxConsumer);
                        context.Connection.Open();
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                _NotesxConsumer_Response.Add(new NotesxConsumerModel
                                {
                                    IDNotesxConsumer = Convert.ToInt32(reader["IDNotesxConsumer"]),
                                    UCI = reader["UCI"].ToString(),
                                    ConsumerName = reader["ConsumerName"].ToString(),
                                    Notes = reader["Notes"].ToString(),
                                    DateC = Convert.ToDateTime(reader["DateC"]),
                                    Active = Convert.ToBoolean(reader["Active"].ToString())
                                });
                            }
                        }
                        context.Connection.Close();
                    }

                }
            }
            catch (Exception _error)
            {
                _NotesxConsumer_Response.Add(new NotesxConsumerModel { _ErrorCode = true });
            }
            return _NotesxConsumer_Response;
        }
        //Agregar
        public bool AddNotesxConsumer(string _UCI,string _Notes, bool _Active, string _UserName)
        {
            bool response = false;
            try
            {
                using (var context = new ApplicationDbContext())
                {
                    using (SqlCommand cmd = new SqlCommand("SP_NotesxConsumer_Create", context.Connection))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@UCI", _UCI);
                        cmd.Parameters.AddWithValue("@Notes", _Notes);
                        cmd.Parameters.AddWithValue("@Active", _Active);
                        cmd.Parameters.AddWithValue("@UserC", _UserName);
                        cmd.Parameters.AddWithValue("@DateC", DateTime.Today);
                        context.Connection.Open();
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                        }
                        response = true;
                    }
                }
                _log._logAction("Create NotesxConsumer", "Create a new NotesxConsumer, UCI:" + _UCI, "AddNotesxConsumer", "NotesxConsumerModel", _UserName);
            }
            catch (Exception _error)
            {
                response = false;
                _log._logError(_error.Message, _error.StackTrace, "AddNotesxConsumer", "NotesxConsumerModel", _UserName);
            }
            return response;
        }
        public bool ValidarNotesConsumerActive(int? _ID,string _UCI,bool _Active, string _UserName)
        {
            bool response = false;

            try
            {
                using (var context = new ApplicationDbContext())
                {
                    using (SqlCommand cmd = new SqlCommand("SP_NotesxConsumer_ValidateNoteActive", context.Connection))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@UCI", _UCI);
                        cmd.Parameters.AddWithValue("@Active", _Active);
                        context.Connection.Open();
                        int ID = 0;
                        string UCI = "";
                        bool Active = false;
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                while (reader.Read())
                                {
                                   ID = Convert.ToInt32(reader["IDNotesxConsumer"].ToString());
                                   UCI = reader["UCI"].ToString();
                                    Active = Convert.ToBoolean(reader["Active"].ToString());   
                                }
                                if (_ID > 0 )
                                {
                                    if (UCI == _UCI && Active == _Active && true == _Active && _ID != ID)
                                    {
                                        response = true;
                                    }
                                    else
                                    {
                                        response = false;
                                    }
                                }
                                else
                                {
                                    if (UCI == _UCI && Active == _Active && true == _Active)
                                    {
                                        response = true;
                                    }
                                    else
                                    {
                                        response = false;
                                    }
                                }
                               
                            }
                            else
                            {
                                response = false;
                            }
                        }
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
                using (var context = new ApplicationDbContext())
                {
                    using (SqlCommand cmd = new SqlCommand("SP_NotesxConsumer_Update", context.Connection))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@IDNotesxConsumer", _IDNotesxConsumer);
                        cmd.Parameters.AddWithValue("@UCI", _UCI);
                        cmd.Parameters.AddWithValue("@Notes", _Notes);
                        cmd.Parameters.AddWithValue("@Active", _Active);
                        cmd.Parameters.AddWithValue("@UserU", _UserName);
                        cmd.Parameters.AddWithValue("@DateU", DateTime.Today);
                        context.Connection.Open();
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                        }
                        response = true;
                    }
                }
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
                using (var context = new ApplicationDbContext())
                {
                    using (SqlCommand cmd = new SqlCommand("SP_NotesxConsumer_Delete", context.Connection))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@IDNotesxConsumer", _IDNotesxConsumer);
                        context.Connection.Open();
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                        }
                        response = true;
                    }
                }
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