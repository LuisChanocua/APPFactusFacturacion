using APPFactusFacturacion.Models;
using APPFactusFacturacion.Services;
using APPFactusFacturacion.Data;
using APPFactusFacturacion.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Globalization;
using System.Text.Json;
using Newtonsoft.Json.Serialization;
using Newtonsoft.Json.Converters;
using System.Net.Http.Json;
using Microsoft.AspNetCore.Authorization;
using APPFactusFacturacion.DTOS.factus_request;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using APPFactusFacturacion.DTOS.models;

namespace APPFactusFacturacion.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IFactusAuth _factusService;

        public HomeController(ILogger<HomeController> logger, IFactusAuth factusService)
        {
            _logger = logger;
            _factusService = factusService;
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
    }
}
