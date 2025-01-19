using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using APPFactusFacturacion.Models;
using APPFactusFacturacion.Data;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using APPFactusFacturacion.Comon;
using APPFactusFacturacion.Services;
using APPFactusFacturacion.Services.Interfaces;

namespace APPFactusFacturacion.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<ProfileUser> _userManager;
        private readonly SignInManager<ProfileUser> _signInManager;
        private readonly IAESCrypto256 _aESCrypto256;

        public AccountController(UserManager<ProfileUser> userManager, SignInManager<ProfileUser> signInManager, IAESCrypto256 aESCrypto256)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _aESCrypto256 = aESCrypto256;
        }

        // GET: /Account/Register
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        // POST: /Account/Register
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new ProfileUser
                {
                    UserName = model.Email,
                    Email = model.Email,
                    FirstName = _aESCrypto256.Encrypt(model.FirstName),
                    LastName = _aESCrypto256.Encrypt(model.LastName),
                    CreatedAt = DateTime.Now
                };

                var result = await _userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(user, "Usuario");

                    await _signInManager.SignInAsync(user, isPersistent: false);
                    return Redirect("facturascreadas");
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            return View(model);
        }


        // GET: /Account/Login
        [HttpGet]
        public IActionResult Login(string? returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            return View();
        }

        // POST: /Account/Login
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model, string? returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;

            if (ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, lockoutOnFailure: false);
                if (result.Succeeded)
                {
                    return !string.IsNullOrEmpty(returnUrl) ? Redirect(returnUrl) : Redirect("facturascreadas");
                }

                if (result.IsLockedOut)
                {
                    return View("Lockout");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                }
            }

            return View(model);
        }

        // POST: /Account/Logout
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Login", "Account");
        }
    }
}
