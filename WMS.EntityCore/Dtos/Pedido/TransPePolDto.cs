namespace WMSWebAPI.Dtos.Pedido
{
    public class TransPePolDto
    {
        public int IdOrdenPedidoPol { get; set; } = 0;
        public int IdOrdenPedidoEnc { get; set; } = 0;
        public string Bl_no { get; set; } = string.Empty;
        public string NoPoliza { get; set; } = string.Empty;
        public string Pto_descarga { get; set; } = string.Empty;
        public string Viaje_no { get; set; } = string.Empty;
        public string Buque_no { get; set; } = string.Empty;
        public string Remitente { get; set; } = string.Empty;
        public DateTime Fecha_abordaje { get; set; } = DateTime.Now;
        public string Destino { get; set; } = string.Empty;
        public string Dir_destino { get; set; } = string.Empty;
        public string Descripcion { get; set; } = string.Empty;
        public string Po_number { get; set; } = string.Empty;
        public int Cantidad { get; set; } = 0;
        public int Piezas { get; set; } = 0;
        public double Total_kgs { get; set; } = 0;
        public double Cbm { get; set; } = 0;
        public string Dua { get; set; } = string.Empty;
        public DateTime Fecha_poliza { get; set; } = DateTime.Now;
        public string Pais_procede { get; set; } = string.Empty;
        public double Tipo_cambio { get; set; } = 0;
        public double Total_valoraduana { get; set; } = 0;
        public int Total_lineas { get; set; } = 0;
        public int Total_bultos { get; set; } = 0;
        public double Total_bultos_peso { get; set; } = 0;
        public double Total_usd { get; set; } = 0;
        public double Total_flete { get; set; } = 0;
        public double Total_seguro { get; set; } = 0;
        public string User_agr { get; set; } = string.Empty;
        public DateTime Fec_agr { get; set; } = DateTime.Now;
        public string User_mod { get; set; } = string.Empty;
        public DateTime Fec_mod { get; set; } = DateTime.Now;
        public string Clave_aduana { get; set; } = string.Empty;
        public string Nit_imp_exp { get; set; } = string.Empty;
        public string Clase { get; set; } = string.Empty;
        public string Mod_transporte { get; set; } = string.Empty;
        public double Total_liquidar { get; set; } = 0;
        public double Total_general { get; set; } = 0;
        public string Codigo_poliza { get; set; } = string.Empty;
        public string Ticket { get; set; } = string.Empty;
        public string Numero_orden { get; set; } = string.Empty;
        public DateTime Fecha_aceptacion { get; set; } = DateTime.Now;
        public DateTime Fecha_llegada { get; set; } = DateTime.Now;
        public double Total_otros { get; set; } = 0;
        public int IdRegimen { get; set; } = 0;
        public bool Activo { get; set; } = false;
        public double Total_bultos_peso_neto { get; set; } = 0;
    }
}