using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using WMS.EntityCore.Log;
using WMS.EntityCore.Propietario;
using WMSWebAPI.Dtos.Login;
using WMSWebAPI.Dtos.Reset_Password;
using WMSWebAPI.Services.Reset_Password;


namespace WMSWebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IConfiguration _config;
        private readonly IResetPasswordService _service;

        public AuthController( IConfiguration config, IResetPasswordService service)
        {
            _config = config;
            _service = service;

        }        

        [HttpPost("login-propietario")]
        [AllowAnonymous]
        public IActionResult Login_proietario([FromBody] LoginDto dto)
        {

            // Capturamos datos para log de sesión
            string? ip = Request.Headers["X-Forwarded-For"].FirstOrDefault();

            if (string.IsNullOrEmpty(ip))
            {
                ip = HttpContext.Connection.RemoteIpAddress?.ToString();
            }
            if (string.IsNullOrEmpty(ip) || ip == "::1")
            {
                ip = "null";
            }

            string userAgent = Request.Headers["User-Agent"].ToString();
            DateTime Fecha = DateTime.UtcNow;

            clsBePropietarios propietario = new clsBePropietarios();
            clsBeLog_error_wms log_error = new clsBeLog_error_wms();
            
            bool esValido = clsLnPropietarios.EsPropietarioValido(_config, dto.Username, dto.Password, ref propietario);


            // Creamos objeto de logportalux
            var logDto = new clsBeLog_portal_ux
            {
                Idpropietario = propietario.IdPropietario,
                Usuario = dto.Username,
                Email = propietario.Email,
                IPAddress = ip,
                UserAgent = userAgent,
                Fecha = Fecha,
                Acceso = esValido,
                UrlAcceso = "Login-propietario",
                MensajeError = esValido ? "acceso correcto": "acceso incorrecto",

            };

            //#GT: el método no retorna excepciones para evitar romper el proceso de login
            clsLnLog_portal_ux._InsertOrUpdate(_config, logDto);
            

            if (!esValido)
            {
                string MsgError = "controlUx: Credenciales incorrectas o propietario inactivo. " + dto.Username;
                //clsLnLog_error_wms.Agregar_Error(_config, MsgError); #EJC20250825: si hay error de base de datos no se puede grabar el log y el mensaje de Unauthorized nunca llega.
                return Unauthorized(new { mensaje = "Credenciales incorrectas o propietario inactivo." });
            }

            // Validamos que las configuraciones necesarias no sean nulas
            var keyString = _config["JwtSettings:Key"];
            var issuer = _config["JwtSettings:Issuer"];
            var audience = _config["JwtSettings:Audience"];

            if (string.IsNullOrEmpty(keyString) || string.IsNullOrEmpty(issuer) || string.IsNullOrEmpty(audience))
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { mensaje = "Configuración de JWT no válida." });
            }

            //string MsgLogin = "controlUx: Inicio de sesión exitoso. " + dto.Username;
            //clsLnLog_error_wms.Agregar_Error(_config, MsgLogin);


            var key = Encoding.ASCII.GetBytes(keyString);

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, dto.Username),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.Name, dto.Username),
                new Claim("rol", "admin")
            };

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddHours(4),
                Issuer = issuer,
                Audience = audience,
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var tokenString = tokenHandler.WriteToken(token);

            // Devolvemos el token y la información del propietario
            return Ok(new
            {
                token = tokenString,
                propietario = new
                {
                    propietario.IdPropietario,
                    propietario.Nombre_comercial,
                    propietario.Activo
                }
            });
        }


        [HttpPost("reset-password")]
        [AllowAnonymous]
        public IActionResult ResetPassword([FromBody] Email_Reset_PasswordDto dto)
        {
            if (dto == null)
            {
                return BadRequest("El Email no puede ser nulo.");
            }

            string? connectionString = _config.GetConnectionString("CST");
            if (string.IsNullOrEmpty(connectionString))
            {
                return StatusCode(500, new { Exito = false, Mensaje = "La cadena de conexión no está configurada." });
            }

            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();

                using (var transaction = connection.BeginTransaction())
                {
                    try
                    {
                        bool exito = _service.Confirmar_Email(dto, connection, transaction);

                        if (!exito)
                        {
                            transaction.Rollback();
                            return Unauthorized(new { mensaje = "Solicitud no procesada." });
                        }


                        transaction.Commit();

                        return Ok(new { Exito = true, Mensaje = "Solicitud de restablecimiento de password procesada correctamente." });
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();

                        var showStackTrace = _config.GetValue<bool>("MostrarDetallesErrores");
                        return StatusCode(500, new
                        {
                            Exito = false,
                            Mensaje = ex.Message,
                            Detalles = showStackTrace ? ex.ToString() : null
                        });
                    }
                }
            }
        }


        [HttpGet("validate-reset-token")]
        [AllowAnonymous]
        public IActionResult ValidateResetToken([FromQuery] string token)
        {

            // Capturamos datos para log de sesión
            string? ip = Request.Headers["X-Forwarded-For"].FirstOrDefault();

            if (string.IsNullOrEmpty(ip))
            {
                ip = HttpContext.Connection.RemoteIpAddress?.ToString();
            }
            if (string.IsNullOrEmpty(ip) || ip == "::1")
            {
                ip = "null";
            }

            string userAgent = Request.Headers["User-Agent"].ToString();
            DateTime Fecha = DateTime.UtcNow;

            if (string.IsNullOrEmpty(token))
            {
                return BadRequest("Proceso invalido.");
            }

            bool esValido = clsLnReset_portal_ux.EsTokenValido(_config, token);

            // Creamos objeto de logportalux
            var logDto = new clsBeLog_portal_ux
            {
                Idpropietario = 0,
                Usuario = token ,
                Email = "",
                IPAddress = ip,
                UserAgent = userAgent,
                Fecha = DateTime.Now,
                Acceso = esValido,
                UrlAcceso = "validate-reset-token",
                MensajeError = esValido ? "acceso correcto" : "acceso incorrecto",

            };

            //#GT: el método no retorna excepciones para evitar romper el proceso de login
            //clsLnLog_portal_ux._InsertOrUpdate(_config, logDto);


            if (!esValido)
            {
                return Unauthorized(new { success = false, mensaje = "Token no valido." });
            }

            return Ok(new { success = true, message = "Token válido." });


        }

        [HttpPost("update-password")]
        [AllowAnonymous]
        public IActionResult UpdatePassword([FromBody] Reset_Password_RequestDto dto)
        {
            if (dto == null)
            {
                return BadRequest("La solicitud no puede estar vacia.");
            }

            string? connectionString = _config.GetConnectionString("CST");
            if (string.IsNullOrEmpty(connectionString))
            {
                return StatusCode(500, new { Exito = false, Mensaje = "La cadena de conexión no está configurada." });
            }

            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();

                using (var transaction = connection.BeginTransaction())
                {
                    try
                    {
                        string MsgRespuesta = "";
                        bool exito = _service.ResetPassword(dto, connection, transaction, out MsgRespuesta);

                        if (!exito)
                        {
                            transaction.Rollback();
                            return Unauthorized(new { mensaje = MsgRespuesta });
                        }


                        transaction.Commit();

                        return Ok(new { Exito = true, Mensaje = MsgRespuesta });
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();

                        var showStackTrace = _config.GetValue<bool>("MostrarDetallesErrores");
                        return StatusCode(500, new
                        {
                            Exito = false,
                            Mensaje = ex.Message,
                            Detalles = showStackTrace ? ex.ToString() : null
                        });
                    }
                }
            }
        }

    }
}