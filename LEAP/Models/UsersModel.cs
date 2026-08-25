using LEAP.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web;

namespace LEAP.Models
{
    public class UsersModel
    {
        public int? IDUsers { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Type_User { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string UserC { get; set; }
        public DateTime? DateC { get; set; }
        public string UserU { get; set; }
        public DateTime? DateU { get; set; }
        public bool _Default { get; set; }
        public bool _ErrorCode { get; set; }
        LogModel _log = new LogModel();
        public List<UsersModel> Get_Users()
        {
            var _Languajes_Response = new List<UsersModel>();
            try
            {
                using (var context = new ApplicationDbContext())
                {
                    using (SqlCommand cmd = new SqlCommand("SP_Users_AllData", context.Connection))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        context.Connection.Open();
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                _Languajes_Response.Add(new UsersModel
                                {
                                    IDUsers = Convert.ToInt32(reader["IDUsers"]),
                                    Name = reader["Name"].ToString(),
                                    LastName = reader["LastName"].ToString(),
                                    Phone = reader["Phone"].ToString(),
                                    Type_User = reader["Type_User"].ToString(),
                                    Email = reader["Email"].ToString(),
                                    UserName = reader["UserName"].ToString(),
                                    _Default = Convert.ToBoolean(reader["_Default"])
                                });
                            }
                        }
                    }
                }
            }
            catch (Exception _error)
            {
                _Languajes_Response.Add(new UsersModel { _ErrorCode = true });
            }
            return _Languajes_Response;
        }

        public UsersModel Get_UsersById(int _IdUser)
        {
            var _User_Response = new UsersModel();
            try
            {
                using (var context = new ApplicationDbContext())
                {
                    using (SqlCommand cmd = new SqlCommand("SP_Users_AllDataByID", context.Connection))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@ID", _IdUser);
                        context.Connection.Open();
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                _User_Response.IDUsers = Convert.ToInt32(reader["IDUsers"]);
                                _User_Response.Name = reader["Name"].ToString();
                                _User_Response.LastName = reader["LastName"].ToString();
                                _User_Response.Phone = reader["Phone"].ToString();
                                _User_Response.Type_User = reader["Type_User"].ToString();
                                _User_Response.DateOfBirth = Convert.ToDateTime(reader["DateOfBirth"]);
                                _User_Response.Email = reader["Email"].ToString();
                                _User_Response.UserName = reader["UserName"].ToString();
                                _User_Response._ErrorCode = false;
                            }
                        }
                    }
                }
            }
            catch (Exception _error)
            {
                _User_Response._ErrorCode = true;
            }
            return _User_Response;
        }

        public bool AddUsers(string _Name, string _LastName, DateTime _DOB,string _UserName, string _Phone, string _Email,string _Type, string _Frase, string _User)
        {
            bool response = false;
            try
            {
                byte[] fraseBytes = Encoding.UTF8.GetBytes(_Frase);
                string FraseEncript = Convert.ToBase64String(fraseBytes);

                //byte[] decodedBytes = Convert.FromBase64String(FraseEncript);
                //string originalFrase = Encoding.UTF8.GetString(decodedBytes);


                using (var context = new ApplicationDbContext())
                {
                    using (SqlCommand cmd = new SqlCommand("SP_Users_Create", context.Connection))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@Name", _Name);
                        cmd.Parameters.AddWithValue("@LastName", _LastName);
                        cmd.Parameters.AddWithValue("@DOB", _DOB);
                        cmd.Parameters.AddWithValue("@UserName", _UserName);
                        cmd.Parameters.AddWithValue("@Phone", _Phone);
                        cmd.Parameters.AddWithValue("@Email", _Email);
                        cmd.Parameters.AddWithValue("@Type", _Type);
                        cmd.Parameters.AddWithValue("@Frase", _Frase);
                        cmd.Parameters.AddWithValue("@UserC", _User);
                        cmd.Parameters.AddWithValue("@DateC", DateTime.Today);
                        context.Connection.Open();
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                        }
                        response = true;
                    }
                }
                _log._logAction("Create User", "Create a new user, name:" + _Name, "AddUser", "UsersModel", _UserName);
            }
            catch (Exception _error)
            {
                response = false;
                _log._logError(_error.Message, _error.StackTrace, "AddUser", "UsersModel", _UserName);
            }
            return response;
        }

        public bool UpdateUsers(string id,string _Name, string _LastName, DateTime _DOB,string _UserName, string _Phone, string _Email, string _Type, string _User)
        {
            bool response = false;
            try
            {
                using (var context = new ApplicationDbContext())
                {
                    using (SqlCommand cmd = new SqlCommand("SP_Users_Update", context.Connection))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@ID", id);
                        cmd.Parameters.AddWithValue("@Name", _Name);
                        cmd.Parameters.AddWithValue("@LastName", _LastName);
                        cmd.Parameters.AddWithValue("@DOB", _DOB);
                        cmd.Parameters.AddWithValue("@UserName", _UserName);
                        cmd.Parameters.AddWithValue("@Phone", _Phone);
                        cmd.Parameters.AddWithValue("@Email", _Email);
                        cmd.Parameters.AddWithValue("@Type", _Type);
                        cmd.Parameters.AddWithValue("@UserU ", _User);
                        cmd.Parameters.AddWithValue("@DateU", DateTime.Today);
                        context.Connection.Open();
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                        }
                        response = true;
                    }
                }
                _log._logAction("Create User", "Create a new user, name:" + _Name, "AddUser", "UsersModel", _UserName);
            }
            catch (Exception _error)
            {
                response = false;
                _log._logError(_error.Message, _error.StackTrace, "AddUser", "UsersModel", _UserName);
            }
            return response;
        }


        public bool DeleteUser(string _IDUser, string _UserName)
        {
            bool response = false;
            try
            {
                using (var context = new ApplicationDbContext())
                {
                    using (SqlCommand cmd = new SqlCommand("SP_User_Delete", context.Connection))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@ID", _IDUser);
                        context.Connection.Open();
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                        }
                        response = true;
                    }
                }
                _log._logAction("Delete User", "Delete User, id:" + _IDUser, "DeleteUser", "UserModel", _UserName);
            }
            catch (Exception _error)
            {
                response = false;
                _log._logError(_error.Message, _error.StackTrace, "DeleteUser", "UserModel", _UserName);
            }
            return response;
        }

        public bool ChangeUser(string _IDUser, string _Frase,string _UserName)
        {
            bool response = false;
            try
            {
                byte[] fraseBytes = Encoding.UTF8.GetBytes(_Frase);
                string FraseEncript = Convert.ToBase64String(fraseBytes);
                using (var context = new ApplicationDbContext())
                {
                    using (SqlCommand cmd = new SqlCommand("SP_Users_Change", context.Connection))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@ID", _IDUser);
                        cmd.Parameters.AddWithValue("@Frase", _Frase);
                        context.Connection.Open();
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                        }
                        response = true;
                    }
                }
                _log._logAction("Delete User", "Delete User, id:" + _IDUser, "DeleteUser", "UserModel", _UserName);
            }
            catch (Exception _error)
            {
                response = false;
                _log._logError(_error.Message, _error.StackTrace, "DeleteUser", "UserModel", _UserName);
            }
            return response;
        }


    }
}