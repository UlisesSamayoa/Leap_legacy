using LEAP.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LEAP.Data
{
    public class ApplicationDbContext : DbContext
    {
      
            private readonly SqlConnection _connection;

            public ApplicationDbContext()
            {
                var connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
                _connection = new SqlConnection(connectionString);
            }

            public SqlConnection Connection
            {
                get { return _connection; }
            }
        }
    
}