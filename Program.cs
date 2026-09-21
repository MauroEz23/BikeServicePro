using BikeServicePro.Helpers;
using BikeServicePro.Repositories.Implementations.JSON;
using BikeServicePro.Repositories.Interfaces;
using BikeServicePro.Services.Implementations;
using BikeServicePro.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// ===== DEPENDENCY INJECTION =====
// Registro de Repositorios (JSON Implementation)
builder.Services.AddScoped<IClienteRepository, JsonClienteRepository>();
builder.Services.AddScoped<IBicicletaRepository, JsonBicicletaRepository>();
builder.Services.AddScoped<IReparacionRepository, JsonReparacionRepository>();
builder.Services.AddScoped<IInventarioRepository, JsonInventarioRepository>();
builder.Services.AddScoped<IUsuarioRepository, JsonUsuarioRepository>();
builder.Services.AddScoped<IMecanicoRepository, JsonMecanicoRepository>();
builder.Services.AddScoped<IServicioRepository, JsonServicioRepository>();

// Registro de Servicios de Negocio
builder.Services.AddScoped<IClienteService, ClienteService>();
builder.Services.AddScoped<IBicicletaService, BicicletaService>();
builder.Services.AddScoped<IReparacionService, ReparacionService>();
builder.Services.AddScoped<IInventarioService, InventarioService>();
builder.Services.AddScoped<IUsuarioService, UsuarioService>();
builder.Services.AddScoped<IReporteService, ReporteService>();
builder.Services.AddScoped<IMecanicoService, MecanicoService>();
builder.Services.AddScoped<IServicioService, ServicioService>();

// Configuración de Session (para autenticación)
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

var app = builder.Build();

// ===== INICIALIZACIÓN DE DATOS =====
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        DataInitializer.Initialize(services).Wait();
    }
    catch (Exception ex)
    {
        var logger = services.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "Error al inicializar los datos de ejemplo.");
    }
}

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseSession();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();