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

namespace APPFactusFacturacion.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IFactus _factusService;
        private readonly ApplicationDbContext _dBContext;

        public HomeController(ILogger<HomeController> logger, IFactus factusService, ApplicationDbContext dbContext)
        {
            _logger = logger;
            _factusService = factusService;
            _dBContext = dbContext;
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

        [HttpPost]
        public async Task<IActionResult> CreateBill(InvoiceViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return Json(new { success = false, message = "Modelo inválido", errors = ModelState.Values.SelectMany(v => v.Errors) });
            }

            var invoiceRequest = new InvoiceRequestDTO
            {
                ReferenceCode = model.ReferenceCode,
                Observation = model.Observation,
                PaymentForm = model.PaymentForm,
                PaymentDueDate = model.PaymentDueDate,
                PaymentMethodCode = model.PaymentMethodCode,
                BillingPeriod = new BillingPeriodDTO
                {
                    StartDate = model.BillingPeriod.StartDate,
                    StartTime = model.BillingPeriod.StartTime,
                    EndDate = model.BillingPeriod.EndDate,
                    EndTime = model.BillingPeriod.EndTime
                },
                Customer = new CustomerDTO
                {
                    Identification = model.Customer.Identification,
                    Dv = model.Customer.Dv,
                    Company = model.Customer.Company,
                    TradeName = model.Customer.TradeName,
                    Names = model.Customer.Names,
                    Address = model.Customer.Address,
                    Email = model.Customer.Email,
                    Phone = model.Customer.Phone,
                    LegalOrganizationId = model.Customer.LegalOrganizationId,
                    TributeId = model.Customer.TributeId,
                    IdentificationDocumentId = model.Customer.IdentificationDocumentId,
                    MunicipalityId = model.Customer.MunicipalityId
                },
                Items = model.Items.Select(item => new InvoiceItemDTO
                {
                    CodeReference = item.CodeReference,
                    Name = item.Name,
                    Quantity = item.Quantity,
                    Price = item.Price,
                    DiscountRate = item.DiscountRate,
                    TaxRate = item.TaxRate,
                    UnitMeasureId = item.UnitMeasureId,
                    StandardCodeId = item.StandardCodeId,
                    IsExcluded = item.IsExcluded,
                    TributeId = item.TributeId,
                    WithholdingTaxes = item.WithholdingTaxes.Select(tax => new WithholdingTaxDTO
                    {
                        Code = tax.Code,
                        WithholdingTaxRate = tax.WithholdingTaxRate
                    }).ToList()
                }).ToList()
            };

            var apiResponse = await _factusService.RegisterInvoiceAsync(invoiceRequest);

            if (apiResponse == null || !apiResponse.success)
            {
                return Json(new { success = false, message = "Error al crear la factura en la API", errors = apiResponse?.errors });
            }

            var user = ValidateUser();
            var newBill = new Bill
            {
                CreatedAt = DateTime.Now,
                UserId = user.Id,
                BillLink =apiResponse.data.bill.cufe
            };

            _dBContext.Bills.Add(newBill);
            await _dBContext.SaveChangesAsync();

            return Json(new { success = true, message = "Factura creada exitosamente" });
        }

        private IdentityUser ValidateUser()
        {
            string userEmail = User.Identity.Name;
            var currentUser = _dBContext.Users.Where(x => x.Email == userEmail).FirstOrDefault();

            if (currentUser == null)
            {
                throw new Exception("Usuario no encontrado.");
            }

            return currentUser;
        }
    }
}
