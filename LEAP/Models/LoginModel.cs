using LEAP.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace LEAP.Models
{
    public class LoginModel
    {
        public string userID { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string username { get; set; }
        public string Pass_word { get; set; }

        public bool ValidateLogin(string _user, string _pwd)
        {
            bool respose = false;

            try
            {
                byte[] fraseBytes = Encoding.UTF8.GetBytes(_pwd);
                string FraseEncript = Convert.ToBase64String(fraseBytes);
                string _pwd_e = Convert.ToBase64String(Encoding.UTF8.GetBytes(_pwd));
                using (var context = new ApplicationDbContext())
                {
                    using (SqlCommand cmd = new SqlCommand("SP_Login_Validate_Login", context.Connection))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@user", _user);
                        cmd.Parameters.AddWithValue("@pwd", _pwd);
                        //cmd.Parameters.AddWithValue("@pwd", _pwd_e);
                        context.Connection.Open();
                        //var isvalid = cmd.ExecuteReader();
                        using (var reader = cmd.ExecuteReader())
                        {
                            respose = reader.HasRows;
                        }
                        context.Connection.Close();
                    }
                }
            }
            catch (Exception _error)
            {
                respose =  false;
            }


            return respose;
        }
    }
}