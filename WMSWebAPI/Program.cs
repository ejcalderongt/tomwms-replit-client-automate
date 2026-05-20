using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi;
using Serilog;
using System.Text;
using WMSWebAPI.Mapping_Profile;
using WMSWebAPI.Services;
using WMSWebAPI.Services.Cambio_Estado;
using WMSWebAPI.Services.Centro_Costo;
using WMSWebAPI.Services.Cliente;
using WMSWebAPI.Services.Ingresos;
using WMSWebAPI.Services.KPI;
using WMSWebAPI.Services.Prefactura;
using WMSWebAPI.Services.Producto.Clasificacion;
using WMSWebAPI.Services.Producto.Familia;
using WMSWebAPI.Services.Producto.Marca;
using WMSWebAPI.Services.Producto.Presentacion;
using WMSWebAPI.Services.Producto.Tipo;
using WMSWebAPI.Services.Producto.Umbas;
using WMSWebAPI.Services.Propietario;
using WMSWebAPI.Services.Proveedor;
using WMSWebAPI.Services.Reset_Password;
using WMSWebAPI.Services.Salidas;

using ISyncIngresosService = WMSWebAPI.Services.Ingresos.ISyncIngresosService;

var builder = WebApplication.CreateBuilder(args);

// ============================================================
// CONFIGURACION GENERAL
// ============================================================

// Connection string
var connectionString = builder.Configuration.GetConnectionString("CST");

// Serilog
var logPath = Path.Combine(AppContext.BaseDirectory, "Logs", "log-.txt");
Log.Logger = new LoggerConfiguration()
    .WriteTo.File(logPath, rollingInterval: RollingInterval.Day)
    .CreateLogger();

builder.Host.UseSerilog();

// Kestrel
builder.WebHost.ConfigureKestrel(options =>
{
    options.Limits.MaxRequestBodySize = 104_857_600; // 100MB
});

// ============================================================
// SERVICIOS BASE
// ============================================================

// DB
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));

// AutoMapper
builder.Services.AddAutoMapper(cfg => { }, typeof(MappingProfile).Assembly);

// Controllers
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

// ============================================================
// INYECCION DE DEPENDENCIAS
// ============================================================

builder.Services.AddScoped<ISyncIngresosService, SyncIngresosService>();
builder.Services.AddScoped<IProductoSyncService, ProductoSyncService>();
builder.Services.AddScoped<ISyncSalidasService, SyncSalidasService>();
builder.Services.AddScoped<IResetPasswordService, ResetPasswordService>();
builder.Services.AddScoped<IProductoClasificacionSyncService, ProductoClasificacionSyncService>();
builder.Services.AddScoped<IProductoMarcaSyncService, ProductoMarcaSyncService>();
builder.Services.AddScoped<IProductoFamiliaSyncService, ProductoFamiliaSyncService>();
builder.Services.AddScoped<IProductoMi3SyncService, ProductoMi3SyncService>();
builder.Services.AddScoped<IClienteMi3SyncService, ClienteMi3SyncService>();
builder.Services.AddScoped<IProductoTipoMi3SyncService, ProductoTipoMi3SyncService>();
builder.Services.AddScoped<IUmbasMi3SyncService, UmbasMi3SyncService>();
builder.Services.AddScoped<IPresentacionMi3SyncService, PresentacionMi3SyncService>();
builder.Services.AddScoped<ISyncProveedorService, SyncProveedorService>();
builder.Services.AddScoped<IKpiReportService, KpiReportService>();
builder.Services.AddScoped<ICentroCostoService, CentroCostoService>();
builder.Services.AddScoped<ICambioEstadoService, CambioEstadoService>();
builder.Services.AddScoped<IAjustesEnvioService, AjustesEnvioService>();
builder.Services.AddScoped<IPrefacturaService, PrefacturaService>();
builder.Services.AddScoped<IPropietarioService, PropietarioService>();

// ============================================================
// JWT
// Compatible con AuthController existente:
// AuthController genera token con JwtSettings:*
// Aquí validamos primero con JwtSettings:* y dejamos fallback a Jwt:*
// ============================================================

var jwtKey =
    builder.Configuration["JwtSettings:Key"]
    ?? builder.Configuration["Jwt:Key"]
    ?? "OPaVvHGoW1WqtwoFdS0er9cC1RMrSCxd5ovsEYw22uzKlsyaO-7uOQB16jL3YnKsLB4U_BX5gWNUk0ELXMsEtg";

var jwtIssuer =
    builder.Configuration["JwtSettings:Issuer"]
    ?? builder.Configuration["Jwt:Issuer"]
    ?? "WMSWebAPI";

var jwtAudience =
    builder.Configuration["JwtSettings:Audience"]
    ?? builder.Configuration["Jwt:Audience"]
    ?? "WMSWebAPIUsers";

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.RequireHttpsMetadata = false;
    options.SaveToken = true;

    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(jwtKey)),

        ValidateIssuer = true,
        ValidIssuer = jwtIssuer,

        ValidateAudience = true,
        ValidAudience = jwtAudience,

        ValidateLifetime = true,
        ClockSkew = TimeSpan.Zero
    };
});

// ============================================================
// SWAGGER
// ============================================================

builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "WMSWebAPI",
        Version = "v1"
    });

    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "JWT Authorization header usando el esquema Bearer. Ej: \"Authorization: Bearer {token}\"",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.Http,
        Scheme = "bearer",
        BearerFormat = "JWT"
    });

    options.AddSecurityRequirement(document => new OpenApiSecurityRequirement
    {
        [new OpenApiSecuritySchemeReference("Bearer", document)] = []
    });
});

// ============================================================
// CORS
// ============================================================

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend", policy =>
    {
        policy.WithOrigins(
                "http://localhost:5000",
                "https://rocket.new",
                "https://1da7a6d0-45be-4c44-bd76-6384ef21d0f0-00-1zyv481h72tzi.riker.replit.dev"
            )
            .AllowAnyHeader()
            .AllowAnyMethod()
            .AllowCredentials();
    });
});

// ============================================================
// APP
// ============================================================

var app = builder.Build();

// Validación de mapeos al arrancar
using (var scope = app.Services.CreateScope())
{
    var logger = scope.ServiceProvider
        .GetRequiredService<ILoggerFactory>()
        .CreateLogger("AutoMapper");

    AutoMapperDiagnostics.ValidateAndReport(scope.ServiceProvider, logger);
}

using (var scope = app.Services.CreateScope())
{
    var mapper = scope.ServiceProvider.GetRequiredService<IMapper>();
    mapper.ConfigurationProvider.AssertConfigurationIsValid();
}

// Base path solo en producción
string? pathBase = null;

if (!app.Environment.IsDevelopment())
{
    pathBase = "/WMSWEB";
    app.UsePathBase(pathBase);
}

// Exception page solo en desarrollo
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

app.UseHttpsRedirection();

app.UseSwagger();
app.UseSwaggerUI(options =>
{
    if (string.IsNullOrEmpty(pathBase))
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "WMS API v1");
    }
    else
    {
        options.SwaggerEndpoint($"{pathBase}/swagger/v1/swagger.json", "WMS API v1");
    }

    options.RoutePrefix = "swagger";
});

app.UseRouting();
app.UseCors("AllowFrontend");
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.Run();