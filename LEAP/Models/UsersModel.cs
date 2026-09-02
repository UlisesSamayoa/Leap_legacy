using LEAP.Data;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

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
            var _Users_Response = new List<UsersModel>();
            try
            {
                _Users_Response = ApiClient.Get<List<UsersModel>>("users");
            }
            catch (Exception)
            {
                _Users_Response.Add(new UsersModel { _ErrorCode = true });
            }
            return _Users_Response;
        }

        public UsersModel Get_UsersById(int _IdUser)
        {
            var _User_Response = new UsersModel();
            try
            {
                _User_Response = ApiClient.Get<UsersModel>("users/" + _IdUser);
                _User_Response._ErrorCode = false;
            }
            catch (Exception)
            {
                _User_Response._ErrorCode = true;
            }
            return _User_Response;
        }

        // "_Frase" era la clave en texto plano (con un Base64 sin efecto real
        // en el original) — ahora viaja tal cual por HTTPS y leap_api la
        // guarda hasheada con bcrypt, nunca en texto plano.
        public bool AddUsers(string _Name, string _LastName, DateTime _DOB, string _UserName, string _Phone, string _Email, string _Type, string _Frase, string _User)
        {
            bool response = false;
            try
            {
                ApiClient.Post<UsersModel>("users", new
                {
                    Name = _Name,
                    LastName = _LastName,
                    DateOfBirth = _DOB.ToString("yyyy-MM-dd"),
                    UserName = _UserName,
                    Phone = _Phone,
                    Email = _Email,
                    Type_User = _Type,
                    password = _Frase,
                });
                response = true;
                _log._logAction("Create User", "Create a new user, name:" + _Name, "AddUser", "UsersModel", _UserName);
            }
            catch (Exception _error)
            {
                response = false;
                _log._logError(_error.Message, _error.StackTrace, "AddUser", "UsersModel", _UserName);
            }
            return response;
        }

        public bool UpdateUsers(string id, string _Name, string _LastName, DateTime _DOB, string _UserName, string _Phone, string _Email, string _Type, string _User)
        {
            bool response = false;
            try
            {
                ApiClient.Put<UsersModel>("users/" + id, new
                {
                    Name = _Name,
                    LastName = _LastName,
                    DateOfBirth = _DOB.ToString("yyyy-MM-dd"),
                    UserName = _UserName,
                    Phone = _Phone,
                    Email = _Email,
                    Type_User = _Type,
                });
                response = true;
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
                ApiClient.Delete("users/" + _IDUser);
                response = true;
                _log._logAction("Delete User", "Delete User, id:" + _IDUser, "DeleteUser", "UserModel", _UserName);
            }
            catch (Exception _error)
            {
                response = false;
                _log._logError(_error.Message, _error.StackTrace, "DeleteUser", "UserModel", _UserName);
            }
            return response;
        }

        public bool ChangeUser(string _IDUser, string _Frase, string _UserName)
        {
            bool response = false;
            try
            {
                ApiClient.Patch<object>("users/" + _IDUser + "/password", new { password = _Frase });
                response = true;
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
