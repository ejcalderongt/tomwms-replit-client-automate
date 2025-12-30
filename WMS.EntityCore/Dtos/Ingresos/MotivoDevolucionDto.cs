using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace WMS.EntityCore.Dtos.Ingresos
{
    public class MotivoDevolucionDto
    {
       
        public int IdMotivoDevolucion { get; set; } = 0;
        public int IdEmpresa { get; set; } = 0;
        public int IdPropietario { get; set; } = 0;
        public string Nombre { get; set; } = "";
        public string UserAgr { get; set; } = "";
        public DateTime? FecAgr { get; set; } = null;
        public string UserMod { get; set; } = "";
        public DateTime? FecMod { get; set; } = null;
        public bool Activo { get; set; } = false;
        public bool EsDetalle { get; set; } = false;

    }
}
