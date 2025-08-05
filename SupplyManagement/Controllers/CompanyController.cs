using System;
using System.Collections.Generic;
using System.Web.Mvc;
using MySql.Data.MySqlClient;
using SupplyManagement.Models;

namespace SupplyManagementApp.Controllers
{
    public class CompanyController : Controller
    {
        // GET: Company
        public ActionResult Index()
        {
            List<Company> companies = new List<Company>();
            DbConnector db = new DbConnector();
            MySqlConnection conn = db.GetConnection();

            try
            {
                conn.Open();
                string query = "SELECT * FROM Companies";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                MySqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    companies.Add(new Company
                    {
                        CompanyId = reader.GetInt32("CompanyId"),
                        CompanyName = reader["CompanyName"] != DBNull.Value ? reader.GetString("CompanyName") : "",
                        Email = reader["Email"] != DBNull.Value ? reader.GetString("Email") : "",
                        PhoneNumber = reader["PhoneNumber"] != DBNull.Value ? reader.GetString("PhoneNumber") : "",
                        LogoUrl = reader["LogoUrl"] != DBNull.Value ? reader.GetString("LogoUrl") : "",
                        IsApproved = reader["IsApproved"] != DBNull.Value && reader.GetBoolean("IsApproved")
                    });

                }
            }
            finally
            {
                conn.Close();
            }

            return View(companies);
        }

        // GET: Company/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Company/Create
        [HttpPost]
        public ActionResult Create(Company company)
        {
            if (ModelState.IsValid)
            {
                DbConnector db = new DbConnector();
                MySqlConnection conn = db.GetConnection();

                try
                {
                    conn.Open();
                    string query = @"INSERT INTO Companies (CompanyName, Email, PhoneNumber, LogoUrl)
                                     VALUES (@CompanyName, @Email, @PhoneNumber, @LogoUrl)";
                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@CompanyName", company.CompanyName);
                    cmd.Parameters.AddWithValue("@Email", company.Email);
                    cmd.Parameters.AddWithValue("@PhoneNumber", company.PhoneNumber);
                    cmd.Parameters.AddWithValue("@LogoUrl", company.LogoUrl);

                    cmd.ExecuteNonQuery();
                    return RedirectToAction("Index");
                }
                finally
                {
                    conn.Close();
                }
            }

            return View(company);
        }
    }
}
