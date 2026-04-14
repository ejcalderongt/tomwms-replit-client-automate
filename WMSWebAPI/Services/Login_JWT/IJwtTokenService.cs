namespace WMSWebAPI.Services.Login_JWT
{
    public interface IJwtTokenService
    {
        string GenerateToken(string usuario);
    }
}