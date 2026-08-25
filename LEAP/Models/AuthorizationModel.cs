using LEAP.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace LEAP.Models
{
    public class AuthorizationModel
    {
        public int? IDAuth { get; set; }
        public string auth { get; set; }
        public string UCI { get; set; }
        public string ConsumerName { get; set; }
        public DateTime _From { get; set; }
        public DateTime _To { get; set; }
        public string _From_s { get; set; }
        public string _To_s { get; set; }
        public string UserC { get; set; }
        public DateTime? DateC { get; set; }
        public string UserU { get; set; }
        public DateTime? DateU { get; set; }
        public bool _ErrorCode { get; set; }
        public int type { get; set; }
        public int estado { get; set; }
        LogModel _log = new LogModel();
        public string regionalC { get; set; }
        public List<AuthorizationModel> Get_Authorizations()
        {
            var _Auth_Response = new List<AuthorizationModel>();
            try
            {
                using (var context = new ApplicationDbContext())
                {
                    using (SqlCommand cmd = new SqlCommand("SP_Authorization_AllData", context.Connection))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        context.Connection.Open();
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                _Auth_Response.Add(new AuthorizationModel
                                {
                                    IDAuth = Convert.ToInt32(reader["IDAuth"]),
                                    auth = reader["auth"].ToString(),
                                    //_From = Convert.ToDateTime(reader["_From"].ToString()),
                                    //_To = Convert.ToDateTime(reader["_To"].ToString()),
                                    _From_s = reader["_From"].ToString().Replace(" 00:00:00",""),
                                    _To_s = reader["_To"].ToString().Replace(" 00:00:00", ""),
                                    UCI = reader["UCI"].ToString(),
                                    ConsumerName = reader["ConsumerName"].ToString(),
                                    estado = Convert.ToInt32(reader["estado"]),
                                    regionalC = reader["regionalC"].ToString()

                                });
                            }
                        }
                    }
                }
            }
            catch (Exception _error)
            {
                _Auth_Response.Add(new AuthorizationModel { _ErrorCode = true });
            }
            return _Auth_Response;
        }
        public AuthorizationModel Get_Authorizations_ByID(int _auth)
        {
            var _Auth_Response = new AuthorizationModel();
            try
            {
                using (var context = new ApplicationDbContext())
                {
                    using (SqlCommand cmd = new SqlCommand("SP_Authorization_AllDataByID", context.Connection))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@ID", _auth);
                        context.Connection.Open();
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                _Auth_Response.IDAuth = Convert.ToInt32(reader["IDAuth"]);
                                _Auth_Response.auth = reader["auth"].ToString();
                                //_Auth_Response._From = reader["_From"].ToString().Replace(" 00:00:00", "");
                                _Auth_Response._From = Convert.ToDateTime(reader["_From"].ToString());
                                _Auth_Response._To = Convert.ToDateTime(reader["_To"].ToString());
                                _Auth_Response._From_s = reader["_From"].ToString().Replace(" 00:00:00", "");
                                _Auth_Response._To_s = reader["_To"].ToString().Replace(" 00:00:00", "");
                                _Auth_Response.UCI = reader["UCI"].ToString();
                                _Auth_Response.ConsumerName = reader["ConsumerName"].ToString();
                                _Auth_Response.estado = Convert.ToInt32(reader["estado"]);
                                //_Auth_Response._To = reader["_To"].ToString().Replace(" 00:00:00", "");
                                _Auth_Response._ErrorCode = false;
                            }
                        }
                    }
                }
            }
            catch (Exception _error)
            {
                _Auth_Response._ErrorCode = true;
            }
            return _Auth_Response;
        }
        public List<AuthorizationModel> Get_Authorizations_ByConsumer(int _auth)
        {
            var _Auth_Response = new List<AuthorizationModel>();
            try
            {
                using (var context = new ApplicationDbContext())
                {
                    using (SqlCommand cmd = new SqlCommand("SP_Authorization_AllDataByConsumer", context.Connection))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@ID", _auth);
                        context.Connection.Open();
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                _Auth_Response.Add(new AuthorizationModel
                                {
                                    IDAuth = Convert.ToInt32(reader["IDAuth"]),
                                auth = reader["auth"].ToString(),
                                //_Auth_Response._From = reader["_From"].ToString().Replace(" 00:00:00", "");
                                _From = Convert.ToDateTime(reader["_From"].ToString()),
                                _To = Convert.ToDateTime(reader["_To"].ToString()),
                                _From_s = reader["_From"].ToString().Replace(" 00:00:00", ""),
                                _To_s = reader["_To"].ToString().Replace(" 00:00:00", ""),
                                UCI = reader["UCI"].ToString(),
                                ConsumerName = reader["ConsumerName"].ToString(),
                                    estado = Convert.ToInt32(reader["estado"]),
                                    //_Auth_Response._To = reader["_To"].ToString().Replace(" 00:00:00", "");
                                    _ErrorCode = false
                            });
                            }
                        }
                    }
                }
            }
            catch (Exception _error)
            {
                _Auth_Response.Add(new AuthorizationModel { _ErrorCode = true });
            }
            return _Auth_Response;
        }
        //Agregar
        //public bool AddAuthorization(string _UCI, string _Authorization, DateTime? _From, DateTime? _To, string _UserName)
        public bool AddAuthorization(string _UCI, string _Authorization, DateTime? _From, DateTime? _To, int estado, string _UserName)
        {
            bool response = false;
            if (_Authorization==null)
            {
                _Authorization = "";
            }
            
            try
            {
                using (var context = new ApplicationDbContext())
                {
                    using (SqlCommand cmd = new SqlCommand("SP_Authorization_Create", context.Connection))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@UCI", _UCI);
                        cmd.Parameters.AddWithValue("@Auth", _Authorization);
                        cmd.Parameters.AddWithValue("@From", _From);
                        cmd.Parameters.AddWithValue("@To", _To);
                        cmd.Parameters.AddWithValue("@estado", estado);
                        cmd.Parameters.AddWithValue("@UserC", _UserName);
                        cmd.Parameters.AddWithValue("@DateC", DateTime.Today);
                        context.Connection.Open();
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                        }
                        response = true;
                    }
                }
                _log._logAction("Create Authorization", "Create a new Authorization, name:" + _Authorization, "AddAuthorization", "AuthorizationModel", _UserName);
            }
            catch (Exception _error)
            {
                response = false;
                _log._logError(_error.Message, _error.StackTrace, "AddAuthorization", "AuthorizationModel", _UserName);
            }
            return response;
        }
       //public bool UpdateAuthorization(string _IDAuthorization, string _UCI, string _Auth, DateTime? _From, DateTime? _To, string _UserName)
            public bool UpdateAuthorization(string _IDAuthorization, string _UCI, string _Auth, DateTime? _From, DateTime? _To, int estado, string _UserName)
        {
            bool response = false;
            try
            {
                using (var context = new ApplicationDbContext())
                {
                    using (SqlCommand cmd = new SqlCommand("SP_Authorization_Update", context.Connection))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@IDAuthorization", _IDAuthorization);
                        cmd.Parameters.AddWithValue("@UCI", _UCI);
                        cmd.Parameters.AddWithValue("@Auth", _Auth);
                        cmd.Parameters.AddWithValue("@From", _From);
                        cmd.Parameters.AddWithValue("@To", _To);
                        cmd.Parameters.AddWithValue("@estado", estado);
                        cmd.Parameters.AddWithValue("@UserU", _UserName);
                        cmd.Parameters.AddWithValue("@DateU", DateTime.Today);
                        context.Connection.Open();
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                        }
                        response = true;
                    }
                }
                _log._logAction("Update Authorization", "Update Authorization, name:" + _Auth, "UpdateAuthorization", "AuthorizationModel", _UserName);
            }
            catch (Exception _error)
            {
                response = false;
                _log._logError(_error.Message, _error.StackTrace, "UpdateAuthorization", "AuthorizationModel", _UserName);
            }
            return response;
        }
        public bool DeleteAuthorization(string _IDAuthorization, string _UserName)
        {
            bool response = false;
            try
            {
                using (var context = new ApplicationDbContext())
                {
                    using (SqlCommand cmd = new SqlCommand("SP_Authorization_Delete", context.Connection))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@IDAuthorization", _IDAuthorization);
                        context.Connection.Open();
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                        }
                        response = true;
                    }
                }
                _log._logAction("Delete Authorization", "Delete Authorizations, id:" + _IDAuthorization, "DeleteAuthorization", "AuthorizationModel", _UserName);
            }
            catch (Exception _error)
            {
                response = false;
                _log._logError(_error.Message, _error.StackTrace, "DeleteAuthorization", "AuthorizationModel", _UserName);
            }
            return response;
        }
    }
}