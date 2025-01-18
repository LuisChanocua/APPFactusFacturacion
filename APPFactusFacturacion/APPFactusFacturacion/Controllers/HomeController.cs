using APPFactusFacturacion.Models;
using APPFactusFacturacion.Services;
using APPFactusFacturacion.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Globalization;
using System.Text.Json;
using Newtonsoft.Json.Serialization;
using Newtonsoft.Json.Converters;
using System.Net.Http.Json;
using Microsoft.AspNetCore.Authorization;

namespace APPFactusFacturacion.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IFactusAuth _factusAuth;

        public HomeController(ILogger<HomeController> logger, IFactusAuth factusAuth)
        {
            _logger = logger;
            _factusAuth = factusAuth;
        }

        #region VIEWS
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        } 
        
        public IActionResult CreateBill()
        {
            return View();
        }
        public IActionResult BillsCreated()
        {
            return View();
        }
        #endregion

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        #region GET REPORT
        [HttpGet]
        public JsonResult GetReport(string typereport)
        {
            try
            {
                var data = new { Name = "Juan", Age = 25 };
                return new JsonResult(data);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
        #endregion
    }
}
