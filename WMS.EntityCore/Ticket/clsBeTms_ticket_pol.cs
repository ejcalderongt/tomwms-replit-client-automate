namespace WMS.EntityCore.Ticket
{
    public class clsBeTms_ticket_pol : ICloneable
    {
        public int IdTicket { get; set; } = 0;
        public int IdOrdenTmsEnc { get; set; } = 0;
        public string NoPoliza { get; set; } = "";
        public string Dua { get; set; } = "";
        public DateTime Fecha_poliza { get; set; } = DateTime.Now;
        public string Pais_procede { get; set; } = "";
        public double Tipo_cambio { get; set; } = 0.0;
        public double Total_valoraduana { get; set; } = 0.0;
        public double Total_bultos_peso { get; set; } = 0.0;
        public double Total_usd { get; set; } = 0.0;
        public double Total_flete { get; set; } = 0.0;
        public double Total_seguro { get; set; } = 0.0;
        public string User_agr { get; set; } = "";
        public DateTime Fec_agr { get; set; } = DateTime.Now;
        public string User_mod { get; set; } = "";
        public DateTime Fec_mod { get; set; } = DateTime.Now;
        public int IdRegimen { get; set; } = 0;
        public string Clave_aduana { get; set; } = "";
        public string Nit_imp_exp { get; set; } = "";
        public string Clase { get; set; } = "";
        public string Mod_transporte { get; set; } = "";
        public double Total_liquidar { get; set; } = 0.0;
        public double Total_general { get; set; } = 0.0;
        public string Codigo_Barra { get; set; } = "";

        public clsBeTms_ticket_pol()
        {
        }

        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }
}