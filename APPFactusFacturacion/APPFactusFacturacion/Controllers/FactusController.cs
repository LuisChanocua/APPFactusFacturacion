using APPFactusFacturacion.Data;
using APPFactusFacturacion.DTOS.factus_request;
using APPFactusFacturacion.DTOS.factus_response;
using APPFactusFacturacion.Models;
using APPFactusFacturacion.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Net.Http;
using System.Text.Json;

namespace APPFactusFacturacion.Controllers
{
    public class FactusController : Controller
    {
        private readonly IFactusAuth _factusService;
        private readonly ApplicationDbContext _dBContext;
        private readonly IUser _userService;

        public FactusController(IFactusAuth factusService, ApplicationDbContext dBContext, IUser userService)
        {
            _factusService = factusService;
            _dBContext = dBContext;
            _userService = userService;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateBill(FactusInvoiceRequestDTO model)
        {
            try
            {
                var apiResponse = await _factusService.RegisterInvoiceAsync(model);

                if (apiResponse == null || !apiResponse.success)
                {
                    return Json(new { success = false, message = "Error al crear la factura en la API", errors = apiResponse?.errors });
                }

                var user = await _userService.GetProfileUserAsync(User.Identity.Name);
                var newBill = new Bill
                {
                    UserId = user.Id,
                    BillIdFactus = apiResponse.data.bill.id,
                    CufeFactus = apiResponse.data.bill.cufe,
                    NumberFactus = apiResponse.data.bill.number,
                    ReferenceCodeFactus = apiResponse.data.bill.reference_code,
                    CreatedAt = DateTime.Now,
                };

                _dBContext.Bills.Add(newBill);
                await _dBContext.SaveChangesAsync();

                return Json(new { success = true, message = "Factura creada exitosamente" });
            }
            catch(Exception ex)
            {
                return Json(new { success = false, message = $"Error en factura factura creada exitosamente {ex.Message}" });
            }           
        }
    }
}
