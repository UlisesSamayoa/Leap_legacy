using LEAP.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace LEAP.Models
{
    public class CitiesModel
    {
        public int? IDCity { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string UserC { get; set; }
        public DateTime? DateC { get; set; }
        public string UserU { get; set; }
        public DateTime? DateU { get; set; }
        public bool _ErrorCode { get; set; }
        LogModel _log = new LogModel();
        public List<CitiesModel> Get_Cities()
        {
            var _Cities_Response = new List<CitiesModel>();
            try
            {
                using (var context = new ApplicationDbContext())
                {
                    using (SqlCommand cmd = new SqlCommand("SP_Cities_AllData", context.Connection))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        context.Connection.Open();
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                _Cities_Response.Add(new CitiesModel
                                {
                                    IDCity = Convert.ToInt32(reader["IDCity"]),
                                    City = reader["City"].ToString(),
                                    State = reader["State"].ToString(),
                                });
                            }
                        }
                    }
                }
            }
            catch (Exception _error)
            {
                _Cities_Response.Add(new CitiesModel { _ErrorCode = true });
            }
            return _Cities_Response;
        }
        public CitiesModel Get_Cities_ByID(int _city)
        {
            var _Cities_Response = new CitiesModel();
            try
            {
                using (var context = new ApplicationDbContext())
                {
                    using (SqlCommand cmd = new SqlCommand("SP_Cities_AllDataByID", context.Connection))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@ID", _city);
                        context.Connection.Open();
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                _Cities_Response.IDCity = Convert.ToInt32(reader["IDCity"]);
                                _Cities_Response.City = reader["City"].ToString();
                                _Cities_Response.State = reader["State"].ToString();
                                _Cities_Response._ErrorCode = false;
                            }
                        }
                    }
                }
            }
            catch (Exception _error)
            {
                _Cities_Response._ErrorCode = true;
            }
            return _Cities_Response;
        }
        public bool AddCity(string _City, string _State, string _UserName)
        {
            bool response = false;
            try
            {
                using (var context = new ApplicationDbContext())
                {
                    using (SqlCommand cmd = new SqlCommand("SP_Cities_Create", context.Connection))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@City", _City);
                        cmd.Parameters.AddWithValue("@State", _State);
                        cmd.Parameters.AddWithValue("@UserC", _UserName);
                        cmd.Parameters.AddWithValue("@DateC", DateTime.Today);
                        context.Connection.Open();
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                        }
                        response = true;
                    }
                }
                _log._logAction("Create City", "Create a new City, name:" + _City, "AddCity", "CityModel", _UserName);
            }
            catch (Exception _error)
            {
                response = false;
                _log._logError(_error.Message, _error.StackTrace, "AddCity", "CityModel", _UserName);
            }
            return response;
        }
        public bool UpdateCity(string _IDCity, string _City, string _State, string _UserName)
        {
            bool response = false;
            try
            {
                using (var context = new ApplicationDbContext())
                {
                    using (SqlCommand cmd = new SqlCommand("SP_Cities_Update", context.Connection))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@IDCity", _IDCity);
                        cmd.Parameters.AddWithValue("@City", _City);
                        cmd.Parameters.AddWithValue("@State", _State);
                        cmd.Parameters.AddWithValue("@UserU", _UserName);
                        cmd.Parameters.AddWithValue("@DateU", DateTime.Today);
                        context.Connection.Open();
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                        }
                        response = true;
                    }
                }
                _log._logAction("Update City", "Update City, name:" + _City, "UpdateCity", "CityModel", _UserName);
            }
            catch (Exception _error)
            {
                response = false;
                _log._logError(_error.Message, _error.StackTrace, "UpdateCity", "CityModel", _UserName);
            }
            return response;
        }
        public bool DeleteCity(string _IDCity, string _UserName)
        {
            bool response = false;
            try
            {
                using (var context = new ApplicationDbContext())
                {
                    using (SqlCommand cmd = new SqlCommand("SP_Cities_Delete", context.Connection))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@IDCity", _IDCity);
                        context.Connection.Open();
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                        }
                        response = true;
                    }
                }
                _log._logAction("Delete City", "Delete City, id:" + _IDCity, "DeleteCity", "CityModel", _UserName);
            }
            catch (Exception _error)
            {
                response = false;
                _log._logError(_error.Message, _error.StackTrace, "DeleteCity", "CityModel", _UserName);
            }
            return response;
        }



    }
}