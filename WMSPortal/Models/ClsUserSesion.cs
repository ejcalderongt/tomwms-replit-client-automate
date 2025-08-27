using Microsoft.AspNetCore.Http;

namespace WMSPortal.Models
{
    public class ClsUserSesion
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public int UserId
        {
            get
            {
                return _httpContextAccessor.HttpContext.Session.GetInt32("userId") ?? 0;
            }
        }

        public string UserName
        {
            get
            {
                return _httpContextAccessor.HttpContext.Session.GetString("userName") ?? "";
            }
        }

        public string UserLastName
        {
            get
            {
                return _httpContextAccessor.HttpContext.Session.GetString("userLastName") ?? "";
            }
        }

        public string UserDirection
        {
            get
            {
                return _httpContextAccessor.HttpContext.Session.GetString("userDirection") ?? "";
            }
        }

        public string UserEmail
        {
            get
            {
                return _httpContextAccessor.HttpContext.Session.GetString("userEmail") ?? "";
            }
        }

        public int UserEmpId
        {
            get
            {
                return _httpContextAccessor.HttpContext.Session.GetInt32("userEmpId") ?? 0;
            }
        }

        public int UserRoleId
        {
            get
            {
                return _httpContextAccessor.HttpContext.Session.GetInt32("userRoleId") ?? 0;
            }
        }

        public ClsUserSesion(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }
    }
}
