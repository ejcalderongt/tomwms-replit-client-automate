
using AppGlobal;
using AutoMapper;
using Microsoft.Data.SqlClient;
using System.Net;
using System.Net.Mail;
using WMS.EntityCore.Propietario;
using WMS.EntityCore.Reset_Password;
using WMSWebAPI.Dtos.Reset_Password;

namespace WMSWebAPI.Services.Reset_Password
{
    public class ResetPasswordService : IResetPasswordService
    {

        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;

        public ResetPasswordService(IConfiguration configuration, IMapper mapper)
        {
            _configuration = configuration;
            _mapper = mapper;
        }

        public bool Confirmar_Email(Email_Reset_PasswordDto EmailDto, SqlConnection conn, SqlTransaction tx)
        {
            try {

                clsBeReset_portal_ux oBeReset_portal_ux = new clsBeReset_portal_ux();
                clsBePropietarios oBePropietario = new clsBePropietarios();

                bool esValido = clsLnPropietarios.EmailValido(_configuration, EmailDto.Email, ref oBePropietario, conn, tx);


                if (!esValido)
                    return false;

               
                var token = Guid.NewGuid().ToString();
                var expiry = DateTime.UtcNow.AddMinutes(30);

                oBeReset_portal_ux.IdReset = clsLnReset_portal_ux.MaxID(_configuration, conn, tx) + 1;
                oBeReset_portal_ux.IdPropietario = oBePropietario.IdPropietario;
                oBeReset_portal_ux.Token = token;
                oBeReset_portal_ux.Used = false;
                oBeReset_portal_ux.Tiempo_Expira = expiry;
                oBeReset_portal_ux.Fec_agr = DateTime.Now;

                var exito = clsLnReset_portal_ux.Insertar(_configuration, oBeReset_portal_ux,conn,tx);

                if (exito == 1)
                {

                    var smtpServer = _configuration["SmtpSettings:Host"];
                    var smtpPort = _configuration.GetValue<int>("SmtpSettings:Port");
                    var enableSsl = _configuration.GetValue<bool>("SmtpSettings:EnableSsl");
                    var senderEmail = _configuration.GetValue<string>("SmtpSettings:FromEmail");
                    var senderName = _configuration.GetValue<string>("SmtpSettings:DisplayName");
                    var senderPassword = _configuration["SmtpSettings:Password"];

                    //var smtpServer = "smtp.ethereal.email";
                    //var smtpPort = 587;
                    //var enableSsl = true;
                    //var senderEmail = "noemy.botsford@ethereal.email";
                    //var senderName = "Soporte Portal UX";
                    //var senderPassword = "NVmw2tj6ydPs86NY8w";

                    using (var smtp = new SmtpClient(smtpServer, smtpPort))
                    {
                        smtp.Credentials = new NetworkCredential(senderEmail, senderPassword);
                        smtp.EnableSsl = enableSsl;

                        var resetLink = $"https://tomwmsreact-dtsolutionsgt.replit.app/new-password?token={token}";

                        var mail = new MailMessage
                        {
                            From = new MailAddress(senderEmail, senderName),
                            Subject = "Recuperación de contraseña",
                            Body = $@"
                            Hola {oBePropietario.Nombre_comercial},<br/><br/>
                            Hemos recibido una solicitud para restablecer tu contraseña.<br/>
                            Haz clic en el siguiente enlace para continuar:<br/><br/>
                            <a href='{resetLink}'>Recuperar contraseña</a><br/><br/>
                            Este enlace expirará en 30 minutos.<br/><br/>
                            Si no solicitaste este cambio, ignora este correo.",
                            IsBodyHtml = true
                        };

                        // destinatario real que puso en el portal
                        mail.To.Add(EmailDto.Email);

                        smtp.Send(mail);
                    }

                }

                return true;

            }
            catch (Exception ex) {
                throw new Exception("Error al procesar Email → " + ex.Message, ex);
            }
        }

        public bool ResetPassword(Reset_Password_RequestDto ResetPassworddto, SqlConnection conn, SqlTransaction tx, out string mensaje)
        {
            mensaje = string.Empty;

            // Buscar token en la tabla
            clsBeReset_portal_ux tokenRecord = new clsBeReset_portal_ux();

             clsLnReset_portal_ux.GetSingle_By_Token(_configuration, ResetPassworddto.Token, ref tokenRecord, conn, tx);

            if (tokenRecord.Token == null)
            {
                mensaje = "Sesión no existe para cambiar de contraseña.";
                return false;
            }

            if (tokenRecord.Used)
            {
                mensaje = "Sesión no valida para cambiar de contraseña";
                return false;
            }

            if (tokenRecord.Tiempo_Expira < DateTime.UtcNow)
            {
                mensaje = "Sesión vencida para cambiar contraseña.";
                return false;
            }

            // Actualizar contraseña del usuario
            clsBePropietarios pPropietario = new clsBePropietarios();
            pPropietario.IdPropietario = tokenRecord.IdPropietario;
            pPropietario.Clave_acceso = clsHelper.Cifrado(ResetPassworddto.NewPassword) ;
            bool actualizado = clsLnPropietarios.Actualizar_Password_Ux(_configuration, pPropietario, conn, tx);

            if (!actualizado)
            {
                mensaje = "No se pudo actualizar la contraseña.";
                return false;
            }

            // Marcar token como usado
            tokenRecord.Used=true;
            clsLnReset_portal_ux.Cerrar_Token(_configuration, tokenRecord, conn, tx);

            mensaje = "Contraseña actualizada correctamente.";
            return true;
        }


    }
}
