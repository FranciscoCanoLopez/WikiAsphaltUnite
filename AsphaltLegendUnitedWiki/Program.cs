using AsphaltLegendUnitedWiki.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// 1. CONFIGURAR LA CADENA DE CONEXIÓN
builder.Services.AddDbContext<AsphaltDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

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