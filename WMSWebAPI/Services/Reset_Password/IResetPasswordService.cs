using Microsoft.Data.SqlClient;
using WMS.EntityCore.Reset_Password;
using WMSWebAPI.Dtos.Reset_Password;

namespace WMSWebAPI.Services.Reset_Password
{
    public interface IResetPasswordService
    {

        bool Confirmar_Email(Email_Reset_PasswordDto EmailDto  ,SqlConnection conn, SqlTransaction tx);
        bool ResetPassword(Reset_Password_RequestDto ResetPassworddto, SqlConnection conn, SqlTransaction tx, out string mensaje);

    }
}
