using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Serilog;
using System.Text;
using WMSWebAPI.Services;
using WMSWebAPI.Services.Ingresos;
using WMSWebAPI.Services.LogPortalUx;
using WMSWebAPI.Services.Producto;
using WMSWebAPI.Services.Reset_Password;
using WMSWebAPI.Services.Salidas;

var builder = WebApplication.CreateBuilder(args);

// Connection string
var connectionString = builder.Configuration.GetConnectionString("CST");

// Serilog
var logPath = Path.Combine(AppContext.BaseDirectory, "Logs", "log-.txt");
Log.Logger = new LoggerConfiguration()
    .WriteTo.File(logPath, rollingInterval: RollingInterval.Day)
    .CreateLogger();
builder.Host.UseSerilog();

// Kestrel
builder.WebHost.ConfigureKestrel(o =>
{
    o.Limits.MaxRequestBodySize = 104_857_600; // 100MB
});

// DB
builder.Services.AddDbContext<ApplicationDbContext>(x => x.UseSqlServer(connectionString));

// AutoMapper 15.0.1 (firma nueva)
builder.Services.AddAutoMapper(cfg => { }, typeof(MappingProfile).Assembly);

// Servicios
builder.Services.AddScoped<ISyncIngresosService, SyncIngresosService>();
builder.Services.AddScoped<IProductoSyncService, ProductoSyncService>();
builder.Services.AddScoped<ISyncSalidasService, SyncSalidasService>();
builder.Services.AddScoped<IResetPasswordService, ResetPasswordService>();
builder.Services.AddScoped<IProductoMhsSyncService, ProductoMhsSyncService>();

// JWT
var key = "OPaVvHGoW1WqtwoFdS0er9cC1RMrSCxd5ovsEYw22uzKlsyaO-7uOQB16jL3YnKsLB4U_BX5gWNUk0ELXMsEtg";
builder.Services.AddAuthentication(x =>
{
    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(x =>
{
    x.RequireHttpsMetadata = false;
    x.SaveToken = true;
    x.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(key)),
        ValidateIssuer = true,
        ValidIssuer = "WMSWebAPI",
        ValidateAudience = true,
        ValidAudience = "WMSWebAPIUsers",
        ValidateLifetime = true,
        ClockSkew = TimeSpan.Zero
    };
});

// Controllers + Swagger
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    // OJO: el nombre del doc es "v1"
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "WMSWebAPI", Version = "v1" });

    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "Bearer" },
                Scheme = "oauth2",
                Name = "Bearer",
                In = ParameterLocation.Header,
            },
            new List<string>()
        }
    });
});

// CORS
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

// ===== App =====
var app = builder.Build();

// Validación de mapeos al arrancar (opcional recomendado)
using (var scope = app.Services.CreateScope())
{
    var mapper = scope.ServiceProvider.GetRequiredService<IMapper>();
    mapper.ConfigurationProvider.AssertConfigurationIsValid();
}

// Base path: SOLO en producción
string? pathBase = null;
if (!app.Environment.IsDevelopment())
{
    pathBase = "/WMSWEB";
    app.UsePathBase(pathBase);
}

// Dev exception page sólo en dev
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

app.UseHttpsRedirection();

app.UseSwagger();
app.UseSwaggerUI(c =>
{
    // Si hay base path, úsalo en el endpoint; si no, usa la ruta absoluta estándar.
    if (string.IsNullOrEmpty(pathBase))
    {
        // Development -> UI en /swagger, JSON en /swagger/v1/swagger.json
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "WMS API v1");
    }
    else
    {
        // Producción -> UI en /WMSWEB/swagger, JSON en /WMSWEB/swagger/v1/swagger.json
        c.SwaggerEndpoint($"{pathBase}/swagger/v1/swagger.json", "WMS API v1");
    }

    c.RoutePrefix = "swagger"; // UI en {base}/swagger
});

app.UseRouting();
app.UseCors("AllowFrontend");
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
