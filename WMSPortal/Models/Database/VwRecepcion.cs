using System;

#nullable disable

namespace WMSPortal.Models.Database
{
    public partial class VwRecepcion
    {
        public int Código { get; set; }
        public string Propietario { get; set; }
        public string Proveedor { get; set; }
        public string Bodega { get; set; }
        public int NoOrdenCompra { get; set; }
        public string NoDocumentoOc { get; set; }
        public DateTime? Fecha { get; set; }
        public string Estado { get; set; }
        public string TipoTrans { get; set; }
        public string Descripcion { get; set; }
        public string Muelle { get; set; }
        public bool? Activo { get; set; }
        public string UsuarioAgrego { get; set; }
        public DateTime? FechaAgrego { get; set; }
        public string IdTipoTransaccion { get; set; }
        public int IdBodega { get; set; }
    }
}
