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
using APPFactusFacturacion.DTOS;
using Microsoft.IdentityModel.Tokens;


namespace APPFactusFacturacion.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IFactus _factusService;
        private readonly IHome _homeService;

        public HomeController(ILogger<HomeController> logger, IFactus factusService, IHome homeService)
        {
            _logger = logger;
            _factusService = factusService;
            _homeService = homeService;
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

        #region APIS
        
        [HttpGet]
        public async Task<IActionResult> GetBills()
        {
            var bills = await _homeService.GetBills();
            return Json(bills);
        }

        #endregion

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
