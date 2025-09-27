using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WMSWebAPI.Dtos.Reset_Password
{
    public class Reset_Password_ResponseDto
    {
        public bool Success { get; set; }
        public required string Message { get; set; }
    }
}
