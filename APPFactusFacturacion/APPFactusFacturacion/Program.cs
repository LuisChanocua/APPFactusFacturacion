using APPFactusFacturacion.Models;
using APPFactusFacturacion.Data;
using APPFactusFacturacion.Services;
using APPFactusFacturacion.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Newtonsoft;
using System;
using APPFactusFacturacion.Data.Seeders;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

var configuration = new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
    .AddUserSecrets<Program>() // Habilitar secretos
    .Build();

// Configuración de HttpClient para FactusAPI
builder.Services.AddHttpClient("FactusAPI", client =>
{
    client.BaseAddress = new Uri(builder.Configuration["ApiSettings:BaseAddress"]);
    client.DefaultRequestHeaders.Add("Accept", "application/json");
});

// Registro de servicios para FACTUS API
builder.Services.AddScoped<IFactus, FactusService>();
builder.Services.AddTransient<AuthenticatedHttpClientHandlerService>();

//Validations and Utilities Services
builder.Services.AddScoped<IAESCrypto256, AESCrypto256Service>();
builder.Services.AddScoped<IUser, UserService>();
builder.Services.AddScoped<IHome, HomeService>();

//using Newtonsoft;
builder.Services.AddControllers()
    .AddNewtonsoftJson(options =>
    {
        options.SerializerSettings.MaxDepth = 5000;
        options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
    });

//Agregar DbContext con SQL Server
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddIdentity<ProfileUser, IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders();

builder.Services.Configure<IdentityOptions>(options =>
{
    // Configuración de contraseñas
    options.Password.RequireDigit = true;
    options.Password.RequireLowercase = true;
    options.Password.RequireUppercase = false;
    options.Password.RequiredLength = 6;
    options.Password.RequireNonAlphanumeric = false;

    // Configuración de bloqueo de usuario
    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
    options.Lockout.MaxFailedAccessAttempts = 5;
    options.Lockout.AllowedForNewUsers = true;

    // Configuración de usuario
    options.User.RequireUniqueEmail = true;
});

// Configuración de cookies para sesiones
builder.Services.ConfigureApplicationCookie(options =>
{
    options.Cookie.HttpOnly = true;
    options.ExpireTimeSpan = TimeSpan.FromHours(1);
    options.LoginPath = "/Account/Login"; // Ruta de inicio de sesión
    options.LogoutPath = "/Account/Logout"; // Ruta de cierre de sesión
    options.AccessDeniedPath = "/Account/AccessDenied"; // Ruta de acceso denegado
    options.SlidingExpiration = true;
});

// Construir la aplicación
var app = builder.Build();

// Llamar al RoleSeeder al iniciar la aplicación
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        await RoleSeeder.SeedRolesAsync(services); // Ejecutar el RoleSeeder
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Error al sembrar roles: {ex.Message}");
    }
}

//Autenticación FACTUS automática al iniciar la aplicación
using (var scope = app.Services.CreateScope())
{
    var authService = scope.ServiceProvider.GetRequiredService<IFactus>();
    var config = builder.Configuration.GetSection("ApiSettings");

    // Autenticarse con la API
    await authService.AuthenticateAsync(config["clientId"], config["clientSecret"], config["username"], config["password"]);
}

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

#region ROUTES
#region VIEWS
app.MapControllerRoute(
    name: "facturascreadas",
    pattern: "facturascreadas",
    defaults: new { controller = "Home", action = "Index" });

app.MapControllerRoute(
    name: "crearfactura",
    pattern: "crearfactura",
    defaults: new { controller = "Home", action = "CreateBill" });
#endregion

#region FACTUS APIS
app.MapControllerRoute(
    name: "api/getMunicipalities",
    pattern: "api/getMunicipalities",
    defaults: new { controller = "Factus", action = "GetMunicipalities" });

app.MapControllerRoute(
    name: "api/getBills",
    pattern: "api/getBills",
    defaults: new { controller = "Home", action = "GetBills" });
#endregion

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Account}/{action=Login}/{id?}");

#endregion
app.Run();
