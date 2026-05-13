using AsphaltLegendUnitedWiki.Data;
using AsphaltLegendUnitedWiki.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// --- NUEVO: REGISTRAR LA VARIABLE GLOBAL ---
var dbSettings = new DbSettings();
// Intentamos cargar la IP inicial desde el config por si acaso
var initialIp = builder.Configuration.GetConnectionString("DefaultConnection")?.Split(';')[0].Replace("Server=", "");
if (!string.IsNullOrEmpty(initialIp)) dbSettings.ServerIp = initialIp;

builder.Services.AddSingleton(dbSettings);

// --- MODIFICADO: CONEXIÓN DINÁMICA ---
builder.Services.AddDbContext<AsphaltDbContext>((serviceProvider, options) =>
{
    var settings = serviceProvider.GetRequiredService<DbSettings>();
    // Construimos la cadena usando la variable global
    string dynamicConn = $"Server={settings.ServerIp},1433;Database=asphalt_unite_wiki;User Id=colaborador;Password=TuClaveSegura_2026;TrustServerCertificate=True;Connect Timeout=5;";

    options.UseSqlServer(dynamicConn);
});

// 2. AGREGAR SERVICIOS DE SESIÓN (ESTO ES LO QUE TE FALTABA)
builder.Services.AddDistributedMemoryCache(); // Guarda la sesión en la RAM
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(60); // La sesión dura 1 hora
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();

// 3. MAPEO DE ARCHIVOS ESTÁTICOS (CSS, JS, Imágenes)
app.MapStaticAssets();

app.UseRouting();

// 4. ACTIVAR EL USO DE SESIONES (DEBE IR AQUÍ, ANTES DE LA AUTORIZACIÓN)
app.UseSession();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Acceso}/{action=Index}/{id?}")
    .WithStaticAssets();

app.Run();