namespace WMSWebAPI.Dtos.Ingresos
{
    public class OrdenCompraPolizaDto
    {
        public int IdOrdenCompraPol { get; set; }
        public int IdOrdenCompraEnc { get; set; }
        public string? Bl_No { get; set; }
        public string? NoPoliza { get; set; }
        public string? Pto_Descarga { get; set; }
        public string? Viaje_No { get; set; }
        public string? Buque_No { get; set; }
        public string? Remitente { get; set; }
        public DateTime? Fecha_Abordaje { get; set; }
        public string? Destino { get; set; }
        public string? Dir_Destino { get; set; }
        public string? Descripcion { get; set; }
        public string? Po_Number { get; set; }
        public int? Cantidad { get; set; }
        public int? Piezas { get; set; }
        public float? Total_Kgs { get; set; }
        public float? Cbm { get; set; }
        public string? Dua { get; set; }
        public DateTime? Fecha_Poliza { get; set; }
        public string? Pais_Procede { get; set; }
        public float? Tipo_Cambio { get; set; }
        public float? Total_ValorAduana { get; set; }
        public int? Total_Lineas { get; set; }
        public int? Total_Bultos { get; set; }
        public float? Total_Bultos_Peso { get; set; }
        public float? Total_Usd { get; set; }
        public float? Total_Flete { get; set; }
        public float? Total_Seguro { get; set; }
        public string? User_Agr { get; set; }
        public DateTime? Fec_Agr { get; set; }
        public string? User_Mod { get; set; }
        public DateTime? Fec_Mod { get; set; }
        public string? Codigo_Poliza { get; set; }
        public string? Ticket { get; set; }
        public string? Numero_Orden { get; set; }
        public DateTime? Fecha_Aceptacion { get; set; }
        public DateTime? Fecha_Llegada { get; set; }
        public float? Total_Otros { get; set; }
        public int? IdRegimen { get; set; }
        public float? Total_Bultos_Peso_Neto { get; set; }
        public string? Clave_Aduana { get; set; }
        public string? Nit_Imp_Exp { get; set; }
        public string? Clase { get; set; }
        public string? Mod_Transporte { get; set; }
        public float? Total_Liquidar { get; set; }
        public float? Total_General { get; set; }
        public string? Codigo_Barra { get; set; }
        public bool Activo { get; set; }
        public int? IdBodega { get; set; }
    }
}
