using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using AzureSample.Models;
using System.Data.SqlClient;

namespace AzureSample.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            var connection = new SqlConnection("server=azurenvg.database.windows.net;database=azuredemonvg;uid=nvgadmin;pwd=Password@123");

            using (connection)
            {
                SqlCommand command = new SqlCommand("SELECT id, name, role FROM dbo.Admin", connection);
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        Console.WriteLine("{0}\t{1}\t{2}", reader.GetInt32(0), reader.GetString(1), reader.GetString(2));
                    }
                }
                else
                {
                    Console.WriteLine("No rows found.");
                }
                reader.Close();
            }

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
