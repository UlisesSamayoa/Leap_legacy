using LEAP.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LEAP.Models
{
    public class LogModel
    {
        public bool _logAction(string _baseAction, string _fullAction, string _Action, string _Controller, string _user)
        {
            bool response = false;
            try
            {
                using (var context = new ApplicationDbContext())
                {
                    using (SqlCommand cmd = new SqlCommand("Insert_logAction", context.Connection))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@baseAction", _baseAction);
                        cmd.Parameters.AddWithValue("@fullAction", _fullAction);
                        cmd.Parameters.AddWithValue("@Action", _Action);
                        cmd.Parameters.AddWithValue("@Controller", _Controller);
                        cmd.Parameters.AddWithValue("@User", _user);
                        cmd.Parameters.AddWithValue("@Date", DateTime.Today);
                        context.Connection.Open();
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                        }
                        response = true;
                    }
                }
            }
            catch (Exception _error)
            {
                 response = false;
            }
            return response;
        }

        public bool _logError(string _baseError, string _fullError, string _Action, string _Controller, string _user)
        {
            bool response = false;
            try
            {
                using (var context = new ApplicationDbContext())
                {
                    using (SqlCommand cmd = new SqlCommand("Insert_logError", context.Connection))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@baseError", _baseError);
                        cmd.Parameters.AddWithValue("@fullError", _fullError);
                        cmd.Parameters.AddWithValue("@Action", _Action);
                        cmd.Parameters.AddWithValue("@Controller", _Controller);
                        cmd.Parameters.AddWithValue("@User", _user);
                        cmd.Parameters.AddWithValue("@Date", DateTime.Today);
                        context.Connection.Open();
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                        }
                        response = true;
                    }
                }
            }
            catch (Exception _error)
            {
                response = false;
            }
            return response;
        }
    }
}