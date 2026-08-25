using LEAP.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace LEAP.Models
{
    public class RegionalCenterModel
    {
        public int IDRegionalCenter { get; set; }
        public string RegionalCenter { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string ZipCode { get; set; }
        public string Ext { get; set; }
        public string UserC { get; set; }
        public DateTime? DateC { get; set; }
        public string UserU { get; set; }
        public DateTime? DateU { get; set; }
        public string serviceC { get; set; }
        public string telephone { get; set; }
        public bool _ErrorCode { get; set; }
        public int IDServiceC { get; set; }

        //METODOS
        LogModel _log = new LogModel();
        public List<RegionalCenterModel> Get_RegionalCenter()
        {
            var _RCenter_Response = new List<RegionalCenterModel>();
            try
            {
                using (var context = new ApplicationDbContext())
                {
                    using (SqlCommand cmd = new SqlCommand("Get_RegionalCenter_allData", context.Connection))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        context.Connection.Open();
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                _RCenter_Response.Add(new RegionalCenterModel
                                {
                                    IDRegionalCenter = Convert.ToInt32(reader["IDRegionalCenter"]),
                                    RegionalCenter = reader["RegionalCenter"].ToString(),
                                    Phone = reader["Phone"].ToString(),
                                    Address = reader["Address"].ToString(),
                                    City = reader["City"].ToString(),
                                    State = reader["State"].ToString(),
                                    ZipCode = reader["ZipCode"].ToString(),
                                    Ext = reader["Ext"].ToString(),
                                    //serviceC = reader["NameC"].ToString(),
                                    //telephone = reader["tel"].ToString(),
                                    _ErrorCode = false
                                });
                            }
                        }
                    }
                }
            }
            catch (Exception _error)
            {
                _RCenter_Response.Add(new RegionalCenterModel { _ErrorCode = true });
            }
            return _RCenter_Response;
        }
        public List<RegionalCenterModel> Get_RegionalByID(int _IDRegionalCenter)
        {
            var productos = new List<RegionalCenterModel>();
            try
            {
                using (var context = new ApplicationDbContext())
                {
                    using (SqlCommand cmd = new SqlCommand("Get_ConsumerByID", context.Connection))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@IDRegionalCenter", _IDRegionalCenter);
                        context.Connection.Open();
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                productos.Add(new RegionalCenterModel
                                {
                                    IDRegionalCenter = Convert.ToInt32(reader["IDRegionalCenter"]),
                                    RegionalCenter = reader["RegionalCenter"].ToString(),
                                    Phone = reader["Phone"].ToString(),
                                    Address = reader["Address"].ToString(),
                                    City = reader["City"].ToString(),
                                    State = reader["State"].ToString(),
                                    ZipCode = reader["ZipCode"].ToString(),
                                    Ext = reader["Ext"].ToString(),
                                   serviceC = reader["NameC"].ToString(),
                                    telephone = reader["tel"].ToString(),
                                     IDServiceC = Convert.ToInt32(reader["IDServiceC"])
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

        public RegionalCenterModel Get_RegionalCenter_ByID(int _RCenterID)
        {
            var _RCenter_Response = new RegionalCenterModel();
            try
            {
                using (var context = new ApplicationDbContext())
                {
                    using (SqlCommand cmd = new SqlCommand("SP_RegionalCenter_allDataByID", context.Connection))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@ID", _RCenterID);
                        context.Connection.Open();
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                _RCenter_Response.IDRegionalCenter = Convert.ToInt32(reader["IDRegionalCenter"]);
                                _RCenter_Response.RegionalCenter = reader["RegionalCenter"].ToString();
                                _RCenter_Response.Phone = reader["Phone"].ToString();
                                _RCenter_Response.Address = reader["Address"].ToString();
                                _RCenter_Response.City = reader["City"].ToString();
                                _RCenter_Response.State = reader["State"].ToString();
                                _RCenter_Response.ZipCode = reader["ZipCode"].ToString();
                                _RCenter_Response.Ext = reader["Ext"].ToString();
                                _RCenter_Response._ErrorCode = false;
                            }
                        }
                    }
                }
            }
            catch (Exception _error)
            {
                _RCenter_Response._ErrorCode = true;
            }
            return _RCenter_Response;
        }

        //Agregar
        public bool AddRegionalCenter(string _RegionalCenter, string _Phone, string _Address, string _City,string _State,string _ZipCode,string _Ext, string _UserName)
        {
            bool response = false;
            try
            {
                using (var context = new ApplicationDbContext())
                {
                    using (SqlCommand cmd = new SqlCommand("SP_RegionalCenter_Create", context.Connection))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@RegionalCenter", _RegionalCenter);
                        cmd.Parameters.AddWithValue("@Phone", _Phone);
                        cmd.Parameters.AddWithValue("@Address", _Address);
//                        cmd.Parameters.AddWithValue("@Address", _notesdate.HasValue ? (object)_notesdate.Value : DBNull.Value);
                        cmd.Parameters.AddWithValue("@City", _City);
                        cmd.Parameters.AddWithValue("@State", _State);
                        cmd.Parameters.AddWithValue("@ZipCode", _ZipCode);
                        cmd.Parameters.AddWithValue("@Ext", _Ext);
                        cmd.Parameters.AddWithValue("@UserC", _UserName);
                        cmd.Parameters.AddWithValue("@DateC", DateTime.Today);
                        context.Connection.Open();
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                        }
                        response = true;
                    }
                }
                _log._logAction("Create RegionalCenter", "Create a new Regional Center, name:" + _RegionalCenter, "AddRegionalCenter", "RegionalCenterModel", _UserName);
            }
            catch (Exception _error)
            {
                response = false;
                _log._logError(_error.Message, _error.StackTrace, "AddRegionalCenter", "RegionalCenterModel", _UserName);
            }
            return response;
        }

        public bool UpdateRegionalCenter(string _IDRegionalCenter, string _RegionalCenter, string _Phone, string _Address, string _City, string _State, string _ZipCode, string _Ext, string _UserName)
        {
            bool response = false;
            try
            {
                using (var context = new ApplicationDbContext())
                {
                    using (SqlCommand cmd = new SqlCommand("SP_RegionalCenter_Update", context.Connection))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@IDRegionalCenter", _IDRegionalCenter);
                        cmd.Parameters.AddWithValue("@RegionalCenter", _RegionalCenter);
                        cmd.Parameters.AddWithValue("@Phone", _Phone);
                        cmd.Parameters.AddWithValue("@Address", _Address);
                        cmd.Parameters.AddWithValue("@City", _City);
                        cmd.Parameters.AddWithValue("@State", _State);
                        cmd.Parameters.AddWithValue("@ZipCode", _ZipCode);
                        cmd.Parameters.AddWithValue("@Ext", _Ext);
                        cmd.Parameters.AddWithValue("@UserU", _UserName);
                        cmd.Parameters.AddWithValue("@DateU", DateTime.Today);
                        context.Connection.Open();
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                        }
                        response = true;
                    }
                }
                _log._logAction("Update RegionalCenter", "Update Regional Center, name:" + _RegionalCenter, "UpdateRegionalCenter", "RegionalCenterModel", _UserName);
            }
            catch (Exception _error)
            {
                response = false;
                _log._logError(_error.Message, _error.StackTrace, "UpdateRegionalCenter", "RegionalCenterModel", _UserName);
            }
            return response;
        }
        public bool DeleteRegionalCenter(string _IDRegionalCenter, string _UserName)
        {
            bool response = false;
            try
            {
                using (var context = new ApplicationDbContext())
                {
                    using (SqlCommand cmd = new SqlCommand("SP_RegionalCenter_Delete", context.Connection))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@IDRegionalCenter", _IDRegionalCenter);
                        context.Connection.Open();
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                        }
                        response = true;
                    }
                }
                _log._logAction("Delete RegionalCenter", "Delete Regional Center, id:" + IDRegionalCenter, "DeleteRegionalCenter", "RegionalCenterModel", _UserName);
            }
            catch (Exception _error)
            {
                response = false;
                _log._logError(_error.Message, _error.StackTrace, "DeleteRegionalCenter", "RegionalCenterModel", _UserName);
            }
            return response;
        }

        //End=======>>>>>>> Stashed changes
    }
}