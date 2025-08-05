using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace SupplyManagement.Models
{
    public class DbConnector
    {
        private MySqlConnection connection;

        public DbConnector()
        {
            string connStr = ConfigurationManager.ConnectionStrings["MyDbConnection"].ConnectionString;
            connection = new MySqlConnection(connStr);
        }

        public MySqlConnection GetConnection()
        {
            return connection;
        }   
    }
}