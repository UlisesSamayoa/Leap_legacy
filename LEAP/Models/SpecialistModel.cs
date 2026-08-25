using LEAP.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace LEAP.Models
{
    public class SpecialistModel
    {
        public int? IDSpecialist { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string ZipCode { get; set; }
        public string HourlyRate { get; set; }
        public string Specialty { get; set; }
        public string UserC { get; set; }
        public DateTime? DateC { get; set; }
        public string UserU { get; set; }
        public DateTime? DateU { get; set; }
        public bool _ErrorCode { get; set; }

        //METODOS
        LogModel _log = new LogModel();
        public List<SpecialistModel> Get_Specialist()
        {
            var _RCenter_Response = new List<SpecialistModel>();
            try
            {
                using (var context = new ApplicationDbContext())
                {
                    using (SqlCommand cmd = new SqlCommand("SP_Specialist_allData", context.Connection))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        context.Connection.Open();
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                _RCenter_Response.Add(new SpecialistModel
                                {
                                    IDSpecialist = Convert.ToInt32(reader["IDSpecialist"]),
                                    Name = reader["Name"].ToString(),
                                    LastName = reader["LastName"].ToString(),
                                    Phone = reader["Phone"].ToString(),
                                    Email = reader["Email"].ToString(),
                                    Address = reader["Address"].ToString(),
                                    City = reader["City"].ToString(),
                                    State = reader["State"].ToString(),
                                    ZipCode = reader["ZipCode"].ToString(),
                                    HourlyRate = reader["HourlyRate"].ToString(),
                                    Specialty = reader["Specialty"].ToString(),
                                    _ErrorCode = false
                                });
                            }
                        }
                    }
                }
            }
            catch (Exception _error)
            {
                _RCenter_Response.Add(new SpecialistModel { _ErrorCode = true });
            }
            return _RCenter_Response;
        }
        public SpecialistModel Get_Specialist_ByID(int _Specialist)
        {
            var _specialist_Response = new SpecialistModel();
            try
            {
                using (var context = new ApplicationDbContext())
                {
                    using (SqlCommand cmd = new SqlCommand("SP_Specialist_AllDataByID", context.Connection))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@ID", _Specialist);
                        context.Connection.Open();
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                _specialist_Response.IDSpecialist = Convert.ToInt32(reader["IDSpecialist"]);
                                _specialist_Response.Name = reader["Name"].ToString();
                                _specialist_Response.LastName = reader["LastName"].ToString();
                                _specialist_Response.Phone = reader["Phone"].ToString();
                                _specialist_Response.Email = reader["Email"].ToString();
                                _specialist_Response.Address = reader["Address"].ToString();
                                _specialist_Response.City = reader["City"].ToString();
                                _specialist_Response.State = reader["State"].ToString();
                                _specialist_Response.ZipCode = reader["ZipCode"].ToString();
                                _specialist_Response.HourlyRate = reader["HourlyRate"].ToString();
                                _specialist_Response.Specialty = reader["Specialty"].ToString();
                                _specialist_Response._ErrorCode = false;
                            }
                        }
                    }
                }
            }
            catch (Exception _error)
            {
                _specialist_Response._ErrorCode = true;
            }
            return _specialist_Response;
        }

        //Agregar
        public bool AddSpecialist(string _Name, string _LastName, string _Phone, string _Email, string _Address, string _City, string _State, string _ZipCode, string _HourlyRate, string Specialty, string _UserName)
        {
            bool response = false;
            try
            {
                using (var context = new ApplicationDbContext())
                {
                    using (SqlCommand cmd = new SqlCommand("SP_Specialist_Create", context.Connection))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@Name", _Name);
                        cmd.Parameters.AddWithValue("@LastName", _LastName);
                        cmd.Parameters.AddWithValue("@Phone", _Phone);
                        cmd.Parameters.AddWithValue("@Email", _Email);
                        cmd.Parameters.AddWithValue("@Address", _Address);
                        cmd.Parameters.AddWithValue("@City", _City);
                        cmd.Parameters.AddWithValue("@State", _State);
                        cmd.Parameters.AddWithValue("@ZipCode", _ZipCode);
                        cmd.Parameters.AddWithValue("@HourlyRate", _HourlyRate);
                        cmd.Parameters.AddWithValue("@Specialty", Specialty);
                        cmd.Parameters.AddWithValue("@UserC", _UserName);
                        cmd.Parameters.AddWithValue("@DateC", DateTime.Today);
                        context.Connection.Open();
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                        }
                        response = true;
                    }
                }
                _log._logAction("Create Specialist", "Create a new Specialist, name:" + _Name + " " + _LastName, "AddSpecialist", "SpecialistModel", _UserName);
            }
            catch (Exception _error)
            {
                response = false;
                _log._logError(_error.Message, _error.StackTrace, "AddSpecialist", "SpecialistModel", _UserName);
            }
            return response;
        }

        public bool UpdateSpecialist(string _IDSpecialist, string _Name, string _LastName, string _Phone, string _Email, string _Address, string _City, string _State, string _ZipCode, string _HourlyRate, string Specialty, string _UserName)
        {
            bool response = false;
            try
            {
                using (var context = new ApplicationDbContext())
                {
                    using (SqlCommand cmd = new SqlCommand("SP_Specialist_Update", context.Connection))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@IDSpecialist", _IDSpecialist);
                        cmd.Parameters.AddWithValue("@Name", _Name);
                        cmd.Parameters.AddWithValue("@LastName", _LastName);
                        cmd.Parameters.AddWithValue("@Phone", _Phone);
                        cmd.Parameters.AddWithValue("@Email", _Email);
                        cmd.Parameters.AddWithValue("@Address", _Address);
                        cmd.Parameters.AddWithValue("@City", _City);
                        cmd.Parameters.AddWithValue("@State", _State);
                        cmd.Parameters.AddWithValue("@ZipCode", _ZipCode);
                        cmd.Parameters.AddWithValue("@HourlyRate", _HourlyRate);
                        cmd.Parameters.AddWithValue("@Specialty", Specialty);
                        cmd.Parameters.AddWithValue("@UserU", _UserName);
                        cmd.Parameters.AddWithValue("@DateU", DateTime.Today);
                        context.Connection.Open();
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                        }
                        response = true;
                    }
                }
                _log._logAction("Update Specialist", "Update Specialist, name:" + Name + " " + LastName, "UpdateSpecialist", "SpecialistModel", _UserName);
            }
            catch (Exception _error)
            {
                response = false;
                _log._logError(_error.Message, _error.StackTrace, "UpdateSpecialist", "SpecialistModel", _UserName);
            }
            return response;
        }
        public bool DeleteSpecialist(string _IDSpecialist, string _UserName)
        {
            bool response = false;
            try
            {
                using (var context = new ApplicationDbContext())
                {
                    using (SqlCommand cmd = new SqlCommand("SP_Specialist_Delete", context.Connection))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@IDSpecialist", _IDSpecialist);
                        context.Connection.Open();
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                        }
                        response = true;
                    }
                }
                _log._logAction("Delete Specialist", "Delete Specialist, id:" + IDSpecialist, "DeleteSpecialist", "SpecialistModel", _UserName);
            }
            catch (Exception _error)
            {
                response = false;
                _log._logError(_error.Message, _error.StackTrace, "DeleteSpecialist", "SpecialistModel", _UserName);
            }
            return response;
        }
    }
}