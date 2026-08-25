using LEAP.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace LEAP.Models
{
    public class LanguajesModel
    {
        public int? IDLanguaje { get; set; }
        public string Languaje { get; set; }
        public string Notes { get; set; }
        public string UserC { get; set; }
        public DateTime? DateC { get; set; }
        public string UserU { get; set; }
        public DateTime? DateU { get; set; }
        public bool _ErrorCode { get; set; }
        LogModel _log = new LogModel();
        public List<LanguajesModel> Get_Languajes()
        {
            var _Languajes_Response = new List<LanguajesModel>();
            try
            {
                using (var context = new ApplicationDbContext())
                {
                    using (SqlCommand cmd = new SqlCommand("SP_Languajes_AllData", context.Connection))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        context.Connection.Open();
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                _Languajes_Response.Add(new LanguajesModel
                                {
                                    IDLanguaje = Convert.ToInt32(reader["IDLanguaje"]),
                                    Languaje = reader["Languaje"].ToString(),
                                    Notes = reader["Notes"].ToString(),
                                });
                            }
                        }
                    }
                }
            }
            catch (Exception _error)
            {
                _Languajes_Response.Add(new LanguajesModel { _ErrorCode = true });
            }
            return _Languajes_Response;
        }
        public LanguajesModel Get_Languajes_ByID(int _Languaje)
        {
            var _Languajes_Response = new LanguajesModel();
            try
            {
                using (var context = new ApplicationDbContext())
                {
                    using (SqlCommand cmd = new SqlCommand("SP_Languajes_AllDataByID", context.Connection))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@ID", _Languaje);
                        context.Connection.Open();
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                _Languajes_Response.IDLanguaje = Convert.ToInt32(reader["IDLanguaje"]);
                                _Languajes_Response.Languaje = reader["Languaje"].ToString();
                                _Languajes_Response.Notes = reader["Notes"].ToString();
                                _Languajes_Response._ErrorCode = false;
                            }
                        }
                    }
                }
            }
            catch (Exception _error)
            {
                _Languajes_Response._ErrorCode = true;
            }
            return _Languajes_Response;
        }

        //Agregar
        public bool AddLanguaje(string _Languaje, string _Notes, string _UserName)
        {
            bool response = false;
            try
            {
                using (var context = new ApplicationDbContext())
                {
                    using (SqlCommand cmd = new SqlCommand("SP_Languajes_Create", context.Connection))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@Languaje", _Languaje);
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
                _log._logAction("Create Languaje", "Create a new Languaje, name:" + _Languaje, "AddLanguaje", "LanguajesModel", _UserName);
            }
            catch (Exception _error)
            {
                response = false;
                _log._logError(_error.Message, _error.StackTrace, "AddLanguaje", "LanguajesModel", _UserName);
            }
            return response;
        }
        public bool UpdateLanguaje(string _IDLanguaje, string _Languaje, string _Notes, string _UserName)
        {
            bool response = false;
            try
            {
                using (var context = new ApplicationDbContext())
                {
                    using (SqlCommand cmd = new SqlCommand("SP_Languajes_Update", context.Connection))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@IDLanguaje", _IDLanguaje);
                        cmd.Parameters.AddWithValue("@Languaje", _Languaje);
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
                _log._logAction("Update Languaje", "Update Languaje, name:" + _Languaje, "UpdateLanguaje", "LanguajesModel", _UserName);
            }
            catch (Exception _error)
            {
                response = false;
                _log._logError(_error.Message, _error.StackTrace, "UpdateLanguaje", "LanguajesModel", _UserName);
            }
            return response;
        }
        public bool DeleteLanguaje(string _IDLanguaje, string _UserName)
        {
            bool response = false;
            try
            {
                using (var context = new ApplicationDbContext())
                {
                    using (SqlCommand cmd = new SqlCommand("SP_Languajes_Delete", context.Connection))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@IDLanguaje", _IDLanguaje);
                        context.Connection.Open();
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                        }
                        response = true;
                    }
                }
                _log._logAction("Delete Languajes", "Delete Languajes, id:" + _IDLanguaje, "DeleteLanguaje", "LanguajesModel", _UserName);
            }
            catch (Exception _error)
            {
                response = false;
                _log._logError(_error.Message, _error.StackTrace, "DeleteLanguaje", "LanguajesModel", _UserName);
            }
            return response;
        }



    }
}