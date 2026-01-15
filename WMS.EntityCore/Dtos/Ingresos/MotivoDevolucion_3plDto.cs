using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace WMS.EntityCore.Dtos.Ingresos
{
    public class MotivoDevolucion_3plDto
    {
        public int IdMotivoDevolucion { get; set; } = 0;
        public int IdEmpresa { get; set; } = 0;
        public int IdPropietario { get; set; } = 0;
        public string Nombre { get; set; } = "";

        // Coinciden con clsBeMotivo_devolucion
        public string User_agr { get; set; } = "";
        public DateTime? Fec_agr { get; set; } = null;
        public string User_mod { get; set; } = "";
        public DateTime? Fec_mod { get; set; } = null;

        public bool Activo { get; set; } = false;
        public bool Es_detalle { get; set; } = false;

        // Propiedades actuales que NO coinciden con el entity (dejarlas en comentario)
        // public string UserAgr { get; set; } = "";
        // public DateTime? FecAgr { get; set; } = null;
        // public string UserMod { get; set; } = "";
        // public DateTime? FecMod { get; set; } = null;
        // public bool EsDetalle { get; set; } = false;
    }

}
