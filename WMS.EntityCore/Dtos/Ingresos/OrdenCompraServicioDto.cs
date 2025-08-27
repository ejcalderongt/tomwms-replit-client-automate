namespace WMSWebAPI.Dtos.Ingresos
{
    public class OrdenCompraServicioDto
    {
        public int IdOrdenCompraServicio { get; set; }
        public int IdOrdenCompraEnc { get; set; }

        public int? IdAcuerdo { get; set; }
        public int IdAcuerdoDet { get; set; }

        public string? Observacion { get; set; }
        public string? Codigo_Producto { get; set; }
        public string? Nombre_Servicio { get; set; }

        public int? Unid_Medida { get; set; }
        public string? Nombre_Unidad { get; set; }

        public int? Corre_DetalleAcuerdo { get; set; }
        public int? Corre_CatalogoProductos { get; set; }

        public int? Cantidad { get; set; }

        public string? User_Agr { get; set; }
        public DateTime? Fec_Agr { get; set; }
        public string? User_Mod { get; set; }
        public DateTime? Fec_Mod { get; set; }

        public int? IdPropietarioBodega { get; set; }
    }
}