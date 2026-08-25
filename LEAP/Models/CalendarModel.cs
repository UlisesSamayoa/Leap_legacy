using LEAP.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace LEAP.Models
{
    public class CalendarModel
    {
        public int EventoId { get; set; }
        public string title { get; set; }
        public string start { get; set; }
        public string start_o { get; set; }
        public string end { get; set; }
        public string description { get; set; }
        public string color { get; set; }
        public bool _ErrorCode { get; set; }
        public List<CalendarModel> Get_Events()
        {
            var _RCenter_Response = new List<CalendarModel>();
            try
            {
                using (var context = new ApplicationDbContext())
                {
                    using (SqlCommand cmd = new SqlCommand("SP_Events_allData", context.Connection))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        context.Connection.Open();
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                _RCenter_Response.Add(new CalendarModel
                                {
                                    EventoId = Convert.ToInt32(reader["IDEvent"]),
                                    title = reader["Title"].ToString(),
                                    start = Convert.ToDateTime(reader["DateEvent"]).ToString("yyyy-MM-ddTHH:mm:ss"),
                                    start_o = Convert.ToDateTime(reader["DateEvent"]).ToString("yyyy-MM-dd"),
                                    end = Convert.ToDateTime(reader["DateEvent"]).ToString("yyyy-MM-ddTHH:mm:ss"),
                                    //start = e.FechaInicio.ToString("yyyy-MM-ddTHH:mm:ss"),
                                    //end = e.FechaFin.ToString("yyyy-MM-ddTHH:mm:ss"),
                                    description = reader["Description"].ToString(),
                                    color = reader["Color"].ToString(),
                                    _ErrorCode = false
                                });
                            }
                        }
                    }
                }
            }
            catch (Exception _error)
            {
                _RCenter_Response.Add(new CalendarModel { _ErrorCode = true });
            }
            return _RCenter_Response;
        }
    }
}