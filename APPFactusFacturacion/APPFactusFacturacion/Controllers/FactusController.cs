using APPFactusFacturacion.Data;
using APPFactusFacturacion.DTOS.factus_request;
using APPFactusFacturacion.DTOS.factus_response;
using APPFactusFacturacion.Models;
using APPFactusFacturacion.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Net.Http;
using System.Text.Json;

namespace APPFactusFacturacion.Controllers
{
    public class FactusController : Controller
    {
        private readonly IFactus _factusService;
        private readonly ApplicationDbContext _dBContext;
        private readonly IUser _userService;
        private readonly IAESCrypto256 _aesCryptoService;

        public FactusController(IFactus factusService, ApplicationDbContext dBContext, IUser userService, IAESCrypto256 aesCryptoService)
        {
            _factusService = factusService;
            _dBContext = dBContext;
            _userService = userService;
            _aesCryptoService = aesCryptoService;
        }

        [HttpGet]
        public async Task<FactusMunicipalitiesResponseDTO> GetMunicipalities()
        {
            var apiResponse = await _factusService.GetMunicipalities();

            if (apiResponse == null || !apiResponse.success)
            {
                return new FactusMunicipalitiesResponseDTO { success = false, message = "Error al cargar los municipios", errors = apiResponse?.errors };
            }

            return apiResponse;
        }

        [Authorize]
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

                    ClientEmail = _aesCryptoService.Encrypt(model.customer.email),
                    ClientName = _aesCryptoService.Encrypt(model.customer.names),
                    ClientPhoneNumber = _aesCryptoService.Encrypt(model.customer.phone),
                    

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
            catch (Exception ex)
            {
                return Json(new { success = false, message = $"Error en factura en el sistema, factura creada exitosamente en factus {ex.Message}" });
            }
        }
    }
}
