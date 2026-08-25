using LEAP.Data;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;

namespace LEAP.Models
{
    public class ServiceCoordinatorModel
    {
        
            public int? IDServiceC { get; set; }
            public string IDRegionalcenter { get; set; }
            public string NameC { get; set; }
            public string Tel { get; set; }

          



        public bool _ErrorCode { get; set; }
        LogModel _log = new LogModel();
        public List<ServiceCoordinatorModel> Get_ServiceC()
        {
            var _ServiceC_Response = new List<ServiceCoordinatorModel>();
            try
            {
                using (var context = new ApplicationDbContext())
                {
                    using (SqlCommand cmd = new SqlCommand("SP_serviceC_AllData", context.Connection))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        context.Connection.Open();
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                _ServiceC_Response.Add(new ServiceCoordinatorModel
                                {
                                    IDServiceC = Convert.ToInt32(reader["IDServiceC"]),
                                    IDRegionalcenter = reader["IDRegionalcenter"].ToString(),
                                    NameC = reader["NameC"].ToString(),
                                    Tel = reader["Tel"].ToString(),
                                });
                            }
                        }
                    }
                }
            }
            catch (Exception _error)
            {
                _ServiceC_Response.Add(new ServiceCoordinatorModel { _ErrorCode = true });
            }
            return _ServiceC_Response;
        }
        public List<ServiceCoordinatorModel> Get_ServiceCbyID(int id)
        {
            var _ServiceC_Response = new List<ServiceCoordinatorModel>();
            try
            {
                using (var context = new ApplicationDbContext())
                {
                    using (SqlCommand cmd = new SqlCommand("SP_serviceC_byID", context.Connection))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@id", id);
                        context.Connection.Open();
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                _ServiceC_Response.Add(new ServiceCoordinatorModel
                                {
                                    IDServiceC = Convert.ToInt32(reader["IDServiceC"]),
                                    IDRegionalcenter = reader["IDRegionalcenter"].ToString(),
                                    NameC = reader["NameC"].ToString(),
                                    Tel = reader["Tel"].ToString(),
                                });
                            }
                        }
                    }
                }
            }
            catch (Exception _error)
            {
                _ServiceC_Response.Add(new ServiceCoordinatorModel { _ErrorCode = true });
            }
            return _ServiceC_Response;
        }

        public ServiceCoordinatorModel Get_Services_ByID(int id)
        {
            var ServiceResponse = new ServiceCoordinatorModel();
            try
            {
                using (var context = new ApplicationDbContext())
                {
                    using (SqlCommand cmd = new SqlCommand("SP_serviceC_byID", context.Connection))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@id", id);
                        context.Connection.Open();
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                ServiceResponse.IDServiceC = Convert.ToInt32(reader["IDServiceC"]);
                                    ServiceResponse.IDRegionalcenter = reader["IDRegionalcenter"].ToString();
                                    ServiceResponse.NameC = reader["NameC"].ToString();
                                ServiceResponse.Tel = reader["Tel"].ToString();
                            }
                        }
                    }
                }
            }
            catch (Exception _error)
            {
                ServiceResponse._ErrorCode = true;
            }
                 return ServiceResponse; 
        }
        public bool UpdateS(int IDServiceC, string NameC, string Tel)
        {
            bool response = false;
            try
            {
                using (var context = new ApplicationDbContext())
                {
                    using (SqlCommand cmd = new SqlCommand("SP_ServiceC_Update", context.Connection))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@IDServiceC", IDServiceC);
                        cmd.Parameters.AddWithValue("@NameC", NameC);
                        cmd.Parameters.AddWithValue("@Tel", Tel);
                      
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
        public bool Create(int regional, string NameC, string Tel)
        {
            bool response = false;
            try
            {
                using (var context = new ApplicationDbContext())
                {
                    using (SqlCommand cmd = new SqlCommand("SP_ServiceC_Create1", context.Connection))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@IDServiceC", regional);
                        cmd.Parameters.AddWithValue("@NameC", NameC);
                        cmd.Parameters.AddWithValue("@Tel", Tel);

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